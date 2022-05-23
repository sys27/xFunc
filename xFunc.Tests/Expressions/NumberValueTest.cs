// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class NumberValueTest
{
    [Fact]
    public void EqualObjectTest()
    {
        var x = new NumberValue(1);
        var y = (object)new NumberValue(1.0);

        Assert.True(x.Equals(y));
    }

    [Fact]
    public void NotEqualObjectTest()
    {
        var x = new NumberValue(1);
        var y = new object();

        Assert.False(x.Equals(y));
    }

    [Fact]
    public void EqualNumberTest()
    {
        var x = new NumberValue(1);
        var y = new NumberValue(1);

        Assert.True(x == y);
    }

    [Fact]
    public void NotEqualNumberTest()
    {
        var x = new NumberValue(1);
        var y = new NumberValue(2);

        Assert.True(x != y);
    }


    [Fact]
    public void EqualDoubleTest()
    {
        var x = new NumberValue(1);
        var y = 1.0;

        Assert.True(x == y);
        Assert.True(y == x);
    }

    [Fact]
    public void NotEqualDoubleTest()
    {
        var x = new NumberValue(1);
        var y = 2.0;

        Assert.True(x != y);
        Assert.True(y != x);
    }

    [Fact]
    public void NumberLessNumberTest()
    {
        var x = new NumberValue(1);
        var y = new NumberValue(2);

        Assert.True(x < y);
        Assert.True(x <= y);
    }

    [Fact]
    public void NumberLessDoubleTest()
    {
        var x = new NumberValue(1);
        var y = 2.0;

        Assert.True(x < y);
        Assert.True(x <= y);
    }

    [Fact]
    public void DoubleLessNumberTest()
    {
        var x = 1.0;
        var y = new NumberValue(2.0);

        Assert.True(x < y);
        Assert.True(x <= y);
    }

    [Fact]
    public void NumberGreaterNumberTest()
    {
        var x = new NumberValue(1);
        var y = new NumberValue(2);

        Assert.True(y > x);
        Assert.True(y >= x);
    }

    [Fact]
    public void NumberGreaterDoubleTest()
    {
        var x = new NumberValue(1);
        var y = 2.0;

        Assert.True(y > x);
        Assert.True(y >= x);
    }

    [Fact]
    public void DoubleGreaterNumberTest()
    {
        var x = 1.0;
        var y = new NumberValue(2);

        Assert.True(y > x);
        Assert.True(y >= x);
    }

    // ....

    [Fact]
    public void NumberLessOrEqualNumberTest()
    {
        var x = new NumberValue(1);
        var y = new NumberValue(1);

        Assert.True(x <= y);
    }

    [Fact]
    public void NumberLessOrEqualDoubleTest()
    {
        var x = new NumberValue(1);
        var y = 1.0;

        Assert.True(x <= y);
    }

    [Fact]
    public void DoubleLessOrEqualNumberTest()
    {
        var x = 1.0;
        var y = new NumberValue(1.0);

        Assert.True(x <= y);
    }

    [Fact]
    public void NumberGreaterOrEqualNumberTest()
    {
        var x = new NumberValue(1);
        var y = new NumberValue(1);

        Assert.True(y >= x);
    }

    [Fact]
    public void NumberGreaterOrEqualDoubleTest()
    {
        var x = new NumberValue(1);
        var y = 1.0;

        Assert.True(y >= x);
    }

    [Fact]
    public void DoubleGreaterOrEqualNumberTest()
    {
        var x = 1.0;
        var y = new NumberValue(1);

        Assert.True(y >= x);
    }

    [Fact]
    public void CompareNullTest()
    {
        var x = new NumberValue(1);

        Assert.Equal(1, x.CompareTo(null));
    }

    [Fact]
    public void RoundTest()
    {
        var x = new NumberValue(1.5);
        var expected = new NumberValue(2.0);

        Assert.Equal(expected, NumberValue.Round(x));
    }
}