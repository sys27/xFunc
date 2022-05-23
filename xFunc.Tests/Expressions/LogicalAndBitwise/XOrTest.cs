// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.LogicalAndBitwise;

public class XOrTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteTest1()
    {
        var exp = new XOr(Number.One, Number.Two);
        var expected = new NumberValue(3.0);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteTest3()
    {
        var exp = new XOr(Bool.True, Bool.True);

        Assert.False((bool)exp.Execute());
    }

    [Fact]
    public void ExecuteTest4()
    {
        var exp = new XOr(Bool.False, Bool.True);

        Assert.True((bool)exp.Execute());
    }

    [Fact]
    public void ExecuteTestLeftIsNotInt()
    {
        var exp = new XOr(new Number(1.5), Number.One);

        Assert.Throws<ArgumentException>(() => exp.Execute());
    }

    [Fact]
    public void ExecuteTestRightIsNotInt()
    {
        var exp = new XOr(Number.One, new Number(1.5));

        Assert.Throws<ArgumentException>(() => exp.Execute());
    }

    [Fact]
    public void ExecuteResultIsNotSupported()
    {
        var exp = new XOr(new ComplexNumber(1), new ComplexNumber(2));

        TestNotSupported(exp);
    }

    [Fact]
    public void CloneTest()
    {
        var exp = new XOr(Bool.True, Bool.False);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}