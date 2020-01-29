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

        /// <summary>
        /// Initializes a new instance of the <see cref="Parser"/> class with default implementations of <see cref="IExpressionFactory"/>.
        /// </summary>
        public Parser() : this(new ExpressionFactory(new Differentiator(), new Simplifier())) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Parser" /> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public Parser(IExpressionFactory factory)
        {
            ExpressionFactory = factory;
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
                var @operator = tokenEnumerator.Operator(Operations.Increment |
                                                         Operations.Decrement);
                if (@operator != null)
                    return ExpressionFactory.CreateOperation(@operator, left);
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

            var @operator = tokenEnumerator.Operator(Operations.MulAssign |
                                                     Operations.DivAssign |
                                                     Operations.AddAssign |
                                                     Operations.SubAssign);
            if (@operator != null)
            {
                var right = Expression(tokenEnumerator) ?? throw new ParseException(); // TODO:

                return ExpressionFactory.CreateOperation(@operator, left, right);
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

            var @operator = tokenEnumerator.Operator(Operations.Assign);
            if (@operator != null)
            {
                var right = Expression(tokenEnumerator) ?? throw new ParseException(); // TODO:

                return ExpressionFactory.CreateOperation(@operator, left, right);
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

            return ExpressionFactory.CreateFromKeyword(def, key, value);
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

            return ExpressionFactory.CreateFromKeyword(undef, key);
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

            return ExpressionFactory.CreateFromKeyword(@if, condition, then, @else);
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

            return ExpressionFactory.CreateFromKeyword(@for, body, init, condition, iter);
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

            return ExpressionFactory.CreateFromKeyword(@while, exp, condition);
        }

        private IExpression FunctionDeclaration(TokenEnumerator tokenEnumerator)
        {
            var scope = tokenEnumerator.CreateScope();

            var id = tokenEnumerator.GetCurrent<IdToken>(); // TODO: create new method to auto move next
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
                    return ExpressionFactory.CreateFunction(id, parameterList.ToArray());
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
                var @operator = tokenEnumerator.Operator(Operations.ConditionalAnd |
                                                         Operations.ConditionalOr);
                if (@operator == null)
                    return left;

                var right = BitwiseOperator(tokenEnumerator) ?? throw new ParseException(); // TODO:

                left = ExpressionFactory.CreateOperation(@operator, left, right);
            }
        }

        private IExpression BitwiseOperator(TokenEnumerator tokenEnumerator)
        {
            var left = EqualityOperator(tokenEnumerator);
            if (left == null)
                return null;

            while (true)
            {
                var @operator = tokenEnumerator.Operator(Operations.And |
                                                         Operations.Or |
                                                         Operations.XOr |
                                                         Operations.Implication |
                                                         Operations.Equality |
                                                         Operations.NOr |
                                                         Operations.NAnd);
                if (@operator == null)
                    return left;

                var right = EqualityOperator(tokenEnumerator) ?? throw new ParseException(); // TODO:

                left = ExpressionFactory.CreateOperation(@operator, left, right);
            }
        }

        private IExpression EqualityOperator(TokenEnumerator tokenEnumerator)
        {
            var left = AddSub(tokenEnumerator);
            if (left == null)
                return null;

            while (true)
            {
                var @operator = tokenEnumerator.Operator(Operations.Equal |
                                                         Operations.NotEqual |
                                                         Operations.LessThan |
                                                         Operations.LessOrEqual |
                                                         Operations.GreaterThan |
                                                         Operations.GreaterOrEqual);
                if (@operator == null)
                    return left;

                var right = AddSub(tokenEnumerator) ?? throw new ParseException(); // TODO:

                left = ExpressionFactory.CreateOperation(@operator, left, right);
            }
        }

        private IExpression AddSub(TokenEnumerator tokenEnumerator)
        {
            var left = MulDivMod(tokenEnumerator);
            if (left == null)
                return null;

            while (true)
            {
                var @operator = tokenEnumerator.Operator(Operations.Addition |
                                                         Operations.Subtraction);
                if (@operator == null)
                    return left;

                var right = MulDivMod(tokenEnumerator) ?? throw new ParseException(); // TODO:

                left = ExpressionFactory.CreateOperation(@operator, left, right);
            }
        }

        private IExpression MulDivMod(TokenEnumerator tokenEnumerator)
        {
            var left = MulImplicit(tokenEnumerator);
            if (left == null)
                return null;

            while (true)
            {
                var @operator = tokenEnumerator.Operator(Operations.Multiplication |
                                                         Operations.Division |
                                                         Operations.Modulo);
                if (@operator == null)
                    return left;

                var right = MulImplicit(tokenEnumerator) ?? throw new ParseException(); // TODO:

                left = ExpressionFactory.CreateOperation(@operator, left, right);
            }
        }

        private IExpression MulImplicit(TokenEnumerator tokenEnumerator)
        {
            return MulImplicitLeftUnary(tokenEnumerator) ??
                LeftUnary(tokenEnumerator);
        }

        private IExpression MulImplicitLeftUnary(TokenEnumerator tokenEnumerator)
        {
            var scope = tokenEnumerator.CreateScope(); // TODO:

            var @operator = tokenEnumerator.Operator(Operations.UnaryMinus);
            var number = Number(tokenEnumerator);
            if (number != null)
            {
                var rightUnary = RightUnary(tokenEnumerator); // TODO: operand?
                if (rightUnary != null)
                {
                    if (@operator != null)
                        number = ExpressionFactory.CreateOperation(@operator, number);

                    // TODO:
                    return ExpressionFactory.CreateOperation(new OperationToken(Operations.Multiplication), number, rightUnary);
                }
            }

            tokenEnumerator.Rollback(scope);
            return null;
        }

        private IExpression LeftUnary(TokenEnumerator tokenEnumerator)
        {
            var @operator = tokenEnumerator.Operator(Operations.Not |
                                                     Operations.UnaryMinus |
                                                     Operations.Addition);
            var operand = Exponentiation(tokenEnumerator);
            if (@operator == null || @operator.Operation == Operations.Addition)
                return operand;

            return ExpressionFactory.CreateOperation(@operator, operand);
        }

        private IExpression Exponentiation(TokenEnumerator tokenEnumerator)
        {
            var left = RightUnary(tokenEnumerator);
            if (left == null)
                return null;

            var @operator = tokenEnumerator.Operator(Operations.Exponentiation);
            if (@operator == null)
                return left;

            var right = Exponentiation(tokenEnumerator) ?? throw new ParseException(); // TODO:

            return ExpressionFactory.CreateOperation(@operator, left, right);
        }

        private IExpression RightUnary(TokenEnumerator tokenEnumerator)
        {
            var scope = tokenEnumerator.CreateScope();

            var number = Number(tokenEnumerator);
            if (number != null)
            {
                var @operator = tokenEnumerator.Operator(Operations.Factorial);
                if (@operator != null)
                    return ExpressionFactory.CreateOperation(@operator, number);

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
                return ExpressionFactory.CreateVariable(function);

            return ExpressionFactory.CreateFunction(function, parameterList.ToArray());
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

            return ExpressionFactory.CreateNumber(number);
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

                    // TODO:
                    return ExpressionFactory.CreateComplexNumber(magnitude, phase);
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

            return ExpressionFactory.CreateVariable(variable);
        }

        private IExpression Boolean(TokenEnumerator tokenEnumerator)
        {
            var boolean = tokenEnumerator.Keyword(Keywords.True) ??
                          tokenEnumerator.Keyword(Keywords.False);
            if (boolean == null)
                return null;

            return ExpressionFactory.CreateFromKeyword(boolean);
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

            return ExpressionFactory.CreateVector(parameterList.ToArray());
        }

        private IExpression Matrix(TokenEnumerator tokenEnumerator)
        {
            var scope = tokenEnumerator.CreateScope();

            // TODO:
            if (!tokenEnumerator.Symbol(Symbols.OpenBrace))
            {
                tokenEnumerator.Rollback(scope);
                return null;
            }

            var parameterList = new List<IExpression>();

            var exp = Vector(tokenEnumerator);
            if (exp == null)
            {
                tokenEnumerator.Rollback(scope);
                return null;
            }

            parameterList.Add(exp);

            while (tokenEnumerator.Symbol(Symbols.Comma))
            {
                exp = Vector(tokenEnumerator) ?? throw new ParseException(); // TODO:

                parameterList.Add(exp);
            }

            if (!tokenEnumerator.Symbol(Symbols.CloseBrace))
                throw new ParseException(); // TODO:

            return ExpressionFactory.CreateMatrix(parameterList.ToArray());
        }

        /// <summary>
        /// Gets or sets the expression factory.
        /// </summary>
        /// <value>
        /// The expression factory.
        /// </value>
        public IExpressionFactory ExpressionFactory { get; set; }

    }

}