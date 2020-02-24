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
using System.Linq;
using xFunc.Maths.Analyzers;
using xFunc.Maths.Expressions;
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

            // TODO: !!!
            var tokensArray = tokens as IToken[] ?? tokens.ToArray();
            if (!tokensArray.Any())
                throw new ArgumentNullException(nameof(tokens));

            var tokenEnumerator = new TokenEnumerator(tokensArray);
            var exp = Statement(tokenEnumerator);
            if (exp == null || !tokenEnumerator.IsEnd)
                throw new ParseException(Resource.ErrorWhileParsingTree);

            return exp;
        }

        private IExpression Statement(TokenEnumerator tokenEnumerator)
        {
            return UnaryAssign(tokenEnumerator) ??
                   BinaryAssign(tokenEnumerator) ??
                   Assign(tokenEnumerator) ??
                   Def(tokenEnumerator) ??
                   Undef(tokenEnumerator) ??
                   If(tokenEnumerator) ??
                   For(tokenEnumerator) ??
                   While(tokenEnumerator) ??
                   Expression(tokenEnumerator);
        }

        private IExpression UnaryAssign(TokenEnumerator tokenEnumerator)
        {
            var scope = tokenEnumerator.CreateScope();

            var left = Variable(tokenEnumerator);
            if (left != null)
            {
                var @operator = tokenEnumerator.Operator(OperatorToken.Increment) ??
                                tokenEnumerator.Operator(OperatorToken.Decrement);
                if (@operator != null)
                    return CreateOperator(@operator, left);
            }

            tokenEnumerator.Rollback(scope);
            return null;
        }

        private IExpression BinaryAssign(TokenEnumerator tokenEnumerator)
        {
            var scope = tokenEnumerator.CreateScope();

            var left = Variable(tokenEnumerator);
            if (left == null)
                return null;

            var @operator = tokenEnumerator.Operator(OperatorToken.MulAssign) ??
                            tokenEnumerator.Operator(OperatorToken.DivAssign) ??
                            tokenEnumerator.Operator(OperatorToken.AddAssign) ??
                            tokenEnumerator.Operator(OperatorToken.SubAssign);
            if (@operator != null)
            {
                var right = Expression(tokenEnumerator) ??
                            throw new ParseException(SecondOperand(@operator));

                return CreateOperator(@operator, left, right);
            }

            tokenEnumerator.Rollback(scope);
            return null;
        }

        private IExpression AssignmentKey(TokenEnumerator tokenEnumerator)
        {
            return FunctionDeclaration(tokenEnumerator) ??
                   Variable(tokenEnumerator);
        }

        private IExpression Assign(TokenEnumerator tokenEnumerator)
        {
            var scope = tokenEnumerator.CreateScope();

            var left = AssignmentKey(tokenEnumerator);
            if (left == null)
                return null;

            var @operator = tokenEnumerator.Operator(OperatorToken.Assign);
            if (@operator != null)
            {
                var right = Expression(tokenEnumerator) ??
                            throw new ParseException(SecondOperand(@operator));

                return CreateOperator(@operator, left, right);
            }

            tokenEnumerator.Rollback(scope);
            return null;
        }

        private IExpression Def(TokenEnumerator tokenEnumerator)
        {
            var def = tokenEnumerator.Keyword(KeywordToken.Define);
            if (def == null)
                return null;

            if (!tokenEnumerator.Symbol(SymbolToken.OpenParenthesis))
                throw new ParseException(OpenParenthesis(def));

            var key = AssignmentKey(tokenEnumerator) ??
                      throw new ParseException(Resource.AssignKeyParseException);

            if (!tokenEnumerator.Symbol(SymbolToken.Comma))
                throw new ParseException(CommaMissing(key));

            var value = Expression(tokenEnumerator) ??
                        throw new ParseException(Resource.DefValueParseException);

            if (!tokenEnumerator.Symbol(SymbolToken.CloseParenthesis))
                throw new ParseException(CloseParenthesis(def));

            return CreateFromKeyword(def, key, value);
        }

        private IExpression Undef(TokenEnumerator tokenEnumerator)
        {
            var undef = tokenEnumerator.Keyword(KeywordToken.Undefine);
            if (undef == null)
                return null;

            if (!tokenEnumerator.Symbol(SymbolToken.OpenParenthesis))
                throw new ParseException(OpenParenthesis(undef));

            var key = AssignmentKey(tokenEnumerator) ??
                      throw new ParseException(Resource.AssignKeyParseException);

            if (!tokenEnumerator.Symbol(SymbolToken.CloseParenthesis))
                throw new ParseException(CloseParenthesis(undef));

            return CreateFromKeyword(undef, key);
        }

        private IExpression If(TokenEnumerator tokenEnumerator)
        {
            var @if = tokenEnumerator.Keyword(KeywordToken.If);
            if (@if == null)
                return null;

            if (!tokenEnumerator.Symbol(SymbolToken.OpenParenthesis))
                throw new ParseException(OpenParenthesis(@if));

            var condition = ConditionalOperator(tokenEnumerator) ??
                            throw new ParseException(Resource.IfConditionParseException);

            if (!tokenEnumerator.Symbol(SymbolToken.Comma))
                throw new ParseException(CommaMissing(condition));

            var then = Expression(tokenEnumerator) ??
                       throw new ParseException(Resource.IfThenParseException);

            IExpression @else = null;
            if (tokenEnumerator.Symbol(SymbolToken.Comma))
                @else = Expression(tokenEnumerator) ??
                        throw new ParseException(Resource.IfElseParseException);

            if (!tokenEnumerator.Symbol(SymbolToken.CloseParenthesis))
                throw new ParseException(CloseParenthesis(@if));

            if (@else != null)
                return CreateFromKeyword(@if, condition, then, @else);

            return CreateFromKeyword(@if, condition, then);
        }

        private IExpression For(TokenEnumerator tokenEnumerator)
        {
            var @for = tokenEnumerator.Keyword(KeywordToken.For);
            if (@for == null)
                return null;

            if (!tokenEnumerator.Symbol(SymbolToken.OpenParenthesis))
                throw new ParseException(OpenParenthesis(@for));

            var body = Statement(tokenEnumerator) ??
                       throw new ParseException(Resource.ForBodyParseException);

            if (!tokenEnumerator.Symbol(SymbolToken.Comma))
                throw new ParseException(CommaMissing(body));

            var init = Statement(tokenEnumerator) ??
                       throw new ParseException(Resource.ForInitParseException);

            if (!tokenEnumerator.Symbol(SymbolToken.Comma))
                throw new ParseException(CommaMissing(init));

            var condition = ConditionalOperator(tokenEnumerator) ??
                            throw new ParseException(Resource.ForConditionParseException);

            if (!tokenEnumerator.Symbol(SymbolToken.Comma))
                throw new ParseException(CommaMissing(condition));

            var iter = Statement(tokenEnumerator) ??
                       throw new ParseException(Resource.ForIterParseException);

            if (!tokenEnumerator.Symbol(SymbolToken.CloseParenthesis))
                throw new ParseException(CloseParenthesis(@for));

            return CreateFromKeyword(@for, body, init, condition, iter);
        }

        private IExpression While(TokenEnumerator tokenEnumerator)
        {
            var @while = tokenEnumerator.Keyword(KeywordToken.While);
            if (@while == null)
                return null;

            if (!tokenEnumerator.Symbol(SymbolToken.OpenParenthesis))
                throw new ParseException(OpenParenthesis(@while));

            var body = Statement(tokenEnumerator) ??
                       throw new ParseException(Resource.WhileBodyParseException);

            if (!tokenEnumerator.Symbol(SymbolToken.Comma))
                throw new ParseException(CommaMissing(body));

            var condition = ConditionalOperator(tokenEnumerator) ??
                            throw new ParseException(Resource.WhileConditionParseException);

            if (!tokenEnumerator.Symbol(SymbolToken.CloseParenthesis))
                throw new ParseException(CloseParenthesis(@while));

            return CreateFromKeyword(@while, body, condition);
        }

        private IExpression FunctionDeclaration(TokenEnumerator tokenEnumerator)
        {
            var scope = tokenEnumerator.CreateScope();

            var id = tokenEnumerator.GetCurrent<IdToken>();
            if (id != null && tokenEnumerator.Symbol(SymbolToken.OpenParenthesis))
            {
                var parameterList = new List<IExpression>();

                var exp = Variable(tokenEnumerator);
                if (exp != null)
                {
                    parameterList.Add(exp);

                    while (tokenEnumerator.Symbol(SymbolToken.Comma))
                    {
                        exp = Variable(tokenEnumerator);
                        if (exp == null)
                        {
                            tokenEnumerator.Rollback(scope);
                            return null;
                        }

                        parameterList.Add(exp);
                    }
                }

                if (tokenEnumerator.Symbol(SymbolToken.CloseParenthesis))
                    return CreateFunction(id, parameterList.ToArray()); // TODO:
            }

            tokenEnumerator.Rollback(scope);

            return null;
        }

        private IExpression Expression(TokenEnumerator tokenEnumerator)
        {
            return Binary(tokenEnumerator);
        }

        private IExpression Binary(TokenEnumerator tokenEnumerator)
        {
            return ConditionalOperator(tokenEnumerator);
        }

        private IExpression ConditionalOperator(TokenEnumerator tokenEnumerator)
        {
            var left = BitwiseOperator(tokenEnumerator);
            if (left == null)
                return null;

            while (true)
            {
                var @operator = tokenEnumerator.Operator(OperatorToken.ConditionalAnd) ??
                                tokenEnumerator.Operator(OperatorToken.ConditionalOr);
                if (@operator == null)
                    return left;

                var right = BitwiseOperator(tokenEnumerator) ??
                            throw new ParseException(SecondOperand(@operator));

                left = CreateOperator(@operator, left, right);
            }
        }

        private IExpression BitwiseOperator(TokenEnumerator tokenEnumerator)
        {
            var left = EqualityOperator(tokenEnumerator);
            if (left == null)
                return null;

            while (true)
            {
                var token = (tokenEnumerator.Operator(OperatorToken.And) ??
                             tokenEnumerator.Operator(OperatorToken.Or) ??
                             tokenEnumerator.Operator(OperatorToken.Implication) ??
                             tokenEnumerator.Operator(OperatorToken.Equality)) ??
                             (IToken)(tokenEnumerator.Keyword(KeywordToken.NAnd) ??
                                      tokenEnumerator.Keyword(KeywordToken.NOr) ??
                                      tokenEnumerator.Keyword(KeywordToken.And) ??
                                      tokenEnumerator.Keyword(KeywordToken.Or) ??
                                      tokenEnumerator.Keyword(KeywordToken.XOr) ??
                                      tokenEnumerator.Keyword(KeywordToken.Eq) ??
                                      tokenEnumerator.Keyword(KeywordToken.Impl));

                if (token == null)
                    return left;

                var right = EqualityOperator(tokenEnumerator) ??
                            throw new ParseException(SecondOperand(token));

                left = CreateOperatorOrKeyword(token, left, right);
            }
        }

        private IExpression EqualityOperator(TokenEnumerator tokenEnumerator)
        {
            var left = AddSub(tokenEnumerator);
            if (left == null)
                return null;

            while (true)
            {
                var @operator = tokenEnumerator.Operator(OperatorToken.Equal) ??
                                tokenEnumerator.Operator(OperatorToken.NotEqual) ??
                                tokenEnumerator.Operator(OperatorToken.LessThan) ??
                                tokenEnumerator.Operator(OperatorToken.LessOrEqual) ??
                                tokenEnumerator.Operator(OperatorToken.GreaterThan) ??
                                tokenEnumerator.Operator(OperatorToken.GreaterOrEqual);
                if (@operator == null)
                    return left;

                var right = AddSub(tokenEnumerator) ??
                            throw new ParseException(SecondOperand(@operator));

                left = CreateOperator(@operator, left, right);
            }
        }

        private IExpression AddSub(TokenEnumerator tokenEnumerator)
        {
            var left = MulDivMod(tokenEnumerator);
            if (left == null)
                return null;

            while (true)
            {
                var @operator = tokenEnumerator.Operator(OperatorToken.Plus) ??
                                tokenEnumerator.Operator(OperatorToken.Minus);
                if (@operator == null)
                    return left;

                var right = MulDivMod(tokenEnumerator) ??
                            throw new ParseException(SecondOperand(@operator));

                left = CreateOperator(@operator, left, right);
            }
        }

        private IExpression MulDivMod(TokenEnumerator tokenEnumerator)
        {
            var left = MulImplicit(tokenEnumerator);
            if (left == null)
                return null;

            while (true)
            {
                var token = (tokenEnumerator.Operator(OperatorToken.Multiplication) ??
                             tokenEnumerator.Operator(OperatorToken.Division) ??
                             tokenEnumerator.Operator(OperatorToken.Modulo)) ??
                             (IToken)tokenEnumerator.Keyword(KeywordToken.Mod);

                if (token == null)
                    return left;

                var right = MulImplicit(tokenEnumerator) ??
                            throw new ParseException(SecondOperand(token));

                left = CreateOperatorOrKeyword(token, left, right);
            }
        }

        private IExpression MulImplicit(TokenEnumerator tokenEnumerator)
        {
            return MulImplicitLeftUnary(tokenEnumerator) ??
                   LeftUnary(tokenEnumerator);
        }

        private IExpression MulImplicitLeftUnary(TokenEnumerator tokenEnumerator)
        {
            var scope = tokenEnumerator.CreateScope();

            var @operator = tokenEnumerator.Operator(OperatorToken.Minus);
            var number = Number(tokenEnumerator);
            if (number != null)
            {
                var rightUnary = Function(tokenEnumerator) ??
                                 Variable(tokenEnumerator) ??
                                 ParenthesesExpression(tokenEnumerator) ??
                                 Matrix(tokenEnumerator) ??
                                 Vector(tokenEnumerator);
                if (rightUnary != null)
                {
                    if (@operator != null)
                        number = CreateUnaryMinus(number);

                    return CreateMultiplication(number, rightUnary);
                }
            }

            tokenEnumerator.Rollback(scope);
            return null;
        }

        private IExpression LeftUnary(TokenEnumerator tokenEnumerator)
        {
            var token = (tokenEnumerator.Operator(OperatorToken.Not) ??
                         tokenEnumerator.Operator(OperatorToken.Minus) ??
                         tokenEnumerator.Operator(OperatorToken.Plus)) ??
                         (IToken)tokenEnumerator.Keyword(KeywordToken.Not);
            var operand = Exponentiation(tokenEnumerator);
            if (token == null || token == OperatorToken.Plus)
                return operand;

            if (token == OperatorToken.Minus)
                return CreateUnaryMinus(operand);

            return CreateOperatorOrKeyword(token, operand);
        }

        private IExpression Exponentiation(TokenEnumerator tokenEnumerator)
        {
            var left = RightUnary(tokenEnumerator);
            if (left == null)
                return null;

            var @operator = tokenEnumerator.Operator(OperatorToken.Exponentiation);
            if (@operator == null)
                return left;

            var right = Exponentiation(tokenEnumerator) ??
                        throw new ParseException(Resource.ExponentParseException);

            return CreateOperator(@operator, left, right);
        }

        private IExpression RightUnary(TokenEnumerator tokenEnumerator)
        {
            var scope = tokenEnumerator.CreateScope();

            var number = Number(tokenEnumerator);
            if (number != null)
            {
                var @operator = tokenEnumerator.Operator(OperatorToken.Factorial);
                if (@operator != null)
                    return CreateOperator(@operator, number);

                tokenEnumerator.Rollback(scope);
            }

            return Operand(tokenEnumerator);
        }

        private IExpression Operand(TokenEnumerator tokenEnumerator)
        {
            return ComplexNumber(tokenEnumerator) ??
                   Number(tokenEnumerator) ??
                   Function(tokenEnumerator) ??
                   Variable(tokenEnumerator) ??
                   Boolean(tokenEnumerator) ??
                   ParenthesesExpression(tokenEnumerator) ??
                   Matrix(tokenEnumerator) ??
                   Vector(tokenEnumerator);
        }

        private IExpression ParenthesesExpression(TokenEnumerator tokenEnumerator)
        {
            if (!tokenEnumerator.Symbol(SymbolToken.OpenParenthesis))
                return null;

            var exp = Expression(tokenEnumerator) ??
                      throw new ParseException(Resource.ExpParenParseException);

            if (!tokenEnumerator.Symbol(SymbolToken.CloseParenthesis))
                throw new ParseException(string.Format(CultureInfo.InvariantCulture, Resource.CloseParenParseException, exp));

            return exp;
        }

        private IExpression Function(TokenEnumerator tokenEnumerator)
        {
            var function = tokenEnumerator.GetCurrent<IdToken>();
            if (function == null)
                return null;

            var parameterList = ParameterList(tokenEnumerator);
            if (parameterList == null)
                return CreateVariable(function);

            return CreateFunction(function, parameterList.ToArray());
        }

        private IEnumerable<IExpression> ParameterList(TokenEnumerator tokenEnumerator)
        {
            if (!tokenEnumerator.Symbol(SymbolToken.OpenParenthesis))
                return null;

            var parameterList = new List<IExpression>();

            var exp = Expression(tokenEnumerator);
            if (exp != null)
            {
                parameterList.Add(exp);

                while (tokenEnumerator.Symbol(SymbolToken.Comma))
                {
                    exp = Expression(tokenEnumerator) ??
                          throw new ParseException(CommaMissing(exp));

                    parameterList.Add(exp);
                }
            }

            if (!tokenEnumerator.Symbol(SymbolToken.CloseParenthesis))
                throw new ParseException(Resource.ParameterListCloseParseException);

            return parameterList;
        }

        private IExpression Number(TokenEnumerator tokenEnumerator)
        {
            var number = tokenEnumerator.GetCurrent<NumberToken>();
            if (number == null)
                return null;

            return CreateNumber(number);
        }

        private IExpression ComplexNumber(TokenEnumerator tokenEnumerator)
        {
            var scope = tokenEnumerator.CreateScope();

            var magnitudeSign = tokenEnumerator.Operator(OperatorToken.Plus) ??
                                tokenEnumerator.Operator(OperatorToken.Minus);
            var magnitude = tokenEnumerator.GetCurrent<NumberToken>();
            if (magnitude != null)
            {
                var hasAngleSymbol = tokenEnumerator.Symbol(SymbolToken.Angle);
                if (hasAngleSymbol)
                {
                    var phaseSign = tokenEnumerator.Operator(OperatorToken.Plus) ??
                                    tokenEnumerator.Operator(OperatorToken.Minus);
                    var phase = tokenEnumerator.GetCurrent<NumberToken>();
                    if (phase == null)
                        throw new ParseException(Resource.PhaseParseException);

                    var hasDegreeSymbol = tokenEnumerator.Symbol(SymbolToken.Degree);
                    if (!hasDegreeSymbol)
                        throw new ParseException(Resource.DegreeComplexNumberParseException);

                    return CreateComplexNumber(magnitudeSign, magnitude, phaseSign, phase);
                }
            }

            tokenEnumerator.Rollback(scope);

            return null;
        }

        private IExpression Variable(TokenEnumerator tokenEnumerator)
        {
            var variable = tokenEnumerator.GetCurrent<IdToken>();
            if (variable == null)
                return null;

            return CreateVariable(variable);
        }

        private IExpression Boolean(TokenEnumerator tokenEnumerator)
        {
            var boolean = tokenEnumerator.Keyword(KeywordToken.True) ??
                          tokenEnumerator.Keyword(KeywordToken.False);
            if (boolean == null)
                return null;

            return CreateFromKeyword(boolean);
        }

        private IExpression Vector(TokenEnumerator tokenEnumerator)
        {
            if (!tokenEnumerator.Symbol(SymbolToken.OpenBrace))
                return null;

            var parameterList = new List<IExpression>();

            var exp = Expression(tokenEnumerator);
            if (exp != null)
            {
                parameterList.Add(exp);

                while (tokenEnumerator.Symbol(SymbolToken.Comma))
                {
                    exp = Expression(tokenEnumerator) ??
                          throw new ParseException(Resource.VectorCommaParseException);

                    parameterList.Add(exp);
                }
            }

            if (!tokenEnumerator.Symbol(SymbolToken.CloseBrace))
                throw new ParseException(Resource.VectorCloseBraceParseException);

            return CreateVector(parameterList.ToArray());
        }

        private IExpression Matrix(TokenEnumerator tokenEnumerator)
        {
            var scope = tokenEnumerator.CreateScope();

            if (tokenEnumerator.Symbol(SymbolToken.OpenBrace))
            {
                var parameterList = new List<IExpression>();

                var exp = Vector(tokenEnumerator);
                if (exp != null)
                {
                    parameterList.Add(exp);

                    while (tokenEnumerator.Symbol(SymbolToken.Comma))
                    {
                        exp = Vector(tokenEnumerator) ??
                              throw new ParseException(Resource.MatrixCommaParseException);

                        parameterList.Add(exp);
                    }

                    if (!tokenEnumerator.Symbol(SymbolToken.CloseBrace))
                        throw new ParseException(Resource.MatrixCloseBraceParseException);

                    return CreateMatrix(parameterList.ToArray()); // TODO:
                }
            }

            tokenEnumerator.Rollback(scope);

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