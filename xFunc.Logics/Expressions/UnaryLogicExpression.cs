// Copyright 2012-2013 Dmitry Kischenko
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

namespace xFunc.Logics.Expressions
{

    /// <summary>
    /// The abstract base class that represents the unary operation.
    /// </summary>
    public abstract class UnaryLogicExpression : ILogicExpression
    {

        /// <summary>
        /// The (first) operand.
        /// </summary>
        protected ILogicExpression argument;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnaryLogicExpression"/> class.
        /// </summary>
        /// <param name="argument">The expression.</param>
        protected UnaryLogicExpression(ILogicExpression argument)
        {
            this.argument = argument;
        }

        /// <summary>
        /// Returns a <see cref="String" /> that represents this instance.
        /// </summary>
        /// <param name="operand">The operand.</param>
        /// <returns>A <see cref="String" /> that represents this instance.</returns>
        protected string ToString(string operand)
        {
            if (argument is Variable)
                return string.Format("{0}{1}", operand, argument);

            return string.Format("{0}({1})", operand, argument);
        }

        /// <summary>
        /// Calculates this logical expression.
        /// </summary>
        /// <param name="parameters">A collection of variables that are used in the expression.</param>
        /// <returns>A result of the calculation.</returns>
        /// <seealso cref="LogicParameterCollection" />
        public abstract bool Calculate(LogicParameterCollection parameters);

        /// <summary>
        /// Gets or sets the expression.
        /// </summary>
        /// <value>The expression.</value>
        public ILogicExpression Argument
        {
            get
            {
                return argument;
            }
            set
            {
                argument = value;
            }
        }

    }

}
