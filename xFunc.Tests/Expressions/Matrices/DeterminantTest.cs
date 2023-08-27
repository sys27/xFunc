// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Matrices;

public class DeterminantTest
{
    [Test]
    public void ExecuteSingleTest()
    {
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { Number.One })
        });

        var det = new Determinant(matrix);
        var expected = new NumberValue(1.0);

        Assert.That(det.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteDoubleTest()
    {
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { Number.One, Number.Two }),
            new Vector(new IExpression[] { new Number(3), new Number(4) }),
        });

        var det = new Determinant(matrix);
        var expected = new NumberValue(-2.0);

        Assert.That(det.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteTest()
    {
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { Number.One, new Number(-2), new Number(3) }),
            new Vector(new IExpression[] { new Number(4), Number.Zero, new Number(6) }),
            new Vector(new IExpression[] { new Number(-7), new Number(8), new Number(9) })
        });

        var det = new Determinant(matrix);
        var expected = new NumberValue(204.0);

        Assert.That(det.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteNegativeTest()
    {
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { new Number(1), new Number(-10), new Number(3), }),
            new Vector(new IExpression[] { new Number(4), new Number(5), new Number(6), }),
            new Vector(new IExpression[] { new Number(7), new Number(8), new Number(9), }),
        });

        var det = new Determinant(matrix);
        var expected = new NumberValue(-72.00000000000004);

        Assert.That(det.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteIsNotSquareTest()
    {
        var matrix = new Matrix(new[]
        {
            new Vector(new IExpression[] { Number.One, new Number(-2), new Number(3) }),
            new Vector(new IExpression[] { new Number(4), Number.Zero, new Number(6) })
        });

        var det = new Determinant(matrix);

        Assert.Throws<ArgumentException>(() => det.Execute());
    }

    [Test]
    public void ExecuteVectorTest()
    {
        var vector = new Vector(new IExpression[] { Number.One, new Number(-2), new Number(3) });
        var det = new Determinant(vector);

        Assert.Throws<ResultIsNotSupportedException>(() => det.Execute());
    }

    [Test]
    public void CloneTest()
    {
        var exp = new Determinant(new Matrix(new[]
        {
            new Vector(new IExpression[] { Number.One, new Number(-2), new Number(3) }),
            new Vector(new IExpression[] { new Number(4), Number.Zero, new Number(6) }),
            new Vector(new IExpression[] { new Number(-7), new Number(8), new Number(9) })
        }));
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}