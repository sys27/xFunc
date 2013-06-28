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
    /// Represents variables in expressions.
    /// </summary>
    public class Variable : IMathExpression
    {

        private IMathExpression parentMathExpression;
        private string name;

        /// <summary>
        /// Initializes a new instance of the <see cref="Variable"/> class.
        /// </summary>
        /// <param name="name">A name of variable.</param>
        public Variable(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Defines an implicit conversion of a Variable object to a string object.
        /// </summary>
        /// <param name="variable">The value to convert.</param>
        /// <returns>An object that contains the converted value.</returns>
        public static implicit operator string(Variable variable)
        {
            return variable == null ? null : variable.Name;
        }

        /// <summary>
        /// Defines an implicit conversion of a string object to a Variable object.
        /// </summary>
        /// <param name="variable">The value to convert.</param>
        /// <returns>An object that contains the converted value.</returns>
        public static implicit operator Variable(string variable)
        {
            return new Variable(variable);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            Variable @var = obj as Variable;
            if (@var != null && @var.Name == name)
                return true;

            return false;
        }

        /// <summary>
        /// Returns a hash function for this type.
        /// </summary>
        /// <returns>A hash code for the current <see cref="Variable"/>.</returns>
        public override int GetHashCode()
        {
            return name.GetHashCode() ^ 1048592;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return name;
        }

        /// <summary>
        /// Do not use this method. It always throws an exception.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotSupportedException">Always.</exception>
        public double Calculate()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Gets value of this variable from <paramref name="parameters"/>.
        /// </summary>
        /// <param name="parameters">Collection of variables.</param>
        /// <returns>A value of this variable.</returns>
        public double Calculate(MathParameterCollection parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException("parameters");

            return parameters[name];
        }

        /// <summary>
        /// Gets value of this variable from <paramref name="parameters"/>.
        /// </summary>
        /// <param name="parameters">Collection of variables.</param>
        /// <param name="functions">Collection of functions.</param>
        /// <returns>A value of this variable.</returns>
        public double Calculate(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            if (parameters == null)
                throw new ArgumentNullException("parameters");

            return parameters[name];
        }

        /// <summary>
        /// Clones this instanse of the <see cref="Variable"/>.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="Variable"/> that is a clone of this instance.</returns>
        public IMathExpression Clone()
        {
            return new Variable(name);
        }

        public IMathExpression Differentiate()
        {
            return Differentiate(new Variable("x"));
        }

        public IMathExpression Differentiate(Variable variable)
        {
            if (Equals(variable))
                return new Number(1);

            return Clone();
        }

        /// <summary>
        /// A name of this variable.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }

        /// <summary>
        /// Get or Set the parent expression.
        /// </summary>
        public IMathExpression Parent
        {
            get
            {
                return parentMathExpression;
            }
            set
            {
                parentMathExpression = value;
            }
        }

    }

}
