// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Globalization;
using System.Numerics;
using static xFunc.Maths.ThrowHelpers;
using static xFunc.Maths.Tokenization.TokenKind;
using Vector = xFunc.Maths.Expressions.Matrices.Vector;

namespace xFunc.Maths;

/// <summary>
/// The parser for mathematical expressions.
/// </summary>
public partial class Parser : IParser
{
    private readonly IDifferentiator differentiator;
    private readonly ISimplifier simplifier;
    private readonly IConverter converter;

    /// <summary>
    /// Initializes a new instance of the <see cref="Parser"/> class with default implementations of <see cref="IDifferentiator"/> and <see cref="ISimplifier" />.
    /// </summary>
    public Parser()
        : this(new Differentiator(), new Simplifier(), new Converter())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Parser" /> class.
    /// </summary>
    /// <param name="differentiator">The differentiator.</param>
    /// <param name="simplifier">The simplifier.</param>
    /// <param name="converter">The converter.</param>
    public Parser(IDifferentiator differentiator, ISimplifier simplifier, IConverter converter)
    {
        if (differentiator is null)
            ArgNull(ExceptionArgument.differentiator);
        if (simplifier is null)
            ArgNull(ExceptionArgument.simplifier);
        if (converter is null)
            ArgNull(ExceptionArgument.converter);

        this.differentiator = differentiator;
        this.simplifier = simplifier;
        this.converter = converter;
    }

    /// <summary>
    /// Parses the specified function.
    /// </summary>
    /// <param name="expression">The string that contains the functions and operators.</param>
    /// <returns>The parsed expression.</returns>
    public IExpression Parse(string expression)
    {
        if (string.IsNullOrWhiteSpace(expression))
            throw new ArgumentNullException(nameof(expression), Resource.NotSpecifiedFunction);

        var lexer = new Lexer(expression);
        var tokenReader = new TokenReader(ref lexer);

        try
        {
            var exp = ParseStatement(ref tokenReader);
            var token = tokenReader.GetCurrent();
            if (exp is null || !tokenReader.IsEnd || token.IsNotEmpty())
                throw new ParseException(Resource.ErrorWhileParsingTree);

            return exp;
        }
        finally
        {
            tokenReader.Dispose();
        }
    }

    // TODO: to expressions?
    private IExpression? ParseStatement(ref TokenReader tokenReader)
        => ParseBinaryAssign(ref tokenReader) ??
           ParseAssign(ref tokenReader) ??
           ParseDef(ref tokenReader) ??
           ParseUndef(ref tokenReader) ??
           ParseFor(ref tokenReader) ??
           ParseWhile(ref tokenReader) ??
           ParseExpression(ref tokenReader);

    private IExpression? AssignmentKey(ref TokenReader tokenReader)
        => ParseFunctionDeclaration(ref tokenReader) ??
           ParseVariable(ref tokenReader);

    private IExpression? ParseAssign(ref TokenReader tokenReader)
        => tokenReader.Scoped(this, static (Parser parser, ref TokenReader reader) =>
        {
            var left = parser.AssignmentKey(ref reader);
            if (left is null)
                return null;

            if (!reader.Check(AssignOperator))
                return null;

            var right = parser.ParseExpression(ref reader) ??
                        MissingSecondOperand(AssignOperator);

            return new Define(left, right);
        });

    private IExpression? ParseDef(ref TokenReader tokenReader)
    {
        var def = tokenReader.GetCurrent(DefineKeyword);
        if (def.IsEmpty())
            return null;

        if (!tokenReader.Check(OpenParenthesisSymbol))
            MissingOpenParenthesis(def.Kind);

        var key = AssignmentKey(ref tokenReader) ??
                  throw new ParseException(Resource.AssignKeyParseException);

        if (!tokenReader.Check(CommaSymbol))
            MissingComma(key);

        var value = ParseExpression(ref tokenReader) ??
                    throw new ParseException(Resource.DefValueParseException);

        if (!tokenReader.Check(CloseParenthesisSymbol))
            MissingCloseParenthesis(def.Kind);

        return new Define(key, value);
    }

    private IExpression? ParseUndef(ref TokenReader tokenReader)
    {
        var undef = tokenReader.GetCurrent(UndefineKeyword);
        if (undef.IsEmpty())
            return null;

        if (!tokenReader.Check(OpenParenthesisSymbol))
            MissingOpenParenthesis(undef.Kind);

        var key = AssignmentKey(ref tokenReader) ??
                  throw new ParseException(Resource.AssignKeyParseException);

        if (!tokenReader.Check(CloseParenthesisSymbol))
            MissingCloseParenthesis(undef.Kind);

        return new Undefine(key);
    }

    private IExpression? ParseFor(ref TokenReader tokenReader)
    {
        var @for = tokenReader.GetCurrent(ForKeyword);
        if (@for.IsEmpty())
            return null;

        if (!tokenReader.Check(OpenParenthesisSymbol))
            MissingOpenParenthesis(@for.Kind);

        var body = ParseStatement(ref tokenReader) ??
                   throw new ParseException(Resource.ForBodyParseException);

        if (!tokenReader.Check(CommaSymbol))
            MissingComma(body);

        var init = ParseStatement(ref tokenReader) ??
                   throw new ParseException(Resource.ForInitParseException);

        if (!tokenReader.Check(CommaSymbol))
            MissingComma(init);

        var condition = ParseConditionalOrOperator(ref tokenReader) ??
                        throw new ParseException(Resource.ForConditionParseException);

        if (!tokenReader.Check(CommaSymbol))
            MissingComma(condition);

        var iter = ParseStatement(ref tokenReader) ??
                   throw new ParseException(Resource.ForIterParseException);

        if (!tokenReader.Check(CloseParenthesisSymbol))
            MissingCloseParenthesis(@for.Kind);

        return new For(body, init, condition, iter);
    }

    private IExpression? ParseWhile(ref TokenReader tokenReader)
    {
        var @while = tokenReader.GetCurrent(WhileKeyword);
        if (@while.IsEmpty())
            return null;

        if (!tokenReader.Check(OpenParenthesisSymbol))
            MissingOpenParenthesis(@while.Kind);

        var body = ParseStatement(ref tokenReader) ??
                   throw new ParseException(Resource.WhileBodyParseException);

        if (!tokenReader.Check(CommaSymbol))
            MissingComma(body);

        var condition = ParseConditionalOrOperator(ref tokenReader) ??
                        throw new ParseException(Resource.WhileConditionParseException);

        if (!tokenReader.Check(CloseParenthesisSymbol))
            MissingCloseParenthesis(@while.Kind);

        return new While(body, condition);
    }

    private IExpression? ParseFunctionDeclaration(ref TokenReader tokenReader)
        => tokenReader.Scoped(this, static (Parser parser, ref TokenReader reader) =>
        {
            var id = reader.GetCurrent(Id);
            if (id.IsEmpty() || !reader.Check(OpenParenthesisSymbol))
                return null;

            var parameterList = ImmutableArray.CreateBuilder<IExpression>(1);

            var exp = parser.ParseVariable(ref reader);
            if (exp is not null)
            {
                parameterList.Add(exp);

                while (reader.Check(CommaSymbol))
                {
                    exp = parser.ParseVariable(ref reader);
                    if (exp is null)
                        return null;

                    parameterList.Add(exp);
                }
            }

            return reader.Check(CloseParenthesisSymbol)
                ? parser.CreateFunction(id, parameterList.ToImmutableArray())
                : null;
        });

    private IExpression? ParseExpression(ref TokenReader tokenReader)
        => ParseBinaryAssign(ref tokenReader) ??
           ParseTernary(ref tokenReader);

    private IExpression? ParseBinaryAssign(ref TokenReader tokenReader)
        => tokenReader.Scoped(this, static (Parser parser, ref TokenReader reader) =>
        {
            var variable = parser.ParseVariable(ref reader);
            if (variable is null)
                return null;

            var @operator = reader.GetCurrent(MulAssignOperator) ||
                            reader.GetCurrent(DivAssignOperator) ||
                            reader.GetCurrent(AddAssignOperator) ||
                            reader.GetCurrent(SubAssignOperator) ||
                            reader.GetCurrent(LeftShiftAssignOperator) ||
                            reader.GetCurrent(RightShiftAssignOperator);

            if (@operator.IsEmpty())
                return null;

            var exp = parser.ParseExpression(ref reader) ??
                      MissingSecondOperand(@operator.Kind);

            return parser.CreateBinaryAssign(@operator, variable, exp);
        });

    private IExpression? ParseTernary(ref TokenReader tokenReader)
        => tokenReader.Scoped(this, static (Parser parser, ref TokenReader reader) =>
        {
            var condition = parser.ParseConditionalOrOperator(ref reader);
            if (condition is null)
                return null;

            if (!reader.Check(QuestionMarkSymbol))
                return condition;

            var then = parser.ParseExpression(ref reader) ??
                       throw new ParseException(Resource.TernaryThenParseException);

            if (!reader.Check(ColonSymbol))
                throw new ParseException(Resource.TernaryColonParseException);

            var @else = parser.ParseExpression(ref reader) ??
                        throw new ParseException(Resource.TernaryElseParseException);

            return new If(condition, then, @else);
        });

    private IExpression? ParseConditionalOrOperator(ref TokenReader tokenReader)
    {
        var left = ParseConditionalAndOperator(ref tokenReader);
        if (left is null)
            return null;

        while (true)
        {
            var @operator = tokenReader.Check(ConditionalOrOperator);
            if (!@operator)
                return left;

            var right = ParseConditionalAndOperator(ref tokenReader) ??
                        MissingSecondOperand(ConditionalOrOperator);

            left = new ConditionalOr(left, right);
        }
    }

    private IExpression? ParseConditionalAndOperator(ref TokenReader tokenReader)
    {
        var left = ParseBitwiseOperator(ref tokenReader);
        if (left is null)
            return null;

        while (true)
        {
            var @operator = tokenReader.Check(ConditionalAndOperator);
            if (!@operator)
                return left;

            var right = ParseBitwiseOperator(ref tokenReader) ??
                        MissingSecondOperand(ConditionalAndOperator);

            left = new ConditionalAnd(left, right);
        }
    }

    private IExpression? ParseBitwiseOperator(ref TokenReader tokenReader)
    {
        var left = ParseOrOperator(ref tokenReader);
        if (left is null)
            return null;

        while (true)
        {
            var token = tokenReader.GetCurrent(ImplicationOperator) ||
                        tokenReader.GetCurrent(EqualityOperator) ||
                        tokenReader.GetCurrent(NAndKeyword) ||
                        tokenReader.GetCurrent(NOrKeyword) ||
                        tokenReader.GetCurrent(EqKeyword) ||
                        tokenReader.GetCurrent(ImplKeyword);

            if (token.IsEmpty())
                return left;

            var right = ParseOrOperator(ref tokenReader) ??
                        MissingSecondOperand(token.Kind);

            left = CreateBitwiseOperator(token, left, right);
        }
    }

    private IExpression? ParseOrOperator(ref TokenReader tokenReader)
    {
        var left = ParseXOrOperator(ref tokenReader);
        if (left is null)
            return null;

        while (true)
        {
            var token = tokenReader.Check(OrOperator) ||
                        tokenReader.Check(OrKeyword);

            if (!token)
                return left;

            var right = ParseXOrOperator(ref tokenReader) ??
                        MissingSecondOperand(OrOperator);

            left = new Or(left, right);
        }
    }

    private IExpression? ParseXOrOperator(ref TokenReader tokenReader)
    {
        var left = ParseAndOperator(ref tokenReader);
        if (left is null)
            return null;

        while (true)
        {
            var token = tokenReader.Check(XOrKeyword);
            if (!token)
                return left;

            var right = ParseAndOperator(ref tokenReader) ??
                        MissingSecondOperand(XOrKeyword);

            left = new XOr(left, right);
        }
    }

    private IExpression? ParseAndOperator(ref TokenReader tokenReader)
    {
        var left = ParseEqualityOperator(ref tokenReader);
        if (left is null)
            return null;

        while (true)
        {
            var token = tokenReader.Check(AndOperator) ||
                        tokenReader.Check(AndKeyword);

            if (!token)
                return left;

            var right = ParseEqualityOperator(ref tokenReader) ??
                        MissingSecondOperand(AndOperator);

            left = new And(left, right);
        }
    }

    private IExpression? ParseEqualityOperator(ref TokenReader tokenReader)
    {
        var left = ParseRelationalOperator(ref tokenReader);
        if (left is null)
            return null;

        while (true)
        {
            var @operator = tokenReader.GetCurrent(EqualOperator) ||
                            tokenReader.GetCurrent(NotEqualOperator);

            if (@operator.IsEmpty())
                return left;

            var right = ParseRelationalOperator(ref tokenReader) ??
                        MissingSecondOperand(@operator.Kind);

            left = CreateEqualityOperator(@operator, left, right);
        }
    }

    private IExpression? ParseRelationalOperator(ref TokenReader tokenReader)
    {
        var left = ParseShift(ref tokenReader);
        if (left is null)
            return null;

        while (true)
        {
            var @operator = tokenReader.GetCurrent(LessThanOperator) ||
                            tokenReader.GetCurrent(LessOrEqualOperator) ||
                            tokenReader.GetCurrent(GreaterThanOperator) ||
                            tokenReader.GetCurrent(GreaterOrEqualOperator);

            if (@operator.IsEmpty())
                return left;

            var right = ParseShift(ref tokenReader) ??
                        MissingSecondOperand(@operator.Kind);

            left = CreateRelationalOperator(@operator, left, right);
        }
    }

    private IExpression? ParseShift(ref TokenReader tokenReader)
    {
        var left = ParseAddSub(ref tokenReader);
        if (left is null)
            return null;

        while (true)
        {
            var @operator = tokenReader.GetCurrent(LeftShiftOperator) ||
                            tokenReader.GetCurrent(RightShiftOperator);

            if (@operator.IsEmpty())
                return left;

            var right = ParseAddSub(ref tokenReader) ??
                        MissingSecondOperand(@operator.Kind);

            left = CreateShift(@operator, left, right);
        }
    }

    private IExpression? ParseAddSub(ref TokenReader tokenReader)
    {
        var left = ParseMulDivMod(ref tokenReader);
        if (left is null)
            return null;

        while (true)
        {
            var @operator = tokenReader.GetCurrent(PlusOperator) ||
                            tokenReader.GetCurrent(MinusOperator);

            if (@operator.IsEmpty())
                return left;

            var right = ParseMulDivMod(ref tokenReader) ??
                        MissingSecondOperand(@operator.Kind);

            left = CreateAddSub(@operator, left, right);
        }
    }

    private IExpression? ParseMulDivMod(ref TokenReader tokenReader)
    {
        var left = ParseMulImplicit(ref tokenReader);
        if (left is null)
            return null;

        while (true)
        {
            var token = tokenReader.GetCurrent(MultiplicationOperator) ||
                        tokenReader.GetCurrent(DivisionOperator) ||
                        tokenReader.GetCurrent(ModuloOperator) ||
                        tokenReader.GetCurrent(ModKeyword);

            if (token.IsEmpty())
                return left;

            var right = ParseMulImplicit(ref tokenReader) ??
                        MissingSecondOperand(token.Kind);

            left = CreateMulDivMod(token, left, right);
        }
    }

    private IExpression? ParseMulImplicit(ref TokenReader tokenReader)
        => ParseMulImplicitLeftUnary(ref tokenReader) ??
           ParseLeftUnary(ref tokenReader);

    private IExpression? ParseMulImplicitLeftUnary(ref TokenReader tokenReader)
        => tokenReader.Scoped(this, static (Parser parser, ref TokenReader reader) =>
        {
            var minusOperator = reader.GetCurrent(MinusOperator);
            var number = parser.ParseNumberAndUnit(ref reader);
            if (number is null)
                return null;

            var rightUnary = parser.ParseMulImplicitExponentiation(ref reader) ??
                             parser.ParseParenthesesExpression(ref reader) ??
                             parser.ParseMatrix(ref reader) ??
                             parser.ParseVector(ref reader);

            if (rightUnary is null)
                return null;

            if (minusOperator.IsNotEmpty())
                number = new UnaryMinus(number);

            return new Mul(number, rightUnary);
        });

    private IExpression? ParseMulImplicitExponentiation(ref TokenReader tokenReader)
    {
        var left = ParseFunctionOrVariable(ref tokenReader);
        if (left is null)
            return null;

        var @operator = tokenReader.GetCurrent(ExponentiationOperator);
        if (@operator.IsEmpty())
            return left;

        var right = ParseExponentiation(ref tokenReader) ??
                    throw new ParseException(Resource.ExponentParseException);

        return new Pow(left, right);
    }

    private IExpression? ParseLeftUnary(ref TokenReader tokenReader)
    {
        var token = tokenReader.GetCurrent(NotOperator) ||
                    tokenReader.GetCurrent(MinusOperator) ||
                    tokenReader.GetCurrent(PlusOperator) ||
                    tokenReader.GetCurrent(NotKeyword);

        var operand = ParseExponentiation(ref tokenReader);
        if (operand is null || token.IsEmpty() || token.Is(PlusOperator))
            return operand;

        if (token.Is(MinusOperator))
            return new UnaryMinus(operand);

        return new Not(operand);
    }

    private IExpression? ParseExponentiation(ref TokenReader tokenReader)
    {
        var left = ParseRightUnary(ref tokenReader);
        if (left is null)
            return null;

        var @operator = tokenReader.GetCurrent(ExponentiationOperator);
        if (@operator.IsEmpty())
            return left;

        var right = ParseLeftUnary(ref tokenReader) ??
                    throw new ParseException(Resource.ExponentParseException);

        return new Pow(left, right);
    }

    private IExpression? ParseRightUnary(ref TokenReader tokenReader)
        => ParseFactorial(ref tokenReader) ??
           ParseIncDec(ref tokenReader) ??
           ParseOperand(ref tokenReader);

    private IExpression? ParseFactorial(ref TokenReader tokenReader)
        => tokenReader.Scoped(this, static (Parser parser, ref TokenReader reader) =>
        {
            var number = parser.ParseNumberAndUnit(ref reader);
            if (number is not null && reader.Check(FactorialOperator))
                return new Fact(number);

            return null;
        });

    private IExpression? ParseIncDec(ref TokenReader tokenReader)
        => tokenReader.Scoped(this, static (Parser parser, ref TokenReader reader) =>
        {
            var variable = parser.ParseVariable(ref reader);
            if (variable is null)
                return null;

            if (reader.Check(IncrementOperator))
                return new Inc(variable);

            if (reader.Check(DecrementOperator))
                return new Dec(variable);

            return variable;
        });

    private IExpression? ParseOperand(ref TokenReader tokenReader)
        => ParsePolarComplexNumber(ref tokenReader) ??
           ParseNumberAndUnit(ref tokenReader) ??
           ParseIf(ref tokenReader) ??
           ParseFunctionOrVariable(ref tokenReader) ??
           ParseBoolean(ref tokenReader) ??
           ParseParenthesesExpression(ref tokenReader) ??
           ParseMatrix(ref tokenReader) ??
           ParseVector(ref tokenReader) ??
           ParseString(ref tokenReader);

    private IExpression? ParseIf(ref TokenReader tokenReader)
    {
        var @if = tokenReader.GetCurrent(IfKeyword);
        if (@if.IsEmpty())
            return null;

        if (!tokenReader.Check(OpenParenthesisSymbol))
            MissingOpenParenthesis(@if.Kind);

        var condition = ParseConditionalOrOperator(ref tokenReader) ??
                        throw new ParseException(Resource.IfConditionParseException);

        if (!tokenReader.Check(CommaSymbol))
            MissingComma(condition);

        var then = ParseExpression(ref tokenReader) ??
                   throw new ParseException(Resource.IfThenParseException);

        IExpression? @else = null;
        if (tokenReader.Check(CommaSymbol))
            @else = ParseExpression(ref tokenReader) ??
                    throw new ParseException(Resource.IfElseParseException);

        if (!tokenReader.Check(CloseParenthesisSymbol))
            MissingCloseParenthesis(@if.Kind);

        if (@else is not null)
            return new If(condition, then, @else);

        return new If(condition, then);
    }

    private IExpression? ParseParenthesesExpression(ref TokenReader tokenReader)
    {
        if (!tokenReader.Check(OpenParenthesisSymbol))
            return null;

        var exp = ParseExpression(ref tokenReader) ??
                  throw new ParseException(Resource.ExpParenParseException);

        if (!tokenReader.Check(CloseParenthesisSymbol))
            throw new ParseException(string.Format(CultureInfo.InvariantCulture, Resource.CloseParenParseException, exp));

        return exp;
    }

    private IExpression? ParseFunctionOrVariable(ref TokenReader tokenReader)
    {
        var function = tokenReader.GetCurrent(Id);
        if (function.IsEmpty())
            return null;

        var parameterList = ParseParameterList(ref tokenReader);
        if (parameterList.IsDefaultOrEmpty)
            return CreateVariable(function);

        return CreateFunction(function, parameterList);
    }

    private ImmutableArray<IExpression> ParseParameterList(ref TokenReader tokenReader)
    {
        if (!tokenReader.Check(OpenParenthesisSymbol))
            return default;

        var parameterList = ImmutableArray.CreateBuilder<IExpression>(1);

        var exp = ParseExpression(ref tokenReader);
        if (exp is not null)
        {
            parameterList.Add(exp);

            while (tokenReader.Check(CommaSymbol))
            {
                exp = ParseExpression(ref tokenReader) ??
                      MissingExpression();

                parameterList.Add(exp);
            }
        }

        if (!tokenReader.Check(CloseParenthesisSymbol))
            throw new ParseException(Resource.ParameterListCloseParseException);

        return parameterList.ToImmutableArray();
    }

    private IExpression? ParseNumberAndUnit(ref TokenReader tokenReader)
    {
        var unit = ParseTemperatureUnit(ref tokenReader) ??
                   ParseAngleUnit(ref tokenReader) ??
                   ParsePowerUnit(ref tokenReader) ??
                   ParseMassUnit(ref tokenReader) ??
                   ParseVolumeUnit(ref tokenReader) ??
                   ParseAreaUnit(ref tokenReader) ??
                   ParseLengthUnit(ref tokenReader) ??
                   ParseTimeUnit(ref tokenReader);
        if (unit is not null)
            return unit;

        var number = tokenReader.GetCurrent(TokenKind.Number);
        if (number.IsNotEmpty())
            return new Number(number.NumberValue);

        return null;
    }

    private IExpression? ParseAngleUnit(ref TokenReader tokenReader)
        => tokenReader.Scoped(this, static (Parser _, ref TokenReader reader) =>
        {
            var number = reader.GetCurrent(TokenKind.Number);
            if (number.IsEmpty())
                return null;

            if (reader.Check(DegreeSymbol))
                return AngleValue.Degree(number.NumberValue).AsExpression();

            var id = reader.GetCurrent(Id);
            if (id.IsEmpty())
                return null;

            return id.StringValue switch
            {
                "deg" or "degree" or "degrees" => AngleValue.Degree(number.NumberValue).AsExpression(),
                "rad" or "radian" or "radians" => AngleValue.Radian(number.NumberValue).AsExpression(),
                "grad" or "gradian" or "gradians" => AngleValue.Gradian(number.NumberValue).AsExpression(),
                _ => null,
            };
        });

    private IExpression? ParsePowerUnit(ref TokenReader tokenReader)
        => tokenReader.Scoped(this, static (Parser _, ref TokenReader reader) =>
        {
            var number = reader.GetCurrent(TokenKind.Number);
            if (number.IsEmpty())
                return null;

            var id = reader.GetCurrent(Id);
            if (id.IsEmpty())
                return null;

            return id.StringValue switch
            {
                "w" => PowerValue.Watt(number.NumberValue).AsExpression(),
                "kw" => PowerValue.Kilowatt(number.NumberValue).AsExpression(),
                "hp" => PowerValue.Horsepower(number.NumberValue).AsExpression(),
                _ => null,
            };
        });

    private IExpression? ParseTemperatureUnit(ref TokenReader tokenReader)
        => tokenReader.Scoped(this, static (Parser _, ref TokenReader reader) =>
        {
            var number = reader.GetCurrent(TokenKind.Number);
            if (number.IsEmpty())
                return null;

            if (reader.Check(DegreeSymbol))
            {
                var id = reader.GetCurrent(Id);
                if (id.IsNotEmpty())
                {
                    if (id.StringValue == "c")
                        return TemperatureValue.Celsius(number.NumberValue).AsExpression();
                    if (id.StringValue == "f")
                        return TemperatureValue.Fahrenheit(number.NumberValue).AsExpression();
                }
            }
            else
            {
                var id = reader.GetCurrent(Id);
                if (id.IsNotEmpty() && id.StringValue == "k")
                    return TemperatureValue.Kelvin(number.NumberValue).AsExpression();
            }

            return null;
        });

    private IExpression? ParseMassUnit(ref TokenReader tokenReader)
        => tokenReader.Scoped(this, static (Parser _, ref TokenReader reader) =>
        {
            var number = reader.GetCurrent(TokenKind.Number);
            if (number.IsEmpty())
                return null;

            var id = reader.GetCurrent(Id);
            if (id.IsEmpty())
                return null;

            return id.StringValue switch
            {
                "mg" => MassValue.Milligram(number.NumberValue).AsExpression(),
                "g" => MassValue.Gram(number.NumberValue).AsExpression(),
                "kg" => MassValue.Kilogram(number.NumberValue).AsExpression(),
                "t" => MassValue.Tonne(number.NumberValue).AsExpression(),
                "oz" => MassValue.Ounce(number.NumberValue).AsExpression(),
                "lb" => MassValue.Pound(number.NumberValue).AsExpression(),
                _ => null,
            };
        });

    private IExpression? ParseLengthUnit(ref TokenReader tokenReader)
        => tokenReader.Scoped(this, static (Parser _, ref TokenReader reader) =>
        {
            var number = reader.GetCurrent(TokenKind.Number);
            if (number.IsEmpty())
                return null;

            var id = reader.GetCurrent(Id);
            if (id.IsEmpty())
                return null;

            return id.StringValue switch
            {
                "m" => LengthValue.Meter(number.NumberValue).AsExpression(),
                "nm" => LengthValue.Nanometer(number.NumberValue).AsExpression(),
                "µm" => LengthValue.Micrometer(number.NumberValue).AsExpression(),
                "mm" => LengthValue.Millimeter(number.NumberValue).AsExpression(),
                "cm" => LengthValue.Centimeter(number.NumberValue).AsExpression(),
                "dm" => LengthValue.Decimeter(number.NumberValue).AsExpression(),
                "km" => LengthValue.Kilometer(number.NumberValue).AsExpression(),
                "in" => LengthValue.Inch(number.NumberValue).AsExpression(),
                "ft" => LengthValue.Foot(number.NumberValue).AsExpression(),
                "yd" => LengthValue.Yard(number.NumberValue).AsExpression(),
                "mi" => LengthValue.Mile(number.NumberValue).AsExpression(),
                "nmi" => LengthValue.NauticalMile(number.NumberValue).AsExpression(),
                "ch" => LengthValue.Chain(number.NumberValue).AsExpression(),
                "rd" => LengthValue.Rod(number.NumberValue).AsExpression(),
                "au" => LengthValue.AstronomicalUnit(number.NumberValue).AsExpression(),
                "ly" => LengthValue.LightYear(number.NumberValue).AsExpression(),
                "pc" => LengthValue.Parsec(number.NumberValue).AsExpression(),
                _ => null,
            };
        });

    private IExpression? ParseTimeUnit(ref TokenReader tokenReader)
        => tokenReader.Scoped(this, static (Parser _, ref TokenReader reader) =>
        {
            var number = reader.GetCurrent(TokenKind.Number);
            if (number.IsEmpty())
                return null;

            var id = reader.GetCurrent(Id);
            if (id.IsEmpty())
                return null;

            return id.StringValue switch
            {
                "s" => TimeValue.Second(number.NumberValue).AsExpression(),
                "ns" => TimeValue.Nanosecond(number.NumberValue).AsExpression(),
                "μs" => TimeValue.Microsecond(number.NumberValue).AsExpression(),
                "ms" => TimeValue.Millisecond(number.NumberValue).AsExpression(),
                "min" => TimeValue.Minute(number.NumberValue).AsExpression(),
                "h" => TimeValue.Hour(number.NumberValue).AsExpression(),
                "day" => TimeValue.Day(number.NumberValue).AsExpression(),
                "week" => TimeValue.Week(number.NumberValue).AsExpression(),
                "year" => TimeValue.Year(number.NumberValue).AsExpression(),
                _ => null,
            };
        });

    private IExpression? ParseAreaUnit(ref TokenReader tokenReader)
        => tokenReader.Scoped(this, static (Parser _, ref TokenReader reader) =>
        {
            var number = reader.GetCurrent(TokenKind.Number);
            if (number.IsEmpty())
                return null;

            var id = reader.GetCurrent(Id);
            if (id.IsEmpty())
                return null;

            var areaValue = id.StringValue switch
            {
                "ha" => AreaValue.Hectare(number.NumberValue).AsExpression(),
                "ac" => AreaValue.Acre(number.NumberValue).AsExpression(),
                _ => null,
            };
            if (areaValue is not null)
                return areaValue;

            areaValue = id.StringValue switch
            {
                "m" => AreaValue.Meter(number.NumberValue).AsExpression(),
                "mm" => AreaValue.Millimeter(number.NumberValue).AsExpression(),
                "cm" => AreaValue.Centimeter(number.NumberValue).AsExpression(),
                "km" => AreaValue.Kilometer(number.NumberValue).AsExpression(),
                "in" => AreaValue.Inch(number.NumberValue).AsExpression(),
                "yd" => AreaValue.Yard(number.NumberValue).AsExpression(),
                "ft" => AreaValue.Foot(number.NumberValue).AsExpression(),
                "mi" => AreaValue.Mile(number.NumberValue).AsExpression(),
                _ => null,
            };
            if (areaValue is null || !reader.Check(ExponentiationOperator))
                return null;

            var exponent = reader.GetCurrent(TokenKind.Number);
            if (exponent.IsEmpty())
                throw new ParseException(Resource.ExponentParseException);

            if (exponent.NumberValue == 2)
                return areaValue.Value.AsExpression();

            return null;
        });

    private IExpression? ParseVolumeUnit(ref TokenReader tokenReader)
        => tokenReader.Scoped(this, static (Parser _, ref TokenReader reader) =>
        {
            var number = reader.GetCurrent(TokenKind.Number);
            if (number.IsEmpty())
                return null;

            var id = reader.GetCurrent(Id);
            if (id.IsEmpty())
                return null;

            var volumeValue = id.StringValue switch
            {
                "gal" => VolumeValue.Gallon(number.NumberValue).AsExpression(),
                "l" => VolumeValue.Liter(number.NumberValue).AsExpression(),
                _ => null,
            };
            if (volumeValue is not null)
                return volumeValue;

            volumeValue = id.StringValue switch
            {
                "m" => VolumeValue.Meter(number.NumberValue).AsExpression(),
                "cm" => VolumeValue.Centimeter(number.NumberValue).AsExpression(),
                "in" => VolumeValue.Inch(number.NumberValue).AsExpression(),
                "yd" => VolumeValue.Yard(number.NumberValue).AsExpression(),
                "ft" => VolumeValue.Foot(number.NumberValue).AsExpression(),
                _ => null,
            };
            if (volumeValue is null || !reader.Check(ExponentiationOperator))
                return null;

            var exponent = reader.GetCurrent(TokenKind.Number);
            if (exponent.IsEmpty())
                throw new ParseException(Resource.ExponentParseException);

            if (exponent.NumberValue == 3)
                return volumeValue.Value.AsExpression();

            return null;
        });

    private IExpression? ParsePolarComplexNumber(ref TokenReader tokenReader)
        => tokenReader.Scoped(this, static (Parser _, ref TokenReader reader) =>
        {
            // plus symbol can be ignored
            reader.GetCurrent(PlusOperator);

            var magnitude = reader.GetCurrent(TokenKind.Number);
            if (magnitude.IsEmpty())
                return null;

            if (!reader.Check(AngleSymbol))
                return null;

            var phaseSign = reader.GetCurrent(PlusOperator) ||
                            reader.GetCurrent(MinusOperator);

            var phase = reader.GetCurrent(TokenKind.Number);
            if (phase.IsEmpty())
                throw new ParseException(Resource.PhaseParseException);

            if (!reader.Check(DegreeSymbol))
                throw new ParseException(Resource.DegreeComplexNumberParseException);

            var magnitudeNumber = magnitude.NumberValue;
            var sign = phaseSign.Is(MinusOperator) ? -1 : 1;
            var phaseNumber = AngleValue.Degree(phase.NumberValue * sign).ToRadian();
            var complex = Complex.FromPolarCoordinates(magnitudeNumber, phaseNumber.Angle.Number);

            return new ComplexNumber(complex);
        });

    private Variable? ParseVariable(ref TokenReader tokenReader)
    {
        var variable = tokenReader.GetCurrent(Id);

        // usually we use 'scope' in such cases, but here we can ignore it,
        // because parsing of variable is always 'scoped'
        // if it is not true anymore, then use 'scope'
        if (variable.IsEmpty() || tokenReader.Check(OpenParenthesisSymbol))
            return null;

        if (variable.StringValue == Variable.X.Name)
            return Variable.X;

        return CreateVariable(variable);
    }

    private IExpression? ParseBoolean(ref TokenReader tokenReader)
    {
        if (tokenReader.Check(TrueKeyword))
            return Bool.True;

        if (tokenReader.Check(FalseKeyword))
            return Bool.False;

        return null;
    }

    private Vector? ParseVector(ref TokenReader tokenReader)
    {
        if (!tokenReader.Check(OpenBraceSymbol))
            return null;

        var exp = ParseExpression(ref tokenReader);
        if (exp is null)
            throw new ParseException(Resource.VectorEmptyError);

        var parameterList = ImmutableArray.CreateBuilder<IExpression>(1);
        parameterList.Add(exp);

        while (tokenReader.Check(CommaSymbol))
        {
            exp = ParseExpression(ref tokenReader) ??
                  throw new ParseException(Resource.VectorCommaParseException);

            parameterList.Add(exp);
        }

        if (!tokenReader.Check(CloseBraceSymbol))
            throw new ParseException(Resource.VectorCloseBraceParseException);

        return new Vector(parameterList.ToImmutableArray());
    }

    private IExpression? ParseMatrix(ref TokenReader tokenReader)
        => tokenReader.Scoped(this, static (Parser parser, ref TokenReader reader) =>
        {
            if (!reader.Check(OpenBraceSymbol))
                return null;

            var exp = parser.ParseVector(ref reader);
            if (exp is null)
                return null;

            var vectors = ImmutableArray.CreateBuilder<Vector>(1);
            vectors.Add(exp);

            while (reader.Check(CommaSymbol))
            {
                exp = parser.ParseVector(ref reader) ??
                      throw new ParseException(Resource.MatrixCommaParseException);

                vectors.Add(exp);
            }

            if (!reader.Check(CloseBraceSymbol))
                throw new ParseException(Resource.MatrixCloseBraceParseException);

            return new Matrix(vectors.ToImmutableArray());
        });

    private IExpression? ParseString(ref TokenReader tokenReader)
    {
        var str = tokenReader.GetCurrent(TokenKind.String);
        if (str.IsEmpty())
            return null;

        return new StringExpression(str.StringValue!);
    }
}