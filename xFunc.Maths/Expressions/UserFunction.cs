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
using System.Collections.Generic;
#if NET35_OR_GREATER || PORTABLE
using System.Linq;
#endif
using System.Text;

namespace xFunc.Maths.Expressions
{

    /// <summary>
    /// Represents user-defined functions.
    /// </summary>
    public class UserFunction : IMathExpression
    {

        private string function;
        private IMathExpression[] arguments;
        private int countOfParams;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserFunction"/> class.
        /// </summary>
        internal UserFunction()
            : this(null, null, -1)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserFunction"/> class.
        /// </summary>
        /// <param name="function">The name of function.</param>
        /// <param name="countOfParams">The count of parameters.</param>
        public UserFunction(string function, int countOfParams) : this(function, null, countOfParams) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserFunction"/> class.
        /// </summary>
        /// <param name="function">The name of function.</param>
        /// <param name="args">Arguments.</param>
        /// <param name="countOfParams">The count of parameters.</param>
        public UserFunction(string function, IMathExpression[] args, int countOfParams)
        {
            this.function = function;
            this.arguments = args;
            this.countOfParams = countOfParams;
        }

        /// <summary>
        /// Determines whether the specified <see cref="Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="Object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            var exp = obj as UserFunction;
            if (exp != null && this.function == exp.function && this.countOfParams == exp.countOfParams)
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
            int hash = 1721;

            hash = (hash * 5701) + function.GetHashCode();
            if (arguments != null)
            {
                foreach (var arg in arguments)
                {
                    hash = (hash * 5701) + arg.GetHashCode();
                }
            }
            hash = (hash * 5701) + countOfParams.GetHashCode();

            return hash;
        }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(function);
            builder.Append('(');
            if (arguments != null)
            {
                foreach (var arg in arguments)
                {
                    builder.Append(arg);
                    builder.Append(',');
                }
                if (arguments.Length > 0)
                    builder.Remove(builder.Length - 1, 1);
            }
            builder.Append(')');

            return builder.ToString();
        }

        /// <summary>
        /// Always throws an exception.
        /// </summary>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public double Calculate()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Always throws an exception.
        /// </summary>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        /// <seealso cref="MathParameterCollection" />
        public double Calculate(MathParameterCollection parameters)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Calculates this mathemarical expression.
        /// </summary>
        /// <param name="parameters">A collection of variables that are used in the expression.</param>
        /// <param name="functions">A collection of functions that are used in the expression.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="MathParameterCollection" />
        /// <seealso cref="MathFunctionCollection" />
        /// <exception cref="System.ArgumentNullException"><paramref name="functions"/> is null.</exception>
        public double Calculate(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            if (functions == null)
                throw new ArgumentNullException("functions");

            var func = functions.Keys.First(uf => uf.Equals(this));
            var newParameters = new MathParameterCollection(parameters);
            for (int i = 0; i < arguments.Length; i++)
            {
                var arg = func.Arguments[i] as Variable;
                newParameters[arg.Name] = this.arguments[i].Calculate(parameters, functions);
            }

            return functions[this].Calculate(newParameters, functions);
        }

        /// <summary>
        /// Calculates a derivative of the expression.
        /// </summary>
        /// <returns>
        /// Returns a derivative of the expression.
        /// </returns>
        public IMathExpression Differentiate()
        {
            return Differentiate(new Variable("x"));
        }

        /// <summary>
        /// Calculates a derivative of the expression.
        /// </summary>
        /// <param name="variable"></param>
        /// <returns>
        /// Returns a derivative of the expression of several variables.
        /// </returns>
        /// <seealso cref="Variable" />
        public IMathExpression Differentiate(Variable variable)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IMathExpression"/> that is a clone of this instance.</returns>
        public IMathExpression Clone()
        {
            return new UserFunction(function, arguments, countOfParams);
        }

        /// <summary>
        /// This property always returns <c>null</c>.
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
        /// Gets the name of function.
        /// </summary>
        /// <value>The name of function.</value>
        public string Function
        {
            get
            {
                return function;
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
        /// Gets the minimum count of parameters. -1 - Infinity.
        /// </summary>
        /// <value>
        /// The minimum count of parameters.
        /// </value>
        public int MinCountOfParams
        {
            get
            {
                return countOfParams;
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
                return countOfParams;
            }
        }

    }

}
