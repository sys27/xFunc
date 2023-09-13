// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.LogicalAndBitwise;

public class XOrTest : BaseExpressionTests
{
    [Test]
    public void ExecuteTest1()
    {
        var exp = new XOr(Number.One, Number.Two);
        var expected = new NumberValue(3.0);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteTest3()
    {
        var exp = new XOr(Bool.True, Bool.True);

        Assert.That((bool)exp.Execute(), Is.False);
    }

    [Test]
    public void ExecuteTest4()
    {
        var exp = new XOr(Bool.False, Bool.True);

        Assert.That((bool)exp.Execute(), Is.True);
    }

    [Test]
    public void ExecuteTestLeftIsNotInt()
    {
        var exp = new XOr(new Number(1.5), Number.One);

        Assert.Throws<ArgumentException>(() => exp.Execute());
    }

    [Test]
    public void ExecuteTestRightIsNotInt()
    {
        var exp = new XOr(Number.One, new Number(1.5));

        Assert.Throws<ArgumentException>(() => exp.Execute());
    }

    [Test]
    public void ExecuteResultIsNotSupported()
    {
        var exp = new XOr(new ComplexNumber(1), new ComplexNumber(2));

        TestNotSupported(exp);
    }

    [Test]
    public void CloneTest()
    {
        var exp = new XOr(Bool.True, Bool.False);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}