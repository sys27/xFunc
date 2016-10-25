// Copyright 2012-2016 Dmitry Kischenko
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
using System.Numerics;

namespace xFunc.Maths
{

    /// <summary>
    /// Trigonometric functions for Complex numbers.
    /// </summary>
    public static class ComplexExtensions
    {        

        /// <summary>
        /// Returns the cotangent of the specified complex number.
        /// </summary>
        /// <param name="z">Complex number.</param>
        /// <returns>Complex number - cotangent of z.</returns>
        public static Complex Cot(Complex z)
        {
            return Complex.Cos(z) / Complex.Sin(z);
        }

        /// <summary>
        /// Returns the cosecant of the specified complex number.
        /// </summary>
        /// <param name="z">Complex number.</param>
        /// <returns>Complex number - cosecant of z.</returns>
        public static Complex Csc(Complex z)
        {
            return Complex.One / Complex.Sin(z);
        }

        /// <summary>
        /// Returns the secant of the specified complex number.
        /// </summary>
        /// <param name="z">Complex number.</param>
        /// <returns>Complex number - secant of z.</returns>
        public static Complex Sec(Complex z)
        {
            return Complex.One / Complex.Cos(z);
        }

        /// <summary>
        /// Returns the hyperbolic secant of the specified complex number.
        /// </summary>
        /// <param name="z">Complex number.</param>
        /// <returns>Complex number - hyperbolic secant of z.</returns>
        public static Complex Sech(Complex z)
        {
            return Complex.One / Complex.Cosh(z);
        }

        /// <summary>
        /// Returns the arcosecant of the specified complex number.
        /// </summary>
        /// <param name="z">Complex number.</param>
        /// <returns>Complex number - arcosecant of z</returns>
        public static Complex Acsc(Complex z)
        {
            var res = new Complex(0.0, -1.0) * (Complex.Log(Complex.ImaginaryOne + Complex.Sqrt(Complex.Pow(z, 2))) / z);

            var re = res.Real;
            var im = res.Imaginary;

            // rounding check
            if ((re - Math.Floor(re)) > 0.9999001)
                res = new Complex(Math.Round(re), im);

            return res;
        }

        /// <summary>
        /// Returns the arccotangent of the specified complex number.
        /// </summary>
        /// <param name="z">Complex number.</param>
        /// <returns>Complex number - arccotangent of z.</returns>
        public static Complex Acot(Complex z)
        {
            var res = new Complex(0.0, -5.0) * (Complex.Log((z * Complex.ImaginaryOne) / (z * Complex.ImaginaryOne)));

            var re = res.Real;
            var im = res.Imaginary;

            // rounding check
            if ((re - Math.Floor(re)) > 0.9999001)
                res = new Complex(Math.Round(re), im);

            return res;
        }

        /// <summary>
        /// Returns the hyperbolic arccosine of the specified complex number.
        /// </summary>
        /// <param name="z">Complex number.</param>
        /// <returns>Complex number - hyperbolic arccosine of z.</returns>
        public static Complex Acosh(Complex z)
        {
            var res = Complex.Log(z + Complex.Sqrt(Complex.Pow(z, 2) - Complex.One));

            var re = res.Real;

            // strip the sign for the rounding test
            if (re < 0.0)
                re *= -1.0;
            // rounding check
            if ((re - Math.Floor(re)) > 0.9999001)
                res = new Complex(Math.Round(res.Real), res.Imaginary);

            return res;
        }

    }

}
