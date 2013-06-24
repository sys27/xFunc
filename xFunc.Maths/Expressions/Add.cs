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
    /// Represents an Addition operation.
    /// </summary>
    public class Add : BinaryMathExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Add"/> class.
        /// </summary>
        public Add() : base(null, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Add"/> class.
        /// </summary>
        /// <param name="firstMathExpression">The left operand.</param>
        /// <param name="secondMathExpression">The right operand.</param>
        /// <seealso cref="IMathExpression"/>
        public Add(IMathExpression firstMathExpression, IMathExpression secondMathExpression) : base(firstMathExpression, secondMathExpression) { }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            if (parentMathExpression is BinaryMathExpression)
            {
                return ToString("({0} + {1})");
            }

            return ToString("{0} + {1}");
        }

        /// <summary>
        /// Calculates this expression. Don't use this method if your expression has variables.
        /// </summary>
        /// <returns>A result of the calculation.</returns>
        public override double Calculate()
        {
            return firstMathExpression.Calculate() + secondMathExpression.Calculate();
        }

        /// <summary>
        /// Calculates this expression.
        /// </summary>
        /// <param name="parameters">A collection of variables that are used in the expression.</param>
        /// <returns>A result of the calculation.</returns>
        public override double Calculate(MathParameterCollection parameters)
        {
            return firstMathExpression.Calculate(parameters) + secondMathExpression.Calculate(parameters);
        }

        public override double Calculate(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            return firstMathExpression.Calculate(parameters, functions) + secondMathExpression.Calculate(parameters, functions);
        }

        public override IMathExpression Differentiate(Variable variable)
        {
            var first = MathParser.HasVar(firstMathExpression, variable);
            var second = MathParser.HasVar(secondMathExpression, variable);

            if (first && second)
            {
                return new Add(firstMathExpression.Clone().Differentiate(variable), secondMathExpression.Differentiate(variable).Clone());
            }
            if (first)
            {
                return firstMathExpression.Clone().Differentiate(variable);
            }
            if (second)
            {
                return secondMathExpression.Differentiate(variable).Clone();
            }

            return new Number(0);
        }

        /// <summary>
        /// Clones this instanse of the <see cref="Add"/>.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IMathExpression"/> that is a clone of this instance.</returns>
        public override IMathExpression Clone()
        {
            return new Add(firstMathExpression.Clone(), secondMathExpression.Clone());
        }

    }

}
