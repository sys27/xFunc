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
            var leftResult = Left.Execute(parameters);
            var rightResult = Right.Execute(parameters);

            return (leftResult, rightResult) switch
            {
                (double left, double rightDouble) => left - rightDouble,

                (double left, AngleValue right) => left - right,
                (AngleValue left, double right) => left - right,
                (AngleValue left, AngleValue right) => left - right,

                (double left, Complex rightComplex) => left - rightComplex,
                (Complex left, double rightDouble) => left - rightDouble,
                (Complex left, Complex rightComplex) => left - rightComplex,

                (Vector left, Vector right) => left.Sub(right, parameters),
                (Matrix left, Matrix right) => left.Sub(right, parameters),

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
        /// Clones this instance of the <see cref="IExpression" />.
        /// </summary>
        /// <param name="left">The left argument of new expression.</param>
        /// <param name="right">The right argument of new expression.</param>
        /// <returns>
        /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
        /// </returns>
        public override IExpression Clone(IExpression? left = null, IExpression? right = null)
            => new Sub(left ?? Left, right ?? Right);
    }
}