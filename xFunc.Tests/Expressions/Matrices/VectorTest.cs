// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Matrices;

public class VectorTest : BaseExpressionTests
{
    [Fact]
    public void NullArgumentsTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Vector(null));
    }

    [Fact]
    public void EmptyArgumentsTest()
    {
        Assert.Throws<ArgumentException>(() => new Vector(new IExpression[0]));
    }

    [Fact]
    public void NullTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Vector(new IExpression[] { null, null }));
    }

    [Fact]
    public void VectorArgumentIsNotNumber()
    {
        var exp = new Vector(new IExpression[] { Bool.False });

        TestNotSupported(exp);
    }

    [Fact]
    public void MulByNumberVectorTest()
    {
        var vector = new Vector(new[] { Number.Two, new Number(3) });
        var number = new Number(5);
        var exp = new Mul(vector, number);

        var expected = new Vector(new[] { new Number(10), new Number(15) });
        var result = exp.Execute();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void AddVectorsTest()
    {
        var vector1 = new Vector(new[] { Number.Two, new Number(3) });
        var vector2 = new Vector(new[] { new Number(7), Number.One });
        var exp = new Add(vector1, vector2);

        var expected = new Vector(new[] { new Number(9), new Number(4) });
        var result = exp.Execute();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void VectorWithAddTest()
    {
        var vector = new Vector(new[] { new Add(Number.Two, new Number(3)) });
        var expected = new Vector(new[] { new Number(5) });
        var result = vector.Execute();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void AddVectorsDiffSizeTest()
    {
        var vector1 = new Vector(new[] { Number.Two, new Number(3) });
        var vector2 = new Vector(new[] { new Number(7), Number.One, new Number(3) });
        var exp = new Add(vector1, vector2);

        Assert.Throws<ArgumentException>(() => exp.Execute());
    }

    [Fact]
    public void SubVectorsTest()
    {
        var vector1 = new Vector(new[] { Number.Two, new Number(3) });
        var vector2 = new Vector(new[] { new Number(7), Number.One });
        var exp = new Sub(vector1, vector2);

        var expected = new Vector(new[] { new Number(-5), Number.Two });
        var result = exp.Execute();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void SubVectorsDiffSizeTest()
    {
        var vector1 = new Vector(new[] { Number.Two, new Number(3) });
        var vector2 = new Vector(new[] { new Number(7), Number.One, new Number(3) });
        var exp = new Sub(vector1, vector2);

        Assert.Throws<ArgumentException>(() => exp.Execute());
    }

    [Fact]
    public void TransposeVectorTest()
    {
        var vector = new Vector(new[] { Number.One, Number.Two });
        var exp = new Transpose(vector);

        var expected = new Matrix(new[]
        {
            new Vector(new[] { Number.One }),
            new Vector(new[] { Number.Two })
        });
        var result = exp.Execute();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void VectorMulMatrixTest()
    {
        var vector = new Vector(new[] { new Number(-2), Number.One });
        var matrix = new Matrix(new[]
        {
            new Vector(new[] { new Number(3) }),
            new Vector(new[] { new Number(-1) })
        });
        var exp = new Mul(vector, matrix);

        var expected = new Matrix(new[] { new Vector(new[] { new Number(-7) }) });
        var result = exp.Execute();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void MultiOpMulAdd()
    {
        // ({1, 2, 3} * 4) + {2, 3, 4}
        var vector1 = new Vector(new[] { Number.One, Number.Two, new Number(3) });
        var vector2 = new Vector(new[] { Number.Two, new Number(3), new Number(4) });
        var mul = new Mul(vector1, new Number(4));
        var add = new Add(mul, vector2);

        var expected = new Vector(new[] { new Number(6), new Number(11), new Number(16) });
        var result = add.Execute();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void MultiOpMulSub()
    {
        // ({1, 2, 3} * 4) - {2, 3, 4}
        var vector1 = new Vector(new[] { Number.One, Number.Two, new Number(3) });
        var vector2 = new Vector(new[] { Number.Two, new Number(3), new Number(4) });
        var mul = new Mul(vector1, new Number(4));
        var sub = new Sub(mul, vector2);

        var expected = new Vector(new[] { Number.Two, new Number(5), new Number(8) });
        var result = sub.Execute();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void MultiOpSubMul()
    {
        // ({2, 3, 4} - {1, 2, 3}) * 4
        var vector1 = new Vector(new[] { Number.One, Number.Two, new Number(3) });
        var vector2 = new Vector(new[] { Number.Two, new Number(3), new Number(4) });
        var sub = new Sub(vector2, vector1);
        var mul = new Mul(sub, new Number(4));

        var expected = new Vector(new[] { new Number(4), new Number(4), new Number(4) });
        var result = mul.Execute();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void MultiOpAddMul()
    {
        // ({2, 3, 4} + {1, 2, 3}) * 4
        var vector1 = new Vector(new[] { Number.One, Number.Two, new Number(3) });
        var vector2 = new Vector(new[] { Number.Two, new Number(3), new Number(4) });
        var add = new Add(vector2, vector1);
        var mul = new Mul(add, new Number(4));

        var expected = new Vector(new[] { new Number(12), new Number(20), new Number(28) });
        var result = mul.Execute();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void MultiOpMulMul()
    {
        // ({1, 2, 3} * 2) * 4
        var vector = new Vector(new[] { Number.One, Number.Two, new Number(3) });
        var mul1 = new Mul(vector, Number.Two);
        var mul2 = new Mul(mul1, new Number(4));

        var expected = new Vector(new[] { new Number(8), new Number(16), new Number(24) });
        var result = mul2.Execute();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void CloneTest()
    {
        var exp = new Vector(new[] { Number.One, Number.Two, new Number(3) });
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}