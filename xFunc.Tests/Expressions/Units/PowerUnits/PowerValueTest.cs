// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.PowerUnits;

public class PowerValueTest
{
    [Test]
    public void EqualTest()
    {
        var power1 = PowerValue.Watt(10);
        var power2 = PowerValue.Watt(10);

        Assert.That(power1.Equals(power2), Is.True);
    }

    [Test]
    public void EqualOperatorTest()
    {
        var power1 = PowerValue.Watt(10);
        var power2 = PowerValue.Watt(10);

        Assert.That(power1 == power2, Is.True);
    }

    [Test]
    public void NotEqualTest()
    {
        var power1 = PowerValue.Watt(10);
        var power2 = PowerValue.Watt(12);

        Assert.That(power1 != power2, Is.True);
    }

    [Test]
    public void LessTest()
    {
        var power1 = PowerValue.Watt(10);
        var power2 = PowerValue.Watt(12);

        Assert.That(power1 < power2, Is.True);
    }

    [Test]
    public void LessFalseTest()
    {
        var power1 = PowerValue.Watt(20);
        var power2 = PowerValue.Watt(12);

        Assert.That(power1 < power2, Is.False);
    }

    [Test]
    public void GreaterTest()
    {
        var power1 = PowerValue.Watt(20);
        var power2 = PowerValue.Watt(12);

        Assert.That(power1 > power2, Is.True);
    }

    [Test]
    public void GreaterFalseTest()
    {
        var power1 = PowerValue.Watt(10);
        var power2 = PowerValue.Watt(12);

        Assert.That(power1 > power2, Is.False);
    }

    [Test]
    public void LessOrEqualTest()
    {
        var power1 = PowerValue.Watt(10);
        var power2 = PowerValue.Watt(10);

        Assert.That(power1 <= power2, Is.True);
    }

    [Test]
    public void GreaterOrEqualTest()
    {
        var power1 = PowerValue.Watt(10);
        var power2 = PowerValue.Watt(10);

        Assert.That(power1 >= power2, Is.True);
    }

    [Test]
    public void CompareToNull()
    {
        var power = PowerValue.Watt(10);

        Assert.That(power.CompareTo(null) > 0, Is.True);
    }

    [Test]
    public void CompareToObject()
    {
        var power1 = PowerValue.Watt(10);
        var power2 = (object)PowerValue.Watt(10);

        Assert.That(power1.CompareTo(power2) == 0, Is.True);
    }

    [Test]
    public void CompareToDouble()
    {
        var power = PowerValue.Watt(10);

        Assert.Throws<ArgumentException>(() => power.CompareTo(1));
    }

    [Test]
    public void ValueNotEqualTest()
    {
        var power1 = PowerValue.Watt(10);
        var power2 = PowerValue.Watt(12);

        Assert.That(power1.Equals(power2), Is.False);
    }

    [Test]
    public void UnitNotEqualTest2()
    {
        var power1 = PowerValue.Watt(10);
        var power2 = PowerValue.Kilowatt(10);

        Assert.That(power1.Equals(power2), Is.False);
    }

    [Test]
    public void EqualDiffTypeTest()
    {
        var power1 = PowerValue.Watt(10);
        var power2 = 3;

        Assert.That(power1.Equals(power2), Is.False);
    }

    [Test]
    public void EqualObjectTest()
    {
        var power1 = PowerValue.Watt(10);
        var power2 = PowerValue.Watt(10);

        Assert.That(power1.Equals(power2 as object), Is.True);
    }

    [Test]
    public void NotEqualObjectTest()
    {
        var power1 = PowerValue.Watt(10);
        var power2 = PowerValue.Watt(20);

        Assert.That(power1.Equals(power2 as object), Is.False);
    }

    [Test]
    public void ToStringWattTest()
    {
        var power = PowerValue.Watt(10);

        Assert.That(power.ToString(), Is.EqualTo("10 W"));
    }

    [Test]
    public void ToStringKilowattTest()
    {
        var power = PowerValue.Kilowatt(10);

        Assert.That(power.ToString(), Is.EqualTo("10 kW"));
    }

    [Test]
    public void ToStringHorsepowerTest()
    {
        var power = PowerValue.Horsepower(10);

        Assert.That(power.ToString(), Is.EqualTo("10 hp"));
    }

    public static IEnumerable<object[]> GetConversionTestCases()
    {
        yield return new object[] { 10.0, PowerUnit.Watt, PowerUnit.Watt, 10.0 };
        yield return new object[] { 10.0, PowerUnit.Watt, PowerUnit.Kilowatt, 10.0 / 1000 };
        yield return new object[] { 10.0, PowerUnit.Watt, PowerUnit.Horsepower, 10.0 / 745.69987158227022 };
        yield return new object[] { 10.0, PowerUnit.Kilowatt, PowerUnit.Kilowatt, 10.0 };
        yield return new object[] { 10.0, PowerUnit.Kilowatt, PowerUnit.Watt, 10.0 * 1000 };
        yield return new object[] { 10.0, PowerUnit.Kilowatt, PowerUnit.Horsepower, 10.0 * 1000 / 745.69987158227022 };
        yield return new object[] { 10.0, PowerUnit.Horsepower, PowerUnit.Horsepower, 10.0 };
        yield return new object[] { 10.0, PowerUnit.Horsepower, PowerUnit.Watt, 10.0 * 745.69987158227022 };
        yield return new object[] { 10.0, PowerUnit.Horsepower, PowerUnit.Kilowatt, 10.0 * 745.69987158227022 / 1000 };
    }

    [Test]
    [TestCaseSource(nameof(GetConversionTestCases))]
    public void ConversionTests(double value, PowerUnit unit, PowerUnit to, double expected)
    {
        var temperatureValue = new PowerValue(new NumberValue(value), unit);
        var converted = temperatureValue.To(to);

        Assert.That(converted.Value.Number, Is.EqualTo(expected).Within(6));
    }
}