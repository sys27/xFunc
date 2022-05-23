// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.TimeUnits;

public class TimeTest
{
    [Fact]
    public void EqualNullTest()
    {
        var exp = TimeValue.Second(10).AsExpression();

        Assert.False(exp.Equals(null));
    }

    [Fact]
    public void EqualNullObjectTest()
    {
        var exp = TimeValue.Second(10).AsExpression();

        Assert.False(exp.Equals((object)null));
    }

    [Fact]
    public void EqualSameTest()
    {
        var exp = TimeValue.Second(10).AsExpression();

        Assert.True(exp.Equals(exp));
    }

    [Fact]
    public void EqualSameObjectTest()
    {
        var exp = TimeValue.Second(10).AsExpression();

        Assert.True(exp.Equals((object)exp));
    }

    [Fact]
    public void EqualDiffTypeTest()
    {
        var exp = TimeValue.Second(10).AsExpression();
        var number = Number.One;

        Assert.False(exp.Equals(number));
    }

    [Fact]
    public void ExecuteTest()
    {
        var exp = TimeValue.Second(10).AsExpression();
        var expected = TimeValue.Second(10);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteTest2()
    {
        var exp = TimeValue.Second(10).AsExpression();
        var expected = TimeValue.Second(10);

        Assert.Equal(expected, exp.Execute(null));
    }

    [Fact]
    public void NullAnalyzerTest1()
    {
        var exp = TimeValue.Second(10).AsExpression();

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Fact]
    public void NullAnalyzerTest2()
    {
        var exp = TimeValue.Second(10).AsExpression();

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }
}