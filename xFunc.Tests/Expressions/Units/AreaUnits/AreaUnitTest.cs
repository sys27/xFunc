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
        yield return [AreaUnit.Meter, VolumeUnit.Meter];
        yield return [AreaUnit.Centimeter, VolumeUnit.Centimeter];
        yield return [AreaUnit.Inch, VolumeUnit.Inch];
        yield return [AreaUnit.Foot, VolumeUnit.Foot];
        yield return [AreaUnit.Yard, VolumeUnit.Yard];
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
        yield return [AreaUnit.Millimeter];
        yield return [AreaUnit.Kilometer];
        yield return [AreaUnit.Mile];
        yield return [AreaUnit.Hectare];
        yield return [AreaUnit.Acre];
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