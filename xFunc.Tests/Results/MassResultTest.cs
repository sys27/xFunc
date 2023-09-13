// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class MassResultTest
{
    [Test]
    public void TryGetMassTest()
    {
        var expected = MassValue.Gram(10);
        var areaResult = new Result.MassResult(expected);
        var result = areaResult.TryGetMass(out var massValue);

        Assert.That(result, Is.True);
        Assert.That(massValue, Is.EqualTo(expected));
    }

    [Test]
    public void TryGetMassFalseTest()
    {
        var areaResult = new Result.NumberResult(NumberValue.One);
        var result = areaResult.TryGetMass(out var areaValue);

        Assert.That(result, Is.False);
        Assert.That(areaValue, Is.Null);
    }

    [Test]
    public void ToStringTest()
    {
        var power = MassValue.Gram(10);
        var result = new Result.MassResult(power);

        Assert.That(result.ToString(), Is.EqualTo("10 'g'"));
    }
}