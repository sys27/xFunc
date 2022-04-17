// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class VolumeNumberResultTest
{
    [Fact]
    public void ResultTest()
    {
        var volume = VolumeValue.Meter(10);
        var result = new VolumeNumberResult(volume);

        Assert.Equal(volume, result.Result);
    }

    [Fact]
    public void IResultTest()
    {
        var volume = VolumeValue.Meter(10);
        var result = new VolumeNumberResult(volume) as IResult;

        Assert.Equal(volume, result.Result);
    }

    [Fact]
    public void ToStringTest()
    {
        var volume = VolumeValue.Meter(10);
        var result = new VolumeNumberResult(volume);

        Assert.Equal("10 m^3", result.ToString());
    }
}