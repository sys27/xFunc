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
                { "+", OperatorToken.Plus },
                { "-", OperatorToken.Minus },
                { "−", OperatorToken.Minus },
                { "*", OperatorToken.Multiplication },
                { "×", OperatorToken.Multiplication },
                { "/", OperatorToken.Division },

                { "^", OperatorToken.Exponentiation },
                { "!", OperatorToken.Factorial },
                { "%", OperatorToken.Modulo },

                { ":=", OperatorToken.Assign },
                { "+=", OperatorToken.AddAssign },
                { "-=", OperatorToken.SubAssign },
                { "−=", OperatorToken.SubAssign },
                { "*=", OperatorToken.MulAssign },
                { "×=", OperatorToken.MulAssign },
                { "/=", OperatorToken.DivAssign },

                { "&&", OperatorToken.ConditionalAnd },
                { "||", OperatorToken.ConditionalOr },

                { "==", OperatorToken.Equal },
                { "!=", OperatorToken.NotEqual },
                { "<=", OperatorToken.LessOrEqual },
                { "<", OperatorToken.LessThan },
                { ">=", OperatorToken.GreaterOrEqual },
                { ">", OperatorToken.GreaterThan },

                { "++", OperatorToken.Increment },
                { "--", OperatorToken.Decrement },
                { "−−", OperatorToken.Decrement },

                { "~", OperatorToken.Not },
                { "&", OperatorToken.And },
                { "|", OperatorToken.Or },
                { "->", OperatorToken.Implication },
                { "−>", OperatorToken.Implication },
                { "=>", OperatorToken.Implication },
                { "<->", OperatorToken.Equality },
                { "<−>", OperatorToken.Equality },
                { "<=>", OperatorToken.Equality },
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