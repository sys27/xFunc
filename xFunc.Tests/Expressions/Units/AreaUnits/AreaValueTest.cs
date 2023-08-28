// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.AreaUnits;

public class AreaValueTest
{
    [Test]
    public void EqualTest()
    {
        var area1 = AreaValue.Meter(10);
        var area2 = AreaValue.Meter(10);

        Assert.That(area1.Equals(area2), Is.True);
    }

    [Test]
    public void EqualOperatorTest()
    {
        var area1 = AreaValue.Meter(10);
        var area2 = AreaValue.Meter(10);

        Assert.That(area1 == area2, Is.True);
    }

    [Test]
    public void NotEqualTest()
    {
        var area1 = AreaValue.Meter(10);
        var area2 = AreaValue.Meter(12);

        Assert.That(area1 != area2, Is.True);
    }

    [Test]
    public void LessTest()
    {
        var area1 = AreaValue.Meter(10);
        var area2 = AreaValue.Meter(12);

        Assert.That(area1 < area2, Is.True);
    }

    [Test]
    public void LessFalseTest()
    {
        var area1 = AreaValue.Meter(20);
        var area2 = AreaValue.Meter(12);

        Assert.That(area1 < area2, Is.False);
    }

    [Test]
    public void GreaterTest()
    {
        var area1 = AreaValue.Meter(20);
        var area2 = AreaValue.Meter(12);

        Assert.That(area1 > area2, Is.True);
    }

    [Test]
    public void GreaterFalseTest()
    {
        var area1 = AreaValue.Meter(10);
        var area2 = AreaValue.Meter(12);

        Assert.That(area1 > area2, Is.False);
    }

    [Test]
    public void LessOrEqualTest()
    {
        var area1 = AreaValue.Meter(10);
        var area2 = AreaValue.Meter(10);

        Assert.That(area1 <= area2, Is.True);
    }

    [Test]
    public void GreaterOrEqualTest()
    {
        var area1 = AreaValue.Meter(10);
        var area2 = AreaValue.Meter(10);

        Assert.That(area1 >= area2, Is.True);
    }

    [Test]
    public void CompareToNull()
    {
        var areaValue = AreaValue.Meter(10);

        Assert.That(areaValue.CompareTo(null) > 0, Is.True);
    }

    [Test]
    public void CompareToObject()
    {
        var area1 = AreaValue.Meter(10);
        var area2 = (object)AreaValue.Meter(10);

        Assert.That(area1.CompareTo(area2) == 0, Is.True);
    }

    [Test]
    public void CompareToDouble()
    {
        var areaValue = AreaValue.Meter(10);

        Assert.Throws<ArgumentException>(() => areaValue.CompareTo(1));
    }

    [Test]
    public void ValueNotEqualTest()
    {
        var area1 = AreaValue.Meter(10);
        var area2 = AreaValue.Meter(12);

        Assert.That(area1.Equals(area2), Is.False);
    }

    [Test]
    public void UnitNotEqualTest2()
    {
        var area1 = AreaValue.Meter(10);
        var area2 = AreaValue.Kilometer(10);

        Assert.That(area1.Equals(area2), Is.False);
    }

    [Test]
    public void EqualDiffTypeTest()
    {
        var area1 = AreaValue.Meter(10);
        var area2 = 3;

        Assert.That(area1.Equals(area2), Is.False);
    }

    [Test]
    public void EqualObjectTest()
    {
        var area1 = AreaValue.Meter(10);
        var area2 = AreaValue.Meter(10);

        Assert.That(area1.Equals(area2 as object), Is.True);
    }

    [Test]
    public void NotEqualObjectTest()
    {
        var area1 = AreaValue.Meter(10);
        var area2 = AreaValue.Meter(20);

        Assert.That(area1.Equals(area2 as object), Is.False);
    }

    [Test]
    public void ToStringSecondTest()
    {
        var areaValue = AreaValue.Meter(10);

        Assert.That(areaValue.ToString(), Is.EqualTo("10 'm^2'"));
    }

    [Test]
    public void AddOperatorTest()
    {
        var area1 = AreaValue.Meter(1);
        var area2 = AreaValue.Kilometer(1);
        var expected = AreaValue.Meter(1000001);
        var result = area1 + area2;

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void SubOperatorTest()
    {
        var area1 = AreaValue.Kilometer(1);
        var area2 = AreaValue.Meter(500000);
        var expected = AreaValue.Kilometer(0.5);
        var result = area1 - area2;

        Assert.That(result, Is.EqualTo(expected));
    }

    public static IEnumerable<object[]> GetConversionTestCases()
    {
        yield return new object[] { 10.0, AreaUnit.Meter, AreaUnit.Meter, 10.0 };
        yield return new object[] { 10.0, AreaUnit.Meter, AreaUnit.Millimeter, 10000000 };
        yield return new object[] { 10.0, AreaUnit.Meter, AreaUnit.Centimeter, 100000 };
        yield return new object[] { 10.0, AreaUnit.Meter, AreaUnit.Kilometer, 0.00001 };
        yield return new object[] { 10.0, AreaUnit.Meter, AreaUnit.Inch, 15500.031 };
        yield return new object[] { 10.0, AreaUnit.Meter, AreaUnit.Foot, 107.63910417 };
        yield return new object[] { 10.0, AreaUnit.Meter, AreaUnit.Yard, 11.959900463 };
        yield return new object[] { 10.0, AreaUnit.Meter, AreaUnit.Mile, 0.000003861 };
        yield return new object[] { 10.0, AreaUnit.Meter, AreaUnit.Hectare, 0.001 };
        yield return new object[] { 10.0, AreaUnit.Meter, AreaUnit.Acre, 0.0024710538 };

        yield return new object[] { 10.0, AreaUnit.Millimeter, AreaUnit.Millimeter, 10.0 };
        yield return new object[] { 10.0, AreaUnit.Millimeter, AreaUnit.Meter, 0.00001 };
        yield return new object[] { 10.0, AreaUnit.Millimeter, AreaUnit.Centimeter, 0.1 };
        yield return new object[] { 10.0, AreaUnit.Millimeter, AreaUnit.Kilometer, 1.0e-11 };
        yield return new object[] { 10.0, AreaUnit.Millimeter, AreaUnit.Inch, 0.015500031 };
        yield return new object[] { 10.0, AreaUnit.Millimeter, AreaUnit.Foot, 0.0001076391 };
        yield return new object[] { 10.0, AreaUnit.Millimeter, AreaUnit.Yard, 0.0000119599 };
        yield return new object[] { 10.0, AreaUnit.Millimeter, AreaUnit.Mile, 3.861021585E-12 };
        yield return new object[] { 10.0, AreaUnit.Millimeter, AreaUnit.Hectare, 1.0e-9 };
        yield return new object[] { 10.0, AreaUnit.Millimeter, AreaUnit.Acre, 2.471053814E-9 };

        yield return new object[] { 10.0, AreaUnit.Centimeter, AreaUnit.Centimeter, 10.0 };
        yield return new object[] { 10.0, AreaUnit.Centimeter, AreaUnit.Meter, 0.001 };
        yield return new object[] { 10.0, AreaUnit.Centimeter, AreaUnit.Millimeter, 1000 };
        yield return new object[] { 10.0, AreaUnit.Centimeter, AreaUnit.Kilometer, 1.0e-9 };
        yield return new object[] { 10.0, AreaUnit.Centimeter, AreaUnit.Inch, 1.5500031 };
        yield return new object[] { 10.0, AreaUnit.Centimeter, AreaUnit.Foot, 0.0107639104 };
        yield return new object[] { 10.0, AreaUnit.Centimeter, AreaUnit.Yard, 0.00119599 };
        yield return new object[] { 10.0, AreaUnit.Centimeter, AreaUnit.Mile, 3.861021585E-10 };
        yield return new object[] { 10.0, AreaUnit.Centimeter, AreaUnit.Hectare, 1.0e-7 };
        yield return new object[] { 10.0, AreaUnit.Centimeter, AreaUnit.Acre, 2.471053814E-7 };

        yield return new object[] { 10.0, AreaUnit.Kilometer, AreaUnit.Kilometer, 10.0 };
        yield return new object[] { 10.0, AreaUnit.Kilometer, AreaUnit.Meter, 10000000.0 };
        yield return new object[] { 10.0, AreaUnit.Kilometer, AreaUnit.Millimeter, 10000000000000.0 };
        yield return new object[] { 10.0, AreaUnit.Kilometer, AreaUnit.Centimeter, 100000000000.0 };
        yield return new object[] { 10.0, AreaUnit.Kilometer, AreaUnit.Inch, 15500031000.062 };
        yield return new object[] { 10.0, AreaUnit.Kilometer, AreaUnit.Foot, 107639104.1671 };
        yield return new object[] { 10.0, AreaUnit.Kilometer, AreaUnit.Yard, 11959900.46301 };
        yield return new object[] { 10.0, AreaUnit.Kilometer, AreaUnit.Mile, 3.8610215854 };
        yield return new object[] { 10.0, AreaUnit.Kilometer, AreaUnit.Hectare, 1000.0 };
        yield return new object[] { 10.0, AreaUnit.Kilometer, AreaUnit.Acre, 2471.0538147 };

        yield return new object[] { 10.0, AreaUnit.Inch, AreaUnit.Inch, 10.0 };
        yield return new object[] { 10.0, AreaUnit.Inch, AreaUnit.Meter, 0.0064516 };
        yield return new object[] { 10.0, AreaUnit.Inch, AreaUnit.Millimeter, 6451.6 };
        yield return new object[] { 10.0, AreaUnit.Inch, AreaUnit.Centimeter, 64.516 };
        yield return new object[] { 10.0, AreaUnit.Inch, AreaUnit.Kilometer, 6.451599999E-9 };
        yield return new object[] { 10.0, AreaUnit.Inch, AreaUnit.Foot, 0.0694444444 };
        yield return new object[] { 10.0, AreaUnit.Inch, AreaUnit.Yard, 0.0077160494 };
        yield return new object[] { 10.0, AreaUnit.Inch, AreaUnit.Mile, 2.490976686E-9 };
        yield return new object[] { 10.0, AreaUnit.Inch, AreaUnit.Hectare, 6.451599999E-7 };
        yield return new object[] { 10.0, AreaUnit.Inch, AreaUnit.Acre, 0.0000015942 };

        yield return new object[] { 10.0, AreaUnit.Foot, AreaUnit.Foot, 10.0 };
        yield return new object[] { 10.0, AreaUnit.Foot, AreaUnit.Meter, 0.9290304 };
        yield return new object[] { 10.0, AreaUnit.Foot, AreaUnit.Millimeter, 929030.4 };
        yield return new object[] { 10.0, AreaUnit.Foot, AreaUnit.Centimeter, 9290.304 };
        yield return new object[] { 10.0, AreaUnit.Foot, AreaUnit.Kilometer, 9.290303999E-7 };
        yield return new object[] { 10.0, AreaUnit.Foot, AreaUnit.Inch, 1440.0 };
        yield return new object[] { 10.0, AreaUnit.Foot, AreaUnit.Yard, 1.1111111111 };
        yield return new object[] { 10.0, AreaUnit.Foot, AreaUnit.Mile, 3.587006427E-7 };
        yield return new object[] { 10.0, AreaUnit.Foot, AreaUnit.Hectare, 0.000092903 };
        yield return new object[] { 10.0, AreaUnit.Foot, AreaUnit.Acre, 0.0002295684 };

        yield return new object[] { 10.0, AreaUnit.Yard, AreaUnit.Yard, 10.0 };
        yield return new object[] { 10.0, AreaUnit.Yard, AreaUnit.Meter, 8.3612736 };
        yield return new object[] { 10.0, AreaUnit.Yard, AreaUnit.Millimeter, 8361273.6 };
        yield return new object[] { 10.0, AreaUnit.Yard, AreaUnit.Centimeter, 83612.736 };
        yield return new object[] { 10.0, AreaUnit.Yard, AreaUnit.Kilometer, 0.0000083613 };
        yield return new object[] { 10.0, AreaUnit.Yard, AreaUnit.Inch, 12960.0 };
        yield return new object[] { 10.0, AreaUnit.Yard, AreaUnit.Foot, 90.0 };
        yield return new object[] { 10.0, AreaUnit.Yard, AreaUnit.Mile, 0.0000032283 };
        yield return new object[] { 10.0, AreaUnit.Yard, AreaUnit.Hectare, 0.0008361274 };
        yield return new object[] { 10.0, AreaUnit.Yard, AreaUnit.Acre, 0.0020661157 };

        yield return new object[] { 10.0, AreaUnit.Mile, AreaUnit.Mile, 10.0 };
        yield return new object[] { 10.0, AreaUnit.Mile, AreaUnit.Meter, 25899881.10336 };
        yield return new object[] { 10.0, AreaUnit.Mile, AreaUnit.Millimeter, 25899881103360.004 };
        yield return new object[] { 10.0, AreaUnit.Mile, AreaUnit.Centimeter, 258998811033.6 };
        yield return new object[] { 10.0, AreaUnit.Mile, AreaUnit.Kilometer, 25.899881103 };
        yield return new object[] { 10.0, AreaUnit.Mile, AreaUnit.Inch, 40144896000.0 };
        yield return new object[] { 10.0, AreaUnit.Mile, AreaUnit.Foot, 278784000.0 };
        yield return new object[] { 10.0, AreaUnit.Mile, AreaUnit.Yard, 30976000.0 };
        yield return new object[] { 10.0, AreaUnit.Mile, AreaUnit.Hectare, 2589.9881103 };
        yield return new object[] { 10.0, AreaUnit.Mile, AreaUnit.Acre, 6400.0 };

        yield return new object[] { 10.0, AreaUnit.Hectare, AreaUnit.Hectare, 10.0 };
        yield return new object[] { 10.0, AreaUnit.Hectare, AreaUnit.Meter, 100000.0 };
        yield return new object[] { 10.0, AreaUnit.Hectare, AreaUnit.Millimeter, 100000000000.0 };
        yield return new object[] { 10.0, AreaUnit.Hectare, AreaUnit.Centimeter, 1000000000.0 };
        yield return new object[] { 10.0, AreaUnit.Hectare, AreaUnit.Kilometer, 0.1 };
        yield return new object[] { 10.0, AreaUnit.Hectare, AreaUnit.Inch, 155000310.00062 };
        yield return new object[] { 10.0, AreaUnit.Hectare, AreaUnit.Foot, 1076391.04167 };
        yield return new object[] { 10.0, AreaUnit.Hectare, AreaUnit.Yard, 119599.00463 };
        yield return new object[] { 10.0, AreaUnit.Hectare, AreaUnit.Mile, 0.0386102159 };
        yield return new object[] { 10.0, AreaUnit.Hectare, AreaUnit.Acre, 24.710538147 };

        yield return new object[] { 10.0, AreaUnit.Acre, AreaUnit.Acre, 10.0 };
        yield return new object[] { 10.0, AreaUnit.Acre, AreaUnit.Meter, 40468.564224 };
        yield return new object[] { 10.0, AreaUnit.Acre, AreaUnit.Millimeter, 40468564224 };
        yield return new object[] { 10.0, AreaUnit.Acre, AreaUnit.Centimeter, 404685642.24 };
        yield return new object[] { 10.0, AreaUnit.Acre, AreaUnit.Kilometer, 0.0404685642 };
        yield return new object[] { 10.0, AreaUnit.Acre, AreaUnit.Inch, 62726400.0 };
        yield return new object[] { 10.0, AreaUnit.Acre, AreaUnit.Foot, 435600.0 };
        yield return new object[] { 10.0, AreaUnit.Acre, AreaUnit.Yard, 48400.0 };
        yield return new object[] { 10.0, AreaUnit.Acre, AreaUnit.Mile, 0.015625 };
        yield return new object[] { 10.0, AreaUnit.Acre, AreaUnit.Hectare, 4.0468564224 };
    }

    [Test]
    [TestCaseSource(nameof(GetConversionTestCases))]
    public void ConversionTests(double value, AreaUnit unit, AreaUnit to, double expected)
    {
        var area = new AreaValue(new NumberValue(value), unit);
        var converted = area.To(to);

        Assert.That(converted.Value.Number, Is.EqualTo(expected).Within(5));
    }
}