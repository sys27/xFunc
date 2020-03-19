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
using System.Collections.Generic;
using System.Diagnostics;
using xFunc.Maths.Analyzers;
using xFunc.Maths.Analyzers.Formatters;
using xFunc.Maths.Resources;

namespace xFunc.Maths.Expressions.Programming
{
    /// <summary>
    /// The base class for binary operations with variable as first (left) operand.
    /// </summary>
    public abstract class VariableBinaryExpression : IExpression
    {
        private Variable variable;
        private IExpression right;

        /// <summary>
        /// Initializes a new instance of the <see cref="VariableBinaryExpression"/> class.
        /// </summary>
        /// <param name="left">The left (first) operand.</param>
        /// <param name="right">The right (second) operand.</param>
        protected VariableBinaryExpression(Variable left, IExpression right)
        {
            Variable = left;
            Right = right;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VariableBinaryExpression"/> class.
        /// </summary>
        /// <param name="arguments">The list of arguments.</param>
        protected VariableBinaryExpression(IList<IExpression> arguments)
        {
            if (arguments == null)
                throw new ArgumentNullException(nameof(arguments));

            if (arguments.Count < 2)
                throw new ParseException(Resource.LessParams);

            if (arguments.Count > 2)
                throw new ParseException(Resource.MoreParams);

            Debug.Assert(arguments[0] is Variable, "The argument is not variable.");

            Variable = (Variable)arguments[0];
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

            if (obj == null || GetType() != obj.GetType())
                return false;

            var exp = (VariableBinaryExpression)obj;

            return variable.Equals(exp.Variable) && right.Equals(exp.Right);
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <param name="formatter">The formatter.</param>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public string ToString(IFormatter formatter) => Analyze(formatter);

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString() => ToString(new CommonFormatter());

        /// <summary>
        /// Executes this expression. Don't use this method if your expression has variables or user-functions.
        /// </summary>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        public object Execute() => Execute(null);

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
        public TResult Analyze<TResult>(IAnalyzer<TResult> analyzer)
        {
            if (analyzer == null)
                throw new ArgumentNullException(nameof(analyzer));

            return AnalyzeInternal(analyzer);
        }

        /// <summary>
        /// Analyzes the current expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="analyzer">The analyzer.</param>
        /// <returns>
        /// The analysis result.
        /// </returns>
        private protected abstract TResult AnalyzeInternal<TResult>(IAnalyzer<TResult> analyzer);

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
        public virtual Variable Variable
        {
            get
            {
                return variable;
            }
            set
            {
                variable = value ?? throw new ArgumentNullException(nameof(value));
                variable.Parent = this;
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