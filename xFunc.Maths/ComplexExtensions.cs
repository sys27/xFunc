// Copyright 2012-2019 Dmitry Kischenko
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
        /// <param name="number">Complex number.</param>
        /// <returns>Cotangent of complex number.</returns>
        public static Complex Cot(Complex number)
        {
            return Complex.Cos(number) / Complex.Sin(number);
        }

        /// <summary>
        /// Returns the secant of the specified complex number.
        /// </summary>
        /// <param name="number">Complex number.</param>
        /// <returns>Secant of complex number.</returns>
        public static Complex Sec(Complex number)
        {
            return Complex.One / Complex.Cos(number);
        }

        /// <summary>
        /// Returns the cosecant of the specified complex number.
        /// </summary>
        /// <param name="number">Complex number.</param>
        /// <returns>Cosecant of complex number.</returns>
        public static Complex Csc(Complex number)
        {
            return Complex.One / Complex.Sin(number);
        }

        /// <summary>
        /// Returns the arccotangent of the specified complex number.
        /// </summary>
        /// <param name="number">Complex number.</param>
        /// <returns>Arccotangent of complex number.</returns>
        public static Complex Acot(Complex number)
        {
            return new Complex(0.0, 0.5) * (Complex.Log(1 - Complex.ImaginaryOne / number) - Complex.Log(1 + Complex.ImaginaryOne / number));
        }

        /// <summary>
        /// Returns the arcosecant of the specified complex number.
        /// </summary>
        /// <param name="number">Complex number.</param>
        /// <returns>Arcosecant of complex number.</returns>
        public static Complex Asec(Complex number)
        {
            return Complex.ImaginaryOne * Complex.Log(Complex.Sqrt(1 / Complex.Pow(number, 2) - 1) + 1 / number);
        }

        /// <summary>
        /// Returns the arcosecant of the specified complex number.
        /// </summary>
        /// <param name="number">Complex number.</param>
        /// <returns>Arcosecant of complex number.</returns>
        public static Complex Acsc(Complex number)
        {
            return -Complex.ImaginaryOne * Complex.Log(Complex.Sqrt(1 - 1 / Complex.Pow(number, 2)) + Complex.ImaginaryOne / number);
        }

        /// <summary>
        /// Returns the hyperbolic cotangent of the specified complex number.
        /// </summary>
        /// <param name="number">Complex number.</param>
        /// <returns>Hyperbolic cotangent of complex number.</returns>
        public static Complex Coth(Complex number)
        {
            return 1 / Complex.Tanh(number);
        }

        /// <summary>
        /// Returns the hyperbolic secant of the specified complex number.
        /// </summary>
        /// <param name="number">Complex number.</param>
        /// <returns>Hyperbolic secant of complex number.</returns>
        public static Complex Sech(Complex number)
        {
            return Complex.One / Complex.Cosh(number);
        }

        /// <summary>
        /// Returns the hyperbolic cosecant of the specified complex number.
        /// </summary>
        /// <param name="number">Complex number.</param>
        /// <returns>Hyperbolic cosecant of complex number.</returns>
        public static Complex Csch(Complex number)
        {
            return Complex.One / Complex.Sinh(number);
        }

        /// <summary>
        /// Returns the hyperbolic arsine of the specified complex number.
        /// </summary>
        /// <param name="number">Complex number.</param>
        /// <returns>Hyperbolic arsine of complex number.</returns>
        public static Complex Asinh(Complex number)
        {
            return Complex.Log(number + Complex.Sqrt(Complex.Pow(number, 2) + 1));
        }

        /// <summary>
        /// Returns the hyperbolic arcosine of the specified complex number.
        /// </summary>
        /// <param name="number">Complex number.</param>
        /// <returns>Hyperbolic arcosine of complex number.</returns>
        public static Complex Acosh(Complex number)
        {
            return Complex.Log(number + Complex.Sqrt(number + Complex.One) * Complex.Sqrt(number - Complex.One));
        }

        /// <summary>
        /// Returns the hyperbolic artangent of the specified complex number.
        /// </summary>
        /// <param name="number">Complex number.</param>
        /// <returns>Hyperbolic arctangent of complex number.</returns>
        public static Complex Atanh(Complex number)
        {
            return 0.5 * Complex.Log((1 + number) / (1 - number));
        }

        /// <summary>
        /// Returns the hyperbolic arcotangent of the specified complex number.
        /// </summary>
        /// <param name="number">Complex number.</param>
        /// <returns>Hyperbolic arcoctangent of complex number.</returns>
        public static Complex Acoth(Complex number)
        {
            return 0.5 * Complex.Log((number + 1) / (number - 1));
            //return 0.5 * Complex.Log(1 + 1 / number) - 0.5 * Complex.Log(1 - 1 / number);
        }

        /// <summary>
        /// Returns the hyperbolic arsecant of the specified complex number.
        /// </summary>
        /// <param name="number">Complex number.</param>
        /// <returns>Hyperbolic arsecant of complex number.</returns>
        public static Complex Asech(Complex number)
        {
            return Complex.Log(1 / number + Complex.Sqrt(1 / number + 1) * Complex.Sqrt(1 / number - 1));
        }

        /// <summary>
        /// Returns the hyperbolic arcosecant of the specified complex number.
        /// </summary>
        /// <param name="number">Complex number.</param>
        /// <returns>Hyperbolic arcosecant of complex number.</returns>
        public static Complex Acsch(Complex number)
        {
            return Complex.Log(1 / number + Complex.Sqrt(1 / Complex.Pow(number, 2) + 1));
        }

    }

}
