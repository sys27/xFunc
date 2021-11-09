// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class ToHexTest : BaseExpressionTests
{
    [Theory]
    [InlineData(0x7, "0x07")]
    [InlineData(0x7FF, "0x07FF")]
    [InlineData(0x7FFFF, "0x07FFFF")]
    [InlineData(0x7FFFFFF, "0x07FFFFFF")]
    public void ExecuteNumberTest(double number, string result)
    {
        var exp = new ToHex(new Number(number));

        Assert.Equal(result, exp.Execute());
    }

    [Fact]
    public void ExecuteNumberExceptionTest()
    {
        var exp = new ToHex(new Number(2.5));

        Assert.Throws<ArgumentException>(() => exp.Execute());
    }

    [Fact]
    public void ExecuteLongMaxNumberTest()
    {
        var exp = new ToHex(new Number(int.MaxValue));

        Assert.Equal("0x7FFFFFFF", exp.Execute());
    }

    [Fact]
    public void ExecuteNegativeNumberTest()
    {
        var exp = new ToHex(new Number(-2));

        Assert.Equal("0xFFFFFFFE", exp.Execute());
    }

    [Fact]
    public void ExecuteBoolTest()
        => TestNotSupported(new ToHex(Bool.False));

    [Fact]
    public void CloseTest()
    {
        var exp = new ToHex(new Number(10));
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}