// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;

namespace xFunc.Tests.Expressions;

public class CurryTest : BaseExpressionTests
{
    [Test]
    public void CtorNullTest()
        => Assert.Throws<ArgumentNullException>(() => new Curry(null));

    [Test]
    public void EqualsSameObjectTest()
    {
        var exp = new Curry(new Lambda(Number.One).AsExpression());

        Assert.That(exp.Equals(exp), Is.True);
    }

    [Test]
    public void EqualsNullTest()
    {
        var exp = new Curry(new Lambda(Number.One).AsExpression());

        Assert.That(exp.Equals(null), Is.False);
    }

    [Test]
    public void EqualsDiffTypeTest()
    {
        var exp = new Curry(new Lambda(Number.One).AsExpression());

        Assert.That(exp.Equals(new object()), Is.False);
    }

    [Test]
    public void NotEqualsTest()
    {
        var exp1 = new Curry(new Lambda(Number.One).AsExpression());
        var exp2 = new Curry(new Lambda(Number.Two).AsExpression());

        Assert.That(exp1.Equals(exp2), Is.False);
    }

    [Test]
    public void ExecuteNonLambdaTest()
        => TestNotSupported(new Curry(Number.One));

    [Test]
    public void ExecuteTooManyParametersTest()
    {
        var exp = new Curry(
            Number.One.ToLambdaExpression(),
            new IExpression[] { Number.One }.ToImmutableArray());

        Assert.Throws<ArgumentException>(() => exp.Execute());
    }

    [Test]
    public void ExecuteExactAmountParametersTest()
    {
        var exp = new Curry(
            Variable.X.ToLambdaExpression("x"),
            new IExpression[] { Number.One }.ToImmutableArray());

        var result = exp.Execute();

        Assert.That(result, Is.EqualTo(NumberValue.One));
    }

    [Test]
    public void ExecuteCurryWithoutParametersTest()
    {
        var exp = new Curry(new Add(Variable.X, Variable.Y).ToLambdaExpression("x", "y"));
        var expected = new Add(Variable.X, Variable.Y)
            .ToLambdaExpression("y")
            .ToLambda("x");

        var result = exp.Execute();

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteCurryTest()
    {
        var exp = new Curry(
            new Add(Variable.X, Variable.Y).ToLambdaExpression("x", "y"),
            new IExpression[] { Number.One }.ToImmutableArray());
        var expected = new Add(Variable.X, Variable.Y).ToLambda("y");

        var result = exp.Execute();

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void NullAnalyzerTest1()
    {
        var exp = new Curry(new Lambda(Number.One).AsExpression());

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Test]
    public void NullAnalyzerTest2()
    {
        var exp = new Curry(new Lambda(Number.One).AsExpression());

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }

    [Test]
    public void CloneTest()
    {
        var exp = new Curry(new Lambda(Number.One).AsExpression());
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }

    [Test]
    public void CloneTest2()
    {
        var exp = new Curry(new Lambda(Number.One).AsExpression());
        var lambda = new Lambda(Number.Two).AsExpression();
        var parameters = new IExpression[] { Number.One }.ToImmutableArray();
        var expected = new Curry(lambda, parameters);
        var clone = exp.Clone(lambda, parameters);

        Assert.That(clone, Is.EqualTo(expected));
    }
}