// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.LogicalAndBitwise;

public class OrTest : BaseExpressionTests
{
    [Test]
    public void ExecuteTest1()
    {
        var exp = new Or(Number.One, Number.Two);
        var expected = new NumberValue(3.0);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteTest3()
    {
        var exp = new Or(Bool.True, Bool.False);

        Assert.That((bool)exp.Execute(), Is.True);
    }

    [Test]
    public void ExecuteTest4()
    {
        var exp = new Or(Bool.False, Bool.False);

        Assert.That((bool)exp.Execute(), Is.False);
    }

    [Test]
    public void ExecuteTestLeftIsNotInt()
    {
        var exp = new Or(new Number(1.5), Number.One);

        Assert.Throws<ArgumentException>(() => exp.Execute());
    }

    [Test]
    public void ExecuteTestRightIsNotInt()
    {
        var exp = new Or(Number.One, new Number(1.5));

        Assert.Throws<ArgumentException>(() => exp.Execute());
    }

    [Test]
    public void ExecuteResultIsNotSupported()
        => TestNotSupported(new Or(new ComplexNumber(1), new ComplexNumber(2)));

    [Test]
    public void CloneTest()
    {
        var exp = new Or(Bool.True, Bool.False);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}