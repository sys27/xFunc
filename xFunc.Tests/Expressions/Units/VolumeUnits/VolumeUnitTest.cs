// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.VolumeUnits;

public class VolumeUnitTest
{
    [Test]
    public void EqualsNullTest()
    {
        var a = VolumeUnit.Meter;

        Assert.That(a.Equals(null), Is.False);
    }

    [Test]
    public void EqualsTest()
    {
        var a = VolumeUnit.Meter;
        var b = VolumeUnit.Meter;

        Assert.That(a.Equals(b), Is.True);
    }

    [Test]
    public void NotEqualsTest()
    {
        var a = VolumeUnit.Meter;
        var b = VolumeUnit.Centimeter;

        Assert.That(a.Equals(b), Is.False);
    }

    [Test]
    public void ObjectEqualsNullTest()
    {
        var a = VolumeUnit.Meter;

        Assert.That(a.Equals(null as object), Is.False);
    }

    [Test]
    public void ObjectEqualsTest()
    {
        var a = VolumeUnit.Meter;
        var b = VolumeUnit.Meter as object;

        Assert.That(a.Equals(b), Is.True);
    }

    [Test]
    public void ObjectEqualsWithDifferentTypesTest()
    {
        var a = VolumeUnit.Meter;
        var b = 1 as object;

        Assert.That(a.Equals(b), Is.False);
    }

    [Test]
    public void EqualsOperatorTest()
    {
        var a = VolumeUnit.Meter;
        var b = VolumeUnit.Meter;

        Assert.That(a == b, Is.True);
    }

    [Test]
    public void NotEqualsOperatorTest()
    {
        var a = VolumeUnit.Meter;
        var b = VolumeUnit.Centimeter;

        Assert.That(a != b, Is.True);
    }

    [Test]
    public void ToStringTest()
    {
        var a = VolumeUnit.Meter;

        Assert.That(a.ToString(), Is.EqualTo("m^3"));
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void FromNameEmptyString(string name)
        => Assert.Throws<ArgumentNullException>(() => VolumeUnit.FromName(name, out _));
}