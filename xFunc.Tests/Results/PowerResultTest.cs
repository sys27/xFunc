// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class PowerResultTest
{
    [Test]
    public void TryGetPowerTest()
    {
        var expected = PowerValue.Watt(10);
        var areaResult = new Result.PowerResult(expected);
        var result = areaResult.TryGetPower(out var powerValue);

        Assert.That(result, Is.True);
        Assert.That(powerValue, Is.EqualTo(expected));
    }

    [Test]
    public void TryGetPowerFalseTest()
    {
        var areaResult = new Result.NumberResult(NumberValue.One);
        var result = areaResult.TryGetPower(out var powerValue);

        Assert.That(result, Is.False);
        Assert.That(powerValue, Is.Null);
    }

    [Test]
    public void ToStringTest()
    {
        var power = PowerValue.Watt(10);
        var result = new Result.PowerResult(power);

        Assert.That(result.ToString(), Is.EqualTo("10 'W'"));
    }
}