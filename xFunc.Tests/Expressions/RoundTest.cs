// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using Xunit;

namespace xFunc.Tests.Expressions
{
    public class RoundTest : BaseExpressionTests
    {
        [Fact]
        public void CalculateRoundWithoutDigits()
        {
            var round = new Round(new Number(5.555555));
            var result = round.Execute();
            var expected = new NumberValue(6.0);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void CalculateRoundWithDigits()
        {
            var round = new Round(new Number(5.555555), Number.Two);
            var result = round.Execute();
            var expected = new NumberValue(5.56);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ExecuteArgumentIsNotNumber()
        {
            var exp = new Round(Bool.False, Number.Two);

            TestNotSupported(exp);
        }

        [Fact]
        public void ExecuteDigitsIsNotNumber()
            => TestNotSupported(new Round(new Number(5.5), Bool.False));

        [Fact]
        public void ExecuteArgumentIsNotInteger()
        {
            var exp = new Round(new Number(5.5), new Number(2.5));

            Assert.Throws<InvalidOperationException>(() => exp.Execute());
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Round(new Number(5.555555), Number.Two);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}