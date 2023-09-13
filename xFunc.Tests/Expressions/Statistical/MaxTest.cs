// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Statistical;

public class MaxTest
{
    [Test]
    public void OneNumberTest()
    {
        var exp = new Max(new[] { Number.Two });
        var result = exp.Execute();
        var expected = new NumberValue(2.0);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void TwoNumberTest()
    {
        var exp = new Max(new[] { Number.Two, new Number(4) });
        var result = exp.Execute();
        var expected = new NumberValue(4.0);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ThreeNumberTest()
    {
        var exp = new Max(new[] { new Number(9), Number.Two, new Number(4) });
        var result = exp.Execute();
        var expected = new NumberValue(9.0);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void VectorTest()
    {
        var exp = new Max(new[] { new Vector(new IExpression[] { Number.One, Number.Two, new Number(3) }) });
        var result = exp.Execute();
        var expected = new NumberValue(3.0);

        Assert.That(result, Is.EqualTo(expected));
    }
}