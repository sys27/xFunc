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
    /// The abstract base class that represents the unary operation.
    /// </summary>
    public abstract class UnaryMathExpression : IMathExpression
    {

        protected IMathExpression parentMathExpression;
        protected IMathExpression firstMathExpression;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnaryMathExpression"/> class.
        /// </summary>
        /// <param name="firstMathExpression">The expression.</param>
        protected UnaryMathExpression(IMathExpression firstMathExpression)
        {
            FirstMathExpression = firstMathExpression;
        }

        /// <summary>
        /// Determines whether the specified <see cref="Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="Object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if (obj == null || GetType() != obj.GetType())
                return false;

            UnaryMathExpression exp = obj as UnaryMathExpression;
            return firstMathExpression.Equals(exp.FirstMathExpression);
        }

        /// <summary>
        /// Returns a <see cref="String" /> that represents this instance.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <returns>A <see cref="String" /> that represents this instance.</returns>
        protected string ToString(string format)
        {
            return string.Format(format, firstMathExpression);
        }

        public abstract double Calculate();

        public abstract double Calculate(MathParameterCollection parameters);

        public abstract double Calculate(MathParameterCollection parameters, MathFunctionCollection functions);

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IMathExpression"/> that is a clone of this instance.</returns>
        public abstract IMathExpression Clone();

        public IMathExpression Differentiate()
        {
            return Differentiate(new Variable("x"));
        }

        public IMathExpression Differentiate(Variable variable)
        {
            if (MathParser.HasVar(firstMathExpression, variable))
            {
                return _Differentiation(variable);
            }

            return new Number(0);
        }

        protected abstract IMathExpression _Differentiation(Variable variable);

        /// <summary>
        /// Gets or sets the expression.
        /// </summary>
        /// <value>The expression.</value>
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
