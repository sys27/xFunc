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
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using xFunc.Maths.Analyzers;

namespace xFunc.Maths.Expressions
{
    /// <summary>
    /// Represents a least common multiple.
    /// </summary>
    public class LCM : DifferentParametersExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LCM"/> class.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <exception cref="ArgumentNullException"><paramref name="args"/> is null.</exception>
        public LCM(IExpression[] args)
            : base(args)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LCM"/> class.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <exception cref="ArgumentNullException"><paramref name="args"/> is null.</exception>
        public LCM(ImmutableArray<IExpression> args)
            : base(args)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LCM"/> class.
        /// </summary>
        /// <param name="first">The first operand.</param>
        /// <param name="second">The second operand.</param>
        public LCM(IExpression first, IExpression second)
            : this(ImmutableArray.Create(first, second))
        {
        }

        /// <inheritdoc />
        public override object Execute(ExpressionParameters? parameters)
        {
            var lcm = new NumberValue(1.0);
            foreach (var argument in Arguments)
            {
                var result = argument.Execute(parameters);

                lcm = result switch
                {
                    NumberValue number => NumberValue.LCM(lcm, number),
                    _ => throw new ResultIsNotSupportedException(this, result),
                };
            }

            return lcm;
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
            => new LCM(arguments ?? Arguments);

        /// <summary>
        /// Gets the minimum count of parameters.
        /// </summary>
        public override int? MinParametersCount => 2;

        /// <summary>
        /// Gets the maximum count of parameters. <c>null</c> - Infinity.
        /// </summary>
        public override int? MaxParametersCount => null;
    }
}