// Copyright 2012-2018 Dmitry Kischenko
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
using xFunc.Maths.Expressions;
using xFunc.Maths.Resources;
using xFunc.Maths.Tokenization.Tokens;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using System.Linq;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Analyzers;

namespace xFunc.Maths
{

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

            var tokenList = tokens.ToList();
            if (!tokenList.Any())
                throw new ArgumentException(nameof(tokens));

            var rpn = ConvertToReversePolishNotation(tokenList);
            var expressions = ConvertTokensToExpressions(rpn);

            var stack = new Stack<IExpression>();
            foreach (var expression in expressions)
            {
                if (expression is Number || expression is ComplexNumber || expression is Bool || expression is Variable)
                {
                    stack.Push(expression);
                }
                else if (expression is BinaryExpression binExp)
                {
                    binExp.Right = stack.Pop();
                    binExp.Left = stack.Pop();

                    stack.Push(binExp);
                }
                else if (expression is UnaryExpression unaryMathExp)
                {
                    unaryMathExp.Argument = stack.Pop();

                    stack.Push(unaryMathExp);
                }
                else if (expression is DifferentParametersExpression func)
                {
                    var arg = new IExpression[func.ParametersCount];
                    for (var i = func.ParametersCount - 1; i >= 0; i--)
                        arg[i] = stack.Pop();

                    func.Arguments = arg;

                    stack.Push(func);
                }
                else if (expression is Define assign)
                {
                    if (stack.Count < 2)
                        throw new ParserException(Resource.InvalidNumberOfVariables);

                    assign.Value = stack.Pop();
                    assign.Key = stack.Pop();

                    stack.Push(assign);
                }
                else if (expression is Undefine undef)
                {
                    undef.Key = stack.Pop();

                    stack.Push(undef);
                }
                else
                {
                    throw new ParserException(Resource.UnexpectedError);
                }
            }

            if (stack.Count > 1)
                throw new ParserException(Resource.ErrorWhileParsingTree);

            return stack.Pop();
        }

        private IEnumerable<IExpression> ConvertTokensToExpressions(IEnumerable<IToken> tokens)
        {
            var preOutput = new List<IExpression>();

            foreach (var token in tokens)
            {
                var exp = ExpressionFactory.Create(token);
                if (exp == null)
                    throw new ParserException(Resource.ErrorWhileParsingTree);

                if (token is FunctionToken t)
                {
                    if (t.CountOfParams < exp.MinParameters)
                        throw new ParserException(Resource.LessParams);
                    if (exp.MaxParameters != -1 && t.CountOfParams > exp.MaxParameters)
                        throw new ParserException(Resource.MoreParams);
                }

                preOutput.Add(exp);
            }

            return preOutput;
        }

        private IEnumerable<IToken> ConvertToReversePolishNotation(IEnumerable<IToken> tokens)
        {
            var output = new List<IToken>();
            var stack = new Stack<IToken>();

            var openBracketToken = new SymbolToken(Symbols.OpenBracket);
            var openBraceToken = new SymbolToken(Symbols.OpenBrace);
            foreach (var token in tokens)
            {
                IToken stackToken;
                if (token is SymbolToken t)
                {
                    switch (t.Symbol)
                    {
                        case Symbols.OpenBracket:
                        case Symbols.OpenBrace:
                            stack.Push(token);
                            break;
                        case Symbols.CloseBracket:
                        case Symbols.CloseBrace:
                            stackToken = stack.Pop();
                            while (!stackToken.Equals(openBracketToken) && !stackToken.Equals(openBraceToken))
                            {
                                output.Add(stackToken);
                                stackToken = stack.Pop();
                            }
                            break;
                        case Symbols.Comma:
                            stackToken = stack.Pop();

                            while (!stackToken.Equals(openBracketToken) && !stackToken.Equals(openBraceToken))
                            {
                                output.Add(stackToken);
                                stackToken = stack.Pop();
                            }

                            stack.Push(stackToken);
                            break;
                    }
                }
                else
                {
                    while (stack.Count != 0 && (stackToken = stack.Peek()).Priority >= token.Priority)
                    {
                        if (stackToken.Equals(openBracketToken) || stackToken.Equals(openBraceToken))
                            break;
                        output.Add(stack.Pop());
                    }

                    stack.Push(token);
                }
            }

            if (stack.Count != 0)
                output.AddRange(stack);

            return output;
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
