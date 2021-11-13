// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Matrices;

public class TransposeTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteMatrixTest()
    {
        var matrix = new Matrix(new[]
        {
            new Vector(new[] { Number.One, Number.Two }),
            new Vector(new[] { new Number(3), new Number(4) }),
            new Vector(new[] { new Number(5), new Number(6) })
        });

        var expected = new Matrix(new[]
        {
            new Vector(new[] { Number.One, new Number(3), new Number(5) }),
            new Vector(new[] { Number.Two, new Number(4), new Number(6) })
        });
        var exp = new Transpose(matrix);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteVectorTest()
    {
        var vector = new Vector(new[] { Number.One, Number.Two });

        var expected = new Matrix(new[]
        {
            new Vector(new[] { Number.One }),
            new Vector(new[] { Number.Two })
        });
        var exp = new Transpose(vector);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteWrongArgumentTypeTest()
    {
        var exp = new Transpose(Bool.True);

        TestNotSupported(exp);
    }

    [Fact]
    public void CloneTest()
    {
        var exp = new Transpose(new Vector(new[] { Number.One, Number.Two }));
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}