// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class VolumeNumberResultTest
{
    [Test]
    public void ResultTest()
    {
        var volume = VolumeValue.Meter(10);
        var result = new VolumeNumberResult(volume);

        Assert.That(result.Result, Is.EqualTo(volume));
    }

    [Test]
    public void IResultTest()
    {
        var volume = VolumeValue.Meter(10);
        var result = new VolumeNumberResult(volume) as IResult;

        Assert.That(result.Result, Is.EqualTo(volume));
    }

    [Test]
    public void ToStringTest()
    {
        var volume = VolumeValue.Meter(10);
        var result = new VolumeNumberResult(volume);

        Assert.That(result.ToString(), Is.EqualTo("10 'm^3'"));
    }
}