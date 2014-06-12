// Copyright 2012-2014 Dmitry Kischenko
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

namespace xFunc.Maths.Expressions.Programming
{

    /// <summary>
    /// Represents the equality operator.
    /// </summary>
    public class Equal : BinaryExpression
    {

        internal Equal() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Equal"/> class.
        /// </summary>
        /// <param name="left">The left (first) operand.</param>
        /// <param name="right">The right (second) operand.</param>
        public Equal(IExpression left, IExpression right)
            : base(left, right) { }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (m_parent is BinaryExpression)
                return ToString("({0} == {1})");

            return ToString("{0} == {1}");
        }

        /// <summary>
        /// Calculates this mathemarical expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        public override object Calculate(ExpressionParameters parameters)
        {
            var leftValue = (double)m_left.Calculate(parameters);
            var rightValue = (double)m_right.Calculate(parameters);

            return (leftValue == rightValue).AsNumber();
        }

        /// <summary>
        /// Creates the clone of this instance.
        /// </summary>
        /// <returns>
        /// Returns the new instance of <see cref="Equal" /> that is a clone of this instance.
        /// </returns>
        public override IExpression Clone()
        {
            return new Equal(m_left.Clone(), m_right.Clone());
        }

    }

}
