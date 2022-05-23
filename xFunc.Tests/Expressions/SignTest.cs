// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class SignTest : BaseExpressionTests
{
    [Fact]
    public void PositiveSignTest()
    {
        var exp = new Sign(new Number(5));
        var result = exp.Execute();

        Assert.Equal(new NumberValue(1.0), result);
    }

    [Fact]
    public void NegativeSignTest()
    {
        var exp = new Sign(new Number(-5));
        var result = exp.Execute();

        Assert.Equal(new NumberValue(-1.0), result);
    }

    [Fact]
    public void AngleSignTest()
    {
        var exp = new Sign(AngleValue.Degree(10).AsExpression());
        var result = exp.Execute();

        Assert.Equal(new NumberValue(1.0), result);
    }

    [Fact]
    public void PowerSignTest()
    {
        var exp = new Sign(PowerValue.Watt(10).AsExpression());
        var result = exp.Execute();

        Assert.Equal(new NumberValue(1.0), result);
    }

    [Fact]
    public void TemperatureSignTest()
    {
        var exp = new Sign(TemperatureValue.Celsius(10).AsExpression());
        var result = exp.Execute();

        Assert.Equal(new NumberValue(1.0), result);
    }

    [Fact]
    public void MassSignTest()
    {
        var exp = new Sign(MassValue.Gram(10).AsExpression());
        var result = exp.Execute();

        Assert.Equal(new NumberValue(1.0), result);
    }

    [Fact]
    public void LengthSignTest()
    {
        var exp = new Sign(LengthValue.Meter(10).AsExpression());
        var result = exp.Execute();

        Assert.Equal(new NumberValue(1.0), result);
    }

    [Fact]
    public void TimeSignTest()
    {
        var exp = new Sign(TimeValue.Second(10).AsExpression());
        var result = exp.Execute();

        Assert.Equal(new NumberValue(1.0), result);
    }

    [Fact]
    public void AreaSignTest()
    {
        var exp = new Sign(AreaValue.Meter(10).AsExpression());
        var result = exp.Execute();

        Assert.Equal(new NumberValue(1.0), result);
    }

    [Fact]
    public void VolumeSignTest()
    {
        var exp = new Sign(VolumeValue.Meter(10).AsExpression());
        var result = exp.Execute();

        Assert.Equal(new NumberValue(1.0), result);
    }

    [Fact]
    public void InvalidParameterTest()
        => TestNotSupported(new Sign(Bool.False));

    [Fact]
    public void CloneTest()
    {
        var exp = new Sign(new Number(-5));
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}