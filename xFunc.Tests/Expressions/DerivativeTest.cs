// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Moq;

namespace xFunc.Tests.Expressions;

public class DerivativeTest
{
    [Fact]
    public void DifferentiatorNull()
        => Assert.Throws<ArgumentNullException>(() => new Derivative(null, null, Variable.X));

    [Fact]
    public void SimplifierNull()
    {
        var differentiator = new Mock<IDifferentiator>().Object;

        Assert.Throws<ArgumentNullException>(() => new Derivative(differentiator, null, Variable.X));
    }

    [Fact]
    public void ExecutePointTest()
    {
        var differentiator = new Mock<IDifferentiator>();
        differentiator
            .Setup(d => d.Analyze(It.IsAny<Variable>(), It.IsAny<DifferentiatorContext>()))
            .Returns<Variable, DifferentiatorContext>((exp, _) => exp);

        var simplifier = new Mock<ISimplifier>();

        var deriv = new Derivative(
            differentiator.Object,
            simplifier.Object,
            Variable.X.ToLambdaExpression(),
            Variable.X,
            Number.Two);

        Assert.Equal(new NumberValue(2.0), deriv.Execute());
    }

    [Fact]
    public void ExecuteNonLambdaTest()
    {
        var differentiator = new Mock<IDifferentiator>();
        var simplifier = new Mock<ISimplifier>();
        var derivative = new Derivative(
            differentiator.Object,
            simplifier.Object,
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