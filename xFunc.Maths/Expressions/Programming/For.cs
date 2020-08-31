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
using System.Collections.Generic;
using xFunc.Maths.Analyzers;

namespace xFunc.Maths.Expressions.Programming
{
    /// <summary>
    /// Represents the "for" loop.
    /// </summary>
    public class For : DifferentParametersExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="For"/> class.
        /// </summary>
        /// <param name="body">The body of loop.</param>
        /// <param name="init">The initializer section.</param>
        /// <param name="cond">The condition section.</param>
        /// <param name="iter">The iterator section.</param>
        public For(IExpression body, IExpression init, IExpression cond, IExpression iter)
            : this(new[] { body, init, cond, iter })
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="For" /> class.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <exception cref="ArgumentNullException"><paramref name="arguments"/> is null.</exception>
        public For(IList<IExpression> arguments)
            : base(arguments)
        {
        }

        /// <summary>
        /// Executes this expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        public override object Execute(ExpressionParameters? parameters)
        {
            for (Initialization.Execute(parameters); (bool)Condition.Execute(parameters); Iteration.Execute(parameters))
                Body.Execute(parameters);

            return double.NaN;
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
        /// Clones this instance of the <see cref="For" />.
        /// </summary>
        /// <returns>
        /// Returns the new instance of <see cref="For" /> that is a clone of this instance.
        /// </returns>
        public override IExpression Clone() =>
            new For(CloneArguments());

        /// <summary>
        /// Gets the body of loop.
        /// </summary>
        /// <value>
        /// The body of loop.
        /// </value>
        public IExpression Body => this[0];

        /// <summary>
        /// Gets the initializer section.
        /// </summary>
        /// <value>
        /// The initializer section.
        /// </value>
        public IExpression Initialization => this[1];

        /// <summary>
        /// Gets the condition section.
        /// </summary>
        /// <value>
        /// The condition section.
        /// </value>
        public IExpression Condition => this[2];

        /// <summary>
        /// Gets the iterator section.
        /// </summary>
        /// <value>
        /// The iterator section.
        /// </value>
        public IExpression Iteration => this[3];

        /// <summary>
        /// Gets the minimum count of parameters.
        /// </summary>
        /// <value>
        /// The minimum count of parameters.
        /// </value>
        public override int? MinParametersCount => 4;

        /// <summary>
        /// Gets the maximum count of parameters. <c>null</c> - Infinity.
        /// </summary>
        /// <value>
        /// The maximum count of parameters.
        /// </value>
        public override int? MaxParametersCount => 4;
    }
}