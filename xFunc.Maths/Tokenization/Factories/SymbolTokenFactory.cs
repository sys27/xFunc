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
using xFunc.Maths.Tokenization.Tokens;

namespace xFunc.Maths.Tokenization.Factories
{
    internal class SymbolTokenFactory : ITokenFactory
    {
        /// <summary>
        /// Creates the token.
        /// </summary>
        /// <param name="function">The string to scan for tokens.</param>
        /// <returns>The token.</returns>
        public IToken CreateToken(ref ReadOnlyMemory<char> function)
        {
            var symbol = function.Span[0] switch
            {
                '(' => SymbolToken.OpenParenthesis,
                ')' => SymbolToken.CloseParenthesis,
                '{' => SymbolToken.OpenBrace,
                '}' => SymbolToken.CloseBrace,
                ',' => SymbolToken.Comma,
                '∠' => SymbolToken.Angle,
                '°' => SymbolToken.Degree,
                _ => null,
            };

            if (symbol == null)
                return null;

            function = function[1..];

            return symbol;
        }
    }
}