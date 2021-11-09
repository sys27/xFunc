// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace xFunc.Maths;

/// <summary>
/// Provides static methods for additional functions.
/// </summary>
internal static class MathExtensions
{
    /// <summary>
    /// The constant which is used to compare two double numbers.
    /// </summary>
    internal const double Epsilon = 1E-14;

    /// <summary>
    /// Formats a complex number.
    /// </summary>
    /// <param name="complex">The complex number.</param>
    /// <returns>The formatted string.</returns>
    internal static string Format(this Complex complex)
    {
        if (Equals(complex.Real, 0))
        {
            if (Equals(complex.Imaginary, 1))
                return "i";
            if (Equals(complex.Imaginary, -1))
                return "-i";

            return $"{complex.Imaginary.ToString(CultureInfo.InvariantCulture)}i";
        }

        if (Equals(complex.Imaginary, 0))
            return complex.Real.ToString(CultureInfo.InvariantCulture);

        if (complex.Imaginary > 0)
            return $"{complex.Real.ToString(CultureInfo.InvariantCulture)}+{complex.Imaginary.ToString(CultureInfo.InvariantCulture)}i";

        return $"{complex.Real.ToString(CultureInfo.InvariantCulture)}{complex.Imaginary.ToString(CultureInfo.InvariantCulture)}i";
    }

    /// <summary>
    /// Determines whether the specified number is equal to the current number.
    /// </summary>
    /// <param name="left">The current number.</param>
    /// <param name="right">The number to compare with the current number.</param>
    /// <returns><c>true</c> if the specified number is equal to the current number; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool Equals(double left, double right)
        => Math.Abs(left - right) < Epsilon;
}