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
    /// Represents the Subtraction operation.
    /// </summary>
    public class Sub : BinaryMathExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Sub"/> class.
        /// </summary>
        public Sub() : base(null, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sub"/> class.
        /// </summary>
        /// <param name="firstMathExpression">The minuend.</param>
        /// <param name="secondMathExpression">The subtrahend.</param>
        public Sub(IMathExpression firstMathExpression, IMathExpression secondMathExpression) : base(firstMathExpression, secondMathExpression) { }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            if (parent is BinaryMathExpression)
            {
                return ToString("({0} - {1})");
            }

            return ToString("{0} - {1}");
        }

        /// <summary>
        /// Calculates this expression. Don't use this method if your expression has variables.
        /// </summary>
        /// <returns>A result of the calculation.</returns>
        public override double Calculate()
        {
            return left.Calculate() - right.Calculate();
        }

        /// <summary>
        /// Calculates this expression.
        /// </summary>
        /// <param name="parameters">A collection of variables that are used in the expression.</param>
        /// <returns>A result of the calculation.</returns>
        public override double Calculate(MathParameterCollection parameters)
        {
            return left.Calculate(parameters) - right.Calculate(parameters);
        }

        /// <summary>
        /// Calculates this expression.
        /// </summary>
        /// <param name="parameters">A collection of variables that are used in the expression.</param>
        /// <param name="functions">A collection of functions.</param>
        /// <returns>A result of the calculation.</returns>
        public override double Calculate(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            return left.Calculate(parameters, functions) - right.Calculate(parameters, functions);
        }

        public override IMathExpression Differentiate(Variable variable)
        {
            var first = MathParser.HasVar(left, variable);
            var second = MathParser.HasVar(right, variable);

            if (first && second)
            {
                return new Sub(left.Clone().Differentiate(variable), right.Clone().Differentiate(variable));
            }
            if (first)
            {
                return left.Clone().Differentiate(variable);
            }
            if (second)
            {
                return new UnaryMinus(right.Clone().Differentiate(variable));
            }

            return new Number(0);
        }
        
        /// <summary>
        /// Clones this instance of the <see cref="Sub"/> class.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IMathExpression"/> that is a clone of this instance.</returns>
        public override IMathExpression Clone()
        {
            return new Sub(left.Clone(), right.Clone());
        }

    }

}
