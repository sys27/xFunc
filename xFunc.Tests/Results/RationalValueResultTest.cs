// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class RationalValueResultTest
{
    [Test]
    public void ResultTest()
    {
        var angle = new RationalValue(1, 2);
        var result = new RationalValueResult(angle);

        Assert.That(result.Result, Is.EqualTo(angle));
    }

    [Test]
    public void IResultTest()
    {
        var angle = new RationalValue(1, 2);
        var result = new RationalValueResult(angle) as IResult;

        Assert.That(result.Result, Is.EqualTo(angle));
    }

    [Test]
    public void ToStringTest()
    {
        var angle = new RationalValue(1, 2);
        var result = new RationalValueResult(angle);

        Assert.That(result.ToString(), Is.EqualTo("1 // 2"));
    }
}