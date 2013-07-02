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
    /// Represents a least common multiple.
    /// </summary>
    public class LCM : BinaryMathExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="LCM"/> class.
        /// </summary>
        public LCM()
            : base(null, null)
        {

        }

        public LCM(IMathExpression firstMathExpression, IMathExpression secondMathExpression)
            : base(firstMathExpression, secondMathExpression)
        {

        }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            return ToString("lcm({0}, {1})");
        }

        public override double Calculate()
        {
            var a = firstMathExpression.Calculate();
            var b = secondMathExpression.Calculate();

            return MathExtentions.LCM(a, b);
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            var a = firstMathExpression.Calculate(parameters);
            var b = secondMathExpression.Calculate(parameters);

            return MathExtentions.LCM(a, b);
        }

        public override double Calculate(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            var a = firstMathExpression.Calculate(parameters, functions);
            var b = secondMathExpression.Calculate(parameters, functions);

            return MathExtentions.LCM(a, b);
        }

        /// <summary>
        /// Clones this instance of the <see cref="LCM"/>.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="LCM"/> that is a clone of this instance.</returns>
        public override IMathExpression Clone()
        {
            return new LCM(firstMathExpression.Clone(), secondMathExpression.Clone());
        }

        public override IMathExpression Differentiate(Variable variable)
        {
            throw new NotSupportedException();
        }

    }

}
