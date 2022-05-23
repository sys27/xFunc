// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class TruncTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteNumberTest()
    {
        var exp = new Trunc(new Number(5.55555555));
        var result = exp.Execute();
        var expected = new NumberValue(5.0);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteAngleTest()
    {
        var exp = new Trunc(AngleValue.Degree(5.55555555).AsExpression());
        var result = exp.Execute();
        var expected = AngleValue.Degree(5);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecutePowerTest()
    {
        var exp = new Trunc(PowerValue.Watt(5.55555555).AsExpression());
        var result = exp.Execute();
        var expected = PowerValue.Watt(5);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteTemperatureTest()
    {
        var exp = new Trunc(TemperatureValue.Celsius(5.55555555).AsExpression());
        var result = exp.Execute();
        var expected = TemperatureValue.Celsius(5);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteMassTest()
    {
        var exp = new Trunc(MassValue.Gram(5.55555555).AsExpression());
        var result = exp.Execute();
        var expected = MassValue.Gram(5);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteLengthTest()
    {
        var exp = new Trunc(LengthValue.Meter(5.55555555).AsExpression());
        var result = exp.Execute();
        var expected = LengthValue.Meter(5);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteTimeTest()
    {
        var exp = new Trunc(TimeValue.Second(5.55555555).AsExpression());
        var result = exp.Execute();
        var expected = TimeValue.Second(5);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteAreaTest()
    {
        var exp = new Trunc(AreaValue.Meter(5.55555555).AsExpression());
        var result = exp.Execute();
        var expected = AreaValue.Meter(5);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteVolumeTest()
    {
        var exp = new Trunc(VolumeValue.Meter(5.55555555).AsExpression());
        var result = exp.Execute();
        var expected = VolumeValue.Meter(5);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteTestException()
        => TestNotSupported(new Trunc(Bool.False));

    [Fact]
    public void CloneTest()
    {
        var exp = new Trunc(Number.Zero);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}