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

using System.Collections.Immutable;
using System.Numerics;
using xFunc.Maths.Analyzers;

namespace xFunc.Maths.Expressions.Trigonometric
{
    /// <summary>
    /// Represents the Arcsecant function.
    /// </summary>
    public class Arcsec : InverseTrigonometricExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Arcsec"/> class.
        /// </summary>
        /// <param name="expression">The argument of function.</param>
        public Arcsec(IExpression expression)
            : base(expression)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Arcsec"/> class.
        /// </summary>
        /// <param name="arguments">The argument of function.</param>
        /// <seealso cref="IExpression"/>
        internal Arcsec(ImmutableArray<IExpression> arguments)
            : base(arguments)
        {
        }

        /// <inheritdoc />
        protected override double ExecuteInternal(double radian) =>
            MathExtensions.Asec(radian);

        /// <inheritdoc />
        protected override Complex ExecuteComplex(Complex complex) =>
            ComplexExtensions.Asec(complex);

        /// <inheritdoc />
        private protected override TResult AnalyzeInternal<TResult>(IAnalyzer<TResult> analyzer)
            => analyzer.Analyze(this);

        /// <inheritdoc />
        private protected override TResult AnalyzeInternal<TResult, TContext>(
            IAnalyzer<TResult, TContext> analyzer,
            TContext context)
            => analyzer.Analyze(this, context);

        /// <inheritdoc />
        public override IExpression Clone(IExpression? argument = null)
            => new Arcsec(argument ?? Argument);
    }
}