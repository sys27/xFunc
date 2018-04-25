// Copyright 2012-2018 Dmitry Kischenko
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
using xFunc.Maths.Analyzers;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.Trigonometric;
using Xunit;

namespace xFunc.Tests.Analyzers
{

    public class DifferentiatorTest
    {

        private readonly Number zero;
        private readonly Number one;
        private readonly Number two;
        private readonly Number three;

        private readonly Variable x;
        private readonly Variable y;

        public DifferentiatorTest()
        {
            zero = new Number(0);
            one = new Number(1);
            two = new Number(2);
            three = new Number(3);

            x = Variable.X;
            y = new Variable("y");
        }

        private IExpression Differentiate(IExpression exp)
        {
            return exp.Analyze(new Differentiator());
        }

        private IExpression Differentiate(IExpression exp, Variable variable)
        {
            return exp.Analyze(new Differentiator(variable));
        }

        private IExpression Differentiate(IExpression exp, Variable variable, ExpressionParameters parameters)
        {
            return exp.Analyze(new Differentiator(parameters, variable));
        }

        #region Args

        [Fact]
        public void VariableIsNullTest()
        {
            var exp = Differentiate(new Number(10), null, null);

            Assert.Equal(zero, exp);
        }

        #endregion

        #region Common

        [Fact]
        public void NumberTest()
        {
            var exp = Differentiate(new Number(10));

            Assert.Equal(zero, exp);
        }

        [Fact]
        public void AbsDerivativeTest1()
        {
            var exp = Differentiate(new Abs(x));
            var expected = new Mul(
                new Number(1),
                new Div(x, new Abs(x)));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void AbsDerivativeTest2()
        {
            var num = new Number(2);
            var mul = new Mul(num, x);

            var exp = new Abs(mul);
            var deriv = Differentiate(exp);
            var expected = new Mul(new Mul(two, one), new Div(new Mul(two, x), new Abs(new Mul(two, x))));

            Assert.Equal(expected, deriv);

            num.Value = 3;
            var abs = new Abs(new Mul(three, x));
            Assert.Equal(abs, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void AbsPartialDerivativeTest1()
        {
            var exp = new Abs(new Mul(x, new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new Mul(new Mul(one, y), new Div(new Mul(x, y), new Abs(new Mul(x, y))));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void AbsPartialDerivativeTest2()
        {
            var exp = new Abs(new Mul(x, new Variable("y")));
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new Mul(new Mul(x, one), new Div(new Mul(x, y), new Abs(new Mul(x, y))));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void AbsPartialDerivativeTest3()
        {
            var deriv = Differentiate(new Abs(x), new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void AddDerivativeTest1()
        {
            var exp = new Add(new Mul(new Number(2), x), new Number(3));
            var deriv = Differentiate(exp);
            var expected = new Mul(new Number(2), new Number(1));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void AddDerivativeTest2()
        {
            var exp = new Add(new Mul(new Number(2), x), new Mul(new Number(3), x));
            var deriv = Differentiate(exp);
            var expected = new Add(
                new Mul(two, one),
                new Mul(three, one));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void AddDerivativeTest3()
        {
            // 2x + 3
            var num1 = new Number(2);
            var mul1 = new Mul(num1, x);

            var num2 = new Number(3);

            var exp = new Add(mul1, num2);
            var deriv = Differentiate(exp);
            var expected = new Mul(two, one);

            Assert.Equal(expected, deriv);

            num1.Value = 5;
            var add = new Add(new Mul(new Number(5), x), three);
            Assert.Equal(add, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void AddPartialDerivativeTest1()
        {
            var exp = new Add(new Add(new Mul(x, new Variable("y")), x), new Variable("y"));
            var deriv = Differentiate(exp);
            var expected = new Add(new Mul(one, y), one);

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void AddPartialDerivativeTest2()
        {
            var exp = new Add(new Add(new Mul(x, new Variable("y")), x), new Variable("y"));
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new Add(new Mul(x, one), one);

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void AddPartialDerivativeTest3()
        {
            var exp = new Add(x, new Number(1));
            var deriv = Differentiate(exp, new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void DivDerivativeTest1()
        {
            var exp = new Div(new Number(1), x);
            var deriv = Differentiate(exp);
            var expected = new Div(new UnaryMinus(new Mul(one, one)), new Pow(x, two));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void DivDerivativeTest2()
        {
            // sin(x) / x
            var exp = new Div(new Sin(x), x);
            var deriv = Differentiate(exp);
            var expected = new Div(new Sub(new Mul(new Mul(new Cos(x), one), x), new Mul(new Sin(x), one)), new Pow(x, two));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void DivDerivativeTest3()
        {
            // (2x) / (3x)
            var num1 = new Number(2);
            var mul1 = new Mul(num1, x);

            var num2 = new Number(3);
            var mul2 = new Mul(num2, x.Clone());

            var exp = new Div(mul1, mul2);
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Sub(
                    new Mul(new Mul(two, one), new Mul(three, x)),
                    new Mul(new Mul(two, x), new Mul(three, one))),
                new Pow(new Mul(three, x), two));

            Assert.Equal(expected, deriv);

            num1.Value = 4;
            num2.Value = 5;
            var div = new Div(new Mul(new Number(4), x), new Mul(new Number(5), x));
            Assert.Equal(div, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void DivPartialDerivativeTest1()
        {
            // (y + x ^ 2) / x
            var exp = new Div(new Add(new Variable("y"), new Pow(x, new Number(2))), x);
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Sub(
                    new Mul(new Mul(one, new Mul(two, new Pow(x, new Sub(two, one)))), x),
                    new Mul(new Add(y, new Pow(x, two)), one)),
                new Pow(x, two));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void DivPartialDerivativeTest2()
        {
            var exp = new Div(new Variable("y"), x);
            var deriv = Differentiate(exp);
            var expected = new Div(new UnaryMinus(new Mul(y, one)), new Pow(x, two));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void DivPartialDerivativeTest3()
        {
            var exp = new Div(new Variable("y"), x);
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new Div(one, x);

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void DivPartialDerivativeTest4()
        {
            // (x + 1) / x
            var exp = new Div(new Add(x, new Number(1)), x);
            var deriv = Differentiate(exp, new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void ExpDerivativeTest1()
        {
            var exp = new Exp(x);
            var deriv = Differentiate(exp);
            var expected = new Mul(one, new Exp(x));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ExpDerivativeTest2()
        {
            var exp = new Exp(new Mul(new Number(2), x));
            var deriv = Differentiate(exp);
            var expected = new Mul(new Mul(two, one), new Exp(new Mul(two, x)));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ExpDerivativeTest3()
        {
            // exp(2x)
            var num = new Number(2);
            var mul = new Mul(num, x);

            var exp = new Exp(mul);
            var deriv = Differentiate(exp);
            var expected = new Mul(new Mul(two, one), new Exp(new Mul(two, x)));

            Assert.Equal(expected, deriv);

            num.Value = 6;
            var exp2 = new Exp(new Mul(new Number(6), x));
            Assert.Equal(exp2, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ExpPartialDerivativeTest1()
        {
            var exp = new Exp(new Mul(x, new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new Mul(new Mul(one, y), new Exp(new Mul(x, y)));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ExpPartialDerivativeTest2()
        {
            var exp = new Exp(new Mul(x, new Variable("y")));
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new Mul(new Mul(x, one), new Exp(new Mul(x, y)));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ExpPartialDerivativeTest3()
        {
            var exp = new Exp(x);
            var deriv = Differentiate(exp, new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void LnDerivativeTest1()
        {
            var exp = new Ln(new Mul(new Number(2), x));
            var deriv = Differentiate(exp);
            var expected = new Div(new Mul(two, one), new Mul(two, x));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void LnDerivativeTest2()
        {
            // ln(2x)
            var num = new Number(2);
            var mul = new Mul(num, x);

            var exp = new Ln(mul);
            var deriv = Differentiate(exp);
            var expected = new Div(new Mul(two, one), new Mul(two, x));

            Assert.Equal(expected, deriv);

            num.Value = 5;
            var ln = new Ln(new Mul(new Number(5), x));
            Assert.Equal(ln, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void LnPartialDerivativeTest1()
        {
            // ln(xy)
            var exp = new Ln(new Mul(x, new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new Div(new Mul(one, y), new Mul(x, y));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void LnPartialDerivativeTest2()
        {
            // ln(xy)
            var exp = new Ln(new Mul(x, new Variable("y")));
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new Div(new Mul(x, one), new Mul(x, y));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void LnPartialDerivativeTest3()
        {
            var exp = new Ln(new Variable("y"));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void LgDerivativeTest1()
        {
            var exp = new Lg(new Mul(new Number(2), x));
            var deriv = Differentiate(exp);
            var expected = new Div(new Mul(two, one), new Mul(new Mul(two, x), new Ln(new Number(10))));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void LgDerivativeTest2()
        {
            // lg(2x)
            var num = new Number(2);
            var mul = new Mul(num, x);

            var exp = new Lg(mul);
            var deriv = Differentiate(exp);
            var expected = new Div(new Mul(two, one), new Mul(new Mul(two, x), new Ln(new Number(10))));

            Assert.Equal(expected, deriv);

            num.Value = 3;
            var lg = new Lg(new Mul(three, x));
            Assert.Equal(lg, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void LgPartialDerivativeTest1()
        {
            // lg(2xy)
            var exp = new Lg(new Mul(new Mul(new Number(2), x), new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(new Mul(two, one), y),
                new Mul(new Mul(new Mul(two, x), y), new Ln(new Number(10))));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void LgPartialDerivativeTest2()
        {
            // lg(2xy)
            var exp = new Lg(x);
            var deriv = Differentiate(exp, new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void LbDerivativeTest1()
        {
            var exp = new Lb(new Mul(new Number(2), x));
            var deriv = Differentiate(exp);
            var expected = new Div(new Mul(two, one), new Mul(new Mul(two, x), new Ln(new Number(2))));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void LbDerivativeTest2()
        {
            // lb(2x)
            var num = new Number(2);
            var mul = new Mul(num, x);

            var exp = new Lb(mul);
            var deriv = Differentiate(exp);
            var expected = new Div(new Mul(two, one), new Mul(new Mul(two, x), new Ln(new Number(2))));

            Assert.Equal(expected, deriv);

            num.Value = 3;
            var lb = new Lb(new Mul(three, x));
            Assert.Equal(lb, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void LbPartialDerivativeTest1()
        {
            // lb(2xy)
            var exp = new Lb(new Mul(new Mul(new Number(2), x), new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(new Mul(two, one), y),
                new Mul(new Mul(new Mul(two, x), y), new Ln(two)));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void LbPartialDerivativeTest2()
        {
            // lb(2xy)
            var exp = new Lb(x);
            var deriv = Differentiate(exp, new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void LogDerivativeTest1()
        {
            var exp = new Log(x, new Number(2));
            var deriv = Differentiate(exp);
            var expected = new Div(one, new Mul(x, new Ln(two)));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void LogDerivativeTest2()
        {
            // log(x, 2)
            var num = new Number(2);

            var exp = new Log(x, num);
            var deriv = Differentiate(exp);
            var expected = new Div(one, new Mul(x, new Ln(two)));

            Assert.Equal(expected, deriv);

            num.Value = 4;
            var log = new Log(x, new Number(4));
            Assert.Equal(log, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void LogDerivativeTest3()
        {
            var exp = new Log(new Number(2), x);
            var deriv = Differentiate(exp);
            var expected = new Div(
                new UnaryMinus(new Mul(new Ln(two), new Div(one, x))),
                new Pow(new Ln(x), two));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void LogPartialDerivativeTest1()
        {
            var exp = new Log(x, new Number(2));
            var deriv = Differentiate(exp, x);
            var expected = new Div(one, new Mul(x, new Ln(two)));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void LogPartialDerivativeTest2()
        {
            var exp = new Log(x, new Number(2));
            var deriv = Differentiate(exp, new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void MulDerivativeTest1()
        {
            var exp = new Mul(new Number(2), x);
            var deriv = Differentiate(exp);
            var expected = new Mul(two, one);

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void MulDerivativeTest2()
        {
            // 2x
            var num = new Number(2);

            var exp = new Mul(num, x);
            var deriv = Differentiate(exp);
            var expected = new Mul(two, one);

            Assert.Equal(expected, deriv);

            num.Value = 3;
            var mul = new Mul(three, x);
            Assert.Equal(mul, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void MulPartialDerivativeTest1()
        {
            // (x + 1) * (y + x)
            var exp = new Mul(new Add(x, new Number(1)), new Add(new Variable("y"), x));
            var deriv = Differentiate(exp);
            var expected = new Add(new Mul(one, new Add(y, x)), new Mul(new Add(x, one), one));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void MulPartialDerivativeTest2()
        {
            // (y + 1) * (3 + x)
            var exp = new Mul(new Add(new Variable("y"), new Number(1)), new Add(new Number(3), x));
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new Mul(one, new Add(three, x));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void MulPartialDerivativeTest3()
        {
            // (x + 1) * (y + x)
            var exp = new Mul(new Add(x, new Number(1)), new Add(new Variable("y"), x));
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new Mul(new Add(x, one), one);

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void MulPartialDerivativeTest4()
        {
            // (x + 1) * (3 + x)
            var exp = new Mul(new Add(x, new Number(1)), new Add(new Number(3), x));
            var deriv = Differentiate(exp, new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void PowDerivativeTest1()
        {
            var exp = new Pow(x, new Number(3));
            var deriv = Differentiate(exp);
            var expected = new Mul(one, new Mul(three, new Pow(x, new Sub(three, one))));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void PowDerivativeTest2()
        {
            // 2 ^ (3x)
            var exp = new Pow(new Number(2), new Mul(new Number(3), x));
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new Mul(new Ln(two), new Pow(two, new Mul(three, x))),
                new Mul(three, one));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void PowDerivativeTest3()
        {
            // x ^ 3
            var num1 = new Number(3);

            var exp = new Pow(x, num1);
            var deriv = Differentiate(exp);
            var expected = new Mul(one, new Mul(three, new Pow(x, new Sub(three, one))));

            Assert.Equal(expected, deriv);

            num1.Value = 4;
            var pow1 = new Pow(x, new Number(4));
            Assert.Equal(pow1, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void PowDerivativeTest4()
        {
            // 2 ^ (3x)
            var num2 = new Number(2);
            var num1 = new Number(3);
            var mul = new Mul(num1, x.Clone());

            var exp = new Pow(num2, mul);
            var deriv = Differentiate(exp);
            var expected = new Mul(
               new Mul(new Ln(two), new Pow(two, new Mul(three, x))),
               new Mul(three, one));

            Assert.Equal(expected, deriv);

            num1.Value = 4;
            var pow2 = new Pow(two, new Mul(new Number(4), x));
            Assert.Equal(pow2, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void PowPartialDerivativeTest1()
        {
            // (yx) ^ 3
            var exp = new Pow(new Mul(new Variable("y"), x), new Number(3));
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new Mul(y, one),
                new Mul(three, new Pow(new Mul(y, x), new Sub(three, one))));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void PowPartialDerivativeTest2()
        {
            // (yx) ^ 3
            var exp = new Pow(new Mul(new Variable("y"), x), new Number(3));
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new Mul(
               new Mul(one, x),
               new Mul(three, new Pow(new Mul(y, x), new Sub(three, one))));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void PowPartialDerivativeTest3()
        {
            var exp = new Pow(x, new Number(3));
            var deriv = Differentiate(exp, new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void RootDerivativeTest1()
        {
            var exp = new Root(x, new Number(3));
            var deriv = Differentiate(exp);
            var expected = new Mul(
                one,
                new Mul(
                    new Div(one, three),
                    new Pow(x, new Sub(new Div(one, three), one))));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void RootDerivativeTest2()
        {
            // root(x, 3)
            var num = new Number(3);

            var exp = new Root(x, num);
            var deriv = Differentiate(exp);
            var expected = new Mul(
                one,
                new Mul(
                    new Div(one, three),
                    new Pow(x, new Sub(new Div(one, three), one))));

            Assert.Equal(expected, deriv);

            num.Value = 4;
            var root = new Root(x, new Number(4));
            Assert.Equal(root, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void RootPartialDerivativeTest1()
        {
            var exp = new Root(new Mul(x, new Variable("y")), new Number(3));
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new Mul(one, y),
                new Mul(
                    new Div(one, three),
                    new Pow(new Mul(x, y), new Sub(new Div(one, three), one))));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void RootPartialDerivativeTest2()
        {
            var exp = new Root(new Variable("y"), new Number(3));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void SqrtDerivativeTest1()
        {
            var exp = new Sqrt(new Mul(new Number(2), x));
            var deriv = Differentiate(exp);
            var expected = new Div(new Mul(two, one), new Mul(two, new Sqrt(new Mul(two, x))));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SqrtDerivativeTest2()
        {
            var num = new Number(2);
            var mul = new Mul(num, x);

            var exp = new Sqrt(mul);
            var deriv = Differentiate(exp);
            var expected = new Div(new Mul(two, one), new Mul(two, new Sqrt(new Mul(two, x))));

            Assert.Equal(expected, deriv);

            num.Value = 3;
            var sqrt = new Sqrt(new Mul(three, x));
            Assert.Equal(sqrt, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SqrtPartialDerivativeTest1()
        {
            // sqrt(2xy)
            var exp = new Sqrt(new Mul(new Mul(new Number(2), x), new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(new Mul(two, one), y),
                new Mul(two, new Sqrt(new Mul(new Mul(two, x), y))));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SqrtPartialDerivativeTest2()
        {
            var exp = new Sqrt(new Variable("y"));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void SubDerivativeTest1()
        {
            // x - sin(x)
            var exp = new Sub(x, new Sin(x));
            var deriv = Differentiate(exp);
            var expected = new Sub(one, new Mul(new Cos(x), one));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SubDerivativeTest2()
        {
            var num1 = new Number(2);
            var mul1 = new Mul(num1, x);

            var num2 = new Number(3);
            var mul2 = new Mul(num2, x.Clone());

            var exp = new Sub(mul1, mul2);
            var deriv = Differentiate(exp);
            var expected = new Sub(new Mul(two, one), new Mul(three, one));

            Assert.Equal(expected, deriv);

            num1.Value = 5;
            num2.Value = 4;
            var sub = new Sub(
                new Mul(new Number(5), x),
                new Mul(new Number(4), x));
            Assert.Equal(sub, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SubPartialDerivativeTest1()
        {
            var exp = new Sub(new Mul(x, new Variable("y")), new Variable("y"));
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new Sub(new Mul(x, one), one);

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SubPartialDerivativeTest2()
        {
            var exp = new Sub(x, new Variable("y"));
            var deriv = Differentiate(exp);

            Assert.Equal(one, deriv);
        }

        [Fact]
        public void SubPartialDerivativeTest3()
        {
            var exp = new Sub(x, new Variable("y"));
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new UnaryMinus(one);

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SubPartialDerivativeTest4()
        {
            var exp = new Sub(x, new Number(1));
            var deriv = Differentiate(exp, new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void UnaryMinusTest()
        {
            var exp = new UnaryMinus(new Sin(x));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(new Mul(new Cos(x), one));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void UnaryMinusTest2()
        {
            var exp = new UnaryMinus(new Sin(new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void DiffVarTest()
        {
            var exp = new Mul(x, new Variable("y"));
            var deriv = Differentiate(exp);
            var expected = new Mul(one, y);

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void VarTest()
        {
            var exp = new Variable("y");
            var deriv = Differentiate(exp);

            Assert.Equal(y, deriv);
        }

        #endregion Common

        #region Trigonometric

        [Fact]
        public void SinDerivativeTest1()
        {
            var exp = new Sin(x);
            var deriv = Differentiate(exp);
            var expected = new Mul(new Cos(x), one);

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SinDerivativeTest2()
        {
            var exp = new Sin(new Mul(new Number(2), x));
            var deriv = Differentiate(exp);
            var expected = new Mul(new Cos(new Mul(two, x)), new Mul(two, one));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SinDerivativeTest3()
        {
            // sin(2x)
            var num = new Number(2);
            var mul = new Mul(num, x);

            var exp = new Sin(mul);
            var deriv = Differentiate(exp);
            var expected = new Mul(new Cos(new Mul(two, x)), new Mul(two, one));

            Assert.Equal(expected, deriv);

            num.Value = 3;
            var sin = new Sin(new Mul(three, x));
            Assert.Equal(sin, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SinPartialDerivativeTest1()
        {
            var exp = new Sin(new Mul(x, new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new Mul(new Cos(new Mul(x, y)), new Mul(one, y));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SinPartialDerivativeTest2()
        {
            var exp = new Sin(new Mul(x, new Variable("y")));
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new Mul(new Cos(new Mul(x, y)), new Mul(x, one));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SinPartialDerivativeTest3()
        {
            var exp = new Sin(new Variable("y"));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void CosDerivativeTest1()
        {
            var exp = new Cos(x);
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(new Mul(new Sin(x), one));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void CosDerivativeTest2()
        {
            var exp = new Cos(new Mul(new Number(2), x));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(new Mul(new Sin(new Mul(two, x)), new Mul(two, one)));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void CosDerivativeTest3()
        {
            // cos(2x)
            var num = new Number(2);
            var mul = new Mul(num, x);

            var exp = new Cos(mul);
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(new Mul(new Sin(new Mul(two, x)), new Mul(two, one)));

            Assert.Equal(expected, deriv);

            num.Value = 7;
            var cos = new Cos(new Mul(new Number(7), x));
            Assert.Equal(cos, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void CosPartialDerivativeTest1()
        {
            var exp = new Cos(new Mul(x, new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(new Mul(new Sin(new Mul(x, y)), new Mul(one, y)));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void CosPartialDerivativeTest2()
        {
            var exp = new Cos(new Mul(x, new Variable("y")));
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new UnaryMinus(new Mul(new Sin(new Mul(x, y)), new Mul(x, one)));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void CosPartialDerivativeTest3()
        {
            var exp = new Cos(x);
            var deriv = Differentiate(exp, new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void TanDerivativeTest1()
        {
            var exp = new Tan(x);
            var deriv = Differentiate(exp);
            var expected = new Div(one, new Pow(new Cos(x), new Number(2)));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void TanDerivativeTest2()
        {
            var exp = new Tan(new Mul(new Number(2), x));
            var deriv = Differentiate(exp);
            var expected = new Div(new Mul(two, one), new Pow(new Cos(new Mul(two, x)), new Number(2)));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void TanDerivativeTest3()
        {
            var num = new Number(2);
            var mul = new Mul(num, x);

            var exp = new Tan(mul);
            var deriv = Differentiate(exp);
            var expected = new Div(new Mul(two, one), new Pow(new Cos(new Mul(two, x)), new Number(2)));

            Assert.Equal(expected, deriv);

            num.Value = 5;
            var tan = new Tan(new Mul(new Number(5), x));
            Assert.Equal(tan, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void TanPartialDerivativeTest1()
        {
            var exp = new Tan(new Mul(x, new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new Div(new Mul(one, y), new Pow(new Cos(new Mul(x, y)), two));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void TanPartialDerivativeTest2()
        {
            var exp = new Tan(new Mul(x, new Variable("y")));
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new Div(new Mul(x, one), new Pow(new Cos(new Mul(x, y)), two));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void TanPartialDerivativeTest3()
        {
            var exp = new Tan(x);
            var deriv = Differentiate(exp, new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void CotDerivativeTest1()
        {
            var exp = new Cot(x);
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(new Div(one, new Pow(new Sin(x), two)));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void CotDerivativeTest2()
        {
            var exp = new Cot(new Mul(new Number(2), x));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(new Div(new Mul(two, one), new Pow(new Sin(new Mul(two, x)), two)));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void CotDerivativeTest3()
        {
            // cot(2x)
            var num = new Number(2);
            var mul = new Mul(num, x);

            var exp = new Cot(mul);
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(new Div(new Mul(two, one), new Pow(new Sin(new Mul(two, x)), two)));

            Assert.Equal(expected, deriv);

            num.Value = 3;
            var cot = new Cot(new Mul(three, x));
            Assert.Equal(cot, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void CotPartialDerivativeTest1()
        {
            var exp = new Cot(new Mul(x, new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(new Div(new Mul(one, y), new Pow(new Sin(new Mul(x, y)), two)));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void CotPartialDerivativeTest2()
        {
            var exp = new Cot(new Mul(x, new Variable("y")));
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new UnaryMinus(new Div(new Mul(x, one), new Pow(new Sin(new Mul(x, y)), two)));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void CotPartialDerivativeTest3()
        {
            var exp = new Cot(x);
            var deriv = Differentiate(exp, new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void SecDerivativeTest1()
        {
            var exp = new Sec(new Mul(new Number(2), x));
            var deriv = Differentiate(exp);
            var expected = new Mul(new Mul(two, one), new Mul(new Tan(new Mul(two, x)), new Sec(new Mul(two, x))));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SecDerivativeTest2()
        {
            // sec(2x)
            var num = new Number(2);
            var mul = new Mul(num, x);

            var exp = new Sec(mul);
            var deriv = Differentiate(exp);
            var expected = new Mul(new Mul(two, one), new Mul(new Tan(new Mul(two, x)), new Sec(new Mul(two, x))));

            Assert.Equal(expected, deriv);

            num.Value = 4;
            var sec = new Sec(new Mul(new Number(4), x));
            Assert.Equal(sec, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SecDerivativeZeroTest()
        {
            var exp = new Sec(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void CscDerivativeTest()
        {
            var exp = new Csc(new Mul(new Number(2), x));
            var deriv = Differentiate(exp);
            var expected = new Mul(
                new UnaryMinus(new Mul(two, one)),
                new Mul(
                    new Cot(new Mul(two, x)),
                    new Csc(new Mul(two, x))));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArcsinDerivativeTest1()
        {
            var exp = new Arcsin(x);
            var deriv = Differentiate(exp);
            var expected = new Div(one, new Sqrt(new Sub(one, new Pow(x, two))));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArcsinDerivativeTest2()
        {
            var exp = new Arcsin(new Mul(new Number(2), x));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(two, one),
                new Sqrt(new Sub(one, new Pow(new Mul(two, x), two))));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArcsinDerivativeTest3()
        {
            // arcsin(2x)
            var num = new Number(2);
            var mul = new Mul(num, x);

            var exp = new Arcsin(mul);
            var deriv = Differentiate(exp);
            var expected = new Div(
               new Mul(two, one),
               new Sqrt(new Sub(one, new Pow(new Mul(two, x), two))));

            Assert.Equal(expected, deriv);

            num.Value = 5;
            var arcsin = new Arcsin(new Mul(new Number(5), x));
            Assert.Equal(arcsin, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArcsinPartialDerivativeTest1()
        {
            var exp = new Arcsin(new Mul(x, new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new Div(
               new Mul(one, y),
               new Sqrt(new Sub(one, new Pow(new Mul(x, y), two))));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArcsinPartialDerivativeTest2()
        {
            var exp = new Arcsin(new Mul(x, new Variable("y")));
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new Div(
               new Mul(x, one),
               new Sqrt(new Sub(one, new Pow(new Mul(x, y), two))));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArcsinPartialDerivativeTest3()
        {
            var exp = new Arcsin(x);
            var deriv = Differentiate(exp, new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void ArccosDerivativeTest1()
        {
            var exp = new Arccos(x);
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(new Div(one, new Sqrt(new Sub(one, new Pow(x, two)))));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArccosDerivativeTest2()
        {
            var exp = new Arccos(new Mul(new Number(2), x));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(new Div(new Mul(two, one), new Sqrt(new Sub(one, new Pow(new Mul(two, x), two)))));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArccosDerivativeTest3()
        {
            // arccos(2x)
            var num = new Number(2);
            var mul = new Mul(num, x);

            var exp = new Arccos(mul);
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(new Div(new Mul(two, one), new Sqrt(new Sub(one, new Pow(new Mul(two, x), two)))));

            Assert.Equal(expected, deriv);

            num.Value = 6;
            var arccos = new Arccos(new Mul(new Number(6), x));
            Assert.Equal(arccos, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArccosPartialDerivativeTest1()
        {
            var exp = new Arccos(new Mul(x, new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(new Div(
                new Mul(one, y),
                new Sqrt(new Sub(one, new Pow(new Mul(x, y), two)))));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArccosPartialDerivativeTest2()
        {
            var exp = new Arccos(new Mul(x, new Variable("y")));
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new UnaryMinus(new Div(
                new Mul(x, one),
                new Sqrt(new Sub(one, new Pow(new Mul(x, y), two)))));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArccosPartialDerivativeTest3()
        {
            var exp = new Arccos(x);
            var deriv = Differentiate(exp, new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void ArctanDerivativeTest1()
        {
            var exp = new Arctan(x);
            var deriv = Differentiate(exp);
            var expected = new Div(one, new Add(one, new Pow(x, two)));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArctanDerivativeTest2()
        {
            var exp = new Arctan(new Mul(new Number(2), x));
            var deriv = Differentiate(exp);
            var expected = new Div(new Mul(two, one), new Add(one, new Pow(new Mul(two, x), two)));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArctanDerivativeTest3()
        {
            // arctan(2x)
            var num = new Number(2);
            var mul = new Mul(num, x);

            var exp = new Arctan(mul);
            var deriv = Differentiate(exp);
            var expected = new Div(new Mul(two, one), new Add(one, new Pow(new Mul(two, x), two)));

            Assert.Equal(expected, deriv);

            num.Value = 6;
            var arctan = new Arctan(new Mul(new Number(6), x));
            Assert.Equal(arctan, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArctanPartialDerivativeTest1()
        {
            var exp = new Arctan(new Mul(x, new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(one, y),
                new Add(one, new Pow(new Mul(x, y), two)));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArctanPartialDerivativeTest2()
        {
            var exp = new Arctan(new Mul(x, new Variable("y")));
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new Div(
                new Mul(x, one),
                new Add(one, new Pow(new Mul(x, y), two)));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArctanPartialDerivativeTest3()
        {
            var exp = new Arctan(x);
            var deriv = Differentiate(exp, new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void ArccotDerivativeTest1()
        {
            var exp = new Arccot(x);
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(new Div(one, new Add(one, new Pow(x, two))));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArccotDerivativeTest2()
        {
            var exp = new Arccot(new Mul(new Number(2), x));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(new Div(new Mul(two, one), new Add(one, new Pow(new Mul(two, x), two))));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArccotDerivativeTest3()
        {
            // arccot(2x)
            var num = new Number(2);
            var mul = new Mul(num, x);

            var exp = new Arccot(mul);
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(new Div(new Mul(two, one), new Add(one, new Pow(new Mul(two, x), two))));

            Assert.Equal(expected, deriv);

            num.Value = 4;
            var arccot = new Arccot(new Mul(new Number(4), x));
            Assert.Equal(arccot, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArccotPartialDerivativeTest1()
        {
            var exp = new Arccot(new Mul(x, new Variable("y")));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(new Div(
                new Mul(one, y),
                new Add(one, new Pow(new Mul(x, y), two))));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArccotPartialDerivativeTest2()
        {
            var exp = new Arccot(new Mul(x, new Variable("y")));
            var deriv = Differentiate(exp, new Variable("y"));
            var expected = new UnaryMinus(new Div(
               new Mul(x, one),
               new Add(one, new Pow(new Mul(x, y), two))));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArccotPartialDerivativeTest3()
        {
            var exp = new Arccot(x);
            var deriv = Differentiate(exp, new Variable("y"));

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void ArcsecDerivativeTest1()
        {
            var exp = new Arcsec(new Mul(new Number(2), x));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(two, one),
                new Mul(
                    new Abs(new Mul(two, x)),
                    new Sqrt(new Sub(new Pow(new Mul(two, x), two), one))));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArcsecDerivativeTest2()
        {
            // arcsec(2x)
            var num = new Number(2);
            var mul = new Mul(num, x);

            var exp = new Arcsec(mul);
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(two, one),
                new Mul(
                    new Abs(new Mul(two, x)),
                    new Sqrt(new Sub(new Pow(new Mul(two, x), two), one))));

            Assert.Equal(expected, deriv);

            num.Value = 4;
            var arcsec = new Arcsec(new Mul(new Number(4), x));
            Assert.Equal(arcsec, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArcsecDerivativeZeroTest()
        {
            var exp = new Arcsec(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void ArccscDerivativeTest1()
        {
            var exp = new Arccsc(new Mul(new Number(2), x));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(new Div(
               new Mul(two, one),
               new Mul(
                   new Abs(new Mul(two, x)),
                   new Sqrt(new Sub(new Pow(new Mul(two, x), two), one)))));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArccscDerivativeTest2()
        {
            // arccsc(2x)
            var num = new Number(2);
            var mul = new Mul(num, x);

            var exp = new Arccsc(mul);
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(new Div(
              new Mul(two, one),
              new Mul(
                  new Abs(new Mul(two, x)),
                  new Sqrt(new Sub(new Pow(new Mul(two, x), two), one)))));

            Assert.Equal(expected, deriv);

            num.Value = 4;
            var arccsc = new Arccsc(new Mul(new Number(4), x));
            Assert.Equal(arccsc, exp);
            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArccscDerivativeZeroTest()
        {
            var exp = new Arccsc(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        #endregion Trigonometric

        #region Hyperbolic

        [Fact]
        public void SinhDerivativeTest()
        {
            var exp = new Sinh(new Mul(new Number(2), x));
            var deriv = Differentiate(exp);
            var expected = new Mul(new Mul(two, one), new Cosh(new Mul(two, x)));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SinhDerivativeZeroTest()
        {
            var exp = new Sinh(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void CoshDerivativeTest()
        {
            var exp = new Cosh(new Mul(new Number(2), x));
            var deriv = Differentiate(exp);
            var expected = new Mul(new Mul(two, one), new Sinh(new Mul(two, x)));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void CoshDerivativeZeroTest()
        {
            var exp = new Cosh(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void TanhDerivativeTest()
        {
            var exp = new Tanh(new Mul(new Number(2), x));
            var deriv = Differentiate(exp);
            var expected = new Div(new Mul(two, one), new Pow(new Cosh(new Mul(two, x)), two));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void TanhDerivativeZeroTest()
        {
            var exp = new Tanh(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void CothDerivativeTest()
        {
            var exp = new Coth(new Mul(new Number(2), x));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(new Div(
                new Mul(two, one),
                new Pow(new Sinh(new Mul(two, x)), two)));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void CothDerivativeZeroTest()
        {
            var exp = new Coth(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void SechDerivativeTest()
        {
            var exp = new Sech(new Mul(new Number(2), x));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(new Mul(
                new Mul(two, one),
                new Mul(new Tanh(new Mul(two, x)), new Sech(new Mul(two, x)))));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void SechDerivativeZeroTest()
        {
            var exp = new Sech(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void CschDerivativeTest()
        {
            var exp = new Csch(new Mul(new Number(2), x));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(new Mul(
                new Mul(two, one),
                new Mul(new Coth(new Mul(two, x)), new Csch(new Mul(two, x)))));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void CschDerivativeZeroTest()
        {
            var exp = new Csch(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void ArsinehDerivativeTest()
        {
            var exp = new Arsinh(new Mul(new Number(2), x));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(two, one),
                new Sqrt(new Add(new Pow(new Mul(two, x), two), one)));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArsinehDerivativeZeroTest()
        {
            var exp = new Arsinh(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void ArcoshDerivativeTest()
        {
            var exp = new Arcosh(new Mul(new Number(2), x));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(two, one),
                new Sqrt(new Sub(new Pow(new Mul(two, x), two), one)));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArcoshDerivativeZeroTest()
        {
            var exp = new Arcosh(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void ArtanhDerivativeTest()
        {
            var exp = new Artanh(new Mul(new Number(2), x));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(two, one),
                new Sub(one, new Pow(new Mul(two, x), two)));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArtanhDerivativeZeroTest()
        {
            var exp = new Artanh(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void ArcothDerivativeTest()
        {
            var exp = new Arcoth(new Mul(new Number(2), x));
            var deriv = Differentiate(exp);
            var expected = new Div(
                new Mul(two, one),
                new Sub(one, new Pow(new Mul(two, x), two)));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArcothDerivativeZeroTest()
        {
            var exp = new Arcoth(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void ArsechDerivativeTest()
        {
            var exp = new Arsech(new Mul(new Number(2), x));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(new Div(
                new Mul(two, one),
                new Mul(new Mul(two, x), new Sqrt(new Sub(one, new Pow(new Mul(two, x), two))))));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArcschDerivativeTest()
        {
            var exp = new Arcsch(new Mul(new Number(2), x));
            var deriv = Differentiate(exp);
            var expected = new UnaryMinus(new Div(
               new Mul(two, one),
               new Mul(new Abs(new Mul(two, x)), new Sqrt(new Add(one, new Pow(new Mul(two, x), two))))));

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void ArcschDerivativeZeroTest()
        {
            var exp = new Arcsch(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        #endregion Hyperbolic

        [Fact]
        public void UserFunctionDerivTest()
        {
            var parameters = new FunctionCollection();
            var uf = new UserFunction("f", new IExpression[] { x }, 1);
            parameters.Add(uf, new Sin(x));

            var diff = Differentiate(uf, "x", parameters);
            var expected = new Mul(new Cos(x), one);

            Assert.Equal(expected, diff);
        }

        [Fact]
        public void UserFunctionDerivNullTest()
        {
            var uf = new UserFunction("f", new IExpression[] { x }, 1);

            Assert.Throws<ArgumentNullException>(() => Differentiate(uf, "x", null));
        }

        [Fact]
        public void DerivSimplify()
        {
            var simp = new Simplifier();
            var exp = new Simplify(simp, new Sin(x));
            var deriv = Differentiate(exp);
            var expected = new Mul(new Cos(x), one);

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void DerivSimplify2()
        {
            var simp = new Simplifier();
            var exp = new Simplify(simp, new Sin(new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void DoubleDiffTest()
        {
            var diff = new Differentiator();
            var simp = new Simplifier();
            var exp = new Derivative(diff, simp, new Derivative(diff, simp, new Sin(x), x), x);
            var deriv = Differentiate(exp);
            var expected = new Mul(new UnaryMinus(new Mul(new Sin(x), one)), one);

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void DoubleDiffNoVarTest()
        {
            var diff = new Differentiator();
            var simp = new Simplifier();
            var exp = new Derivative(diff, simp, new Derivative(diff, simp, new Sin(new Number(1))));
            var deriv = Differentiate(exp);

            Assert.Equal(zero, deriv);
        }

        [Fact]
        public void TripleDiffTest()
        {
            var diff = new Differentiator();
            var simp = new Simplifier();
            var exp = new Derivative(diff, simp, new Derivative(diff, simp, new Derivative(diff, simp, new Sin(x), x), x), x);
            var deriv = Differentiate(exp);
            var expected = new Mul(new UnaryMinus(new Mul(new Mul(new Cos(x), one), one)), one);

            Assert.Equal(expected, deriv);
        }

        [Fact]
        public void NotSupportedTest()
        {
            Assert.Throws<NotSupportedException>(() => Differentiate(new Fact(x)));
        }

    }

}
