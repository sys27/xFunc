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

namespace xFunc.Maths.Expressions.Matrices
{

    /// <summary>
    /// Represents the Inverse function.
    /// </summary>
    public class Inverse : UnaryExpression
    {

        internal Inverse() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Inverse"/> class.
        /// </summary>
        /// <param name="argument">A matrix.</param>
        public Inverse(IExpression argument)
            : base(argument)
        {

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
            return MatrixExtentions.Inverse((Matrix)m_argument.Calculate(parameters), parameters);
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
        /// </returns>
        public override IExpression Clone()
        {
            return new Inverse(this.m_argument.Clone());
        }
        
        /// <summary>
        /// Gets or sets the expression.
        /// </summary>
        /// <value>
        /// The expression.
        /// </value>
        /// <exception cref="System.NotSupportedException">Argument is not <see cref="Matrix"/>.</exception>
        public override IExpression Argument
        {
            get
            {
                return m_argument;
            }
            set
            {
                if (!value.ResultIsMatrix)
                    throw new NotSupportedException();

                m_argument = value;
                m_argument.Parent = this;
            }
        }

        /// <summary>
        /// Gets a value indicating whether result is a matrix.
        /// </summary>
        /// <value>
        ///   <c>true</c> if result is a matrix; otherwise, <c>false</c>.
        /// </value>
        public override bool ResultIsMatrix
        {
            get
            {
                return true;
            }
        }

    }

}
