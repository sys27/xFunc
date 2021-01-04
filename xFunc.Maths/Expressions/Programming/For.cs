// Copyright 2012-2021 Dmytro Kyshchenko
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
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using xFunc.Maths.Analyzers;

namespace xFunc.Maths.Expressions.Programming
{
    /// <summary>
    /// Represents the "for" loop.
    /// </summary>
    public class For : DifferentParametersExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="For"/> class.
        /// </summary>
        /// <param name="body">The body of loop.</param>
        /// <param name="init">The initializer section.</param>
        /// <param name="cond">The condition section.</param>
        /// <param name="iter">The iterator section.</param>
        public For(IExpression body, IExpression init, IExpression cond, IExpression iter)
            : this(ImmutableArray.Create(body, init, cond, iter))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="For" /> class.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <exception cref="ArgumentNullException"><paramref name="arguments"/> is null.</exception>
        public For(ImmutableArray<IExpression> arguments)
            : base(arguments)
        {
        }

        /// <inheritdoc />
        public override object Execute(ExpressionParameters? parameters)
        {
            for (Initialization.Execute(parameters); (bool)Condition.Execute(parameters); Iteration.Execute(parameters))
                Body.Execute(parameters);

            return double.NaN;
        }

        /// <inheritdoc />
        protected override TResult AnalyzeInternal<TResult>(IAnalyzer<TResult> analyzer)
            => analyzer.Analyze(this);

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        protected override TResult AnalyzeInternal<TResult, TContext>(
            IAnalyzer<TResult, TContext> analyzer,
            TContext context)
            => analyzer.Analyze(this, context);

        /// <inheritdoc />
        public override IExpression Clone(ImmutableArray<IExpression>? arguments = null)
            => new For(arguments ?? Arguments);

        /// <summary>
        /// Gets the body of loop.
        /// </summary>
        public IExpression Body => this[0];

        /// <summary>
        /// Gets the initializer section.
        /// </summary>
        public IExpression Initialization => this[1];

        /// <summary>
        /// Gets the condition section.
        /// </summary>
        public IExpression Condition => this[2];

        /// <summary>
        /// Gets the iterator section.
        /// </summary>
        public IExpression Iteration => this[3];

        /// <summary>
        /// Gets the minimum count of parameters.
        /// </summary>
        public override int? MinParametersCount => 4;

        /// <summary>
        /// Gets the maximum count of parameters. <c>null</c> - Infinity.
        /// </summary>
        public override int? MaxParametersCount => 4;
    }
}