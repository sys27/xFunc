// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Matrices;

public class MatrixValueTests
{
    [Test]
    public void CtorEmptyVectorTests()
        => Assert.Throws<ArgumentNullException>(() => MatrixValue.Create(Array.Empty<VectorValue>()));

    public static IEnumerable<object[]> NotEqualsTestData()
    {
        yield return new object[] { default(MatrixValue), MatrixValue.Create(NumberValue.One) };

        yield return new object[] { MatrixValue.Create(NumberValue.One), default(MatrixValue) };
    }

    [Test]
    [TestCaseSource(nameof(NotEqualsTestData))]
    public void NotEqualsTest(MatrixValue v1, MatrixValue v2)
        => Assert.False(v1.Equals(v2));

    public static IEnumerable<object[]> EqualsTestData()
    {
        yield return new object[] { default(MatrixValue), default(MatrixValue) };

        yield return new object[] { MatrixValue.Create(NumberValue.One), MatrixValue.Create(NumberValue.One) };
    }

    [Test]
    [TestCaseSource(nameof(EqualsTestData))]
    public void EqualsTest(MatrixValue v1, MatrixValue v2)
        => Assert.That(v1.Equals(v2), Is.True);

    public static IEnumerable<object[]> NotEqualsAsObjectTestData()
    {
        yield return new object[] { default(MatrixValue), MatrixValue.Create(NumberValue.One) };

        yield return new object[] { MatrixValue.Create(NumberValue.One), default(MatrixValue) };

        yield return new object[] { MatrixValue.Create(NumberValue.One), true };
    }

    [Test]
    [TestCaseSource(nameof(NotEqualsAsObjectTestData))]
    public void NotEqualsAsObjectTest(MatrixValue v1, object v2)
        => Assert.That(v1.Equals(v2), Is.False);

    [Test]
    [TestCaseSource(nameof(EqualsTestData))]
    public void EqualsAsObjectTest(MatrixValue v1, MatrixValue v2)
        => Assert.That(v1.Equals(v2 as object), Is.True);

    [Test]
    [TestCaseSource(nameof(NotEqualsTestData))]
    public void NotEqualsAsOperatorTest(MatrixValue v1, MatrixValue v2)
        => Assert.That(v1 != v2, Is.True);

    [Test]
    [TestCaseSource(nameof(EqualsTestData))]
    public void EqualsAsOperatorTest(MatrixValue v1, MatrixValue v2)
        => Assert.That(v1 == v2, Is.True);

    [Test]
    public void ToStringEmptyTest()
    {
        var matrix = default(MatrixValue);
        var expected = "{{}}";

        Assert.That(matrix.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void ToStringTest()
    {
        var matrix = MatrixValue.Create(new NumberValue[][]
        {
            new NumberValue[] { NumberValue.One },
            new NumberValue[] { NumberValue.Two },
        });
        var expected = "{{1}, {2}}";

        Assert.That(matrix.ToString(), Is.EqualTo(expected));
    }
}