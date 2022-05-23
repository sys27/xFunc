// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class ToBinTest : BaseExpressionTests
{
    [Theory]
    [InlineData(0x7F, "0b01111111")]
    [InlineData(0x7FFF, "0b0111111111111111")]
    [InlineData(0x7FFFFF, "0b011111111111111111111111")]
    [InlineData(0x7FFFFFFF, "0b01111111111111111111111111111111")]
    public void ExecuteNumberTest(double number, string result)
    {
        var exp = new ToBin(new Number(number));

        Assert.Equal(result, exp.Execute());
    }

    [Fact]
    public void ExecuteNumberExceptionTest()
    {
        var exp = new ToBin(new Number(2.5));

        Assert.Throws<ArgumentException>(() => exp.Execute());
    }

    [Fact]
    public void ExecuteLongMaxNumberTest()
    {
        var exp = new ToBin(new Number(int.MaxValue));

        Assert.Equal("0b01111111111111111111111111111111", exp.Execute());
    }

    [Fact]
    public void ExecuteNegativeNumberTest()
    {
        var exp = new ToBin(new Number(-2));

        Assert.Equal("0b11111111111111111111111111111110", exp.Execute());
    }

    [Fact]
    public void ExecuteBoolTest()
        => TestNotSupported(new ToBin(Bool.False));

    [Fact]
    public void CloseTest()
    {
        var exp = new ToBin(new Number(10));
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}