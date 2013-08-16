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

namespace xFunc.Maths.Expressions
{

    /// <summary>
    /// Represents a greatest common divisor.
    /// </summary>
    public class GCD : BinaryMathExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GCD"/> class.
        /// </summary>
        public GCD()
            : base(null, null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GCD"/> class.
        /// </summary>
        /// <param name="firstMathExpression">The first operand.</param>
        /// <param name="secondMathExpression">The second operand.</param>
        public GCD(IMathExpression firstMathExpression, IMathExpression secondMathExpression)
            : base(firstMathExpression, secondMathExpression)
        {

        }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            return ToString("gcd({0}, {1})");
        }

        public override double Calculate()
        {
            var a = left.Calculate();
            var b = right.Calculate();

            return MathExtentions.GCD(a, b);
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            var a = left.Calculate(parameters);
            var b = right.Calculate(parameters);

            return MathExtentions.GCD(a, b);
        }

        public override double Calculate(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            var a = left.Calculate(parameters, functions);
            var b = right.Calculate(parameters, functions);

            return MathExtentions.GCD(a, b);
        }

        /// <summary>
        /// Clones this instance of the <see cref="GCD"/>.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="GCD"/> that is a clone of this instance.</returns>
        public override IMathExpression Clone()
        {
            return new GCD(left.Clone(), right.Clone());
        }

        public override IMathExpression Differentiate(Variable variable)
        {
            throw new NotSupportedException();
        }

    }

}
