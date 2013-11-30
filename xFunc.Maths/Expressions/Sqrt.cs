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
    /// Represents the Square Root function.
    /// </summary>
    public class Sqrt : UnaryExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Sqrt"/> class.
        /// </summary>
        internal Sqrt() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sqrt"/> class.
        /// </summary>
        /// <param name="firstMathExpression">The argument of the function.</param>
        /// <seealso cref="IExpression"/>
        public Sqrt(IExpression firstMathExpression) : base(firstMathExpression) { }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode(1021);
        }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            return ToString("sqrt({0})");
        }

        /// <summary>
        /// Calculates this expression. Don't use this method if your expression has variables.
        /// </summary>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        public override double Calculate()
        {
            return Math.Sqrt(argument.Calculate());
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
            return Math.Sqrt(argument.Calculate(parameters));
        }

        /// <summary>
        /// Calculates a derivative of the expression.
        /// </summary>
        /// <param name="variable">The variable of differentiation.</param>
        /// <returns>
        /// Returns a derivative of the expression of several variables.
        /// </returns>
        /// <seealso cref="Variable" />
        protected override IExpression _Differentiation(Variable variable)
        {
            var mul = new Mul(new Number(2), Clone());
            var div = new Div(argument.Clone().Differentiate(variable), mul);

            return div;
        }

        /// <summary>
        /// Clones this instance of the <see cref="Sqrt"/> class.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IExpression"/> that is a clone of this instance.</returns>
        public override IExpression Clone()
        {
            return new Sqrt(argument.Clone());
        }

    }

}
