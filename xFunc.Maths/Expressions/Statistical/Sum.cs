// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using xFunc.Maths.Analyzers;

namespace xFunc.Maths.Expressions.Statistical
{
    /// <summary>
    /// Represents the "sum" function.
    /// </summary>
    public class Sum : StatisticalExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sum"/> class.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <exception cref="ArgumentNullException"><paramref name="arguments"/> is null.</exception>
        public Sum(IEnumerable<IExpression> arguments)
            : base(arguments)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sum"/> class.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <exception cref="ArgumentNullException"><paramref name="arguments"/> is null.</exception>
        public Sum(ImmutableArray<IExpression> arguments)
            : base(arguments)
        {
        }

        /// <inheritdoc />
        protected override double ExecuteInternal(double[] numbers)
            => numbers.Sum();

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
            => new Sum(arguments ?? Arguments);
    }
}