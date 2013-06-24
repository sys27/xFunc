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
    /// Represents the Absolute operation.
    /// </summary>
    public class Abs : UnaryMathExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Abs"/> class.
        /// </summary>
        public Abs() : base(null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Abs"/> class.
        /// </summary>
        /// <param name="expression">The argument of function.</param>
        /// <seealso cref="IMathExpression"/>
        public Abs(IMathExpression expression) : base(expression) { }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            return ToString("abs({0})");
        }

        /// <summary>
        /// Calculates this Absolute expression. Don't use this method if your expression has variables.
        /// </summary>
        /// <returns>A result of the calculation.</returns>
        public override double Calculate()
        {
            return Math.Abs(firstMathExpression.Calculate());
        }

        /// <summary>
        /// Calculates this Absolute expression.
        /// </summary>
        /// <param name="parameters">A collection of variables that are used in the expression.</param>
        /// <returns>A result of the calculation.</returns>
        public override double Calculate(MathParameterCollection parameters)
        {
            return Math.Abs(firstMathExpression.Calculate(parameters));
        }

        /// <summary>
        /// Calculates this Absolute expression.
        /// </summary>
        /// <param name="parameters">A collection of variables that are used in the expression.</param>
        /// <param name="functions">A collection of functions.</param>
        /// <returns>A result of the calculation.</returns>
        public override double Calculate(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            return Math.Abs(firstMathExpression.Calculate(parameters, functions));
        }

        /// <summary>
        /// Calculates a derivative of the expression.
        /// </summary>
        /// <param name="variable"></param>
        /// <returns>Returns a derivative of the expression of several variables.</returns>
        /// <seealso cref="Variable"/>
        protected override IMathExpression _Differentiation(Variable variable)
        {
            Div div = new Div(firstMathExpression.Clone(), Clone());
            Mul mul = new Mul(firstMathExpression.Clone().Differentiate(variable), div);

            return mul;
        }

        /// <summary>
        /// Clones this instanse of the <see cref="And"/> class.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IMathExpression"/> that is a clone of this instance.</returns>
        public override IMathExpression Clone()
        {
            return new Abs(firstMathExpression.Clone());
        }

    }

}
