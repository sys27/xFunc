// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class RationalResultTest
{
    [Test]
    public void TryGetRationalTest()
    {
        var expected = new RationalValue(1, 2);
        var areaResult = new Result.RationalResult(expected);
        var result = areaResult.TryGetRational(out var rationalValue);

        Assert.That(result, Is.True);
        Assert.That(rationalValue, Is.EqualTo(expected));
    }

    [Test]
    public void TryGetRationalFalseTest()
    {
        var areaResult = new Result.NumberResult(NumberValue.One);
        var result = areaResult.TryGetRational(out var rationalValue);

        Assert.That(result, Is.False);
        Assert.That(rationalValue, Is.Null);
    }

    [Test]
    public void ToStringTest()
    {
        var angle = new RationalValue(1, 2);
        var result = new Result.RationalResult(angle);

        Assert.That(result.ToString(), Is.EqualTo("1 // 2"));
    }
}