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
using xFunc.Maths.Expressions.Units.AngleUnits;

namespace xFunc.Maths.Expressions.Trigonometric
{
    /// <summary>
    /// Represents the Sine function.
    /// </summary>
    public class Sin : TrigonometricExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sin"/> class.
        /// </summary>
        /// <param name="expression">The argument of function.</param>
        public Sin(IExpression expression)
            : base(expression)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sin"/> class.
        /// </summary>
        /// <param name="arguments">The argument of function.</param>
        /// <seealso cref="IExpression"/>
        internal Sin(ImmutableArray<IExpression> arguments)
            : base(arguments)
        {
        }

        /// <inheritdoc />
        /// <seealso cref="ExpressionParameters" />
        protected override NumberValue ExecuteInternal(AngleValue angleValue)
            => AngleValue.Sin(angleValue);

        /// <inheritdoc />
        protected override Complex ExecuteComplex(Complex complex)
            => Complex.Sin(complex);

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
            => new Sin(argument ?? Argument);
    }
}