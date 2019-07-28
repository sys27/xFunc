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

namespace xFunc.Maths.Expressions
{

    /// <summary>
    /// Represents the Modulo operator.
    /// </summary>
    /// <seealso cref="xFunc.Maths.Expressions.BinaryExpression" />
    public class Mod : BinaryExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Mod"/> class.
        /// </summary>
        /// <param name="left">The left (first) operand.</param>
        /// <param name="right">The right (second) operand.</param>
        public Mod(IExpression left, IExpression right) : base(left, right) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mod"/> class.
        /// </summary>
        /// <param name="arguments">The tuple of arguments.</param>
        /// <seealso cref="IExpression"/>
        public Mod((IExpression left, IExpression right) arguments) : base(arguments.left, arguments.right) { }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode(32909, 52541);
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

            if (leftResult is double leftNumber && rightResult is double rightNumber)
                return leftNumber % rightNumber;

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
        /// Creates the clone of this instance.
        /// </summary>
        /// <returns>
        /// Returns the new instance of <see cref="BinaryExpression" /> that is a clone of this instance.
        /// </returns>
        public override IExpression Clone()
        {
            return new Mod(m_left.Clone(), m_right.Clone());
        }

    }

}
