// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Numerics;
using xFunc.Maths.Analyzers;
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions.Units.AngleUnits;
using xFunc.Maths.Expressions.Units.PowerUnits;
using Vector = xFunc.Maths.Expressions.Matrices.Vector;

namespace xFunc.Maths.Expressions
{
    /// <summary>
    /// Represents the Subtraction operator.
    /// </summary>
    public class Sub : BinaryExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sub"/> class.
        /// </summary>
        /// <param name="left">The minuend.</param>
        /// <param name="right">The subtrahend.</param>
        public Sub(IExpression left, IExpression right)
            : base(left, right)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sub"/> class.
        /// </summary>
        /// <param name="arguments">The list of arguments.</param>
        internal Sub(ImmutableArray<IExpression> arguments)
            : base(arguments)
        {
        }

        /// <inheritdoc />
        public override object Execute(ExpressionParameters? parameters)
        {
            var leftResult = Left.Execute(parameters);
            var rightResult = Right.Execute(parameters);

            return (leftResult, rightResult) switch
            {
                (NumberValue left, NumberValue right) => left - right,

                (NumberValue left, AngleValue right) => left - right,
                (AngleValue left, NumberValue right) => left - right,
                (AngleValue left, AngleValue right) => left - right,

                (NumberValue left, PowerValue right) => left - right,
                (PowerValue left, NumberValue right) => left - right,
                (PowerValue left, PowerValue right) => left - right,

                (NumberValue left, Complex right) => left - right,
                (Complex left, NumberValue right) => left - right,
                (Complex left, Complex right) => left - right,

                (Vector left, Vector right) => left.Sub(right, parameters),
                (Matrix left, Matrix right) => left.Sub(right, parameters),

                _ => throw new ResultIsNotSupportedException(this, leftResult, rightResult),
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
        public override IExpression Clone(IExpression? left = null, IExpression? right = null)
            => new Sub(left ?? Left, right ?? Right);
    }
}