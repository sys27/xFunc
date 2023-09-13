// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;
using Vector = xFunc.Maths.Expressions.Matrices.Vector;
using Matrix = xFunc.Maths.Expressions.Matrices.Matrix;

namespace xFunc.Tests.Expressions;

public class SubTest : BaseExpressionTests
{
    [Test]
    public void ExecuteTest1()
    {
        var exp = new Sub(Number.One, Number.Two);
        var expected = new NumberValue(-1.0);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteTest2()
    {
        var exp = new Sub(new ComplexNumber(7, 3), new ComplexNumber(2, 4));
        var expected = new Complex(5, -1);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteTest3()
    {
        var exp = new Sub(new Number(7), new ComplexNumber(2, 4));
        var expected = new Complex(5, -4);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteTest4()
    {
        var exp = new Sub(new ComplexNumber(7, 3), Number.Two);
        var expected = new Complex(5, 3);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteTest6()
    {
        var exp = new Sub(Number.Two, new Sqrt(new Number(-9)));
        var expected = new Complex(2, -3);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void SubTwoVectorsTest()
    {
        var vector1 = new Vector(new IExpression[] { Number.Two, new Number(3) });
        var vector2 = new Vector(new IExpression[] { new Number(7), Number.One });
        var sub = new Sub(vector1, vector2);

        var expected = VectorValue.Create(new NumberValue(-5), NumberValue.Two);
        var result = sub.Execute();

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void SubTwoMatricesTest()
    {
        var matrix1 = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(6), new Number(3) }),
            new Vector(new IExpression[] { Number.Two, Number.One })
        });
        var matrix2 = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(9), Number.Two }),
            new Vector(new IExpression[] { new Number(4), new Number(3) })
        });
        var sub = new Sub(matrix1, matrix2);

        var expected = MatrixValue.Create(new NumberValue[][]
        {
            new NumberValue[] { new NumberValue(-3), NumberValue.One, },
            new NumberValue[] { new NumberValue(-2), new NumberValue(-2), },
        });
        var result = sub.Execute();

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Sub4MatricesTest()
    {
        var vector1 = new Vector(new IExpression[] { Number.One, Number.Two });
        var vector2 = new Vector(new IExpression[] { Number.One, Number.Two });
        var vector3 = new Vector(new IExpression[] { Number.One, Number.Two });
        var vector4 = new Vector(new IExpression[] { Number.One, Number.Two });
        var sub1 = new Sub(vector1, vector2);
        var sub2 = new Sub(vector3, vector4);
        var sub3 = new Sub(sub1, sub2);

        var expected = VectorValue.Create(NumberValue.Zero, NumberValue.Zero);

        Assert.That(sub3.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void SubNumberAndDegree()
    {
        var exp = new Sub(Number.One, AngleValue.Degree(10).AsExpression());
        var actual = exp.Execute();
        var expected = AngleValue.Degree(-9);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void SubRadianAndNumber()
    {
        var exp = new Sub(AngleValue.Radian(10).AsExpression(), Number.One);
        var actual = exp.Execute();
        var expected = AngleValue.Radian(9);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void SubDegreeAndRadian()
    {
        var exp = new Sub(
            AngleValue.Radian(Math.PI).AsExpression(),
            AngleValue.Degree(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = AngleValue.Degree(170);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void SubGradianAndGradian()
    {
        var exp = new Sub(
            AngleValue.Gradian(30).AsExpression(),
            AngleValue.Gradian(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = AngleValue.Gradian(20);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void SubNumberAndPower()
    {
        var exp = new Sub(
            Number.One,
            PowerValue.Watt(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = PowerValue.Watt(-9);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void SubPowerAndNumber()
    {
        var exp = new Sub(
            PowerValue.Watt(10).AsExpression(),
            Number.One
        );
        var actual = exp.Execute();
        var expected = PowerValue.Watt(9);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void SubPowerAndPower()
    {
        var exp = new Sub(
            PowerValue.Watt(20).AsExpression(),
            PowerValue.Watt(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = PowerValue.Watt(10);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void SubNumberAndTemperature()
    {
        var exp = new Sub(
            Number.One,
            TemperatureValue.Celsius(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = TemperatureValue.Celsius(-9);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void SubTemperatureAndNumber()
    {
        var exp = new Sub(
            TemperatureValue.Celsius(10).AsExpression(),
            Number.One
        );
        var actual = exp.Execute();
        var expected = TemperatureValue.Celsius(9);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void SubTemperatureAndTemperature()
    {
        var exp = new Sub(
            TemperatureValue.Celsius(20).AsExpression(),
            TemperatureValue.Celsius(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = TemperatureValue.Celsius(10);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void SubNumberAndMass()
    {
        var exp = new Sub(
            Number.One,
            MassValue.Gram(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = MassValue.Gram(-9);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void SubMassAndNumber()
    {
        var exp = new Sub(
            MassValue.Gram(10).AsExpression(),
            Number.One
        );
        var actual = exp.Execute();
        var expected = MassValue.Gram(9);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void SubMassAndMass()
    {
        var exp = new Sub(
            MassValue.Gram(20).AsExpression(),
            MassValue.Gram(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = MassValue.Gram(10);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void SubNumberAndLength()
    {
        var exp = new Sub(
            Number.One,
            LengthValue.Meter(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = LengthValue.Meter(-9);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void SubLengthAndNumber()
    {
        var exp = new Sub(
            LengthValue.Meter(10).AsExpression(),
            Number.One
        );
        var actual = exp.Execute();
        var expected = LengthValue.Meter(9);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void SubLengthAndLength()
    {
        var exp = new Sub(
            LengthValue.Meter(20).AsExpression(),
            LengthValue.Meter(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = LengthValue.Meter(10);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void SubNumberAndTime()
    {
        var exp = new Sub(
            Number.One,
            TimeValue.Second(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = TimeValue.Second(-9);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void SubTimeAndNumber()
    {
        var exp = new Sub(
            TimeValue.Second(10).AsExpression(),
            Number.One
        );
        var actual = exp.Execute();
        var expected = TimeValue.Second(9);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void SubTimeAndTime()
    {
        var exp = new Sub(
            TimeValue.Second(20).AsExpression(),
            TimeValue.Second(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = TimeValue.Second(10);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void SubNumberAndArea()
    {
        var exp = new Sub(
            Number.One,
            AreaValue.Meter(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = AreaValue.Meter(-9);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void SubAreaAndNumber()
    {
        var exp = new Sub(
            AreaValue.Meter(10).AsExpression(),
            Number.One
        );
        var actual = exp.Execute();
        var expected = AreaValue.Meter(9);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void SubAreaAndArea()
    {
        var exp = new Sub(
            AreaValue.Meter(20).AsExpression(),
            AreaValue.Meter(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = AreaValue.Meter(10);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void SubNumberAndVolume()
    {
        var exp = new Sub(
            Number.One,
            VolumeValue.Meter(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = VolumeValue.Meter(-9);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void SubVolumeAndNumber()
    {
        var exp = new Sub(
            VolumeValue.Meter(10).AsExpression(),
            Number.One
        );
        var actual = exp.Execute();
        var expected = VolumeValue.Meter(9);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void SubVolumeAndVolume()
    {
        var exp = new Sub(
            VolumeValue.Meter(20).AsExpression(),
            VolumeValue.Meter(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = VolumeValue.Meter(10);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteRationalAndRational()
    {
        var exp = new Sub(
            new Rational(Number.One, Number.Two),
            new Rational(Number.Two, Number.One)
        );
        var expected = new RationalValue(-3, 2);
        var actual = exp.Execute();

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteNumberAndRational()
    {
        var exp = new Sub(
            Number.One,
            new Rational(Number.One, Number.Two)
        );
        var expected = new RationalValue(1, 2);
        var actual = exp.Execute();

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteRationalAndNumber()
    {
        var exp = new Sub(
            new Rational(new Number(3), Number.Two),
            Number.One
        );
        var expected = new RationalValue(1, 2);
        var actual = exp.Execute();

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteWrongArgumentTypeTest()
        => TestNotSupported(new Sub(Bool.True, Bool.True));

    [Test]
    public void CloneTest()
    {
        var exp = new Sub(new Number(5), Number.Zero);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}