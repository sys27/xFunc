// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;
using Vector = xFunc.Maths.Expressions.Matrices.Vector;
using Matrix = xFunc.Maths.Expressions.Matrices.Matrix;

namespace xFunc.Tests.Expressions;

public class AddTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteTestNumber1()
    {
        var exp = new Add(Number.One, Number.Two);
        var expected = new NumberValue(3.0);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteTestNumber2()
    {
        var exp = new Add(new Number(-3), Number.Two);
        var expected = new NumberValue(-1.0);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteTestComplexNumber()
    {
        var exp = new Add(new ComplexNumber(7, 3), new ComplexNumber(2, 4));
        var expected = new Complex(9, 7);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteTestNumberComplexNumber()
    {
        var exp = new Add(new Number(7), new ComplexNumber(2, 4));
        var expected = new Complex(9, 4);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteTestComplexNumberNumber()
    {
        var exp = new Add(new ComplexNumber(7, 3), Number.Two);
        var expected = new Complex(9, 3);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteTest6()
    {
        var exp = new Add(Number.Two, new Sqrt(new Number(-9)));
        var expected = new Complex(2, 3);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void AddTwoVectorsTest()
    {
        var vector1 = new Vector(new IExpression[] { Number.Two, new Number(3) });
        var vector2 = new Vector(new IExpression[] { new Number(7), Number.One });
        var add = new Add(vector1, vector2);

        var expected = VectorValue.Create(new NumberValue(9), new NumberValue(4));
        var result = add.Execute();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void AddTwoMatricesTest()
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
        var add = new Add(matrix1, matrix2);

        var expected = MatrixValue.Create(new NumberValue[][]
        {
            new NumberValue[] { new NumberValue(15), new NumberValue(5) },
            new NumberValue[] { new NumberValue(6), new NumberValue(4) },
        });
        var result = add.Execute();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Add4MatricesTest()
    {
        var vector1 = new Vector(new IExpression[] { Number.One, Number.Two });
        var vector2 = new Vector(new IExpression[] { Number.One, Number.Two });
        var vector3 = new Vector(new IExpression[] { Number.One, Number.Two });
        var vector4 = new Vector(new IExpression[] { Number.One, Number.Two });
        var add1 = new Add(vector1, vector2);
        var add2 = new Add(vector3, vector4);
        var add3 = new Add(add1, add2);

        var expected = VectorValue.Create(new NumberValue(4), new NumberValue(8));

        Assert.Equal(expected, add3.Execute());
    }

    [Fact]
    public void AddNumberAndDegree()
    {
        var exp = new Add(Number.One, AngleValue.Degree(10).AsExpression());
        var actual = exp.Execute();
        var expected = AngleValue.Degree(11);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void AddRadianAndNumber()
    {
        var exp = new Add(AngleValue.Radian(10).AsExpression(), Number.One);
        var actual = exp.Execute();
        var expected = AngleValue.Radian(11);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void AddDegreeAndRadian()
    {
        var exp = new Add(
            AngleValue.Degree(10).AsExpression(),
            AngleValue.Radian(Math.PI).AsExpression()
        );
        var actual = exp.Execute();
        var expected = AngleValue.Degree(190);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void AddGradianAndGradian()
    {
        var exp = new Add(
            AngleValue.Gradian(10).AsExpression(),
            AngleValue.Gradian(20).AsExpression()
        );
        var actual = exp.Execute();
        var expected = AngleValue.Gradian(30);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteNumberAndPower()
    {
        var exp = new Add(
            Number.One,
            PowerValue.Watt(1).AsExpression()
        );
        var actual = exp.Execute();
        var expected = PowerValue.Watt(2);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecutePowerAndNumber()
    {
        var exp = new Add(
            PowerValue.Watt(1).AsExpression(),
            Number.One
        );
        var actual = exp.Execute();
        var expected = PowerValue.Watt(2);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecutePowerAndPower()
    {
        var exp = new Add(
            PowerValue.Watt(1).AsExpression(),
            PowerValue.Watt(2).AsExpression()
        );
        var actual = exp.Execute();
        var expected = PowerValue.Watt(3);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteNumberAndTemperature()
    {
        var exp = new Add(
            Number.One,
            TemperatureValue.Celsius(1).AsExpression()
        );
        var actual = exp.Execute();
        var expected = TemperatureValue.Celsius(2);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteTemperatureAndNumber()
    {
        var exp = new Add(
            TemperatureValue.Celsius(1).AsExpression(),
            Number.One
        );
        var actual = exp.Execute();
        var expected = TemperatureValue.Celsius(2);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteTemperatureAndTemperature()
    {
        var exp = new Add(
            TemperatureValue.Celsius(1).AsExpression(),
            TemperatureValue.Celsius(2).AsExpression()
        );
        var actual = exp.Execute();
        var expected = TemperatureValue.Celsius(3);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteNumberAndMass()
    {
        var exp = new Add(
            Number.One,
            MassValue.Gram(1).AsExpression()
        );
        var actual = exp.Execute();
        var expected = MassValue.Gram(2);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteMassAndNumber()
    {
        var exp = new Add(
            MassValue.Gram(1).AsExpression(),
            Number.One
        );
        var actual = exp.Execute();
        var expected = MassValue.Gram(2);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteMassAndMass()
    {
        var exp = new Add(
            MassValue.Gram(1).AsExpression(),
            MassValue.Gram(2).AsExpression()
        );
        var actual = exp.Execute();
        var expected = MassValue.Gram(3);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteNumberAndLength()
    {
        var exp = new Add(
            Number.One,
            LengthValue.Meter(1).AsExpression()
        );
        var actual = exp.Execute();
        var expected = LengthValue.Meter(2);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteLengthAndNumber()
    {
        var exp = new Add(
            LengthValue.Meter(1).AsExpression(),
            Number.One
        );
        var actual = exp.Execute();
        var expected = LengthValue.Meter(2);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteLengthAndLength()
    {
        var exp = new Add(
            LengthValue.Meter(1).AsExpression(),
            LengthValue.Meter(2).AsExpression()
        );
        var actual = exp.Execute();
        var expected = LengthValue.Meter(3);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteNumberAndTime()
    {
        var exp = new Add(
            Number.One,
            TimeValue.Second(1).AsExpression()
        );
        var actual = exp.Execute();
        var expected = TimeValue.Second(2);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteTimeAndNumber()
    {
        var exp = new Add(
            TimeValue.Second(1).AsExpression(),
            Number.One
        );
        var actual = exp.Execute();
        var expected = TimeValue.Second(2);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteTimeAndTime()
    {
        var exp = new Add(
            TimeValue.Second(1).AsExpression(),
            TimeValue.Second(2).AsExpression()
        );
        var actual = exp.Execute();
        var expected = TimeValue.Second(3);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteNumberAndArea()
    {
        var exp = new Add(
            Number.One,
            AreaValue.Meter(1).AsExpression()
        );
        var actual = exp.Execute();
        var expected = AreaValue.Meter(2);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteAreaAndNumber()
    {
        var exp = new Add(
            AreaValue.Meter(1).AsExpression(),
            Number.One
        );
        var actual = exp.Execute();
        var expected = AreaValue.Meter(2);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteTimeAndArea()
    {
        var exp = new Add(
            AreaValue.Meter(1).AsExpression(),
            AreaValue.Meter(2).AsExpression()
        );
        var actual = exp.Execute();
        var expected = AreaValue.Meter(3);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteNumberAndVolume()
    {
        var exp = new Add(
            Number.One,
            VolumeValue.Meter(1).AsExpression()
        );
        var actual = exp.Execute();
        var expected = VolumeValue.Meter(2);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteVolumeAndNumber()
    {
        var exp = new Add(
            VolumeValue.Meter(1).AsExpression(),
            Number.One
        );
        var actual = exp.Execute();
        var expected = VolumeValue.Meter(2);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteTimeAndVolume()
    {
        var exp = new Add(
            VolumeValue.Meter(1).AsExpression(),
            VolumeValue.Meter(2).AsExpression()
        );
        var actual = exp.Execute();
        var expected = VolumeValue.Meter(3);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteStringAndString()
    {
        var exp = new Add(
            new StringExpression("a"),
            new StringExpression("b")
        );
        var actual = exp.Execute();
        var expected = "ab";

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteStringAndNumber()
    {
        var exp = new Add(
            new StringExpression("a"),
            Number.One
        );
        var actual = exp.Execute();
        var expected = "a1";

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteNumberAndString()
    {
        var exp = new Add(
            Number.One,
            new StringExpression("b")
        );
        var actual = exp.Execute();
        var expected = "1b";

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteTestException()
        => TestNotSupported(new Add(Bool.False, Bool.False));

    [Fact]
    public void ExecuteComplexNumberAndBool()
        => TestNotSupported(new Add(new ComplexNumber(7, 3), Bool.False));

    [Fact]
    public void ExecuteBoolAndComplexNumber()
        => TestNotSupported(new Add(Bool.False, new ComplexNumber(7, 3)));

    [Fact]
    public void AnalyzeNull()
    {
        var exp = new Add(Number.One, Number.One);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<object>(null));
    }

    [Fact]
    public void AnalyzeContextNull()
    {
        var exp = new Add(Number.One, Number.One);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<object, object>(null, null));
    }

    [Fact]
    public void CloneTest()
    {
        var exp = new Add(Variable.X, Number.Zero);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}