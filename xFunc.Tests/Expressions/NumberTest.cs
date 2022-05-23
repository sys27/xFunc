// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class NumberTest
{
    [Fact]
    public void EqualsNumberNullTest()
    {
        var number = Number.Zero;

        Assert.False(number.Equals(null));
    }

    [Fact]
    public void EqualsObjectNullTest()
    {
        var number = Number.Zero;

        Assert.False(number.Equals((object)null));
    }

    [Fact]
    public void EqualsNumberThisTest()
    {
        var number = Number.Zero;

        Assert.True(number.Equals(number));
    }

    [Fact]
    public void EqualsObjectThisTest()
    {
        var number = Number.Zero;

        Assert.True(number.Equals((object)number));
    }

    [Fact]
    public void EqualsTest()
    {
        var left = Number.Zero;
        var right = Number.Zero;

        Assert.True(left.Equals(right));
    }

    [Fact]
    public void NotEqualsTest()
    {
        var left = Number.Zero;
        var right = Number.One;

        Assert.False(left.Equals(right));
    }

    [Fact]
    public void ExecuteTest()
    {
        var number = Number.One;

        Assert.Equal(new NumberValue(1.0), number.Execute());
    }

    [Fact]
    public void NanTest()
    {
        var number = new Number(double.NaN);

        Assert.True(number.Value.IsNaN);
    }

    [Fact]
    public void PositiveInfinityTest()
    {
        var number = new Number(double.PositiveInfinity);

        Assert.True(number.Value.IsPositiveInfinity);
    }

    [Fact]
    public void NegativeInfinityTest()
    {
        var number = new Number(double.NegativeInfinity);

        Assert.True(number.Value.IsNegativeInfinity);
    }

    [Fact]
    public void InfinityTest()
    {
        var number = new Number(double.NegativeInfinity);

        Assert.True(number.Value.IsInfinity);
    }

    [Fact]
    public void NullAnalyzerTest1()
    {
        var exp = Number.One;

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Fact]
    public void NullAnalyzerTest2()
    {
        var exp = Number.One;

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }
}