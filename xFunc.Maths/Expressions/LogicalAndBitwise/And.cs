// Copyright 2012-2017 Dmitry Kischenko
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
using System.Diagnostics.CodeAnalysis;
using xFunc.Maths.Analyzers;

namespace xFunc.Maths.Expressions.LogicalAndBitwise
{

    /// <summary>
    /// Represents a AND operation.
    /// </summary>
    public class And : BinaryExpression
    {

        [ExcludeFromCodeCoverage]
        internal And() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="And"/> class.
        /// </summary>
        /// <param name="left">The left (first) operand.</param>
        /// <param name="right">The right (second) operand.</param>
        public And(IExpression left, IExpression right) : base(left, right) { }

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
        /// Executes this AND expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        public override object Execute(ExpressionParameters parameters)
        {
            var left = m_left.Execute(parameters);
            var right = m_right.Execute(parameters);

            if (left is bool && right is bool)
                return (bool)left & (bool)right;
            else
                return (double)((int)Math.Round((double)left, MidpointRounding.AwayFromZero) & (int)Math.Round((double)right, MidpointRounding.AwayFromZero));
        }

        /// <summary>
        /// Analyzes the current expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="analyzer">The analyzer.</param>
        /// <returns>
        /// The analysis result.
        /// </returns>
        public override TResult Analyze<TResult>(IAnalyzer<TResult> analyzer)
        {
            return analyzer.Analyze(this);
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
                if (m_right != null)
                {
                    if (m_right.ResultType == ExpressionResultType.Number)
                        return ExpressionResultType.Number;

                    if (m_right.ResultType == ExpressionResultType.Boolean)
                        return ExpressionResultType.Boolean;
                }

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
                if (m_left != null)
                {
                    if (m_left.ResultType == ExpressionResultType.Number)
                        return ExpressionResultType.Number;

                    if (m_left.ResultType == ExpressionResultType.Boolean)
                        return ExpressionResultType.Boolean;
                }

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
                if (m_left.ResultType == ExpressionResultType.Number || m_right.ResultType == ExpressionResultType.Number)
                    return ExpressionResultType.Number;

                if (m_left.ResultType == ExpressionResultType.Boolean || m_right.ResultType == ExpressionResultType.Boolean)
                    return ExpressionResultType.Boolean;

                return ExpressionResultType.Number | ExpressionResultType.Boolean;
            }
        }

    }

}
