// Copyright 2012-2020 Dmytro Kyshchenko
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Globalization;
using xFunc.Maths.Analyzers;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Angles;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions.Programming;
using xFunc.Maths.Resources;
using xFunc.Maths.Tokenization;
using static xFunc.Maths.Tokenization.TokenKind;
using Vector = xFunc.Maths.Expressions.Matrices.Vector;

namespace xFunc.Maths
{
    /// <summary>
    /// The parser for mathematical expressions.
    /// </summary>
    public partial class Parser : IParser
    {
        private readonly IDifferentiator differentiator;
        private readonly ISimplifier simplifier;

        /// <summary>
        /// Initializes a new instance of the <see cref="Parser"/> class with default implementations of <see cref="IDifferentiator"/> and <see cref="ISimplifier" />.
        /// </summary>
        public Parser()
            : this(new Differentiator(), new Simplifier())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Parser" /> class.
        /// </summary>
        /// <param name="differentiator">The differentiator.</param>
        /// <param name="simplifier">The simplifier.</param>
        public Parser(IDifferentiator differentiator, ISimplifier simplifier)
        {
            this.differentiator = differentiator ??
                                  throw new ArgumentNullException(nameof(differentiator));
            this.simplifier = simplifier ??
                              throw new ArgumentNullException(nameof(simplifier));
        }

        /// <summary>
        /// Parses the specified function.
        /// </summary>
        /// <param name="function">The string that contains the functions and operators.</param>
        /// <returns>The parsed expression.</returns>
        public IExpression Parse(string function)
        {
            if (string.IsNullOrWhiteSpace(function))
                throw new ArgumentNullException(nameof(function), Resource.NotSpecifiedFunction);

            var lexer = new Lexer(function);
            var tokenReader = new TokenReader(ref lexer);

            try
            {
                var exp = Statement(ref tokenReader);
                var token = tokenReader.GetCurrent();
                if (exp == null || !tokenReader.IsEnd || token.IsNotEmpty())
                    throw new ParseException(Resource.ErrorWhileParsingTree);

                return exp;
            }
            finally
            {
                tokenReader.Dispose();
            }
        }

        private IExpression? Statement(ref TokenReader tokenReader)
        {
            // TODO: to expressions?
            return UnaryAssign(ref tokenReader) ??
                   BinaryAssign(ref tokenReader) ??
                   Assign(ref tokenReader) ??
                   Def(ref tokenReader) ??
                   Undef(ref tokenReader) ??
                   If(ref tokenReader) ??
                   For(ref tokenReader) ??
                   While(ref tokenReader) ??
                   Expression(ref tokenReader);
        }

        private IExpression? UnaryAssign(ref TokenReader tokenReader)
        {
            var scope = tokenReader.CreateScope();

            var left = Variable(ref tokenReader);
            if (left != null)
            {
                var @operator = tokenReader.GetCurrent(IncrementOperator) ||
                                tokenReader.GetCurrent(DecrementOperator);

                if (@operator.IsNotEmpty())
                {
                    tokenReader.Commit();

                    return CreateUnaryAssign(@operator, left);
                }
            }

            tokenReader.Rollback(scope);

            return null;
        }

        private IExpression? BinaryAssign(ref TokenReader tokenReader)
        {
            var scope = tokenReader.CreateScope();

            var left = Variable(ref tokenReader);
            if (left == null)
            {
                tokenReader.Rollback(scope);

                return null;
            }

            var @operator = tokenReader.GetCurrent(MulAssignOperator) ||
                            tokenReader.GetCurrent(DivAssignOperator) ||
                            tokenReader.GetCurrent(AddAssignOperator) ||
                            tokenReader.GetCurrent(SubAssignOperator) ||
                            tokenReader.GetCurrent(LeftShiftAssignOperator) ||
                            tokenReader.GetCurrent(RightShiftAssignOperator);

            if (@operator.IsNotEmpty())
            {
                var right = Expression(ref tokenReader) ??
                            throw new ParseException(SecondOperand(ref @operator));

                tokenReader.Commit();

                return CreateBinaryAssign(@operator, left, right);
            }

            tokenReader.Rollback(scope);

            return null;
        }

        private IExpression? AssignmentKey(ref TokenReader tokenReader)
        {
            return FunctionDeclaration(ref tokenReader) ??
                   Variable(ref tokenReader);
        }

        private IExpression? Assign(ref TokenReader tokenReader)
        {
            var scope = tokenReader.CreateScope();

            var left = AssignmentKey(ref tokenReader);
            if (left == null)
            {
                tokenReader.Rollback(scope);

                return null;
            }

            var @operator = tokenReader.GetCurrent(AssignOperator);
            if (@operator.IsNotEmpty())
            {
                var right = Expression(ref tokenReader) ??
                            throw new ParseException(SecondOperand(ref @operator));

                tokenReader.Commit();

                return new Define(left, right);
            }

            tokenReader.Rollback(scope);

            return null;
        }

        private IExpression? Def(ref TokenReader tokenReader)
        {
            var def = tokenReader.GetCurrent(DefineKeyword);
            if (def.IsEmpty())
                return null;

            if (!tokenReader.Check(OpenParenthesisSymbol))
                throw new ParseException(OpenParenthesis(ref def));

            var key = AssignmentKey(ref tokenReader) ??
                      throw new ParseException(Resource.AssignKeyParseException);

            if (!tokenReader.Check(CommaSymbol))
                throw new ParseException(CommaMissing(key));

            var value = Expression(ref tokenReader) ??
                        throw new ParseException(Resource.DefValueParseException);

            if (!tokenReader.Check(CloseParenthesisSymbol))
                throw new ParseException(CloseParenthesis(ref def));

            return new Define(key, value);
        }

        private IExpression? Undef(ref TokenReader tokenReader)
        {
            var undef = tokenReader.GetCurrent(UndefineKeyword);
            if (undef.IsEmpty())
                return null;

            if (!tokenReader.Check(OpenParenthesisSymbol))
                throw new ParseException(OpenParenthesis(ref undef));

            var key = AssignmentKey(ref tokenReader) ??
                      throw new ParseException(Resource.AssignKeyParseException);

            if (!tokenReader.Check(CloseParenthesisSymbol))
                throw new ParseException(CloseParenthesis(ref undef));

            return new Undefine(key);
        }

        private IExpression? If(ref TokenReader tokenReader)
        {
            var @if = tokenReader.GetCurrent(IfKeyword);
            if (@if.IsEmpty())
                return null;

            if (!tokenReader.Check(OpenParenthesisSymbol))
                throw new ParseException(OpenParenthesis(ref @if));

            var condition = ConditionalOperator(ref tokenReader) ??
                            throw new ParseException(Resource.IfConditionParseException);

            if (!tokenReader.Check(CommaSymbol))
                throw new ParseException(CommaMissing(condition));

            var then = Expression(ref tokenReader) ??
                       throw new ParseException(Resource.IfThenParseException);

            IExpression? @else = null;
            if (tokenReader.Check(CommaSymbol))
                @else = Expression(ref tokenReader) ??
                        throw new ParseException(Resource.IfElseParseException);

            if (!tokenReader.Check(CloseParenthesisSymbol))
                throw new ParseException(CloseParenthesis(ref @if));

            if (@else != null)
                return new If(condition, then, @else);

            return new If(condition, then);
        }

        private IExpression? For(ref TokenReader tokenReader)
        {
            var @for = tokenReader.GetCurrent(ForKeyword);
            if (@for.IsEmpty())
                return null;

            if (!tokenReader.Check(OpenParenthesisSymbol))
                throw new ParseException(OpenParenthesis(ref @for));

            var body = Statement(ref tokenReader) ??
                       throw new ParseException(Resource.ForBodyParseException);

            if (!tokenReader.Check(CommaSymbol))
                throw new ParseException(CommaMissing(body));

            var init = Statement(ref tokenReader) ??
                       throw new ParseException(Resource.ForInitParseException);

            if (!tokenReader.Check(CommaSymbol))
                throw new ParseException(CommaMissing(init));

            var condition = ConditionalOperator(ref tokenReader) ??
                            throw new ParseException(Resource.ForConditionParseException);

            if (!tokenReader.Check(CommaSymbol))
                throw new ParseException(CommaMissing(condition));

            var iter = Statement(ref tokenReader) ??
                       throw new ParseException(Resource.ForIterParseException);

            if (!tokenReader.Check(CloseParenthesisSymbol))
                throw new ParseException(CloseParenthesis(ref @for));

            return new For(body, init, condition, iter);
        }

        private IExpression? While(ref TokenReader tokenReader)
        {
            var @while = tokenReader.GetCurrent(WhileKeyword);
            if (@while.IsEmpty())
                return null;

            if (!tokenReader.Check(OpenParenthesisSymbol))
                throw new ParseException(OpenParenthesis(ref @while));

            var body = Statement(ref tokenReader) ??
                       throw new ParseException(Resource.WhileBodyParseException);

            if (!tokenReader.Check(CommaSymbol))
                throw new ParseException(CommaMissing(body));

            var condition = ConditionalOperator(ref tokenReader) ??
                            throw new ParseException(Resource.WhileConditionParseException);

            if (!tokenReader.Check(CloseParenthesisSymbol))
                throw new ParseException(CloseParenthesis(ref @while));

            return new While(body, condition);
        }

        private IExpression? FunctionDeclaration(ref TokenReader tokenReader)
        {
            var scope = tokenReader.CreateScope();

            var id = tokenReader.GetCurrent(Id);
            if (id.IsNotEmpty() && tokenReader.Check(OpenParenthesisSymbol))
            {
                var parameterList = new List<IExpression>();

                var exp = Variable(ref tokenReader);
                if (exp != null)
                {
                    parameterList.Add(exp);

                    while (tokenReader.Check(CommaSymbol))
                    {
                        exp = Variable(ref tokenReader);
                        if (exp == null)
                        {
                            tokenReader.Rollback(scope);

                            return null;
                        }

                        parameterList.Add(exp);
                    }
                }

                if (tokenReader.Check(CloseParenthesisSymbol))
                {
                    tokenReader.Commit();

                    return CreateFunction(id, parameterList);
                }
            }

            tokenReader.Rollback(scope);

            return null;
        }

        private IExpression? Expression(ref TokenReader tokenReader)
        {
            return Ternary(ref tokenReader);
        }

        private IExpression? Ternary(ref TokenReader tokenReader)
        {
            var scope = tokenReader.CreateScope();

            var condition = ConditionalOperator(ref tokenReader);
            if (condition == null)
            {
                tokenReader.Rollback(scope);

                return null;
            }

            if (!tokenReader.Check(QuestionMarkSymbol))
            {
                tokenReader.Commit();

                return condition;
            }

            var then = Expression(ref tokenReader) ??
                       throw new ParseException(Resource.TernaryThenParseException);

            if (!tokenReader.Check(ColonSymbol))
                throw new ParseException(Resource.TernaryColonParseException);

            var @else = Expression(ref tokenReader) ??
                        throw new ParseException(Resource.TernaryElseParseException);

            tokenReader.Commit();

            return new If(condition, then, @else);
        }

        private IExpression? ConditionalOperator(ref TokenReader tokenReader)
        {
            var left = BitwiseOperator(ref tokenReader);
            if (left == null)
                return null;

            while (true)
            {
                var @operator = tokenReader.GetCurrent(ConditionalAndOperator) ||
                                tokenReader.GetCurrent(ConditionalOrOperator);

                if (@operator.IsEmpty())
                    return left;

                var right = BitwiseOperator(ref tokenReader) ??
                            throw new ParseException(SecondOperand(ref @operator));

                left = CreateConditionalOperator(@operator, left, right);
            }
        }

        private IExpression? BitwiseOperator(ref TokenReader tokenReader)
        {
            var left = EqualityOperator(ref tokenReader);
            if (left == null)
                return null;

            while (true)
            {
                var token = tokenReader.GetCurrent(AndOperator) ||
                            tokenReader.GetCurrent(OrOperator) ||
                            tokenReader.GetCurrent(ImplicationOperator) ||
                            tokenReader.GetCurrent(TokenKind.EqualityOperator) ||
                            tokenReader.GetCurrent(NAndKeyword) ||
                            tokenReader.GetCurrent(NOrKeyword) ||
                            tokenReader.GetCurrent(AndKeyword) ||
                            tokenReader.GetCurrent(OrKeyword) ||
                            tokenReader.GetCurrent(XOrKeyword) ||
                            tokenReader.GetCurrent(EqKeyword) ||
                            tokenReader.GetCurrent(ImplKeyword);

                if (token.IsEmpty())
                    return left;

                var right = EqualityOperator(ref tokenReader) ??
                            throw new ParseException(SecondOperand(ref token));

                left = CreateBitwiseOperator(token, left, right);
            }
        }

        private IExpression? EqualityOperator(ref TokenReader tokenReader)
        {
            var left = Shift(ref tokenReader);
            if (left == null)
                return null;

            while (true)
            {
                var @operator = tokenReader.GetCurrent(EqualOperator) ||
                                tokenReader.GetCurrent(NotEqualOperator) ||
                                tokenReader.GetCurrent(LessThanOperator) ||
                                tokenReader.GetCurrent(LessOrEqualOperator) ||
                                tokenReader.GetCurrent(GreaterThanOperator) ||
                                tokenReader.GetCurrent(GreaterOrEqualOperator);

                if (@operator.IsEmpty())
                    return left;

                var right = Shift(ref tokenReader) ??
                            throw new ParseException(SecondOperand(ref @operator));

                left = CreateEqualityOperator(@operator, left, right);
            }
        }

        private IExpression? Shift(ref TokenReader tokenReader)
        {
            var left = AddSub(ref tokenReader);
            if (left == null)
                return null;

            while (true)
            {
                var @operator = tokenReader.GetCurrent(LeftShiftOperator) ||
                                tokenReader.GetCurrent(RightShiftOperator);

                if (@operator.IsEmpty())
                    return left;

                var right = AddSub(ref tokenReader) ??
                            throw new ParseException(SecondOperand(ref @operator));

                left = CreateShift(@operator, left, right);
            }
        }

        private IExpression? AddSub(ref TokenReader tokenReader)
        {
            var left = MulDivMod(ref tokenReader);
            if (left == null)
                return null;

            while (true)
            {
                var @operator = tokenReader.GetCurrent(PlusOperator) ||
                                tokenReader.GetCurrent(MinusOperator);

                if (@operator.IsEmpty())
                    return left;

                var right = MulDivMod(ref tokenReader) ??
                            throw new ParseException(SecondOperand(ref @operator));

                left = CreateAddSub(@operator, left, right);
            }
        }

        private IExpression? MulDivMod(ref TokenReader tokenReader)
        {
            var left = MulImplicit(ref tokenReader);
            if (left == null)
                return null;

            while (true)
            {
                var token = tokenReader.GetCurrent(MultiplicationOperator) ||
                            tokenReader.GetCurrent(DivisionOperator) ||
                            tokenReader.GetCurrent(ModuloOperator) ||
                            tokenReader.GetCurrent(ModKeyword);

                if (token.IsEmpty())
                    return left;

                var right = MulImplicit(ref tokenReader) ??
                            throw new ParseException(SecondOperand(ref token));

                left = CreateMulDivMod(token, left, right);
            }
        }

        private IExpression? MulImplicit(ref TokenReader tokenReader)
        {
            return MulImplicitLeftUnary(ref tokenReader) ??
                   LeftUnary(ref tokenReader);
        }

        private IExpression? MulImplicitLeftUnary(ref TokenReader tokenReader)
        {
            var scope = tokenReader.CreateScope();

            var @operator = tokenReader.GetCurrent(MinusOperator);
            var number = Number(ref tokenReader);
            if (number != null)
            {
                var rightUnary = MulImplicitExponentiation(ref tokenReader) ??
                                 ParenthesesExpression(ref tokenReader) ??
                                 Matrix(ref tokenReader) ??
                                 Vector(ref tokenReader);
                if (rightUnary != null)
                {
                    if (@operator.IsNotEmpty())
                        number = new UnaryMinus(number);

                    tokenReader.Commit();

                    return new Mul(number, rightUnary);
                }
            }

            tokenReader.Rollback(scope);

            return null;
        }

        private IExpression? MulImplicitExponentiation(ref TokenReader tokenReader)
        {
            var left = FunctionOrVariable(ref tokenReader);
            if (left == null)
                return null;

            var @operator = tokenReader.GetCurrent(ExponentiationOperator);
            if (@operator.IsEmpty())
                return left;

            var right = Exponentiation(ref tokenReader) ??
                        throw new ParseException(Resource.ExponentParseException);

            return new Pow(left, right);
        }

        private IExpression? LeftUnary(ref TokenReader tokenReader)
        {
            var token = tokenReader.GetCurrent(NotOperator) ||
                        tokenReader.GetCurrent(MinusOperator) ||
                        tokenReader.GetCurrent(PlusOperator) ||
                        tokenReader.GetCurrent(NotKeyword);

            var operand = Exponentiation(ref tokenReader);
            if (operand == null || token.IsEmpty() || token.Is(PlusOperator))
                return operand;

            if (token.Is(MinusOperator))
                return new UnaryMinus(operand);

            return new Not(operand);
        }

        private IExpression? Exponentiation(ref TokenReader tokenReader)
        {
            var left = RightUnary(ref tokenReader);
            if (left == null)
                return null;

            var @operator = tokenReader.GetCurrent(ExponentiationOperator);
            if (@operator.IsEmpty())
                return left;

            var right = LeftUnary(ref tokenReader) ??
                        throw new ParseException(Resource.ExponentParseException);

            return new Pow(left, right);
        }

        private IExpression? RightUnary(ref TokenReader tokenReader)
        {
            var scope = tokenReader.CreateScope();

            var number = Number(ref tokenReader);
            if (number != null)
            {
                var @operator = tokenReader.GetCurrent(FactorialOperator);
                if (@operator.IsNotEmpty())
                {
                    tokenReader.Commit();

                    return new Fact(number);
                }
            }

            tokenReader.Rollback(scope);

            return Operand(ref tokenReader);
        }

        private IExpression? Operand(ref TokenReader tokenReader)
        {
            return ComplexNumber(ref tokenReader) ??
                   Number(ref tokenReader) ??
                   FunctionOrVariable(ref tokenReader) ??
                   Boolean(ref tokenReader) ??
                   ParenthesesExpression(ref tokenReader) ??
                   Matrix(ref tokenReader) ??
                   Vector(ref tokenReader);
        }

        private IExpression? ParenthesesExpression(ref TokenReader tokenReader)
        {
            if (!tokenReader.Check(OpenParenthesisSymbol))
                return null;

            var exp = Expression(ref tokenReader) ??
                      throw new ParseException(Resource.ExpParenParseException);

            if (!tokenReader.Check(CloseParenthesisSymbol))
                throw new ParseException(string.Format(CultureInfo.InvariantCulture, Resource.CloseParenParseException, exp));

            return exp;
        }

        private IExpression? FunctionOrVariable(ref TokenReader tokenReader)
        {
            var function = tokenReader.GetCurrent(Id);
            if (function.IsEmpty())
                return null;

            var parameterList = ParameterList(ref tokenReader);
            if (parameterList == null)
                return CreateVariable(function);

            return CreateFunction(function, parameterList);
        }

        private IList<IExpression>? ParameterList(ref TokenReader tokenReader)
        {
            if (!tokenReader.Check(OpenParenthesisSymbol))
                return null;

            var parameterList = new List<IExpression>();

            var exp = Expression(ref tokenReader);
            if (exp != null)
            {
                parameterList.Add(exp);

                while (tokenReader.Check(CommaSymbol))
                {
                    exp = Expression(ref tokenReader) ??
                          throw new ParseException(CommaMissing(exp));

                    parameterList.Add(exp);
                }
            }

            if (!tokenReader.Check(CloseParenthesisSymbol))
                throw new ParseException(Resource.ParameterListCloseParseException);

            return parameterList;
        }

        private IExpression? Number(ref TokenReader tokenReader)
        {
            var number = tokenReader.GetCurrent(TokenKind.Number);
            if (number.IsEmpty())
                return null;

            if (tokenReader.Check(DegreeSymbol) || tokenReader.Check(DegreeKeyword))
                return AngleValue.Degree(number.NumberValue).AsExpression();

            if (tokenReader.Check(RadianKeyword))
                return AngleValue.Radian(number.NumberValue).AsExpression();

            if (tokenReader.Check(GradianKeyword))
                return AngleValue.Gradian(number.NumberValue).AsExpression();

            return new Number(number.NumberValue);
        }

        private IExpression? ComplexNumber(ref TokenReader tokenReader)
        {
            var scope = tokenReader.CreateScope();

            var magnitudeSign = tokenReader.GetCurrent(PlusOperator) ||
                                tokenReader.GetCurrent(MinusOperator);

            var magnitude = tokenReader.GetCurrent(TokenKind.Number);
            if (magnitude.IsNotEmpty())
            {
                var hasAngleSymbol = tokenReader.Check(AngleSymbol);
                if (hasAngleSymbol)
                {
                    var phaseSign = tokenReader.GetCurrent(PlusOperator) ||
                                    tokenReader.GetCurrent(MinusOperator);

                    var phase = tokenReader.GetCurrent(TokenKind.Number);
                    if (phase.IsEmpty())
                        throw new ParseException(Resource.PhaseParseException);

                    var hasDegreeSymbol = tokenReader.Check(DegreeSymbol);
                    if (!hasDegreeSymbol)
                        throw new ParseException(Resource.DegreeComplexNumberParseException);

                    tokenReader.Commit();

                    return CreateComplexNumber(magnitudeSign, magnitude, phaseSign, phase);
                }
            }

            tokenReader.Rollback(scope);

            return null;
        }

        private Variable? Variable(ref TokenReader tokenReader)
        {
            var variable = tokenReader.GetCurrent(Id);
            if (variable.IsEmpty())
                return null;

            return CreateVariable(variable);
        }

        private IExpression? Boolean(ref TokenReader tokenReader)
        {
            var boolean = tokenReader.GetCurrent(TrueKeyword) ||
                          tokenReader.GetCurrent(FalseKeyword);

            if (boolean.IsEmpty())
                return null;

            return CreateBoolean(boolean);
        }

        private Vector? Vector(ref TokenReader tokenReader)
        {
            if (!tokenReader.Check(OpenBraceSymbol))
                return null;

            var parameterList = new List<IExpression>();

            var exp = Expression(ref tokenReader);
            if (exp != null)
            {
                parameterList.Add(exp);

                while (tokenReader.Check(CommaSymbol))
                {
                    exp = Expression(ref tokenReader) ??
                          throw new ParseException(Resource.VectorCommaParseException);

                    parameterList.Add(exp);
                }
            }

            if (!tokenReader.Check(CloseBraceSymbol))
                throw new ParseException(Resource.VectorCloseBraceParseException);

            return new Vector(parameterList);
        }

        private IExpression? Matrix(ref TokenReader tokenReader)
        {
            var scope = tokenReader.CreateScope();

            if (tokenReader.Check(OpenBraceSymbol))
            {
                var vectors = new List<Vector>();

                var exp = Vector(ref tokenReader);
                if (exp != null)
                {
                    vectors.Add(exp);

                    while (tokenReader.Check(CommaSymbol))
                    {
                        exp = Vector(ref tokenReader) ??
                              throw new ParseException(Resource.MatrixCommaParseException);

                        vectors.Add(exp);
                    }

                    if (!tokenReader.Check(CloseBraceSymbol))
                        throw new ParseException(Resource.MatrixCloseBraceParseException);

                    tokenReader.Commit();

                    return new Matrix(vectors);
                }
            }

            tokenReader.Rollback(scope);

            return null;
        }

        private static string SecondOperand(ref Token token)
            => string.Format(CultureInfo.InvariantCulture, Resource.SecondOperandParseException, token);

        private static string OpenParenthesis(ref Token token)
            => string.Format(CultureInfo.InvariantCulture, Resource.FunctionOpenParenthesisParseException, token);

        private static string CloseParenthesis(ref Token token)
            => string.Format(CultureInfo.InvariantCulture, Resource.FunctionCloseParenthesisParseException, token);

        private static string CommaMissing(IExpression previousExpression)
            => string.Format(CultureInfo.InvariantCulture, Resource.CommaParseException, previousExpression);
    }
}