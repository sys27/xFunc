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

namespace xFunc.Maths.Expressions.Trigonometric
{

    /// <summary>
    /// Represents the Secant function.
    /// </summary>
    [ReverseFunction(typeof(Arcsec))]
    public class Sec : TrigonometricExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Sec"/> class.
        /// </summary>
        internal Sec() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sec"/> class.
        /// </summary>
        /// <param name="firstMathExpression">The argument of function.</param>
        public Sec(IExpression firstMathExpression)
            : base(firstMathExpression)
        {
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode(6659);
        }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            return ToString("sec({0})");
        }

        /// <summary>
        /// Calculates this mathemarical expression (using degree).
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        protected override double CalculateDergee(ExpressionParameters parameters)
        {
            var radian = (double)argument.Calculate(parameters) * Math.PI / 180;

            return 1 / Math.Cos(radian);
        }

        /// <summary>
        /// Calculates this mathemarical expression (using radian).
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        protected override double CalculateRadian(ExpressionParameters parameters)
        {
            return 1 / Math.Cos((double)argument.Calculate(parameters));
        }

        /// <summary>
        /// Calculates this mathemarical expression (using gradian).
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        protected override double CalculateGradian(ExpressionParameters parameters)
        {
            var radian = (double)argument.Calculate(parameters) * Math.PI / 200;

            return 1 / Math.Cos(radian);
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
            var tan = new Tan(argument.Clone());
            var sec = new Sec(argument.Clone());
            var mul1 = new Mul(tan, sec);
            var mul2 = new Mul(argument.Clone().Differentiate(variable), mul1);

            return mul2;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>The new instance of <see cref="IExpression"/> that is a clone of this instance.</returns>
        public override IExpression Clone()
        {
            return new Sec(argument.Clone());
        }

    }

}
