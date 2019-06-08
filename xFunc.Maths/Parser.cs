// Copyright 2012-2019 Dmitry Kischenko
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

            var rpn = ConvertToReversePolishNotation(tokensArray);

            var arguments = new LinkedList<IExpression>();
            foreach (var token in rpn)
            {
                var expression = ExpressionFactory.Create(token, arguments);

                if (expression is IFunctionExpression functionExpression)
                    for (var i = 0; i < functionExpression.ParametersCount; i++)
                        arguments.RemoveLast();

                arguments.AddLast(expression);
            }

            if (arguments.Count > 1)
                throw new ParseException(Resource.MoreParams);

            return arguments.Last.Value;
        }

        private IEnumerable<IToken> ConvertToReversePolishNotation(IToken[] tokens)
        {
            var output = new List<IToken>(tokens.Length);
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
