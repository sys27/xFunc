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
    /// Represents the Deriv function.
    /// </summary>
    public class Derivative : IMathExpression
    {

        private IMathExpression parent;
        private IMathExpression expression;
        private Variable variable;

        /// <summary>
        /// Initializes a new instance of the <see cref="Derivative"/> class.
        /// </summary>
        internal Derivative() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Derivative"/> class.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        public Derivative(IMathExpression expression, Variable variable)
        {
            this.expression = expression;
            this.variable = variable;
        }

        /// <summary>
        /// Determines whether the specified <see cref="Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="Object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;

            var exp = obj as Derivative;
            if (exp == null)
                return false;

            return expression.Equals(exp.Expression) && variable.Equals(exp.Variable);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            int hash = 587;

            hash = (hash * 1249) + expression.GetHashCode();
            hash = (hash * 1249) + variable.GetHashCode();

            return hash;
        }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            return string.Format("deriv({0}, {1})", expression, variable);
        }

        /// <summary>
        /// Calculates this mathemarical expression. Don't use this method if your expression has variables or functions.
        /// </summary>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        public double Calculate()
        {
            return Differentiate().Calculate();
        }

        /// <summary>
        /// Calculates this mathemarical expression.
        /// </summary>
        /// <param name="parameters">A collection of variables that are used in the expression.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="MathParameterCollection" />
        public double Calculate(MathParameterCollection parameters)
        {
            return Differentiate().Calculate(parameters);
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
        public double Calculate(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            return Differentiate().Calculate(parameters, functions);
        }

        /// <summary>
        /// Calculates a derivative of the expression.
        /// </summary>
        /// <returns>
        /// Returns a derivative of the expression.
        /// </returns>
        public IMathExpression Differentiate()
        {
            if (expression is Derivative)
                return expression.Differentiate(variable).Differentiate(variable);

            return expression.Differentiate(variable);
        }

        /// <summary>
        /// Calculates a derivative of the expression.
        /// </summary>
        /// <param name="variable"></param>
        /// <returns>
        /// Returns a derivative of the expression of several variables.
        /// </returns>
        /// <seealso cref="Variable" />
        /// <remarks>
        /// This method ignores the local <paramref name="variable"/>.
        /// </remarks>
        public IMathExpression Differentiate(Variable variable)
        {
            if (expression is Derivative)
                return expression.Differentiate(this.variable).Differentiate(this.variable);

            return expression.Differentiate(this.variable);
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IMathExpression"/> that is a clone of this instance.</returns>
        public IMathExpression Clone()
        {
            return new Derivative(expression.Clone(), (Variable)variable.Clone());
        }

        /// <summary>
        /// Gets or sets the expression.
        /// </summary>
        /// <value>
        /// The expression.
        /// </value>
        public IMathExpression Expression
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
                if (expression != null)
                    expression.Parent = this;
            }
        }

        /// <summary>
        /// Gets or sets the variable.
        /// </summary>
        /// <value>
        /// The variable.
        /// </value>
        public Variable Variable
        {
            get
            {
                return variable;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                variable = value;
                if (variable != null)
                    variable.Parent = this;
            }
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

    }

}
