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
        [Fact]
        public void DivZero()
        {
            var div = new Div(new Number(0), Variable.X);
            var expected = new Number(0);

            SimpleTest(div, expected);
        }

        [Fact]
        public void DivByZero()
        {
            var div = new Div(Variable.X, new Number(0));

            Assert.Throws<DivideByZeroException>(() => SimpleTest(div, null));
        }

        [Fact]
        public void ZeroDivByZero()
        {
            var div = new Div(new Number(0), new Number(0));
            var expected = new Number(double.NaN);

            SimpleTest(div, expected);
        }

        [Fact]
        public void DivByOne()
        {
            var div = new Div(Variable.X, new Number(1));
            var expected = Variable.X;

            SimpleTest(div, expected);
        }

        [Fact]
        public void DivTwoNumbers()
        {
            var div = new Div(new Number(8), new Number(2));
            var expected = new Number(4);

            SimpleTest(div, expected);
        }

        [Fact]
        public void DivDiff_NumMulVar_DivNum()
        {
            // (2 * x) / 4
            var div = new Div(new Mul(new Number(2), Variable.X), new Number(4));
            var expected = new Div(Variable.X, new Number(2));

            SimpleTest(div, expected);
        }

        [Fact]
        public void DivDiff_VarMulNum_DivNum()
        {
            // (x * 2) / 4
            var div = new Div(new Mul(Variable.X, new Number(2)), new Number(4));
            var expected = new Div(Variable.X, new Number(2));

            SimpleTest(div, expected);
        }

        [Fact]
        public void DivDiffNumDiv_NumMulVar_()
        {
            // 2 / (2 * x)
            var div = new Div(new Number(2), new Mul(new Number(2), Variable.X));
            var expected = new Div(new Number(1), Variable.X);

            SimpleTest(div, expected);
        }

        [Fact]
        public void DivDiffNumDiv_VarMulNum_()
        {
            // 2 / (2 * x)
            var div = new Div(new Number(2), new Mul(Variable.X, new Number(2)));
            var expected = new Div(new Number(1), Variable.X);

            SimpleTest(div, expected);
        }

        [Fact]
        public void DivDiff_NumDivVar_DivNum()
        {
            // (2 / x) / 2
            var div = new Div(new Div(new Number(2), Variable.X), new Number(2));
            var expected = new Div(new Number(1), Variable.X);

            SimpleTest(div, expected);
        }

        [Fact]
        public void DivDiff_VarDivNum_DivNum()
        {
            // (x / 2) / 2
            var div = new Div(new Div(Variable.X, new Number(2)), new Number(2));
            var expected = new Div(Variable.X, new Number(4));

            SimpleTest(div, expected);
        }

        [Fact]
        public void DivDiffNumDiv_NumDivVar_()
        {
            // 2 / (2 / x)
            var div = new Div(new Number(2), new Div(new Number(2), Variable.X));
            var expected = Variable.X;

            SimpleTest(div, expected);
        }

        [Fact]
        public void DivDiffNumDiv_VarDivNum_()
        {
            // 2 / (x / 2)
            var div = new Div(new Number(2), new Div(Variable.X, new Number(2)));
            var expected = new Div(new Number(4), Variable.X);

            SimpleTest(div, expected);
        }

        [Fact]
        public void DivSameVars()
        {
            var x = Variable.X;
            var div = new Div(x, x);
            var expected = new Number(1);

            SimpleTest(div, expected);
        }
    }
}