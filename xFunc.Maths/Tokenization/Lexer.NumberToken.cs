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

namespace xFunc.Maths.Tokenization
{
    /// <summary>
    /// The lexer for mathematical expressions.
    /// </summary>
    internal ref partial struct Lexer
    {
        private bool CreateNumberToken()
            => CreateBinToken() ||
               CreateHexToken() ||
               CreateOctToken() ||
               CreateDecToken();

        private bool CreateBinToken()
        {
            const int prefixLength = 2;

            if (function.Length > prefixLength && CheckBinPrefix(function))
            {
                var numberEnd = prefixLength;
                while (numberEnd < function.Length && IsBinaryNumber(function[numberEnd]))
                    numberEnd++;

                if (numberEnd > prefixLength)
                {
                    var numberString = function[prefixLength..numberEnd];
                    Current = new Token(ParseNumbers.ToInt64(numberString, 2));

                    function = function[numberEnd..];

                    return true;
                }
            }

            return false;
        }

        private bool CreateHexToken()
        {
            const int prefixLength = 2;

            if (function.Length > prefixLength && CheckHexPrefix(function))
            {
                var numberEnd = prefixLength;
                while (numberEnd < function.Length && IsHexNumber(function[numberEnd]))
                    numberEnd++;

                if (numberEnd > prefixLength)
                {
                    var numberString = function[prefixLength..numberEnd];
                    Current = new Token(ParseNumbers.ToInt64(numberString, 16));

                    function = function[numberEnd..];

                    return true;
                }
            }

            return false;
        }

        private bool CreateOctToken()
        {
            const int prefixLength = 1;

            if (function.Length > prefixLength && function[0] == '0')
            {
                var numberEnd = prefixLength;
                while (numberEnd < function.Length && IsOctNumber(function[numberEnd]))
                    numberEnd++;

                if (numberEnd > prefixLength)
                {
                    var numberString = function[prefixLength..numberEnd];
                    Current = new Token(ParseNumbers.ToInt64(numberString, 8));

                    function = function[numberEnd..];

                    return true;
                }
            }

            return false;
        }

        private bool CreateDecToken()
        {
            var endIndex = 0;
            ReadDigits(function, ref endIndex);

            if (endIndex > 0 && function.Length >= endIndex)
            {
                if (CheckNextSymbol(function, ref endIndex, '.'))
                {
                    var dotIndex = endIndex;

                    ReadDigits(function, ref endIndex);

                    if (dotIndex == endIndex)
                        return false;
                }

                if (CheckNextSymbol(function, ref endIndex, 'e') ||
                    CheckNextSymbol(function, ref endIndex, 'E'))
                {
                    _ = CheckNextSymbol(function, ref endIndex, '+') ||
                        CheckNextSymbol(function, ref endIndex, '-');

                    ReadDigits(function, ref endIndex);
                }

                var numberString = function[..endIndex];
                var number = double.Parse(numberString, provider: CultureInfo.InvariantCulture);
                Current = new Token(number);

                function = function[endIndex..];

                return true;
            }

            return false;
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
            => (uint)(symbol - min) <= (uint)(max - min);

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