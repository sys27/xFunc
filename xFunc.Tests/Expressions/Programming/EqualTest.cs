// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Programming;

public class EqualTest
{
    [Fact]
    public void NumberEqualTest()
    {
        var equal = new Equal(new Number(10), new Number(10));
        var result = (bool)equal.Execute();

        Assert.True(result);
    }

    [Fact]
    public void NumberVarEqualTest()
    {
        var parameters = new ParameterCollection
        {
            new Parameter("x", 10),
            new Parameter("y", 10)
        };
        var equal = new Equal(Variable.X, Variable.Y);
        var result = (bool)equal.Execute(parameters);

        Assert.True(result);
    }

    [Fact]
    public void BoolTrueEqualTest()
    {
        var equal = new Equal(Bool.True, Bool.True);
        var result = (bool)equal.Execute();

        Assert.True(result);
    }

    [Fact]
    public void BoolTrueVarEqualTest()
    {
        var parameters = new ParameterCollection
        {
            new Parameter("x", true),
            new Parameter("y", true)
        };
        var equal = new Equal(Variable.X, Variable.Y);
        var result = (bool)equal.Execute(parameters);

        Assert.True(result);
    }

    [Fact]
    public void BoolTrueAndFalseEqualTest()
    {
        var equal = new Equal(Bool.True, Bool.False);
        var result = (bool)equal.Execute();

        Assert.False(result);
    }

    [Fact]
    public void BoolTrueAndFalseVarEqualTest()
    {
        var parameters = new ParameterCollection
        {
            new Parameter("x", true),
            new Parameter("y", false)
        };
        var equal = new Equal(Variable.X, Variable.Y);
        var result = (bool)equal.Execute(parameters);

        Assert.False(result);
    }

    [Fact]
    public void BoolFalseEqualTest()
    {
        var equal = new Equal(Bool.False, Bool.False);
        var result = (bool)equal.Execute();

        Assert.True(result);
    }

    [Fact]
    public void BoolFalseVarEqualTest()
    {
        var parameters = new ParameterCollection
        {
            new Parameter("x", false),
            new Parameter("y", false)
        };
        var equal = new Equal(Variable.X, Variable.Y);
        var result = (bool)equal.Execute(parameters);

        Assert.True(result);
    }

    [Fact]
    public void AngleEqualTest()
    {
        var equal = new Equal(
            AngleValue.Degree(10).AsExpression(),
            AngleValue.Degree(10).AsExpression()
        );
        var result = (bool)equal.Execute();

        Assert.True(result);
    }

    [Fact]
    public void PowerEqualTest()
    {
        var equal = new Equal(
            PowerValue.Watt(10).AsExpression(),
            PowerValue.Watt(10).AsExpression()
        );
        var result = (bool)equal.Execute();

        Assert.True(result);
    }

    [Fact]
    public void TemperatureEqualTest()
    {
        var equal = new Equal(
            TemperatureValue.Celsius(10).AsExpression(),
            TemperatureValue.Celsius(10).AsExpression()
        );
        var result = (bool)equal.Execute();

        Assert.True(result);
    }

    [Fact]
    public void CalculateInvalidParametersTest()
    {
        var equal = new Equal(new ComplexNumber(3, 2), new ComplexNumber(3, 2));

        Assert.Throws<ResultIsNotSupportedException>(() => equal.Execute());
    }

    [Fact]
    public void CloneTest()
    {
        var exp = new Equal(Number.Two, new Number(3));
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}