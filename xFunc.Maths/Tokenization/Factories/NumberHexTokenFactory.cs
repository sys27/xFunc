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
    /// <summary>
    /// The factory which creates number tokens (from hex format).
    /// </summary>
    /// <seealso cref="xFunc.Maths.Tokenization.Factories.ITokenFactory" />
    internal class NumberHexTokenFactory : ITokenFactory
    {
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
            // TODO: span?
            if (index + 2 < function.Length &&
                function[index] == '0' &&
                function[index + 1] == 'x')
            {
                var numberStart = index + 2;
                var numberEnd = numberStart;
                while (numberEnd < function.Length && IsHexNumber(function[numberEnd]))
                    numberEnd++;

                if (numberEnd > numberStart)
                {
                    var numberString = function.Substring(numberStart, numberEnd - numberStart);
                    var token = new NumberToken(Convert.ToInt64(numberString, 16));

                    index = numberEnd;

                    return token;
                }
            }

            return null;
        }

        private bool IsHexNumber(char symbol)
            => char.IsDigit(symbol) || (symbol >= 'a' && symbol <= 'f');
    }
}