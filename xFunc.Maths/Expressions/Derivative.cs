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
using xFunc.Maths.Analyzers;

namespace xFunc.Maths.Expressions
{
    /// <summary>
    /// Represents the 'deriv' function.
    /// </summary>
    public class Derivative : DifferentParametersExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Derivative" /> class.
        /// </summary>
        /// <param name="differentiator">The differentiator.</param>
        /// <param name="simplifier">The simplifier.</param>
        /// <param name="expression">The expression.</param>
        public Derivative(
            IDifferentiator differentiator,
            ISimplifier simplifier,
            IExpression expression)
            : this(differentiator, simplifier, ImmutableArray.Create(expression))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Derivative" /> class.
        /// </summary>
        /// <param name="differentiator">The differentiator.</param>
        /// <param name="simplifier">The simplifier.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        public Derivative(
            IDifferentiator differentiator,
            ISimplifier simplifier,
            IExpression expression,
            Variable variable)
            : this(differentiator, simplifier, ImmutableArray.Create(expression, variable))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Derivative" /> class.
        /// </summary>
        /// <param name="differentiator">The differentiator.</param>
        /// <param name="simplifier">The simplifier.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <param name="point">The point of derivation.</param>
        public Derivative(
            IDifferentiator differentiator,
            ISimplifier simplifier,
            IExpression expression,
            Variable variable,
            Number point)
            : this(differentiator, simplifier, ImmutableArray.Create(expression, variable, point))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Derivative" /> class.
        /// </summary>
        /// <param name="differentiator">The differentiator.</param>
        /// <param name="simplifier">The simplifier.</param>
        /// <param name="args">The arguments.</param>
        /// <exception cref="ArgumentNullException"><paramref name="args"/> is null.</exception>
        internal Derivative(
            IDifferentiator differentiator,
            ISimplifier simplifier,
            ImmutableArray<IExpression> args)
            : base(args)
        {
            Differentiator = differentiator ??
                             throw new ArgumentNullException(nameof(differentiator));
            Simplifier = simplifier ??
                         throw new ArgumentNullException(nameof(simplifier));
        }

        /// <inheritdoc />
        public override object Execute(ExpressionParameters? parameters)
        {
            var variable = Variable;
            var context = new DifferentiatorContext(parameters, variable);
            var diff = Analyze(Differentiator, context);

            var point = DerivativePoint;
            if (point != null)
            {
                parameters ??= new ExpressionParameters();
                parameters.Variables[variable.Name] = point.Value;

                return diff.Execute(parameters);
            }

            return diff.Analyze(Simplifier);
        }

        /// <inheritdoc />
        private protected override TResult AnalyzeInternal<TResult>(IAnalyzer<TResult> analyzer)
            => analyzer.Analyze(this);

        /// <inheritdoc />
        private protected override TResult AnalyzeInternal<TResult, TContext>(
            IAnalyzer<TResult, TContext> analyzer,
            TContext context)
            => analyzer.Analyze(this, context);

        /// <inheritdoc />
        public override IExpression Clone(ImmutableArray<IExpression>? arguments = null)
            => new Derivative(Differentiator, Simplifier, arguments ?? Arguments);

        /// <summary>
        /// Gets the expression.
        /// </summary>
        public IExpression Expression => this[0];

        /// <summary>
        /// Gets the variable.
        /// </summary>
        public Variable Variable => ParametersCount >= 2 ? (Variable)this[1] : Variable.X;

        /// <summary>
        /// Gets the derivative point.
        /// </summary>
        public Number? DerivativePoint => ParametersCount >= 3 ? (Number)this[2] : null;

        /// <summary>
        /// Gets the simplifier.
        /// </summary>
        public ISimplifier Simplifier { get; }

        /// <summary>
        /// Gets the differentiator.
        /// </summary>
        public IDifferentiator Differentiator { get; }

        /// <summary>
        /// Gets the minimum count of parameters.
        /// </summary>
        public override int? MinParametersCount => 1;

        /// <summary>
        /// Gets the maximum count of parameters. <c>null</c> - Infinity.
        /// </summary>
        public override int? MaxParametersCount => 3;
    }
}