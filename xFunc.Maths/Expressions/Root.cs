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
    /// Represents the nth root operation.
    /// </summary>
    public class Root : BinaryMathExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Root"/> class.
        /// </summary>
        public Root() : base(null, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Root"/> class.
        /// </summary>
        /// <param name="firstMathExpression">The first operand.</param>
        /// <param name="secondMathExpression">The second operand.</param>
        public Root(IMathExpression firstMathExpression, IMathExpression secondMathExpression) : base(firstMathExpression, secondMathExpression) { }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            return ToString("root({0}, {1})");
        }

        public override double Calculate()
        {
            return Calculate(null, null);
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return Calculate(parameters, null);
        }

        public override double Calculate(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            var first = firstMathExpression.Calculate(parameters, functions);
            var second = 1 / secondMathExpression.Calculate(parameters, functions);
            if (first < 0 && second % 2 != 0)
            {
                return -Math.Pow(-first, second);
            }

            return Math.Pow(first, second);
        }

        public override IMathExpression Differentiate(Variable variable)
        {
            if (MathParser.HasVar(firstMathExpression, variable) || MathParser.HasVar(secondMathExpression, variable))
            {
                var div = new Div(new Number(1), secondMathExpression.Clone());
                var inv = new Pow(firstMathExpression.Clone(), div);

                return inv.Differentiate(variable);
            }

            return new Number(0);
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IMathExpression"/> that is a clone of this instance.</returns>
        public override IMathExpression Clone()
        {
            return new Root(firstMathExpression.Clone(), secondMathExpression.Clone());
        }

    }

}
