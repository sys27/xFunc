// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Programming;

public class GreaterOrEqualTest
{
    [Fact]
    public void CalculateGreaterTrueTest1()
    {
        var parameters = new ParameterCollection { new Parameter("x", 463) };
        var greaterOrEqual = new GreaterOrEqual(Variable.X, new Number(10));

        Assert.True((bool)greaterOrEqual.Execute(parameters));
    }

    [Fact]
    public void CalculateGreaterTrueTest2()
    {
        var parameters = new ParameterCollection { new Parameter("x", 10) };
        var greaterOrEqual = new GreaterOrEqual(Variable.X, new Number(10));

        Assert.True((bool)greaterOrEqual.Execute(parameters));
    }

    [Fact]
    public void CalculateGreaterFalseTest()
    {
        var parameters = new ParameterCollection { new Parameter("x", 0) };
        var greaterOrEqual = new GreaterOrEqual(Variable.X, new Number(10));

        Assert.False((bool)greaterOrEqual.Execute(parameters));
    }

    [Fact]
    public void GreaterOrEqualAngleTest()
    {
        var exp = new GreaterOrEqual(
            AngleValue.Degree(180).AsExpression(),
            AngleValue.Radian(3).AsExpression()
        );
        var result = (bool)exp.Execute();

        Assert.True(result);
    }

    [Fact]
    public void GreaterOrEqualPowerTest()
    {
        var exp = new GreaterOrEqual(
            PowerValue.Kilowatt(12).AsExpression(),
            PowerValue.Watt(10).AsExpression()
        );
        var result = (bool)exp.Execute();

        Assert.True(result);
    }

    [Fact]
    public void GreaterOrEqualTemperatureTest()
    {
        var exp = new GreaterOrEqual(
            TemperatureValue.Celsius(12).AsExpression(),
            TemperatureValue.Fahrenheit(10).AsExpression()
        );
        var result = (bool)exp.Execute();

        Assert.True(result);
    }

    [Fact]
    public void GreaterOrEqualMassTest()
    {
        var exp = new GreaterOrEqual(
            MassValue.Kilogram(12).AsExpression(),
            MassValue.Gram(10).AsExpression()
        );
        var result = (bool)exp.Execute();

        Assert.True(result);
    }

    [Fact]
    public void GreaterOrEqualLengthTest()
    {
        var exp = new GreaterOrEqual(
            LengthValue.Kilometer(12).AsExpression(),
            LengthValue.Meter(10).AsExpression()
        );
        var result = (bool)exp.Execute();

        Assert.True(result);
    }

    [Fact]
    public void GreaterOrEqualTimeTest()
    {
        var exp = new GreaterOrEqual(
            TimeValue.Minute(12).AsExpression(),
            TimeValue.Second(10).AsExpression()
        );
        var result = (bool)exp.Execute();

        Assert.True(result);
    }

    [Fact]
    public void GreaterOrEqualAreaTest()
    {
        var exp = new GreaterOrEqual(
            AreaValue.Meter(12).AsExpression(),
            AreaValue.Centimeter(10).AsExpression()
        );
        var result = (bool)exp.Execute();

        Assert.True(result);
    }

    [Fact]
    public void GreaterOrEqualVolumeTest()
    {
        var exp = new GreaterOrEqual(
            VolumeValue.Meter(12).AsExpression(),
            VolumeValue.Centimeter(10).AsExpression()
        );
        var result = (bool)exp.Execute();

        Assert.True(result);
    }

    [Fact]
    public void CalculateInvalidTypeTest()
    {
        var greaterOrEqual = new GreaterOrEqual(Bool.True, Bool.True);

        Assert.Throws<ResultIsNotSupportedException>(() => greaterOrEqual.Execute());
    }

    [Fact]
    public void CloneTest()
    {
        var exp = new GreaterOrEqual(Number.Two, new Number(3));
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}