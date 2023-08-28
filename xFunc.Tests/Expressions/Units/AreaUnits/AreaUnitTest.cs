// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.AreaUnits;

public class AreaUnitTest
{
    [Test]
    public void EqualsNullTest()
    {
        var a = AreaUnit.Meter;

        Assert.That(a.Equals(null), Is.False);
    }

    [Test]
    public void EqualsTest()
    {
        var a = AreaUnit.Meter;
        var b = AreaUnit.Meter;

        Assert.That(a.Equals(b), Is.True);
    }

    [Test]
    public void NotEqualsTest()
    {
        var a = AreaUnit.Meter;
        var b = AreaUnit.Kilometer;

        Assert.That(a.Equals(b), Is.False);
    }

    [Test]
    public void ObjectEqualsNullTest()
    {
        var a = AreaUnit.Meter;

        Assert.That(a.Equals(null as object), Is.False);
    }

    [Test]
    public void ObjectEqualsTest()
    {
        var a = AreaUnit.Meter;
        var b = AreaUnit.Meter as object;

        Assert.That(a.Equals(b), Is.True);
    }

    [Test]
    public void ObjectEqualsWithDifferentTypesTest()
    {
        var a = AreaUnit.Meter;
        var b = 1 as object;

        Assert.That(a.Equals(b), Is.False);
    }

    [Test]
    public void EqualsOperatorTest()
    {
        var a = AreaUnit.Meter;
        var b = AreaUnit.Meter;

        Assert.That(a == b, Is.True);
    }

    [Test]
    public void NotEqualsOperatorTest()
    {
        var a = AreaUnit.Meter;
        var b = AreaUnit.Kilometer;

        Assert.That(a != b, Is.True);
    }

    [Test]
    public void ToStringTest()
    {
        var a = AreaUnit.Meter;

        Assert.That(a.ToString(), Is.EqualTo("m^2"));
    }

    public static IEnumerable<object[]> GetToVolumeUnitTest()
    {
        yield return new object[] { AreaUnit.Meter, VolumeUnit.Meter };
        yield return new object[] { AreaUnit.Centimeter, VolumeUnit.Centimeter };
        yield return new object[] { AreaUnit.Inch, VolumeUnit.Inch };
        yield return new object[] { AreaUnit.Foot, VolumeUnit.Foot };
        yield return new object[] { AreaUnit.Yard, VolumeUnit.Yard };
    }

    [Test]
    [TestCaseSource(nameof(GetToVolumeUnitTest))]
    public void ToVolumeUnitTest(AreaUnit unit, VolumeUnit expected)
    {
        var actual = unit.ToVolumeUnit();

        Assert.That(actual, Is.EqualTo(expected));
    }

    public static IEnumerable<object[]> GetToVolumeUnitExceptionTest()
    {
        yield return new object[] { AreaUnit.Millimeter };
        yield return new object[] { AreaUnit.Kilometer };
        yield return new object[] { AreaUnit.Mile };
        yield return new object[] { AreaUnit.Hectare };
        yield return new object[] { AreaUnit.Acre };
    }

    [Test]
    [TestCaseSource(nameof(GetToVolumeUnitExceptionTest))]
    public void ToVolumeUnitExceptionTest(AreaUnit unit)
        => Assert.Throws<InvalidOperationException>(() => unit.ToVolumeUnit());

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void FromNameEmptyString(string name)
        => Assert.Throws<ArgumentNullException>(() => AreaUnit.FromName(name, out _));
}