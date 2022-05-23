// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class LengthNumberResultTest
{
    [Fact]
    public void ResultTest()
    {
        var lengthValue = LengthValue.Meter(10);
        var result = new LengthNumberResult(lengthValue);

        Assert.Equal(lengthValue, result.Result);
    }

    [Fact]
    public void IResultTest()
    {
        var lengthValue = LengthValue.Meter(10);
        var result = new LengthNumberResult(lengthValue) as IResult;

        Assert.Equal(lengthValue, result.Result);
    }

    [Fact]
    public void ToStringTest()
    {
        var lengthValue = LengthValue.Meter(10);
        var result = new LengthNumberResult(lengthValue);

        Assert.Equal("10 m", result.ToString());
    }
}