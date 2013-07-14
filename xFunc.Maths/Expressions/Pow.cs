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
            if (parentMathExpression is BinaryMathExpression)
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
            return Math.Pow(firstMathExpression.Calculate(), secondMathExpression.Calculate());
        }

        /// <summary>
        /// Returns a specified number raised to the specified power.
        /// </summary>
        /// <param name="parameters">A collection of variables that are used in the expression.</param>
        /// <returns>A specified number raised to the specified power.</returns>
        public override double Calculate(MathParameterCollection parameters)
        {
            return Math.Pow(firstMathExpression.Calculate(parameters), secondMathExpression.Calculate(parameters));
        }

        /// <summary>
        /// Returns a specified number raised to the specified power.
        /// </summary>
        /// <param name="parameters">A collection of variables that are used in the expression.</param>
        /// <param name="functions">A collection of functions.</param>
        /// <returns>A specified number raised to the specified power.</returns>
        public override double Calculate(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            return Math.Pow(firstMathExpression.Calculate(parameters, functions), secondMathExpression.Calculate(parameters, functions));
        }

        public override IMathExpression Differentiate(Variable variable)
        {
            if (MathParser.HasVar(firstMathExpression, variable))
            {
                Sub sub = new Sub(secondMathExpression.Clone(), new Number(1));
                Pow inv = new Pow(firstMathExpression.Clone(), sub);
                Mul mul1 = new Mul(secondMathExpression.Clone(), inv);
                Mul mul2 = new Mul(firstMathExpression.Clone().Differentiate(variable), mul1);

                return mul2;
            }
            if (MathParser.HasVar(secondMathExpression, variable))
            {
                Ln ln = new Ln(firstMathExpression.Clone());
                Mul mul1 = new Mul(ln, Clone());
                Mul mul2 = new Mul(mul1, secondMathExpression.Clone().Differentiate(variable));

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
            return new Pow(firstMathExpression.Clone(), secondMathExpression.Clone());
        }

    }

}
