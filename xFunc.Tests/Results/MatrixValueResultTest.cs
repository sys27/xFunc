// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class MatrixValueResultTest
{
    [Fact]
    public void ResultTest()
    {
        var matrixValue = MatrixValue.Create(new NumberValue[][]
        {
            new NumberValue[] { NumberValue.One, NumberValue.Two },
            new NumberValue[] { NumberValue.Two, NumberValue.One },
        });
        var result = new MatrixValueResult(matrixValue);

        Assert.Equal(matrixValue, result.Result);
    }

    [Fact]
    public void IResultTest()
    {
        var matrixValue = MatrixValue.Create(new NumberValue[][]
        {
            new NumberValue[] { NumberValue.One, NumberValue.Two },
            new NumberValue[] { NumberValue.Two, NumberValue.One },
        });
        var result = new MatrixValueResult(matrixValue) as IResult;

        Assert.Equal(matrixValue, result.Result);
    }

    [Fact]
    public void ToStringTest()
    {
        var matrixValue = MatrixValue.Create(new NumberValue[][]
        {
            new NumberValue[] { NumberValue.One, NumberValue.Two },
            new NumberValue[] { NumberValue.Two, NumberValue.One },
        });
        var result = new MatrixValueResult(matrixValue);

        Assert.Equal("{{1, 2}, {2, 1}}", result.ToString());
    }
}