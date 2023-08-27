// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Statistical;

public class ProductTest
{
    [Test]
    public void TwoNumbersTest()
    {
        var sum = new Product(new[] { new Number(3), Number.Two });
        var actual = sum.Execute();
        var expected = new NumberValue(6.0);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void OneNumberTest()
    {
        var sum = new Product(new[] { Number.Two });
        var actual = sum.Execute();
        var expected = new NumberValue(2.0);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void VectorTest()
    {
        var sum = new Product(new[] { new Vector(new IExpression[] { new Number(4), Number.Two }) });
        var actual = sum.Execute();
        var expected = new NumberValue(8.0);

        Assert.That(actual, Is.EqualTo(expected));
    }
}