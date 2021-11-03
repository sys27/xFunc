// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using xFunc.Maths.Analyzers;

namespace xFunc.Maths.Expressions.Statistical
{
    /// <summary>
    /// Represent the Avg function.
    /// </summary>
    /// <seealso cref="DifferentParametersExpression" />
    public class Avg : StatisticalExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Avg"/> class.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        public Avg(IEnumerable<IExpression> arguments)
            : base(arguments)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Avg"/> class.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        public Avg(ImmutableArray<IExpression> arguments)
            : base(arguments)
        {
        }

        /// <inheritdoc />
        protected override double ExecuteInternal(double[] numbers)
            => numbers.Average();

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
            => new Avg(arguments ?? Arguments);
    }
}