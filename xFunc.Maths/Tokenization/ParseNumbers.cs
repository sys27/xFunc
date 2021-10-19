// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace xFunc.Maths.Tokenization
{
    // TODO: temporary?
    // this class was copied from .NET Core source code (dotnet/runtime),
    // we need to use Span<T> based API to convert number to number with different bases.

    /// <summary>Methods for parsing numbers and strings.</summary>
    [ExcludeFromCodeCoverage]
    internal static class ParseNumbers
    {
        /// <summary>
        /// Converts the string representation of a number in a specified base to an equivalent 64-bit signed integer.
        /// </summary>
        /// <param name="value">A string that contains the number to convert.</param>
        /// <param name="fromBase">The base of the number in value, which must be 2, 8, 10, or 16.</param>
        /// <returns>A 64-bit signed integer that is equivalent to the number in value, or 0 (zero) if value is null.</returns>
        public static long ToInt64(ReadOnlySpan<char> value, int fromBase)
        {
            var i = 0;
            var grabNumbersStart = i;
            var result = GrabLongs(fromBase, value, ref i);

            var length = value.Length;
            if (i == grabNumbersStart || i < length)
                throw new FormatException();

            if ((ulong)result == 0x8000000000000000 && fromBase == 10)
                throw new OverflowException();

            return result;
        }

        private static long GrabLongs(int radix, ReadOnlySpan<char> s, ref int i)
        {
            ulong result = 0;
            ulong maxVal;

            if (radix == 10)
            {
                maxVal = 0x7FFFFFFFFFFFFFFF / 10;

                while (i < s.Length && IsDigit(s[i], radix, out var value))
                {
                    if (result > maxVal || ((long)result) < 0)
                        throw new OverflowException();

                    result = result * (ulong)radix + (ulong)value;
                    i++;
                }

                if ((long)result < 0 && result != 0x8000000000000000)
                    throw new OverflowException();
            }
            else
            {
                Debug.Assert(radix == 2 || radix == 8 || radix == 16, "The radix should be equal to 2, 8, 10 or 16.");
                maxVal = radix switch
                {
                    16 => 0xffffffffffffffff / 16,
                    8 => 0xffffffffffffffff / 8,
                    _ => 0xffffffffffffffff / 2,
                };

                while (i < s.Length && IsDigit(s[i], radix, out var value))
                {
                    if (result > maxVal)
                        throw new OverflowException();

                    var temp = result * (ulong)radix + (ulong)value;

                    if (temp < result)
                        throw new OverflowException();

                    result = temp;
                    i++;
                }
            }

            return (long)result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsDigit(char c, int radix, out int result)
        {
            int tmp;
            if ((uint)(c - '0') <= 9)
            {
                result = tmp = c - '0';
            }
            else if ((uint)(c - 'A') <= 'Z' - 'A')
            {
                result = tmp = c - 'A' + 10;
            }
            else if ((uint)(c - 'a') <= 'z' - 'a')
            {
                result = tmp = c - 'a' + 10;
            }
            else
            {
                result = -1;
                return false;
            }

            return tmp < radix;
        }
    }
}