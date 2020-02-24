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
using xFunc.Maths.Expressions.Matrices;
using Vector = xFunc.Maths.Expressions.Matrices.Vector;

namespace xFunc.Maths.Expressions
{
    /// <summary>
    /// Represents the Multiplication operator.
    /// </summary>
    public class Mul : BinaryExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Mul"/> class.
        /// </summary>
        /// <param name="left">The first (left) operand.</param>
        /// <param name="right">The second (right) operand.</param>
        public Mul(IExpression left, IExpression right)
            : base(left, right)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mul"/> class.
        /// </summary>
        /// <param name="arguments">The list of arguments.</param>
        /// <seealso cref="IExpression"/>
        internal Mul(IExpression[] arguments)
            : base(arguments)
        {
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return GetHashCode(7537, 1973);
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
            var leftResult = Left.Execute(parameters);
            var rightResult = Right.Execute(parameters);

            if (leftResult is double leftNumber && rightResult is double rightNumber)
                return leftNumber * rightNumber;

            if (leftResult is Complex || rightResult is Complex)
            {
                var leftComplex = leftResult as Complex? ?? leftResult as double?;
                var rightComplex = rightResult as Complex? ?? rightResult as double?;
                if (leftComplex == null || rightComplex == null)
                    throw new ResultIsNotSupportedException(this, leftResult, rightResult);

                return Complex.Multiply(leftComplex.Value, rightComplex.Value);
            }

            if (leftResult is Matrix || rightResult is Matrix || leftResult is Vector || rightResult is Vector)
            {
                var leftExpResult = leftResult as IExpression ?? new Number((double)leftResult);
                var rightExpResult = rightResult as IExpression ?? new Number((double)rightResult);

                if (leftExpResult is Vector leftVector1 && rightExpResult is Vector rightVector1)
                    return leftVector1.Mul(rightVector1, parameters);

                if (leftExpResult is Vector leftVector2)
                {
                    if (rightExpResult is Matrix rightMaxtir1)
                        return leftVector2.Mul(rightMaxtir1, parameters);

                    return leftVector2.Mul(rightExpResult, parameters);
                }

                if (rightExpResult is Vector rightVector2)
                {
                    if (leftExpResult is Matrix leftMatrix1)
                        return rightVector2.Mul(leftMatrix1, parameters);

                    return rightVector2.Mul(leftExpResult, parameters);
                }

                if (leftExpResult is Matrix leftMatrix2 && rightExpResult is Matrix rightMatrix2)
                    return leftMatrix2.Mul(rightMatrix2, parameters);
                if (leftExpResult is Matrix leftMatrix3)
                    return leftMatrix3.Mul(rightExpResult, parameters);
                if (rightExpResult is Matrix rightMatrix3)
                    return rightMatrix3.Mul(leftExpResult, parameters);
            }

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
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IExpression"/> that is a clone of this instance.</returns>
        public override IExpression Clone()
        {
            return new Mul(Left.Clone(), Right.Clone());
        }
    }
}