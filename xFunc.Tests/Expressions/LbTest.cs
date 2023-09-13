// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class LbTest : BaseExpressionTests
{
    [Test]
    public void ExecuteTest()
    {
        var exp = new Lb(Number.Two);
        var expected = new NumberValue(Math.Log(2, 2));

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteRationalTest()
    {
        var exp = new Lb(new Rational(new Number(2), new Number(3)));
        var expected = new NumberValue(-0.5849625007211563);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteTestException()
        => TestNotSupported(new Lb(Bool.False));

    [Test]
    public void CloneTest()
    {
        var exp = new Lb(new Number(5));
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}