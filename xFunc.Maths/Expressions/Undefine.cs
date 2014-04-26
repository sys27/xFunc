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

namespace xFunc.Maths.Expressions
{

    /// <summary>
    /// Represents the Undefice operation.
    /// </summary>
    public class Undefine : IExpression
    {

        private IExpression key;

        internal Undefine() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Undefine"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public Undefine(IExpression key)
        {
            this.Key = key;
        }

        /// <summary>
        /// Determines whether the specified <see cref="Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;

            var undef = obj as Undefine;
            if (undef == null)
                return false;

            return key.Equals(undef.key);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return key.GetHashCode() ^ 2143;
        }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            return string.Format("undef({0})", key);
        }

        /// <summary>
        /// Calculates this mathemarical expression. Don't use this method if your expression has variables or functions.
        /// </summary>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <exception cref="NotSupportedException">Always.</exception>
        public object Calculate()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Calculates this mathemarical expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        /// <exception cref="ArgumentNullException"><paramref name="parameters"/> is null.</exception>
        public object Calculate(ExpressionParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException("parameters");

            if (key is Variable)
            {
                var e = key as Variable;

                parameters.Parameters.Remove(e.Name);
            }
            else if (key is UserFunction)
            {
                var e = key as UserFunction;

                parameters.Functions.Remove(e);
            }

            return double.NaN;
        }

        /// <summary>
        /// Always throws <see cref="NotSupportedException"/>.
        /// </summary>
        /// <returns>
        /// Throws exception.
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
        /// Throws exception.
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
            return new Undefine(key.Clone());
        }

        /// <summary>
        /// Get or Set the parent expression.
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
        /// Gets the minimum count of parameters. -1 - Infinity.
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

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        /// <exception cref="NotSupportedException"><paramref name="value"/> is not a <see cref="Variable"/> or a <see cref="UserFunction"/>.</exception>
        public IExpression Key
        {
            get
            {
                return key;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                if (!(value is Variable || value is UserFunction))
                    throw new NotSupportedException();

                key = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether result is a matrix.
        /// </summary>
        /// <value>
        ///   <c>true</c> if result is a matrix; otherwise, <c>false</c>.
        /// </value>
        public bool ResultIsMatrix
        {
            get
            {
                return false;
            }
        }

    }

}
