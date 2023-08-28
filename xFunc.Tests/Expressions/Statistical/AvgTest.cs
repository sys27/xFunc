// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Statistical;

public class AvgTest
{
    [Test]
    public void OneNumberTest()
    {
        var exp = new Avg(new[] { Number.Two });
        var result = exp.Execute();

        Assert.That(result, Is.EqualTo(new NumberValue(2.0)));
    }

    [Test]
    public void TwoNumberTest()
    {
        var exp = new Avg(new[] { Number.Two, new Number(4) });
        var result = exp.Execute();

        Assert.That(result, Is.EqualTo(new NumberValue(3.0)));
    }

    [Test]
    public void ThreeNumberTest()
    {
        var exp = new Avg(new[] { new Number(9), Number.Two, new Number(4) });
        var result = exp.Execute();

        Assert.That(result, Is.EqualTo(new NumberValue(5.0)));
    }

    [Test]
    public void VectorTest()
    {
        var exp = new Avg(new[] { new Vector(new IExpression[] { Number.One, Number.Two, new Number(3) }) });
        var result = exp.Execute();

        Assert.That(result, Is.EqualTo(new NumberValue(2.0)));
    }
}