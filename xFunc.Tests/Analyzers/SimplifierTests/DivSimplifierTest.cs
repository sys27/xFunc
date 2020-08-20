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
            var div = new Div(new Number(0), new Variable("x"));
            var expected = new Number(0);

            SimpleTest(div, expected);
        }

        [Fact(DisplayName = "x / 0")]
        public void DivByZero()
        {
            var div = new Div(new Variable("x"), new Number(0));

            Assert.Throws<DivideByZeroException>(() => SimpleTest(div, null));
        }

        [Fact(DisplayName = "0 / 0")]
        public void ZeroDivByZero()
        {
            var div = new Div(new Number(0), new Number(0));
            var expected = new Number(double.NaN);

            SimpleTest(div, expected);
        }

        [Fact(DisplayName = "x / 1")]
        public void DivByOne()
        {
            var div = new Div(new Variable("x"), new Number(1));
            var expected = new Variable("x");

            SimpleTest(div, expected);
        }

        [Fact(DisplayName = "8 / 2")]
        public void DivTwoNumbers()
        {
            var div = new Div(new Number(8), new Number(2));
            var expected = new Number(4);

            SimpleTest(div, expected);
        }

        [Fact(DisplayName = "(2 * x) / 4")]
        public void DivDiff_NumMulVar_DivNum()
        {
            var div = new Div(new Mul(new Number(2), new Variable("x")), new Number(4));
            var expected = new Div(new Variable("x"), new Number(2));

            SimpleTest(div, expected);
        }

        [Fact(DisplayName = "(x * 2) / 4")]
        public void DivDiff_VarMulNum_DivNum()
        {
            var div = new Div(new Mul(new Variable("x"), new Number(2)), new Number(4));
            var expected = new Div(new Variable("x"), new Number(2));

            SimpleTest(div, expected);
        }

        [Fact(DisplayName = "2 / (2 * x)")]
        public void DivDiffNumDiv_NumMulVar_()
        {
            var div = new Div(new Number(2), new Mul(new Number(2), new Variable("x")));
            var expected = new Div(new Number(1), new Variable("x"));

            SimpleTest(div, expected);
        }

        [Fact(DisplayName = "2 / (2 * x)")]
        public void DivDiffNumDiv_VarMulNum_()
        {
            var div = new Div(new Number(2), new Mul(new Variable("x"), new Number(2)));
            var expected = new Div(new Number(1), new Variable("x"));

            SimpleTest(div, expected);
        }

        [Fact(DisplayName = "(2 / x) / 2")]
        public void DivDiff_NumDivVar_DivNum()
        {
            var div = new Div(new Div(new Number(2), new Variable("x")), new Number(2));
            var expected = new Div(new Number(1), new Variable("x"));

            SimpleTest(div, expected);
        }

        [Fact(DisplayName = "(x / 2) / 2")]
        public void DivDiff_VarDivNum_DivNum()
        {
            var div = new Div(new Div(new Variable("x"), new Number(2)), new Number(2));
            var expected = new Div(new Variable("x"), new Number(4));

            SimpleTest(div, expected);
        }

        [Fact(DisplayName = "2 / (2 / x)")]
        public void DivDiffNumDiv_NumDivVar_()
        {
            var div = new Div(new Number(2), new Div(new Number(2), new Variable("x")));
            var expected = new Variable("x");

            SimpleTest(div, expected);
        }

        [Fact(DisplayName = "2 / (x / 2)")]
        public void DivDiffNumDiv_VarDivNum_()
        {
            var div = new Div(new Number(2), new Div(new Variable("x"), new Number(2)));
            var expected = new Div(new Number(4), new Variable("x"));

            SimpleTest(div, expected);
        }

        [Fact(DisplayName = "x / x")]
        public void DivSameVars()
        {
            var div = new Div(new Variable("x"), new Variable("x"));
            var expected = new Number(1);

            SimpleTest(div, expected);
        }
    }
}