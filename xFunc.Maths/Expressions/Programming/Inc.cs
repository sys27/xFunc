// Copyright 2012-2014 Dmitry Kischenko
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

namespace xFunc.Maths.Expressions.Programming
{

    /// <summary>
    /// Represents the increment operator.
    /// </summary>
    public class Inc : UnaryExpression
    {

        internal Inc() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Inc"/> class.
        /// </summary>
        /// <param name="argument">The expression.</param>
        public Inc(IExpression argument)
            : base(argument) { }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return ToString("{0}++");
        }

        /// <summary>
        /// Calculates this mathemarical expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        public override object Calculate(ExpressionParameters parameters)
        {
            var var = (Variable)argument;
            var newValue = parameters.Parameters[var.Name] + 1;
            parameters.Parameters[var.Name] = newValue;

            return newValue;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// Returns the new instance of <see cref="Inc" /> that is a clone of this instance.
        /// </returns>
        public override IExpression Clone()
        {
            return new Inc(argument.Clone());
        }
        
        /// <summary>
        /// Gets or sets the expression.
        /// </summary>
        /// <value>The expression.</value>
        public override IExpression Argument
        {
            get
            {
                return argument;
            }
            set
            {
                if (!(value is Variable))
                    throw new NotSupportedException();

                argument = value;
                argument.Parent = this;
            }
        }

    }

}
