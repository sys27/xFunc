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
    /// Represents the Arccosecant function.
    /// </summary>
    [ReverseFunction(typeof(Csc))]
    public class Arccsc : TrigonometryMathExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Arccsc"/> class.
        /// </summary>
        internal Arccsc() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Arccsc"/> class.
        /// </summary>
        /// <param name="firstMathExpression">The argument of function.</param>
        public Arccsc(IMathExpression firstMathExpression) : base(firstMathExpression) { }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode(5879);
        }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            return ToString("arccsc({0})");
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
            return MathExtentions.Acsc(argument.Calculate(parameters)) / Math.PI * 180;
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
            return MathExtentions.Acsc(argument.Calculate(parameters));
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
            return MathExtentions.Acsc(argument.Calculate(parameters)) / Math.PI * 200;
        }

        /// <summary>
        /// Calculates a derivative of the expression.
        /// </summary>
        /// <param name="variable"></param>
        /// <returns>
        /// Returns a derivative of the expression of several variables.
        /// </returns>
        /// <seealso cref="Variable" />
        protected override IMathExpression _Differentiation(Variable variable)
        {
            var abs = new Abs(argument.Clone());
            var sqr = new Pow(argument.Clone(), new Number(2));
            var sub = new Sub(sqr, new Number(1));
            var sqrt = new Sqrt(sub);
            var mul = new Mul(abs, sqrt);
            var div = new Div(argument.Clone().Differentiate(variable), mul);
            var unary = new UnaryMinus(div);

            return unary;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>The new instance of <see cref="IMathExpression"/> that is a clone of this instance.</returns>
        public override IMathExpression Clone()
        {
            return new Arccsc(argument.Clone());
        }

    }

}
