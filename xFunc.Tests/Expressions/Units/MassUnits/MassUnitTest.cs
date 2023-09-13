// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.MassUnits;

public class MassUnitTest
{
    [Test]
    public void EqualsTest()
    {
        var a = MassUnit.Kilogram;
        var b = MassUnit.Kilogram;

        Assert.That(a.Equals(b), Is.True);
    }

    [Test]
    public void NotEqualsTest()
    {
        var a = MassUnit.Kilogram;
        var b = MassUnit.Gram;

        Assert.That(a.Equals(b), Is.False);
    }

    [Test]
    public void ObjectEqualsTest()
    {
        var a = MassUnit.Kilogram;
        var b = MassUnit.Kilogram as object;

        Assert.That(a.Equals(b), Is.True);
    }

    [Test]
    public void ObjectEqualsWithDifferentTypesTest()
    {
        var a = MassUnit.Kilogram;
        var b = 1 as object;

        Assert.That(a.Equals(b), Is.False);
    }

    [Test]
    public void EqualsOperatorTest()
    {
        var a = MassUnit.Kilogram;
        var b = MassUnit.Kilogram;

        Assert.That(a == b, Is.True);
    }

    [Test]
    public void NotEqualsOperatorTest()
    {
        var a = MassUnit.Kilogram;
        var b = MassUnit.Gram;

        Assert.That(a != b, Is.True);
    }

    [Test]
    public void ToStringTest()
    {
        var a = MassUnit.Kilogram;

        Assert.That(a.ToString(), Is.EqualTo("kg"));
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void FromNameEmptyString(string name)
        => Assert.Throws<ArgumentNullException>(() => MassUnit.FromName(name, out _));
}