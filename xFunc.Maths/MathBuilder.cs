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

        public MathBuilder Sine()
        {
            expression = new Sine(expression);

            return this;
        }

        public MathBuilder Sine(IMathExpression exp)
        {
            expression = new Sine(exp);

            return this;
        }

        public MathBuilder Cosine()
        {
            expression = new Cosine(expression);

            return this;
        }

        public MathBuilder Cosine(IMathExpression exp)
        {
            expression = new Cosine(exp);

            return this;
        }

        public MathBuilder Tangent()
        {
            expression = new Tangent(expression);

            return this;
        }

        public MathBuilder Tangent(IMathExpression exp)
        {
            expression = new Tangent(exp);

            return this;
        }

        public MathBuilder Cotangent()
        {
            expression = new Cotangent(expression);

            return this;
        }

        public MathBuilder Cotangent(IMathExpression exp)
        {
            expression = new Cotangent(exp);

            return this;
        }

        public MathBuilder Secant()
        {
            expression = new Secant(expression);

            return this;
        }

        public MathBuilder Secant(IMathExpression exp)
        {
            expression = new Secant(exp);

            return this;
        }

        public MathBuilder Cosecant()
        {
            expression = new Cosecant(expression);

            return this;
        }

        public MathBuilder Cosecant(IMathExpression exp)
        {
            expression = new Cosecant(exp);

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

        public MathBuilder HyperbolicSine()
        {
            expression = new HyperbolicSine(expression);

            return this;
        }

        public MathBuilder HyperbolicSine(IMathExpression exp)
        {
            expression = new HyperbolicSine(exp);

            return this;
        }

        public MathBuilder HyperbolicCosine()
        {
            expression = new HyperbolicCosine(expression);

            return this;
        }

        public MathBuilder HyperbolicCosine(IMathExpression exp)
        {
            expression = new HyperbolicCosine(exp);

            return this;
        }

        public MathBuilder HyperbolicTangent()
        {
            expression = new HyperbolicTangent(expression);

            return this;
        }

        public MathBuilder HyperbolicTangent(IMathExpression exp)
        {
            expression = new HyperbolicTangent(exp);

            return this;
        }

        public MathBuilder HyperbolicCotangent()
        {
            expression = new HyperbolicCotangent(expression);

            return this;
        }

        public MathBuilder HyperbolicCotangent(IMathExpression exp)
        {
            expression = new HyperbolicCotangent(exp);

            return this;
        }

        public MathBuilder HyperbolicSecant()
        {
            expression = new HyperbolicSecant(expression);

            return this;
        }

        public MathBuilder HyperbolicSecant(IMathExpression exp)
        {
            expression = new HyperbolicSecant(exp);

            return this;
        }

        public MathBuilder HyperbolicCosecant()
        {
            expression = new HyperbolicCosecant(expression);

            return this;
        }

        public MathBuilder HyperbolicCosecant(IMathExpression exp)
        {
            expression = new HyperbolicCosecant(exp);

            return this;
        }

        #endregion

        #region Inverse hyperbolic

        public MathBuilder HyperbolicArsine()
        {
            expression = new HyperbolicArsine(expression);

            return this;
        }

        public MathBuilder HyperbolicArsine(IMathExpression exp)
        {
            expression = new HyperbolicArsine(exp);

            return this;
        }

        public MathBuilder HyperbolicArcosine()
        {
            expression = new HyperbolicArcosine(expression);

            return this;
        }

        public MathBuilder HyperbolicArcosine(IMathExpression exp)
        {
            expression = new HyperbolicArcosine(exp);

            return this;
        }

        public MathBuilder HyperbolicArtangent()
        {
            expression = new HyperbolicArtangent(expression);

            return this;
        }

        public MathBuilder HyperbolicArtangent(IMathExpression exp)
        {
            expression = new HyperbolicArtangent(exp);

            return this;
        }

        public MathBuilder HyperbolicArcotangent()
        {
            expression = new HyperbolicArcotangent(expression);

            return this;
        }

        public MathBuilder HyperbolicArcotangent(IMathExpression exp)
        {
            expression = new HyperbolicArcotangent(exp);

            return this;
        }

        public MathBuilder HyperbolicArsecant()
        {
            expression = new HyperbolicArsecant(expression);

            return this;
        }

        public MathBuilder HyperbolicArsecant(IMathExpression exp)
        {
            expression = new HyperbolicArsecant(exp);

            return this;
        }

        public MathBuilder HyperbolicArcosecant()
        {
            expression = new HyperbolicArcosecant(expression);

            return this;
        }

        public MathBuilder HyperbolicArcosecant(IMathExpression exp)
        {
            expression = new HyperbolicArcosecant(exp);

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
