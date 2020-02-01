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
        public Parser() : this(new Differentiator(), new Simplifier())
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

            var tokensArray = tokens as IToken[] ?? tokens.ToArray();
            if (!tokensArray.Any())
                throw new ArgumentException(nameof(tokens));

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
                var @operator = tokenEnumerator.Operator(Operators.Increment |
                                                         Operators.Decrement);
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

            var @operator = tokenEnumerator.Operator(Operators.MulAssign |
                                                     Operators.DivAssign |
                                                     Operators.AddAssign |
                                                     Operators.SubAssign);
            if (@operator != null)
            {
                var right = Expression(tokenEnumerator) ?? throw new ParseException(); // TODO:

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

            var @operator = tokenEnumerator.Operator(Operators.Assign);
            if (@operator != null)
            {
                var right = Expression(tokenEnumerator) ?? throw new ParseException(); // TODO:

                return CreateOperator(@operator, left, right);
            }

            tokenEnumerator.Rollback(scope);
            return null;
        }

        private IExpression Def(TokenEnumerator tokenEnumerator)
        {
            var def = tokenEnumerator.Keyword(Keywords.Define);
            if (def == null)
                return null;

            if (!tokenEnumerator.Symbol(Symbols.OpenParenthesis))
                throw new ParseException(); // TODO:

            var key = AssignmentKey(tokenEnumerator) ?? throw new ParseException(); // TODO:

            if (!tokenEnumerator.Symbol(Symbols.Comma))
                throw new ParseException(); // TODO:

            var value = Expression(tokenEnumerator) ?? throw new ParseException(); // TODO:

            if (!tokenEnumerator.Symbol(Symbols.CloseParenthesis))
                throw new ParseException(); // TODO:

            return CreateFromKeyword(def, key, value);
        }

        private IExpression Undef(TokenEnumerator tokenEnumerator)
        {
            var undef = tokenEnumerator.Keyword(Keywords.Undefine);
            if (undef == null)
                return null;

            if (!tokenEnumerator.Symbol(Symbols.OpenParenthesis))
                throw new ParseException(); // TODO:

            var key = AssignmentKey(tokenEnumerator) ?? throw new ParseException(); // TODO:

            if (!tokenEnumerator.Symbol(Symbols.CloseParenthesis))
                throw new ParseException(); // TODO:

            return CreateFromKeyword(undef, key);
        }

        private IExpression If(TokenEnumerator tokenEnumerator)
        {
            var @if = tokenEnumerator.Keyword(Keywords.If);
            if (@if == null)
                return null;

            if (!tokenEnumerator.Symbol(Symbols.OpenParenthesis))
                throw new ParseException(); // TODO:

            // TODO:
            var condition = ConditionalOperator(tokenEnumerator) ?? throw new ParseException(); // TODO:

            if (!tokenEnumerator.Symbol(Symbols.Comma))
                throw new ParseException(); // TODO:

            var then = Expression(tokenEnumerator) ?? throw new ParseException(); // TODO:

            IExpression @else = null;
            if (tokenEnumerator.Symbol(Symbols.Comma))
                @else = Expression(tokenEnumerator) ?? throw new ParseException(); // TODO:

            if (!tokenEnumerator.Symbol(Symbols.CloseParenthesis))
                throw new ParseException(); // TODO:

            return CreateFromKeyword(@if, condition, then, @else);
        }

        private IExpression For(TokenEnumerator tokenEnumerator)
        {
            var @for = tokenEnumerator.Keyword(Keywords.For);
            if (@for == null)
                return null;

            if (!tokenEnumerator.Symbol(Symbols.OpenParenthesis))
                throw new ParseException(); // TODO:

            // TODO:
            var body = Statement(tokenEnumerator) ?? throw new ParseException(); // TODO:

            if (!tokenEnumerator.Symbol(Symbols.Comma))
                throw new ParseException(); // TODO:

            var init = Statement(tokenEnumerator) ?? throw new ParseException(); // TODO:

            if (!tokenEnumerator.Symbol(Symbols.Comma))
                throw new ParseException(); // TODO:

            var condition = ConditionalOperator(tokenEnumerator) ?? throw new ParseException(); // TODO:

            if (!tokenEnumerator.Symbol(Symbols.Comma))
                throw new ParseException(); // TODO:

            var iter = Statement(tokenEnumerator) ?? throw new ParseException(); // TODO:

            if (!tokenEnumerator.Symbol(Symbols.CloseParenthesis))
                throw new ParseException(); // TODO:

            return CreateFromKeyword(@for, body, init, condition, iter);
        }

        private IExpression While(TokenEnumerator tokenEnumerator)
        {
            var @while = tokenEnumerator.Keyword(Keywords.While);
            if (@while == null)
                return null;

            if (!tokenEnumerator.Symbol(Symbols.OpenParenthesis))
                throw new ParseException(); // TODO:

            // TODO:
            var exp = Statement(tokenEnumerator) ?? throw new ParseException(); // TODO:

            if (!tokenEnumerator.Symbol(Symbols.Comma))
                throw new ParseException(); // TODO:

            var condition = ConditionalOperator(tokenEnumerator) ?? throw new ParseException(); // TODO:

            if (!tokenEnumerator.Symbol(Symbols.CloseParenthesis))
                throw new ParseException(); // TODO:

            return CreateFromKeyword(@while, exp, condition);
        }

        private IExpression FunctionDeclaration(TokenEnumerator tokenEnumerator)
        {
            var scope = tokenEnumerator.CreateScope();

            var id = tokenEnumerator.GetCurrent<IdToken>();
            if (id != null && tokenEnumerator.Symbol(Symbols.OpenParenthesis))
            {
                var parameterList = new List<IExpression>();

                var exp = Variable(tokenEnumerator);
                if (exp != null)
                {
                    parameterList.Add(exp);

                    while (tokenEnumerator.Symbol(Symbols.Comma))
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

                if (tokenEnumerator.Symbol(Symbols.CloseParenthesis))
                    return CreateFunction(id, parameterList.ToArray());
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
                var @operator = tokenEnumerator.Operator(Operators.ConditionalAnd |
                                                         Operators.ConditionalOr);
                if (@operator == null)
                    return left;

                var right = BitwiseOperator(tokenEnumerator) ?? throw new ParseException(); // TODO:

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
                var @operator = tokenEnumerator.Operator(Operators.And |
                                                         Operators.Or |
                                                         Operators.Implication |
                                                         Operators.Equality);
                if (@operator == null)
                {
                    var keyword = tokenEnumerator.Keyword(Keywords.NAnd |
                                                          Keywords.NOr |
                                                          Keywords.And |
                                                          Keywords.Or |
                                                          Keywords.XOr |
                                                          Keywords.Not |
                                                          Keywords.Eq |
                                                          Keywords.Impl);
                    if (keyword != null)
                    {
                        var right2 = EqualityOperator(tokenEnumerator) ?? throw new ParseException(); // TODO:

                        left = CreateFromKeyword(keyword, left, right2);

                        continue;
                    }

                    return left;
                }

                var right = EqualityOperator(tokenEnumerator) ?? throw new ParseException(); // TODO:

                left = CreateOperator(@operator, left, right);
            }
        }

        private IExpression EqualityOperator(TokenEnumerator tokenEnumerator)
        {
            var left = AddSub(tokenEnumerator);
            if (left == null)
                return null;

            while (true)
            {
                var @operator = tokenEnumerator.Operator(Operators.Equal |
                                                         Operators.NotEqual |
                                                         Operators.LessThan |
                                                         Operators.LessOrEqual |
                                                         Operators.GreaterThan |
                                                         Operators.GreaterOrEqual);
                if (@operator == null)
                    return left;

                var right = AddSub(tokenEnumerator) ?? throw new ParseException(); // TODO:

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
                var @operator = tokenEnumerator.Operator(Operators.Plus |
                                                         Operators.Minus);
                if (@operator == null)
                    return left;

                var right = MulDivMod(tokenEnumerator) ?? throw new ParseException(); // TODO:

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
                var @operator = tokenEnumerator.Operator(Operators.Multiplication |
                                                         Operators.Division |
                                                         Operators.Modulo);
                if (@operator == null)
                {
                    // TODO:
                    var keyword = tokenEnumerator.Keyword(Keywords.Mod);
                    if (keyword != null)
                    {
                        var right2 = MulImplicit(tokenEnumerator) ?? throw new ParseException(); // TODO:

                        left = CreateFromKeyword(keyword, left, right2);

                        continue;
                    }

                    return left;
                }

                var right = MulImplicit(tokenEnumerator) ?? throw new ParseException(); // TODO:

                left = CreateOperator(@operator, left, right);
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

            var @operator = tokenEnumerator.Operator(Operators.Minus);
            var number = Number(tokenEnumerator);
            if (number != null)
            {
                var rightUnary = RightUnary(tokenEnumerator); // TODO: operand?
                if (rightUnary != null)
                {
                    if (@operator != null)
                        // TODO:
                        // number = CreateOperator(@operator, number);
                        number = new UnaryMinus(number);

                    // TODO:
                    return CreateOperator(new OperatorToken(Operators.Multiplication), number, rightUnary);
                }
            }

            tokenEnumerator.Rollback(scope);
            return null;
        }

        private IExpression LeftUnary(TokenEnumerator tokenEnumerator)
        {
            var @operator = tokenEnumerator.Operator(Operators.Not |
                                                     Operators.Minus |
                                                     Operators.Plus);
            var operand = Exponentiation(tokenEnumerator);
            if (@operator == null || @operator.Operator == Operators.Plus)
                return operand;

            if (@operator.Operator == Operators.Minus)
                return new UnaryMinus(operand); // TODO:

            return CreateOperator(@operator, operand);
        }

        private IExpression Exponentiation(TokenEnumerator tokenEnumerator)
        {
            var left = RightUnary(tokenEnumerator);
            if (left == null)
                return null;

            var @operator = tokenEnumerator.Operator(Operators.Exponentiation);
            if (@operator == null)
                return left;

            var right = Exponentiation(tokenEnumerator) ?? throw new ParseException(); // TODO:

            return CreateOperator(@operator, left, right);
        }

        private IExpression RightUnary(TokenEnumerator tokenEnumerator)
        {
            var scope = tokenEnumerator.CreateScope();

            var number = Number(tokenEnumerator);
            if (number != null)
            {
                var @operator = tokenEnumerator.Operator(Operators.Factorial);
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
            if (!tokenEnumerator.Symbol(Symbols.OpenParenthesis))
                return null;

            var exp = Expression(tokenEnumerator) ?? throw new ParseException(); // TODO:

            if (!tokenEnumerator.Symbol(Symbols.CloseParenthesis))
                throw new ParseException(); // TODO:

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
            if (!tokenEnumerator.Symbol(Symbols.OpenParenthesis))
                return null;

            var parameterList = new List<IExpression>();

            var exp = Expression(tokenEnumerator);
            if (exp != null)
            {
                parameterList.Add(exp);

                while (tokenEnumerator.Symbol(Symbols.Comma))
                {
                    exp = Expression(tokenEnumerator) ??
                          throw new ParseException(Resource.NotEnoughParams);

                    parameterList.Add(exp);
                }
            }

            if (!tokenEnumerator.Symbol(Symbols.CloseParenthesis))
                throw new ParseException(); // TODO:

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

            var magnitude = tokenEnumerator.GetCurrent<NumberToken>();
            if (magnitude != null)
            {
                var hasAngleSymbol = tokenEnumerator.Symbol(Symbols.Angle);
                if (hasAngleSymbol)
                {
                    var phase = tokenEnumerator.GetCurrent<NumberToken>();
                    if (phase == null)
                        throw new ParseException(); // TODO:

                    var hasDegreeSymbol = tokenEnumerator.Symbol(Symbols.Degree);
                    if (!hasDegreeSymbol)
                        throw new ParseException(); // TODO:

                    return CreateComplexNumber(magnitude, phase);
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
            var boolean = tokenEnumerator.Keyword(Keywords.True | Keywords.False);
            if (boolean == null)
                return null;

            return CreateFromKeyword(boolean);
        }

        private IExpression Vector(TokenEnumerator tokenEnumerator)
        {
            if (!tokenEnumerator.Symbol(Symbols.OpenBrace))
                return null;

            var parameterList = new List<IExpression>();

            var exp = Expression(tokenEnumerator);
            if (exp != null)
            {
                parameterList.Add(exp);

                while (tokenEnumerator.Symbol(Symbols.Comma))
                {
                    exp = Expression(tokenEnumerator) ?? throw new ParseException(); // TODO:

                    parameterList.Add(exp);
                }
            }

            if (!tokenEnumerator.Symbol(Symbols.CloseBrace))
                throw new ParseException(); // TODO:

            return CreateVector(parameterList.ToArray());
        }

        private IExpression Matrix(TokenEnumerator tokenEnumerator)
        {
            var scope = tokenEnumerator.CreateScope();

            if (tokenEnumerator.Symbol(Symbols.OpenBrace))
            {
                var parameterList = new List<IExpression>();

                var exp = Vector(tokenEnumerator);
                if (exp != null)
                {
                    parameterList.Add(exp);

                    while (tokenEnumerator.Symbol(Symbols.Comma))
                    {
                        exp = Vector(tokenEnumerator) ?? throw new ParseException(); // TODO:

                        parameterList.Add(exp);
                    }

                    if (!tokenEnumerator.Symbol(Symbols.CloseBrace))
                        throw new ParseException(); // TODO:

                    return CreateMatrix(parameterList.ToArray());
                }
            }

            tokenEnumerator.Rollback(scope);

            return null;
        }
    }
}