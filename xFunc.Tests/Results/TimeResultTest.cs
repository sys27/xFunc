// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class TimeResultTest
{
    [Test]
    public void TryGetTimeTest()
    {
        var expected = TimeValue.Second(10);
        var areaResult = new Result.TimeResult(expected);
        var result = areaResult.TryGetTime(out var timeValue);

        Assert.That(result, Is.True);
        Assert.That(timeValue, Is.EqualTo(expected));
    }

    [Test]
    public void TryGetTimeFalseTest()
    {
        var areaResult = new Result.NumberResult(NumberValue.One);
        var result = areaResult.TryGetTime(out var timeValue);

        Assert.That(result, Is.False);
        Assert.That(timeValue, Is.Null);
    }

    [Test]
    public void ToStringTest()
    {
        var lengthValue = TimeValue.Second(10);
        var result = new Result.TimeResult(lengthValue);

        Assert.That(result.ToString(), Is.EqualTo("10 's'"));
    }
}