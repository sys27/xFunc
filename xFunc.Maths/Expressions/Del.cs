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
using System.Linq;
using xFunc.Maths.Analyzers;
using xFunc.Maths.Expressions.Matrices;

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
            this.differentiator = differentiator ??
                                  throw new ArgumentNullException(nameof(differentiator));
            this.simplifier = simplifier ??
                              throw new ArgumentNullException(nameof(simplifier));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Del"/> class.
        /// </summary>
        /// <param name="differentiator">The differentiator.</param>
        /// <param name="simplifier">The simplifier.</param>
        /// <param name="arguments">The argument of function.</param>
        /// <seealso cref="IExpression"/>
        internal Del(IDifferentiator differentiator, ISimplifier simplifier, IExpression[] arguments)
            : base(arguments)
        {
            this.differentiator = differentiator ?? throw new ArgumentNullException(nameof(differentiator));
            this.simplifier = simplifier ?? throw new ArgumentNullException(nameof(simplifier));
        }

        /// <summary>
        /// Executes this expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        /// <exception cref="ArgumentNullException">The differentiator is null.</exception>
        /// <seealso cref="ExpressionParameters" />
        public override object Execute(ExpressionParameters parameters)
        {
            var variables = Helpers.GetAllVariables(Argument).ToList();
            var vector = new Vector(variables.Count);

            differentiator.Parameters = parameters;

            for (var i = 0; i < variables.Count; i++)
            {
                differentiator.Variable = variables[i];

                vector[i] = Argument.Analyze(differentiator).Analyze(simplifier);
            }

            return vector;
        }

        /// <summary>
        /// Analyzes the current expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="analyzer">The analyzer.</param>
        /// <returns>
        /// The analysis result.
        /// </returns>
        private protected override TResult AnalyzeInternal<TResult>(IAnalyzer<TResult> analyzer)
        {
            return analyzer.Analyze(this);
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
        /// </returns>
        public override IExpression Clone()
        {
            return new Del(differentiator, simplifier, Argument.Clone());
        }
    }
}