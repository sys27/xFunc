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
    /// Represents the Define operation.
    /// </summary>
    public class Define : IMathExpression
    {

        private IMathExpression key;
        private IMathExpression value;

        /// <summary>
        /// Initializes a new instance of <see cref="Define"/>.
        /// </summary>
        internal Define() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Define"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public Define(IMathExpression key, IMathExpression value)
        {
            this.Key = key;
            this.value = value;
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

            var def = obj as Define;
            if (def == null)
                return false;

            return key.Equals(def.key) && value.Equals(def.value);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            int hash = 4583;

            hash = (hash * 233) + key.GetHashCode();
            hash = (hash * 233) + value.GetHashCode();

            return hash;
        }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            return string.Format("{0} := {1}", key, value);
        }

        /// <summary>
        /// Throws <see cref="System.NotSupportedException"/>
        /// </summary>
        /// <returns>
        /// The exception.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public double Calculate()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Throws <see cref="System.NotSupportedException" />
        /// </summary>
        /// <param name="parameters">A collection of variables that are used in the expression.</param>
        /// <returns>
        /// The exception.
        /// </returns>
        /// <seealso cref="MathParameterCollection" />
        /// <exception cref="System.ArgumentNullException"><paramref name="parameters"/> is null.</exception>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public double Calculate(MathParameterCollection parameters)
        {
            if (key is Variable)
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters");

                var e = key as Variable;

                parameters[e.Name] = value.Calculate(parameters);
            }
            else
            {
                throw new NotSupportedException();
            }

            return double.NaN;
        }

        /// <summary>
        /// Throws <see cref="System.NotSupportedException" />
        /// </summary>
        /// <param name="parameters">A collection of variables that are used in the expression.</param>
        /// <param name="functions">A collection of functions that are used in the expression.</param>
        /// <returns>
        /// The exception.
        /// </returns>
        /// <seealso cref="MathParameterCollection" />
        /// <exception cref="System.ArgumentNullException"><paramref name="parameters" /> or <paramref name="functions"/> is null.</exception>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public double Calculate(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            if (key is Variable)
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters");

                var e = key as Variable;

                parameters[e.Name] = value.Calculate(parameters);
            }
            else if (key is UserFunction)
            {
                if (functions == null)
                    throw new ArgumentNullException("functions");

                var e = key as UserFunction;

                functions[e] = value;
            }

            return double.NaN;
        }

        /// <summary>
        /// Always throws <see cref="NotSupportedException" />.
        /// </summary>
        /// <returns>
        /// Throws an exception.
        /// </returns>
        /// <seealso cref="Variable" />
        /// <exception cref="NotSupportedException">Always.</exception>
        public IMathExpression Differentiate()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Always throws <see cref="NotSupportedException" />.
        /// </summary>
        /// <param name="variable"></param>
        /// <returns>
        /// Throws an exception.
        /// </returns>
        /// <seealso cref="Variable" />
        /// <exception cref="NotSupportedException">Always.</exception>
        public IMathExpression Differentiate(Variable variable)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Clones this instance of the <see cref="Define"/>.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IMathExpression"/> that is a clone of this instance.</returns>
        public IMathExpression Clone()
        {
            return new Define(key.Clone(), value.Clone());
        }

        /// <summary>
        /// Get or Set the parent expression.
        /// </summary>
        public IMathExpression Parent
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
        /// Gets the minimum count of parameters.
        /// </summary>
        /// <value>
        /// The minimum count of parameters.
        /// </value>
        public int MinCountOfParams
        {
            get
            {
                return 2;
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
                return 2;
            }
        }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        /// <exception cref="NotSupportedException"><paramref name="value"/> is not a <see cref="Variable"/> or a <see cref="UserFunction"/>.</exception>
        public IMathExpression Key
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
                {
                    throw new NotSupportedException();
                }

                key = value;
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public IMathExpression Value
        {
            get
            {
                return value;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                this.value = value;
            }
        }

    }

}
