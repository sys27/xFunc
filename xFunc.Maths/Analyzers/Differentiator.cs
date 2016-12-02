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

namespace xFunc.Maths.Analyzers
{

    public class Differentiator : Analyzer<IExpression>, IDifferentiator
    {

        private Variable variable;
        private ExpressionParameters parameters;

        public Differentiator() : this(new ExpressionParameters(), new Variable("x")) { }

        public Differentiator(Variable variable) : this(new ExpressionParameters(), variable) { }

        public Differentiator(ExpressionParameters parameters, Variable variable)
        {
            this.parameters = parameters;
            this.variable = variable;
        }

        #region Standard

        public override IExpression Analyze(Abs exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            var div = new Div(exp.Argument.Clone(), exp.Clone());
            var mul = new Mul(exp.Argument.Clone().Analyze(this), div);

            return mul;
        }

        public override IExpression Analyze(Add exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            var first = Helpers.HasVar(exp.Left, variable);
            var second = Helpers.HasVar(exp.Right, variable);

            if (first && second)
                return new Add(exp.Left.Clone().Analyze(this), exp.Right.Clone().Analyze(this));
            if (first)
                return exp.Left.Clone().Analyze(this);
            if (second)
                return exp.Right.Clone().Analyze(this);

            return new Number(0);
        }

        public override IExpression Analyze(Derivative exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            var diff = exp.Expression.Analyze(this);
            if (exp.Parent is Derivative)
                diff = diff.Analyze(this);

            return diff;
        }

        public override IExpression Analyze(Div exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            var first = Helpers.HasVar(exp.Left, variable);
            var second = Helpers.HasVar(exp.Right, variable);

            if (first && second)
            {
                var mul1 = new Mul(exp.Left.Clone().Analyze(this), exp.Right.Clone());
                var mul2 = new Mul(exp.Left.Clone(), exp.Right.Clone().Analyze(this));
                var sub = new Sub(mul1, mul2);
                var inv = new Pow(exp.Right.Clone(), new Number(2));
                var division = new Div(sub, inv);

                return division;
            }
            if (first)
            {
                return new Div(exp.Left.Clone().Analyze(this), exp.Right.Clone());
            }
            if (second)
            {
                var mul2 = new Mul(exp.Left.Clone(), exp.Right.Clone().Analyze(this));
                var unMinus = new UnaryMinus(mul2);
                var inv = new Pow(exp.Right.Clone(), new Number(2));
                var division = new Div(unMinus, inv);

                return division;
            }

            return new Number(0);
        }

        public override IExpression Analyze(Exp exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            return new Mul(exp.Argument.Clone().Analyze(this), exp.Clone());
        }

        public override IExpression Analyze(Lb exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            var ln = new Ln(new Number(2));
            var mul = new Mul(exp.Argument.Clone(), ln);
            var div = new Div(exp.Argument.Clone().Analyze(this), mul);

            return div;
        }

        public override IExpression Analyze(Lg exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            var ln = new Ln(new Number(10));
            var mul1 = new Mul(exp.Argument.Clone(), ln);
            var div = new Div(exp.Argument.Clone().Analyze(this), mul1);

            return div;
        }

        public override IExpression Analyze(Ln exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            return new Div(exp.Argument.Clone().Analyze(this), exp.Argument.Clone());
        }

        public override IExpression Analyze(Log exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            if (Helpers.HasVar(exp.Left, variable))
            {
                var ln1 = new Ln(exp.Right.Clone());
                var ln2 = new Ln(exp.Left.Clone());
                var div = new Div(ln1, ln2);

                return Analyze(div);
            }

            // if (Helpers.HasVar(exp.Right, variable))
            var ln = new Ln(exp.Left.Clone());
            var mul = new Mul(exp.Right.Clone(), ln);
            var div2 = new Div(exp.Right.Clone().Analyze(this), mul);

            return div2;
        }

        public override IExpression Analyze(Mul exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            var first = Helpers.HasVar(exp.Left, variable);
            var second = Helpers.HasVar(exp.Right, variable);

            if (first && second)
            {
                var mul1 = new Mul(exp.Left.Clone().Analyze(this), exp.Right.Clone());
                var mul2 = new Mul(exp.Left.Clone(), exp.Right.Clone().Analyze(this));
                var add = new Add(mul1, mul2);

                return add;
            }

            if (first)
                return new Mul(exp.Left.Clone().Analyze(this), exp.Right.Clone());
            if (second)
                return new Mul(exp.Left.Clone(), exp.Right.Clone().Analyze(this));

            return new Number(0);
        }

        public override IExpression Analyze(Number exp)
        {
            return new Number(0);
        }

        public override IExpression Analyze(Pow exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            if (Helpers.HasVar(exp.Left, variable))
            {
                var sub = new Sub(exp.Right.Clone(), new Number(1));
                var inv = new Pow(exp.Left.Clone(), sub);
                var mul1 = new Mul(exp.Right.Clone(), inv);
                var mul2 = new Mul(exp.Left.Clone().Analyze(this), mul1);

                return mul2;
            }

            // if (Helpers.HasVar(exp.Right, variable))
            var ln = new Ln(exp.Left.Clone());
            var mul3 = new Mul(ln, exp.Clone());
            var mul4 = new Mul(mul3, exp.Right.Clone().Analyze(this));

            return mul4;
        }

        public override IExpression Analyze(Root exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            var div = new Div(new Number(1), exp.Right.Clone());
            var pow = new Pow(exp.Left.Clone(), div);

            return Analyze(pow);
        }

        public override IExpression Analyze(Simplify exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            return exp.Argument.Analyze(this);
        }

        public override IExpression Analyze(Sqrt exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            var mul = new Mul(new Number(2), exp.Clone());
            var div = new Div(exp.Argument.Clone().Analyze(this), mul);

            return div;
        }

        public override IExpression Analyze(Sub exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            var first = Helpers.HasVar(exp.Left, variable);
            var second = Helpers.HasVar(exp.Right, variable);

            if (first && second)
                return new Sub(exp.Left.Clone().Analyze(this), exp.Right.Clone().Analyze(this));
            if (first)
                return exp.Left.Clone().Analyze(this);
            if (second)
                return new UnaryMinus(exp.Right.Clone().Analyze(this));

            return new Number(0);
        }

        public override IExpression Analyze(UnaryMinus exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            return new UnaryMinus(exp.Argument.Clone().Analyze(this));
        }

        public override IExpression Analyze(UserFunction exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            return parameters.Functions[exp].Analyze(this);
        }

        public override IExpression Analyze(Variable exp)
        {
            if (exp.Equals(variable))
                return new Number(1);

            return exp.Clone();
        }

        #endregion Standard

        #region Trigonometric

        public override IExpression Analyze(Arccos exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            var pow = new Pow(exp.Argument.Clone(), new Number(2));
            var sub = new Sub(new Number(1), pow);
            var sqrt = new Sqrt(sub);
            var division = new Div(exp.Argument.Clone().Analyze(this), sqrt);
            var unMinus = new UnaryMinus(division);

            return unMinus;
        }

        public override IExpression Analyze(Arccot exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            var involution = new Pow(exp.Argument.Clone(), new Number(2));
            var add = new Add(new Number(1), involution);
            var div = new Div(exp.Argument.Clone().Analyze(this), add);
            var unMinus = new UnaryMinus(div);

            return unMinus;
        }

        public override IExpression Analyze(Arccsc exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            var abs = new Abs(exp.Argument.Clone());
            var sqr = new Pow(exp.Argument.Clone(), new Number(2));
            var sub = new Sub(sqr, new Number(1));
            var sqrt = new Sqrt(sub);
            var mul = new Mul(abs, sqrt);
            var div = new Div(exp.Argument.Clone().Analyze(this), mul);
            var unary = new UnaryMinus(div);

            return unary;
        }

        public override IExpression Analyze(Arcsec exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            var abs = new Abs(exp.Argument.Clone());
            var sqr = new Pow(exp.Argument.Clone(), new Number(2));
            var sub = new Sub(sqr, new Number(1));
            var sqrt = new Sqrt(sub);
            var mul = new Mul(abs, sqrt);
            var div = new Div(exp.Argument.Clone().Analyze(this), mul);

            return div;
        }

        public override IExpression Analyze(Arcsin exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            var involution = new Pow(exp.Argument.Clone(), new Number(2));
            var sub = new Sub(new Number(1), involution);
            var sqrt = new Sqrt(sub);
            var division = new Div(exp.Argument.Clone().Analyze(this), sqrt);

            return division;
        }

        public override IExpression Analyze(Arctan exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            var involution = new Pow(exp.Argument.Clone(), new Number(2));
            var add = new Add(new Number(1), involution);
            var div = new Div(exp.Argument.Clone().Analyze(this), add);

            return div;
        }

        public override IExpression Analyze(Cos exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            var sine = new Sin(exp.Argument.Clone());
            var multiplication = new Mul(sine, exp.Argument.Clone().Analyze(this));
            var unMinus = new UnaryMinus(multiplication);

            return unMinus;
        }

        public override IExpression Analyze(Cot exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            var sine = new Sin(exp.Argument.Clone());
            var involution = new Pow(sine, new Number(2));
            var division = new Div(exp.Argument.Clone().Analyze(this), involution);
            var unMinus = new UnaryMinus(division);

            return unMinus;
        }

        public override IExpression Analyze(Csc exp)
        {
            var unary = new UnaryMinus(exp.Argument.Clone().Analyze(this));
            var cot = new Cot(exp.Argument.Clone());
            var csc = new Csc(exp.Argument.Clone());
            var mul1 = new Mul(cot, csc);
            var mul2 = new Mul(unary, mul1);

            return mul2;
        }

        public override IExpression Analyze(Sec exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            var tan = new Tan(exp.Argument.Clone());
            var sec = new Sec(exp.Argument.Clone());
            var mul1 = new Mul(tan, sec);
            var mul2 = new Mul(exp.Argument.Clone().Analyze(this), mul1);

            return mul2;
        }

        public override IExpression Analyze(Sin exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            var cos = new Cos(exp.Argument.Clone());
            var mul = new Mul(cos, exp.Argument.Clone().Analyze(this));

            return mul;
        }

        public override IExpression Analyze(Tan exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            var cos = new Cos(exp.Argument.Clone());
            var inv = new Pow(cos, new Number(2));
            var div = new Div(exp.Argument.Clone().Analyze(this), inv);

            return div;
        }

        #endregion

        #region Hyperbolic

        public override IExpression Analyze(Arcosh exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            var sqr = new Pow(exp.Argument.Clone(), new Number(2));
            var sub = new Sub(sqr, new Number(1));
            var sqrt = new Sqrt(sub);
            var div = new Div(exp.Argument.Clone().Analyze(this), sqrt);

            return div;
        }

        public override IExpression Analyze(Arcoth exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            var sqr = new Pow(exp.Argument.Clone(), new Number(2));
            var sub = new Sub(new Number(1), sqr);
            var div = new Div(exp.Argument.Clone().Analyze(this), sub);

            return div;
        }

        public override IExpression Analyze(Arcsch exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            var inv = new Pow(exp.Argument.Clone(), new Number(2));
            var add = new Add(new Number(1), inv);
            var sqrt = new Sqrt(add);
            var abs = new Abs(exp.Argument.Clone());
            var mul = new Mul(abs, sqrt);
            var div = new Div(exp.Argument.Clone().Analyze(this), mul);
            var unMinus = new UnaryMinus(div);

            return unMinus;
        }

        public override IExpression Analyze(Arsech exp)
        {
            var inv = new Pow(exp.Argument.Clone(), new Number(2));
            var sub = new Sub(new Number(1), inv);
            var sqrt = new Sqrt(sub);
            var mul = new Mul(exp.Argument.Clone(), sqrt);
            var div = new Div(exp.Argument.Clone().Analyze(this), mul);
            var unMinus = new UnaryMinus(div);

            return unMinus;
        }

        public override IExpression Analyze(Arsinh exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            var sqr = new Pow(exp.Argument.Clone(), new Number(2));
            var add = new Add(sqr, new Number(1));
            var sqrt = new Sqrt(add);
            var div = new Div(exp.Argument.Clone().Analyze(this), sqrt);

            return div;
        }

        public override IExpression Analyze(Artanh exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            var sqr = new Pow(exp.Argument.Clone(), new Number(2));
            var sub = new Sub(new Number(1), sqr);
            var div = new Div(exp.Argument.Clone().Analyze(this), sub);

            return div;
        }

        public override IExpression Analyze(Cosh exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            var sinh = new Sinh(exp.Argument.Clone());
            var mul = new Mul(exp.Argument.Clone().Analyze(this), sinh);

            return mul;
        }

        public override IExpression Analyze(Coth exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            var sinh = new Sinh(exp.Argument.Clone());
            var inv = new Pow(sinh, new Number(2));
            var div = new Div(exp.Argument.Clone().Analyze(this), inv);
            var unMinus = new UnaryMinus(div);

            return unMinus;
        }

        public override IExpression Analyze(Csch exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            var coth = new Coth(exp.Argument.Clone());
            var mul1 = new Mul(coth, exp.Clone());
            var mul2 = new Mul(exp.Argument.Clone().Analyze(this), mul1);
            var unMinus = new UnaryMinus(mul2);

            return unMinus;
        }

        public override IExpression Analyze(Sech exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            var tanh = new Tanh(exp.Argument.Clone());
            var mul1 = new Mul(tanh, exp.Clone());
            var mul2 = new Mul(exp.Argument.Clone().Analyze(this), mul1);
            var unMinus = new UnaryMinus(mul2);

            return unMinus;
        }

        public override IExpression Analyze(Sinh exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            var cosh = new Cosh(exp.Argument.Clone());
            var mul = new Mul(exp.Argument.Clone().Analyze(this), cosh);

            return mul;
        }

        public override IExpression Analyze(Tanh exp)
        {
            if (!Helpers.HasVar(exp, variable))
                return new Number(0);

            var cosh = new Cosh(exp.Argument.Clone());
            var inv = new Pow(cosh, new Number(2));
            var div = new Div(exp.Argument.Clone().Analyze(this), inv);

            return div;
        }

        #endregion Hyperbolic

        public Variable Variable
        {
            get
            {
                return variable;
            }
            set
            {
                variable = value;
            }
        }

        public ExpressionParameters Parameters
        {
            get
            {
                return parameters;
            }
            set
            {
                parameters = value;
            }
        }

    }

}
