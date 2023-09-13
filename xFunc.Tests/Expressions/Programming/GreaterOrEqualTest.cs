// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Programming;

public class GreaterOrEqualTest
{
    [Test]
    public void CalculateGreaterTrueTest1()
    {
        var parameters = new ExpressionParameters { new Parameter("x", 463) };
        var greaterOrEqual = new GreaterOrEqual(Variable.X, new Number(10));

        Assert.That((bool)greaterOrEqual.Execute(parameters), Is.True);
    }

    [Test]
    public void CalculateGreaterTrueTest2()
    {
        var parameters = new ExpressionParameters { new Parameter("x", 10) };
        var greaterOrEqual = new GreaterOrEqual(Variable.X, new Number(10));

        Assert.That((bool)greaterOrEqual.Execute(parameters), Is.True);
    }

    [Test]
    public void CalculateGreaterFalseTest()
    {
        var parameters = new ExpressionParameters { new Parameter("x", 0) };
        var greaterOrEqual = new GreaterOrEqual(Variable.X, new Number(10));

        Assert.That((bool)greaterOrEqual.Execute(parameters), Is.False);
    }

    [Test]
    public void GreaterOrEqualAngleTest()
    {
        var exp = new GreaterOrEqual(
            AngleValue.Degree(180).AsExpression(),
            AngleValue.Radian(3).AsExpression()
        );
        var result = (bool)exp.Execute();

        Assert.That(result, Is.True);
    }

    [Test]
    public void GreaterOrEqualPowerTest()
    {
        var exp = new GreaterOrEqual(
            PowerValue.Kilowatt(12).AsExpression(),
            PowerValue.Watt(10).AsExpression()
        );
        var result = (bool)exp.Execute();

        Assert.That(result, Is.True);
    }

    [Test]
    public void GreaterOrEqualTemperatureTest()
    {
        var exp = new GreaterOrEqual(
            TemperatureValue.Celsius(12).AsExpression(),
            TemperatureValue.Fahrenheit(10).AsExpression()
        );
        var result = (bool)exp.Execute();

        Assert.That(result, Is.True);
    }

    [Test]
    public void GreaterOrEqualMassTest()
    {
        var exp = new GreaterOrEqual(
            MassValue.Kilogram(12).AsExpression(),
            MassValue.Gram(10).AsExpression()
        );
        var result = (bool)exp.Execute();

        Assert.That(result, Is.True);
    }

    [Test]
    public void GreaterOrEqualLengthTest()
    {
        var exp = new GreaterOrEqual(
            LengthValue.Kilometer(12).AsExpression(),
            LengthValue.Meter(10).AsExpression()
        );
        var result = (bool)exp.Execute();

        Assert.That(result, Is.True);
    }

    [Test]
    public void GreaterOrEqualTimeTest()
    {
        var exp = new GreaterOrEqual(
            TimeValue.Minute(12).AsExpression(),
            TimeValue.Second(10).AsExpression()
        );
        var result = (bool)exp.Execute();

        Assert.That(result, Is.True);
    }

    [Test]
    public void GreaterOrEqualAreaTest()
    {
        var exp = new GreaterOrEqual(
            AreaValue.Meter(12).AsExpression(),
            AreaValue.Centimeter(10).AsExpression()
        );
        var result = (bool)exp.Execute();

        Assert.That(result, Is.True);
    }

    [Test]
    public void GreaterOrEqualVolumeTest()
    {
        var exp = new GreaterOrEqual(
            VolumeValue.Meter(12).AsExpression(),
            VolumeValue.Centimeter(10).AsExpression()
        );
        var result = (bool)exp.Execute();

        Assert.That(result, Is.True);
    }

    [Test]
    public void CalculateInvalidTypeTest()
    {
        var greaterOrEqual = new GreaterOrEqual(Bool.True, Bool.True);

        Assert.Throws<ExecutionException>(() => greaterOrEqual.Execute());
    }

    [Test]
    public void CloneTest()
    {
        var exp = new GreaterOrEqual(Number.Two, new Number(3));
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}