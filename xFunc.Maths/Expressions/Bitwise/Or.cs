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
    public class Or : BinaryExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Or"/> class.
        /// </summary>
        internal Or() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Or"/> class.
        /// </summary>
        /// <param name="firstMathExpression">The left operand.</param>
        /// <param name="secondMathExpression">The right operand.</param>
        /// <seealso cref="IExpression"/>
        public Or(IExpression firstMathExpression, IExpression secondMathExpression)
            : base(firstMathExpression, secondMathExpression)
        {

        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode(7727, 5657);
        }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            if (parent is BinaryExpression)
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
#if PORTABLE
            return (int)Math.Round(left.Calculate()) | (int)Math.Round(right.Calculate());
#else
            return (int)Math.Round(left.Calculate(), MidpointRounding.AwayFromZero) | (int)Math.Round(right.Calculate(), MidpointRounding.AwayFromZero);
#endif
        }

        /// <summary>
        /// Calculates this bitwise OR expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        public override double Calculate(ExpressionParameters parameters)
        {
#if PORTABLE
            return (int)Math.Round(left.Calculate(parameters)) | (int)Math.Round(right.Calculate(parameters));
#else
            return (int)Math.Round(left.Calculate(parameters), MidpointRounding.AwayFromZero) | (int)Math.Round(right.Calculate(parameters), MidpointRounding.AwayFromZero);
#endif
        }

        /// <summary>
        /// Clones this instance of the <see cref="Or"/>.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IExpression"/> that is a clone of this instance.</returns>
        public override IExpression Clone()
        {
            return new Or(left.Clone(), right.Clone());
        }

        /// <summary>
        /// Always throws <see cref="NotSupportedException" />.
        /// </summary>
        /// <param name="variable">The variable of differentiation.</param>
        /// <returns>
        /// Throws an exception.
        /// </returns>
        /// <seealso cref="Variable" />
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public override IExpression Differentiate(Variable variable)
        {
            throw new NotSupportedException();
        }

    }

}
