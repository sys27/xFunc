// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class PowerNumberResultTest
{
    [Fact]
    public void ResultTest()
    {
        var power = PowerValue.Watt(10);
        var result = new PowerNumberResult(power);

        Assert.Equal(power, result.Result);
    }

    [Fact]
    public void IResultTest()
    {
        var power = PowerValue.Watt(10);
        var result = new PowerNumberResult(power) as IResult;

        Assert.Equal(power, result.Result);
    }

    [Fact]
    public void ToStringTest()
    {
        var power = PowerValue.Watt(10);
        var result = new PowerNumberResult(power);

        Assert.Equal("10 W", result.ToString());
    }
}