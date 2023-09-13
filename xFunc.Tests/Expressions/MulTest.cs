// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;
using Vector = xFunc.Maths.Expressions.Matrices.Vector;
using Matrix = xFunc.Maths.Expressions.Matrices.Matrix;

namespace xFunc.Tests.Expressions;

public class MulTest : BaseExpressionTests
{
    [Test]
    public void ExecuteMulNumberByNumberTest()
    {
        var exp = new Mul(Number.Two, Number.Two);
        var expected = new NumberValue(4.0);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteMulComplexByComplexTest()
    {
        var exp = new Mul(new ComplexNumber(2, 5), new ComplexNumber(3, 2));
        var expected = new Complex(-4, 19);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteMulComplexByNumberTest()
    {
        var exp = new Mul(new ComplexNumber(2, 5), Number.Two);
        var expected = new Complex(4, 10);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteMulNumberByComplexTest()
    {
        var exp = new Mul(Number.Two, new ComplexNumber(3, 2));
        var expected = new Complex(6, 4);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteMulNumberBySqrtComplexTest()
    {
        var exp = new Mul(Number.Two, new Sqrt(new Number(-9)));
        var expected = new Complex(0, 6);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
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

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
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

    [Test]
    public void ExecuteMulComplexByBool()
    {
        var complex = new ComplexNumber(3, 2);
        var boolean = Bool.True;
        var mul = new Mul(complex, boolean);

        Assert.Throws<ExecutionException>(() => mul.Execute());
    }

    [Test]
    public void ExecuteMulBoolByComplex()
    {
        var boolean = Bool.True;
        var complex = new ComplexNumber(3, 2);
        var mul = new Mul(boolean, complex);

        Assert.Throws<ExecutionException>(() => mul.Execute());
    }

    [Test]
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

        var expected = MatrixValue.Create(new NumberValue(32));

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
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

        var expected = MatrixValue.Create(new NumberValue[][]
        {
            new NumberValue[] { new NumberValue(4), new NumberValue(8), new NumberValue(12) },
            new NumberValue[] { new NumberValue(5), new NumberValue(10), new NumberValue(15) },
            new NumberValue[] { new NumberValue(6), new NumberValue(12), new NumberValue(18) },
        });

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
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

        var expected = MatrixValue.Create(new NumberValue(32));

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
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

        var expected = VectorValue.Create(new NumberValue(5), new NumberValue(10), new NumberValue(15));

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteMulMatrixByNumberTest()
    {
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { Number.One, Number.Two }),
            new Vector(new IExpression[] { new Number(3), new Number(4) })
        });
        var number = new Number(5);
        var exp = new Mul(matrix, number);

        var expected = MatrixValue.Create(new NumberValue[][]
        {
            new NumberValue[] { new NumberValue(5), new NumberValue(10) },
            new NumberValue[] { new NumberValue(15), new NumberValue(20) },
        });

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteMulNumberByMatrixTest()
    {
        var number = new Number(5);
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { Number.One, Number.Two }),
            new Vector(new IExpression[] { new Number(3), new Number(4) })
        });
        var exp = new Mul(number, matrix);

        var expected = MatrixValue.Create(new NumberValue[][]
        {
            new NumberValue[] { new NumberValue(5), new NumberValue(10) },
            new NumberValue[] { new NumberValue(15), new NumberValue(20) },
        });

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void MulNumberAndDegree()
    {
        var exp = new Mul(Number.Two, AngleValue.Degree(10).AsExpression());
        var actual = exp.Execute();
        var expected = AngleValue.Degree(20);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void MulRadianAndNumber()
    {
        var exp = new Mul(AngleValue.Radian(10).AsExpression(), Number.Two);
        var actual = exp.Execute();
        var expected = AngleValue.Radian(20);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void MulNumberAndPower()
    {
        var exp = new Mul(
            Number.Two,
            PowerValue.Watt(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = PowerValue.Watt(20);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void MulPowerAndNumber()
    {
        var exp = new Mul(
            PowerValue.Watt(10).AsExpression(),
            Number.Two
        );
        var actual = exp.Execute();
        var expected = PowerValue.Watt(20);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void MulNumberAndTemperature()
    {
        var exp = new Mul(
            Number.Two,
            TemperatureValue.Celsius(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = TemperatureValue.Celsius(20);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void MulTemperatureAndNumber()
    {
        var exp = new Mul(
            TemperatureValue.Celsius(10).AsExpression(),
            Number.Two
        );
        var actual = exp.Execute();
        var expected = TemperatureValue.Celsius(20);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void MulNumberAndMass()
    {
        var exp = new Mul(
            Number.Two,
            MassValue.Gram(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = MassValue.Gram(20);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void MulMassAndNumber()
    {
        var exp = new Mul(
            MassValue.Gram(10).AsExpression(),
            Number.Two
        );
        var actual = exp.Execute();
        var expected = MassValue.Gram(20);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void MulNumberAndLength()
    {
        var exp = new Mul(
            Number.Two,
            LengthValue.Meter(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = LengthValue.Meter(20);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void MulLengthAndNumber()
    {
        var exp = new Mul(
            LengthValue.Meter(10).AsExpression(),
            Number.Two
        );
        var actual = exp.Execute();
        var expected = LengthValue.Meter(20);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void MulLengthAndLength()
    {
        var exp = new Mul(
            LengthValue.Meter(10).AsExpression(),
            LengthValue.Meter(2).AsExpression()
        );
        var actual = exp.Execute();
        var expected = AreaValue.Meter(20);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void MulAreaAndLength()
    {
        var exp = new Mul(
            AreaValue.Meter(10).AsExpression(),
            LengthValue.Meter(2).AsExpression()
        );
        var actual = exp.Execute();
        var expected = VolumeValue.Meter(20);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void MulLengthAndArea()
    {
        var exp = new Mul(
            LengthValue.Meter(10).AsExpression(),
            AreaValue.Meter(2).AsExpression()
        );
        var actual = exp.Execute();
        var expected = VolumeValue.Meter(20);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void MulNumberAndTime()
    {
        var exp = new Mul(
            Number.Two,
            TimeValue.Second(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = TimeValue.Second(20);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void MulTimeAndNumber()
    {
        var exp = new Mul(
            TimeValue.Second(10).AsExpression(),
            Number.Two
        );
        var actual = exp.Execute();
        var expected = TimeValue.Second(20);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void MulNumberAndArea()
    {
        var exp = new Mul(
            Number.Two,
            AreaValue.Meter(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = AreaValue.Meter(20);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void MulAreaAndNumber()
    {
        var exp = new Mul(
            AreaValue.Meter(10).AsExpression(),
            Number.Two
        );
        var actual = exp.Execute();
        var expected = AreaValue.Meter(20);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void MulNumberAndVolume()
    {
        var exp = new Mul(
            Number.Two,
            VolumeValue.Meter(10).AsExpression()
        );
        var actual = exp.Execute();
        var expected = VolumeValue.Meter(20);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void MulVolumeAndNumber()
    {
        var exp = new Mul(
            VolumeValue.Meter(10).AsExpression(),
            Number.Two
        );
        var actual = exp.Execute();
        var expected = VolumeValue.Meter(20);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteRationalAndRational()
    {
        var exp = new Mul(
            new Rational(Number.One, Number.Two),
            new Rational(Number.Two, Number.One)
        );
        var expected = new RationalValue(1, 1);
        var actual = exp.Execute();

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteNumberAndRational()
    {
        var exp = new Mul(
            Number.One,
            new Rational(Number.Two, Number.One)
        );
        var expected = new RationalValue(2, 1);
        var actual = exp.Execute();

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteRationalAndNumber()
    {
        var exp = new Mul(
            new Rational(new Number(3), Number.Two),
            Number.Two
        );
        var expected = new RationalValue(3, 1);
        var actual = exp.Execute();

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteMulBoolByBoolTest()
        => TestNotSupported(new Mul(Bool.True, Bool.True));

    [Test]
    public void CloneTest()
    {
        var exp = new Mul(Variable.X, Number.Zero);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}