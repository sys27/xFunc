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

using System.Globalization;
using xFunc.Maths.Tokenization.Tokens;

namespace xFunc.Maths.Tokenization.Factories
{
    /// <summary>
    /// The factory which creates number tokens.
    /// </summary>
    /// <seealso cref="xFunc.Maths.Tokenization.Factories.ITokenFactory" />
    internal class NumberTokenFactory : ITokenFactory
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
            var endIndex = index;
            ReadDigits(function, ref endIndex);

            if (endIndex > index)
            {
                if (CheckNextSymbol(function, ref endIndex, '.'))
                {
                    var dotIndex = endIndex;

                    ReadDigits(function, ref endIndex);

                    if (dotIndex == endIndex)
                        return null;
                }

                if (CheckNextSymbol(function, ref endIndex, 'e'))
                {
                    // TODO:
                    if (CheckNextSymbol(function, ref endIndex, '+') ||
                        CheckNextSymbol(function, ref endIndex, '-'))
                    {
                    }

                    ReadDigits(function, ref endIndex);
                }

                var numberString = function.Substring(index, endIndex - index);
                var number = double.Parse(numberString, CultureInfo.InvariantCulture);

                index = endIndex;

                return new NumberToken(number);
            }

            return null;
        }

        private bool CheckNextSymbol(string function, ref int index, char symbol)
        {
            var result = index < function.Length && function[index] == symbol;
            if (result)
                index++;

            return result;
        }

        private void ReadDigits(string function, ref int index)
        {
            while (index < function.Length && char.IsDigit(function[index]))
                index++;
        }
    }
}