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
using xFunc.Maths.Resources;

namespace xFunc.Maths.Expressions
{

    /// <summary>
    /// Represents the Deriv function.
    /// </summary>
    public class Derivative : DifferentParametersExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Derivative"/> class.
        /// </summary>
        internal Derivative()
            : base(null, -1) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Derivative"/> class.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="countOfParams">The count of parameters.</param>
        public Derivative(IExpression[] args, int countOfParams)
            : base(args, countOfParams)
        {
            if (args == null)
                throw new ArgumentNullException("args");
            if (args.Length != countOfParams)
                throw new ArgumentException();
            if (countOfParams == 2 && !(args[1] is Variable))
                throw new ArgumentException(Resource.InvalidExpression);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Derivative"/> class.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        public Derivative(IExpression expression, Variable variable)
            : base(new[] { expression, variable }, 2)
        {
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

            return Expression.Equals(exp.Expression) &&
                   (countOfParams == 2 && Variable.Equals(exp.Variable));
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode(587, 1249);
        }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            if (countOfParams == 1)
                return string.Format("deriv({0})", Expression);

            return string.Format("deriv({0}, {1})", Expression, Variable);
        }

        /// <summary>
        /// Calculates this mathemarical expression. Don't use this method if your expression has variables or functions.
        /// </summary>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        public override double Calculate()
        {
            return Differentiate().Calculate();
        }

        /// <summary>
        /// Calculates this mathemarical expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        public override double Calculate(ExpressionParameters parameters)
        {
            return Differentiate().Calculate(parameters);
        }

        /// <summary>
        /// Calculates a derivative of the expression.
        /// </summary>
        /// <returns>
        /// Returns a derivative of the expression.
        /// </returns>
        public override IExpression Differentiate()
        {
            if (countOfParams == 1)
                return _Differentiate(new Variable("x"));

            return _Differentiate(Variable);
        }

        /// <summary>
        /// Calculates a derivative of the expression.
        /// </summary>
        /// <param name="variable">The variable of differentiation.</param>
        /// <returns>
        /// Returns a derivative of the expression of several variables.
        /// </returns>
        /// <seealso cref="Variable" />
        /// <remarks>
        /// This method ignores the local <paramref name="variable" />.
        /// </remarks>
        public override IExpression Differentiate(Variable variable)
        {
            if (countOfParams == 1)
                return _Differentiate(new Variable("x"));

            return _Differentiate(this.Variable);
        }

        private IExpression _Differentiate(Variable variable)
        {
            if (Expression is Derivative)
                return Expression.Differentiate(variable).Differentiate(variable);

            return Expression.Differentiate(variable);
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IExpression"/> that is a clone of this instance.</returns>
        public override IExpression Clone()
        {
            return new Derivative(CloneArguments(), countOfParams);
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
                return arguments[0];
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                arguments[0] = value;
                arguments[0].Parent = this;
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
                return (Variable)arguments[1];
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                arguments[1] = value;
                arguments[1].Parent = this;
            }
        }

        /// <summary>
        /// Gets the minimum count of parameters.
        /// </summary>
        /// <value>
        /// The minimum count of parameters.
        /// </value>
        public override int MinCountOfParams
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
        public override int MaxCountOfParams
        {
            get
            {
                return 2;
            }
        }

    }

}
