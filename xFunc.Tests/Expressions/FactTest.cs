// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class FactTest : BaseExpressionTests
{
    [Test]
    public void ExecuteTest1()
    {
        var fact = new Fact(new Number(4));
        var expected = new NumberValue(24.0);

        Assert.That(fact.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteTest2()
    {
        var fact = new Fact(Number.Zero);
        var expected = new NumberValue(1.0);

        Assert.That(fact.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteTest3()
    {
        var fact = new Fact(Number.One);
        var expected = new NumberValue(1.0);

        Assert.That(fact.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteTest4()
    {
        var fact = new Fact(new Number(-1));
        var actual = (NumberValue)fact.Execute();

        Assert.True(actual.IsNaN);
    }

    [Test]
    public void ExecuteTestException()
        => TestNotSupported(new Fact(Bool.False));

    [Test]
    public void CloneTest()
    {
        var exp = new Fact(Number.Zero);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}