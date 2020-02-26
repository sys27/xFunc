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

using System.Numerics;
using xFunc.Maths.Analyzers;

namespace xFunc.Maths.Expressions
{
    /// <summary>
    /// Represents the nth root operator.
    /// </summary>
    public class Root : BinaryExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Root"/> class.
        /// </summary>
        /// <param name="radicand">The radicand.</param>
        /// <param name="degree">The degree.</param>
        public Root(IExpression radicand, IExpression degree)
            : base(radicand, degree)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Root"/> class.
        /// </summary>
        /// <param name="arguments">The list of arguments.</param>
        /// <seealso cref="IExpression"/>
        internal Root(IExpression[] arguments)
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
        public override object Execute(ExpressionParameters parameters)
        {
            var leftResult = Left.Execute(parameters);
            var rightResult = Right.Execute(parameters);

            return (leftResult, rightResult) switch
            {
                (double first, double second) when first < 0 && (second % 2 == 0) =>
                    new Complex(0, Complex.Pow(first, 1 / second).Imaginary),

                (double first, double second) => MathExtensions.Pow(first, 1 / second),

                _ => throw new ResultIsNotSupportedException(this, leftResult, rightResult),
            };
        }

        /// <summary>
        /// Analyzes the current expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="analyzer">The analyzer.</param>
        /// <returns>
        /// The analysis result.
        /// </returns>
        private protected override TResult AnalyzeInternal<TResult>(IAnalyzer<TResult> analyzer) =>
            analyzer.Analyze(this);

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IExpression"/> that is a clone of this instance.</returns>
        public override IExpression Clone() =>
            new Root(Left.Clone(), Right.Clone());
    }
}