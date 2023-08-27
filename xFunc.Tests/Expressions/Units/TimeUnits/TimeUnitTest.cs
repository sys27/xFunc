// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.TimeUnits;

public class TimeUnitTest
{
    [Test]
    public void EqualsTest()
    {
        var a = TimeUnit.Second;
        var b = TimeUnit.Second;

        Assert.That(a.Equals(b), Is.True);
    }

    [Test]
    public void NotEqualsTest()
    {
        var a = TimeUnit.Second;
        var b = LengthUnit.Millimeter;

        Assert.That(a.Equals(b), Is.False);
    }

    [Test]
    public void ObjectEqualsTest()
    {
        var a = TimeUnit.Second;
        var b = TimeUnit.Second as object;

        Assert.That(a.Equals(b), Is.True);
    }

    [Test]
    public void ObjectEqualsWithDifferentTypesTest()
    {
        var a = TimeUnit.Second;
        var b = 1 as object;

        Assert.That(a.Equals(b), Is.False);
    }

    [Test]
    public void EqualsOperatorTest()
    {
        var a = TimeUnit.Second;
        var b = TimeUnit.Second;

        Assert.That(a == b, Is.True);
    }

    [Test]
    public void NotEqualsOperatorTest()
    {
        var a = TimeUnit.Second;
        var b = TimeUnit.Millisecond;

        Assert.That(a != b, Is.True);
    }

    [Test]
    public void ToStringTest()
    {
        var a = TimeUnit.Second;

        Assert.That(a.ToString(), Is.EqualTo("s"));
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void FromNameEmptyString(string name)
        => Assert.Throws<ArgumentNullException>(() => TimeUnit.FromName(name, out _));
}