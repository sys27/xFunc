// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using static xFunc.Maths.Tokenization.TokenKind;
using Vector = xFunc.Maths.Expressions.Matrices.Vector;

namespace xFunc.Maths;

/// <summary>
/// The parser for mathematical expressions.
/// </summary>
/// <example>
///   <code>
///     var parser = new Parser();
///     var exp = parser.Parse("sin(x)");
///   </code>
/// </example>
public partial class Parser : IParser
{
    private readonly IDifferentiator differentiator;
    private readonly ISimplifier simplifier;
    private readonly IConverter converter;

    /// <summary>
    /// Initializes a new instance of the <see cref="Parser"/> class with default implementations of <see cref="IDifferentiator"/>, <see cref="ISimplifier" /> and <see cref="IConverter"/>.
    /// </summary>
    /// <seealso cref="Differentiator"/>
    /// <seealso cref="Simplifier"/>
    /// <seealso cref="Converter"/>
    public Parser()
        : this(new Differentiator(), new Simplifier(), new Converter())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Parser" /> class.
    /// </summary>
    /// <param name="differentiator">The differentiator.</param>
    /// <param name="simplifier">The simplifier.</param>
    /// <param name="converter">The unit converter.</param>
    public Parser(IDifferentiator differentiator, ISimplifier simplifier, IConverter converter)
    {
        this.differentiator = differentiator ?? throw new ArgumentNullException(nameof(differentiator));
        this.simplifier = simplifier ?? throw new ArgumentNullException(nameof(simplifier));
        this.converter = converter ?? throw new ArgumentNullException(nameof(converter));
    }

    /// <inheritdoc />
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

    private IExpression? ParseStatement(ref TokenReader tokenReader)
        => ParseFor(ref tokenReader) ??
           ParseWhile(ref tokenReader) ??
           ParseExpression(ref tokenReader);

    private IExpression? ParseFor(ref TokenReader tokenReader)
    {
        var @for = tokenReader.GetCurrentAndAdvance(ForKeyword);
        if (@for.IsEmpty())
            return null;

        if (!tokenReader.CheckAndAdvance(OpenParenthesisSymbol))
            MissingOpenParenthesis(@for.Kind);

        var body = ParseStatement(ref tokenReader) ??
                   throw new ParseException(Resource.ForBodyParseException);

        if (!tokenReader.CheckAndAdvance(CommaSymbol))
            MissingComma(body);

        var init = ParseStatement(ref tokenReader) ??
                   throw new ParseException(Resource.ForInitParseException);

        if (!tokenReader.CheckAndAdvance(CommaSymbol))
            MissingComma(init);

        var condition = ParseConditionalOrOperator(ref tokenReader) ??
                        throw new ParseException(Resource.ForConditionParseException);

        if (!tokenReader.CheckAndAdvance(CommaSymbol))
            MissingComma(condition);

        var iter = ParseStatement(ref tokenReader) ??
                   throw new ParseException(Resource.ForIterParseException);

        if (!tokenReader.CheckAndAdvance(CloseParenthesisSymbol))
            MissingCloseParenthesis(@for.Kind);

        return new For(body, init, condition, iter);
    }

    private IExpression? ParseWhile(ref TokenReader tokenReader)
    {
        var @while = tokenReader.GetCurrentAndAdvance(WhileKeyword);
        if (@while.IsEmpty())
            return null;

        if (!tokenReader.CheckAndAdvance(OpenParenthesisSymbol))
            MissingOpenParenthesis(@while.Kind);

        var body = ParseStatement(ref tokenReader) ??
                   throw new ParseException(Resource.WhileBodyParseException);

        if (!tokenReader.CheckAndAdvance(CommaSymbol))
            MissingComma(body);

        var condition = ParseConditionalOrOperator(ref tokenReader) ??
                        throw new ParseException(Resource.WhileConditionParseException);

        if (!tokenReader.CheckAndAdvance(CloseParenthesisSymbol))
            MissingCloseParenthesis(@while.Kind);

        return new While(body, condition);
    }

    private IExpression? ParseExpression(ref TokenReader tokenReader)
        => ParseAssignOperators(ref tokenReader) ??
           ParseTernary(ref tokenReader);

    private IExpression? ParseAssignOperators(ref TokenReader tokenReader)
        => tokenReader.Scoped(this, static (Parser parser, ref TokenReader reader) =>
        {
            var variable = parser.ParseVariable(ref reader);
            if (variable is null)
                return null;

            var @operator = reader.GetCurrentAndAdvance(AssignOperator) ||
                            reader.GetCurrentAndAdvance(MulAssignOperator) ||
                            reader.GetCurrentAndAdvance(DivAssignOperator) ||
                            reader.GetCurrentAndAdvance(AddAssignOperator) ||
                            reader.GetCurrentAndAdvance(SubAssignOperator) ||
                            reader.GetCurrentAndAdvance(LeftShiftAssignOperator) ||
                            reader.GetCurrentAndAdvance(RightShiftAssignOperator);

            if (@operator.IsEmpty())
                return null;

            var exp = parser.ParseExpression(ref reader) ??
                      MissingSecondOperand(@operator.Kind);

            return parser.CreateAssign(@operator, variable, exp);
        });

    private IExpression? ParseTernary(ref TokenReader tokenReader)
        => tokenReader.Scoped(this, static (Parser parser, ref TokenReader reader) =>
        {
            var condition = parser.ParseConditionalOrOperator(ref reader);
            if (condition is null)
                return null;

            if (!reader.CheckAndAdvance(QuestionMarkSymbol))
                return condition;

            var then = parser.ParseExpression(ref reader) ??
                       throw new ParseException(Resource.TernaryThenParseException);

            if (!reader.CheckAndAdvance(ColonSymbol))
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
            var @operator = tokenReader.CheckAndAdvance(ConditionalOrOperator);
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
            var @operator = tokenReader.CheckAndAdvance(ConditionalAndOperator);
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
            var token = tokenReader.GetCurrentAndAdvance(NAndKeyword) ||
                        tokenReader.GetCurrentAndAdvance(NOrKeyword) ||
                        tokenReader.GetCurrentAndAdvance(EqKeyword) ||
                        tokenReader.GetCurrentAndAdvance(ImplKeyword);

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
            var token = tokenReader.CheckAndAdvance(OrOperator) ||
                        tokenReader.CheckAndAdvance(OrKeyword);

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
            var token = tokenReader.CheckAndAdvance(XOrKeyword);
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
            var token = tokenReader.CheckAndAdvance(AndOperator) ||
                        tokenReader.CheckAndAdvance(AndKeyword);

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
            var @operator = tokenReader.GetCurrentAndAdvance(EqualOperator) ||
                            tokenReader.GetCurrentAndAdvance(NotEqualOperator);

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
            var @operator = tokenReader.GetCurrentAndAdvance(LessThanOperator) ||
                            tokenReader.GetCurrentAndAdvance(LessOrEqualOperator) ||
                            tokenReader.GetCurrentAndAdvance(GreaterThanOperator) ||
                            tokenReader.GetCurrentAndAdvance(GreaterOrEqualOperator);

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
            var @operator = tokenReader.GetCurrentAndAdvance(LeftShiftOperator) ||
                            tokenReader.GetCurrentAndAdvance(RightShiftOperator);

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
            var @operator = tokenReader.GetCurrentAndAdvance(PlusOperator) ||
                            tokenReader.GetCurrentAndAdvance(MinusOperator);

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
            var token = tokenReader.GetCurrentAndAdvance(MultiplicationOperator) ||
                        tokenReader.GetCurrentAndAdvance(DivisionOperator) ||
                        tokenReader.GetCurrentAndAdvance(ModuloOperator) ||
                        tokenReader.GetCurrentAndAdvance(ModKeyword) ||
                        tokenReader.GetCurrentAndAdvance(RationalOperator);

            if (token.IsEmpty())
                return left;

            var right = ParseMulImplicit(ref tokenReader) ??
                        MissingSecondOperand(token.Kind);

            left = CreateMulDivModRational(token, left, right);
        }
    }

    private IExpression? ParseMulImplicit(ref TokenReader tokenReader)
        => ParseMulImplicitLeftUnary(ref tokenReader) ??
           ParseLeftUnary(ref tokenReader);

    private IExpression? ParseMulImplicitLeftUnary(ref TokenReader tokenReader)
        => tokenReader.Scoped(this, static (Parser parser, ref TokenReader reader) =>
        {
            var minusOperator = reader.GetCurrentAndAdvance(MinusOperator);
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

        var @operator = tokenReader.GetCurrentAndAdvance(ExponentiationOperator);
        if (@operator.IsEmpty())
            return left;

        var right = ParseExponentiation(ref tokenReader) ??
                    throw new ParseException(Resource.ExponentParseException);

        return new Pow(left, right);
    }

    private IExpression? ParseLeftUnary(ref TokenReader tokenReader)
    {
        var token = tokenReader.GetCurrentAndAdvance(NotOperator) ||
                    tokenReader.GetCurrentAndAdvance(MinusOperator) ||
                    tokenReader.GetCurrentAndAdvance(PlusOperator) ||
                    tokenReader.GetCurrentAndAdvance(NotKeyword);

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

        var @operator = tokenReader.GetCurrentAndAdvance(ExponentiationOperator);
        if (@operator.IsEmpty())
            return left;

        var right = ParseLeftUnary(ref tokenReader) ??
                    throw new ParseException(Resource.ExponentParseException);

        return new Pow(left, right);
    }

    private IExpression? ParseRightUnary(ref TokenReader tokenReader)
        => ParseIncDec(ref tokenReader) ??
           ParseFactorialOrCallExpression(ref tokenReader);

    private IExpression? ParseIncDec(ref TokenReader tokenReader)
        => tokenReader.Scoped(this, static (Parser parser, ref TokenReader reader) =>
        {
            var variable = parser.ParseVariable(ref reader);
            if (variable is null)
                return null;

            if (reader.CheckAndAdvance(IncrementOperator))
                return new Inc(variable);

            if (reader.CheckAndAdvance(DecrementOperator))
                return new Dec(variable);

            return null;
        });

    private IExpression? ParseFactorialOrCallExpression(ref TokenReader tokenReader)
    {
        var operand = ParseOperand(ref tokenReader);
        if (operand is null)
            return null;

        if (tokenReader.CheckAndAdvance(FactorialOperator))
            return new Fact(operand);

        while (true)
        {
            var parameters = ParseParameterList(ref tokenReader);
            if (parameters is null)
                return operand;

            operand = new CallExpression(operand, parameters.Value);
        }
    }

    private IExpression? ParseOperand(ref TokenReader tokenReader)
        => ParsePolarComplexNumber(ref tokenReader) ??
           ParseNumberAndUnit(ref tokenReader) ??
           ParseIf(ref tokenReader) ??
           ParseAssignFunction(ref tokenReader) ??
           ParseUnassignFunction(ref tokenReader) ??
           ParseFunctionOrVariable(ref tokenReader) ??
           ParseBoolean(ref tokenReader) ??
           ParseParenthesesExpression(ref tokenReader) ??
           ParseLambda(ref tokenReader) ??
           ParseMatrix(ref tokenReader) ??
           ParseVector(ref tokenReader) ??
           ParseString(ref tokenReader);

    private IExpression? ParseIf(ref TokenReader tokenReader)
    {
        var @if = tokenReader.GetCurrentAndAdvance(IfKeyword);
        if (@if.IsEmpty())
            return null;

        if (!tokenReader.CheckAndAdvance(OpenParenthesisSymbol))
            MissingOpenParenthesis(@if.Kind);

        var condition = ParseConditionalOrOperator(ref tokenReader) ??
                        throw new ParseException(Resource.IfConditionParseException);

        if (!tokenReader.CheckAndAdvance(CommaSymbol))
            MissingComma(condition);

        var then = ParseExpression(ref tokenReader) ??
                   throw new ParseException(Resource.IfThenParseException);

        IExpression? @else = null;
        if (tokenReader.CheckAndAdvance(CommaSymbol))
            @else = ParseExpression(ref tokenReader) ??
                    throw new ParseException(Resource.IfElseParseException);

        if (!tokenReader.CheckAndAdvance(CloseParenthesisSymbol))
            MissingCloseParenthesis(@if.Kind);

        if (@else is not null)
            return new If(condition, then, @else);

        return new If(condition, then);
    }

    private IExpression? ParseAssignFunction(ref TokenReader tokenReader)
    {
        var def = tokenReader.GetCurrentAndAdvance(AssignKeyword);
        if (def.IsEmpty())
            return null;

        if (!tokenReader.CheckAndAdvance(OpenParenthesisSymbol))
            MissingOpenParenthesis(def.Kind);

        var key = ParseVariable(ref tokenReader) ??
                  throw new ParseException(Resource.AssignKeyParseException);

        if (!tokenReader.CheckAndAdvance(CommaSymbol))
            MissingComma(key);

        var value = ParseExpression(ref tokenReader) ??
                    throw new ParseException(Resource.DefValueParseException);

        if (!tokenReader.CheckAndAdvance(CloseParenthesisSymbol))
            MissingCloseParenthesis(def.Kind);

        return new Assign(key, value);
    }

    private IExpression? ParseUnassignFunction(ref TokenReader tokenReader)
    {
        var undef = tokenReader.GetCurrentAndAdvance(UnassignKeyword);
        if (undef.IsEmpty())
            return null;

        if (!tokenReader.CheckAndAdvance(OpenParenthesisSymbol))
            MissingOpenParenthesis(undef.Kind);

        var key = ParseVariable(ref tokenReader) ??
                  throw new ParseException(Resource.AssignKeyParseException);

        if (!tokenReader.CheckAndAdvance(CloseParenthesisSymbol))
            MissingCloseParenthesis(undef.Kind);

        return new Unassign(key);
    }

    private IExpression? ParseParenthesesExpression(ref TokenReader tokenReader)
        => tokenReader.Scoped(this, static (Parser parser, ref TokenReader reader) =>
        {
            if (!reader.CheckAndAdvance(OpenParenthesisSymbol))
                return null;

            var exp = parser.ParseExpression(ref reader);
            if (exp is null)
                return null;

            if (reader.CheckAndAdvance(CommaSymbol))
                return null;

            if (!reader.CheckAndAdvance(CloseParenthesisSymbol))
                throw new ParseException(string.Format(CultureInfo.InvariantCulture, Resource.CloseParenParseException, exp));

            if (reader.CheckAndAdvance(LambdaOperator))
                return null;

            return exp;
        });

    private IExpression? ParseLambda(ref TokenReader tokenReader)
    {
        if (!tokenReader.CheckAndAdvance(OpenParenthesisSymbol))
            return null;

        var parameters = new HashSet<string>();
        var parameter = tokenReader.GetCurrentAndAdvance(Id);
        if (parameter.IsNotEmpty())
        {
            parameters.Add(parameter.StringValue!);

            while (tokenReader.CheckAndAdvance(CommaSymbol))
            {
                parameter = tokenReader.GetCurrentAndAdvance(Id);
                if (parameter.IsEmpty())
                    return MissingExpression();

                if (!parameters.Add(parameter.StringValue!))
                    throw new ParseException(string.Format(
                        CultureInfo.InvariantCulture,
                        Resource.DuplidateLambdaParameterParseException,
                        parameter.StringValue!));
            }
        }

        if (!tokenReader.CheckAndAdvance(CloseParenthesisSymbol))
            throw new ParseException(Resource.ParameterListCloseParseException);

        if (!tokenReader.CheckAndAdvance(LambdaOperator))
            throw new ParseException(Resource.MissingLambdaParseException);

        var body = ParseExpression(ref tokenReader) ??
                   throw new ParseException(Resource.MissingLambdaBodyParseException);

        var result = new Lambda(parameters, body).AsExpression();

        return result;
    }

    private IExpression? ParseFunctionOrVariable(ref TokenReader tokenReader)
    {
        var function = tokenReader.GetCurrentAndAdvance(Id);
        if (function.IsEmpty())
            return null;

        var parameterList = ParseParameterList(ref tokenReader);
        if (parameterList is null)
            return CreateVariable(function);

        return CreateFunction(function, parameterList.Value);
    }

    private ImmutableArray<IExpression>? ParseParameterList(ref TokenReader tokenReader)
    {
        if (!tokenReader.CheckAndAdvance(OpenParenthesisSymbol))
            return null;

        var parameterList = ImmutableArray.CreateBuilder<IExpression>(1);

        var exp = ParseExpression(ref tokenReader);
        if (exp is not null)
        {
            parameterList.Add(exp);

            while (tokenReader.CheckAndAdvance(CommaSymbol))
            {
                exp = ParseExpression(ref tokenReader) ??
                      MissingExpression();

                parameterList.Add(exp);
            }
        }

        if (!tokenReader.CheckAndAdvance(CloseParenthesisSymbol))
            throw new ParseException(Resource.ParameterListCloseParseException);

        return parameterList.ToImmutableArray();
    }

    private IExpression? ParseNumberAndUnit(ref TokenReader tokenReader)
    {
        var number = tokenReader.GetCurrentAndAdvance(TokenKind.Number);
        if (number.IsEmpty())
            return null;

        var unitString = tokenReader.GetCurrentAndAdvance(TokenKind.String);
        if (unitString.IsNotEmpty())
        {
            var unit = (ParseTemperatureUnit(number, unitString) ??
                        ParseAngleUnit(number, unitString) ??
                        ParsePowerUnit(number, unitString) ??
                        ParseMassUnit(number, unitString) ??
                        ParseVolumeUnit(number, unitString) ??
                        ParseAreaUnit(number, unitString) ??
                        ParseLengthUnit(number, unitString) ??
                        ParseTimeUnit(number, unitString)) ??
                       throw new ParseException(string.Format(CultureInfo.InvariantCulture, Resource.UnitIsNotSupportedException, unitString.StringValue));

            return unit;
        }

        if (tokenReader.GetCurrentAndAdvance(DegreeSymbol))
            return AngleValue.Degree(number.NumberValue).AsExpression();

        return new Number(number.NumberValue);
    }

    private IExpression? ParseAngleUnit(Token number, Token unit)
    {
        var lowerUnit = unit.StringValue!.ToLowerInvariant();
        if (AngleUnit.Degree.UnitNames.Contains(lowerUnit))
            return AngleValue.Degree(number.NumberValue).AsExpression();

        if (AngleUnit.Radian.UnitNames.Contains(lowerUnit))
            return AngleValue.Radian(number.NumberValue).AsExpression();

        if (AngleUnit.Gradian.UnitNames.Contains(lowerUnit))
            return AngleValue.Gradian(number.NumberValue).AsExpression();

        return null;
    }

    private IExpression? ParsePowerUnit(Token number, Token unit)
        => unit.StringValue!.ToLowerInvariant() switch
        {
            "w" => PowerValue.Watt(number.NumberValue).AsExpression(),
            "kw" => PowerValue.Kilowatt(number.NumberValue).AsExpression(),
            "hp" => PowerValue.Horsepower(number.NumberValue).AsExpression(),
            _ => null,
        };

    private IExpression? ParseTemperatureUnit(Token number, Token unit)
    {
        if (unit.StringValue!.Equals("°c", StringComparison.OrdinalIgnoreCase))
            return TemperatureValue.Celsius(number.NumberValue).AsExpression();

        if (unit.StringValue!.Equals("°f", StringComparison.OrdinalIgnoreCase))
            return TemperatureValue.Fahrenheit(number.NumberValue).AsExpression();

        if (unit.StringValue!.Equals("k", StringComparison.OrdinalIgnoreCase))
            return TemperatureValue.Kelvin(number.NumberValue).AsExpression();

        return null;
    }

    private IExpression? ParseMassUnit(Token number, Token unit)
        => unit.StringValue!.ToLowerInvariant() switch
        {
            "mg" => MassValue.Milligram(number.NumberValue).AsExpression(),
            "g" => MassValue.Gram(number.NumberValue).AsExpression(),
            "kg" => MassValue.Kilogram(number.NumberValue).AsExpression(),
            "t" => MassValue.Tonne(number.NumberValue).AsExpression(),
            "oz" => MassValue.Ounce(number.NumberValue).AsExpression(),
            "lb" => MassValue.Pound(number.NumberValue).AsExpression(),
            _ => null,
        };

    private IExpression? ParseLengthUnit(Token number, Token unit)
        => unit.StringValue!.ToLowerInvariant() switch
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

    private IExpression? ParseTimeUnit(Token number, Token unit)
        => unit.StringValue!.ToLowerInvariant() switch
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

    private IExpression? ParseAreaUnit(Token number, Token unit)
        => unit.StringValue!.ToLowerInvariant() switch
        {
            "ha" => AreaValue.Hectare(number.NumberValue).AsExpression(),
            "ac" => AreaValue.Acre(number.NumberValue).AsExpression(),
            "m^2" => AreaValue.Meter(number.NumberValue).AsExpression(),
            "mm^2" => AreaValue.Millimeter(number.NumberValue).AsExpression(),
            "cm^2" => AreaValue.Centimeter(number.NumberValue).AsExpression(),
            "km^2" => AreaValue.Kilometer(number.NumberValue).AsExpression(),
            "in^2" => AreaValue.Inch(number.NumberValue).AsExpression(),
            "yd^2" => AreaValue.Yard(number.NumberValue).AsExpression(),
            "ft^2" => AreaValue.Foot(number.NumberValue).AsExpression(),
            "mi^2" => AreaValue.Mile(number.NumberValue).AsExpression(),
            _ => null,
        };

    private IExpression? ParseVolumeUnit(Token number, Token unit)
        => unit.StringValue!.ToLowerInvariant() switch
        {
            "gal" => VolumeValue.Gallon(number.NumberValue).AsExpression(),
            "l" => VolumeValue.Liter(number.NumberValue).AsExpression(),
            "m^3" => VolumeValue.Meter(number.NumberValue).AsExpression(),
            "cm^3" => VolumeValue.Centimeter(number.NumberValue).AsExpression(),
            "in^3" => VolumeValue.Inch(number.NumberValue).AsExpression(),
            "yd^3" => VolumeValue.Yard(number.NumberValue).AsExpression(),
            "ft^3" => VolumeValue.Foot(number.NumberValue).AsExpression(),
            _ => null,
        };

    private IExpression? ParsePolarComplexNumber(ref TokenReader tokenReader)
        => tokenReader.Scoped(this, static (Parser _, ref TokenReader reader) =>
        {
            // plus symbol can be ignored
            reader.GetCurrentAndAdvance(PlusOperator);

            var magnitude = reader.GetCurrentAndAdvance(TokenKind.Number);
            if (magnitude.IsEmpty())
                return null;

            if (!reader.CheckAndAdvance(AngleSymbol))
                return null;

            var phaseSign = reader.GetCurrentAndAdvance(PlusOperator) ||
                            reader.GetCurrentAndAdvance(MinusOperator);

            var phase = reader.GetCurrentAndAdvance(TokenKind.Number);
            if (phase.IsEmpty())
                throw new ParseException(Resource.PhaseParseException);

            if (!reader.CheckAndAdvance(DegreeSymbol))
                throw new ParseException(Resource.DegreeComplexNumberParseException);

            var magnitudeNumber = magnitude.NumberValue;
            var sign = phaseSign.Is(MinusOperator) ? -1 : 1;
            var phaseNumber = AngleValue.Degree(phase.NumberValue * sign).ToRadian();
            var complex = Complex.FromPolarCoordinates(magnitudeNumber, phaseNumber.Angle.Number);

            return new ComplexNumber(complex);
        });

    private Variable? ParseVariable(ref TokenReader tokenReader)
    {
        var variable = tokenReader.GetCurrentAndAdvance(Id);

        // usually we use 'scope' in such cases, but here we can ignore it,
        // because parsing of variable is always 'scoped'
        // if it is not true anymore, then use 'scope'
        if (variable.IsEmpty() || tokenReader.CheckAndAdvance(OpenParenthesisSymbol))
            return null;

        if (variable.StringValue == Variable.X.Name)
            return Variable.X;

        return CreateVariable(variable);
    }

    private IExpression? ParseBoolean(ref TokenReader tokenReader)
    {
        if (tokenReader.CheckAndAdvance(TrueKeyword))
            return Bool.True;

        if (tokenReader.CheckAndAdvance(FalseKeyword))
            return Bool.False;

        return null;
    }

    private Vector? ParseVector(ref TokenReader tokenReader)
    {
        if (!tokenReader.CheckAndAdvance(OpenBraceSymbol))
            return null;

        var exp = ParseExpression(ref tokenReader);
        if (exp is null)
            throw new ParseException(Resource.VectorEmptyError);

        var parameterList = ImmutableArray.CreateBuilder<IExpression>(1);
        parameterList.Add(exp);

        while (tokenReader.CheckAndAdvance(CommaSymbol))
        {
            exp = ParseExpression(ref tokenReader) ??
                  throw new ParseException(Resource.VectorCommaParseException);

            parameterList.Add(exp);
        }

        if (!tokenReader.CheckAndAdvance(CloseBraceSymbol))
            throw new ParseException(Resource.VectorCloseBraceParseException);

        return new Vector(parameterList.ToImmutableArray());
    }

    private IExpression? ParseMatrix(ref TokenReader tokenReader)
        => tokenReader.Scoped(this, static (Parser parser, ref TokenReader reader) =>
        {
            if (!reader.CheckAndAdvance(OpenBraceSymbol))
                return null;

            var exp = parser.ParseVector(ref reader);
            if (exp is null)
                return null;

            var vectors = ImmutableArray.CreateBuilder<Vector>(1);
            vectors.Add(exp);

            while (reader.CheckAndAdvance(CommaSymbol))
            {
                exp = parser.ParseVector(ref reader) ??
                      throw new ParseException(Resource.MatrixCommaParseException);

                vectors.Add(exp);
            }

            if (!reader.CheckAndAdvance(CloseBraceSymbol))
                throw new ParseException(Resource.MatrixCloseBraceParseException);

            return new Matrix(vectors.ToImmutableArray());
        });

    private IExpression? ParseString(ref TokenReader tokenReader)
    {
        var str = tokenReader.GetCurrentAndAdvance(TokenKind.String);
        if (str.IsEmpty())
            return null;

        return new StringExpression(str.StringValue!);
    }

    [DoesNotReturn]
    private static IExpression MissingSecondOperand(TokenKind tokenKind)
        => throw new ParseException(string.Format(CultureInfo.InvariantCulture, Resource.SecondOperandParseException, tokenKind));

    [DoesNotReturn]
    private static void MissingOpenParenthesis(TokenKind tokenKind)
        => throw new ParseException(string.Format(CultureInfo.InvariantCulture, Resource.FunctionOpenParenthesisParseException, tokenKind));

    [DoesNotReturn]
    private static void MissingCloseParenthesis(TokenKind tokenKind)
        => throw new ParseException(string.Format(CultureInfo.InvariantCulture, Resource.FunctionCloseParenthesisParseException, tokenKind));

    [DoesNotReturn]
    private static void MissingComma(IExpression previousExpression)
        => throw new ParseException(string.Format(CultureInfo.InvariantCulture, Resource.CommaParseException, previousExpression));

    [DoesNotReturn]
    private static IExpression MissingExpression()
        => throw new ParseException(string.Format(CultureInfo.InvariantCulture, Resource.MissingExpParseException));
}