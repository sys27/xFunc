// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class NumberValueTest
{
    [Test]
    public void EqualObjectTest()
    {
        var x = new NumberValue(1);
        var y = (object)new NumberValue(1.0);

        Assert.That(x.Equals(y), Is.True);
    }

    [Test]
    public void NotEqualObjectTest()
    {
        var x = new NumberValue(1);
        var y = new object();

        Assert.That(x.Equals(y), Is.False);
    }

    [Test]
    public void EqualNumberTest()
    {
        var x = new NumberValue(1);
        var y = new NumberValue(1);

        Assert.That(x == y, Is.True);
    }

    [Test]
    public void NotEqualNumberTest()
    {
        var x = new NumberValue(1);
        var y = new NumberValue(2);

        Assert.That(x != y, Is.True);
    }


    [Test]
    public void EqualDoubleTest()
    {
        var x = new NumberValue(1);
        var y = 1.0;

        Assert.That(x == y, Is.True);
        Assert.That(y == x, Is.True);
    }

    [Test]
    public void NotEqualDoubleTest()
    {
        var x = new NumberValue(1);
        var y = 2.0;

        Assert.That(x != y, Is.True);
        Assert.That(y != x, Is.True);
    }

    [Test]
    public void NumberLessNumberTest()
    {
        var x = new NumberValue(1);
        var y = new NumberValue(2);

        Assert.That(x < y, Is.True);
        Assert.That(x <= y, Is.True);
    }

    [Test]
    public void NumberLessDoubleTest()
    {
        var x = new NumberValue(1);
        var y = 2.0;

        Assert.That(x < y, Is.True);
        Assert.That(x <= y, Is.True);
    }

    [Test]
    public void DoubleLessNumberTest()
    {
        var x = 1.0;
        var y = new NumberValue(2.0);

        Assert.That(x < y, Is.True);
        Assert.That(x <= y, Is.True);
    }

    [Test]
    public void NumberGreaterNumberTest()
    {
        var x = new NumberValue(1);
        var y = new NumberValue(2);

        Assert.That(y > x, Is.True);
        Assert.That(y >= x, Is.True);
    }

    [Test]
    public void NumberGreaterDoubleTest()
    {
        var x = new NumberValue(1);
        var y = 2.0;

        Assert.That(y > x, Is.True);
        Assert.That(y >= x, Is.True);
    }

    [Test]
    public void DoubleGreaterNumberTest()
    {
        var x = 1.0;
        var y = new NumberValue(2);

        Assert.That(y > x, Is.True);
        Assert.That(y >= x, Is.True);
    }

    // ....

    [Test]
    public void NumberLessOrEqualNumberTest()
    {
        var x = new NumberValue(1);
        var y = new NumberValue(1);

        Assert.That(x <= y, Is.True);
    }

    [Test]
    public void NumberLessOrEqualDoubleTest()
    {
        var x = new NumberValue(1);
        var y = 1.0;

        Assert.That(x <= y, Is.True);
    }

    [Test]
    public void DoubleLessOrEqualNumberTest()
    {
        var x = 1.0;
        var y = new NumberValue(1.0);

        Assert.That(x <= y, Is.True);
    }

    [Test]
    public void NumberGreaterOrEqualNumberTest()
    {
        var x = new NumberValue(1);
        var y = new NumberValue(1);

        Assert.That(y >= x, Is.True);
    }

    [Test]
    public void NumberGreaterOrEqualDoubleTest()
    {
        var x = new NumberValue(1);
        var y = 1.0;

        Assert.That(y >= x, Is.True);
    }

    [Test]
    public void DoubleGreaterOrEqualNumberTest()
    {
        var x = 1.0;
        var y = new NumberValue(1);

        Assert.That(y >= x, Is.True);
    }

    [Test]
    public void CompareNullTest()
    {
        var x = new NumberValue(1);

        Assert.That(x.CompareTo(null), Is.EqualTo(1));
    }

    [Test]
    public void CompareToDifferentTypeTest()
    {
        var x = NumberValue.One;
        var y = AngleValue.Degree(90);

        Assert.Throws<ArgumentException>(() => x.CompareTo(y));
    }

    [Test]
    public void RoundTest()
    {
        var x = new NumberValue(1.5);
        var expected = new NumberValue(2.0);

        Assert.That(NumberValue.Round(x), Is.EqualTo(expected));
    }
}