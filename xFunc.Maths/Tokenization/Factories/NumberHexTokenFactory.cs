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
using System.Runtime.CompilerServices;
using xFunc.Maths.Tokenization.Tokens;

namespace xFunc.Maths.Tokenization.Factories
{
    internal class NumberHexTokenFactory : ITokenFactory
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

            const int prefixLength = 2;

            if (span.Length > prefixLength && CheckPrefix(span))
            {
                var numberEnd = prefixLength;
                while (numberEnd < span.Length && IsHexNumber(span[numberEnd]))
                    numberEnd++;

                if (numberEnd > prefixLength)
                {
                    var numberString = span[prefixLength..numberEnd];
                    var token = new NumberToken(ParseNumbers.ToInt64(numberString, 16));

                    function = function[numberEnd..];

                    return token;
                }
            }

            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool CheckPrefix(in ReadOnlySpan<char> span)
            => span[0] == '0' && (span[1] == 'x' || span[1] == 'X');

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsHexNumber(char symbol)
            => IsInRange(symbol, '0', '9') ||
               IsInRange(symbol, 'a', 'f') ||
               IsInRange(symbol, 'A', 'F');

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsInRange(char symbol, char min, char max)
            => (uint) (symbol - min) <= (uint) (max - min);
    }
}