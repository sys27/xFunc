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
    /// <seealso cref="xFunc.Maths.Tokenization.Factories.FactoryBase" />
    public class SymbolTokenFactory : ITokenFactory
    {

        private readonly Dictionary<char, SymbolToken> map;

        /// <summary>
        /// Initializes a new instance of the <see cref="SymbolTokenFactory"/> class.
        /// </summary>
        public SymbolTokenFactory()
        {
            map = new Dictionary<char, SymbolToken>
            {
                { '(', new SymbolToken(Symbols.OpenParenthesis) },
                { ')', new SymbolToken(Symbols.CloseParenthesis) },
                { '{', new SymbolToken(Symbols.OpenBrace) },
                { '}', new SymbolToken(Symbols.CloseBrace) },
                { ',', new SymbolToken(Symbols.Comma) },
            };
        }

        /// <summary>
        /// Creates the token.
        /// </summary>
        /// <param name="function">The string to scan for tokens.</param>
        /// <param name="startIndex">The start index.</param>
        /// <returns>The token.</returns>
        public FactoryResult CreateToken(string function, int startIndex)
        {
            if (!map.TryGetValue(function[startIndex], out var symbol))
                return null;

            return new FactoryResult(symbol, 1);
        }

    }

}