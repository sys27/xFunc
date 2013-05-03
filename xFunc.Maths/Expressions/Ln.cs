﻿// Copyright 2012-2013 Dmitry Kischenko
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
    /// Represents the Natural logarithm function.
    /// </summary>
    public class Ln : UnaryMathExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Ln"/> class.
        /// </summary>
        /// <seealso cref="IMathExpression"/>
        public Ln() : base(null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ln"/> class.
        /// </summary>
        /// <param name="firstMathExpression">The argument of function.</param>
        /// <seealso cref="IMathExpression"/>
        public Ln(IMathExpression firstMathExpression) : base(firstMathExpression) { }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            return ToString("ln({0})");
        }

        /// <summary>
        /// Calculates this Ln expression. Don't use this method if your expression has variables.
        /// </summary>
        /// <returns>A result of the calculation.</returns>
        public override double Calculate()
        {
            return Math.Log(FirstMathExpression.Calculate());
        }

        /// <summary>
        /// Calculates this Ln expression.
        /// </summary>
        /// <param name="parameters">A collection of variables that are used in the expression.</param>
        /// <returns>A result of the calculation.</returns>
        public override double Calculate(MathParameterCollection parameters)
        {
            return Math.Log(FirstMathExpression.Calculate(parameters));
        }

        public override double Calculate(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            return Math.Log(FirstMathExpression.Calculate(parameters, functions));
        }

        protected override IMathExpression _Differentiation(Variable variable)
        {
            Div div = new Div(firstMathExpression.Clone().Differentiate(variable), firstMathExpression.Clone());

            return div;
        }

        /// <summary>
        /// Clones this instanse of the <see cref="Ln"/>.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IMathExpression"/> that is a clone of this instance.</returns>
        public override IMathExpression Clone()
        {
            return new Ln(firstMathExpression.Clone());
        }

    }

}
