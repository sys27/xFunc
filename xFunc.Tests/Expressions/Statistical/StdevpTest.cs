// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Statistical;

public class StdevpTest
{
    [Test]
    public void OneNumberTest()
    {
        var exp = new Stdevp(new[] { new Number(4) });
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(0.0);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void TwoNumberTest()
    {
        var exp = new Stdevp(new[] { new Number(4), new Number(9) });
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(2.5);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ThreeNumberTest()
    {
        var exp = new Stdevp(new[] { new Number(9), Number.Two, new Number(4) });
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(2.94392028877595);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void VectorTest()
    {
        var exp = new Stdevp(new[] { new Vector(new[] { Number.Two, new Number(4), new Number(9) }) });
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(2.94392028877595);

        Assert.That(result, Is.EqualTo(expected));
    }
}