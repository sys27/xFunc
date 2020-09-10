// Copyright 2012-2020 Dmytro Kyshchenko
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

using xFunc.Maths.Analyzers;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Statistical;
using Xunit;

namespace xFunc.Tests.Analyzers.SimplifierTests
{
    public class SimplifierTest : BaseSimplifierTest
    {
        [Fact]
        public void DoubleUnary()
        {
            var un = new UnaryMinus(new UnaryMinus(Variable.X));
            var expected = Variable.X;

            SimpleTest(un, expected);
        }

        [Fact]
        public void UnaryNumber()
        {
            var un = new UnaryMinus(Number.One);
            var expected = new Number(-1);

            SimpleTest(un, expected);
        }

        [Fact]
        public void Define()
        {
            var define = new Define(Variable.X, new Add(Number.Two, Number.Two));
            var expected = new Define(Variable.X, new Number(4));

            SimpleTest(define, expected);
        }

        [Fact]
        public void PowerXZero()
        {
            var pow = new Pow(Variable.X, Number.Zero);
            var expected = Number.One;

            SimpleTest(pow, expected);
        }

        [Fact]
        public void PowerZeroX()
        {
            var pow = new Pow(Number.Zero, Variable.X);
            var expected = Number.Zero;

            SimpleTest(pow, expected);
        }

        [Fact]
        public void PowerOne()
        {
            var pow = new Pow(Variable.X, Number.One);
            var expected = Variable.X;

            SimpleTest(pow, expected);
        }

        [Fact]
        public void PowLog()
        {
            var pow = new Pow(
                new Number(30),
                new Log(new Number(30), Variable.X));
            var expected = Variable.X;

            SimpleTest(pow, expected);
        }

        [Fact]
        public void PowLg()
        {
            var pow = new Pow(
                new Number(10),
                new Lg(Variable.X));
            var expected = Variable.X;

            SimpleTest(pow, expected);
        }

        [Fact]
        public void PowLn()
        {
            var pow = new Pow(
                new Variable("e"),
                new Ln(Variable.X));
            var expected = Variable.X;

            SimpleTest(pow, expected);
        }

        [Fact]
        public void PowLb()
        {
            var pow = new Pow(
                Number.Two,
                new Lb(Variable.X));
            var expected = Variable.X;

            SimpleTest(pow, expected);
        }

        [Fact]
        public void RootOne()
        {
            var root = new Root(Variable.X, Number.One);
            var expected = Variable.X;

            SimpleTest(root, expected);
        }

        [Fact]
        public void Root()
        {
            var root = new Root(Variable.X, new Number(5));

            SimpleTest(root, root);
        }

        [Fact]
        public void Exp()
        {
            var exp = new Exp(new Number(30));

            SimpleTest(exp, exp);
        }

        [Fact]
        public void ExpLn()
        {
            var exp = new Exp(new Ln(new Number(30)));
            var expected = new Number(30);

            SimpleTest(exp, expected);
        }

        [Fact]
        public void Log()
        {
            var log = new Log(Variable.X, Variable.X);
            var expected = Number.One;

            SimpleTest(log, expected);
        }

        [Fact]
        public void Log2()
        {
            var log = new Log(new Number(3), new Number(11));

            SimpleTest(log, log);
        }

        [Fact]
        public void Ln()
        {
            var ln = new Ln(new Variable("e"));
            var expected = Number.One;

            SimpleTest(ln, expected);
        }

        [Fact]
        public void Ln2()
        {
            var ln = new Ln(new Variable("z"));

            SimpleTest(ln, ln);
        }

        [Fact]
        public void Lg()
        {
            var log = new Lg(new Number(10));
            var expected = Number.One;

            SimpleTest(log, expected);
        }

        [Fact]
        public void Lg2()
        {
            var log = new Lg(new Number(101));

            SimpleTest(log, log);
        }

        [Fact]
        public void Lb()
        {
            var log = new Lb(Number.Two);
            var expected = Number.One;

            SimpleTest(log, expected);
        }

        [Fact]
        public void Lb2()
        {
            var log = new Lb(new Number(3));

            SimpleTest(log, log);
        }

        [Fact]
        public void Simplify()
        {
            var simpl = new Simplifier();
            var simp = new Simplify(simpl, new Pow(Variable.X, Number.Zero));
            var expected = Number.One;

            SimpleTest(simp, expected);
        }

        [Fact]
        public void Deriv()
        {
            var diff = new Differentiator();
            var simpl = new Simplifier();
            var simp = new Derivative(diff, simpl, new Add(Number.Two, new Number(3)));
            var expected = new Derivative(diff, simpl, new Number(5));

            SimpleTest(simp, expected);
        }

        [Fact]
        public void UserFunc()
        {
            var exp = new UserFunction("f", new IExpression[] { new Mul(Number.Two, Number.Two) });
            var expected = new UserFunction("f", new IExpression[] { new Number(4) });

            SimpleTest(exp, expected);
        }

        [Fact]
        public void DiffTest()
        {
            var exp = new Count(new IExpression[] { new Add(Number.Two, Number.Two) });
            var expected = new Count(new IExpression[] { new Number(4) });

            SimpleTest(exp, expected);
        }

        [Fact]
        public void UnaryMinusNumberTest()
        {
            var exp = new Abs(new UnaryMinus(Variable.X));
            var expected = Variable.X;

            SimpleTest(exp, expected);
        }

        [Fact]
        public void AbsAbsTest()
        {
            var exp = new Abs(new Abs(Variable.X));
            var expected = new Abs(Variable.X);

            SimpleTest(exp, expected);
        }

        [Fact]
        public void AbsAbsAbsTest()
        {
            var exp = new Abs(new Abs(new Abs(Variable.X)));
            var expected = new Abs(Variable.X);

            SimpleTest(exp, expected);
        }
    }
}