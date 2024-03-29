// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Statistical;

public class SumTest
{
    [Test]
    public void TwoNumbersTest()
    {
        var sum = new Sum(new[] { Number.One, Number.Two });
        var actual = sum.Execute();
        var expected = new NumberValue(3.0);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void OneNumberTest()
    {
        var sum = new Sum(new[] { Number.Two });
        var actual = sum.Execute();
        var expected = new NumberValue(2.0);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void VectorTest()
    {
        var sum = new Sum(new[] { new Vector(new IExpression[] { Number.One, Number.Two }) });
        var actual = sum.Execute();
        var expected = new NumberValue(3.0);

        Assert.That(actual, Is.EqualTo(expected));
    }
}