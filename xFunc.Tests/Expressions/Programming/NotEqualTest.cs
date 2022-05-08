// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Programming;

public class NotEqualTest
{
    [Fact]
    public void NumberEqualTest()
    {
        var equal = new NotEqual(new Number(11), new Number(10));
        var result = (bool)equal.Execute();

        Assert.True(result);
    }

    [Fact]
    public void NumberVarEqualTest()
    {
        var parameters = new ParameterCollection
        {
            new Parameter("x", 11),
            new Parameter("y", 10)
        };
        var equal = new NotEqual(Variable.X, Variable.Y);
        var result = (bool)equal.Execute(parameters);

        Assert.True(result);
    }

    [Fact]
    public void NumberAndBoolVarEqualTest()
    {
        var parameters = new ParameterCollection
        {
            new Parameter("x", 10),
            new Parameter("y", false)
        };
        var equal = new NotEqual(Variable.X, Variable.Y);

        Assert.Throws<ResultIsNotSupportedException>(() => equal.Execute(parameters));
    }

    [Fact]
    public void BoolTrueEqualTest()
    {
        var equal = new NotEqual(Bool.True, Bool.True);
        var result = (bool)equal.Execute();

        Assert.False(result);
    }

    [Fact]
    public void BoolTrueVarEqualTest()
    {
        var parameters = new ParameterCollection
        {
            new Parameter("x", true),
            new Parameter("y", true)
        };
        var equal = new NotEqual(Variable.X, Variable.Y);
        var result = (bool)equal.Execute(parameters);

        Assert.False(result);
    }

    [Fact]
    public void BoolTrueAndFalseEqualTest()
    {
        var equal = new NotEqual(Bool.True, Bool.False);
        var result = (bool)equal.Execute();

        Assert.True(result);
    }

    [Fact]
    public void BoolTrueAndFalseVarEqualTest()
    {
        var parameters = new ParameterCollection
        {
            new Parameter("x", true),
            new Parameter("y", false)
        };
        var equal = new NotEqual(Variable.X, Variable.Y);
        var result = (bool)equal.Execute(parameters);

        Assert.True(result);
    }

    [Fact]
    public void BoolFalseEqualTest()
    {
        var equal = new NotEqual(Bool.False, Bool.False);
        var result = (bool)equal.Execute();

        Assert.False(result);
    }

    [Fact]
    public void BoolFalseVarEqualTest()
    {
        var parameters = new ParameterCollection
        {
            new Parameter("x", false),
            new Parameter("y", false)
        };
        var equal = new NotEqual(Variable.X, Variable.Y);
        var result = (bool)equal.Execute(parameters);

        Assert.False(result);
    }

    [Fact]
    public void AngleNotEqualTest()
    {
        var equal = new NotEqual(
            AngleValue.Degree(10).AsExpression(),
            AngleValue.Radian(12).AsExpression()
        );
        var result = (bool)equal.Execute();

        Assert.True(result);
    }

    [Fact]
    public void PowerNotEqualTest()
    {
        var equal = new NotEqual(
            PowerValue.Watt(10).AsExpression(),
            PowerValue.Kilowatt(12).AsExpression()
        );
        var result = (bool)equal.Execute();

        Assert.True(result);
    }

    [Fact]
    public void TemperatureNotEqualTest()
    {
        var equal = new NotEqual(
            TemperatureValue.Celsius(10).AsExpression(),
            TemperatureValue.Fahrenheit(12).AsExpression()
        );
        var result = (bool)equal.Execute();

        Assert.True(result);
    }

    [Fact]
    public void MassNotEqualTest()
    {
        var equal = new NotEqual(
            MassValue.Gram(10).AsExpression(),
            MassValue.Kilogram(12).AsExpression()
        );
        var result = (bool)equal.Execute();

        Assert.True(result);
    }

    [Fact]
    public void LengthNotEqualTest()
    {
        var equal = new NotEqual(
            LengthValue.Centimeter(10).AsExpression(),
            LengthValue.Meter(12).AsExpression()
        );
        var result = (bool)equal.Execute();

        Assert.True(result);
    }

    [Fact]
    public void TimeNotEqualTest()
    {
        var equal = new NotEqual(
            TimeValue.Second(10).AsExpression(),
            TimeValue.Minute(12).AsExpression()
        );
        var result = (bool)equal.Execute();

        Assert.True(result);
    }

    [Fact]
    public void AreaNotEqualTest()
    {
        var equal = new NotEqual(
            AreaValue.Centimeter(10).AsExpression(),
            AreaValue.Meter(12).AsExpression()
        );
        var result = (bool)equal.Execute();

        Assert.True(result);
    }

    [Fact]
    public void VolumeNotEqualTest()
    {
        var equal = new NotEqual(
            VolumeValue.Centimeter(10).AsExpression(),
            VolumeValue.Meter(12).AsExpression()
        );
        var result = (bool)equal.Execute();

        Assert.True(result);
    }

    [Fact]
    public void CloneTest()
    {
        var exp = new NotEqual(Number.Two, Number.Two);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}