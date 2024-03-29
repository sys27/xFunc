// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class FloorTest : BaseExpressionTests
{
    [Test]
    public void ExecuteNumberTest()
    {
        var floor = new Floor(new Number(5.55555555));
        var result = floor.Execute();
        var expected = new NumberValue(5.0);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteAngleTest()
    {
        var floor = new Floor(AngleValue.Degree(5.55555555).AsExpression());
        var result = floor.Execute();
        var expected = AngleValue.Degree(5);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecutePowerTest()
    {
        var floor = new Floor(PowerValue.Watt(5.55555555).AsExpression());
        var result = floor.Execute();
        var expected = PowerValue.Watt(5);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteTemperatureTest()
    {
        var floor = new Floor(TemperatureValue.Celsius(5.55555555).AsExpression());
        var result = floor.Execute();
        var expected = TemperatureValue.Celsius(5);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteMassTest()
    {
        var floor = new Floor(MassValue.Gram(5.55555555).AsExpression());
        var result = floor.Execute();
        var expected = MassValue.Gram(5);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteLengthTest()
    {
        var floor = new Floor(LengthValue.Meter(5.55555555).AsExpression());
        var result = floor.Execute();
        var expected = LengthValue.Meter(5);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteTimeTest()
    {
        var floor = new Floor(TimeValue.Second(5.55555555).AsExpression());
        var result = floor.Execute();
        var expected = TimeValue.Second(5);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteAreaTest()
    {
        var floor = new Floor(AreaValue.Meter(5.55555555).AsExpression());
        var result = floor.Execute();
        var expected = AreaValue.Meter(5);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteVolumeTest()
    {
        var floor = new Floor(VolumeValue.Meter(5.55555555).AsExpression());
        var result = floor.Execute();
        var expected = VolumeValue.Meter(5);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteTestException()
        => TestNotSupported(new Floor(Bool.False));

    [Test]
    public void CloneTest()
    {
        var exp = new Floor(Number.Zero);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}