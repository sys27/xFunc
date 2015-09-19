// Copyright 2012-2015 Dmitry Kischenko
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

namespace xFunc.Maths.Expressions.LogicalAndBitwise
{

    /// <summary>
    /// Represents a AND operation.
    /// </summary>
    public class And : BinaryExpression
    {

        internal And() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="And"/> class.
        /// </summary>
        /// <param name="left">The left (first) operand.</param>
        /// <param name="right">The right (second) operand.</param>
        public And(IExpression left, IExpression right)
            : base(left, right)
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
            return base.GetHashCode(4691, 9043);
        }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            if (m_parent is BinaryExpression)
            {
                return ToString("({0} and {1})");
            }

            return ToString("{0} and {1}");
        }

        /// <summary>
        /// Calculates this AND expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        public override object Calculate(ExpressionParameters parameters)
        {
#if PORTABLE
            if (ResultType == ExpressionResultType.Number)
                return (int)Math.Round((double)m_left.Calculate(parameters)) & (int)Math.Round((double)m_right.Calculate(parameters));
            else
                return (bool)m_left.Calculate(parameters) & (bool)m_right.Calculate(parameters);
#else
            if (ResultType == ExpressionResultType.Number)
                return (int)Math.Round((double)m_left.Calculate(parameters), MidpointRounding.AwayFromZero) & (int)Math.Round((double)m_right.Calculate(parameters), MidpointRounding.AwayFromZero);
            else
                return (bool)m_left.Calculate(parameters) & (bool)m_right.Calculate(parameters);
#endif
        }

        /// <summary>
        /// Clones this instance of the <see cref="And"/>.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IExpression"/> that is a clone of this instance.</returns>
        public override IExpression Clone()
        {
            return new And(m_left.Clone(), m_right.Clone());
        }

        /// <summary>
        /// Gets the type of the left parameter.
        /// </summary>
        /// <value>
        /// The type of the left parameter.
        /// </value>
        public override ExpressionResultType LeftType
        {
            get
            {
                return ExpressionResultType.Number | ExpressionResultType.Boolean;
            }
        }

        /// <summary>
        /// Gets the type of the right parameter.
        /// </summary>
        /// <value>
        /// The type of the right parameter.
        /// </value>
        public override ExpressionResultType RightType
        {
            get
            {
                return ExpressionResultType.Number | ExpressionResultType.Boolean;
            }
        }

        /// <summary>
        /// Gets the type of the result.
        /// </summary>
        /// <value>
        /// The type of the result.
        /// </value>
        public override ExpressionResultType ResultType
        {
            get
            {
                if (m_left.ResultType.HasFlag(ExpressionResultType.Number | ExpressionResultType.Boolean) && m_right.ResultType.HasFlag(ExpressionResultType.Number | ExpressionResultType.Boolean))
                    return ExpressionResultType.Number | ExpressionResultType.Boolean;

                if (m_left.ResultType.HasFlag(ExpressionResultType.Number) && m_right.ResultType.HasFlag(ExpressionResultType.Number))
                    return ExpressionResultType.Number;

                return ExpressionResultType.Boolean;
            }
        }

    }

}
