// Copyright 2012-2021 Dmytro Kyshchenko
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
using xFunc.Maths.Expressions.Angles;
using xFunc.Maths.Expressions.Trigonometric;
using Xunit;

namespace xFunc.Tests.Analyzers.SimplifierTests
{
    public class MulSimplifierTest : BaseSimplifierTest
    {
        [Fact(DisplayName = "0 * x")]
        public void MulByFirstZero()
        {
            var mul = new Mul(Number.Zero, Variable.X);
            var expected = Number.Zero;

            SimplifyTest(mul, expected);
        }

        [Fact(DisplayName = "x * 0")]
        public void MulBySecondZero()
        {
            var mul = new Mul(Variable.X, Number.Zero);
            var expected = Number.Zero;

            SimplifyTest(mul, expected);
        }

        [Fact(DisplayName = "1 * x")]
        public void MulFirstOne()
        {
            var mul = new Mul(Number.One, Variable.X);
            var expected = Variable.X;

            SimplifyTest(mul, expected);
        }

        [Fact(DisplayName = "x * 1")]
        public void MulSecondOne()
        {
            var mul = new Mul(Variable.X, Number.One);
            var expected = Variable.X;

            SimplifyTest(mul, expected);
        }

        [Fact(DisplayName = "-1x")]
        public void MulFirstMinusOne()
        {
            var mul = new Mul(new Number(-1), Variable.X);
            var expected = new UnaryMinus(Variable.X);

            SimplifyTest(mul, expected);
        }

        [Fact(DisplayName = "x * -1")]
        public void MulSecondMinusOne()
        {
            var mul = new Mul(Variable.X, new Number(-1));
            var expected = new UnaryMinus(Variable.X);

            SimplifyTest(mul, expected);
        }

        [Fact(DisplayName = "2 * 3")]
        public void MulTwoNumbers()
        {
            var mul = new Mul(Number.Two, new Number(3));
            var expected = new Number(6);

            SimplifyTest(mul, expected);
        }

        [Fact(DisplayName = "90 * 2 deg")]
        public void MulNumberAngle()
        {
            var mul = new Mul(
                new Number(90),
                AngleValue.Degree(2).AsExpression()
            );
            var expected = AngleValue.Degree(180).AsExpression();

            SimplifyTest(mul, expected);
        }

        [Fact(DisplayName = "90 deg * 2")]
        public void MulAngleNumber()
        {
            var mul = new Mul(
                AngleValue.Degree(90).AsExpression(),
                new Number(2)
            );
            var expected = AngleValue.Degree(180).AsExpression();

            SimplifyTest(mul, expected);
        }

        [Fact(DisplayName = "2 rad * 90 deg")]
        public void MulTwoAngles()
        {
            var mul = new Mul(
                AngleValue.Radian(2).AsExpression(),
                AngleValue.Degree(90).AsExpression()
            );
            var expected = AngleValue.Degree(114.59155902616465 * 90).AsExpression();

            SimplifyTest(mul, expected);
        }

        [Fact(DisplayName = "2 * (2 * x)")]
        public void MulDiffNumMul_NumMulVar_()
        {
            var mul = new Mul(Number.Two, new Mul(Number.Two, Variable.X));
            var expected = new Mul(new Number(4), Variable.X);

            SimplifyTest(mul, expected);
        }

        [Fact(DisplayName = "2 * (x * 2)")]
        public void MulDiffNumMul_VarMulNum_()
        {
            var mul = new Mul(Number.Two, new Mul(Variable.X, Number.Two));
            var expected = new Mul(new Number(4), Variable.X);

            SimplifyTest(mul, expected);
        }

        [Fact(DisplayName = "(2 * x) * 2")]
        public void MulDiff_NumMulVar_MulNum()
        {
            var mul = new Mul(new Mul(Number.Two, Variable.X), Number.Two);
            var expected = new Mul(new Number(4), Variable.X);

            SimplifyTest(mul, expected);
        }

        [Fact(DisplayName = "(x * 2) * 2")]
        public void MulDiff_VarMulNum_MulNum()
        {
            var mul = new Mul(new Mul(Variable.X, Number.Two), Number.Two);
            var expected = new Mul(new Number(4), Variable.X);

            SimplifyTest(mul, expected);
        }

        [Fact(DisplayName = "2 * (2 / x)")]
        public void MulDiffNumMul_NumDivVar_()
        {
            var mul = new Mul(Number.Two, new Div(Number.Two, Variable.X));
            var expected = new Div(new Number(4), Variable.X);

            SimplifyTest(mul, expected);
        }

        [Fact(DisplayName = "2 * (x / 2)")]
        public void MulDiffNumMul_VarDivNum_()
        {
            var mul = new Mul(Number.Two, new Div(Variable.X, Number.Two));
            var expected = Variable.X;

            SimplifyTest(mul, expected);
        }

        [Fact(DisplayName = "(2 / x) * 2")]
        public void MulDiff_NumDivVar_MulNum()
        {
            var mul = new Mul(new Div(Number.Two, Variable.X), Number.Two);
            var expected = new Div(new Number(4), Variable.X);

            SimplifyTest(mul, expected);
        }

        [Fact(DisplayName = "(x / 2) * 2")]
        public void MulDiff_VarDivNum_MulNum()
        {
            var mul = new Mul(new Div(Variable.X, Number.Two), Number.Two);
            var expected = Variable.X;

            SimplifyTest(mul, expected);
        }

        [Fact(DisplayName = "x * x")]
        public void MulSameVar1()
        {
            var mul = new Mul(Variable.X, Variable.X);
            var expected = new Pow(Variable.X, Number.Two);

            SimplifyTest(mul, expected);
        }

        [Fact(DisplayName = "2x * x")]
        public void MulSameVar2()
        {
            var mul = new Mul(new Mul(Number.Two, Variable.X), Variable.X);
            var expected = new Mul(Number.Two, new Pow(Variable.X, Number.Two));

            SimplifyTest(mul, expected);
        }

        [Fact(DisplayName = "2x * 3x")]
        public void MulSameVar3()
        {
            var mul = new Mul(
                new Mul(Number.Two, Variable.X),
                new Mul(new Number(3), Variable.X)
            );
            var expected = new Mul(new Number(6), new Pow(Variable.X, Number.Two));

            SimplifyTest(mul, expected);
        }

        [Fact(DisplayName = "x * 2x")]
        public void MulSameVar4()
        {
            var mul = new Mul(Variable.X, new Mul(Number.Two, Variable.X));
            var expected = new Mul(Number.Two, new Pow(Variable.X, Number.Two));

            SimplifyTest(mul, expected);
        }

        [Fact(DisplayName = "x * (x * 2)")]
        public void MulSameVar5()
        {
            var mul = new Mul(Variable.X, new Mul(Variable.X, Number.Two));
            var expected = new Mul(Number.Two, new Pow(Variable.X, Number.Two));

            SimplifyTest(mul, expected);
        }

        [Fact(DisplayName = "2x * x")]
        public void MulSameVar6()
        {
            var mul = new Mul(new Mul(Number.Two, Variable.X), Variable.X);
            var expected = new Mul(Number.Two, new Pow(Variable.X, Number.Two));

            SimplifyTest(mul, expected);
        }

        [Fact(DisplayName = "(x * 2) * x")]
        public void MulSameVar7()
        {
            var mul = new Mul(new Mul(Variable.X, Number.Two), Variable.X);
            var expected = new Mul(Number.Two, new Pow(Variable.X, Number.Two));

            SimplifyTest(mul, expected);
        }

        [Fact(DisplayName = "2x * 3x")]
        public void MulSameVar8()
        {
            var mul = new Mul(
                new Mul(Number.Two, Variable.X),
                new Mul(new Number(3), Variable.X)
            );
            var expected = new Mul(new Number(6), new Pow(Variable.X, Number.Two));

            SimplifyTest(mul, expected);
        }

        [Fact(DisplayName = "(x * 2) * (x * 3)")]
        public void MulSameVar9()
        {
            var mul = new Mul(
                new Mul(Variable.X, Number.Two),
                new Mul(Variable.X, new Number(3))
            );
            var expected = new Mul(new Number(6), new Pow(Variable.X, Number.Two));

            SimplifyTest(mul, expected);
        }

        [Fact(DisplayName = "2x * -2x")]
        public void MulSameVar10()
        {
            var mul = new Mul(
                new Mul(Number.Two, Variable.X),
                new Mul(new Number(0.5), Variable.X)
            );
            var expected = new Pow(Variable.X, Number.Two);

            SimplifyTest(mul, expected);
        }

        [Fact(DisplayName = "2x * -3x")]
        public void MulSameVar11()
        {
            var mul = new Mul(
                new Mul(Number.Two, Variable.X),
                new Mul(new Number(-0.5), Variable.X)
            );
            var expected = new UnaryMinus(new Pow(Variable.X, Number.Two));

            SimplifyTest(mul, expected);
        }

        [Fact(DisplayName = "2x * x * (-3)")]
        public void MulSameVar12()
        {
            var mul = new Mul(
                new Mul(Number.Two, Variable.X),
                new Mul(Variable.X, new Number(-3))
            );
            var expected = new Mul(new Number(-6), new Pow(Variable.X, Number.Two));

            SimplifyTest(mul, expected);
        }

        [Fact(DisplayName = "x * 2 * -3x")]
        public void MulSameVar13()
        {
            var mul = new Mul(
                new Mul(Variable.X, Number.Two),
                new Mul(new Number(-3), Variable.X)
            );
            var expected = new Mul(new Number(-6), new Pow(Variable.X, Number.Two));

            SimplifyTest(mul, expected);
        }

        [Fact(DisplayName = "cos(cos(x)) * -sin(x)")]
        public void MulNegativeRightParamTest1()
        {
            var mul = new Mul(
                new Cos(new Cos(Variable.X)),
                new UnaryMinus(new Sin(Variable.X))
            );
            var expected = new UnaryMinus(new Mul(
                new Cos(new Cos(Variable.X)),
                new Sin(Variable.X))
            );

            SimplifyTest(mul, expected);
        }

        [Fact]
        public void MulArgumentSimplified()
        {
            var exp = new Mul(
                Variable.X,
                new Ceil(new Add(Number.One, Number.One))
            );
            var expected = new Mul(Variable.X, new Ceil(Number.Two));

            SimplifyTest(exp, expected);
        }
    }
}