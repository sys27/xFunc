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
    /// Represents the Secant function.
    /// </summary>
    public class Sec : TrigonometryMathExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Sec"/> class.
        /// </summary>
        public Sec()
            : base(null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sec"/> class.
        /// </summary>
        /// <param name="firstMathExpression">The argument of function.</param>
        public Sec(IMathExpression firstMathExpression)
            : base(firstMathExpression)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sec"/> class.
        /// </summary>
        /// <param name="firstMathExpression">The argument of function.</param>
        /// <param name="angleMeasurement">The angle measurement.</param>
        public Sec(IMathExpression firstMathExpression, AngleMeasurement angleMeasurement) : base(firstMathExpression, angleMeasurement) { }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            return ToString("sec({0})");
        }

        protected override double CalculateDergee(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            var radian = firstMathExpression.Calculate(parameters, functions) * Math.PI / 180;

            return 1 / Math.Cos(radian);
        }

        protected override double CalculateRadian(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            return 1 / Math.Cos(firstMathExpression.Calculate(parameters, functions));
        }

        protected override double CalculateGradian(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            var radian = firstMathExpression.Calculate(parameters, functions) * Math.PI / 200;

            return 1 / Math.Cos(radian);
        }

        protected override IMathExpression _Differentiation(Variable variable)
        {
            Tan tan = new Tan(firstMathExpression.Clone(), this.angleMeasurement);
            Sec sec = new Sec(firstMathExpression.Clone(), this.angleMeasurement);
            Mul mul1 = new Mul(tan, sec);
            Mul mul2 = new Mul(firstMathExpression.Clone().Differentiate(variable), mul1);

            return mul2;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>The new instance of <see cref="IMathExpression"/> that is a clone of this instance.</returns>
        public override IMathExpression Clone()
        {
            return new Sec(firstMathExpression.Clone());
        }

    }

}
