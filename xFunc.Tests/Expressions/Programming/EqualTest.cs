// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Programming;

public class EqualTest
{
    [Test]
    public void NumberEqualTest()
    {
        var equal = new Equal(new Number(10), new Number(10));
        var result = (bool)equal.Execute();

        Assert.That(result, Is.True);
    }

    [Test]
    public void NumberVarEqualTest()
    {
        var parameters = new ExpressionParameters
        {
            new Parameter("x", 10),
            new Parameter("y", 10)
        };
        var equal = new Equal(Variable.X, Variable.Y);
        var result = (bool)equal.Execute(parameters);

        Assert.That(result, Is.True);
    }

    [Test]
    public void BoolTrueEqualTest()
    {
        var equal = new Equal(Bool.True, Bool.True);
        var result = (bool)equal.Execute();

        Assert.That(result, Is.True);
    }

    [Test]
    public void BoolTrueVarEqualTest()
    {
        var parameters = new ExpressionParameters
        {
            new Parameter("x", true),
            new Parameter("y", true)
        };
        var equal = new Equal(Variable.X, Variable.Y);
        var result = (bool)equal.Execute(parameters);

        Assert.That(result, Is.True);
    }

    [Test]
    public void BoolTrueAndFalseEqualTest()
    {
        var equal = new Equal(Bool.True, Bool.False);
        var result = (bool)equal.Execute();

        Assert.That(result, Is.False);
    }

    [Test]
    public void BoolTrueAndFalseVarEqualTest()
    {
        var parameters = new ExpressionParameters
        {
            new Parameter("x", true),
            new Parameter("y", false)
        };
        var equal = new Equal(Variable.X, Variable.Y);
        var result = (bool)equal.Execute(parameters);

        Assert.That(result, Is.False);
    }

    [Test]
    public void BoolFalseEqualTest()
    {
        var equal = new Equal(Bool.False, Bool.False);
        var result = (bool)equal.Execute();

        Assert.That(result, Is.True);
    }

    [Test]
    public void BoolFalseVarEqualTest()
    {
        var parameters = new ExpressionParameters
        {
            new Parameter("x", false),
            new Parameter("y", false)
        };
        var equal = new Equal(Variable.X, Variable.Y);
        var result = (bool)equal.Execute(parameters);

        Assert.That(result, Is.True);
    }

    [Test]
    public void AngleEqualTest()
    {
        var equal = new Equal(
            AngleValue.Degree(180).AsExpression(),
            AngleValue.Radian(Math.PI).AsExpression()
        );
        var result = (bool)equal.Execute();

        Assert.That(result, Is.True);
    }

    [Test]
    public void PowerEqualTest()
    {
        var equal = new Equal(
            PowerValue.Watt(1000).AsExpression(),
            PowerValue.Kilowatt(1).AsExpression()
        );
        var result = (bool)equal.Execute();

        Assert.That(result, Is.True);
    }

    [Test]
    public void TemperatureEqualTest()
    {
        var equal = new Equal(
            TemperatureValue.Celsius(10).AsExpression(),
            TemperatureValue.Fahrenheit(50).AsExpression()
        );
        var result = (bool)equal.Execute();

        Assert.That(result, Is.True);
    }

    [Test]
    public void MassEqualTest()
    {
        var equal = new Equal(
            MassValue.Gram(1000).AsExpression(),
            MassValue.Kilogram(1).AsExpression()
        );
        var result = (bool)equal.Execute();

        Assert.That(result, Is.True);
    }

    [Test]
    public void LengthEqualTest()
    {
        var equal = new Equal(
            LengthValue.Centimeter(100).AsExpression(),
            LengthValue.Meter(1).AsExpression()
        );
        var result = (bool)equal.Execute();

        Assert.That(result, Is.True);
    }

    [Test]
    public void TimeEqualTest()
    {
        var equal = new Equal(
            TimeValue.Second(60).AsExpression(),
            TimeValue.Minute(1).AsExpression()
        );
        var result = (bool)equal.Execute();

        Assert.That(result, Is.True);
    }

    [Test]
    public void AreaEqualTest()
    {
        var equal = new Equal(
            AreaValue.Meter(1000000).AsExpression(),
            AreaValue.Kilometer(1).AsExpression()
        );
        var result = (bool)equal.Execute();

        Assert.That(result, Is.True);
    }

    [Test]
    public void VolumeEqualTest()
    {
        var equal = new Equal(
            VolumeValue.Meter(0.001).AsExpression(),
            VolumeValue.Liter(1).AsExpression()
        );
        var result = (bool)equal.Execute();

        Assert.That(result, Is.True);
    }

    [Test]
    public void CalculateInvalidParametersTest()
    {
        var equal = new Equal(new ComplexNumber(3, 2), new ComplexNumber(3, 2));

        Assert.Throws<ExecutionException>(() => equal.Execute());
    }

    [Test]
    public void CloneTest()
    {
        var exp = new Equal(Number.Two, new Number(3));
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}