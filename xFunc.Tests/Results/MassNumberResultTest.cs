// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class MassNumberResultTest
{
    [Fact]
    public void ResultTest()
    {
        var power = MassValue.Gram(10);
        var result = new MassNumberResult(power);

        Assert.Equal(power, result.Result);
    }

    [Fact]
    public void IResultTest()
    {
        var power = MassValue.Gram(10);
        var result = new MassNumberResult(power) as IResult;

        Assert.Equal(power, result.Result);
    }

    [Fact]
    public void ToStringTest()
    {
        var power = MassValue.Gram(10);
        var result = new MassNumberResult(power);

        Assert.Equal("10 g", result.ToString());
    }
}