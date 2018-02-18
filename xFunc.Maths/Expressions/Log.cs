// Copyright 2012-2018 Dmitry Kischenko
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
    /// Represents the Logarithm function.
    /// </summary>
    public class Log : BinaryExpression
    {

        [ExcludeFromCodeCoverage]
        internal Log() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Log"/> class.
        /// </summary>
        /// <param name="arg">The left operand.</param>
        /// <param name="base">The right operand.</param>
        /// <seealso cref="IExpression"/>
        public Log(IExpression arg, IExpression @base) : base(@base, arg) { }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode(8837, 7019);
        }

        /// <summary>
        /// Executes this Log expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        public override object Execute(ExpressionParameters parameters)
        {
            var leftResult = m_left.Execute(parameters);
            if (leftResult is double leftNumber)
            {
                var rightResult = m_right.Execute(parameters);

                if (rightResult is Complex complex)
                    return Complex.Log(complex, leftNumber);

                if (rightResult is double rightNumber)
                    return Math.Log(rightNumber, leftNumber);

                throw new ResultIsNotSupportedException(this, rightResult);
            }

            throw new ResultIsNotSupportedException(this, leftResult);
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
        /// Clones this instance of the <see cref="Log"/>.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IExpression"/> that is a clone of this instance.</returns>
        public override IExpression Clone()
        {
            return new Log(m_right.Clone(), m_left.Clone());
        }

    }

}
