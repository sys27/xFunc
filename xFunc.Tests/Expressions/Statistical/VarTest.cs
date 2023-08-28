// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Statistical;

public class VarTest
{
    [Test]
    public void OneNumberTest()
    {
        var exp = new Var(new[] { new Number(4) });
        var result = (NumberValue)exp.Execute();

        Assert.True(result.IsNaN);
    }

    [Test]
    public void TwoNumberTest()
    {
        var exp = new Var(new[] { new Number(4), new Number(9) });
        var result = exp.Execute();
        var expected = new NumberValue(12.5);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ThreeNumberTest()
    {
        var exp = new Var(new[] { new Number(9), Number.Two, new Number(4) });
        var result = exp.Execute();
        var expected = new NumberValue(13.0);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void VectorTest()
    {
        var exp = new Var(new[] { new Vector(new IExpression[] { Number.Two, new Number(4), new Number(9) }) });
        var result = exp.Execute();
        var expected = new NumberValue(13.0);

        Assert.That(result, Is.EqualTo(expected));
    }
}