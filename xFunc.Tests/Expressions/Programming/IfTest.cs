// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;

namespace xFunc.Tests.Expressions.Programming;

public class IfTest : BaseExpressionTests
{
    [Test]
    public void CtorMaxParametersExceeded()
        => Assert.Throws<ArgumentException>(() => new If(new IExpression[]
        {
            Variable.X,
            Variable.X,
            Variable.X,
            Variable.X,
            Variable.X,
        }.ToImmutableArray()));

    [Test]
    public void CalculateIfElseTest()
    {
        var parameters = new ExpressionParameters { new Parameter("x", 10) };

        var cond = new Equal(Variable.X, new Number(10));
        var @if = new If(cond, new Number(20), Number.Zero);

        Assert.That(@if.Execute(parameters), Is.EqualTo(new NumberValue(20.0)));

        parameters["x"] = new NumberValue(0.0);

        Assert.That(@if.Execute(parameters), Is.EqualTo(new NumberValue(0.0)));
    }

    [Test]
    public void CalculateIfElseNegativeNumberTest()
    {
        var parameters = new ExpressionParameters { new Parameter("x", 0) };

        var cond = new Equal(Variable.X, Number.Zero);
        var @if = new If(cond, Number.One, new UnaryMinus(Number.One));

        Assert.That(@if.Execute(parameters), Is.EqualTo(new NumberValue(1.0)));

        parameters["x"] = new NumberValue(10);

        Assert.That(@if.Execute(parameters), Is.EqualTo(new NumberValue(-1.0)));
    }

    [Test]
    public void CalculateIfTest()
    {
        var parameters = new ExpressionParameters { new Parameter("x", 10) };

        var cond = new Equal(Variable.X, new Number(10));
        var @if = new If(cond, new Number(20));

        Assert.That(@if.Execute(parameters), Is.EqualTo(new NumberValue(20.0)));
    }

    [Test]
    public void CalculateElseTest()
    {
        var parameters = new ExpressionParameters { new Parameter("x", 0) };

        var cond = new Equal(Variable.X, new Number(10));
        var @if = new If(cond, new Number(20));
        var expected = new NumberValue(0.0);

        Assert.That(@if.Execute(parameters), Is.EqualTo(expected));
    }

    [Test]
    public void CloneTest()
    {
        var exp = new If(new Equal(Variable.X, new Number(10)), new Number(3), Number.Two);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }

    [Test]
    public void ConditionIsNotBoolTest()
    {
        var exp = new If(Number.One, Number.One, Number.One);

        TestNotSupported(exp);
    }
}