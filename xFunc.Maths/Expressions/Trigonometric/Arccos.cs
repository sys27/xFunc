// Copyright 2012-2016 Dmitry Kischenko
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

namespace xFunc.Maths.Expressions.Trigonometric
{

    /// <summary>
    /// Represents the Arccosine function.
    /// </summary>
    [ReverseFunction(typeof(Cos))]
    public class Arccos : TrigonometricExpression
    {

        [ExcludeFromCodeCoverage]
        internal Arccos() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Arccos"/> class.
        /// </summary>
        /// <param name="expression">The argument of function.</param>
        public Arccos(IExpression expression) : base(expression) { }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode(3709);
        }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            return ToString("arccos({0})");
        }

        /// <summary>
        /// Calculates this mathematical expression (using degree).
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        protected override double ExecuteDergee(ExpressionParameters parameters)
        {
            var radian = (double)m_argument.Execute(parameters);

            return Math.Acos(radian) / Math.PI * 180;
        }

        /// <summary>
        /// Calculates this mathematical expression (using radian).
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        protected override double ExecuteRadian(ExpressionParameters parameters)
        {
            return Math.Acos((double)m_argument.Execute(parameters));
        }

        /// <summary>
        /// Calculates this mathematical expression (using gradian).
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        protected override double ExecuteGradian(ExpressionParameters parameters)
        {
            var radian = (double)m_argument.Execute(parameters);

            return Math.Acos(radian) / Math.PI * 200;
        }

        /// <summary>
        /// Calculates the this mathematical expression (complex number).
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        protected override Complex ExecuteComplex(ExpressionParameters parameters)
        {
            return Complex.Acos((Complex)m_argument.Execute(parameters));
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
            return new Arccos(m_argument.Clone());
        }

    }

}
