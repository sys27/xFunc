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

namespace xFunc.Maths.Expressions.Trigonometric
{

    /// <summary>
    /// Represents the Sine function.
    /// </summary>
    [ReverseFunction(typeof(Arcsin))]
    public class Sin : TrigonometryMathExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Sin"/> class.
        /// </summary>
        internal Sin() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sin"/> class.
        /// </summary>
        /// <param name="firstMathExpression">The argument of function.</param>
        public Sin(IMathExpression firstMathExpression) : base(firstMathExpression) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sin"/> class.
        /// </summary>
        /// <param name="firstMathExpression">The argument of function.</param>
        /// <param name="angleMeasurement">The angle measurement.</param>
        public Sin(IMathExpression firstMathExpression, AngleMeasurement angleMeasurement) : base(firstMathExpression, angleMeasurement) { }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            return ToString("sin({0})");
        }

        /// <summary>
        /// Calculates this mathemarical expression (using degree).
        /// </summary>
        /// <param name="parameters">A collection of variables that are used in the expression.</param>
        /// <param name="functions">A collection of functions that are used in the expression.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="MathParameterCollection" />
        /// <seealso cref="MathFunctionCollection" />
        protected override double CalculateDergee(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            var radian = argument.Calculate(parameters, functions) * Math.PI / 180;

            return Math.Sin(radian);
        }

        /// <summary>
        /// Calculates this mathemarical expression (using radian).
        /// </summary>
        /// <param name="parameters">A collection of variables that are used in the expression.</param>
        /// <param name="functions">A collection of functions that are used in the expression.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="MathParameterCollection" />
        /// <seealso cref="MathFunctionCollection" />
        protected override double CalculateRadian(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            return Math.Sin(argument.Calculate(parameters, functions));
        }

        /// <summary>
        /// Calculates this mathemarical expression (using gradian).
        /// </summary>
        /// <param name="parameters">A collection of variables that are used in the expression.</param>
        /// <param name="functions">A collection of functions that are used in the expression.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="MathParameterCollection" />
        /// <seealso cref="MathFunctionCollection" />
        protected override double CalculateGradian(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            var radian = argument.Calculate(parameters, functions) * Math.PI / 200;

            return Math.Sin(radian);
        }

        /// <summary>
        /// Calculates a derivative of the expression.
        /// </summary>
        /// <param name="variable"></param>
        /// <returns>
        /// Returns a derivative of the expression of several variables.
        /// </returns>
        /// <seealso cref="Variable" />
        protected override IMathExpression _Differentiation(Variable variable)
        {
            var cos = new Cos(argument.Clone(), angleMeasurement);
            var mul = new Mul(cos, argument.Clone().Differentiate(variable));

            return mul;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>The new instance of <see cref="IMathExpression"/> that is a clone of this instance.</returns>
        public override IMathExpression Clone()
        {
            return new Sin(argument.Clone());
        }

    }

}
