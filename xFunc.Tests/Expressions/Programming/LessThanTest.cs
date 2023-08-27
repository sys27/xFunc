// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Programming;

public class LessThanTest
{
    [Test]
    public void CalculateLessTrueTest()
    {
        var parameters = new ExpressionParameters { new Parameter("x", 0) };
        var lessThen = new LessThan(Variable.X, new Number(10));

        Assert.That((bool)lessThen.Execute(parameters), Is.True);
    }

    [Test]
    public void CalculateLessFalseTest()
    {
        var parameters = new ExpressionParameters { new Parameter("x", 10) };
        var lessThen = new LessThan(Variable.X, new Number(10));

        Assert.That((bool)lessThen.Execute(parameters), Is.False);
    }

    [Test]
    public void LessAngleTest()
    {
        var exp = new LessThan(
            AngleValue.Degree(10).AsExpression(),
            AngleValue.Radian(3.14).AsExpression()
        );
        var result = (bool)exp.Execute();

        Assert.That(result, Is.True);
    }

    [Test]
    public void LessPowerTest()
    {
        var exp = new LessThan(
            PowerValue.Watt(10).AsExpression(),
            PowerValue.Kilowatt(12).AsExpression()
        );
        var result = (bool)exp.Execute();

        Assert.That(result, Is.True);
    }

    [Test]
    public void LessTemperatureTest()
    {
        var exp = new LessThan(
            TemperatureValue.Fahrenheit(10).AsExpression(),
            TemperatureValue.Celsius(12).AsExpression()
        );
        var result = (bool)exp.Execute();

        Assert.That(result, Is.True);
    }

    [Test]
    public void LessMassTest()
    {
        var exp = new LessThan(
            MassValue.Gram(10).AsExpression(),
            MassValue.Kilogram(12).AsExpression()
        );
        var result = (bool)exp.Execute();

        Assert.That(result, Is.True);
    }

    [Test]
    public void LessLengthTest()
    {
        var exp = new LessThan(
            LengthValue.Meter(10).AsExpression(),
            LengthValue.Kilometer(12).AsExpression()
        );
        var result = (bool)exp.Execute();

        Assert.That(result, Is.True);
    }

    [Test]
    public void LessTimeTest()
    {
        var exp = new LessThan(
            TimeValue.Second(10).AsExpression(),
            TimeValue.Minute(12).AsExpression()
        );
        var result = (bool)exp.Execute();

        Assert.That(result, Is.True);
    }

    [Test]
    public void LessAreaTest()
    {
        var exp = new LessThan(
            AreaValue.Meter(10).AsExpression(),
            AreaValue.Kilometer(12).AsExpression()
        );
        var result = (bool)exp.Execute();

        Assert.That(result, Is.True);
    }

    [Test]
    public void LessVolumeTest()
    {
        var exp = new LessThan(
            VolumeValue.Centimeter(10).AsExpression(),
            VolumeValue.Meter(12).AsExpression()
        );
        var result = (bool)exp.Execute();

        Assert.That(result, Is.True);
    }

    [Test]
    public void CalculateInvalidTypeTest()
    {
        var lessThen = new LessThan(Bool.True, Bool.True);

        Assert.Throws<ResultIsNotSupportedException>(() => lessThen.Execute());
    }

    [Test]
    public void CloneTest()
    {
        var exp = new LessThan(Number.Two, new Number(3));
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}