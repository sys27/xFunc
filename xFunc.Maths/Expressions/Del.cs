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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using xFunc.Maths.Analyzers;
using xFunc.Maths.Expressions.Matrices;
using static xFunc.Maths.ThrowHelpers;

namespace xFunc.Maths.Expressions
{
    /// <summary>
    /// Represents the "Del" operator.
    /// </summary>
    /// <seealso cref="UnaryExpression" />
    public class Del : UnaryExpression
    {
        private readonly IDifferentiator differentiator;
        private readonly ISimplifier simplifier;

        /// <summary>
        /// Initializes a new instance of the <see cref="Del"/> class.
        /// </summary>
        /// <param name="differentiator">The differentiator.</param>
        /// <param name="simplifier">The simplifier.</param>
        /// <param name="expression">The expression.</param>
        public Del(IDifferentiator differentiator, ISimplifier simplifier, IExpression expression)
            : base(expression)
        {
            if (differentiator is null)
                ArgNull(ExceptionArgument.differentiator);
            if (simplifier is null)
                ArgNull(ExceptionArgument.simplifier);

            this.differentiator = differentiator;
            this.simplifier = simplifier;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Del"/> class.
        /// </summary>
        /// <param name="differentiator">The differentiator.</param>
        /// <param name="simplifier">The simplifier.</param>
        /// <param name="arguments">The argument of function.</param>
        /// <seealso cref="IExpression"/>
        internal Del(
            IDifferentiator differentiator,
            ISimplifier simplifier,
            ImmutableArray<IExpression> arguments)
            : base(arguments)
        {
            this.differentiator = differentiator;
            this.simplifier = simplifier;
        }

        /// <inheritdoc />
        public override object Execute(ExpressionParameters? parameters)
        {
            var context = new DifferentiatorContext(parameters);

            var variables = Helpers.GetAllVariables(Argument).ToList();
            var vector = ImmutableArray.CreateBuilder<IExpression>(variables.Count);

            foreach (var variable in variables)
            {
                context.Variable = variable;

                vector.Add(Argument
                    .Analyze(differentiator, context)
                    .Analyze(simplifier));
            }

            return new Vector(vector.ToImmutableArray());
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
        public override IExpression Clone(IExpression? argument = null)
            => new Del(differentiator, simplifier, argument ?? Argument);
    }
}