// Copyright 2012-2020 Dmytro Kyshchenko
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
using xFunc.Maths.Resources;

namespace xFunc.Maths.Expressions
{
    /// <summary>
    /// The abstract base class that represents the unary operation.
    /// </summary>
    public abstract class UnaryExpression : IExpression
    {
        /// <summary>
        /// The (first) operand.
        /// </summary>
        private IExpression argument;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnaryExpression"/> class.
        /// </summary>
        /// <param name="argument">The expression.</param>
        protected UnaryExpression(IExpression argument)
        {
            Argument = argument;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnaryExpression"/> class.
        /// </summary>
        /// <param name="arguments">The list of arguments.</param>
        protected UnaryExpression(IExpression[] arguments)
        {
            if (arguments.Length < 1)
                throw new ParseException(Resource.LessParams);

            if (arguments.Length > 1)
                throw new ParseException(Resource.MoreParams);

            Argument = arguments[0];
        }

        /// <summary>
        /// Determines whether the specified <see cref="object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;

            if (obj == null || this.GetType() != obj.GetType())
                return false;

            return argument.Equals(((UnaryExpression)obj).Argument);
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
            return first ^ argument.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <param name="formatter">The formatter.</param>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public string ToString(IFormatter formatter)
        {
            return this.Analyze(formatter);
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.ToString(new CommonFormatter());
        }

        /// <summary>
        /// Executes this expression. Don't use this method if your expression has variables or user-functions.
        /// </summary>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        public object Execute()
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
        /// Clones this instance of the <see cref="IExpression" />.
        /// </summary>
        /// <returns>
        /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
        /// </returns>
        public abstract IExpression Clone();

        /// <summary>
        /// Gets or sets the expression.
        /// </summary>
        /// <value>The expression.</value>
        public virtual IExpression Argument
        {
            get
            {
                return argument;
            }
            set
            {
                argument = value ?? throw new ArgumentNullException(nameof(value));
                argument.Parent = this;
            }
        }

        /// <summary>
        /// Gets or sets the parent expression.
        /// </summary>
        public IExpression Parent { get; set; }
    }
}