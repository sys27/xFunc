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
    /// Represents a bitwise AND operation.
    /// </summary>
    public class And : BinaryMathExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="And"/> class.
        /// </summary>
        public And()
            : base(null, null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="And"/> class.
        /// </summary>
        /// <param name="firstMathExpression">The left operand.</param>
        /// <param name="secondMathExpression">The right operand.</param>
        /// <seealso cref="IMathExpression"/>
        public And(IMathExpression firstMathExpression, IMathExpression secondMathExpression)
            : base(firstMathExpression, secondMathExpression)
        {

        }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            if (parent is BinaryMathExpression)
            {
                return ToString("({0} and {1})");
            }

            return ToString("{0} and {1}");
        }

        /// <summary>
        /// Calculates this bitwise AND expression. Don't use this method if your expression has variables.
        /// </summary>
        /// <returns>A result of the calculation.</returns>
        public override double Calculate()
        {
            return (int)left.Calculate() & (int)right.Calculate();
        }

        /// <summary>
        /// Calculates this bitwise AND expression.
        /// </summary>
        /// <param name="parameters">A collection of variables that are used in the expression.</param>
        /// <returns>A result of the calculation.</returns>
        public override double Calculate(MathParameterCollection parameters)
        {
            return (int)left.Calculate(parameters) & (int)right.Calculate(parameters);
        }

        /// <summary>
        /// Calculates this bitwise NOT expression.
        /// </summary>
        /// <param name="parameters">A collection of variables that are used in the expression.</param>
        /// <param name="functions">A collection of user-defined functions.</param>
        /// <returns>A result of the calculation.</returns>
        public override double Calculate(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            return (int)left.Calculate(parameters, functions) & (int)right.Calculate(parameters, functions);
        }

        /// <summary>
        /// Clones this instance of the <see cref="And"/>.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IMathExpression"/> that is a clone of this instance.</returns>
        public override IMathExpression Clone()
        {
            return new And(left.Clone(), right.Clone());
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
