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

using System.Collections.Immutable;
using System.Numerics;
using xFunc.Maths.Analyzers;
using xFunc.Maths.Expressions.Angles;

namespace xFunc.Maths.Expressions.Trigonometric
{
    /// <summary>
    /// Represents the Arctangent function.
    /// </summary>
    public class Arctan : InverseTrigonometricExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Arctan"/> class.
        /// </summary>
        /// <param name="expression">The argument of function.</param>
        public Arctan(IExpression expression)
            : base(expression)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Arctan"/> class.
        /// </summary>
        /// <param name="arguments">The argument of function.</param>
        /// <seealso cref="IExpression"/>
        internal Arctan(ImmutableArray<IExpression> arguments)
            : base(arguments)
        {
        }

        /// <inheritdoc />
        protected override AngleValue ExecuteInternal(NumberValue radian)
            => AngleValue.Atan(radian);

        /// <inheritdoc />
        protected override Complex ExecuteComplex(Complex complex)
            => Complex.Atan(complex);

        /// <inheritdoc />
        protected override TResult AnalyzeInternal<TResult>(IAnalyzer<TResult> analyzer)
            => analyzer.Analyze(this);

        /// <inheritdoc />
        protected override TResult AnalyzeInternal<TResult, TContext>(
            IAnalyzer<TResult, TContext> analyzer,
            TContext context)
            => analyzer.Analyze(this, context);

        /// <inheritdoc />
        public override IExpression Clone(IExpression? argument = null)
            => new Arctan(argument ?? Argument);
    }
}