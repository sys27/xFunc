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
    /// Represents an Addition operation.
    /// </summary>
    public class Add : BinaryExpression
    {

        [ExcludeFromCodeCoverage]
        internal Add() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Add"/> class.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <seealso cref="IExpression"/>
        public Add(IExpression left, IExpression right) : base(left, right) { }

        /// <summary>
        /// Gets the result type.
        /// </summary>
        /// <returns>
        /// The result type of current expression.
        /// </returns>
        protected override ResultType GetResultType()
        {
            if ((m_left.ResultType.HasFlagNI(ResultType.ComplexNumber) && m_left.ResultType != ResultType.All) ||
                (m_right.ResultType.HasFlagNI(ResultType.ComplexNumber) && m_right.ResultType != ResultType.All))
                return ResultType.ComplexNumber;

            if (m_left.ResultType == ResultType.Number || m_right.ResultType == ResultType.Number)
                return ResultType.Number;

            if (m_left.ResultType == ResultType.Matrix || m_right.ResultType == ResultType.Matrix)
                return ResultType.Matrix;

            if (m_left.ResultType == ResultType.Vector || m_right.ResultType == ResultType.Vector)
                return ResultType.Vector;

            if (m_left.ResultType.HasFlagNI(ResultType.Number) || m_right.ResultType.HasFlagNI(ResultType.Number))
                return ResultType.Number;

            return ResultType.Number | ResultType.ComplexNumber | ResultType.Vector | ResultType.Matrix;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode(6203, 6883);
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
            var leftResult = m_left.Execute(parameters);
            var rightResult = m_right.Execute(parameters);

            if (leftResult is Complex || rightResult is Complex)
            {
                var leftComplex = leftResult as Complex? ?? leftResult as double?;
                var rightComplex = rightResult as Complex? ?? rightResult as double?;
                if (leftComplex == null || rightComplex == null)
                    throw new ResultIsNotSupportedException(this, leftResult, rightResult);

                return Complex.Add(leftComplex.Value, rightComplex.Value);
            }

            if (leftResult is Matrix leftMatrix && rightResult is Matrix rightMatrix)
                return leftMatrix.Add(rightMatrix, parameters);

            if (leftResult is Vector leftVector && rightResult is Vector rightVector)
                return leftVector.Add(rightVector, parameters);

            if (leftResult is double leftDouble && rightResult is double rightDouble)
                return leftDouble + rightDouble;

            throw new ResultIsNotSupportedException(this, leftResult, rightResult);
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
        /// Clones this instance of the <see cref="Add"/>.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IExpression"/> that is a clone of this instance.</returns>
        public override IExpression Clone()
        {
            return new Add(m_left.Clone(), m_right.Clone());
        }

        /// <summary>
        /// Gets the type of the left parameter.
        /// </summary>
        /// <value>
        /// The type of the left parameter.
        /// </value>
        public override ResultType LeftType
        {
            get
            {
                if (m_right != null)
                {
                    if (m_right.ResultType.HasFlagNI(ResultType.ComplexNumber) || m_right.ResultType.HasFlagNI(ResultType.Number))
                        return ResultType.Number | ResultType.ComplexNumber;

                    if (m_right.ResultType == ResultType.Matrix)
                        return ResultType.Matrix;

                    if (m_right.ResultType == ResultType.Vector)
                        return ResultType.Vector;
                }

                return ResultType.Number | ResultType.ComplexNumber | ResultType.Vector | ResultType.Matrix;
            }
        }

        /// <summary>
        /// Gets the type of the right parameter.
        /// </summary>
        /// <value>
        /// The type of the right parameter.
        /// </value>
        public override ResultType RightType
        {
            get
            {
                if (m_left != null)
                {
                    if (m_left.ResultType.HasFlagNI(ResultType.ComplexNumber) || m_left.ResultType.HasFlagNI(ResultType.Number))
                        return ResultType.Number | ResultType.ComplexNumber;

                    if (m_left.ResultType == ResultType.Matrix)
                        return ResultType.Matrix;

                    if (m_left.ResultType == ResultType.Vector)
                        return ResultType.Vector;
                }

                return ResultType.Number | ResultType.ComplexNumber | ResultType.Vector | ResultType.Matrix;
            }
        }

    }

}
