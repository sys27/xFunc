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
using xFunc.Maths.Expressions;

namespace xFunc.Maths
{

    /// <summary>
    /// The differentiator of expressions.
    /// </summary>
    public class Differentiator : IDifferentiator
    {

        private ISimplifier simplifier;

        /// <summary>
        /// Initializes a new instance of the <see cref="Differentiator"/> class.
        /// </summary>
        public Differentiator()
            : this(new Simplifier())
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Differentiator"/> class.
        /// </summary>
        /// <param name="simplifier">The simplifier.</param>
        public Differentiator(ISimplifier simplifier)
        {
            this.simplifier = simplifier;
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>Returns the derivative.</returns>
        public IExpression Differentiate(IExpression expression)
        {
            return Differentiate(expression, new Variable("x"));
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        public IExpression Differentiate(IExpression expression, Variable variable)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");
            if (variable == null)
                throw new ArgumentNullException("variable");

            //return simplifier.Simplify(expression.Differentiate(variable));
            throw new NotImplementedException();
        }

        private IExpression _Differentiate(IExpression expression, Variable variable)
        {
            if (expression is UnaryExpression)
            {
                var un = (UnaryExpression)expression;
                if (!Parser.HasVar(un.Argument, variable))
                    return new Number(0);
            }

            if (expression is Sqrt)
                return Sqrt((Sqrt)expression, variable);
            if (expression is Ln)
                return Ln((Ln)expression, variable);
            if (expression is Lg)
                return Lg((Lg)expression, variable);
            if (expression is Log)
                return Log((Log)expression, variable);
            if (expression is Exp)
                return Exp((Exp)expression, variable);

            throw new NotImplementedException();
        }

        protected virtual IExpression Ln(Ln expression, Variable variable)
        {
            return new Div(_Differentiate(expression.Argument.Clone(), variable), expression.Argument.Clone());
        }

        protected virtual IExpression Lg(Lg expression, Variable variable)
        {
            var ln = new Ln(new Number(10));
            var mul1 = new Mul(expression.Argument.Clone(), ln);
            var div = new Div(_Differentiate(expression.Argument.Clone(), variable), mul1);

            return div;
        }

        protected virtual IExpression Log(Log expression, Variable variable)
        {
            if (Parser.HasVar(expression.Left, variable))
            {
                var ln1 = new Ln(expression.Right.Clone());
                var ln2 = new Ln(expression.Left.Clone());
                var div = new Div(ln1, ln2);

                return _Differentiate(div, variable);
            }
            if (Parser.HasVar(expression.Right, variable))
            {
                var ln = new Ln(expression.Left.Clone());
                var mul = new Mul(expression.Right.Clone(), ln);
                var div = new Div(_Differentiate(expression.Right.Clone(), variable), mul);

                return div;
            }

            return new Number(0);
        }

        protected virtual IExpression Sqrt(Sqrt expression, Variable variable)
        {
            var mul = new Mul(new Number(2), expression.Clone());
            var div = new Div(_Differentiate(expression.Argument.Clone(), variable), mul);

            return div;
        }

        protected virtual IExpression Exp(Exp expression, Variable variable)
        {
            return new Mul(_Differentiate(expression.Argument.Clone(), variable), expression.Clone());
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
