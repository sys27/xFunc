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
using xFunc.Maths.Expressions;

namespace xFunc.Maths
{

    public class MathDifferentiator : IDifferentiator
    {

        private ISimplifier simplifier;

        /// <summary>
        /// Initializes a new instance of the <see cref="MathDifferentiator"/> class.
        /// </summary>
        public MathDifferentiator()
            : this(new MathSimplifier())
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MathDifferentiator"/> class.
        /// </summary>
        /// <param name="simplifier">The simplifier.</param>
        public MathDifferentiator(ISimplifier simplifier)
        {
            this.simplifier = simplifier;
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>Returns the derivative.</returns>
        public IMathExpression Differentiate(IMathExpression expression)
        {
            return Differentiate(expression, new Variable("x"));
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        public IMathExpression Differentiate(IMathExpression expression, Variable variable)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            return simplifier.Simplify(expression.Differentiate(variable));
        }

        /// <summary>
        /// Gets or sets the simplifier.
        /// </summary>
        /// <value>The simplifier.</value>
        public ISimplifier Simplifier
        {
            get
            {
                return simplifier;
            }
            set
            {
                simplifier = value;
            }
        }

    }

}
