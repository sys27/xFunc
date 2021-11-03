// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using xFunc.Maths.Analyzers;

namespace xFunc.Maths.Expressions.Programming
{
    /// <summary>
    /// Represents the "+=" operator.
    /// </summary>
    public class AddAssign : VariableBinaryExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddAssign"/> class.
        /// </summary>
        /// <param name="variable">The variable.</param>
        /// <param name="exp">The expression.</param>
        public AddAssign(Variable variable, IExpression exp)
            : base(variable, exp)
        {
        }

        /// <inheritdoc />
        protected override object Execute(NumberValue variableValue, NumberValue value)
            => variableValue + value;

        /// <inheritdoc />
        protected override TResult AnalyzeInternal<TResult>(IAnalyzer<TResult> analyzer)
            => analyzer.Analyze(this);

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        protected override TResult AnalyzeInternal<TResult, TContext>(
            IAnalyzer<TResult, TContext> analyzer,
            TContext context)
            => analyzer.Analyze(this, context);

        /// <inheritdoc />
        public override IExpression Clone(Variable? variable = null, IExpression? value = null)
            => new AddAssign(variable ?? Variable, value ?? Value);
    }
}