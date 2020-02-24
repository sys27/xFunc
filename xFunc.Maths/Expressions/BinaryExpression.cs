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
    /// The base class for binary operations.
    /// </summary>
    public abstract class BinaryExpression : IExpression
    {
        /// <summary>
        /// The left (first) operand.
        /// </summary>
        private IExpression left;

        /// <summary>
        /// The right (second) operand.
        /// </summary>
        private IExpression right;

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
        /// Initializes a new instance of the <see cref="BinaryExpression"/> class.
        /// </summary>
        /// <param name="arguments">The list of arguments.</param>
        protected BinaryExpression(IExpression[] arguments)
        {
            if (arguments.Length < 2)
                throw new ParseException(Resource.LessParams);

            if (arguments.Length > 2)
                throw new ParseException(Resource.MoreParams);

            Left = arguments[0];
            Right = arguments[1];
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

            return left.Equals(exp.Left) && right.Equals(exp.Right);
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
            var hash = first;

            hash = hash * second + left.GetHashCode();
            hash = hash * second + right.GetHashCode();

            return hash;
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
        /// Gets or sets the left (first) operand.
        /// </summary>
        public virtual IExpression Left
        {
            get
            {
                return left;
            }
            set
            {
                left = value ?? throw new ArgumentNullException(nameof(value));
                left.Parent = this;
            }
        }

        /// <summary>
        /// Gets or sets the right (second) operand.
        /// </summary>
        public virtual IExpression Right
        {
            get
            {
                return right;
            }
            set
            {
                right = value ?? throw new ArgumentNullException(nameof(value));
                right.Parent = this;
            }
        }

        /// <summary>
        /// Gets or sets the parent expression.
        /// </summary>
        public virtual IExpression Parent { get; set; }
    }
}