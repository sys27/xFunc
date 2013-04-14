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

namespace xFunc.Maths.Expressions.Bitwise
{

    /// <summary>
    /// Represents a bitwise OR operation.
    /// </summary>
    public class Or : BinaryMathExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Or"/> class.
        /// </summary>
        public Or()
            : base(null, null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Or"/> class.
        /// </summary>
        /// <param name="firstMathExpression">The left operand.</param>
        /// <param name="secondMathExpression">The right operand.</param>
        /// <seealso cref="IMathExpression"/>
        public Or(IMathExpression firstMathExpression, IMathExpression secondMathExpression)
            : base(firstMathExpression, secondMathExpression)
        {

        }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            if (parentMathExpression is BinaryMathExpression)
            {
                return ToString("({0} or {1})");
            }

            return ToString("{0} or {1}");
        }

        /// <summary>
        /// Calculates this bitwise OR expression. Don't use this method if your expression has variables.
        /// </summary>
        /// <returns>A result of the calculation.</returns>
        public override double Calculate()
        {
            return (int)firstMathExpression.Calculate() | (int)secondMathExpression.Calculate();
        }

        /// <summary>
        /// Calculates this bitwise OR expression.
        /// </summary>
        /// <param name="parameters">A collection of variables that are used in the expression.</param>
        /// <returns>A result of the calculation.</returns>
        public override double Calculate(MathParameterCollection parameters)
        {
            return (int)firstMathExpression.Calculate(parameters) | (int)secondMathExpression.Calculate(parameters);
        }

        /// <summary>
        /// Clones this instanse of the <see cref="Or"/>.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IMathExpression"/> that is a clone of this instance.</returns>
        public override IMathExpression Clone()
        {
            return new Or(firstMathExpression.Clone(), secondMathExpression.Clone());
        }

        /// <summary>
        /// This method always throws the <see cref="NotSupportedException"/> exception.
        /// </summary>
        /// <param name="variable">This method always throws the <see cref="NotSupportedException"/> exception.</param>
        /// <returns>This method always throws the <see cref="NotSupportedException"/> exception.</returns>
        /// <exception cref="NotSupportedException">Always is thrown.</exception>
        public override IMathExpression Differentiate(Variable variable)
        {
            throw new NotSupportedException();
        }

    }

}
