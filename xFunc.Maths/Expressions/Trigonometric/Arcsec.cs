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

namespace xFunc.Maths.Expressions.Trigonometric
{
    /// <summary>
    /// Represents the Arcsecant function.
    /// </summary>
    public class Arcsec : TrigonometricExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Arcsec"/> class.
        /// </summary>
        /// <param name="expression">The argument of function.</param>
        public Arcsec(IExpression expression)
            : base(expression)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Arcsec"/> class.
        /// </summary>
        /// <param name="arguments">The argument of function.</param>
        /// <seealso cref="IExpression"/>
        internal Arcsec(IExpression[] arguments)
            : base(arguments)
        {
        }

        /// <summary>
        /// Calculates this mathematical expression (using degree).
        /// </summary>
        /// <param name="degree">The calculation result of argument.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        protected override double ExecuteDergee(double degree)
        {
            return MathExtensions.Asec(degree) / Math.PI * 180;
        }

        /// <summary>
        /// Calculates this mathematical expression (using radian).
        /// </summary>
        /// <param name="radian">The calculation result of argument.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        protected override double ExecuteRadian(double radian)
        {
            return MathExtensions.Asec(radian);
        }

        /// <summary>
        /// Calculates this mathematical expression (using gradian).
        /// </summary>
        /// <param name="gradian">The calculation result of argument.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        protected override double ExecuteGradian(double gradian)
        {
            return MathExtensions.Asec(gradian) / Math.PI * 200;
        }

        /// <summary>
        /// Calculates the this mathematical expression (complex number).
        /// </summary>
        /// <param name="complex">The calculation result of argument.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        protected override Complex ExecuteComplex(Complex complex)
        {
            return ComplexExtensions.Asec(complex);
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
        {
            return analyzer.Analyze(this);
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>The new instance of <see cref="IExpression"/> that is a clone of this instance.</returns>
        public override IExpression Clone()
        {
            return new Arcsec(Argument.Clone());
        }
    }
}