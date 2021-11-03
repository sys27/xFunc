// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Numerics;
using xFunc.Maths.Analyzers;

namespace xFunc.Maths.Expressions
{
    /// <summary>
    /// Represents the Natural logarithm function.
    /// </summary>
    public class Ln : UnaryExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Ln"/> class.
        /// </summary>
        /// <param name="expression">The argument of function.</param>
        /// <seealso cref="IExpression"/>
        public Ln(IExpression expression)
            : base(expression)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ln"/> class.
        /// </summary>
        /// <param name="arguments">The argument of function.</param>
        /// <seealso cref="IExpression"/>
        internal Ln(ImmutableArray<IExpression> arguments)
            : base(arguments)
        {
        }

        /// <inheritdoc />
        public override object Execute(ExpressionParameters? parameters)
        {
            var result = Argument.Execute(parameters);

            return result switch
            {
                NumberValue number => NumberValue.Ln(number),
                Complex complex => Complex.Log(complex),
                _ => throw new ResultIsNotSupportedException(this, result),
            };
        }

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
            => new Ln(argument ?? Argument);
    }
}