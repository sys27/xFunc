// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class AngleNumberResultTest
{
    [Fact]
    public void ResultTest()
    {
        var angle = AngleValue.Degree(10);
        var result = new AngleNumberResult(angle);

        Assert.Equal(angle, result.Result);
    }

    [Fact]
    public void IResultTest()
    {
        var angle = AngleValue.Degree(10);
        var result = new AngleNumberResult(angle) as IResult;

        Assert.Equal(angle, result.Result);
    }

    [Fact]
    public void ToStringTest()
    {
        var angle = AngleValue.Degree(10);
        var result = new AngleNumberResult(angle);

        Assert.Equal("10 degree", result.ToString());
    }
}