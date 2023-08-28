// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class LambdaResultTest
{
    [Test]
    public void ResultTest()
    {
        var lambda = new Lambda(new[] { "x" }, Variable.X);
        var result = new LambdaResult(lambda);

        Assert.That(result.Result, Is.EqualTo(lambda));
    }

    [Test]
    public void IResultTest()
    {
        var lambda = new Lambda(new[] { "x" }, Variable.X);
        var result = new LambdaResult(lambda) as IResult;

        Assert.That(result.Result, Is.EqualTo(lambda));
    }

    [Test]
    public void ToStringTest()
    {
        var lambda = new Lambda(new[] { "x" }, Variable.X);
        var result = new LambdaResult(lambda);

        Assert.That(result.ToString(), Is.EqualTo("(x) => x"));
    }
}