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
    /// Represents the Arcsecant function.
    /// </summary>
    public class Arcsec : TrigonometryMathExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Arcsec"/> class.
        /// </summary>
        public Arcsec()
            : base(null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Arcsec"/> class.
        /// </summary>
        /// <param name="firstMathExpression">The argument of function.</param>
        public Arcsec(IMathExpression firstMathExpression)
            : base(firstMathExpression)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Arcsec"/> class.
        /// </summary>
        /// <param name="firstMathExpression">The argument of function.</param>
        /// <param name="angleMeasurement">The angle measurement.</param>
        public Arcsec(IMathExpression firstMathExpression, AngleMeasurement angleMeasurement) : base(firstMathExpression, angleMeasurement) { }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            return ToString("arcsec({0})");
        }

        public override double CalculateDergee(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            return MathExtentions.Asec(firstMathExpression.Calculate(parameters, functions)) / Math.PI * 180;
        }

        public override double CalculateRadian(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            return MathExtentions.Asec(firstMathExpression.Calculate(parameters, functions));
        }

        public override double CalculateGradian(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            return MathExtentions.Asec(firstMathExpression.Calculate(parameters, functions)) / Math.PI * 200;
        }

        protected override IMathExpression _Differentiation(Variable variable)
        {
            Abs abs = new Abs(firstMathExpression.Clone());
            Pow sqr = new Pow(firstMathExpression.Clone(), new Number(2));
            Sub sub = new Sub(sqr, new Number(1));
            Sqrt sqrt = new Sqrt(sub);
            Mul mul = new Mul(abs, sqrt);
            Div div = new Div(firstMathExpression.Clone().Differentiate(variable), mul);

            return div;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>The new instance of <see cref="IMathExpression"/> that is a clone of this instance.</returns>
        public override IMathExpression Clone()
        {
            return new Arcsec(firstMathExpression.Clone());
        }

    }

}
