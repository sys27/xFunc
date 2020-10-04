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

namespace xFunc.Maths.Expressions.LogicalAndBitwise
{
    /// <summary>
    /// Represents the Equality operator.
    /// </summary>
    public class Equality : BinaryExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Equality"/> class.
        /// </summary>
        /// <param name="left">The left (first) operand.</param>
        /// <param name="right">The right (second) operand.</param>
        public Equality(IExpression left, IExpression right)
            : base(left, right)
        {
        }

        /// <inheritdoc />
        public override object Execute(ExpressionParameters? parameters)
        {
            var left = Left.Execute(parameters);
            var right = Right.Execute(parameters);

            return (left, right) switch
            {
                (bool leftBool, bool rightBool) => leftBool & rightBool | !leftBool & !rightBool,
                _ => throw new ResultIsNotSupportedException(this, left, right),
            };
        }

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
        public override IExpression Clone(IExpression? left = null, IExpression? right = null)
            => new Equality(left ?? Left, right ?? Right);
    }
}