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

using System.Collections.Generic;
using xFunc.Maths.Tokenization.Tokens;

namespace xFunc.Maths.Tokenization.Factories
{
    /// <summary>
    /// The factory which creates @operator tokens.
    /// </summary>
    /// <seealso cref="xFunc.Maths.Tokenization.Factories.ITokenFactory" />
    internal class OperationTokenFactory : ITokenFactory
    {
        private readonly HashSet<char> restrictedSymbols;

        private readonly IDictionary<string, OperatorToken> operators;

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationTokenFactory"/> class.
        /// </summary>
        public OperationTokenFactory()
        {
            restrictedSymbols = new HashSet<char>
            {
                '(', ')', '{', '}', ',', '°', ' ', '\n', '\r', '\t', '\v', '\f'
            };

            // TODO: copies
            operators = new Dictionary<string, OperatorToken>
            {
                { "+", new OperatorToken(Operators.Plus) },
                { "-", new OperatorToken(Operators.Minus) },
                { "−", new OperatorToken(Operators.Minus) },
                { "*", new OperatorToken(Operators.Multiplication) },
                { "×", new OperatorToken(Operators.Multiplication) },
                { "/", new OperatorToken(Operators.Division) },

                { "^", new OperatorToken(Operators.Exponentiation) },
                { "!", new OperatorToken(Operators.Factorial) },
                { "%", new OperatorToken(Operators.Modulo) },

                { ":=", new OperatorToken(Operators.Assign) },
                { "+=", new OperatorToken(Operators.AddAssign) },
                { "-=", new OperatorToken(Operators.SubAssign) },
                { "−=", new OperatorToken(Operators.SubAssign) },
                { "*=", new OperatorToken(Operators.MulAssign) },
                { "×=", new OperatorToken(Operators.MulAssign) },
                { "/=", new OperatorToken(Operators.DivAssign) },

                { "&&", new OperatorToken(Operators.ConditionalAnd) },
                { "||", new OperatorToken(Operators.ConditionalOr) },

                { "==", new OperatorToken(Operators.Equal) },
                { "!=", new OperatorToken(Operators.NotEqual) },
                { "<=", new OperatorToken(Operators.LessOrEqual) },
                { "<", new OperatorToken(Operators.LessThan) },
                { ">=", new OperatorToken(Operators.GreaterOrEqual) },
                { ">", new OperatorToken(Operators.GreaterThan) },

                { "++", new OperatorToken(Operators.Increment) },
                { "--", new OperatorToken(Operators.Decrement) },
                { "−−", new OperatorToken(Operators.Decrement) },

                { "~", new OperatorToken(Operators.Not) },
                { "&", new OperatorToken(Operators.And) },
                { "|", new OperatorToken(Operators.Or) },
                { "->", new OperatorToken(Operators.Implication) },
                { "−>", new OperatorToken(Operators.Implication) },
                { "=>", new OperatorToken(Operators.Implication) },
                { "<->", new OperatorToken(Operators.Equality) },
                { "<−>", new OperatorToken(Operators.Equality) },
                { "<=>", new OperatorToken(Operators.Equality) },
            };
        }

        /// <summary>
        /// Creates the token.
        /// </summary>
        /// <param name="function">The string to scan for tokens.</param>
        /// <param name="index">The start index.</param>
        /// <returns>
        /// The token.
        /// </returns>
        public IToken CreateToken(string function, ref int index)
        {
            var endIndex = index;
            while (endIndex < function.Length && IsOperatorSymbol(function[endIndex]))
                endIndex++;

            if (endIndex > index)
            {
                var length = endIndex - index;
                var operatorMatch = function.Substring(index, length);

                if (operators.TryGetValue(operatorMatch, out var @operator))
                {
                    index = endIndex;

                    return @operator;
                }
            }

            return null;
        }

        private bool IsOperatorSymbol(char symbol) =>
            !char.IsLetterOrDigit(symbol) && !restrictedSymbols.Contains(symbol);
    }
}