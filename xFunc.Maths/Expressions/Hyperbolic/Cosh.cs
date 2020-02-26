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
using System.Numerics;
using xFunc.Maths.Analyzers;

namespace xFunc.Maths.Expressions.Hyperbolic
{
    /// <summary>
    /// Represents the Hyperbolic cosine function.
    /// </summary>
    public class Cosh : HyperbolicExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Cosh"/> class.
        /// </summary>
        /// <param name="expression">The argument of function.</param>
        public Cosh(IExpression expression)
            : base(expression)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cosh"/> class.
        /// </summary>
        /// <param name="arguments">The argument of function.</param>
        /// <seealso cref="IExpression"/>
        internal Cosh(IExpression[] arguments)
            : base(arguments)
        {
        }

        /// <summary>
        /// Executes this expression.
        /// </summary>
        /// <param name="complex">The calculation result of argument.</param>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        protected override Complex ExecuteComplex(Complex complex) =>
            Complex.Cosh(complex);

        /// <summary>
        /// Calculates this mathematical expression (using degree).
        /// </summary>
        /// <param name="degree">The calculation result of argument.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        protected override double ExecuteDegree(double degree) =>
            Math.Cosh(degree * Math.PI / 180);

        /// <summary>
        /// Calculates this mathematical expression (using radian).
        /// </summary>
        /// <param name="radian">The calculation result of argument.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        protected override double ExecuteRadian(double radian) =>
            Math.Cosh(radian);

        /// <summary>
        /// Calculates this mathematical expression (using gradian).
        /// </summary>
        /// <param name="gradian">The calculation result of argument.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        protected override double ExecuteGradian(double gradian) =>
            Math.Cosh(gradian * Math.PI / 200);

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
        /// <returns>The new instance of <see cref="IExpression"/> that is a clone of this instance.</returns>
        public override IExpression Clone() =>
            new Cosh(Argument.Clone());
    }
}