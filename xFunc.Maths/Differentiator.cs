// Copyright 2012-2016 Dmitry Kischenko
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
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Maths
{

    /// <summary>
    /// The differentiator of expressions.
    /// </summary>
    public class Differentiator : IDifferentiator
    {

        private ISimplifier simplifier;
        private bool simplify = true;

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
            return Differentiate(expression, new Variable("x"), null);
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        public IExpression Differentiate(IExpression expression, Variable variable)
        {
            return Differentiate(expression, variable, null);
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// Returns the derivative.
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="expression"/> or <paramref name="variable"/> is null.</exception>
        public IExpression Differentiate(IExpression expression, Variable variable, ExpressionParameters parameters)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            if (variable == null)
                variable = new Variable("x");

            if (!Helpers.HasVar(expression, variable))
                return new Number(0);

            var deriv = expression as Derivative;
            if (deriv != null)
                expression = deriv.Expression;

            if (simplify)
                return simplifier.Simplify(_Differentiate(expression, variable, parameters));

            return _Differentiate(expression, variable, parameters);
        }

        private IExpression _Differentiate(IExpression expression, Variable variable)
        {
            return _Differentiate(expression, variable, null);
        }

        private IExpression _Differentiate(IExpression expression, Variable variable, ExpressionParameters parameters)
        {
            var number = expression as Number;
            if (number != null)
                return Number(number, variable);
            var @var = expression as Variable;
            if (@var != null)
                return Variable(@var, variable);
            var deriv = expression as Derivative;
            if (deriv != null)
                expression = _Differentiate(deriv.Expression, deriv.Variable, parameters);
            var simp = expression as Simplify;
            if (simp != null)
                expression = simp.Argument;

            var abs = expression as Abs;
            if (abs != null)
                return Abs(abs, variable);
            var add = expression as Add;
            if (add != null)
                return Add(add, variable);
            var div = expression as Div;
            if (div != null)
                return Div(div, variable);
            var exp = expression as Exp;
            if (exp != null)
                return Exp(exp, variable);
            var ln = expression as Ln;
            if (ln != null)
                return Ln(ln, variable);
            var lg = expression as Lg;
            if (lg != null)
                return Lg(lg, variable);
            var log = expression as Log;
            if (log != null)
                return Log(log, variable);
            var mul = expression as Mul;
            if (mul != null)
                return Mul(mul, variable);
            var pow = expression as Pow;
            if (pow != null)
                return Pow(pow, variable);
            var root = expression as Root;
            if (root != null)
                return Root(root, variable);
            var sqrt = expression as Sqrt;
            if (sqrt != null)
                return Sqrt(sqrt, variable);
            var sub = expression as Sub;
            if (sub != null)
                return Sub(sub, variable);
            var minus = expression as UnaryMinus;
            if (minus != null)
                return UnaryMinus(minus, variable);
            var function = expression as UserFunction;
            if (function != null)
                return UserFunction(function, variable, parameters);

            var sin = expression as Sin;
            if (sin != null)
                return Sin(sin, variable);
            var cos = expression as Cos;
            if (cos != null)
                return Cos(cos, variable);
            var tan = expression as Tan;
            if (tan != null)
                return Tan(tan, variable);
            var cot = expression as Cot;
            if (cot != null)
                return Cot(cot, variable);
            var sec = expression as Sec;
            if (sec != null)
                return Sec(sec, variable);
            var csc = expression as Csc;
            if (csc != null)
                return Csc(csc, variable);
            var arcsin = expression as Arcsin;
            if (arcsin != null)
                return Arcsin(arcsin, variable);
            var arccos = expression as Arccos;
            if (arccos != null)
                return Arccos(arccos, variable);
            var arctan = expression as Arctan;
            if (arctan != null)
                return Arctan(arctan, variable);
            var arccot = expression as Arccot;
            if (arccot != null)
                return Arccot(arccot, variable);
            var arcsec = expression as Arcsec;
            if (arcsec != null)
                return Arcsec(arcsec, variable);
            var arccsc = expression as Arccsc;
            if (arccsc != null)
                return Arccsc(arccsc, variable);

            var sinh = expression as Sinh;
            if (sinh != null)
                return Sinh(sinh, variable);
            var cosh = expression as Cosh;
            if (cosh != null)
                return Cosh(cosh, variable);
            var tanh = expression as Tanh;
            if (tanh != null)
                return Tanh(tanh, variable);
            var coth = expression as Coth;
            if (coth != null)
                return Coth(coth, variable);
            var sech = expression as Sech;
            if (sech != null)
                return Sech(sech, variable);
            var csch = expression as Csch;
            if (csch != null)
                return Csch(csch, variable);
            var arsinh = expression as Arsinh;
            if (arsinh != null)
                return Arsinh(arsinh, variable);
            var arcosh = expression as Arcosh;
            if (arcosh != null)
                return Arcosh(arcosh, variable);
            var artanh = expression as Artanh;
            if (artanh != null)
                return Artanh(artanh, variable);
            var arcoth = expression as Arcoth;
            if (arcoth != null)
                return Arcoth(arcoth, variable);
            var arsech = expression as Arsech;
            if (arsech != null)
                return Arsech(arsech, variable);
            var arcsch = expression as Arcsch;
            if (arcsch != null)
                return Arcsch(arcsch, variable);

            throw new NotSupportedException();
        }

        #region Common

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Number(Number expression, Variable variable)
        {
            return new Number(0);
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Variable(Variable expression, Variable variable)
        {
            if (expression.Equals(variable))
                return new Number(1);

            return expression.Clone();
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Abs(Abs expression, Variable variable)
        {
            var div = new Div(expression.Argument.Clone(), expression.Clone());
            var mul = new Mul(_Differentiate(expression.Argument.Clone(), variable), div);

            return mul;
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Add(Add expression, Variable variable)
        {
            var first = Helpers.HasVar(expression.Left, variable);
            var second = Helpers.HasVar(expression.Right, variable);

            if (first && second)
                return new Add(_Differentiate(expression.Left.Clone(), variable), _Differentiate(expression.Right.Clone(), variable));
            if (first)
                return _Differentiate(expression.Left.Clone(), variable);
            if (second)
                return _Differentiate(expression.Right.Clone(), variable);

            return new Number(0);
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Div(Div expression, Variable variable)
        {
            var first = Helpers.HasVar(expression.Left, variable);
            var second = Helpers.HasVar(expression.Right, variable);

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

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Exp(Exp expression, Variable variable)
        {
            return new Mul(_Differentiate(expression.Argument.Clone(), variable), expression.Clone());
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Ln(Ln expression, Variable variable)
        {
            return new Div(_Differentiate(expression.Argument.Clone(), variable), expression.Argument.Clone());
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Lg(Lg expression, Variable variable)
        {
            var ln = new Ln(new Number(10));
            var mul1 = new Mul(expression.Argument.Clone(), ln);
            var div = new Div(_Differentiate(expression.Argument.Clone(), variable), mul1);

            return div;
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Log(Log expression, Variable variable)
        {
            if (Helpers.HasVar(expression.Left, variable))
            {
                var ln1 = new Ln(expression.Right.Clone());
                var ln2 = new Ln(expression.Left.Clone());
                var div = new Div(ln1, ln2);

                return Div(div, variable);
            }

            // if (Helpers.HasVar(expression.Right, variable))
            var ln = new Ln(expression.Left.Clone());
            var mul = new Mul(expression.Right.Clone(), ln);
            var div2 = new Div(_Differentiate(expression.Right.Clone(), variable), mul);

            return div2;
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Mul(Mul expression, Variable variable)
        {
            var first = Helpers.HasVar(expression.Left, variable);
            var second = Helpers.HasVar(expression.Right, variable);

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

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Pow(Pow expression, Variable variable)
        {
            if (Helpers.HasVar(expression.Left, variable))
            {
                var sub = new Sub(expression.Right.Clone(), new Number(1));
                var inv = new Pow(expression.Left.Clone(), sub);
                var mul1 = new Mul(expression.Right.Clone(), inv);
                var mul2 = new Mul(_Differentiate(expression.Left.Clone(), variable), mul1);

                return mul2;
            }

            // if (Helpers.HasVar(expression.Right, variable))
            var ln = new Ln(expression.Left.Clone());
            var mul3 = new Mul(ln, expression.Clone());
            var mul4 = new Mul(mul3, _Differentiate(expression.Right.Clone(), variable));

            return mul4;
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Root(Root expression, Variable variable)
        {
            var div = new Div(new Number(1), expression.Right.Clone());
            var pow = new Pow(expression.Left.Clone(), div);

            return Pow(pow, variable);
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Sqrt(Sqrt expression, Variable variable)
        {
            var mul = new Mul(new Number(2), expression.Clone());
            var div = new Div(_Differentiate(expression.Argument.Clone(), variable), mul);

            return div;
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Sub(Sub expression, Variable variable)
        {
            var first = Helpers.HasVar(expression.Left, variable);
            var second = Helpers.HasVar(expression.Right, variable);

            if (first && second)
                return new Sub(_Differentiate(expression.Left.Clone(), variable), _Differentiate(expression.Right.Clone(), variable));
            if (first)
                return _Differentiate(expression.Left.Clone(), variable);
            if (second)
                return new UnaryMinus(_Differentiate(expression.Right.Clone(), variable));

            return new Number(0);
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression UnaryMinus(UnaryMinus expression, Variable variable)
        {
            return new UnaryMinus(_Differentiate(expression.Argument.Clone(), variable));
        }

        #endregion Common

        #region Trigonometric

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Arccos(Arccos expression, Variable variable)
        {
            var pow = new Pow(expression.Argument.Clone(), new Number(2));
            var sub = new Sub(new Number(1), pow);
            var sqrt = new Sqrt(sub);
            var division = new Div(_Differentiate(expression.Argument.Clone(), variable), sqrt);
            var unMinus = new UnaryMinus(division);

            return unMinus;
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Arccot(Arccot expression, Variable variable)
        {
            var involution = new Pow(expression.Argument.Clone(), new Number(2));
            var add = new Add(new Number(1), involution);
            var div = new Div(_Differentiate(expression.Argument.Clone(), variable), add);
            var unMinus = new UnaryMinus(div);

            return unMinus;
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Arccsc(Arccsc expression, Variable variable)
        {
            var abs = new Abs(expression.Argument.Clone());
            var sqr = new Pow(expression.Argument.Clone(), new Number(2));
            var sub = new Sub(sqr, new Number(1));
            var sqrt = new Sqrt(sub);
            var mul = new Mul(abs, sqrt);
            var div = new Div(_Differentiate(expression.Argument.Clone(), variable), mul);
            var unary = new UnaryMinus(div);

            return unary;
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Arcsec(Arcsec expression, Variable variable)
        {
            var abs = new Abs(expression.Argument.Clone());
            var sqr = new Pow(expression.Argument.Clone(), new Number(2));
            var sub = new Sub(sqr, new Number(1));
            var sqrt = new Sqrt(sub);
            var mul = new Mul(abs, sqrt);
            var div = new Div(_Differentiate(expression.Argument.Clone(), variable), mul);

            return div;
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Arcsin(Arcsin expression, Variable variable)
        {
            var involution = new Pow(expression.Argument.Clone(), new Number(2));
            var sub = new Sub(new Number(1), involution);
            var sqrt = new Sqrt(sub);
            var division = new Div(_Differentiate(expression.Argument.Clone(), variable), sqrt);

            return division;
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Arctan(Arctan expression, Variable variable)
        {
            var involution = new Pow(expression.Argument.Clone(), new Number(2));
            var add = new Add(new Number(1), involution);
            var div = new Div(_Differentiate(expression.Argument.Clone(), variable), add);

            return div;
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Cos(Cos expression, Variable variable)
        {
            var sine = new Sin(expression.Argument.Clone());
            var multiplication = new Mul(sine, _Differentiate(expression.Argument.Clone(), variable));
            var unMinus = new UnaryMinus(multiplication);

            return unMinus;
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Cot(Cot expression, Variable variable)
        {
            var sine = new Sin(expression.Argument.Clone());
            var involution = new Pow(sine, new Number(2));
            var division = new Div(_Differentiate(expression.Argument.Clone(), variable), involution);
            var unMinus = new UnaryMinus(division);

            return unMinus;
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Csc(Csc expression, Variable variable)
        {
            var unary = new UnaryMinus(_Differentiate(expression.Argument.Clone(), variable));
            var cot = new Cot(expression.Argument.Clone());
            var csc = new Csc(expression.Argument.Clone());
            var mul1 = new Mul(cot, csc);
            var mul2 = new Mul(unary, mul1);

            return mul2;
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Sec(Sec expression, Variable variable)
        {
            var tan = new Tan(expression.Argument.Clone());
            var sec = new Sec(expression.Argument.Clone());
            var mul1 = new Mul(tan, sec);
            var mul2 = new Mul(_Differentiate(expression.Argument.Clone(), variable), mul1);

            return mul2;
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Sin(Sin expression, Variable variable)
        {
            var cos = new Cos(expression.Argument.Clone());
            var mul = new Mul(cos, _Differentiate(expression.Argument.Clone(), variable));

            return mul;
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Tan(Tan expression, Variable variable)
        {
            var cos = new Cos(expression.Argument.Clone());
            var inv = new Pow(cos, new Number(2));
            var div = new Div(_Differentiate(expression.Argument.Clone(), variable), inv);

            return div;
        }

        #endregion Trigonometric

        #region Hyperbolic

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Arcosh(Arcosh expression, Variable variable)
        {
            var sqr = new Pow(expression.Argument.Clone(), new Number(2));
            var sub = new Sub(sqr, new Number(1));
            var sqrt = new Sqrt(sub);
            var div = new Div(_Differentiate(expression.Argument.Clone(), variable), sqrt);

            return div;
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Arcoth(Arcoth expression, Variable variable)
        {
            var sqr = new Pow(expression.Argument.Clone(), new Number(2));
            var sub = new Sub(new Number(1), sqr);
            var div = new Div(_Differentiate(expression.Argument.Clone(), variable), sub);

            return div;
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Arcsch(Arcsch expression, Variable variable)
        {
            var inv = new Pow(expression.Argument.Clone(), new Number(2));
            var add = new Add(new Number(1), inv);
            var sqrt = new Sqrt(add);
            var abs = new Abs(expression.Argument.Clone());
            var mul = new Mul(abs, sqrt);
            var div = new Div(_Differentiate(expression.Argument.Clone(), variable), mul);
            var unMinus = new UnaryMinus(div);

            return unMinus;
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Arsech(Arsech expression, Variable variable)
        {
            var inv = new Pow(expression.Argument.Clone(), new Number(2));
            var sub = new Sub(new Number(1), inv);
            var sqrt = new Sqrt(sub);
            var mul = new Mul(expression.Argument.Clone(), sqrt);
            var div = new Div(_Differentiate(expression.Argument.Clone(), variable), mul);
            var unMinus = new UnaryMinus(div);

            return unMinus;
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Arsinh(Arsinh expression, Variable variable)
        {
            var sqr = new Pow(expression.Argument.Clone(), new Number(2));
            var add = new Add(sqr, new Number(1));
            var sqrt = new Sqrt(add);
            var div = new Div(_Differentiate(expression.Argument.Clone(), variable), sqrt);

            return div;
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Artanh(Artanh expression, Variable variable)
        {
            var sqr = new Pow(expression.Argument.Clone(), new Number(2));
            var sub = new Sub(new Number(1), sqr);
            var div = new Div(_Differentiate(expression.Argument.Clone(), variable), sub);

            return div;
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Cosh(Cosh expression, Variable variable)
        {
            var sinh = new Sinh(expression.Argument.Clone());
            var mul = new Mul(_Differentiate(expression.Argument.Clone(), variable), sinh);

            return mul;
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Coth(Coth expression, Variable variable)
        {
            var sinh = new Sinh(expression.Argument.Clone());
            var inv = new Pow(sinh, new Number(2));
            var div = new Div(_Differentiate(expression.Argument.Clone(), variable), inv);
            var unMinus = new UnaryMinus(div);

            return unMinus;
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Csch(Csch expression, Variable variable)
        {
            var coth = new Coth(expression.Argument.Clone());
            var mul1 = new Mul(coth, expression.Clone());
            var mul2 = new Mul(_Differentiate(expression.Argument.Clone(), variable), mul1);
            var unMinus = new UnaryMinus(mul2);

            return unMinus;
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Sech(Sech expression, Variable variable)
        {
            var tanh = new Tanh(expression.Argument.Clone());
            var mul1 = new Mul(tanh, expression.Clone());
            var mul2 = new Mul(_Differentiate(expression.Argument.Clone(), variable), mul1);
            var unMinus = new UnaryMinus(mul2);

            return unMinus;
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Sinh(Sinh expression, Variable variable)
        {
            var cosh = new Cosh(expression.Argument.Clone());
            var mul = new Mul(_Differentiate(expression.Argument.Clone(), variable), cosh);

            return mul;
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression Tanh(Tanh expression, Variable variable)
        {
            var cosh = new Cosh(expression.Argument.Clone());
            var inv = new Pow(cosh, new Number(2));
            var div = new Div(_Differentiate(expression.Argument.Clone(), variable), inv);

            return div;
        }

        #endregion Hyperbolic

        /// <summary>
        /// Differentiates the user function.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Returns the derivative.</returns>
        protected virtual IExpression UserFunction(UserFunction expression, Variable variable, ExpressionParameters parameters)
        {
            var func = parameters.Functions[expression];

            return _Differentiate(func, variable, parameters);
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

        /// <summary>
        /// Enable/disable simplification.
        /// </summary>
        /// <value>
        ///   <c>true</c> if simplify; otherwise, <c>false</c>.
        /// </value>
        public bool Simplify
        {
            get
            {
                return simplify;
            }
            set
            {
                simplify = value;
            }
        }

    }

}
