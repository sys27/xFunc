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
using System.Numerics;

namespace xFunc.Maths
{
    /// <summary>
    /// Provides static methods for additional functions.
    /// </summary>
    internal static class MathExtensions
    {
        /// <summary>
        /// The constant which is used to compare two double numbers.
        /// </summary>
        private const double Epsilon = 1E-14;

        /// <summary>
        /// Formats a complex number.
        /// </summary>
        /// <param name="complex">The complex number.</param>
        /// <returns>The formatted string.</returns>
        public static string Format(this Complex complex)
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
        public static bool Equals(double left, double right)
            => Math.Abs(left - right) < Epsilon;
    }
}