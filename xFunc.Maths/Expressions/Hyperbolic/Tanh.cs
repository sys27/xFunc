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
using System.Numerics;
using xFunc.Maths.Analyzers;
using xFunc.Maths.Expressions.Angles;

namespace xFunc.Maths.Expressions.Hyperbolic
{
    /// <summary>
    /// Represents the Hyperbolic tangent function.
    /// </summary>
    public class Tanh : HyperbolicExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Tanh"/> class.
        /// </summary>
        /// <param name="expression">The argument of function.</param>
        public Tanh(IExpression expression)
            : base(expression)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tanh"/> class.
        /// </summary>
        /// <param name="arguments">The argument of function.</param>
        /// <seealso cref="IExpression"/>
        internal Tanh(ImmutableArray<IExpression> arguments)
            : base(arguments)
        {
        }

        /// <summary>
        /// Executes this expression.
        /// </summary>
        /// <param name="complex">The complex.</param>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        protected override Complex ExecuteComplex(Complex complex) =>
            Complex.Tanh(complex);

        /// <summary>
        /// Calculates this mathematical expression (using radian).
        /// </summary>
        /// <param name="angleValue">The angle.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        protected override double ExecuteInternal(AngleValue angleValue) =>
            Math.Tanh(angleValue.Value);

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
        private protected override TResult AnalyzeInternal<TResult, TContext>(
            IAnalyzer<TResult, TContext> analyzer,
            TContext context)
            => analyzer.Analyze(this, context);

        /// <summary>
        /// Clones this instance of the <see cref="IExpression" />.
        /// </summary>
        /// <param name="argument">The argument of new expression.</param>
        /// <returns>
        /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
        /// </returns>
        public override IExpression Clone(IExpression? argument = null)
            => new Tanh(argument ?? Argument);
    }
}