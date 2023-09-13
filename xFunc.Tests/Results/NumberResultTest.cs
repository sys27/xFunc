// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class NumberResultTest
{
    [Test]
    public void TryGetNumberTest()
    {
        var expected = NumberValue.One;
        var areaResult = new Result.NumberResult(expected);
        var result = areaResult.TryGetNumber(out var numberValue);

        Assert.That(result, Is.True);
        Assert.That(numberValue, Is.EqualTo(expected));
    }

    [Test]
    public void TryGetNumberFalseTest()
    {
        var areaResult = new Result.AngleResult(AngleValue.Degree(10));
        var result = areaResult.TryGetNumber(out var numberValue);

        Assert.That(result, Is.False);
        Assert.That(numberValue, Is.Null);
    }

    [Test]
    public void ToStringTest()
    {
        var result = new Result.NumberResult(10.2);

        Assert.That(result.ToString(), Is.EqualTo("10.2"));
    }
}