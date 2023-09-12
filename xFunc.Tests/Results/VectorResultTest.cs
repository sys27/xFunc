// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class VectorResultTest
{
    [Test]
    public void TryGetVectorTest()
    {
        var expected = VectorValue.Create(new NumberValue(1));
        var areaResult = new Result.VectorResult(expected);
        var result = areaResult.TryGetVector(out var vectorValue);

        Assert.That(result, Is.True);
        Assert.That(vectorValue, Is.EqualTo(expected));
    }

    [Test]
    public void TryGetVectorFalseTest()
    {
        var areaResult = new Result.NumberResult(NumberValue.One);
        var result = areaResult.TryGetVector(out var areaValue);

        Assert.That(result, Is.False);
        Assert.That(areaValue, Is.Null);
    }

    [Test]
    public void ToStringTest()
    {
        var vectorValue = VectorValue.Create(NumberValue.One, NumberValue.Two);
        var result = new Result.VectorResult(vectorValue);

        Assert.That(result.ToString(), Is.EqualTo("{1, 2}"));
    }
}