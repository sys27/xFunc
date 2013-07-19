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

    public class Csc : TrigonometryMathExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Csc"/> class.
        /// </summary>
        public Csc()
            : base(null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Csc"/> class.
        /// </summary>
        /// <param name="firstMathExpression">The argument of function.</param>
        public Csc(IMathExpression firstMathExpression)
            : base(firstMathExpression)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Csc"/> class.
        /// </summary>
        /// <param name="firstMathExpression">The argument of function.</param>
        /// <param name="angleMeasurement">The angle measurement.</param>
        public Csc(IMathExpression firstMathExpression, AngleMeasurement angleMeasurement) : base(firstMathExpression, angleMeasurement) { }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            return ToString("csc({0})");
        }

        public override double CalculateDergee(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            var radian = firstMathExpression.Calculate(parameters, functions) * Math.PI / 180;

            return 1 / Math.Sin(radian);
        }

        public override double CalculateRadian(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            return 1 / Math.Sin(firstMathExpression.Calculate(parameters, functions));
        }

        public override double CalculateGradian(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            var radian = firstMathExpression.Calculate(parameters, functions) * Math.PI / 200;

            return 1 / Math.Sin(radian);
        }

        protected override IMathExpression _Differentiation(Variable variable)
        {
            UnaryMinus unary = new UnaryMinus(firstMathExpression.Clone().Differentiate(variable));
            Cot cot = new Cot(firstMathExpression.Clone(), this.angleMeasurement);
            Csc csc = new Csc(firstMathExpression.Clone(), this.angleMeasurement);
            Mul mul1 = new Mul(cot, csc);
            Mul mul2 = new Mul(unary, mul1);

            return mul2;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>The new instance of <see cref="IMathExpression"/> that is a clone of this instance.</returns>
        public override IMathExpression Clone()
        {
            return new Csc(firstMathExpression.Clone());
        }

    }

}
