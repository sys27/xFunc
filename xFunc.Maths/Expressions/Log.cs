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
        internal Log() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Log"/> class.
        /// </summary>
        /// <param name="arg">The left operand.</param>
        /// <param name="base">The right operand.</param>
        /// <seealso cref="IMathExpression"/>
        public Log(IMathExpression arg, IMathExpression @base) : base(@base, arg) { }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode(8837, 7019);
        }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            return ToString("log({1}, {0})");
        }

        /// <summary>
        /// Calculates this Log expression. Don't use this method if your expression has variables.
        /// </summary>
        /// <returns>A result of the calculation.</returns>
        public override double Calculate()
        {
            return Math.Log(right.Calculate(), left.Calculate());
        }

        /// <summary>
        /// Calculates this Log expression.
        /// </summary>
        /// <param name="parameters">A collection of variables that are used in the expression.</param>
        /// <returns>A result of the calculation.</returns>
        public override double Calculate(MathParameterCollection parameters)
        {
            return Math.Log(right.Calculate(parameters), left.Calculate(parameters));
        }

        /// <summary>
        /// Calculates this Log expression.
        /// </summary>
        /// <param name="parameters">A collection of variables that are used in the expression.</param>
        /// <param name="functions">A collection of functions.</param>
        /// <returns>A result of the calculation.</returns>
        public override double Calculate(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            return Math.Log(right.Calculate(parameters, functions), left.Calculate(parameters, functions));
        }

        /// <summary>
        /// Calculates a derivative of the expression.
        /// </summary>
        /// <param name="variable"></param>
        /// <returns>
        /// Returns a derivative of the expression of several variables.
        /// </returns>
        /// <seealso cref="Variable" />
        /// <exception cref="NotSupportedException">The base of log is not a number.</exception>
        public override IMathExpression Differentiate(Variable variable)
        {
            if (MathParser.HasVar(right, variable))
            {
                if (!(left is Number))
                    // todo: error message
                    throw new NotSupportedException();

                var ln = new Ln(left.Clone());
                var mul = new Mul(right.Clone(), ln);
                var div = new Div(right.Clone().Differentiate(variable), mul);

                return div;
            }

            return new Number(0);
        }

        /// <summary>
        /// Clones this instance of the <see cref="Log"/>.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IMathExpression"/> that is a clone of this instance.</returns>
        public override IMathExpression Clone()
        {
            return new Log(right.Clone(), left.Clone());
        }

    }

}
