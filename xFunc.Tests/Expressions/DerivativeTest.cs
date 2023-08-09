// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using NSubstitute;

namespace xFunc.Tests.Expressions;

public class DerivativeTest
{
    [Fact]
    public void DifferentiatorNull()
        => Assert.Throws<ArgumentNullException>(() => new Derivative(null, null, Variable.X));

    [Fact]
    public void SimplifierNull()
    {
        var differentiator = Substitute.For<IDifferentiator>();

        Assert.Throws<ArgumentNullException>(() => new Derivative(differentiator, null, Variable.X));
    }

    [Fact]
    public void ExecutePointTest()
    {
        var differentiator = Substitute.For<IDifferentiator>();
        differentiator
            .Analyze(Arg.Any<Variable>(), Arg.Any<DifferentiatorContext>())
            .Returns(info => info.Arg<Variable>());

        var simplifier = Substitute.For<ISimplifier>();

        var deriv = new Derivative(
            differentiator,
            simplifier,
            Variable.X.ToLambdaExpression(),
            Variable.X,
            Number.Two);

        Assert.Equal(new NumberValue(2.0), deriv.Execute());
    }

    [Fact]
    public void ExecuteNonLambdaTest()
    {
        var differentiator = Substitute.For<IDifferentiator>();
        var simplifier = Substitute.For<ISimplifier>();
        var derivative = new Derivative(
            differentiator,
            simplifier,
            Number.One);

        Assert.Throws<ResultIsNotSupportedException>(() => derivative.Execute());
    }

    [Fact]
    public void ExecuteNullDerivTest()
        => Assert.Throws<ArgumentNullException>(() => new Derivative(null, null, Variable.X));

    [Fact]
    public void CloneTest()
    {
        var exp = new Derivative(new Differentiator(), new Simplifier(), new Sin(Variable.X), Variable.X, Number.One);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}