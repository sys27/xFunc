// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.LogicalAndBitwise;

public class NotTest : BaseExpressionTests
{
    [Test]
    public void ExecuteTest1()
    {
        var exp = new Not(Number.Two);
        var expected = new NumberValue(-3.0);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteTest3()
    {
        var exp = new Not(Bool.True);

        Assert.That((bool) exp.Execute(), Is.False);
    }

    [Test]
    public void ExecuteTestValueIsNotInt()
    {
        var exp = new Not(new Number(1.5));

        Assert.Throws<ArgumentException>(() => exp.Execute());
    }

    [Test]
    public void ExecuteResultIsNotSupported()
        => TestNotSupported(new Not(new ComplexNumber(1)));

    [Test]
    public void CloneTest()
    {
        var exp = new Not(Bool.False);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}