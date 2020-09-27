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
using xFunc.Maths.Resources;

namespace xFunc.Maths.Expressions
{
    /// <summary>
    /// Represents the "round" function.
    /// </summary>
    public class Round : DifferentParametersExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Round"/> class.
        /// </summary>
        /// <param name="argument">The expression that represents a double-precision floating-point number to be rounded.</param>
        public Round(IExpression argument)
            : this(ImmutableArray.Create(argument))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Round"/> class.
        /// </summary>
        /// <param name="argument">The expression that represents a double-precision floating-point number to be rounded.</param>
        /// <param name="digits">The expression that represents the number of fractional digits in the return value.</param>
        public Round(IExpression argument, IExpression digits)
            : this(ImmutableArray.Create(argument, digits))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Round"/> class.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <exception cref="ArgumentNullException"><paramref name="args"/> is null.</exception>
        internal Round(ImmutableArray<IExpression> args)
            : base(args)
        {
        }

        /// <inheritdoc />
        public override object Execute(ExpressionParameters? parameters)
        {
            var result = Argument.Execute(parameters);
            var digits = Digits?.Execute(parameters) ?? 0.0;
            if (result is double arg && digits is double digitsDouble)
            {
                if (!digitsDouble.IsInt())
                    throw new InvalidOperationException(Resource.ValueIsNotInteger);

                return Math.Round(arg, (int)digitsDouble, MidpointRounding.AwayFromZero);
            }

            throw new ResultIsNotSupportedException(this, result);
        }

        /// <inheritdoc />
        private protected override TResult AnalyzeInternal<TResult>(IAnalyzer<TResult> analyzer)
            => analyzer.Analyze(this);

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        private protected override TResult AnalyzeInternal<TResult, TContext>(
            IAnalyzer<TResult, TContext> analyzer,
            TContext context)
            => analyzer.Analyze(this, context);

        /// <inheritdoc />
        public override IExpression Clone(ImmutableArray<IExpression>? arguments = null)
            => new Round(arguments ?? Arguments);

        /// <summary>
        /// Gets the expression that represents a double-precision floating-point number to be rounded.
        /// </summary>
        public IExpression Argument => this[0];

        /// <summary>
        /// Gets the expression that represents the number of fractional digits in the return value.
        /// </summary>
        public IExpression? Digits => ParametersCount == 2 ? this[1] : null;

        /// <summary>
        /// Gets the minimum count of parameters.
        /// </summary>
        public override int? MinParametersCount => 1;

        /// <summary>
        /// Gets the maximum count of parameters. <c>null</c> - Infinity.
        /// </summary>
        public override int? MaxParametersCount => 2;
    }
}