// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions.Statistical;
using Xunit;

namespace xFunc.Tests.Expressions.Statistical
{
    public class ProductTest
    {
        [Fact]
        public void TwoNumbersTest()
        {
            var sum = new Product(new[] { new Number(3), Number.Two });
            var actual = sum.Execute();
            var expected = new NumberValue(6.0);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OneNumberTest()
        {
            var sum = new Product(new[] { Number.Two });
            var actual = sum.Execute();
            var expected = new NumberValue(2.0);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VectorTest()
        {
            var sum = new Product(new[] { new Vector(new[] { new Number(4), Number.Two }) });
            var actual = sum.Execute();
            var expected = new NumberValue(8.0);

            Assert.Equal(expected, actual);
        }
    }
}