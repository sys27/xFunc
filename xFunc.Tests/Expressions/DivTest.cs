// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions;

public class DivTest : BaseExpressionTests
{
    [Test]
    public void ExecuteTest1()
    {
        var exp = new Div(Number.One, Number.Two);
        var expected = new NumberValue(1.0 / 2.0);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteTest2()
    {
        var exp = new Div(new ComplexNumber(3, 2), new ComplexNumber(2, 4));
        var expected = new Complex(0.7, -0.4);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteTest3()
    {
        var exp = new Div(new Number(3), new ComplexNumber(2, 4));
        var expected = new Complex(0.3, -0.6);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteTest4()
    {
        var exp = new Div(new ComplexNumber(3, 2), Number.Two);
        var expected = new Complex(1.5, 1);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteTest6()
    {
        var exp = new Div(new Sqrt(new Number(-16)), Number.Two);
        var expected = new Complex(0, 2);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void DivRadianAndNumber()
    {
        var exp = new Div(AngleValue.Radian(10).AsExpression(), Number.Two);
        var actual = exp.Execute();
        var expected = AngleValue.Radian(5);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void DivPowerAndNumber()
    {
        var exp = new Div(
            PowerValue.Watt(10).AsExpression(),
            new Number(2)
        );
        var actual = exp.Execute();
        var expected = PowerValue.Watt(5);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void DivTemperatureAndNumber()
    {
        var exp = new Div(
            TemperatureValue.Celsius(10).AsExpression(),
            new Number(2)
        );
        var actual = exp.Execute();
        var expected = TemperatureValue.Celsius(5);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void DivMassAndNumber()
    {
        var exp = new Div(
            MassValue.Gram(10).AsExpression(),
            new Number(2)
        );
        var actual = exp.Execute();
        var expected = MassValue.Gram(5);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void DivLengthAndNumber()
    {
        var exp = new Div(
            LengthValue.Meter(10).AsExpression(),
            new Number(2)
        );
        var actual = exp.Execute();
        var expected = LengthValue.Meter(5);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void DivTimeAndNumber()
    {
        var exp = new Div(
            TimeValue.Second(10).AsExpression(),
            new Number(2)
        );
        var actual = exp.Execute();
        var expected = TimeValue.Second(5);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void DivAreaAndNumber()
    {
        var exp = new Div(
            AreaValue.Meter(10).AsExpression(),
            new Number(2)
        );
        var actual = exp.Execute();
        var expected = AreaValue.Meter(5);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void DivVolumeAndNumber()
    {
        var exp = new Div(
            VolumeValue.Meter(10).AsExpression(),
            new Number(2)
        );
        var actual = exp.Execute();
        var expected = VolumeValue.Meter(5);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteRationalAndRational()
    {
        var exp = new Div(
            new Rational(Number.One, Number.Two),
            new Rational(Number.Two, Number.One)
        );
        var expected = new RationalValue(1, 4);
        var actual = exp.Execute();

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteNumberAndRational()
    {
        var exp = new Div(
            Number.One,
            new Rational(Number.Two, Number.One)
        );
        var expected = new RationalValue(1, 2);
        var actual = exp.Execute();

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteRationalAndNumber()
    {
        var exp = new Div(
            new Rational(new Number(3), Number.Two),
            Number.Two
        );
        var expected = new RationalValue(3, 4);
        var actual = exp.Execute();

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteBoolTest()
    {
        var exp = new Div(Bool.False, Bool.True);

        TestNotSupported(exp);
    }

    [Test]
    public void ExecuteComplexNumberBoolTest()
        => TestNotSupported(new Div(new ComplexNumber(2, 4), Bool.True));

    [Test]
    public void ExecuteBoolComplexNumberTest()
        => TestNotSupported(new Div(Bool.True, new ComplexNumber(2, 4)));

    [Test]
    public void CloneTest()
    {
        var exp = new Div(Variable.X, Number.Zero);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}