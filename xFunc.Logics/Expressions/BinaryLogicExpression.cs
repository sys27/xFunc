// Copyright 2012-2015 Dmitry Kischenko
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
        protected ILogicExpression left;
        /// <summary>
        /// The right (second) operand.
        /// </summary>
        protected ILogicExpression right;

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryLogicExpression"/> class.
        /// </summary>
        /// <param name="left">The first (left) operand.</param>
        /// <param name="right">The second (right) operand.</param>
        protected BinaryLogicExpression(ILogicExpression left, ILogicExpression right)
        {
            this.left = left;
            this.right = right;
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

            if (left is Variable || left is Const)
                first = left.ToString();
            else
                first = "(" + left + ")";

            if (right is Variable || right is Const)
                second = right.ToString();
            else
                second = "(" + right + ")";

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
        public ILogicExpression Left
        {
            get
            {
                return left;
            }
            set
            {
                left = value;
            }
        }

        /// <summary>
        /// Gets or sets the second (right) operand.
        /// </summary>
        /// <value>The second (right) operand.</value>
        public ILogicExpression Right
        {
            get
            {
                return right;
            }
            set
            {
                right = value;
            }
        }

    }

}
