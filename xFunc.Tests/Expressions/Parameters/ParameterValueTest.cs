// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Parameters;

public class ParameterValueTest
{
    [Test]
    public void EqualNullTest()
    {
        var value = new ParameterValue(1);

        Assert.That(value.Equals(null as object), Is.False);
    }

    [Test]
    public void EqualObjectTest()
    {
        var x = new ParameterValue(1);
        var y = new ParameterValue(1) as object;

        Assert.That(x.Equals(y), Is.True);
    }

    [Test]
    public void EqualOperatorTest()
    {
        var x = new ParameterValue(1);
        var y = new ParameterValue(1);

        Assert.That(x == y, Is.True);
    }

    [Test]
    public void NotEqualOperatorTest()
    {
        var x = new ParameterValue(1);
        var y = new ParameterValue(2);

        Assert.That(x != y, Is.True);
    }
}