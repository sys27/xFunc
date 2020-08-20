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

using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;
using Xunit;

namespace xFunc.Tests.Analyzers.SimplifierTests
{
    public class MulSimplifierTest : BaseSimplifierTest
    {
        [Fact(DisplayName = "0 * x")]
        public void MulByFirstZero()
        {
            var mul = new Mul(new Number(0), new Variable("x"));
            var expected = new Number(0);

            SimpleTest(mul, expected);
        }

        [Fact(DisplayName = "x * 0")]
        public void MulBySecondZero()
        {
            var mul = new Mul(new Variable("x"), new Number(0));
            var expected = new Number(0);

            SimpleTest(mul, expected);
        }

        [Fact(DisplayName = "1 * x")]
        public void MulFirstOne()
        {
            var mul = new Mul(new Number(1), new Variable("x"));
            var expected = new Variable("x");

            SimpleTest(mul, expected);
        }

        [Fact(DisplayName = "x * 1")]
        public void MulSecondOne()
        {
            var mul = new Mul(new Variable("x"), new Number(1));
            var expected = new Variable("x");

            SimpleTest(mul, expected);
        }

        [Fact(DisplayName = "-1x")]
        public void MulFirstMinusOne()
        {
            var mul = new Mul(new Number(-1), new Variable("x"));
            var expected = new UnaryMinus(new Variable("x"));

            SimpleTest(mul, expected);
        }

        [Fact(DisplayName = "x * -1")]
        public void MulSecondMinusOne()
        {
            var mul = new Mul(new Variable("x"), new Number(-1));
            var expected = new UnaryMinus(new Variable("x"));

            SimpleTest(mul, expected);
        }

        [Fact(DisplayName = "2 * 3")]
        public void MulTwoNumbers()
        {
            var mul = new Mul(new Number(2), new Number(3));
            var expected = new Number(6);

            SimpleTest(mul, expected);
        }

        [Fact(DisplayName = "2 * (2 * x)")]
        public void MulDiffNumMul_NumMulVar_()
        {
            var mul = new Mul(new Number(2), new Mul(new Number(2), new Variable("x")));
            var expected = new Mul(new Number(4), new Variable("x"));

            SimpleTest(mul, expected);
        }

        [Fact(DisplayName = "2 * (x * 2)")]
        public void MulDiffNumMul_VarMulNum_()
        {
            var mul = new Mul(new Number(2), new Mul(new Variable("x"), new Number(2)));
            var expected = new Mul(new Number(4), new Variable("x"));

            SimpleTest(mul, expected);
        }

        [Fact(DisplayName = "(2 * x) * 2")]
        public void MulDiff_NumMulVar_MulNum()
        {
            var mul = new Mul(new Mul(new Number(2), new Variable("x")), new Number(2));
            var expected = new Mul(new Number(4), new Variable("x"));

            SimpleTest(mul, expected);
        }

        [Fact(DisplayName = "(x * 2) * 2")]
        public void MulDiff_VarMulNum_MulNum()
        {
            var mul = new Mul(new Mul(new Variable("x"), new Number(2)), new Number(2));
            var expected = new Mul(new Number(4), new Variable("x"));

            SimpleTest(mul, expected);
        }

        [Fact(DisplayName = "2 * (2 / x)")]
        public void MulDiffNumMul_NumDivVar_()
        {
            var mul = new Mul(new Number(2), new Div(new Number(2), new Variable("x")));
            var expected = new Div(new Number(4), new Variable("x"));

            SimpleTest(mul, expected);
        }

        [Fact(DisplayName = "2 * (x / 2)")]
        public void MulDiffNumMul_VarDivNum_()
        {
            var mul = new Mul(new Number(2), new Div(new Variable("x"), new Number(2)));
            var expected = new Variable("x");

            SimpleTest(mul, expected);
        }

        [Fact(DisplayName = "(2 / x) * 2")]
        public void MulDiff_NumDivVar_MulNum()
        {
            var mul = new Mul(new Div(new Number(2), new Variable("x")), new Number(2));
            var expected = new Div(new Number(4), new Variable("x"));

            SimpleTest(mul, expected);
        }

        [Fact(DisplayName = "(x / 2) * 2")]
        public void MulDiff_VarDivNum_MulNum()
        {
            var mul = new Mul(new Div(new Variable("x"), new Number(2)), new Number(2));
            var expected = new Variable("x");

            SimpleTest(mul, expected);
        }

        [Fact(DisplayName = "x * x")]
        public void MulSameVar1()
        {
            var mul = new Mul(new Variable("x"), new Variable("x"));
            var expected = new Pow(new Variable("x"), new Number(2));

            SimpleTest(mul, expected);
        }

        [Fact(DisplayName = "2x * x")]
        public void MulSameVar2()
        {
            var mul = new Mul(new Mul(new Number(2), new Variable("x")), new Variable("x"));
            var expected = new Mul(new Number(2), new Pow(new Variable("x"), new Number(2)));

            SimpleTest(mul, expected);
        }

        [Fact(DisplayName = "2x * 3x")]
        public void MulSameVar3()
        {
            var mul = new Mul(
                new Mul(new Number(2), new Variable("x")),
                new Mul(new Number(3), new Variable("x"))
            );
            var expected = new Mul(new Number(6), new Pow(new Variable("x"), new Number(2)));

            SimpleTest(mul, expected);
        }

        [Fact(DisplayName = "x * 2x")]
        public void MulSameVar4()
        {
            var mul = new Mul(new Variable("x"), new Mul(new Number(2), new Variable("x")));
            var expected = new Mul(new Number(2), new Pow(new Variable("x"), new Number(2)));

            SimpleTest(mul, expected);
        }

        [Fact(DisplayName = "x * (x * 2)")]
        public void MulSameVar5()
        {
            var mul = new Mul(new Variable("x"), new Mul(new Variable("x"), new Number(2)));
            var expected = new Mul(new Number(2), new Pow(new Variable("x"), new Number(2)));

            SimpleTest(mul, expected);
        }

        [Fact(DisplayName = "2x * x")]
        public void MulSameVar6()
        {
            var mul = new Mul(new Mul(new Number(2), new Variable("x")), new Variable("x"));
            var expected = new Mul(new Number(2), new Pow(new Variable("x"), new Number(2)));

            SimpleTest(mul, expected);
        }

        [Fact(DisplayName = "(x * 2) * x")]
        public void MulSameVar7()
        {
            var mul = new Mul(new Mul(new Variable("x"), new Number(2)), new Variable("x"));
            var expected = new Mul(new Number(2), new Pow(new Variable("x"), new Number(2)));

            SimpleTest(mul, expected);
        }

        [Fact(DisplayName = "2x * 3x")]
        public void MulSameVar8()
        {
            var mul = new Mul(
                new Mul(new Number(2), new Variable("x")),
                new Mul(new Number(3), new Variable("x"))
            );
            var expected = new Mul(new Number(6), new Pow(new Variable("x"), new Number(2)));

            SimpleTest(mul, expected);
        }

        [Fact(DisplayName = "(x * 2) * (x * 3)")]
        public void MulSameVar9()
        {
            var mul = new Mul(
                new Mul(new Variable("x"), new Number(2)),
                new Mul(new Variable("x"), new Number(3))
            );
            var expected = new Mul(new Number(6), new Pow(new Variable("x"), new Number(2)));

            SimpleTest(mul, expected);
        }

        [Fact(DisplayName = "2x * -2x")]
        public void MulSameVar10()
        {
            var mul = new Mul(
                new Mul(new Number(2), new Variable("x")),
                new Mul(new Number(0.5), new Variable("x"))
            );
            var expected = new Pow(new Variable("x"), new Number(2));

            SimpleTest(mul, expected);
        }

        [Fact(DisplayName = "2x * -3x")]
        public void MulSameVar11()
        {
            var mul = new Mul(
                new Mul(new Number(2), new Variable("x")),
                new Mul(new Number(-0.5), new Variable("x"))
            );
            var expected = new UnaryMinus(new Pow(new Variable("x"), new Number(2)));

            SimpleTest(mul, expected);
        }

        [Fact(DisplayName = "2x * x * (-3)")]
        public void MulSameVar12()
        {
            var mul = new Mul(
                new Mul(new Number(2), new Variable("x")),
                new Mul(new Variable("x"), new Number(-3))
            );
            var expected = new Mul(new Number(-6), new Pow(new Variable("x"), new Number(2)));

            SimpleTest(mul, expected);
        }

        [Fact(DisplayName = "x * 2 * -3x")]
        public void MulSameVar13()
        {
            var mul = new Mul(
                new Mul(new Variable("x"), new Number(2)),
                new Mul(new Number(-3), new Variable("x"))
            );
            var expected = new Mul(new Number(-6), new Pow(new Variable("x"), new Number(2)));

            SimpleTest(mul, expected);
        }

        [Fact(DisplayName = "cos(cos(x)) * -sin(x)")]
        public void MulNegativeRightParamTest1()
        {
            var mul = new Mul(
                new Cos(new Cos(new Variable("x"))),
                new UnaryMinus(new Sin(new Variable("x")))
            );
            var expected = new UnaryMinus(new Mul(
                new Cos(new Cos(new Variable("x"))),
                new Sin(new Variable("x")))
            );

            SimpleTest(mul, expected);
        }
    }
}