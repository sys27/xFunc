// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class RationalValueTests
{
    [Fact]
    public void Equals_TwoEqualNumbers_ReturnsTrue()
    {
        var a = new RationalValue(3, 4);
        var b = new RationalValue(3, 4);

        Assert.True(a.Equals(b));
    }

    [Fact]
    public void Equals_NumeratorIsDifferent_ReturnsFalse()
    {
        var a = new RationalValue(3, 4);
        var b = new RationalValue(1, 4);

        Assert.False(a.Equals(b));
    }

    [Fact]
    public void Equals_DenominatorIsDifferent_ReturnsFalse()
    {
        var a = new RationalValue(3, 4);
        var b = new RationalValue(3, 2);

        Assert.False(a.Equals(b));
    }

    [Fact]
    public void Equals_NumeratorAndDenominatorAreDifferent_ReturnsFalse()
    {
        var a = new RationalValue(3, 4);
        var b = new RationalValue(4, 2);

        Assert.False(a.Equals(b));
    }

    [Fact]
    public void EqualsObject_TwoEqualNumbers_ReturnsTrue()
    {
        var a = new RationalValue(3, 4);
        var b = new RationalValue(3, 4) as object;

        Assert.True(a.Equals(b));
    }

    [Fact]
    public void EqualsObject_NumeratorAndDenominatorAreDifferent_ReturnsFalse()
    {
        var a = new RationalValue(3, 4);
        var b = new RationalValue(4, 2) as object;

        Assert.False(a.Equals(b));
    }

    [Fact]
    public void EqualsObject_DiffTypes_ReturnsFalse()
    {
        var a = new RationalValue(3, 4);
        var b = 3.0 as object;

        Assert.False(a.Equals(b));
    }

    [Fact]
    public void Equals_DiffDenominator_ReturnsTrue()
    {
        var a = new RationalValue(1, 2);
        var b = new RationalValue(2, 4);

        Assert.True(a.Equals(b));
    }

    [Fact]
    public void EqualsOperator_TwoEqualNumbers_ReturnsTrue()
    {
        var a = new RationalValue(3, 4);
        var b = new RationalValue(3, 4);

        Assert.True(a == b);
    }

    [Fact]
    public void NotEqualsOperator_TwoEqualNumbers_ReturnsTrue()
    {
        var a = new RationalValue(1, 4);
        var b = new RationalValue(3, 4);

        Assert.True(a != b);
    }

    [Fact]
    public void CompareTo_WithPositive_FirstLess()
    {
        var a = new RationalValue(3, 4);
        var b = new RationalValue(4, 2);

        Assert.Equal(-1, a.CompareTo(b));
    }

    [Fact]
    public void CompareTo_WithNegative_FirstLess()
    {
        var a = new RationalValue(-3, 4);
        var b = new RationalValue(4, 2);

        Assert.Equal(-1, a.CompareTo(b));
    }

    [Fact]
    public void CompareTo_WithPositive_FirstGreater()
    {
        var a = new RationalValue(4, 2);
        var b = new RationalValue(3, 4);

        Assert.Equal(1, a.CompareTo(b));
    }

    [Fact]
    public void CompareTo_WithPositive_Equal()
    {
        var a = new RationalValue(4, 2);
        var b = new RationalValue(4, 2);

        Assert.Equal(0, a.CompareTo(b));
    }

    [Fact]
    public void CompareTo_WithNegative_Equal()
    {
        var a = new RationalValue(-4, 2);
        var b = new RationalValue(-4, 2);

        Assert.Equal(0, a.CompareTo(b));
    }

    [Fact]
    public void CompareToObject_WithNull_Greater()
    {
        var a = new RationalValue(-4, 2);

        Assert.Equal(1, a.CompareTo(null));
    }

    [Fact]
    public void CompareToObject_DiffType_ThrowsException()
    {
        var a = new RationalValue(-4, 2);

        Assert.Throws<ArgumentException>(() => a.CompareTo(3.0 as object));
    }

    [Fact]
    public void CompareToObject_WithNegative_Equal()
    {
        var a = new RationalValue(-4, 2);
        var b = new RationalValue(-4, 2) as object;

        Assert.Equal(0, a.CompareTo(b));
    }

    [Fact]
    public void LessThenOperator_WithPositive_ReturnsTrue()
    {
        var a = new RationalValue(3, 4);
        var b = new RationalValue(4, 2);

        Assert.True(a < b);
    }

    [Fact]
    public void LessThenOrEqualOperator_WithPositive_ReturnsTrue()
    {
        var a = new RationalValue(3, 4);
        var b = new RationalValue(3, 4);

        Assert.True(a <= b);
    }

    [Fact]
    public void GreaterThenOperator_WithPositive_ReturnsTrue()
    {
        var a = new RationalValue(4, 2);
        var b = new RationalValue(3, 4);

        Assert.True(a > b);
    }

    [Fact]
    public void GreaterThenOrEqualOperator_WithPositive_ReturnsTrue()
    {
        var a = new RationalValue(3, 4);
        var b = new RationalValue(3, 4);

        Assert.True(a >= b);
    }

    [Fact]
    public void ToStringTest()
    {
        var a = new RationalValue(3, 4);
        const string expected = "3 // 4";
        var result = a.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ToCanonical_IsNotConvertible_KeepTheSame()
    {
        var rationalValue = new RationalValue(3, 4);
        var expected = new RationalValue(3, 4);
        var result = rationalValue.ToCanonical();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ToCanonical_IsConvertible_ReturnNewRational()
    {
        var rationalValue = new RationalValue(4, 4);
        var expected = new RationalValue(1, 1);
        var result = rationalValue.ToCanonical();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ToCanonical_IsConvertible_ReturnNewRational2()
    {
        var rationalValue = new RationalValue(4, 2);
        var expected = new RationalValue(2, 1);
        var result = rationalValue.ToCanonical();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ToCanonical_IsConvertible_ReturnNewRational3()
    {
        var rationalValue = new RationalValue(5, 15);
        var expected = new RationalValue(1, 3);
        var result = rationalValue.ToCanonical();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ToCanonical_WithNegativeNumerator_ReturnNewRational3()
    {
        var rationalValue = new RationalValue(-5, 15);
        var expected = new RationalValue(-1, 3);
        var result = rationalValue.ToCanonical();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ToCanonical_WithNegativeDenominator_ReturnNewRational3()
    {
        var rationalValue = new RationalValue(5, -15);
        var expected = new RationalValue(-1, 3);
        var result = rationalValue.ToCanonical();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ToCanonical_WithNegativeNumeratorAndDenominator_ReturnNewRational3()
    {
        var rationalValue = new RationalValue(-5, -15);
        var expected = new RationalValue(1, 3);
        var result = rationalValue.ToCanonical();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void UnaryMinus_WithPositiveNumber_ReturnNegative()
    {
        var rational = new RationalValue(3, 4);
        var expected = new RationalValue(-3, 4);
        var result = -rational;

        Assert.Equal(expected, result);
    }

    [Fact]
    public void UnaryMinus_WithNegativeNumber_ReturnPositive()
    {
        var rational = new RationalValue(-3, 4);
        var expected = new RationalValue(3, 4);
        var result = -rational;

        Assert.Equal(expected, result);
    }

    [Fact]
    public void AddOperator_TwoNumbers_ReturnNewRational()
    {
        var a = new RationalValue(3, 4);
        var b = new RationalValue(5, 2);
        var expected = new RationalValue(13, 4);
        var result = a + b;

        Assert.Equal(expected, result);
    }

    [Fact]
    public void AddOperator_WithSameDenominator_ReturnNewRational()
    {
        var a = new RationalValue(3, 4);
        var b = new RationalValue(5, 4);
        var expected = new RationalValue(2, 1);
        var result = a + b;

        Assert.Equal(expected, result);
    }

    [Fact]
    public void AddOperator_RationalAndDouble_ReturnNewRational()
    {
        var a = new RationalValue(3, 4);
        var b = new NumberValue(5.0);
        var expected = new RationalValue(23, 4);
        var result = a + b;

        Assert.Equal(expected, result);
    }

    [Fact]
    public void AddOperator_DoubleAndRational_ReturnNewRational()
    {
        var a = new NumberValue(3.0);
        var b = new RationalValue(5, 2);
        var expected = new RationalValue(11, 2);
        var result = a + b;

        Assert.Equal(expected, result);
    }

    [Fact]
    public void SubOperator_TwoNumbers_ReturnNewRational()
    {
        var a = new RationalValue(3, 4);
        var b = new RationalValue(5, 2);
        var expected = new RationalValue(-7, 4);
        var result = a - b;

        Assert.Equal(expected, result);
    }

    [Fact]
    public void SubOperator_WithSameDenominator_ReturnNewRational()
    {
        var a = new RationalValue(3, 4);
        var b = new RationalValue(5, 4);
        var expected = new RationalValue(-1, 2);
        var result = a - b;

        Assert.Equal(expected, result);
    }

    [Fact]
    public void SubOperator_RationalAndDouble_ReturnNewRational()
    {
        var a = new RationalValue(3, 4);
        var b = new NumberValue(5.0);
        var expected = new RationalValue(-17, 4);
        var result = a - b;

        Assert.Equal(expected, result);
    }

    [Fact]
    public void SubOperator_DoubleAndRational_ReturnNewRational()
    {
        var a = new NumberValue(3.0);
        var b = new RationalValue(5, 2);
        var expected = new RationalValue(1, 2);
        var result = a - b;

        Assert.Equal(expected, result);
    }

    [Fact]
    public void MulOperator_TwoNumbers_ReturnNewRational()
    {
        var a = new RationalValue(3, 4);
        var b = new RationalValue(5, 2);
        var expected = new RationalValue(15, 8);
        var result = a * b;

        Assert.Equal(expected, result);
    }

    [Fact]
    public void MulOperator_RationalAndDouble_ReturnNewRational()
    {
        var a = new RationalValue(3, 4);
        var b = new NumberValue(5.0);
        var expected = new RationalValue(15, 4);
        var result = a * b;

        Assert.Equal(expected, result);
    }

    [Fact]
    public void MulOperator_DoubleAndRational_ReturnNewRational()
    {
        var a = new NumberValue(3.0);
        var b = new RationalValue(5, 2);
        var expected = new RationalValue(15, 2);
        var result = a * b;

        Assert.Equal(expected, result);
    }

    [Fact]
    public void DivOperator_TwoNumbers_ReturnNewRational()
    {
        var a = new RationalValue(3, 4);
        var b = new RationalValue(5, 2);
        var expected = new RationalValue(3, 10);
        var result = a / b;

        Assert.Equal(expected, result);
    }

    [Fact]
    public void DivOperator_RationalAndDouble_ReturnNewRational()
    {
        var a = new RationalValue(3, 4);
        var b = new NumberValue(5.0);
        var expected = new RationalValue(3, 20);
        var result = a / b;

        Assert.Equal(expected, result);
    }

    [Fact]
    public void DivOperator_DoubleAndRational_ReturnNewRational()
    {
        var a = new NumberValue(3.0);
        var b = new RationalValue(5, 2);
        var expected = new RationalValue(6, 5);
        var result = a / b;

        Assert.Equal(expected, result);
    }

    [Fact]
    public void AbsTest()
    {
        var rational = new RationalValue(-1, 3);
        var expected = new RationalValue(1, 3);
        var result = RationalValue.Abs(rational);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void PowTest()
    {
        var rational = new RationalValue(2, 3);
        var result = RationalValue.Pow(rational, new NumberValue(3));
        var expected = new RationalValue(8, 27);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void PowNegativeTest()
    {
        var rational = new RationalValue(2, 3);
        var result = RationalValue.Pow(rational, new NumberValue(-3));
        var expected = new RationalValue(27, 8);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void LbTest()
    {
        var rational = new RationalValue(2, 3);
        var result = RationalValue.Lb(rational);
        var expected = new NumberValue(-0.5849625007211563);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void LgTest()
    {
        var rational = new RationalValue(2, 3);
        var result = RationalValue.Lg(rational);
        var expected = new NumberValue(-0.17609125905568124);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void LnTest()
    {
        var rational = new RationalValue(2, 3);
        var result = RationalValue.Ln(rational);
        var expected = new NumberValue(-0.4054651081081645);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void LogTest()
    {
        var rational = new RationalValue(2, 3);
        var result = RationalValue.Log(rational, new NumberValue(3));
        var expected = new NumberValue(-0.3690702464285426);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ToIrrationalTest()
    {
        var b = new RationalValue(1, 2);
        var expected = new NumberValue(0.5);
        var actual = b.ToIrrational();

        Assert.Equal(expected, actual);
    }
}