// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Programming;

public class ConditionalOrTest
{
    [Test]
    public void CalculateOrTrueTest1()
    {
        var parameters = new ExpressionParameters { new Parameter("x", 0) };
        var lessThen = new LessThan(Variable.X, new Number(10));
        var greaterThen = new GreaterThan(Variable.X, new Number(-10));
        var or = new ConditionalOr(lessThen, greaterThen);

        Assert.That((bool) or.Execute(parameters));
    }

    [Test]
    public void CalculateOrTrueTest2()
    {
        var parameters = new ExpressionParameters { new Parameter("x", 0) };
        var lessThen = new LessThan(Variable.X, new Number(-10));
        var greaterThen = new GreaterThan(Variable.X, new Number(-10));
        var or = new ConditionalOr(lessThen, greaterThen);

        Assert.That((bool) or.Execute(parameters));
    }

    [Test]
    public void ExecuteInvalidParametersTest()
    {
        var or = new ConditionalOr(Number.One, Number.Two);

        Assert.Throws<ExecutionException>(() => or.Execute());
    }

    [Test]
    public void CloneTest()
    {
        var lessThen = new LessThan(Variable.X, new Number(10));
        var greaterThen = new GreaterThan(Variable.X, new Number(10));
        var exp = new ConditionalOr(lessThen, greaterThen);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}