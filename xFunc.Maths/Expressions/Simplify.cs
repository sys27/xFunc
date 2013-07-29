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
    
    public class Simplify  : IMathExpression
    {

        private IMathExpression firstMathExpression;

        /// <summary>
        /// Initializes a new instance of the <see cref="Simplify"/> class.
        /// </summary>
        public Simplify() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Simplify"/> class.
        /// </summary>
        /// <param name="firstMathExpression">The argument of function.</param>
        public Simplify(IMathExpression firstMathExpression)
        {
            this.firstMathExpression = firstMathExpression;
        }

        /// <summary>
        /// Determines whether the specified <see cref="Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="Object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            var simp = obj as Simplify;
            if (simp != null && firstMathExpression.Equals(simp.firstMathExpression))
                return true;

            return false;
        }

        /// <summary>
        /// Returns a <see cref="String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return string.Format("simplify({0})", firstMathExpression.ToString());
        }

        public double Calculate()
        {
            throw new NotSupportedException();
        }

        public double Calculate(MathParameterCollection parameters)
        {
            throw new NotSupportedException();
        }

        public double Calculate(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            throw new NotSupportedException();
        }

        public IMathExpression Differentiate()
        {
            throw new NotSupportedException();
        }

        public IMathExpression Differentiate(Variable variable)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IMathExpression"/> that is a clone of this instance.</returns>
        public IMathExpression Clone()
        {
            return new Simplify(firstMathExpression.Clone());
        }

        /// <summary>
        /// This property always returns <c>null</c>.
        /// </summary>
        public IMathExpression Parent
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        public IMathExpression FirstMathExpression
        {
            get
            {
                return firstMathExpression;
            }
            set
            {
                firstMathExpression = value;
            }
        }

    }

}
