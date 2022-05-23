// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.LogicalAndBitwise;

public class OrTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteTest1()
    {
        var exp = new Or(Number.One, Number.Two);
        var expected = new NumberValue(3.0);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteTest3()
    {
        var exp = new Or(Bool.True, Bool.False);

        Assert.True((bool)exp.Execute());
    }

    [Fact]
    public void ExecuteTest4()
    {
        var exp = new Or(Bool.False, Bool.False);

        Assert.False((bool)exp.Execute());
    }

    [Fact]
    public void ExecuteTestLeftIsNotInt()
    {
        var exp = new Or(new Number(1.5), Number.One);

        Assert.Throws<ArgumentException>(() => exp.Execute());
    }

    [Fact]
    public void ExecuteTestRightIsNotInt()
    {
        var exp = new Or(Number.One, new Number(1.5));

        Assert.Throws<ArgumentException>(() => exp.Execute());
    }

    [Fact]
    public void ExecuteResultIsNotSupported()
        => TestNotSupported(new Or(new ComplexNumber(1), new ComplexNumber(2)));

    [Fact]
    public void CloneTest()
    {
        var exp = new Or(Bool.True, Bool.False);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}