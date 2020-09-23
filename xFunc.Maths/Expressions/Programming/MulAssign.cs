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

using System.Diagnostics.CodeAnalysis;
using xFunc.Maths.Analyzers;

namespace xFunc.Maths.Expressions.Programming
{
    /// <summary>
    /// Represents the "*=" operator.
    /// </summary>
    public class MulAssign : VariableBinaryExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MulAssign"/> class.
        /// </summary>
        /// <param name="variable">The variable.</param>
        /// <param name="exp">The expression.</param>
        public MulAssign(Variable variable, IExpression exp)
            : base(variable, exp)
        {
        }

        /// <summary>
        /// Executes this expression.
        /// </summary>
        /// <param name="variableValue">The value of variable.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        protected override object Execute(double variableValue, double value)
            => variableValue * value;

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
        [ExcludeFromCodeCoverage]
        private protected override TResult AnalyzeInternal<TResult, TContext>(
            IAnalyzer<TResult, TContext> analyzer,
            TContext context)
            => analyzer.Analyze(this, context);

        /// <summary>
        /// Clones this instance of the <see cref="IExpression" />.
        /// </summary>
        /// <param name="variable">The variable argument of new expression.</param>
        /// <param name="value">The value argument of new expression.</param>
        /// <returns>
        /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
        /// </returns>
        public override IExpression Clone(Variable? variable = null, IExpression? value = null)
            => new MulAssign(variable ?? Variable, value ?? Value);
    }
}