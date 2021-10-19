// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Units.AngleUnits;
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

            SimplifyTest(div, expected);
        }

        [Fact(DisplayName = "x / 0")]
        public void DivByZero()
        {
            var div = new Div(Variable.X, Number.Zero);

            Assert.Throws<DivideByZeroException>(() => SimplifyTest(div, null));
        }

        [Fact(DisplayName = "0 / 0")]
        public void ZeroDivByZero()
        {
            var div = new Div(Number.Zero, Number.Zero);
            var actual = (Number)div.Analyze(simplifier);

            Assert.True(actual.Value.IsNaN);
        }

        [Fact(DisplayName = "x / 1")]
        public void DivByOne()
        {
            var div = new Div(Variable.X, Number.One);
            var expected = Variable.X;

            SimplifyTest(div, expected);
        }

        [Fact(DisplayName = "8 / 2")]
        public void DivTwoNumbers()
        {
            var div = new Div(new Number(8), Number.Two);
            var expected = new Number(4);

            SimplifyTest(div, expected);
        }

        [Fact(DisplayName = "90 / 2 deg")]
        public void DivNumberAngle()
        {
            var div = new Div(
                new Number(90),
                AngleValue.Degree(2).AsExpression()
            );
            var expected = AngleValue.Degree(45).AsExpression();

            SimplifyTest(div, expected);
        }

        [Fact(DisplayName = "90 deg / 2")]
        public void DivAngleNumber()
        {
            var div = new Div(
                AngleValue.Degree(90).AsExpression(),
                new Number(2)
            );
            var expected = AngleValue.Degree(45).AsExpression();

            SimplifyTest(div, expected);
        }

        [Fact(DisplayName = "2 rad / 90 deg")]
        public void DivTwoAngles()
        {
            var div = new Div(
                AngleValue.Radian(2).AsExpression(),
                AngleValue.Degree(90).AsExpression()
            );
            var expected = AngleValue.Degree(114.59155902616465 / 90).AsExpression();

            SimplifyTest(div, expected);
        }

        [Fact(DisplayName = "(2 * x) / 4")]
        public void DivDiff_NumMulVar_DivNum()
        {
            var div = new Div(new Mul(Number.Two, Variable.X), new Number(4));
            var expected = new Div(Variable.X, Number.Two);

            SimplifyTest(div, expected);
        }

        [Fact(DisplayName = "(x * 2) / 4")]
        public void DivDiff_VarMulNum_DivNum()
        {
            var div = new Div(new Mul(Variable.X, Number.Two), new Number(4));
            var expected = new Div(Variable.X, Number.Two);

            SimplifyTest(div, expected);
        }

        [Fact(DisplayName = "2 / (2 * x)")]
        public void DivDiffNumDiv_NumMulVar_()
        {
            var div = new Div(Number.Two, new Mul(Number.Two, Variable.X));
            var expected = new Div(Number.One, Variable.X);

            SimplifyTest(div, expected);
        }

        [Fact(DisplayName = "2 / (2 * x)")]
        public void DivDiffNumDiv_VarMulNum_()
        {
            var div = new Div(Number.Two, new Mul(Variable.X, Number.Two));
            var expected = new Div(Number.One, Variable.X);

            SimplifyTest(div, expected);
        }

        [Fact(DisplayName = "(2 / x) / 2")]
        public void DivDiff_NumDivVar_DivNum()
        {
            var div = new Div(new Div(Number.Two, Variable.X), Number.Two);
            var expected = new Div(Number.One, Variable.X);

            SimplifyTest(div, expected);
        }

        [Fact(DisplayName = "(x / 2) / 2")]
        public void DivDiff_VarDivNum_DivNum()
        {
            var div = new Div(new Div(Variable.X, Number.Two), Number.Two);
            var expected = new Div(Variable.X, new Number(4));

            SimplifyTest(div, expected);
        }

        [Fact(DisplayName = "2 / (2 / x)")]
        public void DivDiffNumDiv_NumDivVar_()
        {
            var div = new Div(Number.Two, new Div(Number.Two, Variable.X));
            var expected = Variable.X;

            SimplifyTest(div, expected);
        }

        [Fact(DisplayName = "2 / (x / 2)")]
        public void DivDiffNumDiv_VarDivNum_()
        {
            var div = new Div(Number.Two, new Div(Variable.X, Number.Two));
            var expected = new Div(new Number(4), Variable.X);

            SimplifyTest(div, expected);
        }

        [Fact(DisplayName = "x / x")]
        public void DivSameVars()
        {
            var div = new Div(Variable.X, Variable.X);
            var expected = Number.One;

            SimplifyTest(div, expected);
        }

        [Fact]
        public void DivArgumentSimplified()
        {
            var exp = new Div(
                Variable.X,
                new Ceil(new Add(Number.One, Number.One))
            );
            var expected = new Div(Variable.X, new Ceil(Number.Two));

            SimplifyTest(exp, expected);
        }
    }
}