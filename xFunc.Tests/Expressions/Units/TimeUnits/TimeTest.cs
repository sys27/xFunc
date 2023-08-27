// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.TimeUnits;

public class TimeTest
{
    [Test]
    public void EqualNullTest()
    {
        var exp = TimeValue.Second(10).AsExpression();

        Assert.That(exp.Equals(null), Is.False);
    }

    [Test]
    public void EqualNullObjectTest()
    {
        var exp = TimeValue.Second(10).AsExpression();

        Assert.That(exp.Equals((object)null), Is.False);
    }

    [Test]
    public void EqualSameTest()
    {
        var exp = TimeValue.Second(10).AsExpression();

        Assert.That(exp.Equals(exp), Is.True);
    }

    [Test]
    public void EqualSameObjectTest()
    {
        var exp = TimeValue.Second(10).AsExpression();

        Assert.That(exp.Equals((object)exp), Is.True);
    }

    [Test]
    public void EqualDiffTypeTest()
    {
        var exp = TimeValue.Second(10).AsExpression();
        var number = Number.One;

        Assert.That(exp.Equals(number), Is.False);
    }

    [Test]
    public void ExecuteTest()
    {
        var exp = TimeValue.Second(10).AsExpression();
        var expected = TimeValue.Second(10);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteTest2()
    {
        var exp = TimeValue.Second(10).AsExpression();
        var expected = TimeValue.Second(10);

        Assert.That(exp.Execute(null), Is.EqualTo(expected));
    }

    [Test]
    public void NullAnalyzerTest1()
    {
        var exp = TimeValue.Second(10).AsExpression();

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Test]
    public void NullAnalyzerTest2()
    {
        var exp = TimeValue.Second(10).AsExpression();

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }
}