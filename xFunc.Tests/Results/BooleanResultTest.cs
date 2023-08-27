// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class BooleanResultTest
{
    [Test]
    public void ResultTest()
    {
        var result = new BooleanResult(true);

        Assert.True(result.Result);
    }

    [Test]
    public void IResultTest()
    {
        var result = new BooleanResult(true) as IResult;

        Assert.True((bool) result.Result);
    }

    [Test]
    public void ToStringTest()
    {
        var result = new BooleanResult(true);

        Assert.That(result.ToString(), Is.EqualTo("True"));
    }
}