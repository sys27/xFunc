// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Programming;

public class WhileTest
{
    [Test]
    public void EqualsSameInstanceTest()
    {
        var body = new AddAssign(Variable.X, Number.Two);
        var cond = new LessThan(Variable.X, new Number(10));
        var loop = new While(body, cond);

        Assert.That(loop.Equals(loop), Is.True);
    }

    [Test]
    public void EqualsNullTest()
    {
        var body = new AddAssign(Variable.X, Number.Two);
        var cond = new LessThan(Variable.X, new Number(10));
        var loop = new While(body, cond);

        Assert.That(loop.Equals(null), Is.False);
    }

    [Test]
    public void EqualsDiffTypeTest()
    {
        var body = new AddAssign(Variable.X, Number.Two);
        var cond = new LessThan(Variable.X, new Number(10));
        var loop = new While(body, cond);

        Assert.That(loop.Equals(new object()), Is.False);
    }

    [Test]
    public void EqualsSameObjectTest()
    {
        var body = new AddAssign(Variable.X, Number.Two);
        var cond = new LessThan(Variable.X, new Number(10));
        var loop1 = new While(body, cond);
        var loop2 = new While(body, cond);

        Assert.That(loop1.Equals(loop2), Is.True);
    }

    [Test]
    public void EqualsDiffBodyTest()
    {
        var cond = new LessThan(Variable.X, new Number(10));
        var loop1 = new While(new AddAssign(Variable.X, Number.Two), cond);
        var loop2 = new While(new AddAssign(Variable.Y, Number.Two), cond);

        Assert.That(loop1.Equals(loop2), Is.False);
    }

    [Test]
    public void EqualsDiffConditionTest()
    {
        var body = new AddAssign(Variable.X, Number.Two);
        var loop1 = new While(body, new LessThan(Variable.X, new Number(10)));
        var loop2 = new While(body, new LessThan(Variable.Y, new Number(10)));

        Assert.That(loop1.Equals(loop2), Is.False);
    }

    [Test]
    public void EqualsDiffBodyAndConditionTest()
    {
        var loop1 = new While(new AddAssign(Variable.X, Number.Two), new LessThan(Variable.X, new Number(10)));
        var loop2 = new While(new AddAssign(Variable.Y, Number.Two), new LessThan(Variable.Y, new Number(10)));

        Assert.That(loop1.Equals(loop2), Is.False);
    }

    [Test]
    public void ExecuteWhileTest()
    {
        var parameters = new ExpressionParameters
        {
            new Parameter("x", 0)
        };

        var body = new AddAssign(Variable.X, Number.Two);
        var cond = new LessThan(Variable.X, new Number(10));
        var loop = new While(body, cond);

        var result = loop.Execute(parameters);

        Assert.That(parameters["x"].Value, Is.EqualTo(new NumberValue(10.0)));
        Assert.That(result, Is.EqualTo(EmptyValue.Instance));
    }

    [Test]
    public void ExecuteNonBoolTest()
    {
        var body = new AddAssign(Variable.X, Number.Two);
        var cond = new Number(10);
        var loop = new While(body, cond);

        Assert.Throws<ExecutionException>(() => loop.Execute());
    }

    [Test]
    public void NullAnalyzerTest1()
    {
        var body = new AddAssign(Variable.X, Number.Two);
        var cond = new LessThan(Variable.X, new Number(10));
        var loop = new While(body, cond);

        Assert.Throws<ArgumentNullException>(() => loop.Analyze<string>(null));
    }

    [Test]
    public void NullAnalyzerTest2()
    {
        var body = new AddAssign(Variable.X, Number.Two);
        var cond = new LessThan(Variable.X, new Number(10));
        var loop = new While(body, cond);

        Assert.Throws<ArgumentNullException>(() => loop.Analyze<string, object>(null, null));
    }

    [Test]
    public void CloneTest()
    {
        var body = new Assign(Variable.X, new Add(Variable.X, Number.Two));
        var cond = new LessThan(Variable.X, new Number(10));

        var exp = new While(body, cond);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}