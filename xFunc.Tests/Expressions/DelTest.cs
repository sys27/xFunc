// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using NSubstitute;

namespace xFunc.Tests.Expressions;

public class DelTest
{
    [Fact]
    public void DifferentiatorNull()
        => Assert.Throws<ArgumentNullException>(() => new Del(null, null, null));

    [Fact]
    public void SimplifierNull()
    {
        var differentiator = Substitute.For<IDifferentiator>();

        Assert.Throws<ArgumentNullException>(() => new Del(differentiator, null, null));
    }

    [Fact]
    public void ExecuteTest1()
    {
        var exp = new Add(
            new Add(
                new Mul(Number.Two, Variable.X),
                new Pow(Variable.Y, Number.Two)
            ),
            new Pow(new Variable("z"), new Number(3))
        ).ToLambdaExpression();
        var del = new Del(new Differentiator(), new Simplifier(), exp);

        var expected = new Vector(new IExpression[]
        {
            Number.Two,
            new Mul(Number.Two, Variable.Y),
            new Mul(new Number(3), new Pow(new Variable("z"), Number.Two))
        }).ToLambda();

        Assert.Equal(expected, del.Execute());
    }

    [Fact]
    public void ExecuteTest2()
    {
        var exp = new Add(
            new Add(new Mul(Number.Two, new Variable("x1")), new Pow(new Variable("x2"), Number.Two)),
            new Pow(new Variable("x3"), new Number(3))
        ).ToLambdaExpression();
        var del = new Del(new Differentiator(), new Simplifier(), exp);

        var expected = new Vector(new IExpression[]
        {
            Number.Two,
            new Mul(Number.Two, new Variable("x2")),
            new Mul(new Number(3), new Pow(new Variable("x3"), Number.Two))
        }).ToLambda();

        Assert.Equal(expected, del.Execute());
    }

    [Fact]
    public void ExecuteNonLambdaTest()
    {
        var differentiator = Substitute.For<IDifferentiator>();
        var simplifier = Substitute.For<ISimplifier>();
        var del = new Del(
            differentiator,
            simplifier,
            Number.One);

        Assert.Throws<ResultIsNotSupportedException>(() => del.Execute());
    }

    [Fact]
    public void NullDiffTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Del(null, null, Variable.X));
    }

    [Fact]
    public void NullSimplifierTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Del(new Differentiator(), null, Variable.X));
    }

    [Fact]
    public void CloneTest()
    {
        var exp = new Add(
            new Add(new Mul(Number.Two, new Variable("x1")), new Pow(new Variable("x2"), Number.Two)),
            new Pow(new Variable("x3"), new Number(3))
        );
        var del = new Del(new Differentiator(), new Simplifier(), exp);
        var clone = del.Clone();

        Assert.Equal(del, clone);
    }
}