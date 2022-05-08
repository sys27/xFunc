// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Programming;

public class GreaterTest
{
    [Fact]
    public void CalculateGreaterTrueTest()
    {
        var parameters = new ParameterCollection { new Parameter("x", 463) };
        var greaterThen = new GreaterThan(Variable.X, new Number(10));

        Assert.True((bool)greaterThen.Execute(parameters));
    }

    [Fact]
    public void CalculateGreaterFalseTest()
    {
        var parameters = new ParameterCollection { new Parameter("x", 0) };
        var greaterThan = new GreaterThan(Variable.X, new Number(10));

        Assert.False((bool)greaterThan.Execute(parameters));
    }

    [Fact]
    public void GreaterAngleTest()
    {
        var exp = new GreaterThan(
            AngleValue.Degree(180).AsExpression(),
            AngleValue.Radian(3).AsExpression()
        );
        var result = (bool)exp.Execute();

        Assert.True(result);
    }

    [Fact]
    public void GreaterPowerTest()
    {
        var exp = new GreaterThan(
            PowerValue.Kilowatt(12).AsExpression(),
            PowerValue.Watt(10).AsExpression()
        );
        var result = (bool)exp.Execute();

        Assert.True(result);
    }

    [Fact]
    public void GreaterTemperatureTest()
    {
        var exp = new GreaterThan(
            TemperatureValue.Celsius(12).AsExpression(),
            TemperatureValue.Fahrenheit(10).AsExpression()
        );
        var result = (bool)exp.Execute();

        Assert.True(result);
    }

    [Fact]
    public void GreaterMassTest()
    {
        var exp = new GreaterThan(
            MassValue.Kilogram(12).AsExpression(),
            MassValue.Gram(10).AsExpression()
        );
        var result = (bool)exp.Execute();

        Assert.True(result);
    }

    [Fact]
    public void GreaterLengthTest()
    {
        var exp = new GreaterThan(
            LengthValue.Kilometer(12).AsExpression(),
            LengthValue.Meter(10).AsExpression()
        );
        var result = (bool)exp.Execute();

        Assert.True(result);
    }

    [Fact]
    public void GreaterTimeTest()
    {
        var exp = new GreaterThan(
            TimeValue.Minute(12).AsExpression(),
            TimeValue.Second(10).AsExpression()
        );
        var result = (bool)exp.Execute();

        Assert.True(result);
    }

    [Fact]
    public void GreaterAreaTest()
    {
        var exp = new GreaterThan(
            AreaValue.Meter(12).AsExpression(),
            AreaValue.Centimeter(10).AsExpression()
        );
        var result = (bool)exp.Execute();

        Assert.True(result);
    }

    [Fact]
    public void GreaterVolumeTest()
    {
        var exp = new GreaterThan(
            VolumeValue.Meter(12).AsExpression(),
            VolumeValue.Centimeter(10).AsExpression()
        );
        var result = (bool)exp.Execute();

        Assert.True(result);
    }

    [Fact]
    public void CalculateInvalidTypeTest()
    {
        var greaterThan = new GreaterThan(Bool.True, Bool.True);

        Assert.Throws<ResultIsNotSupportedException>(() => greaterThan.Execute());
    }

    [Fact]
    public void CloneTest()
    {
        var exp = new GreaterThan(Number.Two, new Number(3));
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}