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
using xFunc.Maths.Expressions.Trigonometric;

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

            if (expression is Abs)
                return Abs((Abs)expression, variable);
            if (expression is Add)
                return Add((Add)expression, variable);
            if (expression is Div)
                return Div((Div)expression, variable);
            if (expression is Exp)
                return Exp((Exp)expression, variable);
            if (expression is Ln)
                return Ln((Ln)expression, variable);
            if (expression is Lg)
                return Lg((Lg)expression, variable);
            if (expression is Log)
                return Log((Log)expression, variable);
            if (expression is Mul)
                return Mul((Mul)expression, variable);
            if (expression is Pow)
                return Pow((Pow)expression, variable);
            if (expression is Root)
                return Root((Root)expression, variable);
            if (expression is Sqrt)
                return Sqrt((Sqrt)expression, variable);
            if (expression is Sub)
                return Sub((Sub)expression, variable);
            if (expression is UnaryMinus)
                return UnaryMinus((UnaryMinus)expression, variable);

            // todo: expression
            throw new NotSupportedException();
        }

        #region Common

        protected virtual IExpression Abs(Abs expression, Variable variable)
        {
            var div = new Div(expression.Argument.Clone(), expression.Clone());
            var mul = new Mul(_Differentiate(expression.Argument.Clone(), variable), div);

            return mul;
        }

        protected virtual IExpression Add(Add expression, Variable variable)
        {
            var first = Parser.HasVar(expression.Left, variable);
            var second = Parser.HasVar(expression.Right, variable);

            if (first && second)
                return new Add(_Differentiate(expression.Left.Clone(), variable), _Differentiate(expression.Right.Clone(), variable));
            if (first)
                return _Differentiate(expression.Left.Clone(), variable);
            if (second)
                return _Differentiate(expression.Right.Clone(), variable);

            return new Number(0);
        }

        protected virtual IExpression Div(Div expression, Variable variable)
        {
            var first = Parser.HasVar(expression.Left, variable);
            var second = Parser.HasVar(expression.Right, variable);

            if (first && second)
            {
                var mul1 = new Mul(_Differentiate(expression.Left.Clone(), variable), expression.Right.Clone());
                var mul2 = new Mul(expression.Left.Clone(), _Differentiate(expression.Right.Clone(), variable));
                var sub = new Sub(mul1, mul2);
                var inv = new Pow(expression.Right.Clone(), new Number(2));
                var division = new Div(sub, inv);

                return division;
            }
            if (first)
            {
                return new Div(_Differentiate(expression.Left.Clone(), variable), expression.Right.Clone());
            }
            if (second)
            {
                var mul2 = new Mul(expression.Left.Clone(), _Differentiate(expression.Right.Clone(), variable));
                var unMinus = new UnaryMinus(mul2);
                var inv = new Pow(expression.Right.Clone(), new Number(2));
                var division = new Div(unMinus, inv);

                return division;
            }

            return new Number(0);
        }

        protected virtual IExpression Exp(Exp expression, Variable variable)
        {
            return new Mul(_Differentiate(expression.Argument.Clone(), variable), expression.Clone());
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

                return Div(div, variable);
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

        protected virtual IExpression Mul(Mul expression, Variable variable)
        {
            var first = Parser.HasVar(expression.Left, variable);
            var second = Parser.HasVar(expression.Right, variable);

            if (first && second)
            {
                var mul1 = new Mul(_Differentiate(expression.Left.Clone(), variable), expression.Right.Clone());
                var mul2 = new Mul(expression.Left.Clone(), _Differentiate(expression.Right.Clone(), variable));
                var add = new Add(mul1, mul2);

                return add;
            }

            if (first)
                return new Mul(_Differentiate(expression.Left.Clone(), variable), expression.Right.Clone());
            if (second)
                return new Mul(expression.Left.Clone(), _Differentiate(expression.Right.Clone(), variable));

            return new Number(0);
        }

        protected virtual IExpression Pow(Pow expression, Variable variable)
        {
            if (Parser.HasVar(expression.Left, variable))
            {
                var sub = new Sub(expression.Right.Clone(), new Number(1));
                var inv = new Pow(expression.Left.Clone(), sub);
                var mul1 = new Mul(expression.Right.Clone(), inv);
                var mul2 = new Mul(_Differentiate(expression.Left.Clone(), variable), mul1);

                return mul2;
            }
            if (Parser.HasVar(expression.Right, variable))
            {
                var ln = new Ln(expression.Left.Clone());
                var mul1 = new Mul(ln, expression.Clone());
                var mul2 = new Mul(mul1, _Differentiate(expression.Right.Clone(), variable));

                return mul2;
            }

            return new Number(0);
        }

        protected virtual IExpression Root(Root expression, Variable variable)
        {
            if (Parser.HasVar(expression.Left, variable) || Parser.HasVar(expression.Right, variable))
            {
                var div = new Div(new Number(1), expression.Right.Clone());
                var pow = new Pow(expression.Left.Clone(), div);

                return Pow(pow, variable);
            }

            return new Number(0);
        }

        protected virtual IExpression Sqrt(Sqrt expression, Variable variable)
        {
            var mul = new Mul(new Number(2), expression.Clone());
            var div = new Div(_Differentiate(expression.Argument.Clone(), variable), mul);

            return div;
        }

        protected virtual IExpression Sub(Sub expression, Variable variable)
        {
            var first = Parser.HasVar(expression.Left, variable);
            var second = Parser.HasVar(expression.Right, variable);

            if (first && second)
                return new Sub(_Differentiate(expression.Left.Clone(), variable), _Differentiate(expression.Right.Clone(), variable));
            if (first)
                return _Differentiate(expression.Left.Clone(), variable);
            if (second)
                return new UnaryMinus(_Differentiate(expression.Right.Clone(), variable));

            return new Number(0);
        }

        protected virtual IExpression UnaryMinus(UnaryMinus expression, Variable variable)
        {
            return new UnaryMinus(_Differentiate(expression.Argument.Clone(), variable));
        }

        #endregion Common

        #region Trigonometric

        protected virtual IExpression Arccos(Arccos expression, Variable variable)
        {

        }

        protected virtual IExpression Arccot(Arccot expression, Variable variable)
        {

        }

        protected virtual IExpression Arccsc(Arccsc expression, Variable variable)
        {

        }

        protected virtual IExpression Arcsec(Arcsec expression, Variable variable)
        {

        }

        protected virtual IExpression Arcsin(Arcsin expression, Variable variable)
        {

        }

        protected virtual IExpression Arctan(Arctan expression, Variable variable)
        {

        }

        protected virtual IExpression Cos(Cos expression, Variable variable)
        {

        }

        protected virtual IExpression Cot(Cot expression, Variable variable)
        {

        }

        protected virtual IExpression Csc(Csc expression, Variable variable)
        {

        }

        protected virtual IExpression Sec(Sec expression, Variable variable)
        {

        }

        protected virtual IExpression Sin(Sin expression, Variable variable)
        {

        }

        protected virtual IExpression Tan(Tan expression, Variable variable)
        {
            var cos = new Cos(expression.Argument.Clone());
            var inv = new Pow(cos, new Number(2));
            var div = new Div(_Differentiate(expression.Argument.Clone(), variable), inv);

            return div;
        }

        #endregion Trigonometric

        #region Hyperbolic



        #endregion Hyperbolic

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
