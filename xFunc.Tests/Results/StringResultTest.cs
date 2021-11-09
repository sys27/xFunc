// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class StringResultTest
{
    [Fact]
    public void ResultTest()
    {
        var result = new StringResult("hello");

        Assert.Equal("hello", result.Result);
    }

    [Fact]
    public void IResultTest()
    {
        var result = new StringResult("hello") as IResult;

        Assert.Equal("hello", result.Result);
    }

    [Fact]
    public void NullTest()
    {
        Assert.Throws<ArgumentNullException>(() => new StringResult(null));
    }

    [Fact]
    public void ToStringTest()
    {
        var result = new StringResult("hello");

        Assert.Equal("hello", result.ToString());
    }
}