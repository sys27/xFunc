// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class LambdaResultTest
{
    [Fact]
    public void ResultTest()
    {
        var lambda = new Lambda(new[] { "x" }, Variable.X);
        var result = new LambdaResult(lambda);

        Assert.Equal(lambda, result.Result);
    }

    [Fact]
    public void IResultTest()
    {
        var lambda = new Lambda(new[] { "x" }, Variable.X);
        var result = new LambdaResult(lambda) as IResult;

        Assert.Equal(lambda, result.Result);
    }

    [Fact]
    public void ToStringTest()
    {
        var lambda = new Lambda(new[] { "x" }, Variable.X);
        var result = new LambdaResult(lambda);

        Assert.Equal("(x) => x", result.ToString());
    }
}