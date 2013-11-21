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

namespace xFunc.Maths.Expressions
{

    /// <summary>
    /// Represents the Simplify operation.
    /// </summary>
    public class Simplify  : IExpression
    {

        private IExpression expression;

        /// <summary>
        /// Initializes a new instance of the <see cref="Simplify"/> class.
        /// </summary>
        internal Simplify() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Simplify"/> class.
        /// </summary>
        /// <param name="expression">The argument of function.</param>
        public Simplify(IExpression expression)
        {
            this.expression = expression;
        }

        /// <summary>
        /// Determines whether the specified <see cref="Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="Object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            var simp = obj as Simplify;
            if (simp != null && expression.Equals(simp.expression))
                return true;

            return false;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return expression.GetHashCode() ^ 457;
        }

        /// <summary>
        /// Returns a <see cref="String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return string.Format("simplify({0})", expression);
        }

        /// <summary>
        /// Throws <see cref="NotSupportedException"/>.
        /// </summary>
        /// <returns>
        /// The exception.
        /// </returns>
        /// <exception cref="NotSupportedException">Always.</exception>
        public double Calculate()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Throws <see cref="NotSupportedException" />.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// The exception.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        /// <exception cref="NotSupportedException">Always.</exception>
        public double Calculate(ExpressionParameters parameters)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Always throws <see cref="NotSupportedException"/>.
        /// </summary>
        /// <returns>
        /// Throws an exception.
        /// </returns>
        /// <exception cref="NotSupportedException">Always.</exception>
        public IExpression Differentiate()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Always throws <see cref="NotSupportedException" />.
        /// </summary>
        /// <param name="variable">The variable of differentiation.</param>
        /// <returns>
        /// Throws an exception.
        /// </returns>
        /// <seealso cref="Variable" />
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public IExpression Differentiate(Variable variable)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IExpression"/> that is a clone of this instance.</returns>
        public IExpression Clone()
        {
            return new Simplify(expression.Clone());
        }

        /// <summary>
        /// This property always returns <c>null</c>.
        /// </summary>
        public IExpression Parent
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        /// <summary>
        /// Gets or sets the expression.
        /// </summary>
        /// <value>
        /// The expression.
        /// </value>
        public IExpression Expression
        {
            get
            {
                return expression;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                expression = value;
            }
        }

        /// <summary>
        /// Gets the minimum count of parameters.
        /// </summary>
        /// <value>
        /// The minimum count of parameters.
        /// </value>
        public int MinCountOfParams
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// Gets the maximum count of parameters. -1 - Infinity.
        /// </summary>
        /// <value>
        /// The maximum count of parameters.
        /// </value>
        public int MaxCountOfParams
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// Gets the count of parameters.
        /// </summary>
        /// <value>
        /// The count of parameters.
        /// </value>
        public int CountOfParams
        {
            get
            {
                return 1;
            }
        }

    }

}
