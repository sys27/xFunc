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
    /// Represents the Arctangent function.
    /// </summary>
    [ReverseFunction(typeof(Tan))]
    public class Arctan : TrigonometryMathExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Arctan"/> class.
        /// </summary>
        public Arctan() : base(null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Arctan"/> class.
        /// </summary>
        /// <param name="firstMathExpression">The argument of function.</param>
        public Arctan(IMathExpression firstMathExpression) : base(firstMathExpression) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Arctan"/> class.
        /// </summary>
        /// <param name="firstMathExpression">The argument of function.</param>
        /// <param name="angleMeasurement">The angle measurement.</param>
        public Arctan(IMathExpression firstMathExpression, AngleMeasurement angleMeasurement) : base(firstMathExpression, angleMeasurement) { }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            return ToString("arctan({0})");
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
            var radian = argument.Calculate(parameters, functions);

            return Math.Atan(radian) / Math.PI * 180;
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
            return Math.Atan(argument.Calculate(parameters, functions));
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
            var radian = argument.Calculate(parameters, functions);

            return Math.Atan(radian) / Math.PI * 200;
        }

        protected override IMathExpression _Differentiation(Variable variable)
        {
            var involution = new Pow(argument.Clone(), new Number(2));
            var add = new Add(new Number(1), involution);
            var div = new Div(argument.Clone().Differentiate(variable), add);

            return div;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>The new instance of <see cref="IMathExpression"/> that is a clone of this instance.</returns>
        public override IMathExpression Clone()
        {
            return new Arctan(argument.Clone());
        }

    }

}
