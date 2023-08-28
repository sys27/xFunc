// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.MassUnits;

public class MassValueTest
{
    [Test]
    public void EqualTest()
    {
        var mass1 = MassValue.Gram(10);
        var mass2 = MassValue.Gram(10);

        Assert.That(mass1.Equals(mass2), Is.True);
    }

    [Test]
    public void EqualOperatorTest()
    {
        var mass1 = MassValue.Gram(10);
        var mass2 = MassValue.Gram(10);

        Assert.That(mass1 == mass2, Is.True);
    }

    [Test]
    public void NotEqualTest()
    {
        var mass1 = MassValue.Gram(10);
        var mass2 = MassValue.Gram(12);

        Assert.That(mass1 != mass2, Is.True);
    }

    [Test]
    public void LessTest()
    {
        var mass1 = MassValue.Gram(10);
        var mass2 = MassValue.Gram(12);

        Assert.That(mass1 < mass2, Is.True);
    }

    [Test]
    public void LessFalseTest()
    {
        var mass1 = MassValue.Gram(20);
        var mass2 = MassValue.Gram(12);

        Assert.That(mass1 < mass2, Is.False);
    }

    [Test]
    public void GreaterTest()
    {
        var mass1 = MassValue.Gram(20);
        var mass2 = MassValue.Gram(12);

        Assert.That(mass1 > mass2, Is.True);
    }

    [Test]
    public void GreaterFalseTest()
    {
        var mass1 = MassValue.Gram(10);
        var mass2 = MassValue.Gram(12);

        Assert.That(mass1 > mass2, Is.False);
    }

    [Test]
    public void LessOrEqualTest()
    {
        var mass1 = MassValue.Gram(10);
        var mass2 = MassValue.Gram(10);

        Assert.That(mass1 <= mass2, Is.True);
    }

    [Test]
    public void GreaterOrEqualTest()
    {
        var mass1 = MassValue.Gram(10);
        var mass2 = MassValue.Gram(10);

        Assert.That(mass1 >= mass2, Is.True);
    }

    [Test]
    public void CompareToNull()
    {
        var mass = MassValue.Gram(10);

        Assert.That(mass.CompareTo(null) > 0, Is.True);
    }

    [Test]
    public void CompareToObject()
    {
        var mass1 = MassValue.Gram(10);
        var mass2 = (object)MassValue.Gram(10);

        Assert.That(mass1.CompareTo(mass2) == 0, Is.True);
    }

    [Test]
    public void CompareToDouble()
    {
        var mass = MassValue.Gram(10);

        Assert.Throws<ArgumentException>(() => mass.CompareTo(1));
    }

    [Test]
    public void ValueNotEqualTest()
    {
        var mass1 = MassValue.Gram(10);
        var mass2 = MassValue.Gram(12);

        Assert.That(mass1.Equals(mass2), Is.False);
    }

    [Test]
    public void UnitNotEqualTest2()
    {
        var mass1 = MassValue.Gram(10);
        var mass2 = MassValue.Kilogram(10);

        Assert.That(mass1.Equals(mass2), Is.False);
    }

    [Test]
    public void EqualDiffTypeTest()
    {
        var mass1 = MassValue.Gram(10);
        var mass2 = 3;

        Assert.That(mass1.Equals(mass2), Is.False);
    }

    [Test]
    public void EqualObjectTest()
    {
        var mass1 = MassValue.Gram(10);
        var mass2 = MassValue.Gram(10);

        Assert.That(mass1.Equals(mass2 as object), Is.True);
    }

    [Test]
    public void NotEqualObjectTest()
    {
        var mass1 = MassValue.Gram(10);
        var mass2 = MassValue.Gram(20);

        Assert.That(mass1.Equals(mass2 as object), Is.False);
    }

    [Test]
    public void ToStringMilligramTest()
    {
        var mass = MassValue.Milligram(10);

        Assert.That(mass.ToString(), Is.EqualTo("10 mg"));
    }

    [Test]
    public void ToStringGramTest()
    {
        var mass = MassValue.Gram(10);

        Assert.That(mass.ToString(), Is.EqualTo("10 g"));
    }

    [Test]
    public void ToStringKilogramTest()
    {
        var mass = MassValue.Kilogram(10);

        Assert.That(mass.ToString(), Is.EqualTo("10 kg"));
    }

    [Test]
    public void ToStringTonneTest()
    {
        var mass = MassValue.Tonne(10);

        Assert.That(mass.ToString(), Is.EqualTo("10 t"));
    }

    [Test]
    public void ToStringOunceTest()
    {
        var mass = MassValue.Ounce(10);

        Assert.That(mass.ToString(), Is.EqualTo("10 oz"));
    }

    [Test]
    public void ToStringPoundTest()
    {
        var mass = MassValue.Pound(10);

        Assert.That(mass.ToString(), Is.EqualTo("10 lb"));
    }

    [Test]
    public void AddOperatorTest()
    {
        var mass1 = MassValue.Gram(1);
        var mass2 = MassValue.Kilogram(1);
        var expected = MassValue.Gram(1001);
        var result = mass1 + mass2;

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void SubOperatorTest()
    {
        var mass1 = MassValue.Kilogram(1);
        var mass2 = MassValue.Gram(1);
        var expected = MassValue.Kilogram(0.999);
        var result = mass1 - mass2;

        Assert.That(result, Is.EqualTo(expected));
    }

    public static IEnumerable<object[]> GetConversionTestCases()
    {
        yield return new object[] { 10.0, MassUnit.Milligram, MassUnit.Milligram, 10.0 };
        yield return new object[] { 1000.0, MassUnit.Milligram, MassUnit.Gram, 1.0 };
        yield return new object[] { 1000000.0, MassUnit.Milligram, MassUnit.Kilogram, 1.0 };
        yield return new object[] { 1000000000.0, MassUnit.Milligram, MassUnit.Tonne, 1.0 };
        yield return new object[] { 1000.0, MassUnit.Milligram, MassUnit.Ounce, 0.0352739619 };
        yield return new object[] { 1000.0, MassUnit.Milligram, MassUnit.Pound, 0.0022046226 };
        yield return new object[] { 10.0, MassUnit.Gram, MassUnit.Gram, 10.0 };
        yield return new object[] { 10.0, MassUnit.Gram, MassUnit.Milligram, 10000.0 };
        yield return new object[] { 10.0, MassUnit.Gram, MassUnit.Kilogram, 0.01 };
        yield return new object[] { 1000.0, MassUnit.Gram, MassUnit.Tonne, 0.001 };
        yield return new object[] { 10.0, MassUnit.Gram, MassUnit.Ounce, 0.35273962 };
        yield return new object[] { 10.0, MassUnit.Gram, MassUnit.Pound, 0.022046226 };
        yield return new object[] { 10.0, MassUnit.Kilogram, MassUnit.Kilogram, 10.0 };
        yield return new object[] { 10.0, MassUnit.Kilogram, MassUnit.Milligram, 10000000.0 };
        yield return new object[] { 10.0, MassUnit.Kilogram, MassUnit.Gram, 10000 };
        yield return new object[] { 10.0, MassUnit.Kilogram, MassUnit.Tonne, 0.01 };
        yield return new object[] { 10.0, MassUnit.Kilogram, MassUnit.Ounce, 352.739619807 };
        yield return new object[] { 10.0, MassUnit.Kilogram, MassUnit.Pound, 22.046226218 };
        yield return new object[] { 10.0, MassUnit.Tonne, MassUnit.Tonne, 10.0 };
        yield return new object[] { 10.0, MassUnit.Tonne, MassUnit.Milligram, 10000000000.0 };
        yield return new object[] { 10.0, MassUnit.Tonne, MassUnit.Gram, 10000000.0 };
        yield return new object[] { 10.0, MassUnit.Tonne, MassUnit.Kilogram, 10000.0 };
        yield return new object[] { 1.0, MassUnit.Tonne, MassUnit.Ounce, 35273.961980687 };
        yield return new object[] { 1.0, MassUnit.Tonne, MassUnit.Pound, 2204.622621849 };
        yield return new object[] { 10.0, MassUnit.Ounce, MassUnit.Ounce, 10.0 };
        yield return new object[] { 10.0, MassUnit.Ounce, MassUnit.Milligram, 283495.231 };
        yield return new object[] { 10.0, MassUnit.Ounce, MassUnit.Gram, 283.495231 };
        yield return new object[] { 10.0, MassUnit.Ounce, MassUnit.Kilogram, 0.283495231 };
        yield return new object[] { 10.0, MassUnit.Ounce, MassUnit.Tonne, 0.000283495 };
        yield return new object[] { 1.0, MassUnit.Ounce, MassUnit.Pound, 0.0625 };
        yield return new object[] { 10.0, MassUnit.Pound, MassUnit.Pound, 10.0 };
        yield return new object[] { 10.0, MassUnit.Pound, MassUnit.Milligram, 4535923.7 };
        yield return new object[] { 10.0, MassUnit.Pound, MassUnit.Gram, 4535.9237 };
        yield return new object[] { 10.0, MassUnit.Pound, MassUnit.Kilogram, 4.5359237 };
        yield return new object[] { 10.0, MassUnit.Pound, MassUnit.Tonne, 0.004535924 };
        yield return new object[] { 10.0, MassUnit.Pound, MassUnit.Ounce, 160.0 };
    }

    [Test]
    [TestCaseSource(nameof(GetConversionTestCases))]
    public void ConversionTests(double value, MassUnit unit, MassUnit to, double expected)
    {
        var mass = new MassValue(new NumberValue(value), unit);
        var converted = mass.To(to);

        Assert.That(converted.Value.Number, Is.EqualTo(expected).Within(6));
    }
}