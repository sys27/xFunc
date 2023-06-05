// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;
using Vector = xFunc.Maths.Expressions.Matrices.Vector;
using Matrix = xFunc.Maths.Expressions.Matrices.Matrix;

namespace xFunc.Tests.Expressions;

public class MulTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteMulNumberByNumberTest()
    {
        var exp = new Mul(Number.Two, Number.Two);
        var expected = new NumberValue(4.0);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteMulComplexByComplexTest()
    {
        var exp = new Mul(new ComplexNumber(2, 5), new ComplexNumber(3, 2));
        var expected = new Complex(-4, 19);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteMulComplexByNumberTest()
    {
        var exp = new Mul(new ComplexNumber(2, 5), Number.Two);
        var expected = new Complex(4, 10);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteMulNumberByComplexTest()
    {
        var exp = new Mul(Number.Two, new ComplexNumber(3, 2));
        var expected = new Complex(6, 4);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteMulNumberBySqrtComplexTest()
    {
        var exp = new Mul(Number.Two, new Sqrt(new Number(-9)));
        var expected = new Complex(0, 6);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteDotProductTest()
    {
        var vector1 = new Vector(new IExpression[]
        {
            Number.One, Number.Two, new Number(3)
        });
        var vector2 = new Vector(new IExpression[]
        {
            new Number(4), new Number(5), new Number(6)
        });
        var exp = new Mul(vector1, vector2);
        var expected = new NumberValue(32.0);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteDotProductErrorTest()
    {
        var vector1 = new Vector(new IExpression[]
        {
            Number.One, Number.Two,
        });
        var vector2 = new Vector(new IExpression[]
        {
            new Number(4), new Number(5), new Number(6)
        });
        var exp = new Mul(vector1, vector2);

        Assert.Throws<ArgumentException>(() => exp.Execute());
    }

    [Fact]
    public void ExecuteMulComplexByBool()
    {
        var complex = new ComplexNumber(3, 2);
        var boolean = Bool.True;
        var mul = new Mul(complex, boolean);

        Assert.Throws<ResultIsNotSupportedException>(() => mul.Execute());
    }

    [Fact]
    public void ExecuteMulBoolByComplex()
    {
        var boolean = Bool.True;
        var complex = new ComplexNumber(3, 2);
        var mul = new Mul(boolean, complex);

        Assert.Throws<ResultIsNotSupportedException>(() => mul.Execute());
    }

    [Fact]
    public void ExecuteMulVectorByMatrixTest()
    {
        var vector = new Vector(new IExpression[]
        {
            Number.One,
            Number.Two,
            new Number(3)
        });
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(4) }),
            new Vector(new IExpression[] { new Number(5) }),
            new Vector(new IExpression[] { new Number(6) })
        });
        var exp = new Mul(vector, matrix);

        var expected = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(32) })
        });

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteMulMatrixByVectorTest()
    {
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(4) }),
            new Vector(new IExpression[] { new Number(5) }),
            new Vector(new IExpression[] { new Number(6) })
        });
        var vector = new Vector(new IExpression[]
        {
            Number.One,
            Number.Two,
            new Number(3)
        });
        var exp = new Mul(matrix, vector);

        var expected = new Matrix(new[]
        {
            new Vector(new IExpression[]
            {
                new Number(4), new Number(8), new Number(12)
            }),
            new Vector(new IExpression[]
            {
                new Number(5), new Number(10), new Number(15)
            }),
            new Vector(new IExpression[]
            {
                new Number(6), new Number(12), new Number(18)
            })
        });

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteMulMatrixByMatrixTest()
    {
        var matrix1 = new Matrix(new[]
        {
            new Vector(new IExpression[] { Number.One, Number.Two, new Number(3) })
        });
        var matrix2 = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(4) }),
            new Vector(new IExpression[] { new Number(5) }),
            new Vector(new IExpression[] { new Number(6) })
        });
        var exp = new Mul(matrix1, matrix2);

        var expected = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(32) })
        });

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteMulNumberByVectorTest()
    {
        var number = new Number(5);
        var vector = new Vector(new IExpression[]
        {
            Number.One,
            Number.Two,
            new Number(3)
        });
        var exp = new Mul(number, vector);

        var expected = new Vector(new IExpression[]
        {
            new Number(5),
            new Number(10),
            new Number(15)
        });

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteMulMatrixByNumberTest()
    {
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { Number.One, Number.Two }),
            new Vector(new IExpression[] { new Number(3), new Number(4) })
        });
        var number = new Number(5);
        var exp = new Mul(matrix, number);

        var expected = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(5), new Number(10) }),
            new Vector(new IExpression[] { new Number(15), new Number(20) })
        });

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteMulNumberByMatrixTest()
    {
        var number = new Number(5);
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { Number.One, Number.Two }),
            new Vector(new IExpression[] { new Number(3), new Number(4) })
        });
        var exp = new Mul(number, matrix);

        var expected = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(5), new Number(10) }),
            new Vector(new IExpression[] { new Number(15), new Number(20) })
        });

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void MulNumberAndDegree()
    {
        var exp = new Mul(Number.Two, AngleValue.Degree(10).AsExpression());
        var actual = exp.Execute();
        var expected = AngleValue.Degree(20);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MulRadianAndNumber()
    {
        var exp = new Mul(AngleValue.Radian(10).AsExpression(), Number.Two);
        var actual = exp.Execute();
        var expected = AngleValue.Radian(20);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MulNumberAndPower()
    {
        var exp = new Mul(
            Number.Two,
            PowerValue.Watt(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = PowerValue.Watt(20);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MulPowerAndNumber()
    {
        var exp = new Mul(
            PowerValue.Watt(10).AsExpression(),
            Number.Two
        );
        var actual = exp.Execute();
        var expected = PowerValue.Watt(20);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MulNumberAndTemperature()
    {
        var exp = new Mul(
            Number.Two,
            TemperatureValue.Celsius(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = TemperatureValue.Celsius(20);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MulTemperatureAndNumber()
    {
        var exp = new Mul(
            TemperatureValue.Celsius(10).AsExpression(),
            Number.Two
        );
        var actual = exp.Execute();
        var expected = TemperatureValue.Celsius(20);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MulNumberAndMass()
    {
        var exp = new Mul(
            Number.Two,
            MassValue.Gram(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = MassValue.Gram(20);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MulMassAndNumber()
    {
        var exp = new Mul(
            MassValue.Gram(10).AsExpression(),
            Number.Two
        );
        var actual = exp.Execute();
        var expected = MassValue.Gram(20);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MulNumberAndLength()
    {
        var exp = new Mul(
            Number.Two,
            LengthValue.Meter(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = LengthValue.Meter(20);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MulLengthAndNumber()
    {
        var exp = new Mul(
            LengthValue.Meter(10).AsExpression(),
            Number.Two
        );
        var actual = exp.Execute();
        var expected = LengthValue.Meter(20);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MulLengthAndLength()
    {
        var exp = new Mul(
            LengthValue.Meter(10).AsExpression(),
            LengthValue.Meter(2).AsExpression()
        );
        var actual = exp.Execute();
        var expected = AreaValue.Meter(20);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MulAreaAndLength()
    {
        var exp = new Mul(
            AreaValue.Meter(10).AsExpression(),
            LengthValue.Meter(2).AsExpression()
        );
        var actual = exp.Execute();
        var expected = VolumeValue.Meter(20);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MulLengthAndArea()
    {
        var exp = new Mul(
            LengthValue.Meter(10).AsExpression(),
            AreaValue.Meter(2).AsExpression()
        );
        var actual = exp.Execute();
        var expected = VolumeValue.Meter(20);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MulNumberAndTime()
    {
        var exp = new Mul(
            Number.Two,
            TimeValue.Second(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = TimeValue.Second(20);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MulTimeAndNumber()
    {
        var exp = new Mul(
            TimeValue.Second(10).AsExpression(),
            Number.Two
        );
        var actual = exp.Execute();
        var expected = TimeValue.Second(20);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MulNumberAndArea()
    {
        var exp = new Mul(
            Number.Two,
            AreaValue.Meter(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = AreaValue.Meter(20);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MulAreaAndNumber()
    {
        var exp = new Mul(
            AreaValue.Meter(10).AsExpression(),
            Number.Two
        );
        var actual = exp.Execute();
        var expected = AreaValue.Meter(20);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MulNumberAndVolume()
    {
        var exp = new Mul(
            Number.Two,
            VolumeValue.Meter(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = VolumeValue.Meter(20);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MulVolumeAndNumber()
    {
        var exp = new Mul(
            VolumeValue.Meter(10).AsExpression(),
            Number.Two
        );
        var actual = exp.Execute();
        var expected = VolumeValue.Meter(20);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteMulBoolByBoolTest()
        => TestNotSupported(new Mul(Bool.True, Bool.True));

    [Fact]
    public void CloneTest()
    {
        var exp = new Mul(Variable.X, Number.Zero);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}