// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class TimeNumberResultTest
{
    [Fact]
    public void ResultTest()
    {
        var lengthValue = TimeValue.Second(10);
        var result = new TimeNumberResult(lengthValue);

        Assert.Equal(lengthValue, result.Result);
    }

    [Fact]
    public void IResultTest()
    {
        var lengthValue = TimeValue.Second(10);
        var result = new TimeNumberResult(lengthValue) as IResult;

        Assert.Equal(lengthValue, result.Result);
    }

    [Fact]
    public void ToStringTest()
    {
        var lengthValue = TimeValue.Second(10);
        var result = new TimeNumberResult(lengthValue);

        Assert.Equal("10 s", result.ToString());
    }
}