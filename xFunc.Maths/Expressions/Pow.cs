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

namespace xFunc.Maths.Expressions
{

    /// <summary>
    /// Represents the Exponentiation operation.
    /// </summary>
    public class Pow : BinaryExpression
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
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            if (m_parent is BinaryExpression && !(m_parent is Add || m_parent is Sub || m_parent is Mul))
                return ToString("({0} ^ {1})");

            return ToString("{0} ^ {1}");
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
            var leftResult = m_left.Execute(parameters);
            var rightResult = m_right.Execute(parameters);

            if (ResultType == ExpressionResultType.ComplexNumber)
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

        /// <summary>
        /// Gets the type of the result.
        /// </summary>
        /// <value>
        /// The type of the result.
        /// </value>
        public override ExpressionResultType ResultType
        {
            get
            {
                if (m_left.ResultType.HasFlagNI(ExpressionResultType.ComplexNumber))
                    return ExpressionResultType.ComplexNumber;

                return ExpressionResultType.Number;
            }
        }

    }

}
