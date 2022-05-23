// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class CeilTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteTestNumber()
    {
        var ceil = new Ceil(new Number(5.55555555));
        var result = (NumberValue)ceil.Execute();
        var expected = new NumberValue(6.0);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteTestAngleNumber()
    {
        var ceil = new Ceil(AngleValue.Degree(5.55555555).AsExpression());
        var result = ceil.Execute();
        var expected = AngleValue.Degree(6);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteTestPowerNumber()
    {
        var ceil = new Ceil(PowerValue.Watt(5.55555555).AsExpression());
        var result = ceil.Execute();
        var expected = PowerValue.Watt(6);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteTestTemperatureNumber()
    {
        var ceil = new Ceil(TemperatureValue.Celsius(5.55555555).AsExpression());
        var result = ceil.Execute();
        var expected = TemperatureValue.Celsius(6);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteTestMassNumber()
    {
        var ceil = new Ceil(MassValue.Gram(5.55555555).AsExpression());
        var result = ceil.Execute();
        var expected = MassValue.Gram(6);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteTestLengthNumber()
    {
        var ceil = new Ceil(LengthValue.Meter(5.55555555).AsExpression());
        var result = ceil.Execute();
        var expected = LengthValue.Meter(6);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteTestTimeNumber()
    {
        var ceil = new Ceil(TimeValue.Second(5.55555555).AsExpression());
        var result = ceil.Execute();
        var expected = TimeValue.Second(6);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteTestAreaNumber()
    {
        var ceil = new Ceil(AreaValue.Meter(5.55555555).AsExpression());
        var result = ceil.Execute();
        var expected = AreaValue.Meter(6);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteTestVolumeNumber()
    {
        var ceil = new Ceil(VolumeValue.Meter(5.55555555).AsExpression());
        var result = ceil.Execute();
        var expected = VolumeValue.Meter(6);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteTestException()
        => TestNotSupported(new Ceil(Bool.False));

    [Fact]
    public void CloneTest()
    {
        var exp = new Ceil(Number.Zero);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}