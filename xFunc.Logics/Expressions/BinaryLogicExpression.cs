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
    /// The base class for binary operations.
    /// </summary>
    public abstract class BinaryLogicExpression : ILogicExpression
    {

        /// <summary>
        /// The left (first) operand.
        /// </summary>
        protected ILogicExpression firstOperand;
        /// <summary>
        /// The right (second) operand.
        /// </summary>
        protected ILogicExpression secondOperand;

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryLogicExpression"/> class.
        /// </summary>
        /// <param name="firstOperand">The first (left) operand.</param>
        /// <param name="secondOperand">The second (right) operand.</param>
        protected BinaryLogicExpression(ILogicExpression firstOperand, ILogicExpression secondOperand)
        {
            this.firstOperand = firstOperand;
            this.secondOperand = secondOperand;
        }

        /// <summary>
        /// Returns a <see cref="String" /> that represents this instance.
        /// </summary>
        /// <param name="operand">The operand.</param>
        /// <returns>A <see cref="String" /> that represents this instance.</returns>
        protected string ToString(string operand)
        {
            string first;
            string second;

            if (firstOperand is Variable || firstOperand is Const)
                first = firstOperand.ToString();
            else
                first = "(" + firstOperand + ")";

            if (secondOperand is Variable || secondOperand is Const)
                second = secondOperand.ToString();
            else
                second = "(" + secondOperand + ")";

            return first + " " + operand + " " + second;
        }

        /// <summary>
        /// Calculates this logical expression.
        /// </summary>
        /// <param name="parameters">A collection of variables that are used in the expression.</param>
        /// <returns>A result of the calculation.</returns>
        /// <seealso cref="LogicParameterCollection" />
        public abstract bool Calculate(LogicParameterCollection parameters);

        /// <summary>
        /// Gets or sets the first (left) operand.
        /// </summary>
        /// <value>The first (left) operand.</value>
        public ILogicExpression FirstOperand
        {
            get
            {
                return firstOperand;
            }
            set
            {
                firstOperand = value;
            }
        }

        /// <summary>
        /// Gets or sets the second (right) operand.
        /// </summary>
        /// <value>The second (right) operand.</value>
        public ILogicExpression SecondOperand
        {
            get
            {
                return secondOperand;
            }
            set
            {
                secondOperand = value;
            }
        }

    }

}
