// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class TemperatureNumberResultTest
{
    [Fact]
    public void ResultTest()
    {
        var power = TemperatureValue.Celsius(10);
        var result = new TemperatureNumberResult(power);

        Assert.Equal(power, result.Result);
    }

    [Fact]
    public void IResultTest()
    {
        var power = TemperatureValue.Celsius(10);
        var result = new TemperatureNumberResult(power) as IResult;

        Assert.Equal(power, result.Result);
    }

    [Fact]
    public void ToStringTest()
    {
        var power = TemperatureValue.Celsius(10);
        var result = new TemperatureNumberResult(power);

        Assert.Equal("10 °C", result.ToString());
    }
}