// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class ModTest : BaseExpressionTests
{
    [Test]
    public void ExecuteTest1()
    {
        var exp = new Mod(new Number(25), new Number(7));
        var result = exp.Execute();
        var expected = new NumberValue(4.0);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteTest2()
    {
        var exp = new Mod(new Number(25), new Number(5));
        var result = exp.Execute();

        Assert.That(result, Is.EqualTo(new NumberValue(0.0)));
    }

    [Test]
    public void ExecuteTest3()
    {
        var exp = new Mod(Number.Zero, new Number(5));
        var result = exp.Execute();

        Assert.That(result, Is.EqualTo(new NumberValue(0.0)));
    }

    [Test]
    public void ExecuteTest4()
    {
        var exp = new Mod(new Number(5), Number.Zero);
        var result = (NumberValue)exp.Execute();

        Assert.True(result.IsNaN);
    }

    [Test]
    public void ExecuteResultIsNotSupported()
        => TestNotSupported(new Mod(Bool.True, Bool.False));

    [Test]
    public void CloneTest()
    {
        var exp = new Mod(new Number(5), Number.Zero);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}