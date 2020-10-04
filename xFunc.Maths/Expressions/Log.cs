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

namespace xFunc.Maths.Expressions
{
    /// <summary>
    /// Represents the Logarithm function.
    /// </summary>
    public class Log : BinaryExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Log"/> class.
        /// </summary>
        /// <param name="base">The right operand.</param>
        /// <param name="arg">The left operand.</param>
        /// <seealso cref="IExpression"/>
        public Log(IExpression @base, IExpression arg)
            : base(@base, arg)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Log"/> class.
        /// </summary>
        /// <param name="arguments">The list of arguments.</param>
        /// <seealso cref="IExpression"/>
        public Log(ImmutableArray<IExpression> arguments)
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
                (NumberValue left, NumberValue right) => NumberValue.Log(right, left),
                (NumberValue left, Complex complex) => (object)NumberValue.Log(complex, left),
                _ => throw new ResultIsNotSupportedException(this, leftResult, rightResult),
            };
        }

        /// <inheritdoc />
        private protected override TResult AnalyzeInternal<TResult>(IAnalyzer<TResult> analyzer)
            => analyzer.Analyze(this);

        /// <inheritdoc />
        private protected override TResult AnalyzeInternal<TResult, TContext>(
            IAnalyzer<TResult, TContext> analyzer,
            TContext context)
            => analyzer.Analyze(this, context);

        /// <inheritdoc />
        public override IExpression Clone(IExpression? left = null, IExpression? right = null)
            => new Log(left ?? Left, right ?? Right);
    }
}