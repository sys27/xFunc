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
using xFunc.Maths.Analyzers.Formatters;

namespace xFunc.Maths.Expressions
{

    /// <summary>
    /// The abstract base class that represents the unary operation.
    /// </summary>
    public abstract class UnaryExpression : CachedResultTypeExpression
    {

        /// <summary>
        /// The (first) operand.
        /// </summary>
        protected IExpression m_argument;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnaryExpression"/> class.
        /// </summary>
        protected UnaryExpression() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnaryExpression"/> class.
        /// </summary>
        /// <param name="argument">The expression.</param>
        protected UnaryExpression(IExpression argument)
        {
            Argument = argument;
        }

        /// <summary>
        /// Determines whether the specified <see cref="Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="Object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;

            if (obj == null || this.GetType() != obj.GetType())
                return false;

            return m_argument.Equals(((UnaryExpression)obj).Argument);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return GetHashCode(7577);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        protected int GetHashCode(int first)
        {
            return first ^ m_argument.GetHashCode();
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
        /// <returns>
        /// The result type of current expression.
        /// </returns>
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
        /// Gets or sets the expression.
        /// </summary>
        /// <value>The expression.</value>
        public virtual IExpression Argument
        {
            get
            {
                return m_argument;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                if ((ArgumentType & value.ResultType) == ExpressionResultType.None)
                    throw new ParameterTypeMismatchException(ArgumentType, value.ResultType);

                m_argument = value;
                m_argument.Parent = this;

                IsChanged = true;
            }
        }

        /// <summary>
        /// Gets the type of the argument.
        /// </summary>
        /// <value>
        /// The type of the argument.
        /// </value>
        public virtual ExpressionResultType ArgumentType => ExpressionResultType.Number;

        /// <summary>
        /// Get or Set the parent expression.
        /// </summary>
        public override IExpression Parent { get; set; }

        /// <summary>
        /// Gets the minimum count of parameters.
        /// </summary>
        /// <value>
        /// The minimum count of parameters.
        /// </value>
        public override int MinParameters => 1;

        /// <summary>
        /// Gets the maximum count of parameters. -1 - Infinity.
        /// </summary>
        /// <value>
        /// The maximum count of parameters.
        /// </value>
        public override int MaxParameters => 1;

        /// <summary>
        /// Gets the count of parameters.
        /// </summary>
        /// <value>
        /// The count of parameters.
        /// </value>
        public override int ParametersCount => 1;

    }

}
