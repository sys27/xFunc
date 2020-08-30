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
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Resources;
using xFunc.Maths.Tokenization.Tokens;

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
        /// <param name="tokens">The list of tokens.</param>
        /// <returns>The parsed expression.</returns>
        public IExpression Parse(IEnumerable<IToken> tokens)
        {
            if (tokens == null)
                throw new ArgumentNullException(nameof(tokens));

            var tokenReader = new TokenReader(tokens);
            try
            {
                var exp = Statement(ref tokenReader);
                var token = tokenReader.GetCurrent<IToken>();
                if (exp == null || !tokenReader.IsEnd || token != null)
                    throw new ParseException(Resource.ErrorWhileParsingTree);

                return exp;
            }
            finally
            {
                tokenReader.Dispose();
            }
        }

        private IExpression Statement(ref TokenReader tokenReader)
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

        private IExpression UnaryAssign(ref TokenReader tokenReader)
        {
            var scope = tokenReader.CreateScope();

            var left = Variable(ref tokenReader);
            if (left != null)
            {
                var @operator = tokenReader.Operator(OperatorToken.Increment) ??
                                tokenReader.Operator(OperatorToken.Decrement);
                if (@operator != null)
                {
                    tokenReader.Commit();

                    return CreateUnaryAssign(@operator, left);
                }
            }

            tokenReader.Rollback(scope);

            return null;
        }

        private IExpression BinaryAssign(ref TokenReader tokenReader)
        {
            var scope = tokenReader.CreateScope();

            var left = Variable(ref tokenReader);
            if (left == null)
            {
                tokenReader.Rollback(scope);

                return null;
            }

            var @operator = tokenReader.Operator(OperatorToken.MulAssign) ??
                            tokenReader.Operator(OperatorToken.DivAssign) ??
                            tokenReader.Operator(OperatorToken.AddAssign) ??
                            tokenReader.Operator(OperatorToken.SubAssign);
            if (@operator != null)
            {
                var right = Expression(ref tokenReader) ??
                            throw new ParseException(SecondOperand(@operator));

                tokenReader.Commit();

                return CreateBinaryAssign(@operator, left, right);
            }

            tokenReader.Rollback(scope);

            return null;
        }

        private IExpression AssignmentKey(ref TokenReader tokenReader)
        {
            return FunctionDeclaration(ref tokenReader) ??
                   Variable(ref tokenReader);
        }

        private IExpression Assign(ref TokenReader tokenReader)
        {
            var scope = tokenReader.CreateScope();

            var left = AssignmentKey(ref tokenReader);
            if (left == null)
            {
                tokenReader.Rollback(scope);

                return null;
            }

            var @operator = tokenReader.Operator(OperatorToken.Assign);
            if (@operator != null)
            {
                var right = Expression(ref tokenReader) ??
                            throw new ParseException(SecondOperand(@operator));

                tokenReader.Commit();

                return CreateAssign(left, right);
            }

            tokenReader.Rollback(scope);

            return null;
        }

        private IExpression Def(ref TokenReader tokenReader)
        {
            var def = tokenReader.Keyword(KeywordToken.Define);
            if (def == null)
                return null;

            if (!tokenReader.Symbol(SymbolToken.OpenParenthesis))
                throw new ParseException(OpenParenthesis(def));

            var key = AssignmentKey(ref tokenReader) ??
                      throw new ParseException(Resource.AssignKeyParseException);

            if (!tokenReader.Symbol(SymbolToken.Comma))
                throw new ParseException(CommaMissing(key));

            var value = Expression(ref tokenReader) ??
                        throw new ParseException(Resource.DefValueParseException);

            if (!tokenReader.Symbol(SymbolToken.CloseParenthesis))
                throw new ParseException(CloseParenthesis(def));

            return CreateAssign(key, value);
        }

        private IExpression Undef(ref TokenReader tokenReader)
        {
            var undef = tokenReader.Keyword(KeywordToken.Undefine);
            if (undef == null)
                return null;

            if (!tokenReader.Symbol(SymbolToken.OpenParenthesis))
                throw new ParseException(OpenParenthesis(undef));

            var key = AssignmentKey(ref tokenReader) ??
                      throw new ParseException(Resource.AssignKeyParseException);

            if (!tokenReader.Symbol(SymbolToken.CloseParenthesis))
                throw new ParseException(CloseParenthesis(undef));

            return CreateUndef(key);
        }

        private IExpression If(ref TokenReader tokenReader)
        {
            var @if = tokenReader.Keyword(KeywordToken.If);
            if (@if == null)
                return null;

            if (!tokenReader.Symbol(SymbolToken.OpenParenthesis))
                throw new ParseException(OpenParenthesis(@if));

            var condition = ConditionalOperator(ref tokenReader) ??
                            throw new ParseException(Resource.IfConditionParseException);

            if (!tokenReader.Symbol(SymbolToken.Comma))
                throw new ParseException(CommaMissing(condition));

            var then = Expression(ref tokenReader) ??
                       throw new ParseException(Resource.IfThenParseException);

            IExpression @else = null;
            if (tokenReader.Symbol(SymbolToken.Comma))
                @else = Expression(ref tokenReader) ??
                        throw new ParseException(Resource.IfElseParseException);

            if (!tokenReader.Symbol(SymbolToken.CloseParenthesis))
                throw new ParseException(CloseParenthesis(@if));

            if (@else != null)
                return CreateIf(condition, then, @else);

            return CreateIf(condition, then);
        }

        private IExpression For(ref TokenReader tokenReader)
        {
            var @for = tokenReader.Keyword(KeywordToken.For);
            if (@for == null)
                return null;

            if (!tokenReader.Symbol(SymbolToken.OpenParenthesis))
                throw new ParseException(OpenParenthesis(@for));

            var body = Statement(ref tokenReader) ??
                       throw new ParseException(Resource.ForBodyParseException);

            if (!tokenReader.Symbol(SymbolToken.Comma))
                throw new ParseException(CommaMissing(body));

            var init = Statement(ref tokenReader) ??
                       throw new ParseException(Resource.ForInitParseException);

            if (!tokenReader.Symbol(SymbolToken.Comma))
                throw new ParseException(CommaMissing(init));

            var condition = ConditionalOperator(ref tokenReader) ??
                            throw new ParseException(Resource.ForConditionParseException);

            if (!tokenReader.Symbol(SymbolToken.Comma))
                throw new ParseException(CommaMissing(condition));

            var iter = Statement(ref tokenReader) ??
                       throw new ParseException(Resource.ForIterParseException);

            if (!tokenReader.Symbol(SymbolToken.CloseParenthesis))
                throw new ParseException(CloseParenthesis(@for));

            return CreateFor(body, init, condition, iter);
        }

        private IExpression While(ref TokenReader tokenReader)
        {
            var @while = tokenReader.Keyword(KeywordToken.While);
            if (@while == null)
                return null;

            if (!tokenReader.Symbol(SymbolToken.OpenParenthesis))
                throw new ParseException(OpenParenthesis(@while));

            var body = Statement(ref tokenReader) ??
                       throw new ParseException(Resource.WhileBodyParseException);

            if (!tokenReader.Symbol(SymbolToken.Comma))
                throw new ParseException(CommaMissing(body));

            var condition = ConditionalOperator(ref tokenReader) ??
                            throw new ParseException(Resource.WhileConditionParseException);

            if (!tokenReader.Symbol(SymbolToken.CloseParenthesis))
                throw new ParseException(CloseParenthesis(@while));

            return CreateWhile(body, condition);
        }

        private IExpression FunctionDeclaration(ref TokenReader tokenReader)
        {
            var scope = tokenReader.CreateScope();

            var id = tokenReader.GetCurrent<IdToken>();
            if (id != null && tokenReader.Symbol(SymbolToken.OpenParenthesis))
            {
                var parameterList = new List<IExpression>();

                var exp = Variable(ref tokenReader);
                if (exp != null)
                {
                    parameterList.Add(exp);

                    while (tokenReader.Symbol(SymbolToken.Comma))
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

                if (tokenReader.Symbol(SymbolToken.CloseParenthesis))
                {
                    tokenReader.Commit();

                    return CreateFunction(id, parameterList);
                }
            }

            tokenReader.Rollback(scope);

            return null;
        }

        private IExpression Expression(ref TokenReader tokenReader)
        {
            return Ternary(ref tokenReader);
        }

        private IExpression Ternary(ref TokenReader tokenReader)
        {
            var scope = tokenReader.CreateScope();

            var condition = ConditionalOperator(ref tokenReader);
            if (condition == null)
            {
                tokenReader.Rollback(scope);

                return null;
            }

            if (!tokenReader.Symbol(SymbolToken.QuestionMark))
            {
                tokenReader.Commit();

                return condition;
            }

            var then = Expression(ref tokenReader) ??
                       throw new ParseException(Resource.TernaryThenParseException);

            if (!tokenReader.Symbol(SymbolToken.Colon))
                throw new ParseException(Resource.TernaryColonParseException);

            var @else = Expression(ref tokenReader) ??
                        throw new ParseException(Resource.TernaryElseParseException);

            tokenReader.Commit();

            return CreateIf(condition, then, @else);
        }

        private IExpression ConditionalOperator(ref TokenReader tokenReader)
        {
            var left = BitwiseOperator(ref tokenReader);
            if (left == null)
                return null;

            while (true)
            {
                var @operator = tokenReader.Operator(OperatorToken.ConditionalAnd) ??
                                tokenReader.Operator(OperatorToken.ConditionalOr);
                if (@operator == null)
                    return left;

                var right = BitwiseOperator(ref tokenReader) ??
                            throw new ParseException(SecondOperand(@operator));

                left = CreateConditionalOperator(@operator, left, right);
            }
        }

        private IExpression BitwiseOperator(ref TokenReader tokenReader)
        {
            var left = EqualityOperator(ref tokenReader);
            if (left == null)
                return null;

            while (true)
            {
                var token = (tokenReader.Operator(OperatorToken.And) ??
                             tokenReader.Operator(OperatorToken.Or) ??
                             tokenReader.Operator(OperatorToken.Implication) ??
                             tokenReader.Operator(OperatorToken.Equality)) ??
                            (IToken)(tokenReader.Keyword(KeywordToken.NAnd) ??
                                     tokenReader.Keyword(KeywordToken.NOr) ??
                                     tokenReader.Keyword(KeywordToken.And) ??
                                     tokenReader.Keyword(KeywordToken.Or) ??
                                     tokenReader.Keyword(KeywordToken.XOr) ??
                                     tokenReader.Keyword(KeywordToken.Eq) ??
                                     tokenReader.Keyword(KeywordToken.Impl));

                if (token == null)
                    return left;

                var right = EqualityOperator(ref tokenReader) ??
                            throw new ParseException(SecondOperand(token));

                left = CreateBitwiseOperator(token, left, right);
            }
        }

        private IExpression EqualityOperator(ref TokenReader tokenReader)
        {
            var left = AddSub(ref tokenReader);
            if (left == null)
                return null;

            while (true)
            {
                var @operator = tokenReader.Operator(OperatorToken.Equal) ??
                                tokenReader.Operator(OperatorToken.NotEqual) ??
                                tokenReader.Operator(OperatorToken.LessThan) ??
                                tokenReader.Operator(OperatorToken.LessOrEqual) ??
                                tokenReader.Operator(OperatorToken.GreaterThan) ??
                                tokenReader.Operator(OperatorToken.GreaterOrEqual);
                if (@operator == null)
                    return left;

                var right = AddSub(ref tokenReader) ??
                            throw new ParseException(SecondOperand(@operator));

                left = CreateEqualityOperator(@operator, left, right);
            }
        }

        private IExpression AddSub(ref TokenReader tokenReader)
        {
            var left = MulDivMod(ref tokenReader);
            if (left == null)
                return null;

            while (true)
            {
                var @operator = tokenReader.Operator(OperatorToken.Plus) ??
                                tokenReader.Operator(OperatorToken.Minus);
                if (@operator == null)
                    return left;

                var right = MulDivMod(ref tokenReader) ??
                            throw new ParseException(SecondOperand(@operator));

                left = CreateAddSub(@operator, left, right);
            }
        }

        private IExpression MulDivMod(ref TokenReader tokenReader)
        {
            var left = MulImplicit(ref tokenReader);
            if (left == null)
                return null;

            while (true)
            {
                var token = (tokenReader.Operator(OperatorToken.Multiplication) ??
                             tokenReader.Operator(OperatorToken.Division) ??
                             tokenReader.Operator(OperatorToken.Modulo)) ??
                            (IToken)tokenReader.Keyword(KeywordToken.Mod);

                if (token == null)
                    return left;

                var right = MulImplicit(ref tokenReader) ??
                            throw new ParseException(SecondOperand(token));

                left = CreateMulDivMod(token, left, right);
            }
        }

        private IExpression MulImplicit(ref TokenReader tokenReader)
        {
            return MulImplicitLeftUnary(ref tokenReader) ??
                   LeftUnary(ref tokenReader);
        }

        private IExpression MulImplicitLeftUnary(ref TokenReader tokenReader)
        {
            var scope = tokenReader.CreateScope();

            var @operator = tokenReader.Operator(OperatorToken.Minus);
            var number = Number(ref tokenReader);
            if (number != null)
            {
                var rightUnary = MulImplicitExponentiation(ref tokenReader) ??
                                 ParenthesesExpression(ref tokenReader) ??
                                 Matrix(ref tokenReader) ??
                                 Vector(ref tokenReader);
                if (rightUnary != null)
                {
                    if (@operator != null)
                        number = CreateUnaryMinus(number);

                    tokenReader.Commit();

                    // TODO:
                    return CreateMultiplication(number, rightUnary);
                }
            }

            tokenReader.Rollback(scope);

            return null;
        }

        private IExpression MulImplicitExponentiation(ref TokenReader tokenReader)
        {
            var left = Function(ref tokenReader) ??
                       Variable(ref tokenReader);
            if (left == null)
                return null;

            var @operator = tokenReader.Operator(OperatorToken.Exponentiation);
            if (@operator == null)
                return left;

            var right = Exponentiation(ref tokenReader) ??
                        throw new ParseException(Resource.ExponentParseException);

            return CreateExponentiation(left, right);
        }

        private IExpression LeftUnary(ref TokenReader tokenReader)
        {
            var token = (tokenReader.Operator(OperatorToken.Not) ??
                         tokenReader.Operator(OperatorToken.Minus) ??
                         tokenReader.Operator(OperatorToken.Plus)) ??
                        (IToken)tokenReader.Keyword(KeywordToken.Not);
            var operand = Exponentiation(ref tokenReader);
            if (token == null || token == OperatorToken.Plus)
                return operand;

            if (token == OperatorToken.Minus)
                return CreateUnaryMinus(operand);

            return CreateNot(operand);
        }

        private IExpression Exponentiation(ref TokenReader tokenReader)
        {
            var left = RightUnary(ref tokenReader);
            if (left == null)
                return null;

            var @operator = tokenReader.Operator(OperatorToken.Exponentiation);
            if (@operator == null)
                return left;

            var right = LeftUnary(ref tokenReader) ??
                        throw new ParseException(Resource.ExponentParseException);

            return CreateExponentiation(left, right);
        }

        private IExpression RightUnary(ref TokenReader tokenReader)
        {
            var scope = tokenReader.CreateScope();

            var number = Number(ref tokenReader);
            if (number != null)
            {
                var @operator = tokenReader.Operator(OperatorToken.Factorial);
                if (@operator != null)
                {
                    tokenReader.Commit();

                    return CreateFactorial(number);
                }
            }

            tokenReader.Rollback(scope);

            return Operand(ref tokenReader);
        }

        private IExpression Operand(ref TokenReader tokenReader)
        {
            return ComplexNumber(ref tokenReader) ??
                   Number(ref tokenReader) ??
                   Function(ref tokenReader) ??
                   Variable(ref tokenReader) ??
                   Boolean(ref tokenReader) ??
                   ParenthesesExpression(ref tokenReader) ??
                   Matrix(ref tokenReader) ??
                   Vector(ref tokenReader);
        }

        private IExpression ParenthesesExpression(ref TokenReader tokenReader)
        {
            if (!tokenReader.Symbol(SymbolToken.OpenParenthesis))
                return null;

            var exp = Expression(ref tokenReader) ??
                      throw new ParseException(Resource.ExpParenParseException);

            if (!tokenReader.Symbol(SymbolToken.CloseParenthesis))
                throw new ParseException(string.Format(CultureInfo.InvariantCulture, Resource.CloseParenParseException, exp));

            return exp;
        }

        private IExpression Function(ref TokenReader tokenReader)
        {
            var function = tokenReader.GetCurrent<IdToken>();
            if (function == null)
                return null;

            var parameterList = ParameterList(ref tokenReader);
            if (parameterList == null)
                return CreateVariable(function);

            return CreateFunction(function, parameterList);
        }

        private IList<IExpression> ParameterList(ref TokenReader tokenReader)
        {
            if (!tokenReader.Symbol(SymbolToken.OpenParenthesis))
                return null;

            var parameterList = new List<IExpression>();

            var exp = Expression(ref tokenReader);
            if (exp != null)
            {
                parameterList.Add(exp);

                while (tokenReader.Symbol(SymbolToken.Comma))
                {
                    exp = Expression(ref tokenReader) ??
                          throw new ParseException(CommaMissing(exp));

                    parameterList.Add(exp);
                }
            }

            if (!tokenReader.Symbol(SymbolToken.CloseParenthesis))
                throw new ParseException(Resource.ParameterListCloseParseException);

            return parameterList;
        }

        private IExpression Number(ref TokenReader tokenReader)
        {
            var number = tokenReader.GetCurrent<NumberToken>();
            if (number == null)
                return null;

            if (tokenReader.Symbol(SymbolToken.Degree) ||
                tokenReader.Keyword(KeywordToken.Degree) != null)
                return CreateAngleNumber(number, AngleUnit.Degree);

            if (tokenReader.Keyword(KeywordToken.Radian) != null)
                return CreateAngleNumber(number, AngleUnit.Radian);

            if (tokenReader.Keyword(KeywordToken.Gradian) != null)
                return CreateAngleNumber(number, AngleUnit.Gradian);

            return CreateNumber(number);
        }

        private IExpression ComplexNumber(ref TokenReader tokenReader)
        {
            var scope = tokenReader.CreateScope();

            var magnitudeSign = tokenReader.Operator(OperatorToken.Plus) ??
                                tokenReader.Operator(OperatorToken.Minus);
            var magnitude = tokenReader.GetCurrent<NumberToken>();
            if (magnitude != null)
            {
                var hasAngleSymbol = tokenReader.Symbol(SymbolToken.Angle);
                if (hasAngleSymbol)
                {
                    var phaseSign = tokenReader.Operator(OperatorToken.Plus) ??
                                    tokenReader.Operator(OperatorToken.Minus);
                    var phase = tokenReader.GetCurrent<NumberToken>();
                    if (phase == null)
                        throw new ParseException(Resource.PhaseParseException);

                    var hasDegreeSymbol = tokenReader.Symbol(SymbolToken.Degree);
                    if (!hasDegreeSymbol)
                        throw new ParseException(Resource.DegreeComplexNumberParseException);

                    tokenReader.Commit();

                    return CreateComplexNumber(magnitudeSign, magnitude, phaseSign, phase);
                }
            }

            tokenReader.Rollback(scope);

            return null;
        }

        private Variable Variable(ref TokenReader tokenReader)
        {
            var variable = tokenReader.GetCurrent<IdToken>();
            if (variable == null)
                return null;

            return CreateVariable(variable);
        }

        private IExpression Boolean(ref TokenReader tokenReader)
        {
            var boolean = tokenReader.Keyword(KeywordToken.True) ??
                          tokenReader.Keyword(KeywordToken.False);
            if (boolean == null)
                return null;

            return CreateBoolean(boolean);
        }

        private Vector Vector(ref TokenReader tokenReader)
        {
            if (!tokenReader.Symbol(SymbolToken.OpenBrace))
                return null;

            var parameterList = new List<IExpression>();

            var exp = Expression(ref tokenReader);
            if (exp != null)
            {
                parameterList.Add(exp);

                while (tokenReader.Symbol(SymbolToken.Comma))
                {
                    exp = Expression(ref tokenReader) ??
                          throw new ParseException(Resource.VectorCommaParseException);

                    parameterList.Add(exp);
                }
            }

            if (!tokenReader.Symbol(SymbolToken.CloseBrace))
                throw new ParseException(Resource.VectorCloseBraceParseException);

            return CreateVector(parameterList);
        }

        private IExpression Matrix(ref TokenReader tokenReader)
        {
            var scope = tokenReader.CreateScope();

            if (tokenReader.Symbol(SymbolToken.OpenBrace))
            {
                var vectors = new List<Vector>();

                var exp = Vector(ref tokenReader);
                if (exp != null)
                {
                    vectors.Add(exp);

                    while (tokenReader.Symbol(SymbolToken.Comma))
                    {
                        exp = Vector(ref tokenReader) ??
                              throw new ParseException(Resource.MatrixCommaParseException);

                        vectors.Add(exp);
                    }

                    if (!tokenReader.Symbol(SymbolToken.CloseBrace))
                        throw new ParseException(Resource.MatrixCloseBraceParseException);

                    tokenReader.Commit();

                    return CreateMatrix(vectors);
                }
            }

            tokenReader.Rollback(scope);

            return null;
        }

        private static string SecondOperand(IToken token)
            => string.Format(CultureInfo.InvariantCulture, Resource.SecondOperandParseException, token);

        private static string OpenParenthesis(IToken token)
            => string.Format(CultureInfo.InvariantCulture, Resource.FunctionOpenParenthesisParseException, token);

        private static string CloseParenthesis(IToken token)
            => string.Format(CultureInfo.InvariantCulture, Resource.FunctionCloseParenthesisParseException, token);

        private static string CommaMissing(IExpression previousExpression)
            => string.Format(CultureInfo.InvariantCulture, Resource.CommaParseException, previousExpression);
    }
}