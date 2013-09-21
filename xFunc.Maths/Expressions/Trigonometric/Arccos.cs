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
    /// Represents the Arccosine function.
    /// </summary>
    [ReverseFunction(typeof(Cos))]
    public class Arccos : TrigonometryMathExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Arccos"/> class.
        /// </summary>
        public Arccos() : base(null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Arccos"/> class.
        /// </summary>
        /// <param name="firstMathExpression">The argument of function.</param>
        public Arccos(IMathExpression firstMathExpression) : base(firstMathExpression) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Arccos"/> class.
        /// </summary>
        /// <param name="firstMathExpression">The argument of function.</param>
        /// <param name="angleMeasurement">The angle measurement.</param>
        public Arccos(IMathExpression firstMathExpression, AngleMeasurement angleMeasurement) : base(firstMathExpression, angleMeasurement) { }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            return ToString("arccos({0})");
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

            return Math.Acos(radian) / Math.PI * 180;
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
            return Math.Acos(argument.Calculate(parameters, functions));
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

            return Math.Acos(radian) / Math.PI * 200;
        }

        protected override IMathExpression _Differentiation(Variable variable)
        {
            var involution = new Pow(argument.Clone(), new Number(2));
            var sub = new Sub(new Number(1), involution);
            var sqrt = new Sqrt(sub);
            var division = new Div(argument.Clone().Differentiate(variable), sqrt);
            var unMinus = new UnaryMinus(division);

            return unMinus;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>The new instance of <see cref="IMathExpression"/> that is a clone of this instance.</returns>
        public override IMathExpression Clone()
        {
            return new Arccos(argument.Clone());
        }

    }

}
