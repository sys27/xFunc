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
    public class AddSimplifierTest : BaseSimplifierTest
    {
        [Fact(DisplayName = "0 + x")]
        public void AddFirstZero()
        {
            var add = new Add(Number.Zero, Variable.X);
            var expected = Variable.X;

            SimplifyTest(add, expected);
        }

        [Fact(DisplayName = "x + 0")]
        public void AddSecondZero()
        {
            var add = new Add(Variable.X, Number.Zero);
            var expected = Variable.X;

            SimplifyTest(add, expected);
        }

        [Fact(DisplayName = "3 + 2")]
        public void AddTwoNumbers()
        {
            var add = new Add(new Number(3), Number.Two);
            var expected = new Number(5);

            SimplifyTest(add, expected);
        }

        [Fact(DisplayName = "-x + 2")]
        public void AddFirstUnaryMinus()
        {
            var add = new Add(new UnaryMinus(Variable.X), Number.Two);
            var expected = new Sub(Number.Two, Variable.X);

            SimplifyTest(add, expected);
        }

        [Fact(DisplayName = "2 + (-x)")]
        public void AddSecondUnaryMinus()
        {
            var add = new Add(Number.Two, new UnaryMinus(Variable.X));
            var expected = new Sub(Number.Two, Variable.X);

            SimplifyTest(add, expected);
        }

        [Fact(DisplayName = "2 + (2 + x)")]
        public void AddDiffNumAdd_NumAddVar_()
        {
            var add = new Add(Number.Two, new Add(Number.Two, Variable.X));
            var expected = new Add(Variable.X, new Number(4));

            SimplifyTest(add, expected);
        }

        [Fact(DisplayName = "2 + (x + 2)")]
        public void AddDiffNumAdd_VarAddNum_()
        {
            var add = new Add(Number.Two, new Add(Variable.X, Number.Two));
            var expected = new Add(Variable.X, new Number(4));

            SimplifyTest(add, expected);
        }

        [Fact(DisplayName = "(2 + x) + 2")]
        public void AddDiff_NumAddVar_AddNum()
        {
            var add = new Add(new Add(Number.Two, Variable.X), Number.Two);
            var expected = new Add(Variable.X, new Number(4));

            SimplifyTest(add, expected);
        }

        [Fact(DisplayName = "(x + 2) + 2")]
        public void AddDiff_VarAddNum_AddNum()
        {
            var add = new Add(new Add(Variable.X, Number.Two), Number.Two);
            var expected = new Add(Variable.X, new Number(4));

            SimplifyTest(add, expected);
        }

        [Fact(DisplayName = "2 + (2 - x)")]
        public void AddDiffNum_NumSubVar_()
        {
            var add = new Add(Number.Two, new Sub(Number.Two, Variable.X));
            var expected = new Sub(new Number(4), Variable.X);

            SimplifyTest(add, expected);
        }

        [Fact(DisplayName = "2 + (x - 2)")]
        public void AddDiffNum_VarSubNum_()
        {
            var add = new Add(Number.Two, new Sub(Variable.X, Number.Two));
            var expected = Variable.X;

            SimplifyTest(add, expected);
        }

        [Fact(DisplayName = "(2 - x) + 2")]
        public void AddDiff_NumSubVar_AddNum()
        {
            var add = new Add(new Sub(Number.Two, Variable.X), Number.Two);
            var expected = new Sub(new Number(4), Variable.X);

            SimplifyTest(add, expected);
        }

        [Fact(DisplayName = "(x - 2) + 2")]
        public void AddDiff_VarSubNum_AddNum()
        {
            var add = new Add(new Sub(Variable.X, Number.Two), Number.Two);
            var expected = Variable.X;

            SimplifyTest(add, expected);
        }

        [Fact(DisplayName = "x + x")]
        public void AddSaveVars1()
        {
            var exp = new Add(Variable.X, Variable.X);
            var expected = new Mul(Number.Two, Variable.X);

            SimplifyTest(exp, expected);
        }

        [Fact(DisplayName = "2x + x")]
        public void AddSaveVars2()
        {
            var exp = new Add(new Mul(Number.Two, Variable.X), Variable.X);
            var expected = new Mul(new Number(3), Variable.X);

            SimplifyTest(exp, expected);
        }

        [Fact(DisplayName = "x + 2x")]
        public void AddSaveVars3()
        {
            var exp = new Add(Variable.X, new Mul(Number.Two, Variable.X));
            var expected = new Mul(new Number(3), Variable.X);

            SimplifyTest(exp, expected);
        }

        [Fact(DisplayName = "x + x * 2")]
        public void AddSaveVars4()
        {
            var exp = new Add(Variable.X, new Mul(Variable.X, Number.Two));
            var expected = new Mul(new Number(3), Variable.X);

            SimplifyTest(exp, expected);
        }

        [Fact(DisplayName = "2x + 3x")]
        public void AddSaveVars5()
        {
            var exp = new Add(
                new Mul(Number.Two, Variable.X),
                new Mul(new Number(3), Variable.X)
            );
            var expected = new Mul(new Number(5), Variable.X);

            SimplifyTest(exp, expected);
        }

        [Fact(DisplayName = "-x + x")]
        public void AddSaveVars6()
        {
            var exp = new Add(new UnaryMinus(Variable.X), Variable.X);
            var expected = Number.Zero;

            SimplifyTest(exp, expected);
        }

        [Fact(DisplayName = "-x + 2x")]
        public void AddSaveVars7()
        {
            var exp = new Add(
                new UnaryMinus(Variable.X),
                new Mul(Number.Two, Variable.X)
            );
            var expected = Variable.X;

            SimplifyTest(exp, expected);
        }

        [Fact(DisplayName = "x * 2 + x")]
        public void AddSaveVars8()
        {
            var exp = new Add(new Mul(Variable.X, Number.Two), Variable.X);
            var expected = new Mul(new Number(3), Variable.X);

            SimplifyTest(exp, expected);
        }

        [Fact(DisplayName = "x * 2 + x * 3")]
        public void AddSaveVars9()
        {
            var exp = new Add(
                new Mul(Variable.X, Number.Two),
                new Mul(Variable.X, new Number(3))
            );
            var expected = new Mul(new Number(5), Variable.X);

            SimplifyTest(exp, expected);
        }

        [Fact(DisplayName = "3x + -2x")]
        public void AddSaveVars10()
        {
            var exp = new Add(
                new Mul(new Number(3), Variable.X),
                new Mul(new Number(-2), Variable.X)
            );
            var expected = Variable.X;

            SimplifyTest(exp, expected);
        }

        [Fact(DisplayName = "3x + -4x")]
        public void AddSaveVars11()
        {
            var exp = new Add(
                new Mul(new Number(3), Variable.X),
                new Mul(new Number(-4), Variable.X)
            );
            var expected = new UnaryMinus(Variable.X);

            SimplifyTest(exp, expected);
        }

        [Fact(DisplayName = "-2x + x * 3")]
        public void AddSameVars12()
        {
            var exp = new Add(
                new Mul(new Number(-2), Variable.X),
                new Mul(Variable.X, new Number(3))
            );
            var expected = Variable.X;

            SimplifyTest(exp, expected);
        }

        [Fact(DisplayName = "x * 3 + -2x")]
        public void AddSameVars13()
        {
            var exp = new Add(
                new Mul(Variable.X, new Number(3)),
                new Mul(new Number(-2), Variable.X)
            );
            var expected = Variable.X;

            SimplifyTest(exp, expected);
        }

        [Fact]
        public void AddArgumentSimplified()
        {
            var exp = new Add(
                Variable.X,
                new Ceil(new Add(Number.One, Number.One))
            );
            var expected = new Add(Variable.X, new Ceil(Number.Two));

            SimplifyTest(exp, expected);
        }
    }
}