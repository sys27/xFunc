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
    /// The factory which creates symbol tokens.
    /// </summary>
    /// <seealso cref="xFunc.Maths.Tokenization.Factories.ITokenFactory" />
    internal class SymbolTokenFactory : ITokenFactory
    {
        private readonly Dictionary<char, SymbolToken> symbols;

        /// <summary>
        /// Initializes a new instance of the <see cref="SymbolTokenFactory"/> class.
        /// </summary>
        public SymbolTokenFactory()
        {
            // TODO:
            symbols = new Dictionary<char, SymbolToken>
            {
                { '(', SymbolToken.OpenParenthesis },
                { ')', SymbolToken.CloseParenthesis },
                { '{', SymbolToken.OpenBrace },
                { '}', SymbolToken.CloseBrace },
                { ',', SymbolToken.Comma },
                { '∠', SymbolToken.Angle },
                { '°', SymbolToken.Degree },
            };
        }

        /// <summary>
        /// Creates the token.
        /// </summary>
        /// <param name="function">The string to scan for tokens.</param>
        /// <param name="index">The start index.</param>
        /// <returns>The token.</returns>
        public IToken CreateToken(string function, ref int index)
        {
            if (!symbols.TryGetValue(function[index], out var symbol))
                return null;

            index++;

            return symbol;
        }
    }
}