// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class ToNumberTest
{
    [Test]
    public void ExecuteAngleTest()
    {
        var exp = new ToNumber(AngleValue.Degree(10).AsExpression());
        var actual = exp.Execute();
        var expected = new NumberValue(10.0);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ExecutePowerTest()
    {
        var exp = new ToNumber(PowerValue.Watt(10).AsExpression());
        var actual = exp.Execute();
        var expected = new NumberValue(10.0);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteTemperatureTest()
    {
        var exp = new ToNumber(TemperatureValue.Celsius(10).AsExpression());
        var actual = exp.Execute();
        var expected = new NumberValue(10.0);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteMassTest()
    {
        var exp = new ToNumber(MassValue.Gram(10).AsExpression());
        var actual = exp.Execute();
        var expected = new NumberValue(10.0);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteLengthTest()
    {
        var exp = new ToNumber(LengthValue.Meter(10).AsExpression());
        var actual = exp.Execute();
        var expected = new NumberValue(10.0);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteTimeTest()
    {
        var exp = new ToNumber(TimeValue.Second(10).AsExpression());
        var actual = exp.Execute();
        var expected = new NumberValue(10.0);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteAreaTest()
    {
        var exp = new ToNumber(AreaValue.Meter(10).AsExpression());
        var actual = exp.Execute();
        var expected = new NumberValue(10.0);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteVolumeTest()
    {
        var exp = new ToNumber(VolumeValue.Meter(10).AsExpression());
        var actual = exp.Execute();
        var expected = new NumberValue(10.0);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteRationalTest()
    {
        var exp = new ToNumber(new Rational(new Number(1), new Number(2)));
        var actual = exp.Execute();

        Assert.That(actual, Is.EqualTo(NumberValue.Half));
    }

    [Test]
    public void ExecuteBoolTest()
    {
        Assert.Throws<ResultIsNotSupportedException>(() => new ToNumber(Bool.False).Execute());
    }

    [Test]
    public void NullAnalyzerTest1()
    {
        var exp = new ToNumber(new Number(10));

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Test]
    public void NullAnalyzerTest2()
    {
        var exp = new ToNumber(new Number(10));

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }

    [Test]
    public void CloneTest()
    {
        var exp = new ToNumber(AngleValue.Degree(10).AsExpression());
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}