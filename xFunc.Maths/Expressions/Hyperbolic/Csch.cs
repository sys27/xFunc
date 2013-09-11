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
    /// Represents the Hyperbolic cosecant function.
    /// </summary>
    [ReverseFunction(typeof(Arcsch))]
    public class Csch : HyperbolicMathExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Csch"/> class.
        /// </summary>
        public Csch()
            : base(null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Csch"/> class.
        /// </summary>
        /// <param name="firstMathExpression">The argument of function.</param>
        public Csch(IMathExpression firstMathExpression)
            : base(firstMathExpression)
        {

        }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            return ToString("csch({0})");
        }

        /// <summary>
        /// Calculates this mathemarical expression. Don't use this method if your expression has variables or functions.
        /// </summary>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        public override double Calculate()
        {
            return MathExtentions.Csch(argument.Calculate());
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
            return MathExtentions.Csch(argument.Calculate(parameters));
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
            return MathExtentions.Csch(argument.Calculate(parameters, functions));
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>The new instance of <see cref="IMathExpression"/> that is a clone of this instance.</returns>
        public override IMathExpression Clone()
        {
            return new Csch(argument.Clone());
        }

        protected override IMathExpression _Differentiation(Variable variable)
        {
            var coth = new Coth(argument.Clone());
            var csch = Clone();
            var mul1 = new Mul(coth, csch);
            var mul2 = new Mul(argument.Clone().Differentiate(variable), mul1);
            var unMinus = new UnaryMinus(mul2);

            return unMinus;
        }

    }

}
