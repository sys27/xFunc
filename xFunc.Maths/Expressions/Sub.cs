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
using System.Numerics;
using xFunc.Maths.Analyzers;
using xFunc.Maths.Expressions.Angles;
using xFunc.Maths.Expressions.Matrices;
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