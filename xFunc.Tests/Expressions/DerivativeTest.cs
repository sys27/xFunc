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
            .Setup(d => d.Analyze(It.IsAny<Derivative>(), It.IsAny<DifferentiatorContext>()))
            .Returns<Derivative, DifferentiatorContext>((exp, context) => exp.Expression);

        var simplifier = new Mock<ISimplifier>();

        var deriv = new Derivative(
            differentiator.Object,
            simplifier.Object,
            Variable.X,
            Variable.X,
            Number.Two);

        Assert.Equal(new NumberValue(2.0), deriv.Execute());
    }

    [Fact]
    public void ExecuteNullDerivTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Derivative(null, null, Variable.X));
    }

    [Fact]
    public void ExecuteNullSimpTest()
    {
        var differentiator = new Mock<IDifferentiator>();
        differentiator
            .Setup(d => d.Analyze(It.IsAny<Derivative>(), It.IsAny<DifferentiatorContext>()))
            .Returns<Derivative, DifferentiatorContext>((e, context) => e.Expression);

        var simplifier = new Mock<ISimplifier>();

        var exp = new Derivative(differentiator.Object, simplifier.Object, Variable.X);

        var result = exp.Execute();

        Assert.Equal(result, result);
    }

    [Fact]
    public void CloneTest()
    {
        var exp = new Derivative(new Differentiator(), new Simplifier(), new Sin(Variable.X), Variable.X, Number.One);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}