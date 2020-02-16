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
using System.Globalization;
using System.Runtime.CompilerServices;
using xFunc.Maths.Tokenization.Tokens;

namespace xFunc.Maths.Tokenization.Factories
{
    internal class NumberTokenFactory : ITokenFactory
    {
        /// <summary>
        /// Creates the token.
        /// </summary>
        /// <param name="function">The string to scan for tokens.</param>
        /// <returns>
        /// The token.
        /// </returns>
        public IToken CreateToken(ref ReadOnlyMemory<char> function)
        {
            var span = function.Span;

            var endIndex = 0;
            ReadDigits(span, ref endIndex);

            if (endIndex > 0 && span.Length >= endIndex)
            {
                if (CheckNextSymbol(span, ref endIndex, '.'))
                {
                    var dotIndex = endIndex;

                    ReadDigits(span, ref endIndex);

                    if (dotIndex == endIndex)
                        return null;
                }

                if (CheckNextSymbol(span, ref endIndex, 'e') ||
                    CheckNextSymbol(span, ref endIndex, 'E'))
                {
                    var _ = CheckNextSymbol(span, ref endIndex, '+') ||
                            CheckNextSymbol(span, ref endIndex, '-');

                    ReadDigits(span, ref endIndex);
                }

                var numberString = span[..endIndex];
                var number = double.Parse(numberString, provider: CultureInfo.InvariantCulture);
                var token = new NumberToken(number);

                function = function[endIndex..];

                return token;
            }

            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool CheckNextSymbol(in ReadOnlySpan<char> function, ref int index, char symbol)
        {
            var result = index < function.Length && function[index] == symbol;
            if (result)
                index++;

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ReadDigits(in ReadOnlySpan<char> function, ref int index)
        {
            while (index < function.Length && char.IsDigit(function[index]))
                index++;
        }
    }
}