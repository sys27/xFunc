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
    /// Represents the Logarithm function.
    /// </summary>
    public class Log : BinaryMathExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Log"/> class.
        /// </summary>
        /// <seealso cref="IMathExpression"/>
        public Log() : base(null, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Log"/> class.
        /// </summary>
        /// <param name="firstMathExpression">The left operand.</param>
        /// <param name="secondMathExpression">The right operand.</param>
        /// <seealso cref="IMathExpression"/>
        public Log(IMathExpression firstMathExpression, IMathExpression secondMathExpression) : base(firstMathExpression, secondMathExpression) { }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            return ToString("log({0}, {1})");
        }

        /// <summary>
        /// Calculates this Log expression. Don't use this method if your expression has variables.
        /// </summary>
        /// <returns>A result of the calculation.</returns>
        public override double Calculate()
        {
            return Math.Log(firstMathExpression.Calculate(), secondMathExpression.Calculate());
        }

        /// <summary>
        /// Calculates this Log expression.
        /// </summary>
        /// <param name="parameters">A collection of variables that are used in the expression.</param>
        /// <returns>A result of the calculation.</returns>
        public override double Calculate(MathParameterCollection parameters)
        {
            return Math.Log(firstMathExpression.Calculate(parameters), secondMathExpression.Calculate(parameters));
        }

        public override IMathExpression Differentiate(Variable variable)
        {
            if (MathParser.HasVar(firstMathExpression, variable))
            {
                if (!(secondMathExpression is Number))
                    throw new NotSupportedException();

                Ln ln = new Ln(secondMathExpression.Clone());
                Multiplication mul = new Multiplication(firstMathExpression.Clone(), ln);
                Division div = new Division(firstMathExpression.Clone().Differentiate(variable), mul);

                return div;
            }

            return new Number(0);
        }

        /// <summary>
        /// Clones this instanse of the <see cref="Log"/>.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IMathExpression"/> that is a clone of this instance.</returns>
        public override IMathExpression Clone()
        {
            return new Log(firstMathExpression.Clone(), secondMathExpression.Clone());
        }

    }

}
