// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class StringExpressionTests : BaseExpressionTests
{
    [Test]
    public void NullCtor()
    {
        Assert.Throws<ArgumentNullException>(() => new StringExpression(null));
    }

    [Test]
    public void ExecuteTest()
    {
        var exp = new StringExpression("hello");

        Assert.That(exp.Execute(), Is.EqualTo("hello"));
    }

    [Test]
    public void ExecuteWithParamsTest()
    {
        var exp = new StringExpression("hello");
        var parameters = new ExpressionParameters();

        Assert.That(exp.Execute(parameters), Is.EqualTo("hello"));
    }

    [Test]
    public void EqualsNumberNullTest()
    {
        var exp = new StringExpression("hello");

        Assert.That(exp.Equals(null), Is.False);
    }

    [Test]
    public void EqualsObjectNullTest()
    {
        var exp = new StringExpression("hello");

        Assert.That(exp.Equals((object)null), Is.False);
    }

    [Test]
    public void EqualsNumberThisTest()
    {
        var exp = new StringExpression("hello");

        Assert.That(exp.Equals(exp), Is.True);
    }

    [Test]
    public void EqualsObjectThisTest()
    {
        var exp = new StringExpression("hello");

        Assert.That(exp.Equals((object)exp), Is.True);
    }

    [Test]
    public void EqualsTest()
    {
        var left = new StringExpression("hello");
        var right = new StringExpression("hello");

        Assert.That(left.Equals(right), Is.True);
    }

    [Test]
    public void NotEqualsTest()
    {
        var left = new StringExpression("hello");
        var right = new StringExpression("hello1");

        Assert.That(left.Equals(right), Is.False);
    }

    [Test]
    public void EqualsWithDifferentTypeTest()
    {
        var left = new StringExpression("hello");
        var right = Number.One;

        Assert.That(left.Equals(right), Is.False);
    }

    [Test]
    public void EqualsAsObjectTest()
    {
        var left = new StringExpression("hello");
        var right = new StringExpression("hello") as object;

        Assert.That(left.Equals(right), Is.True);
    }

    [Test]
    public void NullAnalyzerTest1()
    {
        var exp = new StringExpression("hello");

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Test]
    public void NullAnalyzerTest2()
    {
        var exp = new StringExpression("hello");

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }
}