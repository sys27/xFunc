// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;

namespace xFunc.Tests.Expressions.Programming;

public class ForTest
{
    [Test]
    public void ExecuteTest()
    {
        var parameters = new ExpressionParameters();

        var init = new Assign(new Variable("i"), Number.Zero);
        var cond = new LessThan(new Variable("i"), new Number(10));
        var iter = new AddAssign(new Variable("i"), Number.One);

        var loop = new For(new Variable("i"), init, cond, iter);
        var result = loop.Execute(parameters);

        Assert.That(parameters["i"].Value, Is.EqualTo(new NumberValue(10.0)));
        Assert.That(result, Is.Null);
    }

    [Test]
    public void ExecuteNonBoolTest()
    {
        var init = new Assign(new Variable("i"), Number.Zero);
        var cond = new Number(10);
        var iter = new AddAssign(new Variable("i"), Number.One);

        var loop = new For(new Variable("i"), init, cond, iter);

        Assert.Throws<ExecutionException>(() => loop.Execute());
    }

    [Test]
    public void CloneTest()
    {
        var i = new Variable("i");
        var init = new Assign(i, Number.Zero);
        var cond = new LessThan(i, new Number(10));
        var iter = new Assign(i, new Add(i, Number.One));

        var exp = new For(i, init, cond, iter);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }

    [Test]
    public void CloneTest2()
    {
        var i = new Variable("i");
        var init = new Assign(i, Number.Zero);
        var cond = new LessThan(i, new Number(10));
        var iter = new Assign(i, new Add(i, Number.One));
        var exp = new For(i, init, cond, iter);

        var clone = exp.Clone(new IExpression[] { Variable.X, init, cond, iter }.ToImmutableArray());
        var expected = new For(Variable.X, init, cond, iter);

        Assert.That(clone, Is.EqualTo(expected));
    }
}