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
    public void ExecuteTemperatureTest()
    {
        var exp = new Frac(TemperatureValue.Celsius(5.5).AsExpression());
        var result = exp.Execute();
        var expected = TemperatureValue.Celsius(0.5);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteNegativeTemperatureTest()
    {
        var exp = new Frac(TemperatureValue.Celsius(-5.5).AsExpression());
        var result = exp.Execute();
        var expected = TemperatureValue.Celsius(-0.5);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteMassTest()
    {
        var exp = new Frac(MassValue.Gram(5.5).AsExpression());
        var result = exp.Execute();
        var expected = MassValue.Gram(0.5);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteNegativeMassTest()
    {
        var exp = new Frac(MassValue.Gram(-5.5).AsExpression());
        var result = exp.Execute();
        var expected = MassValue.Gram(-0.5);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteLengthTest()
    {
        var exp = new Frac(LengthValue.Meter(5.5).AsExpression());
        var result = exp.Execute();
        var expected = LengthValue.Meter(0.5);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteNegativeLengthTest()
    {
        var exp = new Frac(LengthValue.Meter(-5.5).AsExpression());
        var result = exp.Execute();
        var expected = LengthValue.Meter(-0.5);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteTimeTest()
    {
        var exp = new Frac(TimeValue.Second(5.5).AsExpression());
        var result = exp.Execute();
        var expected = TimeValue.Second(0.5);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteNegativeTimeTest()
    {
        var exp = new Frac(TimeValue.Second(-5.5).AsExpression());
        var result = exp.Execute();
        var expected = TimeValue.Second(-0.5);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteAreaTest()
    {
        var exp = new Frac(AreaValue.Meter(5.5).AsExpression());
        var result = exp.Execute();
        var expected = AreaValue.Meter(0.5);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteNegativeAreaTest()
    {
        var exp = new Frac(AreaValue.Meter(-5.5).AsExpression());
        var result = exp.Execute();
        var expected = AreaValue.Meter(-0.5);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteVolumeTest()
    {
        var exp = new Frac(VolumeValue.Meter(5.5).AsExpression());
        var result = exp.Execute();
        var expected = VolumeValue.Meter(0.5);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteNegativeVolumeTest()
    {
        var exp = new Frac(VolumeValue.Meter(-5.5).AsExpression());
        var result = exp.Execute();
        var expected = VolumeValue.Meter(-0.5);

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