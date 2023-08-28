// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class MassNumberResultTest
{
    [Test]
    public void ResultTest()
    {
        var power = MassValue.Gram(10);
        var result = new MassNumberResult(power);

        Assert.That(result.Result, Is.EqualTo(power));
    }

    [Test]
    public void IResultTest()
    {
        var power = MassValue.Gram(10);
        var result = new MassNumberResult(power) as IResult;

        Assert.That(result.Result, Is.EqualTo(power));
    }

    [Test]
    public void ToStringTest()
    {
        var power = MassValue.Gram(10);
        var result = new MassNumberResult(power);

        Assert.That(result.ToString(), Is.EqualTo("10 g"));
    }
}