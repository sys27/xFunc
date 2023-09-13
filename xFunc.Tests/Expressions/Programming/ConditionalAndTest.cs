// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Programming;

public class ConditionalAndTest
{
    [Test]
    public void CalculateAndTrueTest()
    {
        var parameters = new ExpressionParameters { new Parameter("x", 0) };
        var lessThen = new LessThan(Variable.X, new Number(10));
        var greaterThen = new GreaterThan(Variable.X, new Number(-10));
        var and = new ConditionalAnd(lessThen, greaterThen);

        Assert.That((bool) and.Execute(parameters), Is.True);
    }

    [Test]
    public void CalculateAndFalseTest()
    {
        var parameters = new ExpressionParameters { new Parameter("x", 0) };
        var lessThen = new LessThan(Variable.X, new Number(10));
        var greaterThen = new GreaterThan(Variable.X, new Number(10));
        var and = new ConditionalAnd(lessThen, greaterThen);

        Assert.That((bool) and.Execute(parameters), Is.False);
    }

    [Test]
    public void CalculateInvalidParametersTest()
    {
        var and = new ConditionalAnd(Number.One, Number.Two);

        Assert.Throws<ExecutionException>(() => and.Execute());
    }

    [Test]
    public void CloneTest()
    {
        var lessThen = new LessThan(Variable.X, new Number(10));
        var greaterThen = new GreaterThan(Variable.X, new Number(10));
        var exp = new ConditionalAnd(lessThen, greaterThen);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}