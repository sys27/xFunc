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
using xFunc.Maths.Resources;

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
        public const double Epsilon = 1E-14;

        /// <summary>
        /// sqrt(2).
        /// </summary>
        public const double Sqrt2 = 1.4142135623730951;

        /// <summary>
        /// sqrt(3).
        /// </summary>
        public const double Sqrt3 = 1.7320508075688772;

        /// <summary>
        /// 0.
        /// </summary>
        public const double Zero = 0.0;

        /// <summary>
        /// 1.
        /// </summary>
        public const double One = 1.0;

        /// <summary>
        /// 2.
        /// </summary>
        public const double Two = 2.0;

        /// <summary>
        /// 1 / 2 = 0.5.
        /// </summary>
        public const double Half = 0.5;

        /// <summary>
        /// sqrt(2) / 2.
        /// </summary>
        public const double Sqrt2By2 = Sqrt2 / 2.0;

        /// <summary>
        /// sqrt(3) / 2.
        /// </summary>
        public const double Sqrt3By2 = Sqrt3 / 2.0;

        /// <summary>
        /// sqrt(3) / 3.
        /// </summary>
        public const double Sqrt3By3 = Sqrt3 / 3.0;

        /// <summary>
        /// 2 * sqrt(3) / 3.
        /// </summary>
        public const double Sqrt3By3By2 = 2 * Sqrt3By3;

        /// <summary>
        /// Returns the cotangent of the specified angle.
        /// </summary>
        /// <param name="d">An angle, measured in radians.</param>
        /// <returns>The cotangent of d.</returns>
        public static double Cot(double d)
            => Math.Cos(d) / Math.Sin(d);

        /// <summary>
        /// Returns the hyperbolic cotangent of the specified angle.
        /// </summary>
        /// <param name="d">An angle, measured in radians.</param>
        /// <returns>The hyperbolic cotangent of value.</returns>
        public static double Coth(double d)
            => (Math.Exp(d) + Math.Exp(-d)) / (Math.Exp(d) - Math.Exp(-d));

        /// <summary>
        /// Returns the secant of the specified angle.
        /// </summary>
        /// <param name="d">An angle, measured in radians.</param>
        /// <returns>The secant of d.</returns>
        public static double Sec(double d)
            => 1 / Math.Cos(d);

        /// <summary>
        /// Returns the hyperbolic secant of the specified angle.
        /// </summary>
        /// <param name="d">An angle, measured in radians.</param>
        /// <returns>The hyperbolic secant of value.</returns>
        public static double Sech(double d)
            => 2 / (Math.Exp(d) + Math.Exp(-d));

        /// <summary>
        /// Returns the cosecant of the specified angle.
        /// </summary>
        /// <param name="d">An angle, measured in radians.</param>
        /// <returns>The cosecant of d.</returns>
        public static double Csc(double d)
            => 1 / Math.Sin(d);

        /// <summary>
        /// Returns the hyperbolic cosecant of the specified angle.
        /// </summary>
        /// <param name="d">An angle, measured in radians.</param>
        /// <returns>The hyperbolic cosecant of value.</returns>
        public static double Csch(double d)
            => 2 / (Math.Exp(d) - Math.Exp(-d));

        /// <summary>
        /// Returns the angle whose hyperbolic sine is the specified number.
        /// </summary>
        /// <param name="d">A number representing a hyperbolic sine.</param>
        /// <returns>An angle, measured in radians.</returns>
        public static double Asinh(double d)
            => Math.Log(d + Math.Sqrt(d * d + 1));

        /// <summary>
        /// Returns the angle whose hyperbolic cosine is the specified number.
        /// </summary>
        /// <param name="d">A number representing a hyperbolic cosine.</param>
        /// <returns>An angle, measured in radians.</returns>
        public static double Acosh(double d)
            => Math.Log(d + Math.Sqrt(d + 1) * Math.Sqrt(d - 1));

        /// <summary>
        /// Returns the angle whose hyperbolic tangent is the specified number.
        /// </summary>
        /// <param name="d">A number representing a hyperbolic tangent.</param>
        /// <returns>An angle, measured in radians.</returns>
        public static double Atanh(double d)
            => Math.Log((1 + d) / (1 - d)) / 2;

        /// <summary>
        /// Returns the angle whose cotangent is the specified number.
        /// </summary>
        /// <param name="d">A number representing a cotangent.</param>
        /// <returns>An angle, measured in radians.</returns>
        public static double Acot(double d)
            => Math.PI / 2 - Math.Atan(d);

        /// <summary>
        /// Returns the angle whose hyperbolic cotangent is the specified number.
        /// </summary>
        /// <param name="d">A number representing a hyperbolic cotangent.</param>
        /// <returns>An angle, measured in radians.</returns>
        public static double Acoth(double d)
            => Math.Log((d + 1) / (d - 1)) / 2;

        /// <summary>
        /// Returns the angle whose secant is the specified number.
        /// </summary>
        /// <param name="d">A number representing a secant.</param>
        /// <returns>An angle, measured in radians.</returns>
        public static double Asec(double d)
            => Math.Acos(1 / d);

        /// <summary>
        /// Returns the angle whose hyperbolic secant is the specified number.
        /// </summary>
        /// <param name="d">A number representing a hyperbolic secant.</param>
        /// <returns>An angle, measured in radians.</returns>
        public static double Asech(double d)
        {
            var z = 1 / d;
            return Math.Log(z + Math.Sqrt(z + 1) * Math.Sqrt(z - 1));
        }

        /// <summary>
        /// Returns the angle whose cosecant is the specified number.
        /// </summary>
        /// <param name="d">A number representing a hyperbolic cosecant.</param>
        /// <returns>An angle, measured in radians.</returns>
        public static double Acsc(double d)
            => Math.Asin(1 / d);

        /// <summary>
        /// Returns the angle whose hyperbolic cosecant is the specified number.
        /// </summary>
        /// <param name="d">A number representing a hyperbolic cosecant.</param>
        /// <returns>An angle, measured in radians.</returns>
        public static double Acsch(double d)
            => Math.Log(1 / d + Math.Sqrt(1 / d * d + 1));

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
        /// Check that double is an integer.
        /// </summary>
        /// <param name="value">The double value.</param>
        /// <returns>true if <paramref name="value"/> is an integer; otherwise, false.</returns>
        public static bool IsInt(this double value)
            => Math.Abs(value % 1) <= Epsilon;

        /// <summary>
        /// Calculates AND operation between two double values.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of AND operation.</returns>
        /// <exception cref="InvalidOperationException"><paramref name="left"/> or <paramref name="right"/> is not an integer.</exception>
        public static double And(this double left, double right)
        {
            if (!left.IsInt())
                throw new ArgumentException(Resource.ValueIsNotInteger, nameof(left));
            if (!right.IsInt())
                throw new ArgumentException(Resource.ValueIsNotInteger, nameof(right));

            return (int)left & (int)right;
        }

        /// <summary>
        /// Calculates OR operation between two double values.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of OR operation.</returns>
        /// <exception cref="InvalidOperationException"><paramref name="left"/> or <paramref name="right"/> is not an integer.</exception>
        public static double Or(this double left, double right)
        {
            if (!left.IsInt())
                throw new ArgumentException(Resource.ValueIsNotInteger, nameof(left));
            if (!right.IsInt())
                throw new ArgumentException(Resource.ValueIsNotInteger, nameof(right));

            return (int)left | (int)right;
        }

        /// <summary>
        /// Calculates XOR operation between two double values.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of XOR operation.</returns>
        /// <exception cref="InvalidOperationException"><paramref name="left"/> or <paramref name="right"/> is not an integer.</exception>
        public static double XOr(this double left, double right)
        {
            if (!left.IsInt())
                throw new ArgumentException(Resource.ValueIsNotInteger, nameof(left));
            if (!right.IsInt())
                throw new ArgumentException(Resource.ValueIsNotInteger, nameof(right));

            return (int)left ^ (int)right;
        }

        /// <summary>
        /// Calculates NOT operation.
        /// </summary>
        /// <param name="value">The left operand.</param>
        /// <returns>The result of NOT operation.</returns>
        /// <exception cref="InvalidOperationException"><paramref name="value"/> is not an integer.</exception>
        public static double Not(this double value)
        {
            if (!value.IsInt())
                throw new ArgumentException(Resource.ValueIsNotInteger, nameof(value));

            return ~(int)value;
        }

        /// <summary>
        /// Shifts <paramref name="left"/> by number of bits from <paramref name="right"/>.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of '&lt;&lt;' operation.</returns>
        /// <exception cref="InvalidOperationException"><paramref name="left"/> or <paramref name="right"/> is not an integer.</exception>
        public static double LeftShift(this double left, double right)
        {
            if (!left.IsInt())
                throw new ArgumentException(Resource.ValueIsNotInteger, nameof(left));
            if (!right.IsInt())
                throw new ArgumentException(Resource.ValueIsNotInteger, nameof(right));

            return (int)left << (int)right;
        }

        /// <summary>
        /// Shifts <paramref name="left"/> by number of bits from <paramref name="right"/>.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of '&gt;&gt;' operation.</returns>
        /// <exception cref="InvalidOperationException"><paramref name="left"/> or <paramref name="right"/> is not an integer.</exception>
        public static double RightShift(this double left, double right)
        {
            if (!left.IsInt())
                throw new ArgumentException(Resource.ValueIsNotInteger, nameof(left));
            if (!right.IsInt())
                throw new ArgumentException(Resource.ValueIsNotInteger, nameof(right));

            return (int)left >> (int)right;
        }

        /// <summary>
        /// Determines whether the specified number is equal to the current number.
        /// </summary>
        /// <param name="left">The current number.</param>
        /// <param name="right">The number to compare with the current number.</param>
        /// <returns><c>true</c> if the specified number is equal to the current number; otherwise, <c>false</c>.</returns>
        public static bool Equals(double left, double right)
            => Math.Abs(left - right) < Epsilon;

        /// <summary>
        /// Returns the fractional part of the number.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns>The fractional part.</returns>
        public static double Frac(double number)
        {
            if (number >= 0)
                return number - Math.Floor(number);

            return number - Math.Ceiling(number);
        }
    }
}