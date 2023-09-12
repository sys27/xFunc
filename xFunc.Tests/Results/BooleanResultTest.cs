// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class BooleanResultTest
{
    [Test]
    public void TryGetBooleanTest()
    {
        var areaResult = new Result.BooleanResult(true);
        var result = areaResult.TryGetBoolean(out var boolValue);

        Assert.That(result, Is.True);
        Assert.That(boolValue, Is.True);
    }

    [Test]
    public void TryGetBoolFalseTest()
    {
        var areaResult = new Result.NumberResult(NumberValue.One);
        var result = areaResult.TryGetBoolean(out var boolValue);

        Assert.That(result, Is.False);
        Assert.That(boolValue, Is.Null);
    }

    [Test]
    public void ToStringTest()
    {
        var result = new Result.BooleanResult(true);

        Assert.That(result.ToString(), Is.EqualTo("True"));
    }
}