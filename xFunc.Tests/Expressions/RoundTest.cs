// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class RoundTest : BaseExpressionTests
{
    [Test]
    public void CalculateRoundWithoutDigits()
    {
        var round = new Round(new Number(5.555555));
        var result = round.Execute();
        var expected = new NumberValue(6.0);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void CalculateRoundWithDigits()
    {
        var round = new Round(new Number(5.555555), Number.Two);
        var result = round.Execute();
        var expected = new NumberValue(5.56);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void RoundAngleWithDigits()
    {
        var round = new Round(AngleValue.Degree(5.555555).AsExpression(), Number.Two);
        var result = round.Execute();
        var expected = AngleValue.Degree(5.56);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void RoundPowerWithDigits()
    {
        var round = new Round(PowerValue.Watt(5.555555).AsExpression(), Number.Two);
        var result = round.Execute();
        var expected = PowerValue.Watt(5.56);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void RoundTemperatureWithDigits()
    {
        var round = new Round(TemperatureValue.Celsius(5.555555).AsExpression(), Number.Two);
        var result = round.Execute();
        var expected = TemperatureValue.Celsius(5.56);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void RoundMassWithDigits()
    {
        var round = new Round(MassValue.Gram(5.555555).AsExpression(), Number.Two);
        var result = round.Execute();
        var expected = MassValue.Gram(5.56);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void RoundLengthWithDigits()
    {
        var round = new Round(LengthValue.Meter(5.555555).AsExpression(), Number.Two);
        var result = round.Execute();
        var expected = LengthValue.Meter(5.56);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void RoundTimeWithDigits()
    {
        var round = new Round(TimeValue.Second(5.555555).AsExpression(), Number.Two);
        var result = round.Execute();
        var expected = TimeValue.Second(5.56);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void RoundAreaWithDigits()
    {
        var round = new Round(AreaValue.Meter(5.555555).AsExpression(), Number.Two);
        var result = round.Execute();
        var expected = AreaValue.Meter(5.56);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void RoundVolumeWithDigits()
    {
        var round = new Round(VolumeValue.Meter(5.555555).AsExpression(), Number.Two);
        var result = round.Execute();
        var expected = VolumeValue.Meter(5.56);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteArgumentIsNotNumber()
    {
        var exp = new Round(Bool.False, Number.Two);

        TestNotSupported(exp);
    }

    [Test]
    public void ExecuteDigitsIsNotNumber()
        => TestNotSupported(new Round(new Number(5.5), Bool.False));

    [Test]
    public void ExecuteArgumentIsNotInteger()
    {
        var exp = new Round(new Number(5.5), new Number(2.5));

        Assert.Throws<InvalidOperationException>(() => exp.Execute());
    }

    [Test]
    public void CloneTest()
    {
        var exp = new Round(new Number(5.555555), Number.Two);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}