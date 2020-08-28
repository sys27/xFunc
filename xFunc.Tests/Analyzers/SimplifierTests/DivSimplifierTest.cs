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
using xFunc.Maths.Expressions;
using Xunit;

namespace xFunc.Tests.Analyzers.SimplifierTests
{
    public class DivSimplifierTest : BaseSimplifierTest
    {
        [Fact(DisplayName = "0 / x")]
        public void DivZero()
        {
            var div = new Div(Number.Zero, Variable.X);
            var expected = Number.Zero;

            SimpleTest(div, expected);
        }

        [Fact(DisplayName = "x / 0")]
        public void DivByZero()
        {
            var div = new Div(Variable.X, Number.Zero);

            Assert.Throws<DivideByZeroException>(() => SimpleTest(div, null));
        }

        [Fact(DisplayName = "0 / 0")]
        public void ZeroDivByZero()
        {
            var div = new Div(Number.Zero, Number.Zero);
            var actual = (Number)div.Analyze(simplifier);

            Assert.True(actual.IsNaN);
        }

        [Fact(DisplayName = "x / 1")]
        public void DivByOne()
        {
            var div = new Div(Variable.X, Number.One);
            var expected = Variable.X;

            SimpleTest(div, expected);
        }

        [Fact(DisplayName = "8 / 2")]
        public void DivTwoNumbers()
        {
            var div = new Div(new Number(8), Number.Two);
            var expected = new Number(4);

            SimpleTest(div, expected);
        }

        [Fact(DisplayName = "(2 * x) / 4")]
        public void DivDiff_NumMulVar_DivNum()
        {
            var div = new Div(new Mul(Number.Two, Variable.X), new Number(4));
            var expected = new Div(Variable.X, Number.Two);

            SimpleTest(div, expected);
        }

        [Fact(DisplayName = "(x * 2) / 4")]
        public void DivDiff_VarMulNum_DivNum()
        {
            var div = new Div(new Mul(Variable.X, Number.Two), new Number(4));
            var expected = new Div(Variable.X, Number.Two);

            SimpleTest(div, expected);
        }

        [Fact(DisplayName = "2 / (2 * x)")]
        public void DivDiffNumDiv_NumMulVar_()
        {
            var div = new Div(Number.Two, new Mul(Number.Two, Variable.X));
            var expected = new Div(Number.One, Variable.X);

            SimpleTest(div, expected);
        }

        [Fact(DisplayName = "2 / (2 * x)")]
        public void DivDiffNumDiv_VarMulNum_()
        {
            var div = new Div(Number.Two, new Mul(Variable.X, Number.Two));
            var expected = new Div(Number.One, Variable.X);

            SimpleTest(div, expected);
        }

        [Fact(DisplayName = "(2 / x) / 2")]
        public void DivDiff_NumDivVar_DivNum()
        {
            var div = new Div(new Div(Number.Two, Variable.X), Number.Two);
            var expected = new Div(Number.One, Variable.X);

            SimpleTest(div, expected);
        }

        [Fact(DisplayName = "(x / 2) / 2")]
        public void DivDiff_VarDivNum_DivNum()
        {
            var div = new Div(new Div(Variable.X, Number.Two), Number.Two);
            var expected = new Div(Variable.X, new Number(4));

            SimpleTest(div, expected);
        }

        [Fact(DisplayName = "2 / (2 / x)")]
        public void DivDiffNumDiv_NumDivVar_()
        {
            var div = new Div(Number.Two, new Div(Number.Two, Variable.X));
            var expected = Variable.X;

            SimpleTest(div, expected);
        }

        [Fact(DisplayName = "2 / (x / 2)")]
        public void DivDiffNumDiv_VarDivNum_()
        {
            var div = new Div(Number.Two, new Div(Variable.X, Number.Two));
            var expected = new Div(new Number(4), Variable.X);

            SimpleTest(div, expected);
        }

        [Fact(DisplayName = "x / x")]
        public void DivSameVars()
        {
            var div = new Div(Variable.X, Variable.X);
            var expected = Number.One;

            SimpleTest(div, expected);
        }
    }
}