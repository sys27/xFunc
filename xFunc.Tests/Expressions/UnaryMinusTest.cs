// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions;

public class UnaryMinusTest : BaseExpressionTests
{
    [Test]
    public void ExecuteNumberTest()
    {
        var exp = new UnaryMinus(new Number(10));

        Assert.That(exp.Execute(), Is.EqualTo(new NumberValue(-10.0)));
    }

    [Test]
    public void ExecuteAngleNumberTest()
    {
        var exp = new UnaryMinus(AngleValue.Degree(10).AsExpression());
        var expected = AngleValue.Degree(-10);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecutePowerNumberTest()
    {
        var exp = new UnaryMinus(PowerValue.Watt(10).AsExpression());
        var expected = PowerValue.Watt(-10);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteTemperatureNumberTest()
    {
        var exp = new UnaryMinus(TemperatureValue.Celsius(10).AsExpression());
        var expected = TemperatureValue.Celsius(-10);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteMassNumberTest()
    {
        var exp = new UnaryMinus(MassValue.Gram(10).AsExpression());
        var expected = MassValue.Gram(-10);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteLengthNumberTest()
    {
        var exp = new UnaryMinus(LengthValue.Meter(10).AsExpression());
        var expected = LengthValue.Meter(-10);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteTimeNumberTest()
    {
        var exp = new UnaryMinus(TimeValue.Second(10).AsExpression());
        var expected = TimeValue.Second(-10);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteAreaNumberTest()
    {
        var exp = new UnaryMinus(AreaValue.Meter(10).AsExpression());
        var expected = AreaValue.Meter(-10);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteVolumeNumberTest()
    {
        var exp = new UnaryMinus(VolumeValue.Meter(10).AsExpression());
        var expected = VolumeValue.Meter(-10);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteComplexTest()
    {
        var complex = new Complex(2, 3);
        var exp = new UnaryMinus(new ComplexNumber(complex));

        Assert.That(exp.Execute(), Is.EqualTo(Complex.Negate(complex)));
    }

    [Test]
    public void ExecuteRationalNumberTest()
    {
        var rational = new UnaryMinus(new Rational(Number.One, Number.Two));
        var expected = new RationalValue(-1, 2);
        var actual = rational.Execute();

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void NotSupportedException()
        => TestNotSupported(new UnaryMinus(Bool.False));

    [Test]
    public void CloneTest()
    {
        var exp = new UnaryMinus(Number.Zero);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }

    [Test]
    public void NullAnalyzerTest1()
    {
        var exp = new UnaryMinus(Number.One);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Test]
    public void NullAnalyzerTest2()
    {
        var exp = new UnaryMinus(Number.One);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }
}