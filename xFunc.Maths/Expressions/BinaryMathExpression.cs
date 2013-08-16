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
    /// The base class for binary operations.
    /// </summary>
    public abstract class BinaryMathExpression : IMathExpression
    {

        /// <summary>
        /// The parent expression of this expression.
        /// </summary>
        protected IMathExpression parentMathExpression;
        /// <summary>
        /// The left (first) operand.
        /// </summary>
        protected IMathExpression firstMathExpression;
        /// <summary>
        /// The right (second) operand.
        /// </summary>
        protected IMathExpression secondMathExpression;

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryMathExpression"/> class.
        /// </summary>
        /// <param name="firstMathExpression">The left (first) operand.</param>
        /// <param name="secondMathExpression">The right (second) operand.</param>
        protected BinaryMathExpression(IMathExpression firstMathExpression, IMathExpression secondMathExpression)
        {
            FirstMathExpression = firstMathExpression;
            SecondMathExpression = secondMathExpression;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;

            var exp = obj as BinaryMathExpression;
            if (exp == null)
                return false;

            return firstMathExpression.Equals(exp.FirstMathExpression) && secondMathExpression.Equals(exp.SecondMathExpression);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <param name="format">The format of result string.</param>
        /// <returns>A string that represents the current object.</returns>
        protected string ToString(string format)
        {
            return string.Format(format, firstMathExpression, secondMathExpression);
        }

        /// <summary>
        /// Calculates this mathemarical expression. Don't use this method if your expression has variables or functions.
        /// </summary>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        public abstract double Calculate();

        /// <summary>
        /// Calculates this mathemarical expression.
        /// </summary>
        /// <param name="parameters">A collection of variables that are used in the expression.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="MathParameterCollection" />
        public abstract double Calculate(MathParameterCollection parameters);

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
        public abstract double Calculate(MathParameterCollection parameters, MathFunctionCollection functions);

        /// <summary>
        /// Creates the clone of this instance.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="BinaryMathExpression"/> that is a clone of this instance.</returns>
        public abstract IMathExpression Clone();

        public IMathExpression Differentiate()
        {
            return Differentiate(new Variable("x"));
        }

        public abstract IMathExpression Differentiate(Variable variable);

        /// <summary>
        /// The left (first) operand.
        /// </summary>
        public IMathExpression FirstMathExpression
        {
            get
            {
                return firstMathExpression;
            }
            set
            {
                firstMathExpression = value;
                if (firstMathExpression != null)
                    firstMathExpression.Parent = this;
            }
        }

        /// <summary>
        /// The right (second) operand.
        /// </summary>
        public IMathExpression SecondMathExpression
        {
            get
            {
                return secondMathExpression;
            }
            set
            {
                secondMathExpression = value;
                if (secondMathExpression != null)
                    secondMathExpression.Parent = this;
            }
        }

        /// <summary>
        /// Get or Set the parent expression.
        /// </summary>
        public IMathExpression Parent
        {
            get
            {
                return parentMathExpression;
            }
            set
            {
                parentMathExpression = value;
            }
        }

    }

}
