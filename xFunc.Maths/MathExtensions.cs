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
using System.Linq;
using System.Numerics;

namespace xFunc.Maths
{
    /// <summary>
    /// Provides static methods for additional functions.
    /// </summary>
    public static class MathExtensions
    {
        /// <summary>
        /// Returns a specified number raised to the specified power.
        /// </summary>
        /// <param name="number">A double-precision floating-point number to be raised to a power.</param>
        /// <param name="power">A double-precision floating-point number that specifies a power.</param>
        /// <returns>The <paramref name="number"/> raised to the <paramref name="power"/>.</returns>
        public static object Pow(double number, double power)
        {
            if (number < 0)
            {
                if ((BitConverter.DoubleToInt64Bits(power) & 1) == 1)
                {
                    return -Math.Pow(-number, power);
                }

                if (power > 0 && power < 1)
                {
                    return new Complex(0, Math.Pow(-number, power));
                }

                if (power < 0 && power > -1)
                {
                    return new Complex(0, -Math.Pow(-number, power));
                }
            }

            return Math.Pow(number, power);
        }

        /// <summary>
        /// Returns the cotangent of the specified angle.
        /// </summary>
        /// <param name="d">An angle, measured in radians.</param>
        /// <returns>The cotangent of d.</returns>
        public static double Cot(double d)
        {
            return Math.Cos(d) / Math.Sin(d);
        }

        /// <summary>
        /// Returns the hyperbolic cotangent of the specified angle.
        /// </summary>
        /// <param name="d">An angle, measured in radians.</param>
        /// <returns>The hyperbolic cotangent of value.</returns>
        public static double Coth(double d)
        {
            return (Math.Exp(d) + Math.Exp(-d)) / (Math.Exp(d) - Math.Exp(-d));
        }

        /// <summary>
        /// Returns the secant of the specified angle.
        /// </summary>
        /// <param name="d">An angle, measured in radians.</param>
        /// <returns>The secant of d.</returns>
        public static double Sec(double d)
        {
            return 1 / Math.Cos(d);
        }

        /// <summary>
        /// Returns the hyperbolic secant of the specified angle.
        /// </summary>
        /// <param name="d">An angle, measured in radians.</param>
        /// <returns>The hyperbolic secant of value.</returns>
        public static double Sech(double d)
        {
            return 2 / (Math.Exp(d) + Math.Exp(-d));
        }

        /// <summary>
        /// Returns the cosecant of the specified angle.
        /// </summary>
        /// <param name="d">An angle, measured in radians.</param>
        /// <returns>The cosecant of d.</returns>
        public static double Csc(double d)
        {
            return 1 / Math.Sin(d);
        }

        /// <summary>
        /// Returns the hyperbolic cosecant of the specified angle.
        /// </summary>
        /// <param name="d">An angle, measured in radians.</param>
        /// <returns>The hyperbolic cosecant of value.</returns>
        public static double Csch(double d)
        {
            return 2 / (Math.Exp(d) - Math.Exp(-d));
        }

        /// <summary>
        /// Returns the angle whose hyperbolic sine is the specified number.
        /// </summary>
        /// <param name="d">A number representing a hyperbolic sine.</param>
        /// <returns>An angle, measured in radians.</returns>
        public static double Asinh(double d)
        {
            return Math.Log(d + Math.Sqrt(d * d + 1));
        }

        /// <summary>
        /// Returns the angle whose hyperbolic cosine is the specified number.
        /// </summary>
        /// <param name="d">A number representing a hyperbolic cosine.</param>
        /// <returns>An angle, measured in radians.</returns>
        public static double Acosh(double d)
        {
            return Math.Log(d + Math.Sqrt(d + 1) * Math.Sqrt(d - 1));
        }

        /// <summary>
        /// Returns the angle whose hyperbolic tangent is the specified number.
        /// </summary>
        /// <param name="d">A number representing a hyperbolic tangent.</param>
        /// <returns>An angle, measured in radians.</returns>
        public static double Atanh(double d)
        {
            return Math.Log((1 + d) / (1 - d)) / 2;
        }

        /// <summary>
        /// Returns the angle whose cotangent is the specified number.
        /// </summary>
        /// <param name="d">A number representing a cotangent.</param>
        /// <returns>An angle, measured in radians.</returns>
        public static double Acot(double d)
        {
            return Math.PI / 2 - Math.Atan(d);
        }

        /// <summary>
        /// Returns the angle whose hyperbolic cotangent is the specified number.
        /// </summary>
        /// <param name="d">A number representing a hyperbolic cotangent.</param>
        /// <returns>An angle, measured in radians.</returns>
        public static double Acoth(double d)
        {
            return Math.Log((d + 1) / (d - 1)) / 2;
        }

        /// <summary>
        /// Returns the angle whose secant is the specified number.
        /// </summary>
        /// <param name="d">A number representing a secant.</param>
        /// <returns>An angle, measured in radians.</returns>
        public static double Asec(double d)
        {
            return Math.Acos(1 / d);
        }

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
        {
            return Math.Asin(1 / d);
        }

        /// <summary>
        /// Returns the angle whose hyperbolic cosecant is the specified number.
        /// </summary>
        /// <param name="d">A number representing a hyperbolic cosecant.</param>
        /// <returns>An angle, measured in radians.</returns>
        public static double Acsch(double d)
        {
            return Math.Log(1 / d + Math.Sqrt(1 / d * d + 1));
        }

        private static double GCD(double a, double b)
        {
            while (!(b.Equals(0) || Math.Abs(b) < 1E-14))
                b = a % (a = b);

            return a;
        }

        /// <summary>
        /// Computes the polynomial greatest common divisor.
        /// </summary>
        /// <param name="numbers">The numbers.</param>
        /// <returns>The greatest common divisor.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="numbers"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="numbers" /> should contain at least 2 elements.</exception>
        public static double GCD(params double[] numbers)
        {
            if (numbers == null)
                throw new ArgumentNullException(nameof(numbers));
            if (numbers.Length < 2)
                throw new ArgumentException();

            return numbers.Aggregate(GCD);
        }

        private static double LCM(double a, double b)
        {
            var numerator = Math.Abs(a * b);

            return numerator / GCD(a, b);
        }

        /// <summary>
        /// Computes the polynomial least common multiple.
        /// </summary>
        /// <param name="numbers">The numbers.</param>
        /// <returns>The least common multiple.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="numbers"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="numbers" /> should contain at least 2 elements.</exception>
        public static double LCM(params double[] numbers)
        {
            if (numbers == null)
                throw new ArgumentNullException(nameof(numbers));
            if (numbers.Length < 2)
                throw new ArgumentException();

            return numbers.Aggregate(LCM);
        }

        /// <summary>
        /// Converts <paramref name="number"/> to the new numeral system.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="numeralSystem">The numeral system.</param>
        /// <returns>String that contains the number in the new numeral system.</returns>
        public static string ToNewBase(int number, NumeralSystem numeralSystem)
        {
            return numeralSystem switch
            {
                NumeralSystem.Decimal => number.ToString(CultureInfo.InvariantCulture),
                NumeralSystem.Binary => "0b" + Convert.ToString(number, 2),
                NumeralSystem.Octal => "0" + Convert.ToString(number, 8),
                NumeralSystem.Hexidecimal => "0x" + Convert.ToString(number, 16),
                _ => null,
            };
        }

        /// <summary>
        /// Computes the factorial.
        /// </summary>
        /// <param name="n">An argument.</param>
        /// <returns>The factorial.</returns>
        public static double Fact(double n)
        {
            if (n < 0)
                return double.NaN;

            double result = 1;

            for (var i = n; i > 0; i--)
                result *= i;

            return result;
        }

        /// <summary>
        /// Formats a complex number.
        /// </summary>
        /// <param name="complex">The complex number.</param>
        /// <returns>The formatted string.</returns>
        public static string Format(this Complex complex)
        {
            if (complex.Real == 0)
            {
                if (complex.Imaginary == 1)
                    return "i";
                if (complex.Imaginary == -1)
                    return "-i";

                return $"{complex.Imaginary.ToString(CultureInfo.InvariantCulture)}i";
            }

            if (complex.Imaginary == 0)
                return $"{complex.Real.ToString(CultureInfo.InvariantCulture)}";

            if (complex.Imaginary > 0)
                return $"{complex.Real.ToString(CultureInfo.InvariantCulture)}+{complex.Imaginary.ToString(CultureInfo.InvariantCulture)}i";

            return $"{complex.Real.ToString(CultureInfo.InvariantCulture)}{complex.Imaginary.ToString(CultureInfo.InvariantCulture)}i";
        }
    }
}