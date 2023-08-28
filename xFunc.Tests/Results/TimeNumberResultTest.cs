// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class TimeNumberResultTest
{
    [Test]
    public void ResultTest()
    {
        var lengthValue = TimeValue.Second(10);
        var result = new TimeNumberResult(lengthValue);

        Assert.That(result.Result, Is.EqualTo(lengthValue));
    }

    [Test]
    public void IResultTest()
    {
        var lengthValue = TimeValue.Second(10);
        var result = new TimeNumberResult(lengthValue) as IResult;

        Assert.That(result.Result, Is.EqualTo(lengthValue));
    }

    [Test]
    public void ToStringTest()
    {
        var lengthValue = TimeValue.Second(10);
        var result = new TimeNumberResult(lengthValue);

        Assert.That(result.ToString(), Is.EqualTo("10 s"));
    }
}