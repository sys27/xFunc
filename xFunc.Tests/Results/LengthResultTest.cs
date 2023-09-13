// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class LengthResultTest
{
    [Test]
    public void TryGetLengthTest()
    {
        var expected = LengthValue.Meter(10);
        var areaResult = new Result.LengthResult(expected);
        var result = areaResult.TryGetLength(out var lengthValue);

        Assert.That(result, Is.True);
        Assert.That(lengthValue, Is.EqualTo(expected));
    }

    [Test]
    public void TryGetLengthFalseTest()
    {
        var areaResult = new Result.NumberResult(NumberValue.One);
        var result = areaResult.TryGetLength(out var lengthValue);

        Assert.That(result, Is.False);
        Assert.That(lengthValue, Is.Null);
    }

    [Test]
    public void ToStringTest()
    {
        var lengthValue = LengthValue.Meter(10);
        var result = new Result.LengthResult(lengthValue);

        Assert.That(result.ToString(), Is.EqualTo("10 'm'"));
    }
}