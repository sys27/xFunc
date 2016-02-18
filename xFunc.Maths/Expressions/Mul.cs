// Copyright 2012-2016 Dmitry Kischenko
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
using xFunc.Maths.Expressions.Matrices;

namespace xFunc.Maths.Expressions
{

    /// <summary>
    /// Represents the Multiplication operation.
    /// </summary>
    public class Mul : BinaryExpression
    {

        internal Mul() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mul"/> class.
        /// </summary>
        /// <param name="left">The first (left) operand.</param>
        /// <param name="right">The second (right) operand.</param>
        public Mul(IExpression left, IExpression right) : base(left, right) { }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode(7537, 1973);
        }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            if (m_parent is BinaryExpression)
            {
                return ToString("({0} * {1})");
            }

            return ToString("{0} * {1}");
        }

        /// <summary>
        /// Calculates this mathemarical expression.
        /// </summary>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        public override object Calculate(ExpressionParameters parameters)
        {
            if (ResultType == ExpressionResultType.Matrix)
            {
                if (m_left is Vector && m_right is Vector)
                    throw new NotSupportedException();

                IExpression l;
                if (m_left is Vector || m_left is Matrix)
                {
                    l = m_left;
                }
                else
                {
                    var temp = m_left.Calculate(parameters);
                    if (temp is IExpression)
                        l = (IExpression)temp;
                    else
                        l = new Number((double)temp);
                }
                IExpression r;
                if (m_right is Vector || m_right is Matrix)
                {
                    r = m_right;
                }
                else
                {
                    var temp = m_right.Calculate(parameters);
                    if (temp is IExpression)
                        r = (IExpression)temp;
                    else
                        r = new Number((double)temp);
                }

                if (l is Vector)
                {
                    if (r is Matrix)
                        return ((Vector)l).Mul((Matrix)r, parameters);

                    return ((Vector)l).Mul(r, parameters);
                }
                if (r is Vector)
                {
                    if (l is Matrix)
                        return ((Matrix)l).Mul((Vector)r, parameters);

                    return ((Vector)r).Mul(l, parameters);
                }

                if (l is Matrix && r is Matrix)
                    return ((Matrix)l).Mul((Matrix)r, parameters);
                if (l is Matrix)
                    return ((Matrix)l).Mul(r, parameters);
                if (r is Matrix)
                    return ((Matrix)r).Mul(l, parameters);
            }

            if (ResultType == ExpressionResultType.Number)
                return (double)m_left.Calculate(parameters) * (double)m_right.Calculate(parameters);

            throw new NotSupportedException();
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IExpression"/> that is a clone of this instance.</returns>
        public override IExpression Clone()
        {
            return new Mul(m_left.Clone(), m_right.Clone());
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
                return ExpressionResultType.Number | ExpressionResultType.Matrix;
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
                return ExpressionResultType.Number | ExpressionResultType.Matrix;
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
                if (m_left.ResultType.HasFlagNI(ExpressionResultType.Number) && m_right.ResultType.HasFlagNI(ExpressionResultType.Number))
                    return ExpressionResultType.Number;

                return ExpressionResultType.Matrix;
            }
        }

    }

}
