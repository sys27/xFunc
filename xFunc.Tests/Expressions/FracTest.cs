// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class FracTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteNumberTest()
    {
        var exp = new Frac(new Number(5.5));
        var result = exp.Execute();
        var expected = new NumberValue(0.5);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteNegativeNumberTest()
    {
        var exp = new Frac(new Number(-5.5));
        var result = exp.Execute();
        var expected = new NumberValue(-0.5);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteAngleTest()
    {
        var exp = new Frac(AngleValue.Degree(5.5).AsExpression());
        var result = exp.Execute();
        var expected = AngleValue.Degree(0.5);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteNegativeAngleTest()
    {
        var exp = new Frac(AngleValue.Degree(-5.5).AsExpression());
        var result = exp.Execute();
        var expected = AngleValue.Degree(-0.5);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecutePowerTest()
    {
        var exp = new Frac(PowerValue.Watt(5.5).AsExpression());
        var result = exp.Execute();
        var expected = PowerValue.Watt(0.5);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteNegativePowerTest()
    {
        var exp = new Frac(PowerValue.Watt(-5.5).AsExpression());
        var result = exp.Execute();
        var expected = PowerValue.Watt(-0.5);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteTestException()
        => TestNotSupported(new Frac(Bool.False));

    [Fact]
    public void CloneTest()
    {
        var exp = new Frac(Number.Zero);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}