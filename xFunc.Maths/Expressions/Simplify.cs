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
using xFunc.Maths.Analyzers;

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
            this.simplifier = simplifier ?? throw new ArgumentNullException(nameof(simplifier));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Simplify"/> class.
        /// </summary>
        /// <param name="simplifier">The simplifier.</param>
        /// <param name="arguments">The argument of function.</param>
        /// <seealso cref="IExpression"/>
        internal Simplify(ISimplifier simplifier, IExpression[] arguments)
            : base(arguments)
        {
            this.simplifier = simplifier ?? throw new ArgumentNullException(nameof(simplifier));
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
        public override object Execute(ExpressionParameters parameters)
        {
            return this.Analyze(simplifier);
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
        /// <returns>Returns the new instance of <see cref="IExpression"/> that is a clone of this instance.</returns>
        public override IExpression Clone()
        {
            return new Simplify(simplifier, Argument.Clone());
        }
    }
}