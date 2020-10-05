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
using System.Collections.Immutable;
using System.Linq;
using xFunc.Maths.Analyzers;
using xFunc.Maths.Analyzers.Formatters;
using xFunc.Maths.Resources;

namespace xFunc.Maths.Expressions
{
    /// <summary>
    /// The base class for expressions with different number of parameters.
    /// </summary>
    public abstract class DifferentParametersExpression : IExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DifferentParametersExpression" /> class.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        protected DifferentParametersExpression(IEnumerable<IExpression> arguments)
            : this(arguments.ToImmutableArray())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DifferentParametersExpression" /> class.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        protected DifferentParametersExpression(ImmutableArray<IExpression> arguments)
        {
            if (arguments == null)
                throw new ArgumentNullException(nameof(arguments));

            if (arguments.Length < MinParametersCount)
                throw new ArgumentException(Resource.LessParams, nameof(arguments));

            if (arguments.Length > MaxParametersCount)
                throw new ArgumentException(Resource.MoreParams, nameof(arguments));

            if (arguments.Any(exp => exp is null))
                throw new ArgumentNullException(nameof(arguments));

            Arguments = arguments;
        }

        /// <summary>
        /// Gets or sets the <see cref="IExpression"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="IExpression"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns>The argument.</returns>
        public IExpression this[int index] => Arguments[index];

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is null || GetType() != obj.GetType())
                return false;

            var diff = (DifferentParametersExpression)obj;

            if (Arguments.Length != diff.Arguments.Length)
                return false;

            return Arguments.SequenceEqual(diff.Arguments);
        }

        /// <inheritdoc />
        public string ToString(IFormatter formatter) => Analyze(formatter);

        /// <inheritdoc />
        public override string ToString() => ToString(new CommonFormatter());

        /// <inheritdoc />
        public virtual object Execute() => Execute(null);

        /// <inheritdoc />
        public abstract object Execute(ExpressionParameters? parameters);

        /// <inheritdoc />
        public TResult Analyze<TResult>(IAnalyzer<TResult> analyzer)
        {
            if (analyzer is null)
                throw new ArgumentNullException(nameof(analyzer));

            return AnalyzeInternal(analyzer);
        }

        /// <inheritdoc />
        public TResult Analyze<TResult, TContext>(
            IAnalyzer<TResult, TContext> analyzer,
            TContext context)
        {
            if (analyzer is null)
                throw new ArgumentNullException(nameof(analyzer));

            return AnalyzeInternal(analyzer, context);
        }

        /// <summary>
        /// Analyzes the current expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="analyzer">The analyzer.</param>
        /// <returns>
        /// The analysis result.
        /// </returns>
        protected abstract TResult AnalyzeInternal<TResult>(IAnalyzer<TResult> analyzer);

        /// <summary>
        /// Analyzes the current expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="TContext">The type of additional parameter for analyzer.</typeparam>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="context">The context.</param>
        /// <returns>The analysis result.</returns>
        protected abstract TResult AnalyzeInternal<TResult, TContext>(
            IAnalyzer<TResult, TContext> analyzer,
            TContext context);

        /// <summary>
        /// Clones this instance of the <see cref="IExpression" />.
        /// </summary>
        /// <param name="arguments">The list of arguments.</param>
        /// <returns>
        /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
        /// </returns>
        public abstract IExpression Clone(ImmutableArray<IExpression>? arguments = null);

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public ImmutableArray<IExpression> Arguments { get; }

        /// <summary>
        /// Gets the count of parameters.
        /// </summary>
        public int ParametersCount => Arguments.Length;

        /// <summary>
        /// Gets the minimum count of parameters.
        /// </summary>
        public abstract int? MinParametersCount { get; }

        /// <summary>
        /// Gets the maximum count of parameters. <c>null</c> - Infinity.
        /// </summary>
        public abstract int? MaxParametersCount { get; }
    }
}