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
using Xunit;

namespace xFunc.Tests.Analyzers.SimplifierTests
{
    public class SubSimplifierTest : BaseSimplifierTest
    {
        [Fact]
        public void SubFirstZero()
        {
            var sub = new Sub(new Number(0), Variable.X);
            var expected = new UnaryMinus(Variable.X);

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubSecondZero()
        {
            var sub = new Sub(Variable.X, new Number(0));
            var expected = Variable.X;

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubTwoNumbers()
        {
            var sub = new Sub(new Number(3), new Number(2));
            var expected = new Number(1);

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubSecondUnaryMinus()
        {
            var sub = new Sub(new Number(2), new UnaryMinus(Variable.X));
            var expected = new Add(new Number(2), Variable.X);

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubDiff_NumAddVar_SubNum()
        {
            // (2 + x) - 2
            var sub = new Sub(new Add(new Number(2), Variable.X), new Number(2));
            var expected = Variable.X;

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubDiff_VarAddNum_SubNum()
        {
            // (x + 2) - 2
            var sub = new Sub(new Add(Variable.X, new Number(2)), new Number(2));
            var expected = Variable.X;

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubDiffNumSub_NumAddVar_()
        {
            // 2 - (2 + x)
            var sub = new Sub(new Number(2), new Add(new Number(2), Variable.X));
            var expected = new UnaryMinus(Variable.X);

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubDiffNumSub_VarAddNum_()
        {
            // 2 - (x + 2)
            var sub = new Sub(new Number(2), new Add(Variable.X, new Number(2)));
            var expected = new UnaryMinus(Variable.X);

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubDiff_NumSubVar_SubNum()
        {
            var sub = new Sub(new Sub(new Number(2), Variable.X), new Number(2));
            var expected = new UnaryMinus(Variable.X);

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubDiff_VarSubNum_SubNum()
        {
            var sub = new Sub(new Sub(Variable.X, new Number(2)), new Number(2));
            var expected = new Sub(Variable.X, new Number(4));

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubDiffNumSub_NumSubVar_()
        {
            var sub = new Sub(new Number(2), new Sub(new Number(2), Variable.X));
            var expected = Variable.X;

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubDiffNumSub_VarSubNum_()
        {
            var sub = new Sub(new Number(2), new Sub(Variable.X, new Number(2)));
            var expected = new Sub(new Number(4), Variable.X);

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubSameVars1()
        {
            // x - x
            var sub = new Sub(Variable.X, Variable.X);
            var expected = new Number(0);

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubSameVars2()
        {
            // (x - x) - x
            var x = Variable.X;
            var sub = new Sub(new Sub(x, x), x);
            var expected = new UnaryMinus(x);

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubSameVars3()
        {
            // 2x - x
            var x = Variable.X;
            var sub = new Sub(new Mul(new Number(2), x), x);
            var expected = x;

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubSameVars4()
        {
            // x - 2x
            var x = Variable.X;
            var sub = new Sub(x, new Mul(new Number(2), x));
            var expected = new UnaryMinus(x);

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubSameVars5()
        {
            // x - (x * 2)
            var x = Variable.X;
            var sub = new Sub(x, new Mul(x, new Number(2)));
            var expected = new UnaryMinus(x);

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubSameVars6()
        {
            // 2x - x
            var x = Variable.X;
            var sub = new Sub(new Mul(new Number(2), x), x);
            var expected = x;

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubSameVars7()
        {
            // (x * 2) - x
            var x = Variable.X;
            var sub = new Sub(new Mul(x, new Number(2)), x);
            var expected = x;

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubSameVars8()
        {
            // 3x - 2x
            var x = Variable.X;
            var sub = new Sub(new Mul(new Number(3), x), new Mul(new Number(2), x));
            var expected = x;

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubSameVars9()
        {
            // (x * 3) - (x * 2)
            var x = Variable.X;
            var sub = new Sub(new Mul(x, new Number(3)), new Mul(x, new Number(2)));
            var expected = x;

            SimpleTest(sub, expected);
        }

        [Fact]
        public void SubSameVars10()
        {
            // (x * 4) - (x * 2)
            var x = Variable.X;
            var sub = new Sub(new Mul(x, new Number(4)), new Mul(x, new Number(2)));
            var expected = new Mul(new Number(2), x);

            SimpleTest(sub, expected);
        }

        [Fact]
        public void AddSameVars11()
        {
            // 3x - x*2
            var var = Variable.X;
            var exp = new Sub(
                new Mul(new Number(3), var),
                new Mul(var, new Number(2)));
            var expected = var;

            SimpleTest(exp, expected);
        }

        [Fact]
        public void AddSameVars12()
        {
            // x*3 - 2x
            var var = Variable.X;
            var exp = new Sub(
                new Mul(var, new Number(3)),
                new Mul(new Number(2), var));
            var expected = var;

            SimpleTest(exp, expected);
        }
    }
}