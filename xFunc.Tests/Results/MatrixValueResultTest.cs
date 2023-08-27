// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class MatrixValueResultTest
{
    [Test]
    public void ResultTest()
    {
        var matrixValue = MatrixValue.Create(new NumberValue[][]
        {
            new NumberValue[] { NumberValue.One, NumberValue.Two },
            new NumberValue[] { NumberValue.Two, NumberValue.One },
        });
        var result = new MatrixValueResult(matrixValue);

        Assert.That(result.Result, Is.EqualTo(matrixValue));
    }

    [Test]
    public void IResultTest()
    {
        var matrixValue = MatrixValue.Create(new NumberValue[][]
        {
            new NumberValue[] { NumberValue.One, NumberValue.Two },
            new NumberValue[] { NumberValue.Two, NumberValue.One },
        });
        var result = new MatrixValueResult(matrixValue) as IResult;

        Assert.That(result.Result, Is.EqualTo(matrixValue));
    }

    [Test]
    public void ToStringTest()
    {
        var matrixValue = MatrixValue.Create(new NumberValue[][]
        {
            new NumberValue[] { NumberValue.One, NumberValue.Two },
            new NumberValue[] { NumberValue.Two, NumberValue.One },
        });
        var result = new MatrixValueResult(matrixValue);

        Assert.That(result.ToString(), Is.EqualTo("{{1, 2}, {2, 1}}"));
    }
}