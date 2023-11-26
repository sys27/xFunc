// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;

namespace xFunc.Tests.Expressions.Matrices;

public class MatrixTest
{
    [Test]
    public void CtorNullTest()
        => Assert.Throws<ArgumentNullException>(() => new Matrix(new ImmutableArray<Vector>()));

    [Test]
    public void ExecuteTest()
    {
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(3) }),
            new Vector(new IExpression[] { new Number(-1) })
        });

        var result = matrix.Execute();
        var expected = MatrixValue.Create(new NumberValue[][]
        {
            new NumberValue[] { new NumberValue(3) },
            new NumberValue[] { new NumberValue(-1) },
        });

        Assert.That(expected, Is.EqualTo(result));
    }

    [Test]
    public void MulByNumberMatrixTest()
    {
        var vector1 = new Vector(new IExpression[] { Number.Two, new Number(3) });
        var vector2 = new Vector(new IExpression[] { new Number(9), new Number(5) });
        var matrix = new Matrix(new[] { vector1, vector2 });
        var number = new Number(5);
        var exp = new Mul(matrix, number);

        var expected = MatrixValue.Create(new NumberValue[][]
        {
            new NumberValue[] { new NumberValue(10), new NumberValue(15) },
            new NumberValue[] { new NumberValue(45), new NumberValue(25) },
        });
        var result = exp.Execute();

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void AddMatricesTest()
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
        var exp = new Add(matrix1, matrix2);

        var expected = MatrixValue.Create(new NumberValue[][]
        {
            new NumberValue[] { new NumberValue(15), new NumberValue(5) },
            new NumberValue[] { new NumberValue(6), new NumberValue(4) },
        });
        var result = exp.Execute();

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void AddMatricesDiffSizeTest1()
    {
        Assert.Throws<InvalidMatrixException>(() =>
            new Matrix(new[]
            {
                new Vector(new IExpression[] { new Number(9), Number.Two }),
                new Vector(new IExpression[] { new Number(4), new Number(3), new Number(9) })
            }));
    }

    [Test]
    public void AddMatricesDiffSizeTest2()
    {
        var matrix1 = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(6), new Number(3) }),
            new Vector(new IExpression[] { Number.Two, Number.One })
        });
        var matrix2 = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(9), Number.Two }),
            new Vector(new IExpression[] { new Number(4), new Number(3) }),
            new Vector(new IExpression[] { Number.One, new Number(7) })
        });
        var exp = new Add(matrix1, matrix2);

        Assert.Throws<ArgumentException>(() => exp.Execute());
    }

    [Test]
    public void SubMatricesTest()
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
        var exp = new Sub(matrix1, matrix2);

        var expected = MatrixValue.Create(new NumberValue[][]
        {
            new NumberValue[] { new NumberValue(-3), new NumberValue(1) },
            new NumberValue[] { new NumberValue(-2), new NumberValue(-2) },
        });
        var result = exp.Execute();

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void SubMatricesDiffSizeTest1()
    {
        Assert.Throws<InvalidMatrixException>(() =>
            new Matrix(new[]
            {
                new Vector(new IExpression[] { new Number(9), Number.Two }),
                new Vector(new IExpression[] { new Number(4), new Number(3), new Number(9) })
            }));
    }

    [Test]
    public void SubMatricesDiffSizeTest2()
    {
        var matrix1 = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(6), new Number(3) }),
            new Vector(new IExpression[] { Number.Two, Number.One })
        });
        var matrix2 = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(9), Number.Two }),
            new Vector(new IExpression[] { new Number(4), new Number(3) }),
            new Vector(new IExpression[] { new Number(6), Number.One })
        });
        var exp = new Sub(matrix1, matrix2);

        Assert.Throws<ArgumentException>(() => exp.Execute());
    }

    [Test]
    public void TransposeMatrixTest()
    {
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { Number.One, Number.Two }),
            new Vector(new IExpression[] { new Number(3), new Number(4) }),
            new Vector(new IExpression[] { new Number(5), new Number(6) })
        });
        var exp = new Transpose(matrix);

        var expected = MatrixValue.Create(new NumberValue[][]
        {
            new NumberValue[] { NumberValue.One, new NumberValue(3), new NumberValue(5) },
            new NumberValue[] { NumberValue.Two, new NumberValue(4), new NumberValue(6) },
        });
        var result = exp.Execute();

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void MulMatrices1()
    {
        var left = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(-2), Number.One }),
            new Vector(new IExpression[] { new Number(5), new Number(4) })
        });
        var right = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(3) }),
            new Vector(new IExpression[] { new Number(-1) })
        });
        var exp = new Mul(left, right);

        var expected = MatrixValue.Create(new NumberValue[][]
        {
            new NumberValue[] { new NumberValue(-7) },
            new NumberValue[] { new NumberValue(11) },
        });
        var result = exp.Execute();

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void MulMatrices2()
    {
        var left = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(5), new Number(8), new Number(-4) }),
            new Vector(new IExpression[] { new Number(6), new Number(9), new Number(-5) }),
            new Vector(new IExpression[] { new Number(4), new Number(7), new Number(-3) })
        });
        var right = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(3), Number.Two, new Number(5) }),
            new Vector(new IExpression[] { new Number(4), new Number(-1), new Number(3) }),
            new Vector(new IExpression[] { new Number(9), new Number(6), new Number(5) })
        });
        var exp = new Mul(left, right);

        var expected = MatrixValue.Create(new NumberValue[][]
        {
            new NumberValue[] { new NumberValue(11), new NumberValue(-22), new NumberValue(29) },
            new NumberValue[] { new NumberValue(9), new NumberValue(-27), new NumberValue(32) },
            new NumberValue[] { new NumberValue(13), new NumberValue(-17), new NumberValue(26) },
        });
        var result = exp.Execute();

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void MulMatrices3()
    {
        var left = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(3) }),
            new Vector(new IExpression[] { new Number(-1) })
        });
        var right = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(-2), Number.One }),
            new Vector(new IExpression[] { new Number(5), new Number(4) })
        });
        var exp = new Mul(left, right);

        Assert.Throws<ArgumentException>(() => exp.Execute());
    }

    [Test]
    public void MulMatrices4()
    {
        var left = new Matrix(new[]
        {
            new Vector(new IExpression[] { Number.One }),
            new Vector(new IExpression[] { Number.Two }),
            new Vector(new IExpression[] { new Number(3) }),
            new Vector(new IExpression[] { new Number(4) }),
            new Vector(new IExpression[] { new Number(5) }),
            new Vector(new IExpression[] { new Number(6) }),
            new Vector(new IExpression[] { new Number(7) }),
            new Vector(new IExpression[] { new Number(8) }),
            new Vector(new IExpression[] { new Number(9) }),
            new Vector(new IExpression[] { new Number(10) }),
            new Vector(new IExpression[] { new Number(11) }),
        });
        var right = new Matrix(new[]
        {
            new Vector(new IExpression[]
            {
                Number.One,
                Number.Two,
                new Number(3),
                new Number(4),
                new Number(5),
                new Number(6),
                new Number(7),
                new Number(8),
                new Number(9),
                new Number(10),
                new Number(11),
            }),
        });
        var exp = new Mul(left, right);
        var expected = MatrixValue.Create(new NumberValue[][]
        {
            new NumberValue[]
            {
                NumberValue.One, NumberValue.Two, new NumberValue(3), new NumberValue(4), new NumberValue(5), new NumberValue(6), new NumberValue(7), new NumberValue(8), new NumberValue(9), new NumberValue(10), new NumberValue(11),
            },
            new NumberValue[]
            {
                NumberValue.Two, new NumberValue(4), new NumberValue(6), new NumberValue(8), new NumberValue(10), new NumberValue(12), new NumberValue(14), new NumberValue(16), new NumberValue(18), new NumberValue(20), new NumberValue(22),
            },
            new NumberValue[]
            {
                new NumberValue(3), new NumberValue(6), new NumberValue(9), new NumberValue(12), new NumberValue(15), new NumberValue(18), new NumberValue(21), new NumberValue(24), new NumberValue(27), new NumberValue(30), new NumberValue(33),
            },
            new NumberValue[]
            {
                new NumberValue(4), new NumberValue(8), new NumberValue(12), new NumberValue(16), new NumberValue(20), new NumberValue(24), new NumberValue(28), new NumberValue(32), new NumberValue(36), new NumberValue(40), new NumberValue(44),
            },
            new NumberValue[]
            {
                new NumberValue(5), new NumberValue(10), new NumberValue(15), new NumberValue(20), new NumberValue(25), new NumberValue(30), new NumberValue(35), new NumberValue(40), new NumberValue(45), new NumberValue(50), new NumberValue(55),
            },
            new NumberValue[]
            {
                new NumberValue(6), new NumberValue(12), new NumberValue(18), new NumberValue(24), new NumberValue(30), new NumberValue(36), new NumberValue(42), new NumberValue(48), new NumberValue(54), new NumberValue(60), new NumberValue(66),
            },
            new NumberValue[]
            {
                new NumberValue(7), new NumberValue(14), new NumberValue(21), new NumberValue(28), new NumberValue(35), new NumberValue(42), new NumberValue(49), new NumberValue(56), new NumberValue(63), new NumberValue(70), new NumberValue(77),
            },
            new NumberValue[]
            {
                new NumberValue(8), new NumberValue(16), new NumberValue(24), new NumberValue(32), new NumberValue(40), new NumberValue(48), new NumberValue(56), new NumberValue(64), new NumberValue(72), new NumberValue(80), new NumberValue(88),
            },
            new NumberValue[]
            {
                new NumberValue(9), new NumberValue(18), new NumberValue(27), new NumberValue(36), new NumberValue(45), new NumberValue(54), new NumberValue(63), new NumberValue(72), new NumberValue(81), new NumberValue(90), new NumberValue(99),
            },
            new NumberValue[]
            {
                new NumberValue(10), new NumberValue(20), new NumberValue(30), new NumberValue(40), new NumberValue(50), new NumberValue(60), new NumberValue(70), new NumberValue(80), new NumberValue(90), new NumberValue(100), new NumberValue(110),
            },
            new NumberValue[]
            {
                new NumberValue(11), new NumberValue(22), new NumberValue(33), new NumberValue(44), new NumberValue(55), new NumberValue(66), new NumberValue(77), new NumberValue(88), new NumberValue(99), new NumberValue(110), new NumberValue(121),
            },
        });

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void MatrixMulVectorTest()
    {
        var vector = new Vector(new IExpression[] { new Number(-2), Number.One });
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(3) }),
            new Vector(new IExpression[] { new Number(-1) })
        });
        var exp = new Mul(matrix, vector);

        var expected = MatrixValue.Create(new NumberValue[][]
        {
            new NumberValue[] { new NumberValue(-6), new NumberValue(3) },
            new NumberValue[] { new NumberValue(2), new NumberValue(-1) },
        });
        var result = exp.Execute();

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void DetTest()
    {
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { Number.One, new Number(-2), new Number(3) }),
            new Vector(new IExpression[] { new Number(4), Number.Zero, new Number(6) }),
            new Vector(new IExpression[] { new Number(-7), new Number(8), new Number(9) })
        });
        var exp = new Determinant(matrix);

        var det = exp.Execute();
        var expected = new NumberValue(204.0);

        Assert.That(det, Is.EqualTo(expected));
    }

    [Test]
    public void InverseTest()
    {
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(3), new Number(7), Number.Two, new Number(5) }),
            new Vector(new IExpression[] { Number.One, new Number(8), new Number(4), Number.Two }),
            new Vector(new IExpression[] { Number.Two, Number.One, new Number(9), new Number(3) }),
            new Vector(new IExpression[] { new Number(5), new Number(4), new Number(7), Number.One })
        });
        var exp = new Inverse(matrix);

        var expected = MatrixValue.Create(new[]
        {
            new NumberValue[] { new NumberValue(0.0970873786407767), new NumberValue(-0.18270079435128), new NumberValue(-0.114739629302736), new NumberValue(0.224183583406884) },
            new NumberValue[] { new NumberValue(-0.0194174757281553), new NumberValue(0.145631067961165), new NumberValue(-0.0679611650485437), new NumberValue(0.00970873786407767) },
            new NumberValue[] { new NumberValue(-0.087378640776699), new NumberValue(0.0644307149161518), new NumberValue(0.103265666372463), new NumberValue(-0.00176522506619595) },
            new NumberValue[] { new NumberValue(0.203883495145631), new NumberValue(-0.120035304501324), new NumberValue(0.122683142100618), new NumberValue(-0.147396293027361) },
        });

        var actual = exp.Execute();

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void CloneTest()
    {
        var exp = new Matrix(new[]
        {
            new Vector(new IExpression[] { Number.One, new Number(-2), new Number(3) }),
            new Vector(new IExpression[] { new Number(4), Number.Zero, new Number(6) }),
            new Vector(new IExpression[] { new Number(-7), new Number(8), new Number(9) })
        });
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }

    [Test]
    public void NullVectorArrayTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Matrix(null));
    }

    [Test]
    public void NullTest()
    {
        Assert.Throws<ArgumentException>(() => new Matrix(new Vector[0]));
    }

    [Test]
    public void NullVectorElementTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Matrix(new[]
        {
            new Vector(new IExpression[] { null, null }),
            new Vector(new IExpression[] { null, null }),
        }));
    }

    [Test]
    public void MatrixAnalyzeNull()
    {
        var exp = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(3) }),
            new Vector(new IExpression[] { new Number(-1) })
        });

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Test]
    public void MatrixAnalyzeNull2()
    {
        var exp = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(3) }),
            new Vector(new IExpression[] { new Number(-1) })
        });

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }

    [Test]
    public void EqualsTest()
    {
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(3) }),
            new Vector(new IExpression[] { new Number(-1) })
        });

        Assert.That(matrix.Equals(matrix));
    }

    [Test]
    public void EqualsNullTest()
    {
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(3) }),
            new Vector(new IExpression[] { new Number(-1) })
        });

        Assert.That(matrix.Equals(null), Is.False);
    }

    [Test]
    public void EqualsDiffTypeTest()
    {
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(3) }),
            new Vector(new IExpression[] { new Number(-1) })
        });
        var number = Number.Two;

        Assert.That(matrix.Equals(number), Is.False);
    }

    [Test]
    public void EqualsDiffCountTest()
    {
        var matrix1 = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(3) }),
            new Vector(new IExpression[] { new Number(-1) })
        });
        var matrix2 = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(3) }),
        });

        Assert.That(matrix1.Equals(matrix2), Is.False);
    }
}