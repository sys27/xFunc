// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.AngleUnits;

public class AngleValueTest
{
    [Test]
    public void EqualTest()
    {
        var angle1 = AngleValue.Degree(10);
        var angle2 = AngleValue.Degree(10);

        Assert.That(angle1.Equals(angle2), Is.True);
    }

    [Test]
    public void EqualOperatorTest()
    {
        var angle1 = AngleValue.Degree(10);
        var angle2 = AngleValue.Degree(10);

        Assert.That(angle1 == angle2, Is.True);
    }

    [Test]
    public void NotEqualTest()
    {
        var angle1 = AngleValue.Degree(10);
        var angle2 = AngleValue.Degree(12);

        Assert.That(angle1 != angle2, Is.True);
    }

    [Test]
    public void LessTest()
    {
        var angle1 = AngleValue.Degree(10);
        var angle2 = AngleValue.Degree(12);

        Assert.That(angle1 < angle2, Is.True);
    }

    [Test]
    public void LessFalseTest()
    {
        var angle1 = AngleValue.Degree(20);
        var angle2 = AngleValue.Degree(12);

        Assert.That(angle1 < angle2, Is.False);
    }

    [Test]
    public void GreaterTest()
    {
        var angle1 = AngleValue.Degree(20);
        var angle2 = AngleValue.Degree(12);

        Assert.That(angle1 > angle2, Is.True);
    }

    [Test]
    public void GreaterFalseTest()
    {
        var angle1 = AngleValue.Degree(10);
        var angle2 = AngleValue.Degree(12);

        Assert.That(angle1 > angle2, Is.False);
    }

    [Test]
    public void LessOrEqualTest()
    {
        var angle1 = AngleValue.Degree(10);
        var angle2 = AngleValue.Degree(10);

        Assert.That(angle1 <= angle2, Is.True);
    }

    [Test]
    public void GreaterOrEqualTest()
    {
        var angle1 = AngleValue.Degree(10);
        var angle2 = AngleValue.Degree(10);

        Assert.That(angle1 >= angle2, Is.True);
    }

    [Test]
    public void CompareToNull()
    {
        var angle = AngleValue.Degree(10);

        Assert.That(angle.CompareTo(null) > 0, Is.True);
    }

    [Test]
    public void CompareToObject()
    {
        var angle1 = AngleValue.Degree(10);
        var angle2 = (object)AngleValue.Degree(10);

        Assert.That(angle1.CompareTo(angle2) == 0, Is.True);
    }

    [Test]
    public void CompareToDouble()
    {
        var angle = AngleValue.Degree(10);

        Assert.Throws<ArgumentException>(() => angle.CompareTo(1));
    }

    [Test]
    public void ValueNotEqualTest()
    {
        var angle1 = AngleValue.Degree(10);
        var angle2 = AngleValue.Degree(12);

        Assert.That(angle1.Equals(angle2), Is.False);
    }

    [Test]
    public void UnitNotEqualTest2()
    {
        var angle1 = AngleValue.Degree(10);
        var angle2 = AngleValue.Radian(10);

        Assert.That(angle1.Equals(angle2), Is.False);
    }

    [Test]
    public void EqualDiffTypeTest()
    {
        var angle1 = AngleValue.Degree(10);
        var angle2 = 3;

        Assert.That(angle1.Equals(angle2), Is.False);
    }

    [Test]
    public void EqualObjectTest()
    {
        var angle1 = AngleValue.Degree(10);
        var angle2 = AngleValue.Degree(10);

        Assert.That(angle1.Equals(angle2 as object), Is.True);
    }

    [Test]
    public void NotEqualObjectTest()
    {
        var angle1 = AngleValue.Degree(10);
        var angle2 = AngleValue.Degree(20);

        Assert.That(angle1.Equals(angle2 as object), Is.False);
    }

    [Test]
    public void ToStringDegreeTest()
    {
        var angle = AngleValue.Degree(10);

        Assert.That(angle.ToString(), Is.EqualTo("10 degree"));
    }

    [Test]
    public void ToStringRadianTest()
    {
        var angle = AngleValue.Radian(10);

        Assert.That(angle.ToString(), Is.EqualTo("10 radian"));
    }

    [Test]
    public void ToStringGradianTest()
    {
        var angle = AngleValue.Gradian(10);

        Assert.That(angle.ToString(), Is.EqualTo("10 gradian"));
    }

    [Test]
    public void DegreeToDegreeTest()
    {
        var angle = AngleValue.Degree(10);
        var actual = angle.To(AngleUnit.Degree);
        var expected = AngleValue.Degree(10);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void DegreeToRadianTest()
    {
        var angle = AngleValue.Degree(10);
        var actual = angle.To(AngleUnit.Radian);
        var expected = AngleValue.Radian(10 * Math.PI / 180);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void DegreeToGradianTest()
    {
        var angle = AngleValue.Degree(10);
        var actual = angle.To(AngleUnit.Gradian);
        var expected = AngleValue.Gradian(10 / 0.9);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void RadianToDegreeTest()
    {
        var angle = AngleValue.Radian(10);
        var actual = angle.To(AngleUnit.Degree);
        var expected = AngleValue.Degree(10 * 180 / Math.PI);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void RadianToRadianTest()
    {
        var angle = AngleValue.Radian(10);
        var actual = angle.To(AngleUnit.Radian);
        var expected = AngleValue.Radian(10);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void RadianToGradianTest()
    {
        var angle = AngleValue.Radian(10);
        var actual = angle.To(AngleUnit.Gradian);
        var expected = AngleValue.Gradian(10 * 200 / Math.PI);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GradianToDegreeTest()
    {
        var angle = AngleValue.Gradian(10);
        var actual = angle.To(AngleUnit.Degree);
        var expected = AngleValue.Degree(10 * 0.9);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GradianToRadianTest()
    {
        var angle = AngleValue.Gradian(10);
        var actual = angle.To(AngleUnit.Radian);
        var expected = AngleValue.Radian(10 * Math.PI / 200);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GradianToGradianTest()
    {
        var angle = AngleValue.Gradian(10);
        var actual = angle.To(AngleUnit.Gradian);
        var expected = AngleValue.Gradian(10);

        Assert.That(actual, Is.EqualTo(expected));
    }

    public static IEnumerable<object[]> NormalizeTestsCases()
    {
        yield return new object[] { 0, AngleUnit.Degree, 0 };
        yield return new object[] { 0, AngleUnit.Radian, 0 };
        yield return new object[] { 0, AngleUnit.Gradian, 0 };
        yield return new object[] { 90, AngleUnit.Degree, 90 };
        yield return new object[] { 1.5707963267948966, AngleUnit.Radian, 1.5707963267948966 };
        yield return new object[] { 100, AngleUnit.Gradian, 100 };
        yield return new object[] { 360, AngleUnit.Degree, 0 };
        yield return new object[] { 6.283185307179586, AngleUnit.Radian, 0 };
        yield return new object[] { 400, AngleUnit.Gradian, 0 };
        yield return new object[] { 1110.0, AngleUnit.Degree, 30 };
        yield return new object[] { 19.37315469713706, AngleUnit.Radian, 0.5235987755982988 };
        yield return new object[] { 1233, AngleUnit.Gradian, 33 };
        yield return new object[] { 1770.0, AngleUnit.Degree, 330 };
        yield return new object[] { 30.892327760299633, AngleUnit.Radian, 5.759586531581287 };
        yield return new object[] { 1966, AngleUnit.Gradian, 366 };
        yield return new object[] { -390.0, AngleUnit.Degree, 330 };
        yield return new object[] { -6.8067840827778845, AngleUnit.Radian, 5.759586531581287 };
        yield return new object[] { -434.0, AngleUnit.Gradian, 366 };
    }

    [Test]
    [TestCaseSource(nameof(NormalizeTestsCases))]
    public void NormalizeTests(double angleValue, AngleUnit unit, double expectedValue)
    {
        var angle = new AngleValue(new NumberValue(angleValue), unit);
        var normalized = angle.Normalize();
        var expected = new AngleValue(new NumberValue(expectedValue), unit);

        Assert.That(normalized, Is.EqualTo(expected));
    }
}