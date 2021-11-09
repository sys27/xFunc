// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Moq;

namespace xFunc.Tests.Expressions;

public class SimplifyTest
{
    [Fact]
    public void SimplifierNull()
        => Assert.Throws<ArgumentNullException>(() => new Simplify(null, null));

    [Fact]
    public void ExecuteTest()
    {
        var mock = new Mock<ISimplifier>();
        mock
            .Setup(x => x.Analyze(It.IsAny<Simplify>()))
            .Returns<IExpression>(x => x);

        var exp = new Simplify(mock.Object, new Sin(Variable.X));

        Assert.Equal(exp, exp.Execute());
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