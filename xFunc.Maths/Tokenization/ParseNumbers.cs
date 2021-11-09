#pragma warning disable SA1636

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// The MIT License (MIT)
//
// Copyright (c) .NET Foundation and Contributors
//
// All rights reserved.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace xFunc.Maths.Tokenization;

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

#pragma warning restore SA1636