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
        [Fact(DisplayName = "0 - x")]
        public void SubFirstZero()
        {
            var sub = new Sub(new Number(0), Variable.X);
            var expected = new UnaryMinus(Variable.X);

            SimpleTest(sub, expected);
        }

        [Fact(DisplayName = "x - 0")]
        public void SubSecondZero()
        {
            var sub = new Sub(Variable.X, new Number(0));
            var expected = Variable.X;

            SimpleTest(sub, expected);
        }

        [Fact(DisplayName = "3 - 2")]
        public void SubTwoNumbers()
        {
            var sub = new Sub(new Number(3), new Number(2));
            var expected = new Number(1);

            SimpleTest(sub, expected);
        }

        [Fact(DisplayName = "2 - -x")]
        public void SubSecondUnaryMinus()
        {
            var sub = new Sub(new Number(2), new UnaryMinus(Variable.X));
            var expected = new Add(new Number(2), Variable.X);

            SimpleTest(sub, expected);
        }

        [Fact(DisplayName = "(2 + x) - 2")]
        public void SubDiff_NumAddVar_SubNum()
        {
            var sub = new Sub(new Add(new Number(2), Variable.X), new Number(2));
            var expected = Variable.X;

            SimpleTest(sub, expected);
        }

        [Fact(DisplayName = "(x + 2) - 2")]
        public void SubDiff_VarAddNum_SubNum()
        {
            var sub = new Sub(new Add(Variable.X, new Number(2)), new Number(2));
            var expected = Variable.X;

            SimpleTest(sub, expected);
        }

        [Fact(DisplayName = "2 - (2 + x)")]
        public void SubDiffNumSub_NumAddVar_()
        {
            var sub = new Sub(new Number(2), new Add(new Number(2), Variable.X));
            var expected = new UnaryMinus(Variable.X);

            SimpleTest(sub, expected);
        }

        [Fact(DisplayName = "2 - (x + 2)")]
        public void SubDiffNumSub_VarAddNum_()
        {
            var sub = new Sub(new Number(2), new Add(Variable.X, new Number(2)));
            var expected = new UnaryMinus(Variable.X);

            SimpleTest(sub, expected);
        }

        [Fact(DisplayName = "(2 - x) - 2")]
        public void SubDiff_NumSubVar_SubNum()
        {
            var sub = new Sub(new Sub(new Number(2), Variable.X), new Number(2));
            var expected = new UnaryMinus(Variable.X);

            SimpleTest(sub, expected);
        }

        [Fact(DisplayName = "(x - 2) - 2")]
        public void SubDiff_VarSubNum_SubNum()
        {
            var sub = new Sub(new Sub(Variable.X, new Number(2)), new Number(2));
            var expected = new Sub(Variable.X, new Number(4));

            SimpleTest(sub, expected);
        }

        [Fact(DisplayName = "2 - (2 - x)")]
        public void SubDiffNumSub_NumSubVar_()
        {
            var sub = new Sub(new Number(2), new Sub(new Number(2), Variable.X));
            var expected = Variable.X;

            SimpleTest(sub, expected);
        }

        [Fact(DisplayName = "2 - (x - 2)")]
        public void SubDiffNumSub_VarSubNum_()
        {
            var sub = new Sub(new Number(2), new Sub(Variable.X, new Number(2)));
            var expected = new Sub(new Number(4), Variable.X);

            SimpleTest(sub, expected);
        }

        [Fact(DisplayName = "x - x")]
        public void SubSameVars1()
        {
            var sub = new Sub(Variable.X, Variable.X);
            var expected = new Number(0);

            SimpleTest(sub, expected);
        }

        [Fact(DisplayName = "(x - x) - x")]
        public void SubSameVars2()
        {
            var sub = new Sub(new Sub(Variable.X, Variable.X), Variable.X);
            var expected = new UnaryMinus(Variable.X);

            SimpleTest(sub, expected);
        }

        [Fact(DisplayName = "2x - x")]
        public void SubSameVars3()
        {
            var sub = new Sub(new Mul(new Number(2), Variable.X), Variable.X);
            var expected = Variable.X;

            SimpleTest(sub, expected);
        }

        [Fact(DisplayName = "x - 2x")]
        public void SubSameVars4()
        {
            var sub = new Sub(Variable.X, new Mul(new Number(2), Variable.X));
            var expected = new UnaryMinus(Variable.X);

            SimpleTest(sub, expected);
        }

        [Fact(DisplayName = "x - (x * 2)")]
        public void SubSameVars5()
        {
            var sub = new Sub(Variable.X, new Mul(Variable.X, new Number(2)));
            var expected = new UnaryMinus(Variable.X);

            SimpleTest(sub, expected);
        }

        [Fact(DisplayName = "2x - x")]
        public void SubSameVars6()
        {
            var sub = new Sub(new Mul(new Number(2), Variable.X), Variable.X);
            var expected = Variable.X;

            SimpleTest(sub, expected);
        }

        [Fact(DisplayName = "(x * 2) - x")]
        public void SubSameVars7()
        {
            var sub = new Sub(new Mul(Variable.X, new Number(2)), Variable.X);
            var expected = Variable.X;

            SimpleTest(sub, expected);
        }

        [Fact(DisplayName = "3x - 2x")]
        public void SubSameVars8()
        {
            var sub = new Sub(
                new Mul(new Number(3), Variable.X),
                new Mul(new Number(2), Variable.X)
            );
            var expected = Variable.X;

            SimpleTest(sub, expected);
        }

        [Fact(DisplayName = "(x * 3) - (x * 2)")]
        public void SubSameVars9()
        {
            var sub = new Sub(
                new Mul(Variable.X, new Number(3)),
                new Mul(Variable.X, new Number(2))
            );
            var expected = Variable.X;

            SimpleTest(sub, expected);
        }

        [Fact(DisplayName = "(x * 4) - (x * 2)")]
        public void SubSameVars10()
        {
            var sub = new Sub(
                new Mul(Variable.X, new Number(4)),
                new Mul(Variable.X, new Number(2))
            );
            var expected = new Mul(new Number(2), Variable.X);

            SimpleTest(sub, expected);
        }

        [Fact(DisplayName = "3x - x * 2")]
        public void AddSameVars11()
        {
            var exp = new Sub(
                new Mul(new Number(3), Variable.X),
                new Mul(Variable.X, new Number(2))
            );
            var expected = Variable.X;

            SimpleTest(exp, expected);
        }

        [Fact(DisplayName = "x * 3 - 2x")]
        public void AddSameVars12()
        {
            var exp = new Sub(
                new Mul(Variable.X, new Number(3)),
                new Mul(new Number(2), Variable.X)
            );
            var expected = Variable.X;

            SimpleTest(exp, expected);
        }
    }
}