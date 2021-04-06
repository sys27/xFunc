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
using xFunc.Maths.Expressions.Units.AngleUnits;
using Xunit;

namespace xFunc.Tests.Analyzers.SimplifierTests
{
    public class SubSimplifierTest : BaseSimplifierTest
    {
        [Fact(DisplayName = "0 - x")]
        public void SubFirstZero()
        {
            var sub = new Sub(Number.Zero, Variable.X);
            var expected = new UnaryMinus(Variable.X);

            SimplifyTest(sub, expected);
        }

        [Fact(DisplayName = "x - 0")]
        public void SubSecondZero()
        {
            var sub = new Sub(Variable.X, Number.Zero);
            var expected = Variable.X;

            SimplifyTest(sub, expected);
        }

        [Fact(DisplayName = "3 - 2")]
        public void SubTwoNumbers()
        {
            var sub = new Sub(new Number(3), Number.Two);
            var expected = Number.One;

            SimplifyTest(sub, expected);
        }

        [Fact(DisplayName = "90 - 2 deg")]
        public void SubNumberAngle()
        {
            var sub = new Sub(
                new Number(90),
                AngleValue.Degree(2).AsExpression()
            );
            var expected = AngleValue.Degree(88).AsExpression();

            SimplifyTest(sub, expected);
        }

        [Fact(DisplayName = "90 deg - 2")]
        public void SubAngleNumber()
        {
            var sub = new Sub(
                AngleValue.Degree(90).AsExpression(),
                new Number(2)
            );
            var expected = AngleValue.Degree(88).AsExpression();

            SimplifyTest(sub, expected);
        }

        [Fact(DisplayName = "2 rad - 90 deg")]
        public void SubTwoAngles()
        {
            var sub = new Sub(
                AngleValue.Radian(2).AsExpression(),
                AngleValue.Degree(90).AsExpression()
            );
            var expected = AngleValue.Degree(114.59155902616465 - 90).AsExpression();

            SimplifyTest(sub, expected);
        }

        [Fact(DisplayName = "2 - -x")]
        public void SubSecondUnaryMinus()
        {
            var sub = new Sub(Number.Two, new UnaryMinus(Variable.X));
            var expected = new Add(Number.Two, Variable.X);

            SimplifyTest(sub, expected);
        }

        [Fact(DisplayName = "(2 + x) - 2")]
        public void SubDiff_NumAddVar_SubNum()
        {
            var sub = new Sub(new Add(Number.Two, Variable.X), Number.Two);
            var expected = Variable.X;

            SimplifyTest(sub, expected);
        }

        [Fact(DisplayName = "(x + 2) - 2")]
        public void SubDiff_VarAddNum_SubNum()
        {
            var sub = new Sub(new Add(Variable.X, Number.Two), Number.Two);
            var expected = Variable.X;

            SimplifyTest(sub, expected);
        }

        [Fact(DisplayName = "2 - (2 + x)")]
        public void SubDiffNumSub_NumAddVar_()
        {
            var sub = new Sub(Number.Two, new Add(Number.Two, Variable.X));
            var expected = new UnaryMinus(Variable.X);

            SimplifyTest(sub, expected);
        }

        [Fact(DisplayName = "2 - (x + 2)")]
        public void SubDiffNumSub_VarAddNum_()
        {
            var sub = new Sub(Number.Two, new Add(Variable.X, Number.Two));
            var expected = new UnaryMinus(Variable.X);

            SimplifyTest(sub, expected);
        }

        [Fact(DisplayName = "(2 - x) - 2")]
        public void SubDiff_NumSubVar_SubNum()
        {
            var sub = new Sub(new Sub(Number.Two, Variable.X), Number.Two);
            var expected = new UnaryMinus(Variable.X);

            SimplifyTest(sub, expected);
        }

        [Fact(DisplayName = "(x - 2) - 2")]
        public void SubDiff_VarSubNum_SubNum()
        {
            var sub = new Sub(new Sub(Variable.X, Number.Two), Number.Two);
            var expected = new Sub(Variable.X, new Number(4));

            SimplifyTest(sub, expected);
        }

        [Fact(DisplayName = "2 - (2 - x)")]
        public void SubDiffNumSub_NumSubVar_()
        {
            var sub = new Sub(Number.Two, new Sub(Number.Two, Variable.X));
            var expected = Variable.X;

            SimplifyTest(sub, expected);
        }

        [Fact(DisplayName = "2 - (x - 2)")]
        public void SubDiffNumSub_VarSubNum_()
        {
            var sub = new Sub(Number.Two, new Sub(Variable.X, Number.Two));
            var expected = new Sub(new Number(4), Variable.X);

            SimplifyTest(sub, expected);
        }

        [Fact(DisplayName = "x - x")]
        public void SubSameVars1()
        {
            var sub = new Sub(Variable.X, Variable.X);
            var expected = Number.Zero;

            SimplifyTest(sub, expected);
        }

        [Fact(DisplayName = "(x - x) - x")]
        public void SubSameVars2()
        {
            var sub = new Sub(new Sub(Variable.X, Variable.X), Variable.X);
            var expected = new UnaryMinus(Variable.X);

            SimplifyTest(sub, expected);
        }

        [Fact(DisplayName = "2x - x")]
        public void SubSameVars3()
        {
            var sub = new Sub(new Mul(Number.Two, Variable.X), Variable.X);
            var expected = Variable.X;

            SimplifyTest(sub, expected);
        }

        [Fact(DisplayName = "x - 2x")]
        public void SubSameVars4()
        {
            var sub = new Sub(Variable.X, new Mul(Number.Two, Variable.X));
            var expected = new UnaryMinus(Variable.X);

            SimplifyTest(sub, expected);
        }

        [Fact(DisplayName = "(x + y) - 2 * (x + y)")]
        public void SubComplexX()
        {
            var sub = new Sub(
                new Add(Variable.X, Variable.Y),
                new Mul(Number.Two, new Add(Variable.X, Variable.Y))
            );
            var expected = new UnaryMinus(new Add(Variable.X, Variable.Y));

            SimplifyTest(sub, expected);
        }

        [Fact(DisplayName = "x - (x * 2)")]
        public void SubSameVars5()
        {
            var sub = new Sub(Variable.X, new Mul(Variable.X, Number.Two));
            var expected = new UnaryMinus(Variable.X);

            SimplifyTest(sub, expected);
        }

        [Fact(DisplayName = "2 * (x + y) - (x + y)")]
        public void SubComplexX2()
        {
            var sub = new Sub(
                new Mul(Number.Two, new Add(Variable.X, Variable.Y)),
                new Add(Variable.X, Variable.Y)
            );
            var expected = new Add(Variable.X, Variable.Y);

            SimplifyTest(sub, expected);
        }

        [Fact(DisplayName = "2x - x")]
        public void SubSameVars6()
        {
            var sub = new Sub(new Mul(Number.Two, Variable.X), Variable.X);
            var expected = Variable.X;

            SimplifyTest(sub, expected);
        }

        [Fact(DisplayName = "(x * 2) - x")]
        public void SubSameVars7()
        {
            var sub = new Sub(new Mul(Variable.X, Number.Two), Variable.X);
            var expected = Variable.X;

            SimplifyTest(sub, expected);
        }

        [Fact(DisplayName = "3x - 2x")]
        public void SubSameVars8()
        {
            var sub = new Sub(
                new Mul(new Number(3), Variable.X),
                new Mul(Number.Two, Variable.X)
            );
            var expected = Variable.X;

            SimplifyTest(sub, expected);
        }

        [Fact(DisplayName = "3 * (x + y) - 2 * (x + y)")]
        public void SubComplexX3()
        {
            var sub = new Sub(
                new Mul(new Number(3), new Add(Variable.X, Variable.Y)),
                new Mul(Number.Two, new Add(Variable.X, Variable.Y))
            );
            var expected = new Add(Variable.X, Variable.Y);

            SimplifyTest(sub, expected);
        }

        [Fact(DisplayName = "(x * 3) - (x * 2)")]
        public void SubSameVars9()
        {
            var sub = new Sub(
                new Mul(Variable.X, new Number(3)),
                new Mul(Variable.X, Number.Two)
            );
            var expected = Variable.X;

            SimplifyTest(sub, expected);
        }

        [Fact(DisplayName = "(x * 4) - (x * 2)")]
        public void SubSameVars10()
        {
            var sub = new Sub(
                new Mul(Variable.X, new Number(4)),
                new Mul(Variable.X, Number.Two)
            );
            var expected = new Mul(Number.Two, Variable.X);

            SimplifyTest(sub, expected);
        }

        [Fact(DisplayName = "3x - x * 2")]
        public void AddSameVars11()
        {
            var exp = new Sub(
                new Mul(new Number(3), Variable.X),
                new Mul(Variable.X, Number.Two)
            );
            var expected = Variable.X;

            SimplifyTest(exp, expected);
        }

        [Fact(DisplayName = "x * 3 - 2x")]
        public void AddSameVars12()
        {
            var exp = new Sub(
                new Mul(Variable.X, new Number(3)),
                new Mul(Number.Two, Variable.X)
            );
            var expected = Variable.X;

            SimplifyTest(exp, expected);
        }

        [Fact]
        public void SubArgumentSimplified()
        {
            var exp = new Sub(
                Variable.X,
                new Ceil(new Add(Number.One, Number.One))
            );
            var expected = new Sub(Variable.X, new Ceil(Number.Two));

            SimplifyTest(exp, expected);
        }
    }
}