// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using NSubstitute;

namespace xFunc.Tests.Expressions;

public class SimplifyTest
{
    [Fact]
    public void SimplifierNull()
        => Assert.Throws<ArgumentNullException>(() => new Simplify(null, null));

    [Fact]
    public void ExecuteTest()
    {
        var simplifier = Substitute.For<ISimplifier>();
        simplifier.Analyze(Arg.Any<Sin>()).Returns(info => info.Arg<Sin>());

        var lambda = new Sin(Variable.X).ToLambda(Variable.X.Name);
        var exp = new Simplify(simplifier, lambda.AsExpression());

        Assert.Equal(lambda, exp.Execute());
    }

    [Fact]
    public void ExecuteNonLambdaTest()
    {
        var simplifier = Substitute.For<ISimplifier>();
        var simplify = new Simplify(simplifier, Number.One);

        Assert.Throws<ResultIsNotSupportedException>(() => simplify.Execute());
    }

    [Fact]
    public void ExecuteNullTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Simplify(null, new Sin(Variable.X)).Execute());
    }

    [Fact]
    public void CloneTest()
    {
        var exp = new Simplify(new Simplifier(), new Sin(Variable.X));
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}