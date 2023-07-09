// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Matrices;

public class VectorValueTests
{
    [Fact]
    public void CtorEmptyArrayTest()
        => Assert.Throws<ArgumentNullException>(() => VectorValue.Create(Array.Empty<NumberValue>()));

    public static IEnumerable<object[]> NotEqualsTestData()
    {
        yield return new object[] { default(VectorValue), VectorValue.Create(NumberValue.One) };

        yield return new object[] { VectorValue.Create(NumberValue.One), default(VectorValue) };
    }

    [Theory]
    [MemberData(nameof(NotEqualsTestData))]
    public void NotEqualsTest(VectorValue v1, VectorValue v2)
        => Assert.False(v1.Equals(v2));

    public static IEnumerable<object[]> EqualsTestData()
    {
        yield return new object[] { default(VectorValue), default(VectorValue) };

        yield return new object[] { VectorValue.Create(NumberValue.One), VectorValue.Create(NumberValue.One) };
    }

    [Theory]
    [MemberData(nameof(EqualsTestData))]
    public void EqualsTest(VectorValue v1, VectorValue v2)
        => Assert.True(v1.Equals(v2));

    public static IEnumerable<object[]> NotEqualsAsObjectTestData()
    {
        yield return new object[] { default(VectorValue), VectorValue.Create(NumberValue.One) };

        yield return new object[] { VectorValue.Create(NumberValue.One), default(VectorValue) };

        yield return new object[] { VectorValue.Create(NumberValue.One), true };
    }

    [Theory]
    [MemberData(nameof(NotEqualsAsObjectTestData))]
    public void NotEqualsAsObjectTest(VectorValue v1, object v2)
        => Assert.False(v1.Equals(v2));

    [Theory]
    [MemberData(nameof(EqualsTestData))]
    public void EqualsAsObjectTest(VectorValue v1, VectorValue v2)
        => Assert.True(v1.Equals(v2 as object));

    [Theory]
    [MemberData(nameof(NotEqualsTestData))]
    public void NotEqualsAsOperatorTest(VectorValue v1, VectorValue v2)
        => Assert.True(v1 != v2);

    [Theory]
    [MemberData(nameof(EqualsTestData))]
    public void EqualsAsOperatorTest(VectorValue v1, VectorValue v2)
        => Assert.True(v1 == v2);

    [Fact]
    public void ToStringEmptyTest()
    {
        var vector = default(VectorValue);
        var expected = "{}";

        Assert.Equal(expected, vector.ToString());
    }

    [Fact]
    public void ToStringTest()
    {
        var vector = VectorValue.Create(NumberValue.One, NumberValue.Two);
        var expected = "{1, 2}";

        Assert.Equal(expected, vector.ToString());
    }
}