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
using static xFunc.Maths.ThrowHelpers;

namespace xFunc.Maths.Expressions
{
    /// <summary>
    /// Represents the Simplify function.
    /// </summary>
    public class Simplify : UnaryExpression
    {
        private readonly ISimplifier simplifier;

        /// <summary>
        /// Initializes a new instance of the <see cref="Simplify"/> class.
        /// </summary>
        /// <param name="simplifier">The simplifier.</param>
        /// <param name="expression">The argument of function.</param>
        public Simplify(ISimplifier simplifier, IExpression expression)
            : base(expression)
        {
            if (simplifier is null)
                ArgNull(ExceptionArgument.simplifier);

            this.simplifier = simplifier;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Simplify"/> class.
        /// </summary>
        /// <param name="simplifier">The simplifier.</param>
        /// <param name="arguments">The argument of function.</param>
        /// <seealso cref="IExpression"/>
        internal Simplify(ISimplifier simplifier, ImmutableArray<IExpression> arguments)
            : base(arguments)
        {
            this.simplifier = simplifier;
        }

        /// <summary>
        /// Executes this expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        /// <exception cref="ArgumentNullException">Simplifier is null.</exception>
        /// <seealso cref="ExpressionParameters" />
        public override object Execute(ExpressionParameters? parameters)
            => Analyze(simplifier);

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
            => new Simplify(simplifier, argument ?? Argument);
    }
}