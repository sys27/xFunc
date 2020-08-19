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
        [Fact]
        public void MulByFirstZero()
        {
            var mul = new Mul(new Number(0), Variable.X);
            var expected = new Number(0);

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulBySecondZero()
        {
            var mul = new Mul(Variable.X, new Number(0));
            var expected = new Number(0);

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulFirstOne()
        {
            var mul = new Mul(new Number(1), Variable.X);
            var expected = Variable.X;

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulSecondOne()
        {
            var mul = new Mul(Variable.X, new Number(1));
            var expected = Variable.X;

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulFirstMinusOne()
        {
            var mul = new Mul(new Number(-1), Variable.X);
            var expected = new UnaryMinus(Variable.X);

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulSecondMinusOne()
        {
            var mul = new Mul(Variable.X, new Number(-1));
            var expected = new UnaryMinus(Variable.X);

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulTwoNumbers()
        {
            var mul = new Mul(new Number(2), new Number(3));
            var expected = new Number(6);

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulDiffNumMul_NumMulVar_()
        {
            var mul = new Mul(new Number(2), new Mul(new Number(2), Variable.X));
            var expected = new Mul(new Number(4), Variable.X);

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulDiffNumMul_VarMulNum_()
        {
            var mul = new Mul(new Number(2), new Mul(Variable.X, new Number(2)));
            var expected = new Mul(new Number(4), Variable.X);

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulDiff_NumMulVar_MulNum()
        {
            var mul = new Mul(new Mul(new Number(2), Variable.X), new Number(2));
            var expected = new Mul(new Number(4), Variable.X);

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulDiff_VarMulNum_MulNum()
        {
            var mul = new Mul(new Mul(Variable.X, new Number(2)), new Number(2));
            var expected = new Mul(new Number(4), Variable.X);

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulDiffNumMul_NumDivVar_()
        {
            // 2 * (2 / x)
            var mul = new Mul(new Number(2), new Div(new Number(2), Variable.X));
            var expected = new Div(new Number(4), Variable.X);

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulDiffNumMul_VarDivNum_()
        {
            // 2 * (x / 2)
            var mul = new Mul(new Number(2), new Div(Variable.X, new Number(2)));
            var expected = Variable.X;

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulDiff_NumDivVar_MulNum()
        {
            // (2 / x) * 2
            var mul = new Mul(new Div(new Number(2), Variable.X), new Number(2));
            var expected = new Div(new Number(4), Variable.X);

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulDiff_VarDivNum_MulNum()
        {
            // (x / 2) * 2
            var mul = new Mul(new Div(Variable.X, new Number(2)), new Number(2));
            var expected = Variable.X;

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulSameVar1()
        {
            // x * x
            var var = Variable.X;
            var mul = new Mul(var, var);
            var expected = new Pow(var, new Number(2));

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulSameVar2()
        {
            // 2x * x
            var var = Variable.X;
            var mul = new Mul(new Mul(new Number(2), var), var);
            var expected = new Mul(new Number(2), new Pow(var, new Number(2)));

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulSameVar3()
        {
            // 2x * 3x
            var var = Variable.X;
            var mul = new Mul(new Mul(new Number(2), var), new Mul(new Number(3), var));
            var expected = new Mul(new Number(6), new Pow(var, new Number(2)));

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulSameVar4()
        {
            // x * 2x
            var var = Variable.X;
            var mul = new Mul(var, new Mul(new Number(2), var));
            var expected = new Mul(new Number(2), new Pow(var, new Number(2)));

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulSameVar5()
        {
            // x * (x * 2)
            var var = Variable.X;
            var mul = new Mul(var, new Mul(var, new Number(2)));
            var expected = new Mul(new Number(2), new Pow(var, new Number(2)));

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulSameVar6()
        {
            // 2x * x
            var var = Variable.X;
            var mul = new Mul(new Mul(new Number(2), var), var);
            var expected = new Mul(new Number(2), new Pow(var, new Number(2)));

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulSameVar7()
        {
            // (x * 2) * x
            var var = Variable.X;
            var mul = new Mul(new Mul(var, new Number(2)), var);
            var expected = new Mul(new Number(2), new Pow(var, new Number(2)));

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulSameVar8()
        {
            // 2x * 3x
            var var = Variable.X;
            var mul = new Mul(new Mul(new Number(2), var), new Mul(new Number(3), var));
            var expected = new Mul(new Number(6), new Pow(var, new Number(2)));

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulSameVar9()
        {
            // (x * 2) * (x * 3)
            var var = Variable.X;
            var mul = new Mul(new Mul(var, new Number(2)), new Mul(var, new Number(3)));
            var expected = new Mul(new Number(6), new Pow(var, new Number(2)));

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulSameVar10()
        {
            // 2x * -2x
            var var = Variable.X;
            var mul = new Mul(new Mul(new Number(2), var), new Mul(new Number(0.5), var));
            var expected = new Pow(var, new Number(2));

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulSameVar11()
        {
            // 2x * -3x
            var var = Variable.X;
            var mul = new Mul(new Mul(new Number(2), var), new Mul(new Number(-0.5), var));
            var expected = new UnaryMinus(new Pow(var, new Number(2)));

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulSameVar12()
        {
            // 2x * x*(-3)
            var var = Variable.X;
            var mul = new Mul(new Mul(new Number(2), var), new Mul(var, new Number(-3)));
            var expected = new Mul(new Number(-6), new Pow(var, new Number(2)));

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulSameVar13()
        {
            // x*2 * -3x
            var var = Variable.X;
            var mul = new Mul(new Mul(var, new Number(2)), new Mul(new Number(-3), var));
            var expected = new Mul(new Number(-6), new Pow(var, new Number(2)));

            SimpleTest(mul, expected);
        }

        [Fact]
        public void MulNegativeRightParamTest1()
        {
            // sin(cos(x))
            var x = Variable.X;
            var mul = new Mul(new Cos(new Cos(x)), new UnaryMinus(new Sin(x)));
            var expected = new UnaryMinus(new Mul(new Cos(new Cos(x)), new Sin(x)));

            SimpleTest(mul, expected);
        }
    }
}