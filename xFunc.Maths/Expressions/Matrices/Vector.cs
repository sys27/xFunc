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

namespace xFunc.Maths.Expressions.Matrices
{
    /// <summary>
    /// Represents a vector.
    /// </summary>
    public class Vector : DifferentParametersExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector"/> class.
        /// </summary>
        /// <param name="args">The values of vector.</param>
        /// <exception cref="ArgumentNullException"><paramref name="args"/> is null.</exception>
        public Vector(IExpression[] args)
            : base(args)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector"/> class.
        /// </summary>
        /// <param name="args">The values of vector.</param>
        /// <exception cref="ArgumentNullException"><paramref name="args"/> is null.</exception>
        public Vector(ImmutableArray<IExpression> args)
            : base(args)
        {
        }

        /// <inheritdoc />
        public override object Execute(ExpressionParameters? parameters)
        {
            var args = ImmutableArray.CreateBuilder<IExpression>(ParametersCount);

            for (var i = 0; i < ParametersCount; i++)
            {
                if (this[i] is Number)
                {
                    args.Add(this[i]);
                }
                else
                {
                    var result = this[i].Execute(parameters);
                    if (result is NumberValue number)
                        args.Add(new Number(number));
                    else
                        throw new ResultIsNotSupportedException(this, result);
                }
            }

            return new Vector(args.ToImmutableArray());
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
            => new Vector(arguments ?? Arguments);

        /// <summary>
        /// Calculates current vector and returns it as an array.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>The array which represents current vector.</returns>
        internal NumberValue[] ToCalculatedArray(ExpressionParameters? parameters)
        {
            var results = new NumberValue[ParametersCount];

            for (var i = 0; i < ParametersCount; i++)
                results[i] = (NumberValue)this[i].Execute(parameters);

            return results;
        }

        /// <summary>
        /// Gets the minimum count of parameters.
        /// </summary>
        public override int? MinParametersCount => 1;

        /// <summary>
        /// Gets the maximum count of parameters. <c>null</c> - Infinity.
        /// </summary>
        public override int? MaxParametersCount => null;
    }
}