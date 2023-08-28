// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using NSubstitute;

namespace xFunc.Tests.Expressions;

public class SimplifyTest
{
    [Test]
    public void SimplifierNull()
        => Assert.Throws<ArgumentNullException>(() => new Simplify(null, null));

    [Test]
    public void ExecuteTest()
    {
        var simplifier = Substitute.For<ISimplifier>();
        simplifier.Analyze(Arg.Any<Sin>()).Returns(info => info.Arg<Sin>());

        var lambda = new Sin(Variable.X).ToLambda(Variable.X.Name);
        var exp = new Simplify(simplifier, lambda.AsExpression());

        Assert.That(exp.Execute(), Is.EqualTo(lambda));
    }

    [Test]
    public void ExecuteNonLambdaTest()
    {
        var simplifier = Substitute.For<ISimplifier>();
        var simplify = new Simplify(simplifier, Number.One);

        Assert.Throws<ResultIsNotSupportedException>(() => simplify.Execute());
    }

    [Test]
    public void ExecuteNullTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Simplify(null, new Sin(Variable.X)).Execute());
    }

    [Test]
    public void CloneTest()
    {
        var exp = new Simplify(new Simplifier(), new Sin(Variable.X));
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}