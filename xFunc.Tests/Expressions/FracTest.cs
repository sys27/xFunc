// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class FracTest : BaseExpressionTests
{
    [Test]
    public void ExecuteNumberTest()
    {
        var exp = new Frac(new Number(5.5));
        var result = exp.Execute();
        var expected = new NumberValue(0.5);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteNegativeNumberTest()
    {
        var exp = new Frac(new Number(-5.5));
        var result = exp.Execute();
        var expected = new NumberValue(-0.5);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteAngleTest()
    {
        var exp = new Frac(AngleValue.Degree(5.5).AsExpression());
        var result = exp.Execute();
        var expected = AngleValue.Degree(0.5);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteNegativeAngleTest()
    {
        var exp = new Frac(AngleValue.Degree(-5.5).AsExpression());
        var result = exp.Execute();
        var expected = AngleValue.Degree(-0.5);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecutePowerTest()
    {
        var exp = new Frac(PowerValue.Watt(5.5).AsExpression());
        var result = exp.Execute();
        var expected = PowerValue.Watt(0.5);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteNegativePowerTest()
    {
        var exp = new Frac(PowerValue.Watt(-5.5).AsExpression());
        var result = exp.Execute();
        var expected = PowerValue.Watt(-0.5);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteTemperatureTest()
    {
        var exp = new Frac(TemperatureValue.Celsius(5.5).AsExpression());
        var result = exp.Execute();
        var expected = TemperatureValue.Celsius(0.5);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteNegativeTemperatureTest()
    {
        var exp = new Frac(TemperatureValue.Celsius(-5.5).AsExpression());
        var result = exp.Execute();
        var expected = TemperatureValue.Celsius(-0.5);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteMassTest()
    {
        var exp = new Frac(MassValue.Gram(5.5).AsExpression());
        var result = exp.Execute();
        var expected = MassValue.Gram(0.5);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteNegativeMassTest()
    {
        var exp = new Frac(MassValue.Gram(-5.5).AsExpression());
        var result = exp.Execute();
        var expected = MassValue.Gram(-0.5);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteLengthTest()
    {
        var exp = new Frac(LengthValue.Meter(5.5).AsExpression());
        var result = exp.Execute();
        var expected = LengthValue.Meter(0.5);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteNegativeLengthTest()
    {
        var exp = new Frac(LengthValue.Meter(-5.5).AsExpression());
        var result = exp.Execute();
        var expected = LengthValue.Meter(-0.5);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteTimeTest()
    {
        var exp = new Frac(TimeValue.Second(5.5).AsExpression());
        var result = exp.Execute();
        var expected = TimeValue.Second(0.5);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteNegativeTimeTest()
    {
        var exp = new Frac(TimeValue.Second(-5.5).AsExpression());
        var result = exp.Execute();
        var expected = TimeValue.Second(-0.5);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteAreaTest()
    {
        var exp = new Frac(AreaValue.Meter(5.5).AsExpression());
        var result = exp.Execute();
        var expected = AreaValue.Meter(0.5);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteNegativeAreaTest()
    {
        var exp = new Frac(AreaValue.Meter(-5.5).AsExpression());
        var result = exp.Execute();
        var expected = AreaValue.Meter(-0.5);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteVolumeTest()
    {
        var exp = new Frac(VolumeValue.Meter(5.5).AsExpression());
        var result = exp.Execute();
        var expected = VolumeValue.Meter(0.5);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteNegativeVolumeTest()
    {
        var exp = new Frac(VolumeValue.Meter(-5.5).AsExpression());
        var result = exp.Execute();
        var expected = VolumeValue.Meter(-0.5);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteTestException()
        => TestNotSupported(new Frac(Bool.False));

    [Test]
    public void CloneTest()
    {
        var exp = new Frac(Number.Zero);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}