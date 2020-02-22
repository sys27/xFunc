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

namespace xFunc.Maths.Tokenization
{
    public partial class Lexer
    {
        private IToken CreateNumberToken(ref ReadOnlyMemory<char> function)
        {
            return CreateBinToken(ref function) ??
                   CreateHexToken(ref function) ??
                   CreateOctToken(ref function) ??
                   CreateToken(ref function);
        }

        private IToken CreateBinToken(ref ReadOnlyMemory<char> function)
        {
            var span = function.Span;

            const int prefixLength = 2;

            if (span.Length > prefixLength && CheckBinPrefix(span))
            {
                var numberEnd = prefixLength;
                while (numberEnd < function.Length && IsBinaryNumber(span[numberEnd]))
                    numberEnd++;

                if (numberEnd > prefixLength)
                {
                    var numberString = span[prefixLength..numberEnd];
                    var token = new NumberToken(ParseNumbers.ToInt64(numberString, 2));

                    function = function[numberEnd..];

                    return token;
                }
            }

            return null;
        }

        private IToken CreateHexToken(ref ReadOnlyMemory<char> function)
        {
            var span = function.Span;

            const int prefixLength = 2;

            if (span.Length > prefixLength && CheckHexPrefix(span))
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

        private IToken CreateOctToken(ref ReadOnlyMemory<char> function)
        {
            var span = function.Span;

            const int prefixLength = 1;

            if (span.Length > prefixLength && span[0] == '0')
            {
                var numberEnd = prefixLength;
                while (numberEnd < span.Length && IsOctNumber(span[numberEnd]))
                    numberEnd++;

                if (numberEnd > prefixLength)
                {
                    var numberString = span[prefixLength..numberEnd];
                    var token = new NumberToken(ParseNumbers.ToInt64(numberString, 8));

                    function = function[numberEnd..];

                    return token;
                }
            }

            return null;
        }

        private IToken CreateToken(ref ReadOnlyMemory<char> function)
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
        private static bool CheckBinPrefix(in ReadOnlySpan<char> span)
            => span[0] == '0' && (span[1] == 'b' || span[1] == 'B');

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool CheckHexPrefix(in ReadOnlySpan<char> span)
            => span[0] == '0' && (span[1] == 'x' || span[1] == 'X');

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsBinaryNumber(char symbol)
            => symbol == '0' || symbol == '1';

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsHexNumber(char symbol)
            => IsInRange(symbol, '0', '9') ||
               IsInRange(symbol, 'a', 'f') ||
               IsInRange(symbol, 'A', 'F');

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsOctNumber(char symbol)
            => IsInRange(symbol, '0', '7');

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsInRange(char symbol, char min, char max)
            => (uint) (symbol - min) <= (uint) (max - min);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool CheckNextSymbol(in ReadOnlySpan<char> function, ref int index, char symbol)
        {
            var result = index < function.Length && function[index] == symbol;
            if (result)
                index++;

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ReadDigits(in ReadOnlySpan<char> function, ref int index)
        {
            while (index < function.Length && char.IsDigit(function[index]))
                index++;
        }
    }
}