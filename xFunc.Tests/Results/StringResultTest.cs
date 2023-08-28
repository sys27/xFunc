// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class StringResultTest
{
    [Test]
    public void ResultTest()
    {
        var result = new StringResult("hello");

        Assert.That(result.Result, Is.EqualTo("hello"));
    }

    [Test]
    public void IResultTest()
    {
        var result = new StringResult("hello") as IResult;

        Assert.That(result.Result, Is.EqualTo("hello"));
    }

    [Test]
    public void NullTest()
        => Assert.Throws<ArgumentNullException>(() => new StringResult(null));

    [Test]
    public void ToStringTest()
    {
        var result = new StringResult("hello");

        Assert.That(result.ToString(), Is.EqualTo("hello"));
    }
}