// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class NumberResultTest
{
    [Fact]
    public void ResultTest()
    {
        var result = new NumberResult(10.2);

        Assert.Equal(10.2, result.Result);
    }

    [Fact]
    public void IResultTest()
    {
        var result = new NumberResult(10.2) as IResult;

        Assert.Equal(10.2, result.Result);
    }

    [Fact]
    public void ToStringTest()
    {
        var result = new NumberResult(10.2);

        Assert.Equal("10.2", result.ToString());
    }
}