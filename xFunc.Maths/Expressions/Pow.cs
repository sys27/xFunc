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

namespace xFunc.Maths.Expressions
{

    /// <summary>
    /// Represents the Exponentiation operation.
    /// </summary>
    public class Pow : CachedBinaryExpression
    {

        [ExcludeFromCodeCoverage]
        internal Pow() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pow"/> class.
        /// </summary>
        /// <param name="base">The base.</param>
        /// <param name="exponent">The exponent.</param>
        public Pow(IExpression @base, IExpression exponent) : base(@base, exponent) { }

        /// <summary>
        /// Gets the result type.
        /// </summary>
        /// <returns>
        /// The result type of current expression.
        /// </returns>
        protected override ExpressionResultType GetResultType()
        {
            if (m_left.ResultType.HasFlagNI(ExpressionResultType.ComplexNumber))
                return ExpressionResultType.ComplexNumber;

            return ExpressionResultType.Number;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode(2273, 127);
        }

        /// <summary>
        /// Returns a specified number raised to the specified power.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A specified number raised to the specified power.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        public override object Execute(ExpressionParameters parameters)
        {
            var resultType = this.ResultType;

            var leftResult = m_left.Execute(parameters);
            var rightResult = m_right.Execute(parameters);

            if (resultType == ExpressionResultType.ComplexNumber)
                return Complex.Pow((Complex)leftResult, rightResult is Complex ? (Complex)rightResult : (double)rightResult);

            return MathExtensions.Pow((double)leftResult, (double)rightResult);
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
        /// Clones this instance of the <see cref="Pow"/> class.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IExpression"/> that is a clone of this instance.</returns>
        public override IExpression Clone()
        {
            return new Pow(m_left.Clone(), m_right.Clone());
        }

        /// <summary>
        /// Gets the type of the left parameter.
        /// </summary>
        /// <value>
        /// The type of the left parameter.
        /// </value>
        public override ExpressionResultType LeftType
        {
            get
            {
                if (m_right != null && m_right.ResultType.HasFlagNI(ExpressionResultType.ComplexNumber))
                    return ExpressionResultType.ComplexNumber;

                return ExpressionResultType.Number | ExpressionResultType.ComplexNumber;
            }
        }

        /// <summary>
        /// Gets the type of the right parameter.
        /// </summary>
        /// <value>
        /// The type of the right parameter.
        /// </value>
        public override ExpressionResultType RightType
        {
            get
            {
                if (m_left != null && m_left.ResultType.HasFlagNI(ExpressionResultType.ComplexNumber))
                    return ExpressionResultType.Number | ExpressionResultType.ComplexNumber;

                return ExpressionResultType.Number;
            }
        }

    }

}
