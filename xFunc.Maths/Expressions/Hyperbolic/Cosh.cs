// Copyright 2012-2017 Dmitry Kischenko
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
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using xFunc.Maths.Analyzers;

namespace xFunc.Maths.Expressions.Hyperbolic
{

    /// <summary>
    /// Represents the Hyperbolic cosine function.
    /// </summary>
    [ReverseFunction(typeof(Arcosh))]
    public class Cosh : HyperbolicExpression
    {

        [ExcludeFromCodeCoverage]
        internal Cosh() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cosh"/> class.
        /// </summary>
        /// <param name="expression">The argument of function.</param>
        public Cosh(IExpression expression) : base(expression) { }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode(4799);
        }

        /// <summary>
        /// Executes this expression.
        /// </summary>
        /// <param name="complex">The calculation result of argument.</param>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        protected override Complex ExecuteComplex(Complex complex)
        {
            return Complex.Cosh(complex);
        }

        /// <summary>
        /// Executes this expression.
        /// </summary>
        /// <param name="number">The calculation result of argument.</param>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        protected override double ExecuteNumber(double number)
        {
            return Math.Cosh(number);
        }

        /// <summary>
        /// Analyzes the current expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="analyzer">The analyzer.</param>
        /// <returns>
        /// The analysis result.
        /// </returns>
        public override TResult Analyze<TResult>(IAnalyzer<TResult> analyzer)
        {
            return analyzer.Analyze(this);
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>The new instance of <see cref="IExpression"/> that is a clone of this instance.</returns>
        public override IExpression Clone()
        {
            return new Cosh(m_argument.Clone());
        }

    }

}
