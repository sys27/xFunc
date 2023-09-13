// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Matrices;

public class VectorValueTests
{
    [Test]
    public void CtorEmptyArrayTest()
        => Assert.Throws<ArgumentNullException>(() => VectorValue.Create(Array.Empty<NumberValue>()));

    public static IEnumerable<object[]> NotEqualsTestData()
    {
        yield return new object[] { default(VectorValue), VectorValue.Create(NumberValue.One) };

        yield return new object[] { VectorValue.Create(NumberValue.One), default(VectorValue) };
    }

    [Test]
    [TestCaseSource(nameof(NotEqualsTestData))]
    public void NotEqualsTest(VectorValue v1, VectorValue v2)
        => Assert.False(v1.Equals(v2));

    public static IEnumerable<object[]> EqualsTestData()
    {
        yield return new object[] { default(VectorValue), default(VectorValue) };

        yield return new object[] { VectorValue.Create(NumberValue.One), VectorValue.Create(NumberValue.One) };
    }

    [Test]
    [TestCaseSource(nameof(EqualsTestData))]
    public void EqualsTest(VectorValue v1, VectorValue v2)
        => Assert.True(v1.Equals(v2));

    public static IEnumerable<object[]> NotEqualsAsObjectTestData()
    {
        yield return new object[] { default(VectorValue), VectorValue.Create(NumberValue.One) };

        yield return new object[] { VectorValue.Create(NumberValue.One), default(VectorValue) };

        yield return new object[] { VectorValue.Create(NumberValue.One), true };
    }

    [Test]
    [TestCaseSource(nameof(NotEqualsAsObjectTestData))]
    public void NotEqualsAsObjectTest(VectorValue v1, object v2)
        => Assert.That(v1.Equals(v2), Is.False);

    [Test]
    [TestCaseSource(nameof(EqualsTestData))]
    public void EqualsAsObjectTest(VectorValue v1, VectorValue v2)
        => Assert.That(v1.Equals(v2 as object), Is.True);

    [Test]
    [TestCaseSource(nameof(NotEqualsTestData))]
    public void NotEqualsAsOperatorTest(VectorValue v1, VectorValue v2)
        => Assert.That(v1 != v2, Is.True);

    [Test]
    [TestCaseSource(nameof(EqualsTestData))]
    public void EqualsAsOperatorTest(VectorValue v1, VectorValue v2)
        => Assert.That(v1 == v2, Is.True);

    [Test]
    public void ToStringEmptyTest()
    {
        var vector = default(VectorValue);
        var expected = "{}";

        Assert.That(vector.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void ToStringTest()
    {
        var vector = VectorValue.Create(NumberValue.One, NumberValue.Two);
        var expected = "{1, 2}";

        Assert.That(vector.ToString(), Is.EqualTo(expected));
    }

    public static IEnumerable<object[]> AbsTestData()
    {
        yield return new object[]
        {
            VectorValue.Create(new NumberValue(1), new NumberValue(2), new NumberValue(3)),
            new NumberValue(3.7416573867739413)
        };

        yield return new object[]
        {
            VectorValue.Create(
                new NumberValue(1),
                new NumberValue(2),
                new NumberValue(3),
                new NumberValue(4),
                new NumberValue(5),
                new NumberValue(6),
                new NumberValue(7),
                new NumberValue(8)),
            new NumberValue(14.2828568570857)
        };

        yield return new object[]
        {
            VectorValue.Create(
                new NumberValue(1),
                new NumberValue(2),
                new NumberValue(3),
                new NumberValue(4),
                new NumberValue(5),
                new NumberValue(6),
                new NumberValue(7),
                new NumberValue(8),
                new NumberValue(9)),
            new NumberValue(16.881943016134134)
        };
    }

    [Test]
    [TestCaseSource(nameof(AbsTestData))]
    public void AbsTest(VectorValue vector, NumberValue expected)
        => Assert.That(VectorValue.Abs(vector), Is.EqualTo(expected));

    public static IEnumerable<object[]> AddTestData()
    {
        yield return new object[]
        {
            VectorValue.Create(new NumberValue(1), new NumberValue(2), new NumberValue(3)),
            VectorValue.Create(new NumberValue(3), new NumberValue(2), new NumberValue(1)),
            VectorValue.Create(new NumberValue(4), new NumberValue(4), new NumberValue(4)),
        };

        yield return new object[]
        {
            VectorValue.Create(
                new NumberValue(1),
                new NumberValue(2),
                new NumberValue(3),
                new NumberValue(4),
                new NumberValue(5),
                new NumberValue(6),
                new NumberValue(7),
                new NumberValue(8)),
            VectorValue.Create(
                new NumberValue(1),
                new NumberValue(2),
                new NumberValue(3),
                new NumberValue(4),
                new NumberValue(5),
                new NumberValue(6),
                new NumberValue(7),
                new NumberValue(8)),
            VectorValue.Create(
                new NumberValue(2),
                new NumberValue(4),
                new NumberValue(6),
                new NumberValue(8),
                new NumberValue(10),
                new NumberValue(12),
                new NumberValue(14),
                new NumberValue(16)),
        };

        yield return new object[]
        {
            VectorValue.Create(
                new NumberValue(1),
                new NumberValue(2),
                new NumberValue(3),
                new NumberValue(4),
                new NumberValue(5),
                new NumberValue(6),
                new NumberValue(7),
                new NumberValue(8),
                new NumberValue(9)),
            VectorValue.Create(
                new NumberValue(1),
                new NumberValue(2),
                new NumberValue(3),
                new NumberValue(4),
                new NumberValue(5),
                new NumberValue(6),
                new NumberValue(7),
                new NumberValue(8),
                new NumberValue(9)),
            VectorValue.Create(
                new NumberValue(2),
                new NumberValue(4),
                new NumberValue(6),
                new NumberValue(8),
                new NumberValue(10),
                new NumberValue(12),
                new NumberValue(14),
                new NumberValue(16),
                new NumberValue(18)),
        };
    }

    [Test]
    [TestCaseSource(nameof(AddTestData))]
    public void AddTest(VectorValue v1, VectorValue v2, VectorValue expected)
        => Assert.That(v1 + v2, Is.EqualTo(expected));

    public static IEnumerable<object[]> SubTestData()
    {
        yield return new object[]
        {
            VectorValue.Create(new NumberValue(1), new NumberValue(2), new NumberValue(3)),
            VectorValue.Create(new NumberValue(3), new NumberValue(2), new NumberValue(1)),
            VectorValue.Create(new NumberValue(-2), new NumberValue(0), new NumberValue(2)),
        };

        yield return new object[]
        {
            VectorValue.Create(
                new NumberValue(1),
                new NumberValue(2),
                new NumberValue(3),
                new NumberValue(4),
                new NumberValue(5),
                new NumberValue(6),
                new NumberValue(7),
                new NumberValue(8)),
            VectorValue.Create(
                new NumberValue(1),
                new NumberValue(2),
                new NumberValue(3),
                new NumberValue(4),
                new NumberValue(5),
                new NumberValue(6),
                new NumberValue(7),
                new NumberValue(8)),
            VectorValue.Create(
                new NumberValue(0),
                new NumberValue(0),
                new NumberValue(0),
                new NumberValue(0),
                new NumberValue(0),
                new NumberValue(0),
                new NumberValue(0),
                new NumberValue(0)),
        };

        yield return new object[]
        {
            VectorValue.Create(
                new NumberValue(10),
                new NumberValue(20),
                new NumberValue(30),
                new NumberValue(40),
                new NumberValue(50),
                new NumberValue(60),
                new NumberValue(70),
                new NumberValue(80),
                new NumberValue(90)),
            VectorValue.Create(
                new NumberValue(1),
                new NumberValue(2),
                new NumberValue(3),
                new NumberValue(4),
                new NumberValue(5),
                new NumberValue(6),
                new NumberValue(7),
                new NumberValue(8),
                new NumberValue(9)),
            VectorValue.Create(
                new NumberValue(9),
                new NumberValue(18),
                new NumberValue(27),
                new NumberValue(36),
                new NumberValue(45),
                new NumberValue(54),
                new NumberValue(63),
                new NumberValue(72),
                new NumberValue(81)),
        };
    }

    [Test]
    [TestCaseSource(nameof(SubTestData))]
    public void SubTest(VectorValue v1, VectorValue v2, VectorValue expected)
        => Assert.That(v1 - v2, Is.EqualTo(expected));

    public static IEnumerable<object[]> MulByScalarTestData()
    {
        yield return new object[]
        {
            VectorValue.Create(new NumberValue(1), new NumberValue(2), new NumberValue(3)),
            NumberValue.Two,
            VectorValue.Create(new NumberValue(2), new NumberValue(4), new NumberValue(6)),
        };

        yield return new object[]
        {
            VectorValue.Create(
                new NumberValue(1),
                new NumberValue(2),
                new NumberValue(3),
                new NumberValue(4),
                new NumberValue(5),
                new NumberValue(6),
                new NumberValue(7),
                new NumberValue(8)),
            NumberValue.Two,
            VectorValue.Create(
                new NumberValue(2),
                new NumberValue(4),
                new NumberValue(6),
                new NumberValue(8),
                new NumberValue(10),
                new NumberValue(12),
                new NumberValue(14),
                new NumberValue(16)),
        };

        yield return new object[]
        {
            VectorValue.Create(
                new NumberValue(1),
                new NumberValue(2),
                new NumberValue(3),
                new NumberValue(4),
                new NumberValue(5),
                new NumberValue(6),
                new NumberValue(7),
                new NumberValue(8),
                new NumberValue(9)),
            NumberValue.Two,
            VectorValue.Create(
                new NumberValue(2),
                new NumberValue(4),
                new NumberValue(6),
                new NumberValue(8),
                new NumberValue(10),
                new NumberValue(12),
                new NumberValue(14),
                new NumberValue(16),
                new NumberValue(18)),
        };
    }

    [Test]
    [TestCaseSource(nameof(MulByScalarTestData))]
    public void MulByScalarTest(VectorValue v1, NumberValue v2, VectorValue expected)
        => Assert.That(v1 * v2, Is.EqualTo(expected));

    public static IEnumerable<object[]> MulTestData()
    {
        yield return new object[]
        {
            VectorValue.Create(new NumberValue(1), new NumberValue(2), new NumberValue(3)),
            VectorValue.Create(new NumberValue(3), new NumberValue(2), new NumberValue(1)),
            new NumberValue(10),
        };

        yield return new object[]
        {
            VectorValue.Create(
                new NumberValue(1),
                new NumberValue(2),
                new NumberValue(3),
                new NumberValue(4),
                new NumberValue(5),
                new NumberValue(6),
                new NumberValue(7),
                new NumberValue(8)),
            VectorValue.Create(
                new NumberValue(1),
                new NumberValue(2),
                new NumberValue(3),
                new NumberValue(4),
                new NumberValue(5),
                new NumberValue(6),
                new NumberValue(7),
                new NumberValue(8)),
            new NumberValue(204),
        };

        yield return new object[]
        {
            VectorValue.Create(
                new NumberValue(10),
                new NumberValue(20),
                new NumberValue(30),
                new NumberValue(40),
                new NumberValue(50),
                new NumberValue(60),
                new NumberValue(70),
                new NumberValue(80),
                new NumberValue(90)),
            VectorValue.Create(
                new NumberValue(1),
                new NumberValue(2),
                new NumberValue(3),
                new NumberValue(4),
                new NumberValue(5),
                new NumberValue(6),
                new NumberValue(7),
                new NumberValue(8),
                new NumberValue(9)),
            new NumberValue(2850),
        };
    }

    [Test]
    [TestCaseSource(nameof(MulTestData))]
    public void MulTest(VectorValue v1, VectorValue v2, NumberValue expected)
        => Assert.That(v1 * v2, Is.EqualTo(expected));

    public static IEnumerable<object[]> SumTestData()
    {
        yield return new object[]
        {
            VectorValue.Create(new NumberValue(1), new NumberValue(2), new NumberValue(3)),
            new NumberValue(6),
        };

        yield return new object[]
        {
            VectorValue.Create(
                new NumberValue(1),
                new NumberValue(2),
                new NumberValue(3),
                new NumberValue(4),
                new NumberValue(5),
                new NumberValue(6),
                new NumberValue(7),
                new NumberValue(8)),
            new NumberValue(36),
        };

        yield return new object[]
        {
            VectorValue.Create(
                new NumberValue(10),
                new NumberValue(20),
                new NumberValue(30),
                new NumberValue(40),
                new NumberValue(50),
                new NumberValue(60),
                new NumberValue(70),
                new NumberValue(80),
                new NumberValue(90)),
            new NumberValue(450),
        };
    }

    [Test]
    [TestCaseSource(nameof(SumTestData))]
    public void SumTest(VectorValue v1, NumberValue expected)
        => Assert.That(v1.Sum(), Is.EqualTo(expected));
}