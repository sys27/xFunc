using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Maths
{

    public class MathBuilder
    {

        private IMathExpression expression;

        public MathBuilder()
        {

        }

        public MathBuilder(IMathExpression expression)
        {
            this.expression = expression;
        }

        public MathBuilder Variable(string name)
        {
            expression = new Variable(name);

            return this;
        }

        public MathBuilder Number(double number)
        {
            expression = new Number(number);

            return this;
        }

        #region Trigonometric

        public MathBuilder Sin()
        {
            expression = new Sin(expression);

            return this;
        }

        public MathBuilder Sin(IMathExpression exp)
        {
            expression = new Sin(exp);

            return this;
        }

        public MathBuilder Cos()
        {
            expression = new Cos(expression);

            return this;
        }

        public MathBuilder Cos(IMathExpression exp)
        {
            expression = new Cos(exp);

            return this;
        }

        public MathBuilder Tan()
        {
            expression = new Tan(expression);

            return this;
        }

        public MathBuilder Tan(IMathExpression exp)
        {
            expression = new Tan(exp);

            return this;
        }

        public MathBuilder Cot()
        {
            expression = new Cot(expression);

            return this;
        }

        public MathBuilder Cot(IMathExpression exp)
        {
            expression = new Cot(exp);

            return this;
        }

        public MathBuilder Sec()
        {
            expression = new Sec(expression);

            return this;
        }

        public MathBuilder Sec(IMathExpression exp)
        {
            expression = new Sec(exp);

            return this;
        }

        public MathBuilder Csc()
        {
            expression = new Csc(expression);

            return this;
        }

        public MathBuilder Csc(IMathExpression exp)
        {
            expression = new Csc(exp);

            return this;
        }

        #endregion

        #region Inverse trigonometric

        public MathBuilder Arcsin()
        {
            expression = new Arcsin(expression);

            return this;
        }

        public MathBuilder Arcsin(IMathExpression exp)
        {
            expression = new Arcsin(exp);

            return this;
        }

        public MathBuilder Arccos()
        {
            expression = new Arccos(expression);

            return this;
        }

        public MathBuilder Arccos(IMathExpression exp)
        {
            expression = new Arccos(exp);

            return this;
        }

        public MathBuilder Arctan()
        {
            expression = new Arctan(expression);

            return this;
        }

        public MathBuilder Arctan(IMathExpression exp)
        {
            expression = new Arctan(exp);

            return this;
        }

        public MathBuilder Arccot()
        {
            expression = new Arccot(expression);

            return this;
        }

        public MathBuilder Arccot(IMathExpression exp)
        {
            expression = new Arccot(exp);

            return this;
        }

        public MathBuilder Arcsec()
        {
            expression = new Arcsec(expression);

            return this;
        }

        public MathBuilder Arcsec(IMathExpression exp)
        {
            expression = new Arcsec(exp);

            return this;
        }

        public MathBuilder Arccsc()
        {
            expression = new Arccsc(expression);

            return this;
        }

        public MathBuilder Arccsc(IMathExpression exp)
        {
            expression = new Arccsc(exp);

            return this;
        }

        #endregion

        #region Hyperbolic

        public MathBuilder Sinh()
        {
            expression = new Sinh(expression);

            return this;
        }

        public MathBuilder Sinh(IMathExpression exp)
        {
            expression = new Sinh(exp);

            return this;
        }

        public MathBuilder Cosh()
        {
            expression = new Cosh(expression);

            return this;
        }

        public MathBuilder Cosh(IMathExpression exp)
        {
            expression = new Cosh(exp);

            return this;
        }

        public MathBuilder Tanh()
        {
            expression = new Tanh(expression);

            return this;
        }

        public MathBuilder Tanh(IMathExpression exp)
        {
            expression = new Tanh(exp);

            return this;
        }

        public MathBuilder Coth()
        {
            expression = new Coth(expression);

            return this;
        }

        public MathBuilder Coth(IMathExpression exp)
        {
            expression = new Coth(exp);

            return this;
        }

        public MathBuilder Sech()
        {
            expression = new Sech(expression);

            return this;
        }

        public MathBuilder Sech(IMathExpression exp)
        {
            expression = new Sech(exp);

            return this;
        }

        public MathBuilder Csch()
        {
            expression = new Csch(expression);

            return this;
        }

        public MathBuilder Csch(IMathExpression exp)
        {
            expression = new Csch(exp);

            return this;
        }

        #endregion

        #region Inverse hyperbolic

        public MathBuilder Arsinh()
        {
            expression = new Arsinh(expression);

            return this;
        }

        public MathBuilder Arsinh(IMathExpression exp)
        {
            expression = new Arsinh(exp);

            return this;
        }

        public MathBuilder Arcosh()
        {
            expression = new Arcosh(expression);

            return this;
        }

        public MathBuilder Arcosh(IMathExpression exp)
        {
            expression = new Arcosh(exp);

            return this;
        }

        public MathBuilder Artanh()
        {
            expression = new Artanh(expression);

            return this;
        }

        public MathBuilder Artanh(IMathExpression exp)
        {
            expression = new Artanh(exp);

            return this;
        }

        public MathBuilder Arcoth()
        {
            expression = new Arcoth(expression);

            return this;
        }

        public MathBuilder Arcoth(IMathExpression exp)
        {
            expression = new Arcoth(exp);

            return this;
        }

        public MathBuilder Arsech()
        {
            expression = new Arsech(expression);

            return this;
        }

        public MathBuilder Arsech(IMathExpression exp)
        {
            expression = new Arsech(exp);

            return this;
        }

        public MathBuilder Arcsch()
        {
            expression = new Arcsch(expression);

            return this;
        }

        public MathBuilder Arcsch(IMathExpression exp)
        {
            expression = new Arcsch(exp);

            return this;
        }

        #endregion

        public IMathExpression Expression
        {
            get
            {
                return expression;
            }
        }

    }

}
