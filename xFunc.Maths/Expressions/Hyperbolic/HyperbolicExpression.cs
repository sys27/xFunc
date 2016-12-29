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
        protected HyperbolicExpression(IExpression argument)
            : base(argument)
        {
        }

        /// <summary>
        /// Gets the type of the argument.
        /// </summary>
        /// <value>
        /// The type of the argument.
        /// </value>
        public override ExpressionResultType ArgumentType { get; } = ExpressionResultType.Number | ExpressionResultType.ComplexNumber;

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
                return m_argument.ResultType;
            }
        }

    }

}
