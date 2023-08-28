// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class AreaNumberResultTest
{
    [Test]
    public void ResultTest()
    {
        var areaValue = AreaValue.Meter(10);
        var result = new AreaNumberResult(areaValue);

        Assert.That(result.Result, Is.EqualTo(areaValue));
    }

    [Test]
    public void IResultTest()
    {
        var areaValue = AreaValue.Meter(10);
        var result = new AreaNumberResult(areaValue) as IResult;

        Assert.That(result.Result, Is.EqualTo(areaValue));
    }

    [Test]
    public void ToStringTest()
    {
        var areaValue = AreaValue.Meter(10);
        var result = new AreaNumberResult(areaValue);

        Assert.That(result.ToString(), Is.EqualTo("10 'm^2'"));
    }
}