// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class VolumeResultTest
{
    [Test]
    public void TryGetVolumeTest()
    {
        var expected = VolumeValue.Meter(10);
        var areaResult = new Result.VolumeResult(expected);
        var result = areaResult.TryGetVolume(out var volumeValue);

        Assert.That(result, Is.True);
        Assert.That(volumeValue, Is.EqualTo(expected));
    }

    [Test]
    public void TryGetVolumeFalseTest()
    {
        var areaResult = new Result.NumberResult(NumberValue.One);
        var result = areaResult.TryGetVolume(out var volumeValue);

        Assert.That(result, Is.False);
        Assert.That(volumeValue, Is.Null);
    }

    [Test]
    public void ToStringTest()
    {
        var volume = VolumeValue.Meter(10);
        var result = new Result.VolumeResult(volume);

        Assert.That(result.ToString(), Is.EqualTo("10 'm^3'"));
    }
}