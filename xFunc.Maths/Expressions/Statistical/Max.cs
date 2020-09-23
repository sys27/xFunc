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

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using xFunc.Maths.Analyzers;

namespace xFunc.Maths.Expressions.Statistical
{
    /// <summary>
    /// Represent the Max function.
    /// </summary>
    /// <seealso cref="DifferentParametersExpression" />
    public class Max : StatisticalExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Max"/> class.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        public Max(IEnumerable<IExpression> arguments)
            : base(arguments)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Max"/> class.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        public Max(ImmutableArray<IExpression> arguments)
            : base(arguments)
        {
        }

        /// <summary>
        /// Executes this expression.
        /// </summary>
        /// <param name="numbers">The array of expressions.</param>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        private protected override double ExecuteInternal(double[] numbers) => numbers.Max();

        /// <summary>
        /// Analyzes the current expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="analyzer">The analyzer.</param>
        /// <returns>
        /// The analysis result.
        /// </returns>
        private protected override TResult AnalyzeInternal<TResult>(IAnalyzer<TResult> analyzer)
            => analyzer.Analyze(this);

        /// <summary>
        /// Analyzes the current expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="TContext">The type of additional parameter for analyzer.</typeparam>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="context">The context.</param>
        /// <returns>The analysis result.</returns>
        [ExcludeFromCodeCoverage]
        private protected override TResult AnalyzeInternal<TResult, TContext>(
            IAnalyzer<TResult, TContext> analyzer,
            TContext context)
            => analyzer.Analyze(this, context);

        /// <summary>
        /// Clones this instance of the <see cref="IExpression" />.
        /// </summary>
        /// <param name="arguments">The list of arguments.</param>
        /// <returns>
        /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
        /// </returns>
        public override IExpression Clone(ImmutableArray<IExpression>? arguments = null)
            => new Max(arguments ?? Arguments);
    }
}