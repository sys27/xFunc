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
using System.Text;

namespace xFunc.Maths.Expressions
{

    /// <summary>
    /// The base class for expressions with different number of parameters.
    /// </summary>
    public abstract class DifferentParametersExpression : IMathExpression
    {

        /// <summary>
        /// The parent expression of this expression.
        /// </summary>
        protected IMathExpression parent;
        /// <summary>
        /// The arguments.
        /// </summary>
        protected IMathExpression[] arguments;
        /// <summary>
        /// The count of parameters.
        /// </summary>
        protected int countOfParams;

        /// <summary>
        /// Initializes a new instance of the <see cref="DifferentParametersExpression"/> class.
        /// </summary>
        /// <param name="countOfParams">The count of parameters.</param>
        protected DifferentParametersExpression(int countOfParams)
            : this(null, countOfParams)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DifferentParametersExpression" /> class.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <param name="countOfParams">The count of parameters.</param>
        protected DifferentParametersExpression(IMathExpression[] arguments, int countOfParams)
        {
            this.arguments = arguments;
            this.countOfParams = countOfParams;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        protected int GetHashCode(int first, int second)
        {
            int hash = first;

            foreach (var item in arguments)
                hash = hash * second + item.GetHashCode();

            return hash;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="function">The function.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        protected string ToString(string function)
        {
            var sb = new StringBuilder();

            sb.Append(function).Append('(');
            foreach (var item in arguments)
                sb.Append(item).Append(", ");
            sb.Remove(sb.Length - 2, 2).Append(')');

            return sb.ToString();
        }

        /// <summary>
        /// Calculates this mathemarical expression. Don't use this method if your expression has variables or functions.
        /// </summary>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        public abstract double Calculate();

        /// <summary>
        /// Calculates this mathemarical expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        public abstract double Calculate(ExpressionParameters parameters);

        /// <summary>
        /// Calculates a derivative of the expression.
        /// </summary>
        /// <returns>
        /// Returns a derivative of the expression.
        /// </returns>
        public abstract IMathExpression Differentiate();

        /// <summary>
        /// Calculates a derivative of the expression.
        /// </summary>
        /// <param name="variable">The variable of differentiation.</param>
        /// <returns>
        /// Returns a derivative of the expression of several variables.
        /// </returns>
        /// <seealso cref="Variable" />
        public abstract IMathExpression Differentiate(Variable variable);

        /// <summary>
        /// Clones this instance of the <see cref="IMathExpression" />.
        /// </summary>
        /// <returns>
        /// Returns the new instance of <see cref="IMathExpression" /> that is a clone of this instance.
        /// </returns>
        public abstract IMathExpression Clone();

        /// <summary>
        /// Closes the arguments.
        /// </summary>
        /// <returns>The new array of <see cref="IMathExpression"/>.</returns>
        protected IMathExpression[] CloneArguments()
        {
            var args = new IMathExpression[arguments.Length];
            for (int i = 0; i < arguments.Length; i++)
                args[i] = arguments[i].Clone();

            return args;
        }

        /// <summary>
        /// Get or Set the parent expression.
        /// </summary>
        public IMathExpression Parent
        {
            get
            {
                return parent;
            }
            set
            {
                parent = value;
            }
        }

        /// <summary>
        /// Gets or sets the arguments.
        /// </summary>
        /// <value>The arguments.</value>
        public IMathExpression[] Arguments
        {
            get
            {
                return arguments;
            }
            set
            {
                arguments = value;
            }
        }

        /// <summary>
        /// Gets the minimum count of parameters.
        /// </summary>
        /// <value>
        /// The minimum count of parameters.
        /// </value>
        public abstract int MinCountOfParams { get; }

        /// <summary>
        /// Gets the maximum count of parameters. -1 - Infinity.
        /// </summary>
        /// <value>
        /// The maximum count of parameters.
        /// </value>
        public abstract int MaxCountOfParams { get; }

        /// <summary>
        /// Gets or Sets the count of parameters.
        /// </summary>
        /// <value>
        /// The count of parameters.
        /// </value>
        public int CountOfParams
        {
            get
            {
                return countOfParams;
            }
            set
            {
                countOfParams = value;
            }
        }

    }

}
