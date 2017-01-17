// Copyright 2012-2017 Dmitry Kischenko
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
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.Collections;
using Xunit;
using xFunc.Maths.Analyzers;

namespace xFunc.Tests.Analyzers
{

    public class DifferentiatorTest
    {

        public DifferentiatorTest() { }

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

            Assert.Equal("0", exp.ToString());
        }

        #endregion

        #region Common

        [Fact]
        public void NumberTest()
        {
            var exp = Differentiate(new Number(10));

            Assert.Equal("0", exp.ToString());
        }

        [Fact]
        public void AbsDerivativeTest1()
        {
            var exp = Differentiate(new Abs(new Variable("x")));

            Assert.Equal("1 * (x / abs(x))", exp.ToString());
        }

        [Fact]
        public void AbsDerivativeTest2()
        {
            var num = new Number(2);
            var x = new Variable("x");
            var mul = new Mul(num, x);

            var exp = new Abs(mul);
            var deriv = Differentiate(exp);

            Assert.Equal("2 * 1 * ((2 * x) / abs(2 * x))", deriv.ToString());

            num.Value = 3;
            Assert.Equal("abs(3 * x)", exp.ToString());
            Assert.Equal("2 * 1 * ((2 * x) / abs(2 * x))", deriv.ToString());
        }

        [Fact]
        public void AbsPartialDerivativeTest1()
        {
            var exp = new Abs(new Mul(new Variable("x"), new Variable("y")));
            var deriv = Differentiate(exp);
            Assert.Equal("1 * y * ((x * y) / abs(x * y))", deriv.ToString());
        }

        [Fact]
        public void AbsPartialDerivativeTest2()
        {
            var exp = new Abs(new Mul(new Variable("x"), new Variable("y")));
            var deriv = Differentiate(exp, new Variable("y"));
            Assert.Equal("x * 1 * ((x * y) / abs(x * y))", deriv.ToString());
        }

        [Fact]
        public void AbsPartialDerivativeTest3()
        {
            var deriv = Differentiate(new Abs(new Variable("x")), new Variable("y"));
            Assert.Equal("0", deriv.ToString());
        }

        [Fact]
        public void AddDerivativeTest1()
        {
            var exp = new Add(new Mul(new Number(2), new Variable("x")), new Number(3));
            var deriv = Differentiate(exp);

            Assert.Equal("2 * 1", deriv.ToString());
        }

        [Fact]
        public void AddDerivativeTest2()
        {
            var exp = new Add(new Mul(new Number(2), new Variable("x")), new Mul(new Number(3), new Variable("x")));
            var deriv = Differentiate(exp);

            Assert.Equal("2 * 1 + 3 * 1", deriv.ToString());
        }

        [Fact]
        public void AddDerivativeTest3()
        {
            // 2x + 3
            var num1 = new Number(2);
            var x = new Variable("x");
            var mul1 = new Mul(num1, x);

            var num2 = new Number(3);

            var exp = new Add(mul1, num2);
            var deriv = Differentiate(exp);

            Assert.Equal("2 * 1", deriv.ToString());

            num1.Value = 5;
            Assert.Equal("5 * x + 3", exp.ToString());
            Assert.Equal("2 * 1", deriv.ToString());
        }

        [Fact]
        public void AddPartialDerivativeTest1()
        {
            var exp = new Add(new Add(new Mul(new Variable("x"), new Variable("y")), new Variable("x")), new Variable("y"));
            var deriv = Differentiate(exp);
            Assert.Equal("1 * y + 1", deriv.ToString());
        }

        [Fact]
        public void AddPartialDerivativeTest2()
        {
            var exp = new Add(new Add(new Mul(new Variable("x"), new Variable("y")), new Variable("x")), new Variable("y"));
            var deriv = Differentiate(exp, new Variable("y"));
            Assert.Equal("x * 1 + 1", deriv.ToString());
        }

        [Fact]
        public void AddPartialDerivativeTest3()
        {
            var exp = new Add(new Variable("x"), new Number(1));
            var deriv = Differentiate(exp, new Variable("y"));
            Assert.Equal("0", deriv.ToString());
        }

        [Fact]
        public void DivDerivativeTest1()
        {
            var exp = new Div(new Number(1), new Variable("x"));
            var deriv = Differentiate(exp);

            Assert.Equal("-(1 * 1) / (x ^ 2)", deriv.ToString());
        }

        [Fact]
        public void DivDerivativeTest2()
        {
            // sin(x) / x
            var exp = new Div(new Sin(new Variable("x")), new Variable("x"));
            var deriv = Differentiate(exp);

            Assert.Equal("(cos(x) * 1 * x - sin(x) * 1) / (x ^ 2)", deriv.ToString());
        }

        [Fact]
        public void DivDerivativeTest3()
        {
            // (2x) / (3x)
            var num1 = new Number(2);
            var x = new Variable("x");
            var mul1 = new Mul(num1, x);

            var num2 = new Number(3);
            var mul2 = new Mul(num2, x.Clone());

            var exp = new Div(mul1, mul2);
            var deriv = Differentiate(exp);

            Assert.Equal("(2 * 1 * 3 * x - 2 * x * 3 * 1) / ((3 * x) ^ 2)", deriv.ToString());

            num1.Value = 4;
            num2.Value = 5;
            Assert.Equal("(4 * x) / (5 * x)", exp.ToString());
            Assert.Equal("(2 * 1 * 3 * x - 2 * x * 3 * 1) / ((3 * x) ^ 2)", deriv.ToString());
        }

        [Fact]
        public void DivPartialDerivativeTest1()
        {
            // (y + x ^ 2) / x
            var exp = new Div(new Add(new Variable("y"), new Pow(new Variable("x"), new Number(2))), new Variable("x"));
            var deriv = Differentiate(exp);
            Assert.Equal("(1 * 2 * x ^ (2 - 1) * x - (y + x ^ 2) * 1) / (x ^ 2)", deriv.ToString());
        }

        [Fact]
        public void DivPartialDerivativeTest2()
        {
            var exp = new Div(new Variable("y"), new Variable("x"));
            var deriv = Differentiate(exp);
            Assert.Equal("-(y * 1) / (x ^ 2)", deriv.ToString());
        }

        [Fact]
        public void DivPartialDerivativeTest3()
        {
            var exp = new Div(new Variable("y"), new Variable("x"));
            var deriv = Differentiate(exp, new Variable("y"));
            Assert.Equal("1 / x", deriv.ToString());
        }

        [Fact]
        public void DivPartialDerivativeTest4()
        {
            // (x + 1) / x
            var exp = new Div(new Add(new Variable("x"), new Number(1)), new Variable("x"));
            var deriv = Differentiate(exp, new Variable("y"));
            Assert.Equal("0", deriv.ToString());
        }

        [Fact]
        public void ExpDerivativeTest1()
        {
            var exp = new Exp(new Variable("x"));
            var deriv = Differentiate(exp);

            Assert.Equal("1 * exp(x)", deriv.ToString());
        }

        [Fact]
        public void ExpDerivativeTest2()
        {
            var exp = new Exp(new Mul(new Number(2), new Variable("x")));
            var deriv = Differentiate(exp);

            Assert.Equal("2 * 1 * exp(2 * x)", deriv.ToString());
        }

        [Fact]
        public void ExpDerivativeTest3()
        {
            // exp(2x)
            var num = new Number(2);
            var x = new Variable("x");
            var mul = new Mul(num, x);

            var exp = new Exp(mul);
            var deriv = Differentiate(exp);

            Assert.Equal("2 * 1 * exp(2 * x)", deriv.ToString());

            num.Value = 6;
            Assert.Equal("exp(6 * x)", exp.ToString());
            Assert.Equal("2 * 1 * exp(2 * x)", deriv.ToString());
        }

        [Fact]
        public void ExpPartialDerivativeTest1()
        {
            var exp = new Exp(new Mul(new Variable("x"), new Variable("y")));
            var deriv = Differentiate(exp);
            Assert.Equal("1 * y * exp(x * y)", deriv.ToString());
        }

        [Fact]
        public void ExpPartialDerivativeTest2()
        {
            var exp = new Exp(new Mul(new Variable("x"), new Variable("y")));
            var deriv = Differentiate(exp, new Variable("y"));
            Assert.Equal("x * 1 * exp(x * y)", deriv.ToString());
        }

        [Fact]
        public void ExpPartialDerivativeTest3()
        {
            var exp = new Exp(new Variable("x"));
            var deriv = Differentiate(exp, new Variable("y"));
            Assert.Equal("0", deriv.ToString());
        }

        [Fact]
        public void LnDerivativeTest1()
        {
            var exp = new Ln(new Mul(new Number(2), new Variable("x")));
            var deriv = Differentiate(exp);

            Assert.Equal("(2 * 1) / (2 * x)", deriv.ToString());
        }

        [Fact]
        public void LnDerivativeTest2()
        {
            // ln(2x)
            var num = new Number(2);
            var x = new Variable("x");
            var mul = new Mul(num, x);

            var exp = new Ln(mul);
            var deriv = Differentiate(exp);

            Assert.Equal("(2 * 1) / (2 * x)", deriv.ToString());

            num.Value = 5;
            Assert.Equal("ln(5 * x)", exp.ToString());
            Assert.Equal("(2 * 1) / (2 * x)", deriv.ToString());
        }

        [Fact]
        public void LnPartialDerivativeTest1()
        {
            // ln(xy)
            var exp = new Ln(new Mul(new Variable("x"), new Variable("y")));
            var deriv = Differentiate(exp);
            Assert.Equal("(1 * y) / (x * y)", deriv.ToString());
        }

        [Fact]
        public void LnPartialDerivativeTest2()
        {
            // ln(xy)
            var exp = new Ln(new Mul(new Variable("x"), new Variable("y")));
            var deriv = Differentiate(exp, new Variable("y"));
            Assert.Equal("(x * 1) / (x * y)", deriv.ToString());
        }

        [Fact]
        public void LnPartialDerivativeTest3()
        {
            var exp = new Ln(new Variable("y"));
            var deriv = Differentiate(exp);
            Assert.Equal("0", deriv.ToString());
        }

        [Fact]
        public void LgDerivativeTest1()
        {
            var exp = new Lg(new Mul(new Number(2), new Variable("x")));
            var deriv = Differentiate(exp);

            Assert.Equal("(2 * 1) / (2 * x * ln(10))", deriv.ToString());
        }

        [Fact]
        public void LgDerivativeTest2()
        {
            // lg(2x)
            var num = new Number(2);
            var x = new Variable("x");
            var mul = new Mul(num, x);

            var exp = new Lg(mul);
            var deriv = Differentiate(exp);

            Assert.Equal("(2 * 1) / (2 * x * ln(10))", deriv.ToString());

            num.Value = 3;
            Assert.Equal("lg(3 * x)", exp.ToString());
            Assert.Equal("(2 * 1) / (2 * x * ln(10))", deriv.ToString());
        }

        [Fact]
        public void LgPartialDerivativeTest1()
        {
            // lg(2xy)
            var exp = new Lg(new Mul(new Mul(new Number(2), new Variable("x")), new Variable("y")));
            var deriv = Differentiate(exp);
            Assert.Equal("(2 * 1 * y) / (2 * x * y * ln(10))", deriv.ToString());
        }

        [Fact]
        public void LgPartialDerivativeTest2()
        {
            // lg(2xy)
            var exp = new Lg(new Variable("x"));
            var deriv = Differentiate(exp, new Variable("y"));
            Assert.Equal("0", deriv.ToString());
        }

        [Fact]
        public void LbDerivativeTest1()
        {
            var exp = new Lb(new Mul(new Number(2), new Variable("x")));
            var deriv = Differentiate(exp);

            Assert.Equal("(2 * 1) / (2 * x * ln(2))", deriv.ToString());
        }

        [Fact]
        public void LbDerivativeTest2()
        {
            // lb(2x)
            var num = new Number(2);
            var x = new Variable("x");
            var mul = new Mul(num, x);

            var exp = new Lb(mul);
            var deriv = Differentiate(exp);

            Assert.Equal("(2 * 1) / (2 * x * ln(2))", deriv.ToString());

            num.Value = 3;
            Assert.Equal("lb(3 * x)", exp.ToString());
            Assert.Equal("(2 * 1) / (2 * x * ln(2))", deriv.ToString());
        }

        [Fact]
        public void LbPartialDerivativeTest1()
        {
            // lb(2xy)
            var exp = new Lb(new Mul(new Mul(new Number(2), new Variable("x")), new Variable("y")));
            var deriv = Differentiate(exp);
            Assert.Equal("(2 * 1 * y) / (2 * x * y * ln(2))", deriv.ToString());
        }

        [Fact]
        public void LbPartialDerivativeTest2()
        {
            // lb(2xy)
            var exp = new Lb(new Variable("x"));
            var deriv = Differentiate(exp, new Variable("y"));
            Assert.Equal("0", deriv.ToString());
        }

        [Fact]
        public void LogDerivativeTest1()
        {
            var exp = new Log(new Variable("x"), new Number(2));
            var deriv = Differentiate(exp);

            Assert.Equal("1 / (x * ln(2))", deriv.ToString());
        }

        [Fact]
        public void LogDerivativeTest2()
        {
            // log(x, 2)
            var num = new Number(2);
            var x = new Variable("x");

            var exp = new Log(x, num);
            var deriv = Differentiate(exp);

            Assert.Equal("1 / (x * ln(2))", deriv.ToString());

            num.Value = 4;
            Assert.Equal("log(4, x)", exp.ToString());
            Assert.Equal("1 / (x * ln(2))", deriv.ToString());
        }

        [Fact]
        public void LogDerivativeTest3()
        {
            var exp = new Log(new Number(2), new Variable("x"));
            var deriv = Differentiate(exp);

            Assert.Equal("-(ln(2) * (1 / x)) / (ln(x) ^ 2)", deriv.ToString());
        }

        [Fact]
        public void LogPartialDerivativeTest1()
        {
            var exp = new Log(new Variable("x"), new Number(2));
            var deriv = Differentiate(exp, new Variable("x"));
            Assert.Equal("1 / (x * ln(2))", deriv.ToString());
        }

        [Fact]
        public void LogPartialDerivativeTest2()
        {
            var exp = new Log(new Variable("x"), new Number(2));
            var deriv = Differentiate(exp, new Variable("y"));
            Assert.Equal("0", deriv.ToString());
        }

        [Fact]
        public void MulDerivativeTest1()
        {
            var exp = new Mul(new Number(2), new Variable("x"));
            var deriv = Differentiate(exp);

            Assert.Equal("2 * 1", deriv.ToString());
        }

        [Fact]
        public void MulDerivativeTest2()
        {
            // 2x
            var num = new Number(2);
            var x = new Variable("x");

            var exp = new Mul(num, x);
            var deriv = Differentiate(exp);

            Assert.Equal("2 * 1", deriv.ToString());

            num.Value = 3;
            Assert.Equal("3 * x", exp.ToString());
            Assert.Equal("2 * 1", deriv.ToString());
        }

        [Fact]
        public void MulPartialDerivativeTest1()
        {
            // (x + 1) * (y + x)
            var exp = new Mul(new Add(new Variable("x"), new Number(1)), new Add(new Variable("y"), new Variable("x")));
            var deriv = Differentiate(exp);
            Assert.Equal("1 * (y + x) + (x + 1) * 1", deriv.ToString());
        }

        [Fact]
        public void MulPartialDerivativeTest2()
        {
            // (y + 1) * (3 + x)
            var exp = new Mul(new Add(new Variable("y"), new Number(1)), new Add(new Number(3), new Variable("x")));
            var deriv = Differentiate(exp, new Variable("y"));
            Assert.Equal("1 * (3 + x)", deriv.ToString());
        }

        [Fact]
        public void MulPartialDerivativeTest3()
        {
            // (x + 1) * (y + x)
            var exp = new Mul(new Add(new Variable("x"), new Number(1)), new Add(new Variable("y"), new Variable("x")));
            var deriv = Differentiate(exp, new Variable("y"));
            Assert.Equal("(x + 1) * 1", deriv.ToString());
        }

        [Fact]
        public void MulPartialDerivativeTest4()
        {
            // (x + 1) * (3 + x)
            var exp = new Mul(new Add(new Variable("x"), new Number(1)), new Add(new Number(3), new Variable("x")));
            var deriv = Differentiate(exp, new Variable("y"));
            Assert.Equal("0", deriv.ToString());
        }

        [Fact]
        public void PowDerivativeTest1()
        {
            var exp = new Pow(new Variable("x"), new Number(3));
            var deriv = Differentiate(exp);

            Assert.Equal("1 * 3 * x ^ (3 - 1)", deriv.ToString());
        }

        [Fact]
        public void PowDerivativeTest2()
        {
            // 2 ^ (3x)
            var exp = new Pow(new Number(2), new Mul(new Number(3), new Variable("x")));
            var deriv = Differentiate(exp);

            Assert.Equal("ln(2) * 2 ^ (3 * x) * 3 * 1", deriv.ToString());
        }

        [Fact]
        public void PowDerivativeTest3()
        {
            // x ^ 3
            var x = new Variable("x");
            var num1 = new Number(3);

            var exp = new Pow(x, num1);
            var deriv = Differentiate(exp);

            Assert.Equal("1 * 3 * x ^ (3 - 1)", deriv.ToString());

            num1.Value = 4;
            Assert.Equal("x ^ 4", exp.ToString());
            Assert.Equal("1 * 3 * x ^ (3 - 1)", deriv.ToString());

            // 2 ^ (3x)
            var num2 = new Number(2);
            num1 = new Number(3);
            var mul = new Mul(num1, x.Clone());

            exp = new Pow(num2, mul);
            deriv = Differentiate(exp);

            Assert.Equal("ln(2) * 2 ^ (3 * x) * 3 * 1", deriv.ToString());

            num1.Value = 4;
            Assert.Equal("2 ^ (4 * x)", exp.ToString());
            Assert.Equal("ln(2) * 2 ^ (3 * x) * 3 * 1", deriv.ToString());
        }

        [Fact]
        public void PowPartialDerivativeTest1()
        {
            // (yx) ^ 3
            var exp = new Pow(new Mul(new Variable("y"), new Variable("x")), new Number(3));
            var deriv = Differentiate(exp);
            Assert.Equal("y * 1 * 3 * (y * x) ^ (3 - 1)", deriv.ToString());
        }

        [Fact]
        public void PowPartialDerivativeTest2()
        {
            // (yx) ^ 3
            var exp = new Pow(new Mul(new Variable("y"), new Variable("x")), new Number(3));
            var deriv = Differentiate(exp, new Variable("y"));
            Assert.Equal("1 * x * 3 * (y * x) ^ (3 - 1)", deriv.ToString());
        }

        [Fact]
        public void PowPartialDerivativeTest3()
        {
            var exp = new Pow(new Variable("x"), new Number(3));
            var deriv = Differentiate(exp, new Variable("y"));
            Assert.Equal("0", deriv.ToString());
        }

        [Fact]
        public void RootDerivativeTest1()
        {
            var exp = new Root(new Variable("x"), new Number(3));
            var deriv = Differentiate(exp);

            Assert.Equal("1 * (1 / 3) * x ^ ((1 / 3) - 1)", deriv.ToString());
        }

        [Fact]
        public void RootDerivativeTest2()
        {
            // root(x, 3)
            var num = new Number(3);
            var x = new Variable("x");

            var exp = new Root(x, num);
            var deriv = Differentiate(exp);

            Assert.Equal("1 * (1 / 3) * x ^ ((1 / 3) - 1)", deriv.ToString());

            num.Value = 4;
            Assert.Equal("root(x, 4)", exp.ToString());
            Assert.Equal("1 * (1 / 3) * x ^ ((1 / 3) - 1)", deriv.ToString());
        }

        [Fact]
        public void RootPartialDerivativeTest1()
        {
            var exp = new Root(new Mul(new Variable("x"), new Variable("y")), new Number(3));
            var deriv = Differentiate(exp);
            Assert.Equal("1 * y * (1 / 3) * (x * y) ^ ((1 / 3) - 1)", deriv.ToString());
        }

        [Fact]
        public void RootPartialDerivativeTest2()
        {
            var exp = new Root(new Variable("y"), new Number(3));
            var deriv = Differentiate(exp);
            Assert.Equal("0", deriv.ToString());
        }

        [Fact]
        public void SqrtDerivativeTest1()
        {
            var exp = new Sqrt(new Mul(new Number(2), new Variable("x")));
            var deriv = Differentiate(exp);

            Assert.Equal("(2 * 1) / (2 * sqrt(2 * x))", deriv.ToString());
        }

        [Fact]
        public void SqrtDerivativeTest2()
        {
            var num = new Number(2);
            var x = new Variable("x");
            var mul = new Mul(num, x);

            var exp = new Sqrt(mul);
            var deriv = Differentiate(exp);

            Assert.Equal("(2 * 1) / (2 * sqrt(2 * x))", deriv.ToString());

            num.Value = 3;
            Assert.Equal("sqrt(3 * x)", exp.ToString());
            Assert.Equal("(2 * 1) / (2 * sqrt(2 * x))", deriv.ToString());
        }

        [Fact]
        public void SqrtPartialDerivativeTest1()
        {
            // sqrt(2xy)
            var exp = new Sqrt(new Mul(new Mul(new Number(2), new Variable("x")), new Variable("y")));
            var deriv = Differentiate(exp);
            Assert.Equal("(2 * 1 * y) / (2 * sqrt(2 * x * y))", deriv.ToString());
        }

        [Fact]
        public void SqrtPartialDerivativeTest2()
        {
            var exp = new Sqrt(new Variable("y"));
            var deriv = Differentiate(exp);
            Assert.Equal("0", deriv.ToString());
        }

        [Fact]
        public void SubDerivativeTest1()
        {
            // x - sin(x)
            var exp = new Sub(new Variable("x"), new Sin(new Variable("x")));
            var deriv = Differentiate(exp);

            Assert.Equal("1 - cos(x) * 1", deriv.ToString());
        }

        [Fact]
        public void SubDerivativeTest2()
        {
            var num1 = new Number(2);
            var x = new Variable("x");
            var mul1 = new Mul(num1, x);

            var num2 = new Number(3);
            var mul2 = new Mul(num2, x.Clone());

            var exp = new Sub(mul1, mul2);
            var deriv = Differentiate(exp);

            Assert.Equal("2 * 1 - 3 * 1", deriv.ToString());

            num1.Value = 5;
            num2.Value = 4;
            Assert.Equal("5 * x - 4 * x", exp.ToString());
            Assert.Equal("2 * 1 - 3 * 1", deriv.ToString());
        }

        [Fact]
        public void SubPartialDerivativeTest1()
        {
            var exp = new Sub(new Mul(new Variable("x"), new Variable("y")), new Variable("y"));
            var deriv = Differentiate(exp, new Variable("y"));
            Assert.Equal("x * 1 - 1", deriv.ToString());
        }

        [Fact]
        public void SubPartialDerivativeTest2()
        {
            var exp = new Sub(new Variable("x"), new Variable("y"));
            var deriv = Differentiate(exp);
            Assert.Equal("1", deriv.ToString());
        }

        [Fact]
        public void SubPartialDerivativeTest3()
        {
            var exp = new Sub(new Variable("x"), new Variable("y"));
            var deriv = Differentiate(exp, new Variable("y"));
            Assert.Equal("-1", deriv.ToString());
        }

        [Fact]
        public void SubPartialDerivativeTest4()
        {
            var exp = new Sub(new Variable("x"), new Number(1));
            var deriv = Differentiate(exp, new Variable("y"));
            Assert.Equal("0", deriv.ToString());
        }

        [Fact]
        public void UnaryMinusTest()
        {
            var exp = new UnaryMinus(new Sin(new Variable("x")));
            var deriv = Differentiate(exp);

            Assert.Equal("-(cos(x) * 1)", deriv.ToString());
        }

        [Fact]
        public void UnaryMinusTest2()
        {
            var exp = new UnaryMinus(new Sin(new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal("0", deriv.ToString());
        }

        [Fact]
        public void DiffVarTest()
        {
            var exp = new Mul(new Variable("x"), new Variable("y"));
            var deriv = Differentiate(exp);

            Assert.Equal("1 * y", deriv.ToString());
        }

        [Fact]
        public void VarTest()
        {
            var exp = new Variable("y");
            var deriv = Differentiate(exp);

            Assert.Equal("y", deriv.ToString());
        }

        #endregion Common

        #region Trigonometric

        [Fact]
        public void SinDerivativeTest1()
        {
            var exp = new Sin(new Variable("x"));
            var deriv = Differentiate(exp);

            Assert.Equal("cos(x) * 1", deriv.ToString());
        }

        [Fact]
        public void SinDerivativeTest2()
        {
            var exp = new Sin(new Mul(new Number(2), new Variable("x")));
            var deriv = Differentiate(exp);

            Assert.Equal("cos(2 * x) * 2 * 1", deriv.ToString());
        }

        [Fact]
        public void SinDerivativeTest3()
        {
            // sin(2x)
            var num = new Number(2);
            var x = new Variable("x");
            var mul = new Mul(num, x);

            var exp = new Sin(mul);
            var deriv = Differentiate(exp);

            Assert.Equal("cos(2 * x) * 2 * 1", deriv.ToString());

            num.Value = 3;
            Assert.Equal("sin(3 * x)", exp.ToString());
            Assert.Equal("cos(2 * x) * 2 * 1", deriv.ToString());
        }

        [Fact]
        public void SinPartialDerivativeTest1()
        {
            var exp = new Sin(new Mul(new Variable("x"), new Variable("y")));
            var deriv = Differentiate(exp);
            Assert.Equal("cos(x * y) * 1 * y", deriv.ToString());
        }

        [Fact]
        public void SinPartialDerivativeTest2()
        {
            var exp = new Sin(new Mul(new Variable("x"), new Variable("y")));
            var deriv = Differentiate(exp, new Variable("y"));
            Assert.Equal("cos(x * y) * x * 1", deriv.ToString());
        }

        [Fact]
        public void SinPartialDerivativeTest3()
        {
            var exp = new Sin(new Variable("y"));
            var deriv = Differentiate(exp);
            Assert.Equal("0", deriv.ToString());
        }

        [Fact]
        public void CosDerivativeTest1()
        {
            var exp = new Cos(new Variable("x"));
            var deriv = Differentiate(exp);

            Assert.Equal("-(sin(x) * 1)", deriv.ToString());
        }

        [Fact]
        public void CosDerivativeTest2()
        {
            var exp = new Cos(new Mul(new Number(2), new Variable("x")));
            var deriv = Differentiate(exp);

            Assert.Equal("-(sin(2 * x) * 2 * 1)", deriv.ToString());
        }

        [Fact]
        public void CosDerivativeTest3()
        {
            // cos(2x)
            var num = new Number(2);
            var x = new Variable("x");
            var mul = new Mul(num, x);

            var exp = new Cos(mul);
            var deriv = Differentiate(exp);

            Assert.Equal("-(sin(2 * x) * 2 * 1)", deriv.ToString());

            num.Value = 7;
            Assert.Equal("cos(7 * x)", exp.ToString());
            Assert.Equal("-(sin(2 * x) * 2 * 1)", deriv.ToString());
        }

        [Fact]
        public void CosPartialDerivativeTest1()
        {
            var exp = new Cos(new Mul(new Variable("x"), new Variable("y")));
            var deriv = Differentiate(exp);
            Assert.Equal("-(sin(x * y) * 1 * y)", deriv.ToString());
        }

        [Fact]
        public void CosPartialDerivativeTest2()
        {
            var exp = new Cos(new Mul(new Variable("x"), new Variable("y")));
            var deriv = Differentiate(exp, new Variable("y"));
            Assert.Equal("-(sin(x * y) * x * 1)", deriv.ToString());
        }

        [Fact]
        public void CosPartialDerivativeTest3()
        {
            var exp = new Cos(new Variable("x"));
            var deriv = Differentiate(exp, new Variable("y"));
            Assert.Equal("0", deriv.ToString());
        }

        [Fact]
        public void TanDerivativeTest1()
        {
            var exp = new Tan(new Variable("x"));
            var deriv = Differentiate(exp);

            Assert.Equal("1 / (cos(x) ^ 2)", deriv.ToString());
        }

        [Fact]
        public void TanDerivativeTest2()
        {
            var exp = new Tan(new Mul(new Number(2), new Variable("x")));
            var deriv = Differentiate(exp);

            Assert.Equal("(2 * 1) / (cos(2 * x) ^ 2)", deriv.ToString());
        }

        [Fact]
        public void TanDerivativeTest3()
        {
            var num = new Number(2);
            var x = new Variable("x");
            var mul = new Mul(num, x);

            var exp = new Tan(mul);
            var deriv = Differentiate(exp);

            Assert.Equal("(2 * 1) / (cos(2 * x) ^ 2)", deriv.ToString());

            num.Value = 5;
            Assert.Equal("tan(5 * x)", exp.ToString());
            Assert.Equal("(2 * 1) / (cos(2 * x) ^ 2)", deriv.ToString());
        }

        [Fact]
        public void TanPartialDerivativeTest1()
        {
            var exp = new Tan(new Mul(new Variable("x"), new Variable("y")));
            var deriv = Differentiate(exp);
            Assert.Equal("(1 * y) / (cos(x * y) ^ 2)", deriv.ToString());
        }

        [Fact]
        public void TanPartialDerivativeTest2()
        {
            var exp = new Tan(new Mul(new Variable("x"), new Variable("y")));
            var deriv = Differentiate(exp, new Variable("y"));
            Assert.Equal("(x * 1) / (cos(x * y) ^ 2)", deriv.ToString());
        }

        [Fact]
        public void TanPartialDerivativeTest3()
        {
            var exp = new Tan(new Variable("x"));
            var deriv = Differentiate(exp, new Variable("y"));
            Assert.Equal("0", deriv.ToString());
        }

        [Fact]
        public void CotDerivativeTest1()
        {
            var exp = new Cot(new Variable("x"));
            var deriv = Differentiate(exp);

            Assert.Equal("-(1 / (sin(x) ^ 2))", deriv.ToString());
        }

        [Fact]
        public void CotDerivativeTest2()
        {
            var exp = new Cot(new Mul(new Number(2), new Variable("x")));
            var deriv = Differentiate(exp);

            Assert.Equal("-((2 * 1) / (sin(2 * x) ^ 2))", deriv.ToString());
        }

        [Fact]
        public void CotDerivativeTest3()
        {
            // cot(2x)
            var num = new Number(2);
            var x = new Variable("x");
            var mul = new Mul(num, x);

            var exp = new Cot(mul);
            var deriv = Differentiate(exp);

            Assert.Equal("-((2 * 1) / (sin(2 * x) ^ 2))", deriv.ToString());

            num.Value = 3;
            Assert.Equal("cot(3 * x)", exp.ToString());
            Assert.Equal("-((2 * 1) / (sin(2 * x) ^ 2))", deriv.ToString());
        }

        [Fact]
        public void CotPartialDerivativeTest1()
        {
            var exp = new Cot(new Mul(new Variable("x"), new Variable("y")));
            var deriv = Differentiate(exp);
            Assert.Equal("-((1 * y) / (sin(x * y) ^ 2))", deriv.ToString());
        }

        [Fact]
        public void CotPartialDerivativeTest2()
        {
            var exp = new Cot(new Mul(new Variable("x"), new Variable("y")));
            var deriv = Differentiate(exp, new Variable("y"));
            Assert.Equal("-((x * 1) / (sin(x * y) ^ 2))", deriv.ToString());
        }

        [Fact]
        public void CotPartialDerivativeTest3()
        {
            var exp = new Cot(new Variable("x"));
            var deriv = Differentiate(exp, new Variable("y"));
            Assert.Equal("0", deriv.ToString());
        }

        [Fact]
        public void SecDerivativeTest1()
        {
            var exp = new Sec(new Mul(new Number(2), new Variable("x")));
            var deriv = Differentiate(exp);

            Assert.Equal("2 * 1 * tan(2 * x) * sec(2 * x)", deriv.ToString());
        }

        [Fact]
        public void SecDerivativeTest2()
        {
            // sec(2x)
            var num = new Number(2);
            var x = new Variable("x");
            var mul = new Mul(num, x);

            var exp = new Sec(mul);
            var deriv = Differentiate(exp);

            Assert.Equal("2 * 1 * tan(2 * x) * sec(2 * x)", deriv.ToString());

            num.Value = 4;
            Assert.Equal("sec(4 * x)", exp.ToString());
            Assert.Equal("2 * 1 * tan(2 * x) * sec(2 * x)", deriv.ToString());
        }

        [Fact]
        public void SecDerivativeZeroTest()
        {
            var exp = new Sec(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal("0", deriv.ToString());
        }

        [Fact]
        public void CscDerivativeTest()
        {
            var exp = new Csc(new Mul(new Number(2), new Variable("x")));
            var deriv = Differentiate(exp);

            Assert.Equal("-(2 * 1) * cot(2 * x) * csc(2 * x)", deriv.ToString());
        }

        [Fact]
        public void ArcsinDerivativeTest1()
        {
            var exp = new Arcsin(new Variable("x"));
            var deriv = Differentiate(exp);

            Assert.Equal("1 / sqrt(1 - x ^ 2)", deriv.ToString());
        }

        [Fact]
        public void ArcsinDerivativeTest2()
        {
            var exp = new Arcsin(new Mul(new Number(2), new Variable("x")));
            var deriv = Differentiate(exp);

            Assert.Equal("(2 * 1) / sqrt(1 - (2 * x) ^ 2)", deriv.ToString());
        }

        [Fact]
        public void ArcsinDerivativeTest3()
        {
            // arcsin(2x)
            var num = new Number(2);
            var x = new Variable("x");
            var mul = new Mul(num, x);

            var exp = new Arcsin(mul);
            var deriv = Differentiate(exp);

            Assert.Equal("(2 * 1) / sqrt(1 - (2 * x) ^ 2)", deriv.ToString());

            num.Value = 5;
            Assert.Equal("arcsin(5 * x)", exp.ToString());
            Assert.Equal("(2 * 1) / sqrt(1 - (2 * x) ^ 2)", deriv.ToString());
        }

        [Fact]
        public void ArcsinPartialDerivativeTest1()
        {
            var exp = new Arcsin(new Mul(new Variable("x"), new Variable("y")));
            var deriv = Differentiate(exp);
            Assert.Equal("(1 * y) / sqrt(1 - (x * y) ^ 2)", deriv.ToString());
        }

        [Fact]
        public void ArcsinPartialDerivativeTest2()
        {
            var exp = new Arcsin(new Mul(new Variable("x"), new Variable("y")));
            var deriv = Differentiate(exp, new Variable("y"));
            Assert.Equal("(x * 1) / sqrt(1 - (x * y) ^ 2)", deriv.ToString());
        }

        [Fact]
        public void ArcsinPartialDerivativeTest3()
        {
            var exp = new Arcsin(new Variable("x"));
            var deriv = Differentiate(exp, new Variable("y"));
            Assert.Equal("0", deriv.ToString());
        }

        [Fact]
        public void ArccosDerivativeTest1()
        {
            var exp = new Arccos(new Variable("x"));
            var deriv = Differentiate(exp);

            Assert.Equal("-(1 / sqrt(1 - x ^ 2))", deriv.ToString());
        }

        [Fact]
        public void ArccosDerivativeTest2()
        {
            var exp = new Arccos(new Mul(new Number(2), new Variable("x")));
            var deriv = Differentiate(exp);

            Assert.Equal("-((2 * 1) / sqrt(1 - (2 * x) ^ 2))", deriv.ToString());
        }

        [Fact]
        public void ArccosDerivativeTest3()
        {
            // arccos(2x)
            var num = new Number(2);
            var x = new Variable("x");
            var mul = new Mul(num, x);

            var exp = new Arccos(mul);
            var deriv = Differentiate(exp);

            Assert.Equal("-((2 * 1) / sqrt(1 - (2 * x) ^ 2))", deriv.ToString());

            num.Value = 6;
            Assert.Equal("arccos(6 * x)", exp.ToString());
            Assert.Equal("-((2 * 1) / sqrt(1 - (2 * x) ^ 2))", deriv.ToString());
        }

        [Fact]
        public void ArccosPartialDerivativeTest1()
        {
            var exp = new Arccos(new Mul(new Variable("x"), new Variable("y")));
            var deriv = Differentiate(exp);
            Assert.Equal("-((1 * y) / sqrt(1 - (x * y) ^ 2))", deriv.ToString());
        }

        [Fact]
        public void ArccosPartialDerivativeTest2()
        {
            var exp = new Arccos(new Mul(new Variable("x"), new Variable("y")));
            var deriv = Differentiate(exp, new Variable("y"));
            Assert.Equal("-((x * 1) / sqrt(1 - (x * y) ^ 2))", deriv.ToString());
        }

        [Fact]
        public void ArccosPartialDerivativeTest3()
        {
            var exp = new Arccos(new Variable("x"));
            var deriv = Differentiate(exp, new Variable("y"));
            Assert.Equal("0", deriv.ToString());
        }

        [Fact]
        public void ArctanDerivativeTest1()
        {
            var exp = new Arctan(new Variable("x"));
            var deriv = Differentiate(exp);

            Assert.Equal("1 / (1 + x ^ 2)", deriv.ToString());
        }

        [Fact]
        public void ArctanDerivativeTest2()
        {
            var exp = new Arctan(new Mul(new Number(2), new Variable("x")));
            var deriv = Differentiate(exp);

            Assert.Equal("(2 * 1) / (1 + (2 * x) ^ 2)", deriv.ToString());
        }

        [Fact]
        public void ArctanDerivativeTest3()
        {
            // arctan(2x)
            var num = new Number(2);
            var x = new Variable("x");
            var mul = new Mul(num, x);

            var exp = new Arctan(mul);
            var deriv = Differentiate(exp);

            Assert.Equal("(2 * 1) / (1 + (2 * x) ^ 2)", deriv.ToString());

            num.Value = 6;
            Assert.Equal("arctan(6 * x)", exp.ToString());
            Assert.Equal("(2 * 1) / (1 + (2 * x) ^ 2)", deriv.ToString());
        }

        [Fact]
        public void ArctanPartialDerivativeTest1()
        {
            var exp = new Arctan(new Mul(new Variable("x"), new Variable("y")));
            var deriv = Differentiate(exp);
            Assert.Equal("(1 * y) / (1 + (x * y) ^ 2)", deriv.ToString());
        }

        [Fact]
        public void ArctanPartialDerivativeTest2()
        {
            var exp = new Arctan(new Mul(new Variable("x"), new Variable("y")));
            var deriv = Differentiate(exp, new Variable("y"));
            Assert.Equal("(x * 1) / (1 + (x * y) ^ 2)", deriv.ToString());
        }

        [Fact]
        public void ArctanPartialDerivativeTest3()
        {
            var exp = new Arctan(new Variable("x"));
            var deriv = Differentiate(exp, new Variable("y"));
            Assert.Equal("0", deriv.ToString());
        }

        [Fact]
        public void ArccotDerivativeTest1()
        {
            var exp = new Arccot(new Variable("x"));
            var deriv = Differentiate(exp);

            Assert.Equal("-(1 / (1 + x ^ 2))", deriv.ToString());
        }

        [Fact]
        public void ArccotDerivativeTest2()
        {
            var exp = new Arccot(new Mul(new Number(2), new Variable("x")));
            var deriv = Differentiate(exp);

            Assert.Equal("-((2 * 1) / (1 + (2 * x) ^ 2))", deriv.ToString());
        }

        [Fact]
        public void ArccotDerivativeTest3()
        {
            // arccot(2x)
            var num = new Number(2);
            var x = new Variable("x");
            var mul = new Mul(num, x);

            var exp = new Arccot(mul);
            var deriv = Differentiate(exp);

            Assert.Equal("-((2 * 1) / (1 + (2 * x) ^ 2))", deriv.ToString());

            num.Value = 4;
            Assert.Equal("arccot(4 * x)", exp.ToString());
            Assert.Equal("-((2 * 1) / (1 + (2 * x) ^ 2))", deriv.ToString());
        }

        [Fact]
        public void ArccotPartialDerivativeTest1()
        {
            var exp = new Arccot(new Mul(new Variable("x"), new Variable("y")));
            var deriv = Differentiate(exp);
            Assert.Equal("-((1 * y) / (1 + (x * y) ^ 2))", deriv.ToString());
        }

        [Fact]
        public void ArccotPartialDerivativeTest2()
        {
            var exp = new Arccot(new Mul(new Variable("x"), new Variable("y")));
            var deriv = Differentiate(exp, new Variable("y"));
            Assert.Equal("-((x * 1) / (1 + (x * y) ^ 2))", deriv.ToString());
        }

        [Fact]
        public void ArccotPartialDerivativeTest3()
        {
            var exp = new Arccot(new Variable("x"));
            var deriv = Differentiate(exp, new Variable("y"));
            Assert.Equal("0", deriv.ToString());
        }

        [Fact]
        public void ArcsecDerivativeTest1()
        {
            var exp = new Arcsec(new Mul(new Number(2), new Variable("x")));
            var deriv = Differentiate(exp);

            Assert.Equal("(2 * 1) / (abs(2 * x) * sqrt((2 * x) ^ 2 - 1))", deriv.ToString());
        }

        [Fact]
        public void ArcsecDerivativeTest2()
        {
            // arcsec(2x)
            var num = new Number(2);
            var x = new Variable("x");
            var mul = new Mul(num, x);

            var exp = new Arcsec(mul);
            var deriv = Differentiate(exp);

            Assert.Equal("(2 * 1) / (abs(2 * x) * sqrt((2 * x) ^ 2 - 1))", deriv.ToString());

            num.Value = 4;
            Assert.Equal("arcsec(4 * x)", exp.ToString());
            Assert.Equal("(2 * 1) / (abs(2 * x) * sqrt((2 * x) ^ 2 - 1))", deriv.ToString());
        }

        [Fact]
        public void ArcsecDerivativeZeroTest()
        {
            var exp = new Arcsec(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal("0", deriv.ToString());
        }

        [Fact]
        public void ArccscDerivativeTest1()
        {
            var exp = new Arccsc(new Mul(new Number(2), new Variable("x")));
            var deriv = Differentiate(exp);

            Assert.Equal("-((2 * 1) / (abs(2 * x) * sqrt((2 * x) ^ 2 - 1)))", deriv.ToString());
        }

        [Fact]
        public void ArccscDerivativeTest2()
        {
            // arccsc(2x)
            var num = new Number(2);
            var x = new Variable("x");
            var mul = new Mul(num, x);

            var exp = new Arccsc(mul);
            var deriv = Differentiate(exp);

            Assert.Equal("-((2 * 1) / (abs(2 * x) * sqrt((2 * x) ^ 2 - 1)))", deriv.ToString());

            num.Value = 4;
            Assert.Equal("arccsc(4 * x)", exp.ToString());
            Assert.Equal("-((2 * 1) / (abs(2 * x) * sqrt((2 * x) ^ 2 - 1)))", deriv.ToString());
        }

        [Fact]
        public void ArccscDerivativeZeroTest()
        {
            var exp = new Arccsc(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal("0", deriv.ToString());
        }

        #endregion Trigonometric

        #region Hyperbolic

        [Fact]
        public void SinhDerivativeTest()
        {
            var exp = new Sinh(new Mul(new Number(2), new Variable("x")));
            var deriv = Differentiate(exp);

            Assert.Equal("2 * 1 * cosh(2 * x)", deriv.ToString());
        }

        [Fact]
        public void SinhDerivativeZeroTest()
        {
            var exp = new Sinh(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal("0", deriv.ToString());
        }

        [Fact]
        public void CoshDerivativeTest()
        {
            var exp = new Cosh(new Mul(new Number(2), new Variable("x")));
            var deriv = Differentiate(exp);

            Assert.Equal("2 * 1 * sinh(2 * x)", deriv.ToString());
        }

        [Fact]
        public void CoshDerivativeZeroTest()
        {
            var exp = new Cosh(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal("0", deriv.ToString());
        }

        [Fact]
        public void TanhDerivativeTest()
        {
            var exp = new Tanh(new Mul(new Number(2), new Variable("x")));
            var deriv = Differentiate(exp);

            Assert.Equal("(2 * 1) / (cosh(2 * x) ^ 2)", deriv.ToString());
        }

        [Fact]
        public void TanhDerivativeZeroTest()
        {
            var exp = new Tanh(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal("0", deriv.ToString());
        }

        [Fact]
        public void CothDerivativeTest()
        {
            var exp = new Coth(new Mul(new Number(2), new Variable("x")));
            var deriv = Differentiate(exp);

            Assert.Equal("-((2 * 1) / (sinh(2 * x) ^ 2))", deriv.ToString());
        }

        [Fact]
        public void CothDerivativeZeroTest()
        {
            var exp = new Coth(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal("0", deriv.ToString());
        }

        [Fact]
        public void SechDerivativeTest()
        {
            var exp = new Sech(new Mul(new Number(2), new Variable("x")));
            var deriv = Differentiate(exp);

            Assert.Equal("-(2 * 1 * tanh(2 * x) * sech(2 * x))", deriv.ToString());
        }

        [Fact]
        public void SechDerivativeZeroTest()
        {
            var exp = new Sech(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal("0", deriv.ToString());
        }

        [Fact]
        public void CschDerivativeTest()
        {
            var exp = new Csch(new Mul(new Number(2), new Variable("x")));
            var deriv = Differentiate(exp);

            Assert.Equal("-(2 * 1 * coth(2 * x) * csch(2 * x))", deriv.ToString());
        }

        [Fact]
        public void CschDerivativeZeroTest()
        {
            var exp = new Csch(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal("0", deriv.ToString());
        }

        [Fact]
        public void ArsinehDerivativeTest()
        {
            var exp = new Arsinh(new Mul(new Number(2), new Variable("x")));
            var deriv = Differentiate(exp);

            Assert.Equal("(2 * 1) / sqrt((2 * x) ^ 2 + 1)", deriv.ToString());
        }

        [Fact]
        public void ArsinehDerivativeZeroTest()
        {
            var exp = new Arsinh(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal("0", deriv.ToString());
        }

        [Fact]
        public void ArcoshDerivativeTest()
        {
            var exp = new Arcosh(new Mul(new Number(2), new Variable("x")));
            var deriv = Differentiate(exp);

            Assert.Equal("(2 * 1) / sqrt((2 * x) ^ 2 - 1)", deriv.ToString());
        }

        [Fact]
        public void ArcoshDerivativeZeroTest()
        {
            var exp = new Arcosh(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal("0", deriv.ToString());
        }

        [Fact]
        public void ArtanhDerivativeTest()
        {
            var exp = new Artanh(new Mul(new Number(2), new Variable("x")));
            var deriv = Differentiate(exp);

            Assert.Equal("(2 * 1) / (1 - (2 * x) ^ 2)", deriv.ToString());
        }

        [Fact]
        public void ArtanhDerivativeZeroTest()
        {
            var exp = new Artanh(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal("0", deriv.ToString());
        }

        [Fact]
        public void ArcothDerivativeTest()
        {
            var exp = new Arcoth(new Mul(new Number(2), new Variable("x")));
            var deriv = Differentiate(exp);

            Assert.Equal("(2 * 1) / (1 - (2 * x) ^ 2)", deriv.ToString());
        }

        [Fact]
        public void ArcothDerivativeZeroTest()
        {
            var exp = new Arcoth(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal("0", deriv.ToString());
        }

        [Fact]
        public void ArsechDerivativeTest()
        {
            var exp = new Arsech(new Mul(new Number(2), new Variable("x")));
            var deriv = Differentiate(exp);

            Assert.Equal("-((2 * 1) / (2 * x * sqrt(1 - (2 * x) ^ 2)))", deriv.ToString());
        }

        [Fact]
        public void ArcschDerivativeTest()
        {
            var exp = new Arcsch(new Mul(new Number(2), new Variable("x")));
            var deriv = Differentiate(exp);

            Assert.Equal("-((2 * 1) / (abs(2 * x) * sqrt(1 + (2 * x) ^ 2)))", deriv.ToString());
        }

        [Fact]
        public void ArcschDerivativeZeroTest()
        {
            var exp = new Arcsch(new Mul(new Number(2), new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal("0", deriv.ToString());
        }

        #endregion Hyperbolic

        [Fact]
        public void UserFunctionDerivTest()
        {
            var parameters = new FunctionCollection();
            var uf = new UserFunction("f", new IExpression[] { new Variable("x") }, 1);
            parameters.Add(uf, new Sin(new Variable("x")));

            var diff = Differentiate(uf, "x", parameters);

            Assert.Equal("cos(x) * 1", diff.ToString());
        }

        [Fact]
        public void UserFunctionDerivNullTest()
        {
            var uf = new UserFunction("f", new IExpression[] { new Variable("x") }, 1);

            Assert.Throws<ArgumentNullException>(() => Differentiate(uf, "x", null));
        }

        [Fact]
        public void DerivSimplify()
        {
            var exp = new Simplify(new Sin(new Variable("x")));
            var deriv = Differentiate(exp);

            Assert.Equal("cos(x) * 1", deriv.ToString());
        }

        [Fact]
        public void DerivSimplify2()
        {
            var exp = new Simplify(new Sin(new Variable("z")));
            var deriv = Differentiate(exp);

            Assert.Equal("0", deriv.ToString());
        }

        [Fact]
        public void DerivTest()
        {
            var exp = new Derivative(new Number(2));
            var deriv = Differentiate(exp);

            Assert.Equal("0", deriv.ToString());
        }

        [Fact]
        public void DoubleDiffTest()
        {
            var exp = new Derivative(new Derivative(new Sin(new Variable("x")), new Variable("x")), new Variable("x"));
            var deriv = Differentiate(exp);

            Assert.Equal("-(sin(x) * 1) * 1", deriv.ToString());
        }

        [Fact]
        public void TripleDiffTest()
        {
            var exp = new Derivative(new Derivative(new Derivative(new Sin(new Variable("x")), new Variable("x")), new Variable("x")), new Variable("x"));
            var deriv = Differentiate(exp);

            Assert.Equal("-(cos(x) * 1 * 1) * 1", deriv.ToString());
        }

        [Fact]
        public void NotSupportedTest()
        {
            Assert.Throws<NotSupportedException>(() => Differentiate(new Fact(new Variable("x"))));
        }

    }

}
