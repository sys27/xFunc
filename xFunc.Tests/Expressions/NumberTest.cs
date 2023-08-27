// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class NumberTest
{
    [Test]
    public void EqualsNumberNullTest()
    {
        var number = Number.Zero;

        Assert.That(number.Equals(null), Is.False);
    }

    [Test]
    public void EqualsObjectNullTest()
    {
        var number = Number.Zero;

        Assert.That(number.Equals((object)null), Is.False);
    }

    [Test]
    public void EqualsNumberThisTest()
    {
        var number = Number.Zero;

        Assert.That(number.Equals(number), Is.True);
    }

    [Test]
    public void EqualsObjectThisTest()
    {
        var number = Number.Zero;

        Assert.That(number.Equals((object)number), Is.True);
    }

    [Test]
    public void EqualsTest()
    {
        var left = Number.Zero;
        var right = Number.Zero;

        Assert.That(left.Equals(right), Is.True);
    }

    [Test]
    public void NotEqualsTest()
    {
        var left = Number.Zero;
        var right = Number.One;

        Assert.That(left.Equals(right), Is.False);
    }

    [Test]
    public void ExecuteTest()
    {
        var number = Number.One;

        Assert.That(number.Execute(), Is.EqualTo(new NumberValue(1.0)));
    }

    [Test]
    public void NanTest()
    {
        var number = new Number(double.NaN);

        Assert.That(number.Value.IsNaN, Is.True);
    }

    [Test]
    public void PositiveInfinityTest()
    {
        var number = new Number(double.PositiveInfinity);

        Assert.That(number.Value.IsPositiveInfinity, Is.True);
    }

    [Test]
    public void NegativeInfinityTest()
    {
        var number = new Number(double.NegativeInfinity);

        Assert.That(number.Value.IsNegativeInfinity, Is.True);
    }

    [Test]
    public void InfinityTest()
    {
        var number = new Number(double.NegativeInfinity);

        Assert.That(number.Value.IsInfinity, Is.True);
    }

    [Test]
    public void NullAnalyzerTest1()
    {
        var exp = Number.One;

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Test]
    public void NullAnalyzerTest2()
    {
        var exp = Number.One;

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }
}