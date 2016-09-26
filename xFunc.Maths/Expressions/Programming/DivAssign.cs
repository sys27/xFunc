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

namespace xFunc.Maths.Expressions.Programming
{

    /// <summary>
    /// Represents the "/=" operation.
    /// </summary>
    public class DivAssign : BinaryExpression
    {

        internal DivAssign() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddAssign"/> class.
        /// </summary>
        /// <param name="variable">The variable.</param>
        /// <param name="exp">The expression.</param>
        public DivAssign(IExpression variable, IExpression exp)
            : base(variable, exp) { }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return ToString("{0} /= {1}");
        }

        /// <summary>
        /// Executes this expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        public override object Execute(ExpressionParameters parameters)
        {
            var var = (Variable)m_left;
            var parameter = var.Execute(parameters);
            if (parameter is bool)
                throw new NotSupportedException();

            var newValue = Convert.ToDouble(parameter) / (double)m_right.Execute(parameters);
            parameters.Variables[var.Name] = newValue;

            return newValue;
        }

        /// <summary>
        /// Creates the clone of this instance.
        /// </summary>
        /// <returns>
        /// Returns the new instance of <see cref="DivAssign" /> that is a clone of this instance.
        /// </returns>
        public override IExpression Clone()
        {
            return new DivAssign(m_left.Clone(), m_right.Clone());
        }

        /// <summary>
        /// The left (first) operand.
        /// </summary>
        public override IExpression Left
        {
            get
            {
                return m_left;
            }
            set
            {
                if (!(value is Variable))
                    throw new NotSupportedException();

                m_left = value;
                m_left.Parent = this;
            }
        }

    }

}
