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
using System.Numerics;

namespace xFunc.Maths.Expressions.Hyperbolic
{

    /// <summary>
    /// The base class for hyperbolic functions.
    /// </summary>
    /// <seealso cref="xFunc.Maths.Expressions.UnaryExpression" />
    public abstract class HyperbolicExpression : UnaryExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="HyperbolicExpression"/> class.
        /// </summary>
        protected HyperbolicExpression() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HyperbolicExpression" /> class.
        /// </summary>
        /// <param name="argument">The expression.</param>
        protected HyperbolicExpression(IExpression argument) : base(argument) { }

        /// <summary>
        /// Gets the result type.
        /// </summary>
        /// <returns>
        /// The result type of current expression.
        /// </returns>
        protected override ResultType GetResultType()
        {
            return m_argument.ResultType;
        }

        /// <summary>
        /// Executes this expression.
        /// </summary>
        /// <param name="complex">The calculation result of argument.</param>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        protected abstract Complex ExecuteComplex(Complex complex);

        /// <summary>
        /// Executes this expression.
        /// </summary>
        /// <param name="number">The calculation result of argument.</param>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        protected abstract double ExecuteNumber(double number);

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
            var result = m_argument.Execute(parameters);
            if (result is Complex complex)
                return ExecuteComplex(complex);
            if (result is double number)
                return ExecuteNumber(number);

            throw new ResultIsNotSupportedException(this, result);
        }

        /// <summary>
        /// Gets the type of the argument.
        /// </summary>
        /// <value>
        /// The type of the argument.
        /// </value>
        public override ResultType ArgumentType => ResultType.Number | ResultType.ComplexNumber;

    }

}
