// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.LengthUnits;

public class LengthUnitTest
{
    [Test]
    public void EqualsTest()
    {
        var a = LengthUnit.Meter;
        var b = LengthUnit.Meter;

        Assert.That(a.Equals(b), Is.True);
    }

    [Test]
    public void NotEqualsTest()
    {
        var a = LengthUnit.Meter;
        var b = LengthUnit.Millimeter;

        Assert.That(a.Equals(b), Is.False);
    }

    [Test]
    public void ObjectEqualsTest()
    {
        var a = LengthUnit.Meter;
        var b = LengthUnit.Meter as object;

        Assert.That(a.Equals(b), Is.True);
    }

    [Test]
    public void ObjectEqualsWithDifferentTypesTest()
    {
        var a = LengthUnit.Meter;
        var b = 1 as object;

        Assert.That(a.Equals(b), Is.False);
    }

    [Test]
    public void EqualsOperatorTest()
    {
        var a = LengthUnit.Meter;
        var b = LengthUnit.Meter;

        Assert.That(a == b, Is.True);
    }

    [Test]
    public void NotEqualsOperatorTest()
    {
        var a = LengthUnit.Meter;
        var b = LengthUnit.Millimeter;

        Assert.That(a != b, Is.True);
    }

    [Test]
    public void ToStringTest()
    {
        var a = LengthUnit.Meter;

        Assert.That(a.ToString(), Is.EqualTo("m"));
    }

    public static IEnumerable<object[]> GetToAreaUnitTest()
    {
        yield return [LengthUnit.Meter, AreaUnit.Meter];
        yield return [LengthUnit.Millimeter, AreaUnit.Millimeter];
        yield return [LengthUnit.Centimeter, AreaUnit.Centimeter];
        yield return [LengthUnit.Kilometer, AreaUnit.Kilometer];
        yield return [LengthUnit.Inch, AreaUnit.Inch];
        yield return [LengthUnit.Foot, AreaUnit.Foot];
        yield return [LengthUnit.Yard, AreaUnit.Yard];
        yield return [LengthUnit.Mile, AreaUnit.Mile];
    }

    [Test]
    [TestCaseSource(nameof(GetToAreaUnitTest))]
    public void ToAreaUnitTest(LengthUnit unit, AreaUnit expected)
    {
        var actual = unit.ToAreaUnit();

        Assert.That(actual, Is.EqualTo(expected));
    }

    public static IEnumerable<object[]> GetToAreaUnitExceptionTest()
    {
        yield return [LengthUnit.Nanometer];
        yield return [LengthUnit.Micrometer];
        yield return [LengthUnit.Decimeter];
        yield return [LengthUnit.NauticalMile];
        yield return [LengthUnit.Chain];
        yield return [LengthUnit.Rod];
        yield return [LengthUnit.AstronomicalUnit];
        yield return [LengthUnit.LightYear];
        yield return [LengthUnit.Parsec];
    }

    [Test]
    [TestCaseSource(nameof(GetToAreaUnitExceptionTest))]
    public void ToAreaUnitExceptionTest(LengthUnit unit)
        => Assert.Throws<InvalidOperationException>(() => unit.ToAreaUnit());

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void FromNameEmptyString(string name)
        => Assert.Throws<ArgumentNullException>(() => LengthUnit.FromName(name, out _));
}