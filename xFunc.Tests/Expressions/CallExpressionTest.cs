// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;

namespace xFunc.Tests.Expressions;

public class CallExpressionTest
{
    [Test]
    public void EqualsNullTest()
    {
        var lambda = new Lambda(Array.Empty<string>(), Number.One).AsExpression();
        var exp = new CallExpression(lambda, ImmutableArray<IExpression>.Empty);

        Assert.That(exp.Equals(null), Is.False);
    }

    [Test]
    public void EqualsSameTest()
    {
        var lambda = new Lambda(Array.Empty<string>(), Number.One).AsExpression();
        var exp = new CallExpression(lambda, ImmutableArray<IExpression>.Empty);

        Assert.That(exp.Equals(exp), Is.True);
    }

    [Test]
    public void EqualsTest()
    {
        var exp1 = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            ImmutableArray<IExpression>.Empty);
        var exp2 = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            ImmutableArray<IExpression>.Empty);

        Assert.That(exp1.Equals(exp2), Is.True);
    }

    [Test]
    public void NotEqualsParametersTest()
    {
        var exp1 = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            ImmutableArray<IExpression>.Empty);
        var exp2 = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            new IExpression[] { Number.One }.ToImmutableArray());

        Assert.That(exp1.Equals(exp2), Is.False);
    }

    [Test]
    public void NotEqualsLambdaTest()
    {
        var exp1 = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            ImmutableArray<IExpression>.Empty);
        var exp2 = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.Two).AsExpression(),
            ImmutableArray<IExpression>.Empty);

        Assert.That(exp1.Equals(exp2), Is.False);
    }

    [Test]
    public void NotEqualsTest()
    {
        var exp1 = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            ImmutableArray<IExpression>.Empty);
        var exp2 = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            new IExpression[] { Number.One }.ToImmutableArray());

        Assert.That(exp1.Equals(exp2), Is.False);
    }

    [Test]
    public void EqualsObjectNullTest()
    {
        var exp = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            ImmutableArray<IExpression>.Empty);

        Assert.That(exp.Equals((object)null), Is.False);
    }

    [Test]
    public void EqualsObjectSameTest()
    {
        var exp = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            ImmutableArray<IExpression>.Empty);

        Assert.That(exp.Equals((object)exp), Is.True);
    }

    [Test]
    public void EqualsObjectTest()
    {
        var exp1 = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            ImmutableArray<IExpression>.Empty);
        var exp2 = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            ImmutableArray<IExpression>.Empty);

        Assert.That(exp1.Equals((object)exp2), Is.True);
    }

    [Test]
    public void NotEqualsDifferentTypesTest()
    {
        var exp1 = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            ImmutableArray<IExpression>.Empty);
        var exp2 = Variable.X;

        Assert.That(exp1.Equals((object)exp2), Is.False);
    }

    [Test]
    public void NotEqualsObjectTest()
    {
        var exp1 = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            ImmutableArray<IExpression>.Empty);
        var exp2 = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            new IExpression[] { Number.One }.ToImmutableArray());

        Assert.That(exp1.Equals((object)exp2), Is.False);
    }

    [Test]
    public void ExecuteTest()
    {
        var exp = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            ImmutableArray<IExpression>.Empty);

        var result = exp.Execute();

        Assert.That(result, Is.EqualTo(NumberValue.One));
    }

    [Test]
    public void ExecuteWithNullParametersTest()
    {
        var exp = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            ImmutableArray<IExpression>.Empty);

        var result = exp.Execute(null);

        Assert.That(result, Is.EqualTo(NumberValue.One));
    }

    [Test]
    public void ExecuteWithNotFunctionTest()
    {
        var exp = new CallExpression(
            Number.One,
            ImmutableArray<IExpression>.Empty);

        Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute(new ExpressionParameters()));
    }

    [Test]
    public void ExecuteWithoutParametersTest()
    {
        var exp = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            ImmutableArray<IExpression>.Empty);

        var result = exp.Execute(new ExpressionParameters());
        var expected = Number.One.Value;

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteWithParametersTest()
    {
        var exp = new CallExpression(
            new Lambda(new[] { Variable.X.Name }, Variable.X).AsExpression(),
            new IExpression[] { Number.One }.ToImmutableArray());

        var result = exp.Execute(new ExpressionParameters());
        var expected = Number.One.Value;

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void NullAnalyzerTest1()
    {
        var exp = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            ImmutableArray<IExpression>.Empty);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Test]
    public void NullAnalyzerTest2()
    {
        var exp = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            ImmutableArray<IExpression>.Empty);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }

    [Test]
    public void CloneTest()
    {
        var exp = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            ImmutableArray<IExpression>.Empty);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}