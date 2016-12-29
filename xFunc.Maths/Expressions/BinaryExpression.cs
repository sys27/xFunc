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
using xFunc.Maths.Analyzers;

namespace xFunc.Maths.Expressions
{

    /// <summary>
    /// The base class for binary operations.
    /// </summary>
    public abstract class BinaryExpression : IExpression
    {

        /// <summary>
        /// The parent expression of this expression.
        /// </summary>
        protected IExpression m_parent;
        /// <summary>
        /// The left (first) operand.
        /// </summary>
        protected IExpression m_left;
        /// <summary>
        /// The right (second) operand.
        /// </summary>
        protected IExpression m_right;

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryExpression"/> class.
        /// </summary>
        protected BinaryExpression()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryExpression"/> class.
        /// </summary>
        /// <param name="left">The left (first) operand.</param>
        /// <param name="right">The right (second) operand.</param>
        protected BinaryExpression(IExpression left, IExpression right)
        {
            Left = left;
            Right = right;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;

            if (obj == null || this.GetType() != obj.GetType())
                return false;

            var exp = (BinaryExpression)obj;

            return m_left.Equals(exp.Left) && m_right.Equals(exp.Right);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return GetHashCode(6871, 6803);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        protected int GetHashCode(int first, int second)
        {
            int hash = first;

            hash = hash * second + m_left.GetHashCode();
            hash = hash * second + m_right.GetHashCode();

            return hash;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <param name="format">The format of result string.</param>
        /// <returns>A string that represents the current object.</returns>
        protected string ToString(string format)
        {
            return string.Format(format, m_left, m_right);
        }

        /// <summary>
        /// Executes this expression. Don't use this method if your expression has variables or user-functions.
        /// </summary>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        public virtual object Execute()
        {
            return Execute(null);
        }

        /// <summary>
        /// Executes this expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        public abstract object Execute(ExpressionParameters parameters);

        /// <summary>
        /// Analyzes the current expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="analyzer">The analyzer.</param>
        /// <returns>
        /// The analysis result.
        /// </returns>
        public abstract TResult Analyze<TResult>(IAnalyzer<TResult> analyzer);

        /// <summary>
        /// Creates the clone of this instance.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="BinaryExpression"/> that is a clone of this instance.</returns>
        public abstract IExpression Clone();

        /// <summary>
        /// The left (first) operand.
        /// </summary>
        public virtual IExpression Left
        {
            get
            {
                return m_left;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                if ((LeftType & value.ResultType) == ExpressionResultType.None)
                    throw new ParameterTypeMismatchException(LeftType, value.ResultType);

                m_left = value;
                m_left.Parent = this;
            }
        }

        /// <summary>
        /// Gets the type of the left parameter.
        /// </summary>
        /// <value>
        /// The type of the left parameter.
        /// </value>
        public virtual ExpressionResultType LeftType { get; } = ExpressionResultType.Number;

        /// <summary>
        /// The right (second) operand.
        /// </summary>
        public virtual IExpression Right
        {
            get
            {
                return m_right;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                if ((RightType & value.ResultType) == ExpressionResultType.None)
                    throw new ParameterTypeMismatchException(RightType, value.ResultType);

                m_right = value;
                m_right.Parent = this;
            }
        }

        /// <summary>
        /// Gets the type of the right parameter.
        /// </summary>
        /// <value>
        /// The type of the right parameter.
        /// </value>
        public virtual ExpressionResultType RightType { get; } = ExpressionResultType.Number;

        /// <summary>
        /// Get or Set the parent expression.
        /// </summary>
        public IExpression Parent
        {
            get
            {
                return m_parent;
            }
            set
            {
                m_parent = value;
            }
        }

        /// <summary>
        /// Gets the minimum count of parameters.
        /// </summary>
        /// <value>
        /// The minimum count of parameters.
        /// </value>
        public int MinParameters { get; } = 2;

        /// <summary>
        /// Gets the maximum count of parameters. -1 - Infinity.
        /// </summary>
        /// <value>
        /// The maximum count of parameters.
        /// </value>
        public int MaxParameters { get; } = 2;

        /// <summary>
        /// Gets the count of parameters.
        /// </summary>
        /// <value>
        /// The count of parameters.
        /// </value>
        public int ParametersCount { get; } = 2;

        /// <summary>
        /// Gets the type of the result.
        /// Default: Number.
        /// </summary>
        /// <value>
        /// The type of the result.
        /// </value>
        public virtual ExpressionResultType ResultType { get; } = ExpressionResultType.Number;

    }

}
