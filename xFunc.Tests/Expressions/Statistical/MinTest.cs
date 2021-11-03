// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions.Statistical;
using Xunit;

namespace xFunc.Tests.Expressions.Statistical
{
    public class MinTest
    {
        [Fact]
        public void OneNumberTest()
        {
            var exp = new Min(new[] { Number.Two });
            var result = exp.Execute();

            Assert.Equal(new NumberValue(2.0), result);
        }

        [Fact]
        public void TwoNumberTest()
        {
            var exp = new Min(new[] { Number.Two, new Number(4) });
            var result = exp.Execute();

            Assert.Equal(new NumberValue(2.0), result);
        }

        [Fact]
        public void ThreeNumberTest()
        {
            var exp = new Min(new[] { new Number(9), Number.Two, new Number(4) });
            var result = exp.Execute();

            Assert.Equal(new NumberValue(2.0), result);
        }

        [Fact]
        public void VectorTest()
        {
            var exp = new Min(new[] { new Vector(new[] { Number.One, Number.Two, new Number(3) }) });
            var result = exp.Execute();

            Assert.Equal(new NumberValue(1.0), result);
        }
    }
}