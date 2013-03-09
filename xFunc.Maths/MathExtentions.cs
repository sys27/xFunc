// Copyright 2012-2013 Dmitry Kischenko
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

namespace xFunc.Maths
{

    /// <summary>
    /// Provides static methods for trigonometric and hyperbolic functions.
    /// </summary>
    public static class MathExtentions
    {

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

    }

}
