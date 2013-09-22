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

namespace xFunc.Maths.Expressions.Hyperbolic
{

    /// <summary>
    /// Represents the Hyperbolic cotangent function.
    /// </summary>
    [ReverseFunction(typeof(Arcoth))]
    public class Coth : HyperbolicMathExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Coth"/> class.
        /// </summary>
        internal Coth() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Coth"/> class.
        /// </summary>
        /// <param name="firstMathExpression">The argument of function.</param>
        public Coth(IMathExpression firstMathExpression)
            : base(firstMathExpression)
        {

        }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            return ToString("coth({0})");
        }

        /// <summary>
        /// Calculates this mathemarical expression. Don't use this method if your expression has variables or functions.
        /// </summary>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        public override double Calculate()
        {
            return MathExtentions.Coth(argument.Calculate());
        }

        /// <summary>
        /// Calculates this mathemarical expression.
        /// </summary>
        /// <param name="parameters">A collection of variables that are used in the expression.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="MathParameterCollection" />
        public override double Calculate(MathParameterCollection parameters)
        {
            return MathExtentions.Coth(argument.Calculate(parameters));
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
        public override double Calculate(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            return MathExtentions.Coth(argument.Calculate(parameters, functions));
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>The new instance of <see cref="IMathExpression"/> that is a clone of this instance.</returns>
        public override IMathExpression Clone()
        {
            return new Coth(argument.Clone());
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
            var sinh = new Sinh(argument.Clone());
            var inv = new Pow(sinh, new Number(2));
            var div = new Div(argument.Clone().Differentiate(variable), inv);
            var unMinus = new UnaryMinus(div);

            return unMinus;
        }

    }

}
