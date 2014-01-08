// Copyright 2012-2014 Dmitry Kischenko
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
    public class Log : BinaryExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Log"/> class.
        /// </summary>
        /// <seealso cref="IExpression"/>
        internal Log() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Log"/> class.
        /// </summary>
        /// <param name="arg">The left operand.</param>
        /// <param name="base">The right operand.</param>
        /// <seealso cref="IExpression"/>
        public Log(IExpression arg, IExpression @base) : base(@base, arg) { }

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
            return ToString("log({0}, {1})");
        }

        /// <summary>
        /// Calculates this Log expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        public override object Calculate(ExpressionParameters parameters)
        {
            return Math.Log((double)right.Calculate(parameters), (double)left.Calculate(parameters));
        }

        /// <summary>
        /// Calculates a derivative of the expression.
        /// </summary>
        /// <param name="variable">The variable of differentiation.</param>
        /// <returns>
        /// Returns a derivative of the expression of several variables.
        /// </returns>
        /// <seealso cref="Variable" />
        public override IExpression Differentiate(Variable variable)
        {
            if (Parser.HasVar(left, variable))
            {
                var ln1 = new Ln(right.Clone());
                var ln2 = new Ln(left.Clone());
                var div = new Div(ln1, ln2);

                return div.Differentiate(variable);
            }
            if (Parser.HasVar(right, variable))
            {
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
        /// <returns>Returns the new instance of <see cref="IExpression"/> that is a clone of this instance.</returns>
        public override IExpression Clone()
        {
            return new Log(right.Clone(), left.Clone());
        }

    }

}
