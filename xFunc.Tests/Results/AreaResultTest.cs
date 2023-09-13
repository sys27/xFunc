// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class AreaResultTest
{
    [Test]
    public void TryGetAreaTest()
    {
        var expected = AreaValue.Kilometer(10);
        var areaResult = new Result.AreaResult(expected);
        var result = areaResult.TryGetArea(out var areaValue);

        Assert.That(result, Is.True);
        Assert.That(areaValue, Is.EqualTo(expected));
    }

    [Test]
    public void TryGetAreaFalseTest()
    {
        var areaResult = new Result.NumberResult(NumberValue.One);
        var result = areaResult.TryGetArea(out var areaValue);

        Assert.That(result, Is.False);
        Assert.That(areaValue, Is.Null);
    }

    [Test]
    public void ToStringTest()
    {
        var areaValue = AreaValue.Meter(10);
        var result = new Result.AreaResult(areaValue);

        Assert.That(result.ToString(), Is.EqualTo("10 'm^2'"));
    }
}