// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class TemperatureResultTest
{
    [Test]
    public void TryGetTemperatureTest()
    {
        var expected = TemperatureValue.Celsius(10);
        var areaResult = new Result.TemperatureResult(expected);
        var result = areaResult.TryGetTemperature(out var temperatureValue);

        Assert.That(result, Is.True);
        Assert.That(temperatureValue, Is.EqualTo(expected));
    }

    [Test]
    public void TryGetTemperatureFalseTest()
    {
        var areaResult = new Result.NumberResult(NumberValue.One);
        var result = areaResult.TryGetTemperature(out var temperatureValue);

        Assert.That(result, Is.False);
        Assert.That(temperatureValue, Is.Null);
    }

    [Test]
    public void ToStringTest()
    {
        var power = TemperatureValue.Celsius(10);
        var result = new Result.TemperatureResult(power);

        Assert.That(result.ToString(), Is.EqualTo("10 '°C'"));
    }
}