// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class AngleNumberResultTest
{
    [Test]
    public void ResultTest()
    {
        var angle = AngleValue.Degree(10);
        var result = new AngleNumberResult(angle);

        Assert.That(result.Result, Is.EqualTo(angle));
    }

    [Test]
    public void IResultTest()
    {
        var angle = AngleValue.Degree(10);
        var result = new AngleNumberResult(angle) as IResult;

        Assert.That(result.Result, Is.EqualTo(angle));
    }

    [Test]
    public void ToStringTest()
    {
        var angle = AngleValue.Degree(10);
        var result = new AngleNumberResult(angle);

        Assert.That(result.ToString(), Is.EqualTo("10 'degree'"));
    }
}