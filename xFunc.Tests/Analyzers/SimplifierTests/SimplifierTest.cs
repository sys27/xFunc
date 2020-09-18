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

using System;
using xFunc.Maths.Analyzers;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions.Programming;
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
        public void Simplify()
        {
            var simp = new Simplify(simplifier, new Pow(Variable.X, Number.Zero));
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

        [Fact]
        public void MatrixTest()
        {
            var exp = new Matrix(new[]
            {
                new Vector(new IExpression[]
                {
                    new Add(Number.One, Number.Two),
                }),
            });
            var expected = new Matrix(new[]
            {
                new Vector(new IExpression[]
                {
                    new Number(3),
                }),
            });

            SimpleTest(exp, expected);
        }

        [Fact]
        public void AddAssignTest()
        {
            var exp = new AddAssign(Variable.X, new Add(Number.One, Number.Two));
            var expected = new AddAssign(Variable.X, new Number(3));

            SimpleTest(exp, expected);
        }

        [Theory]
        [ClassData(typeof(AllExpressionsData))]
        public void TestNullException(Type type) => TestNullExp(type);
    }
}