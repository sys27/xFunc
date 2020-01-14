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
    internal class TokenEnumerator
    {
        private readonly IToken[] list;
        private int index;

        internal TokenEnumerator(IToken[] list) : this(list, 0)
        {
        }

        private TokenEnumerator(IToken[] list, int index)
        {
            this.list = list;
            this.index = index;
        }

        public TToken GetCurrent<TToken>() where TToken : class, IToken
        {
            if (index >= list.Length)
                return null;

            return list[index] as TToken;
        }

        public bool MoveNext()
        {
            if (index >= list.Length)
            {
                index = list.Length;

                return false;
            }

            ++index;

            return true;
        }

        public TokenEnumerator CreateScope()
        {
            return new TokenEnumerator(list, index);
        }

        public void SwitchScope(TokenEnumerator tokenEnumerator)
        {
            index = tokenEnumerator.index;
        }

        public bool IsEnd => index >= list.Length;
    }

    /// <summary>
    /// The parser for mathematical expressions.
    /// </summary>
    public class Parser : IParser
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
            var scope = tokenEnumerator.CreateScope(); // TODO:

            var left = Variable(scope);
            if (left == null)
                return null;

            var @operator = Operator(scope, Operations.Increment | Operations.Decrement);
            if (@operator != null)
            {
                tokenEnumerator.SwitchScope(scope);

                return ExpressionFactory.CreateOperation(@operator, left);
            }

            return null;
        }

        private IExpression BinaryAssign(TokenEnumerator tokenEnumerator)
        {
            var scope = tokenEnumerator.CreateScope(); // TODO:

            var left = Variable(scope);
            if (left == null)
                return null;

            var @operator = Operator(scope,
                                     Operations.MulAssign |
                                     Operations.DivAssign |
                                     Operations.AddAssign |
                                     Operations.SubAssign);
            if (@operator != null)
            {
                var right = Expression(scope);
                if (right == null)
                    throw new ParseException(); // TODO:

                tokenEnumerator.SwitchScope(scope);

                return ExpressionFactory.CreateOperation(@operator, left, right);
            }

            return null;
        }

        private IExpression AssignmentKey(TokenEnumerator tokenEnumerator)
        {
            var left = FunctionDeclaration(tokenEnumerator);
            if (left == null)
            {
                left = Variable(tokenEnumerator);
                if (left == null)
                    return null;
            }

            return left;
        }

        private IExpression Assign(TokenEnumerator tokenEnumerator)
        {
            var scope = tokenEnumerator.CreateScope(); // TODO:

            var left = AssignmentKey(scope);
            if (left == null)
                return null;

            var @operator = Operator(scope, Operations.Assign);
            if (@operator != null)
            {
                var right = Expression(scope);
                if (right == null)
                    throw new ParseException(); // TODO:

                tokenEnumerator.SwitchScope(scope);

                return ExpressionFactory.CreateOperation(@operator, left, right);
            }

            return null;
        }

        private IExpression Def(TokenEnumerator tokenEnumerator)
        {
            var def = Keyword(tokenEnumerator, Keywords.Define);
            if (def == null)
                return null;

            if (!Symbol(tokenEnumerator, Symbols.OpenParenthesis))
                throw new ParseException(); // TODO:

            var key = AssignmentKey(tokenEnumerator);
            if (key == null)
                throw new ParseException(); // TODO:

            if (!Symbol(tokenEnumerator, Symbols.Comma))
                throw new ParseException(); // TODO:

            var value = Expression(tokenEnumerator);
            if (value == null)
                throw new ParseException(); // TODO:

            if (!Symbol(tokenEnumerator, Symbols.CloseParenthesis))
                throw new ParseException(); // TODO:

            return ExpressionFactory.CreateFromKeyword(def, key, value);
        }

        private IExpression Undef(TokenEnumerator tokenEnumerator)
        {
            var undef = Keyword(tokenEnumerator, Keywords.Undefine);
            if (undef == null)
                return null;

            if (!Symbol(tokenEnumerator, Symbols.OpenParenthesis))
                throw new ParseException(); // TODO:

            var key = AssignmentKey(tokenEnumerator);
            if (key == null)
                throw new ParseException(); // TODO:

            if (!Symbol(tokenEnumerator, Symbols.CloseParenthesis))
                throw new ParseException(); // TODO:

            return ExpressionFactory.CreateFromKeyword(undef, key);
        }

        private IExpression If(TokenEnumerator tokenEnumerator)
        {
            var @if = Keyword(tokenEnumerator, Keywords.If);
            if (@if == null)
                return null;

            if (!Symbol(tokenEnumerator, Symbols.OpenParenthesis))
                throw new ParseException(); // TODO:

            // TODO:
            var condition = ConditionalOperator(tokenEnumerator);
            if (condition == null)
                throw new ParseException(); // TODO:

            if (!Symbol(tokenEnumerator, Symbols.Comma))
                throw new ParseException(); // TODO:

            var then = Expression(tokenEnumerator);
            if (then == null)
                throw new ParseException(); // TODO:

            IExpression @else = null;
            if (Symbol(tokenEnumerator, Symbols.Comma))
            {
                @else = Expression(tokenEnumerator);
                if (@else == null)
                    throw new ParseException(); // TODO:
            }

            if (!Symbol(tokenEnumerator, Symbols.CloseParenthesis))
                throw new ParseException(); // TODO:

            return ExpressionFactory.CreateFromKeyword(@if, condition, then, @else);
        }

        private IExpression For(TokenEnumerator tokenEnumerator)
        {
            var @for = Keyword(tokenEnumerator, Keywords.For);
            if (@for == null)
                return null;

            if (!Symbol(tokenEnumerator, Symbols.OpenParenthesis))
                throw new ParseException(); // TODO:

            // TODO:
            var body = Statement(tokenEnumerator);
            if (body == null)
                throw new ParseException(); // TODO:

            if (!Symbol(tokenEnumerator, Symbols.Comma))
                throw new ParseException(); // TODO:

            var init = Statement(tokenEnumerator);
            if (init == null)
                throw new ParseException(); // TODO:

            if (!Symbol(tokenEnumerator, Symbols.Comma))
                throw new ParseException(); // TODO:

            var condition = ConditionalOperator(tokenEnumerator);
            if (condition == null)
                throw new ParseException(); // TODO:

            if (!Symbol(tokenEnumerator, Symbols.Comma))
                throw new ParseException(); // TODO:

            var iter = Statement(tokenEnumerator);
            if (iter == null)
                throw new ParseException(); // TODO:

            if (!Symbol(tokenEnumerator, Symbols.CloseParenthesis))
                throw new ParseException(); // TODO:

            return ExpressionFactory.CreateFromKeyword(@for, body, init, condition, iter);
        }

        private IExpression While(TokenEnumerator tokenEnumerator)
        {
            var @while = Keyword(tokenEnumerator, Keywords.While);
            if (@while == null)
                return null;

            if (!Symbol(tokenEnumerator, Symbols.OpenParenthesis))
                throw new ParseException(); // TODO:

            // TODO:
            var exp = Statement(tokenEnumerator);
            if (exp == null)
                throw new ParseException(); // TODO:

            if (!Symbol(tokenEnumerator, Symbols.Comma))
                throw new ParseException(); // TODO:

            var condition = ConditionalOperator(tokenEnumerator);
            if (condition == null)
                throw new ParseException(); // TODO:

            if (!Symbol(tokenEnumerator, Symbols.CloseParenthesis))
                throw new ParseException(); // TODO:

            return ExpressionFactory.CreateFromKeyword(@while, exp, condition);
        }

        private IExpression FunctionDeclaration(TokenEnumerator tokenEnumerator)
        {
            var scope = tokenEnumerator.CreateScope(); // TODO:

            var id = scope.GetCurrent<IdToken>();
            if (id == null)
                return null;

            scope.MoveNext();
            if (!Symbol(scope, Symbols.OpenParenthesis))
                return null;

            var parameterList = new List<IExpression>();

            var exp = Variable(scope);
            if (exp != null)
            {
                parameterList.Add(exp);

                while (Symbol(scope, Symbols.Comma))
                {
                    exp = Variable(scope);
                    if (exp == null)
                        return null;

                    parameterList.Add(exp);
                }
            }

            if (!Symbol(scope, Symbols.CloseParenthesis))
                return null;

            tokenEnumerator.SwitchScope(scope);

            return ExpressionFactory.CreateFunction(id, parameterList.ToArray());
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
                var @operator = Operator(tokenEnumerator, Operations.ConditionalAnd | Operations.ConditionalOr);
                if (@operator == null)
                    return left;

                var right = BitwiseOperator(tokenEnumerator);
                if (right == null)
                    throw new ParseException(); // TODO:

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
                var @operator = Operator(tokenEnumerator,
                                        Operations.And |
                                        Operations.Or |
                                        Operations.XOr |
                                        Operations.Implication |
                                        Operations.Equality |
                                        Operations.NOr |
                                        Operations.NAnd);
                if (@operator == null)
                    return left;

                var right = EqualityOperator(tokenEnumerator);
                if (right == null)
                    throw new ParseException(); // TODO:

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
                var @operator = Operator(tokenEnumerator,
                                         Operations.Equal |
                                         Operations.NotEqual |
                                         Operations.LessThan |
                                         Operations.LessOrEqual |
                                         Operations.GreaterThan |
                                         Operations.GreaterOrEqual);
                if (@operator == null)
                    return left;

                var right = AddSub(tokenEnumerator);
                if (right == null)
                    throw new ParseException(); // TODO:

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
                var @operator = Operator(tokenEnumerator, Operations.Addition | Operations.Subtraction);
                if (@operator == null)
                    return left;

                var right = MulDivMod(tokenEnumerator);
                if (right == null)
                    throw new ParseException(); // TODO:

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
                var @operator = Operator(tokenEnumerator,
                                        Operations.Multiplication |
                                        Operations.Division |
                                        Operations.Modulo);
                if (@operator == null)
                    return left;

                var right = MulImplicit(tokenEnumerator);
                if (right == null)
                    throw new ParseException(); // TODO:

                left = ExpressionFactory.CreateOperation(@operator, left, right);
            }
        }

        private IExpression MulImplicit(TokenEnumerator tokenEnumerator)
        {
            var implicitMul = MulImplicitLeftUnary(tokenEnumerator);
            if (implicitMul != null)
                return implicitMul;

            return LeftUnary(tokenEnumerator);
        }

        private IExpression MulImplicitLeftUnary(TokenEnumerator tokenEnumerator)
        {
            var scope = tokenEnumerator.CreateScope(); // TODO:

            var @operator = Operator(scope, Operations.UnaryMinus);
            var number = Number(scope);
            if (number == null)
                return null;

            var rightUnary = RightUnary(scope); // TODO: operand?
            if (rightUnary == null)
                return null;

            tokenEnumerator.SwitchScope(scope);

            if (@operator != null)
                number = ExpressionFactory.CreateOperation(@operator, number);

            return ExpressionFactory.CreateOperation(new OperationToken(Operations.Multiplication), number, rightUnary);
        }

        private IExpression LeftUnary(TokenEnumerator tokenEnumerator)
        {
            var @operator = Operator(tokenEnumerator,
                                     Operations.Not |
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

            var @operator = Operator(tokenEnumerator, Operations.Exponentiation);
            if (@operator == null)
                return left;

            var right = Exponentiation(tokenEnumerator);
            if (right == null)
                throw new ParseException(); // TODO:

            return ExpressionFactory.CreateOperation(@operator, left, right);
        }

        private IExpression RightUnary(TokenEnumerator tokenEnumerator)
        {
            var number = Number(tokenEnumerator);
            if (number == null)
                return Operand(tokenEnumerator);

            var @operator = Operator(tokenEnumerator, Operations.Factorial);
            if (@operator == null)
                return number;

            return ExpressionFactory.CreateOperation(@operator, number);
        }

        private IExpression Operand(TokenEnumerator tokenEnumerator)
        {
            return Number(tokenEnumerator) ??
                Function(tokenEnumerator) ??
                Variable(tokenEnumerator) ??
                Boolean(tokenEnumerator) ??
                ComplexNumber(tokenEnumerator) ??
                ParenthesesExpression(tokenEnumerator) ??
                Matrix(tokenEnumerator) ??
                Vector(tokenEnumerator);
        }

        private IExpression ParenthesesExpression(TokenEnumerator tokenEnumerator)
        {
            if (!Symbol(tokenEnumerator, Symbols.OpenParenthesis))
                return null;

            var exp = Expression(tokenEnumerator);
            if (exp == null)
                throw new ParseException(); // TODO:

            if (!Symbol(tokenEnumerator, Symbols.CloseParenthesis))
                throw new ParseException(); // TODO:

            return exp;
        }

        private IExpression Function(TokenEnumerator tokenEnumerator)
        {
            var function = tokenEnumerator.GetCurrent<IdToken>();
            if (function == null)
                return null;

            tokenEnumerator.MoveNext();

            var parameterList = ParameterList(tokenEnumerator);
            if (parameterList == null)
                return ExpressionFactory.CreateVariable(function);

            return ExpressionFactory.CreateFunction(function, parameterList.ToArray());
        }

        private IEnumerable<IExpression> ParameterList(TokenEnumerator tokenEnumerator)
        {
            if (!Symbol(tokenEnumerator, Symbols.OpenParenthesis))
                return null;

            var parameterList = new List<IExpression>();

            var exp = Statement(tokenEnumerator); // TODO: Expression(tokenEnumerator);
            if (exp != null)
            {
                parameterList.Add(exp);

                while (Symbol(tokenEnumerator, Symbols.Comma))
                {
                    exp = Statement(tokenEnumerator); // TODO: Expression(tokenEnumerator);
                    if (exp == null)
                        throw new ParseException(Resource.NotEnoughParams);

                    parameterList.Add(exp);
                }
            }

            if (!Symbol(tokenEnumerator, Symbols.CloseParenthesis))
                throw new ParseException(); // TODO:

            return parameterList;
        }

        private IExpression Number(TokenEnumerator tokenEnumerator)
        {
            var number = tokenEnumerator.GetCurrent<NumberToken>();
            if (number == null)
                return null;

            var exp = ExpressionFactory.CreateNumber(number);
            tokenEnumerator.MoveNext();

            return exp;
        }

        private IExpression ComplexNumber(TokenEnumerator tokenEnumerator)
        {
            var complexNumber = tokenEnumerator.GetCurrent<ComplexNumberToken>();
            if (complexNumber == null)
                return null;

            var exp = ExpressionFactory.CreateComplexNumber(complexNumber);
            tokenEnumerator.MoveNext();

            return exp;
        }

        private IExpression Variable(TokenEnumerator tokenEnumerator)
        {
            var variable = tokenEnumerator.GetCurrent<IdToken>();
            if (variable == null)
                return null;

            var exp = ExpressionFactory.CreateVariable(variable);
            tokenEnumerator.MoveNext();

            return exp;
        }

        private IExpression Boolean(TokenEnumerator tokenEnumerator)
        {
            var boolean = Keyword(tokenEnumerator, Keywords.True) ??
                          Keyword(tokenEnumerator, Keywords.False);
            if (boolean == null)
                return null;

            return ExpressionFactory.CreateFromKeyword(boolean);
        }

        private IExpression Vector(TokenEnumerator tokenEnumerator)
        {
            if (!Symbol(tokenEnumerator, Symbols.OpenBrace))
                return null;

            var parameterList = new List<IExpression>();

            var exp = Expression(tokenEnumerator);
            if (exp != null)
            {
                parameterList.Add(exp);

                while (Symbol(tokenEnumerator, Symbols.Comma))
                {
                    exp = Expression(tokenEnumerator);
                    if (exp == null)
                        throw new ParseException(); // TODO:

                    parameterList.Add(exp);
                }
            }

            if (!Symbol(tokenEnumerator, Symbols.CloseBrace))
                throw new ParseException(); // TODO:

            return ExpressionFactory.CreateVector(parameterList.ToArray());
        }

        private IExpression Matrix(TokenEnumerator tokenEnumerator)
        {
            var scope = tokenEnumerator.CreateScope();

            if (!Symbol(scope, Symbols.OpenBrace) ||
                !Symbol(scope, Symbols.OpenBrace))
                return null;

            tokenEnumerator.MoveNext();

            var parameterList = new List<IExpression>();

            var exp = Vector(tokenEnumerator);
            if (exp != null)
            {
                parameterList.Add(exp);

                while (Symbol(tokenEnumerator, Symbols.Comma))
                {
                    exp = Vector(tokenEnumerator);
                    if (exp == null)
                        throw new ParseException(); // TODO:

                    parameterList.Add(exp);
                }
            }

            if (!Symbol(tokenEnumerator, Symbols.CloseBrace))
                throw new ParseException(); // TODO:

            return ExpressionFactory.CreateMatrix(parameterList.ToArray());
        }

        private bool Symbol(TokenEnumerator tokenEnumerator, Symbols symbol)
        {
            var result = tokenEnumerator.GetCurrent<SymbolToken>()?.Symbol == symbol;
            if (result)
                tokenEnumerator.MoveNext();

            return result;
        }

        private OperationToken Operator(TokenEnumerator tokenEnumerator, Operations operations)
        {
            var token = tokenEnumerator.GetCurrent<OperationToken>();
            if (token != null && operations.HasFlag(token.Operation))
            {
                tokenEnumerator.MoveNext();

                return token;
            }

            return null;
        }

        private KeywordToken Keyword(TokenEnumerator tokenEnumerator, Keywords keyword)
        {
            var token = tokenEnumerator.GetCurrent<KeywordToken>();
            var result = token?.Keyword == keyword;
            if (!result)
                return null;

            tokenEnumerator.MoveNext();

            return token;
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