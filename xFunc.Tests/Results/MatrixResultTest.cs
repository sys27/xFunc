// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class MatrixResultTest
{
    [Test]
    public void TryGetMatrixTest()
    {
        var expected = MatrixValue.Create(NumberValue.One);
        var areaResult = new Result.MatrixResult(expected);
        var result = areaResult.TryGetMatrix(out var matrixValue);

        Assert.That(result, Is.True);
        Assert.That(matrixValue, Is.EqualTo(expected));
    }

    [Test]
    public void TryGetMatrixFalseTest()
    {
        var areaResult = new Result.NumberResult(NumberValue.One);
        var result = areaResult.TryGetMatrix(out var matrixValue);

        Assert.That(result, Is.False);
        Assert.That(matrixValue, Is.Null);
    }

    [Test]
    public void ToStringTest()
    {
        var matrixValue = MatrixValue.Create(new NumberValue[][]
        {
            new NumberValue[] { NumberValue.One, NumberValue.Two },
            new NumberValue[] { NumberValue.Two, NumberValue.One },
        });
        var result = new Result.MatrixResult(matrixValue);

        Assert.That(result.ToString(), Is.EqualTo("{{1, 2}, {2, 1}}"));
    }
}