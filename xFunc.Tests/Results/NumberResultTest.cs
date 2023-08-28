// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class NumberResultTest
{
    [Test]
    public void ResultTest()
    {
        var result = new NumberResult(10.2);

        Assert.That(result.Result, Is.EqualTo(10.2));
    }

    [Test]
    public void IResultTest()
    {
        var result = new NumberResult(10.2) as IResult;

        Assert.That(result.Result, Is.EqualTo(10.2));
    }

    [Test]
    public void ToStringTest()
    {
        var result = new NumberResult(10.2);

        Assert.That(result.ToString(), Is.EqualTo("10.2"));
    }
}