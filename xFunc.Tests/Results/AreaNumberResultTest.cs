// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class AreaNumberResultTest
{
    [Fact]
    public void ResultTest()
    {
        var areaValue = AreaValue.Meter(10);
        var result = new AreaNumberResult(areaValue);

        Assert.Equal(areaValue, result.Result);
    }

    [Fact]
    public void IResultTest()
    {
        var areaValue = AreaValue.Meter(10);
        var result = new AreaNumberResult(areaValue) as IResult;

        Assert.Equal(areaValue, result.Result);
    }

    [Fact]
    public void ToStringTest()
    {
        var areaValue = AreaValue.Meter(10);
        var result = new AreaNumberResult(areaValue);

        Assert.Equal("10 m^2", result.ToString());
    }
}