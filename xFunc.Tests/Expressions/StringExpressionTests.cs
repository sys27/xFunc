// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class StringExpressionTests : BaseExpressionTests
{
    [Fact]
    public void NullCtor()
    {
        Assert.Throws<ArgumentNullException>(() => new StringExpression(null));
    }

    [Fact]
    public void ExecuteTest()
    {
        var exp = new StringExpression("hello");
        var expected = "hello";

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteWithParamsTest()
    {
        var exp = new StringExpression("hello");
        var parameters = new ExpressionParameters();
        var expected = "hello";

        Assert.Equal(expected, exp.Execute(parameters));
    }

    [Fact]
    public void EqualsNumberNullTest()
    {
        var exp = new StringExpression("hello");

        Assert.False(exp.Equals(null));
    }

    [Fact]
    public void EqualsObjectNullTest()
    {
        var exp = new StringExpression("hello");

        Assert.False(exp.Equals((object)null));
    }

    [Fact]
    public void EqualsNumberThisTest()
    {
        var exp = new StringExpression("hello");

        Assert.True(exp.Equals(exp));
    }

    [Fact]
    public void EqualsObjectThisTest()
    {
        var exp = new StringExpression("hello");

        Assert.True(exp.Equals((object)exp));
    }

    [Fact]
    public void EqualsTest()
    {
        var left = new StringExpression("hello");
        var right = new StringExpression("hello");

        Assert.True(left.Equals(right));
    }

    [Fact]
    public void NotEqualsTest()
    {
        var left = new StringExpression("hello");
        var right = new StringExpression("hello1");

        Assert.False(left.Equals(right));
    }

    [Fact]
    public void EqualsWithDifferentTypeTest()
    {
        var left = new StringExpression("hello");
        var right = Number.One;

        Assert.False(left.Equals(right));
    }

    [Fact]
    public void EqualsAsObjectTest()
    {
        var left = new StringExpression("hello");
        var right = new StringExpression("hello") as object;

        Assert.True(left.Equals(right));
    }

    [Fact]
    public void NullAnalyzerTest1()
    {
        var exp = new StringExpression("hello");

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Fact]
    public void NullAnalyzerTest2()
    {
        var exp = new StringExpression("hello");

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }
}