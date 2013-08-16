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
    /// Represents the Exponentiation operation.
    /// </summary>
    public class Pow : BinaryMathExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Pow"/> class.
        /// </summary>
        public Pow() : base(null, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pow"/> class.
        /// </summary>
        /// <param name="firstOperand">The base.</param>
        /// <param name="secondOperand">The exponent.</param>
        public Pow(IMathExpression firstOperand, IMathExpression secondOperand) : base(firstOperand, secondOperand) { }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            if (parent is BinaryMathExpression)
            {
                return ToString("({0} ^ {1})");
            }

            return ToString("{0} ^ {1}");
        }

        /// <summary>
        /// Returns a specified number raised to the specified power.
        /// </summary>
        /// <returns>A specified number raised to the specified power.</returns>
        public override double Calculate()
        {
            return Math.Pow(left.Calculate(), right.Calculate());
        }

        /// <summary>
        /// Returns a specified number raised to the specified power.
        /// </summary>
        /// <param name="parameters">A collection of variables that are used in the expression.</param>
        /// <returns>A specified number raised to the specified power.</returns>
        public override double Calculate(MathParameterCollection parameters)
        {
            return Math.Pow(left.Calculate(parameters), right.Calculate(parameters));
        }

        /// <summary>
        /// Returns a specified number raised to the specified power.
        /// </summary>
        /// <param name="parameters">A collection of variables that are used in the expression.</param>
        /// <param name="functions">A collection of functions.</param>
        /// <returns>A specified number raised to the specified power.</returns>
        public override double Calculate(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            return Math.Pow(left.Calculate(parameters, functions), right.Calculate(parameters, functions));
        }

        public override IMathExpression Differentiate(Variable variable)
        {
            if (MathParser.HasVar(left, variable))
            {
                var sub = new Sub(right.Clone(), new Number(1));
                var inv = new Pow(left.Clone(), sub);
                var mul1 = new Mul(right.Clone(), inv);
                var mul2 = new Mul(left.Clone().Differentiate(variable), mul1);

                return mul2;
            }
            if (MathParser.HasVar(right, variable))
            {
                var ln = new Ln(left.Clone());
                var mul1 = new Mul(ln, Clone());
                var mul2 = new Mul(mul1, right.Clone().Differentiate(variable));

                return mul2;
            }
            
            return new Number(0);
        }

        /// <summary>
        /// Clones this instance of the <see cref="Pow"/> class.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IMathExpression"/> that is a clone of this instance.</returns>
        public override IMathExpression Clone()
        {
            return new Pow(left.Clone(), right.Clone());
        }

    }

}
