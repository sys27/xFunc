// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;
using Vector = xFunc.Maths.Expressions.Matrices.Vector;
using Matrix = xFunc.Maths.Expressions.Matrices.Matrix;

namespace xFunc.Tests.Expressions;

public class SubTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteTest1()
    {
        var exp = new Sub(Number.One, Number.Two);
        var expected = new NumberValue(-1.0);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteTest2()
    {
        var exp = new Sub(new ComplexNumber(7, 3), new ComplexNumber(2, 4));
        var expected = new Complex(5, -1);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteTest3()
    {
        var exp = new Sub(new Number(7), new ComplexNumber(2, 4));
        var expected = new Complex(5, -4);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteTest4()
    {
        var exp = new Sub(new ComplexNumber(7, 3), Number.Two);
        var expected = new Complex(5, 3);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteTest6()
    {
        var exp = new Sub(Number.Two, new Sqrt(new Number(-9)));
        var expected = new Complex(2, -3);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void SubTwoVectorsTest()
    {
        var vector1 = new Vector(new IExpression[] { Number.Two, new Number(3) });
        var vector2 = new Vector(new IExpression[] { new Number(7), Number.One });
        var sub = new Sub(vector1, vector2);

        var expected = new Vector(new IExpression[] { new Number(-5), Number.Two });
        var result = sub.Execute();

        Assert.Equal(expected, result);
    }

    [Fact]
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

        var expected = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(-3), Number.One }),
            new Vector(new IExpression[] { new Number(-2), new Number(-2) })
        });
        var result = sub.Execute();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Sub4MatricesTest()
    {
        var vector1 = new Vector(new IExpression[] { Number.One, Number.Two });
        var vector2 = new Vector(new IExpression[] { Number.One, Number.Two });
        var vector3 = new Vector(new IExpression[] { Number.One, Number.Two });
        var vector4 = new Vector(new IExpression[] { Number.One, Number.Two });
        var sub1 = new Sub(vector1, vector2);
        var sub2 = new Sub(vector3, vector4);
        var sub3 = new Sub(sub1, sub2);

        var expected = new Vector(new IExpression[] { Number.Zero, Number.Zero });

        Assert.Equal(expected, sub3.Execute());
    }

    [Fact]
    public void SubNumberAndDegree()
    {
        var exp = new Sub(Number.One, AngleValue.Degree(10).AsExpression());
        var actual = exp.Execute();
        var expected = AngleValue.Degree(-9);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SubRadianAndNumber()
    {
        var exp = new Sub(AngleValue.Radian(10).AsExpression(), Number.One);
        var actual = exp.Execute();
        var expected = AngleValue.Radian(9);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SubDegreeAndRadian()
    {
        var exp = new Sub(
            AngleValue.Radian(Math.PI).AsExpression(),
            AngleValue.Degree(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = AngleValue.Degree(170);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SubGradianAndGradian()
    {
        var exp = new Sub(
            AngleValue.Gradian(30).AsExpression(),
            AngleValue.Gradian(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = AngleValue.Gradian(20);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SubNumberAndPower()
    {
        var exp = new Sub(
            Number.One,
            PowerValue.Watt(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = PowerValue.Watt(-9);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SubPowerAndNumber()
    {
        var exp = new Sub(
            PowerValue.Watt(10).AsExpression(),
            Number.One
        );
        var actual = exp.Execute();
        var expected = PowerValue.Watt(9);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SubPowerAndPower()
    {
        var exp = new Sub(
            PowerValue.Watt(20).AsExpression(),
            PowerValue.Watt(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = PowerValue.Watt(10);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SubNumberAndTemperature()
    {
        var exp = new Sub(
            Number.One,
            TemperatureValue.Celsius(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = TemperatureValue.Celsius(-9);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SubTemperatureAndNumber()
    {
        var exp = new Sub(
            TemperatureValue.Celsius(10).AsExpression(),
            Number.One
        );
        var actual = exp.Execute();
        var expected = TemperatureValue.Celsius(9);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SubTemperatureAndTemperature()
    {
        var exp = new Sub(
            TemperatureValue.Celsius(20).AsExpression(),
            TemperatureValue.Celsius(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = TemperatureValue.Celsius(10);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SubNumberAndMass()
    {
        var exp = new Sub(
            Number.One,
            MassValue.Gram(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = MassValue.Gram(-9);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SubMassAndNumber()
    {
        var exp = new Sub(
            MassValue.Gram(10).AsExpression(),
            Number.One
        );
        var actual = exp.Execute();
        var expected = MassValue.Gram(9);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SubMassAndMass()
    {
        var exp = new Sub(
            MassValue.Gram(20).AsExpression(),
            MassValue.Gram(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = MassValue.Gram(10);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SubNumberAndLength()
    {
        var exp = new Sub(
            Number.One,
            LengthValue.Meter(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = LengthValue.Meter(-9);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SubLengthAndNumber()
    {
        var exp = new Sub(
            LengthValue.Meter(10).AsExpression(),
            Number.One
        );
        var actual = exp.Execute();
        var expected = LengthValue.Meter(9);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SubLengthAndLength()
    {
        var exp = new Sub(
            LengthValue.Meter(20).AsExpression(),
            LengthValue.Meter(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = LengthValue.Meter(10);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SubNumberAndTime()
    {
        var exp = new Sub(
            Number.One,
            TimeValue.Second(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = TimeValue.Second(-9);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SubTimeAndNumber()
    {
        var exp = new Sub(
            TimeValue.Second(10).AsExpression(),
            Number.One
        );
        var actual = exp.Execute();
        var expected = TimeValue.Second(9);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SubTimeAndTime()
    {
        var exp = new Sub(
            TimeValue.Second(20).AsExpression(),
            TimeValue.Second(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = TimeValue.Second(10);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SubNumberAndArea()
    {
        var exp = new Sub(
            Number.One,
            AreaValue.Meter(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = AreaValue.Meter(-9);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SubAreaAndNumber()
    {
        var exp = new Sub(
            AreaValue.Meter(10).AsExpression(),
            Number.One
        );
        var actual = exp.Execute();
        var expected = AreaValue.Meter(9);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SubAreaAndArea()
    {
        var exp = new Sub(
            AreaValue.Meter(20).AsExpression(),
            AreaValue.Meter(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = AreaValue.Meter(10);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SubNumberAndVolume()
    {
        var exp = new Sub(
            Number.One,
            VolumeValue.Meter(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = VolumeValue.Meter(-9);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SubVolumeAndNumber()
    {
        var exp = new Sub(
            VolumeValue.Meter(10).AsExpression(),
            Number.One
        );
        var actual = exp.Execute();
        var expected = VolumeValue.Meter(9);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SubVolumeAndVolume()
    {
        var exp = new Sub(
            VolumeValue.Meter(20).AsExpression(),
            VolumeValue.Meter(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = VolumeValue.Meter(10);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteWrongArgumentTypeTest()
        => TestNotSupported(new Sub(Bool.True, Bool.True));

    [Fact]
    public void CloneTest()
    {
        var exp = new Sub(new Number(5), Number.Zero);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}