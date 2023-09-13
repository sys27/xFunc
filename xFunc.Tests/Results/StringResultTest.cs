// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class StringResultTest
{
    [Test]
    public void TryGetStringTest()
    {
        var expected = "hello";
        var areaResult = new Result.StringResult(expected);
        var result = areaResult.TryGetString(out var stringValue);

        Assert.That(result, Is.True);
        Assert.That(stringValue, Is.EqualTo(expected));
    }

    [Test]
    public void TryGetStringFalseTest()
    {
        var areaResult = new Result.NumberResult(NumberValue.One);
        var result = areaResult.TryGetString(out var stringValue);

        Assert.That(result, Is.False);
        Assert.That(stringValue, Is.Null);
    }

    [Test]
    public void NullTest()
        => Assert.Throws<ArgumentNullException>(() => new Result.StringResult(null!));

    [Test]
    public void ToStringTest()
    {
        var result = new Result.StringResult("hello");

        Assert.That(result.ToString(), Is.EqualTo("hello"));
    }
}