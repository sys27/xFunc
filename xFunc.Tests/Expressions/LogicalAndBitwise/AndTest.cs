// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.LogicalAndBitwise;

public class AndTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteTest1()
    {
        var exp = new And(Number.One, new Number(3));
        var expected = new NumberValue(1.0);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteTest3()
    {
        var exp = new And(Bool.True, Bool.False);

        Assert.False((bool)exp.Execute());
    }

    [Fact]
    public void ExecuteTest4()
    {
        var exp = new And(Bool.True, Bool.True);

        Assert.True((bool)exp.Execute());
    }

    [Fact]
    public void ExecuteTestLeftIsNotInt()
    {
        var exp = new And(new Number(1.5), Number.One);

        Assert.Throws<ArgumentException>(() => exp.Execute());
    }

    [Fact]
    public void ExecuteTestRightIsNotInt()
    {
        var exp = new And(Number.One, new Number(1.5));

        Assert.Throws<ArgumentException>(() => exp.Execute());
    }

    [Fact]
    public void ExecuteResultIsNotSupported()
    {
        var exp = new And(new ComplexNumber(1), new ComplexNumber(2));

        TestNotSupported(exp);
    }

    [Fact]
    public void CloneTest()
    {
        var exp = new And(Bool.True, Bool.False);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}