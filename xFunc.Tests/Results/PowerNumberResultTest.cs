// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class PowerNumberResultTest
{
    [Test]
    public void ResultTest()
    {
        var power = PowerValue.Watt(10);
        var result = new PowerNumberResult(power);

        Assert.That(result.Result, Is.EqualTo(power));
    }

    [Test]
    public void IResultTest()
    {
        var power = PowerValue.Watt(10);
        var result = new PowerNumberResult(power) as IResult;

        Assert.That(result.Result, Is.EqualTo(power));
    }

    [Test]
    public void ToStringTest()
    {
        var power = PowerValue.Watt(10);
        var result = new PowerNumberResult(power);

        Assert.That(result.ToString(), Is.EqualTo("10 'W'"));
    }
}