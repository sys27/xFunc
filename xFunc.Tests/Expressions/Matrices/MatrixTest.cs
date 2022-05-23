// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Matrices;

public class MatrixTest
{
    [Fact]
    public void MulByNumberMatrixTest()
    {
        var vector1 = new Vector(new IExpression[] { Number.Two, new Number(3) });
        var vector2 = new Vector(new IExpression[] { new Number(9), new Number(5) });
        var matrix = new Matrix(new[] { vector1, vector2 });
        var number = new Number(5);
        var exp = new Mul(matrix, number);

        var expected = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(10), new Number(15) }),
            new Vector(new IExpression[] { new Number(45), new Number(25) })
        });
        var result = exp.Execute();

        Assert.Equal(expected, result);
    }

    [Fact]
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

        var expected = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(15), new Number(5) }),
            new Vector(new IExpression[] { new Number(6), new Number(4) })
        });
        var result = exp.Execute();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void AddMatricesDiffSizeTest1()
    {
        Assert.Throws<MatrixIsInvalidException>(() =>
            new Matrix(new[]
            {
                new Vector(new IExpression[] { new Number(9), Number.Two }),
                new Vector(new IExpression[] { new Number(4), new Number(3), new Number(9) })
            }));
    }

    [Fact]
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

    [Fact]
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

        var expected = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(-3), Number.One }),
            new Vector(new IExpression[] { new Number(-2), new Number(-2) })
        });
        var result = exp.Execute();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void SubMatricesDiffSizeTest1()
    {
        Assert.Throws<MatrixIsInvalidException>(() =>
            new Matrix(new[]
            {
                new Vector(new IExpression[] { new Number(9), Number.Two }),
                new Vector(new IExpression[] { new Number(4), new Number(3), new Number(9) })
            }));
    }

    [Fact]
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

    [Fact]
    public void TransposeMatrixTest()
    {
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { Number.One, Number.Two }),
            new Vector(new IExpression[] { new Number(3), new Number(4) }),
            new Vector(new IExpression[] { new Number(5), new Number(6) })
        });
        var exp = new Transpose(matrix);

        var expected = new Matrix(new[]
        {
            new Vector(new IExpression[] { Number.One, new Number(3), new Number(5) }),
            new Vector(new IExpression[] { Number.Two, new Number(4), new Number(6) })
        });
        var result = exp.Execute();

        Assert.Equal(expected, result);
    }

    [Fact]
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

        var expected = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(-7) }),
            new Vector(new IExpression[] { new Number(11) })
        });
        var result = exp.Execute();

        Assert.Equal(expected, result);
    }

    [Fact]
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

        var expected = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(11), new Number(-22), new Number(29) }),
            new Vector(new IExpression[] { new Number(9), new Number(-27), new Number(32) }),
            new Vector(new IExpression[] { new Number(13), new Number(-17), new Number(26) })
        });
        var result = exp.Execute();

        Assert.Equal(expected, result);
    }

    [Fact]
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

    [Fact]
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
        var expected = new Matrix(new[]
        {
            new Vector(new IExpression[]
            {
                Number.One, Number.Two, new Number(3), new Number(4), new Number(5), new Number(6), new Number(7), new Number(8), new Number(9), new Number(10), new Number(11),
            }),
            new Vector(new IExpression[]
            {
                Number.Two, new Number(4), new Number(6), new Number(8), new Number(10), new Number(12), new Number(14), new Number(16), new Number(18), new Number(20), new Number(22),
            }),
            new Vector(new IExpression[]
            {
                new Number(3), new Number(6), new Number(9), new Number(12), new Number(15), new Number(18), new Number(21), new Number(24), new Number(27), new Number(30), new Number(33),
            }),
            new Vector(new IExpression[]
            {
                new Number(4), new Number(8), new Number(12), new Number(16), new Number(20), new Number(24), new Number(28), new Number(32), new Number(36), new Number(40), new Number(44),
            }),
            new Vector(new IExpression[]
            {
                new Number(5), new Number(10), new Number(15), new Number(20), new Number(25), new Number(30), new Number(35), new Number(40), new Number(45), new Number(50), new Number(55),
            }),
            new Vector(new IExpression[]
            {
                new Number(6), new Number(12), new Number(18), new Number(24), new Number(30), new Number(36), new Number(42), new Number(48), new Number(54), new Number(60), new Number(66),
            }),
            new Vector(new IExpression[]
            {
                new Number(7), new Number(14), new Number(21), new Number(28), new Number(35), new Number(42), new Number(49), new Number(56), new Number(63), new Number(70), new Number(77),
            }),
            new Vector(new IExpression[]
            {
                new Number(8), new Number(16), new Number(24), new Number(32), new Number(40), new Number(48), new Number(56), new Number(64), new Number(72), new Number(80), new Number(88),
            }),
            new Vector(new IExpression[]
            {
                new Number(9), new Number(18), new Number(27), new Number(36), new Number(45), new Number(54), new Number(63), new Number(72), new Number(81), new Number(90), new Number(99),
            }),
            new Vector(new IExpression[]
            {
                new Number(10), new Number(20), new Number(30), new Number(40), new Number(50), new Number(60), new Number(70), new Number(80), new Number(90), new Number(100), new Number(110),
            }),
            new Vector(new IExpression[]
            {
                new Number(11), new Number(22), new Number(33), new Number(44), new Number(55), new Number(66), new Number(77), new Number(88), new Number(99), new Number(110), new Number(121),
            }),
        });

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void MatrixMulVectorTest()
    {
        var vector = new Vector(new IExpression[] { new Number(-2), Number.One });
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(3) }),
            new Vector(new IExpression[] { new Number(-1) })
        });
        var exp = new Mul(matrix, vector);

        var expected = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(-6), new Number(3) }),
            new Vector(new IExpression[] { Number.Two, new Number(-1) })
        });
        var result = exp.Execute();

        Assert.Equal(expected, result);
    }

    [Fact]
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

        Assert.Equal(expected, det);
    }

    [Fact]
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

        var expected = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(0.0970873786407767), new Number(-0.18270079435128), new Number(-0.114739629302736), new Number(0.224183583406884) }),
            new Vector(new IExpression[] { new Number(-0.0194174757281553), new Number(0.145631067961165), new Number(-0.0679611650485437), new Number(0.00970873786407767) }),
            new Vector(new IExpression[] { new Number(-0.087378640776699), new Number(0.0644307149161518), new Number(0.103265666372463), new Number(-0.00176522506619595) }),
            new Vector(new IExpression[] { new Number(0.203883495145631), new Number(-0.120035304501324), new Number(0.122683142100618), new Number(-0.147396293027361) })
        });

        var actual = exp.Execute();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CloneTest()
    {
        var exp = new Matrix(new[]
        {
            new Vector(new IExpression[] { Number.One, new Number(-2), new Number(3) }),
            new Vector(new IExpression[] { new Number(4), Number.Zero, new Number(6) }),
            new Vector(new IExpression[] { new Number(-7), new Number(8), new Number(9) })
        });
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }

    [Fact]
    public void NullVectorArrayTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Matrix(null));
    }

    [Fact]
    public void NullTest()
    {
        Assert.Throws<ArgumentException>(() => new Matrix(new Vector[0]));
    }

    [Fact]
    public void NullVectorElementTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Matrix(new[]
        {
            new Vector(new IExpression[] { null, null }),
            new Vector(new IExpression[] { null, null }),
        }));
    }

    [Fact]
    public void ExecuteTest()
    {
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(3) }),
            new Vector(new IExpression[] { new Number(-1) })
        });

        var result = matrix.Execute();

        Assert.True(matrix.Equals(result));
    }

    [Fact]
    public void MatrixAnalyzeNull()
    {
        var exp = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(3) }),
            new Vector(new IExpression[] { new Number(-1) })
        });

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Fact]
    public void MatrixAnalyzeNull2()
    {
        var exp = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(3) }),
            new Vector(new IExpression[] { new Number(-1) })
        });

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }

    [Fact]
    public void EqualsTest()
    {
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(3) }),
            new Vector(new IExpression[] { new Number(-1) })
        });

        Assert.True(matrix.Equals(matrix));
    }

    [Fact]
    public void EqualsNullTest()
    {
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(3) }),
            new Vector(new IExpression[] { new Number(-1) })
        });

        Assert.False(matrix.Equals(null));
    }

    [Fact]
    public void EqualsDiffTypeTest()
    {
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(3) }),
            new Vector(new IExpression[] { new Number(-1) })
        });
        var number = Number.Two;

        Assert.False(matrix.Equals(number));
    }

    [Fact]
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

        Assert.False(matrix1.Equals(matrix2));
    }
}