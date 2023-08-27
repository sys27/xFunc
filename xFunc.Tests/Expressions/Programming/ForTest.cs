// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Programming;

public class ForTest
{
    [Test]
    public void CalculateForTest()
    {
        var parameters = new ExpressionParameters();

        var init = new Assign(new Variable("i"), Number.Zero);
        var cond = new LessThan(new Variable("i"), new Number(10));
        var iter = new Assign(new Variable("i"), new Add(new Variable("i"), Number.One));

        var @for = new For(new Variable("i"), init, cond, iter);
        @for.Execute(parameters);

        Assert.That(parameters["i"].Value, Is.EqualTo(new NumberValue(10.0)));
    }

    [Test]
    public void CloneTest()
    {
        var init = new Assign(new Variable("i"), Number.Zero);
        var cond = new LessThan(new Variable("i"), new Number(10));
        var iter = new Assign(new Variable("i"), new Add(new Variable("i"), Number.One));

        var exp = new For(new Variable("i"), init, cond, iter);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}