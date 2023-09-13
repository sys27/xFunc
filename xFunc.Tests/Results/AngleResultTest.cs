// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class AngleResultTest
{
    [Test]
    public void TryGetAngleTest()
    {
        var expected = AngleValue.Degree(90);
        var angleResult = new Result.AngleResult(expected);
        var result = angleResult.TryGetAngle(out var angleValue);

        Assert.That(result, Is.True);
        Assert.That(angleValue, Is.EqualTo(expected));
    }

    [Test]
    public void TryGetAngleFalseTest()
    {
        var angleResult = new Result.NumberResult(NumberValue.One);
        var result = angleResult.TryGetAngle(out var angleValue);

        Assert.That(result, Is.False);
        Assert.That(angleValue, Is.Null);
    }

    [Test]
    public void ToStringTest()
    {
        var angle = AngleValue.Degree(10);
        var result = new Result.AngleResult(angle);

        Assert.That(result.ToString(), Is.EqualTo("10 'degree'"));
    }
}