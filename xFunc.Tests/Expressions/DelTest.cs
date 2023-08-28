// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using NSubstitute;

namespace xFunc.Tests.Expressions;

public class DelTest
{
    [Test]
    public void DifferentiatorNull()
        => Assert.Throws<ArgumentNullException>(() => new Del(null, null, null));

    [Test]
    public void SimplifierNull()
    {
        var differentiator = Substitute.For<IDifferentiator>();

        Assert.Throws<ArgumentNullException>(() => new Del(differentiator, null, null));
    }

    [Test]
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

        Assert.That(del.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteTest2()
    {
        var exp = new Add(
            new Add(new Mul(Number.Two, new Variable("x1")), new Pow(new Variable("x2"), Number.Two)),
            new UnaryMinus(new Variable("x3"))
        ).ToLambdaExpression();
        var del = new Del(new Differentiator(), new Simplifier(), exp);

        var expected = new Vector(new IExpression[]
        {
            Number.Two,
            new Mul(Number.Two, new Variable("x2")),
            new Number(-1),
        }).ToLambda();

        Assert.That(del.Execute(), Is.EqualTo(expected));
    }

    [Test]
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

    [Test]
    public void NullDiffTest()
        => Assert.Throws<ArgumentNullException>(() => new Del(null, null, Variable.X));

    [Test]
    public void NullSimplifierTest()
        => Assert.Throws<ArgumentNullException>(() => new Del(new Differentiator(), null, Variable.X));

    [Test]
    public void CloneTest()
    {
        var exp = new Add(
            new Add(new Mul(Number.Two, new Variable("x1")), new Pow(new Variable("x2"), Number.Two)),
            new Pow(new Variable("x3"), new Number(3))
        );
        var del = new Del(new Differentiator(), new Simplifier(), exp);
        var clone = del.Clone();

        Assert.That(clone, Is.EqualTo(del));
    }
}