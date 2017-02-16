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
using xFunc.Maths.Analyzers;
using xFunc.Maths.Analyzers.Formatters;

namespace xFunc.Maths.Expressions
{

    /// <summary>
    /// The base class for binary operations.
    /// </summary>
    public abstract class BinaryExpression : CachedResultTypeExpression
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
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="formatter">The formatter.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString(IFormatter formatter)
        {
            return this.Analyze(formatter);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.ToString(new CommonFormatter());
        }

        /// <summary>
        /// Gets the result type.
        /// </summary>
        /// <returns>The result type of current expression.</returns>
        protected override ExpressionResultType GetResultType()
        {
            return ExpressionResultType.Number;
        }

        /// <summary>
        /// Executes this expression. Don't use this method if your expression has variables or user-functions.
        /// </summary>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        public override object Execute()
        {
            return Execute(null);
        }

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

                IsChanged = true;
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

                IsChanged = true;
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
        public override IExpression Parent
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
        public override int MinParameters { get; } = 2;

        /// <summary>
        /// Gets the maximum count of parameters. -1 - Infinity.
        /// </summary>
        /// <value>
        /// The maximum count of parameters.
        /// </value>
        public override int MaxParameters { get; } = 2;

        /// <summary>
        /// Gets the count of parameters.
        /// </summary>
        /// <value>
        /// The count of parameters.
        /// </value>
        public override int ParametersCount { get; } = 2;

    }

}
