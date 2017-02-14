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
using xFunc.Maths.Expressions.Matrices;

namespace xFunc.Maths.Expressions
{

    /// <summary>
    /// Represents the Multiplication operation.
    /// </summary>
    public class Mul : BinaryExpression
    {

        private bool isChanged = false;
        private ExpressionResultType? resultType;

        [ExcludeFromCodeCoverage]
        internal Mul() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mul"/> class.
        /// </summary>
        /// <param name="left">The first (left) operand.</param>
        /// <param name="right">The second (right) operand.</param>
        public Mul(IExpression left, IExpression right) : base(left, right) { }

        private ExpressionResultType GetResultType()
        {
            if ((m_left.ResultType.HasFlagNI(ExpressionResultType.ComplexNumber) && m_left.ResultType != ExpressionResultType.All) ||
                (m_right.ResultType.HasFlagNI(ExpressionResultType.ComplexNumber) && m_right.ResultType != ExpressionResultType.All))
                return ExpressionResultType.ComplexNumber;

            if (m_left.ResultType.HasFlagNI(ExpressionResultType.Number) && m_right.ResultType.HasFlagNI(ExpressionResultType.Number))
                return ExpressionResultType.Number;

            if (m_left.ResultType == ExpressionResultType.Matrix || m_right.ResultType == ExpressionResultType.Matrix)
                return ExpressionResultType.Matrix;

            if (m_right.ResultType == ExpressionResultType.Vector || m_left.ResultType == ExpressionResultType.Vector)
                return ExpressionResultType.Vector;

            return ExpressionResultType.Number | ExpressionResultType.ComplexNumber | ExpressionResultType.Vector | ExpressionResultType.Matrix;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode(7537, 1973);
        }

        /// <summary>
        /// Executes this expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        /// <exception cref="System.NotSupportedException">The multiplication of two vectors is not allowed.</exception>
        /// <seealso cref="ExpressionParameters" />
        public override object Execute(ExpressionParameters parameters)
        {
            var resultType = this.ResultType;
            if (resultType == ExpressionResultType.Matrix || resultType == ExpressionResultType.Vector)
            {
                var temp = m_left.Execute(parameters);
                var leftExpResult = temp is IExpression ? (IExpression)temp : new Number((double)temp);

                temp = m_right.Execute(parameters);
                var rightExpResult = temp is IExpression ? (IExpression)temp : new Number((double)temp);

                if (leftExpResult is Vector && rightExpResult is Vector)
                    return ((Vector)leftExpResult).Cross((Vector)rightExpResult, parameters);

                if (leftExpResult is Vector)
                {
                    if (rightExpResult is Matrix)
                        return ((Vector)leftExpResult).Mul((Matrix)rightExpResult, parameters);

                    return ((Vector)leftExpResult).Mul(rightExpResult, parameters);
                }
                if (rightExpResult is Vector)
                {
                    if (leftExpResult is Matrix)
                        return ((Matrix)leftExpResult).Mul((Vector)rightExpResult, parameters);

                    return ((Vector)rightExpResult).Mul(leftExpResult, parameters);
                }

                if (leftExpResult is Matrix && rightExpResult is Matrix)
                    return ((Matrix)leftExpResult).Mul((Matrix)rightExpResult, parameters);
                if (leftExpResult is Matrix)
                    return ((Matrix)leftExpResult).Mul(rightExpResult, parameters);
                if (rightExpResult is Matrix)
                    return ((Matrix)rightExpResult).Mul(leftExpResult, parameters);
            }

            var leftResult = m_left.Execute(parameters);
            var rightResult = m_right.Execute(parameters);

            if (resultType == ExpressionResultType.ComplexNumber)
            {
                var leftComplex = leftResult is Complex ? (Complex)leftResult : (double)leftResult;
                var rightComplex = rightResult is Complex ? (Complex)rightResult : (double)rightResult;

                return Complex.Multiply(leftComplex, rightComplex);
            }

            return (double)leftResult * (double)rightResult;
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
        /// <returns>Returns the new instance of <see cref="IExpression"/> that is a clone of this instance.</returns>
        public override IExpression Clone()
        {
            return new Mul(m_left.Clone(), m_right.Clone());
        }

        /// <summary>
        /// The left (first) operand.
        /// </summary>
        public override IExpression Left
        {
            get
            {
                return base.Left;
            }
            set
            {
                base.Left = value;
                isChanged = true;
            }
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
                if (m_right != null && m_right.ResultType == ExpressionResultType.Vector)
                    return ExpressionResultType.Number | ExpressionResultType.Vector | ExpressionResultType.Matrix;

                return ExpressionResultType.Number | ExpressionResultType.ComplexNumber | ExpressionResultType.Vector | ExpressionResultType.Matrix;
            }
        }

        /// <summary>
        /// The right (second) operand.
        /// </summary>
        public override IExpression Right
        {
            get
            {
                return base.Right;
            }
            set
            {
                base.Right = value;
                isChanged = true;
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
                if (m_left != null && m_left.ResultType == ExpressionResultType.Vector)
                    return ExpressionResultType.Number | ExpressionResultType.Vector | ExpressionResultType.Matrix;

                return ExpressionResultType.Number | ExpressionResultType.ComplexNumber | ExpressionResultType.Vector | ExpressionResultType.Matrix;
            }
        }

        /// <summary>
        /// Gets the type of the result.
        /// </summary>
        /// <value>
        /// The type of the result.
        /// </value>
        /// <remarks>
        /// Usage of this property can affect performance. Don't use this property each time if you need to check result type of current expression. Just store/cache value only once and use it everywhere.
        /// </remarks>
        public override ExpressionResultType ResultType
        {
            get
            {
                if (this.resultType == null || isChanged)
                {
                    resultType = GetResultType();
                    isChanged = false;
                }

                return resultType.Value;
            }
        }

    }

}
