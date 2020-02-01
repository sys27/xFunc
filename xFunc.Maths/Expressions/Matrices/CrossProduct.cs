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

using xFunc.Maths.Analyzers;

namespace xFunc.Maths.Expressions.Matrices
{
    /// <summary>
    /// Represents a cross product of vectors.
    /// </summary>
    public class CrossProduct : BinaryExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CrossProduct"/> class.
        /// </summary>
        /// <param name="left">The left (first) operand.</param>
        /// <param name="right">The right (second) operand.</param>
        public CrossProduct(IExpression left, IExpression right) : base(left, right)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CrossProduct"/> class.
        /// </summary>
        /// <param name="arguments">The list of arguments.</param>
        /// <seealso cref="IExpression"/>
        public CrossProduct(IExpression[] arguments) : base(arguments)
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
            return base.GetHashCode(9391, 91621);
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
            var left = this.m_left.Execute(parameters);
            var right = this.m_right.Execute(parameters);
            if (left is Vector leftVector && right is Vector rightVector)
                return leftVector.Cross(rightVector, parameters);

            throw new ResultIsNotSupportedException(this, new[] { left, right });
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
        /// <returns>
        /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
        /// </returns>
        public override IExpression Clone()
        {
            return new CrossProduct(m_left.Clone(), m_right.Clone());
        }
    }
}