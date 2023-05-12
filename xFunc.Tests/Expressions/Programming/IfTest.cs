// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;

namespace xFunc.Tests.Expressions.Programming;

public class IfTest : BaseExpressionTests
{
    [Fact]
    public void CtorMaxParametersExceeded()
        => Assert.Throws<ArgumentException>(() => new If(new IExpression[]
        {
            Variable.X,
            Variable.X,
            Variable.X,
            Variable.X,
            Variable.X,
        }.ToImmutableArray()));

    [Fact]
    public void CalculateIfElseTest()
    {
        var parameters = new ExpressionParameters { new Parameter("x", 10) };

        var cond = new Equal(Variable.X, new Number(10));
        var @if = new If(cond, new Number(20), Number.Zero);

        Assert.Equal(new NumberValue(20.0), @if.Execute(parameters));

        parameters["x"] = new NumberValue(0.0);

        Assert.Equal(new NumberValue(0.0), @if.Execute(parameters));
    }

    [Fact]
    public void CalculateIfElseNegativeNumberTest()
    {
        var parameters = new ExpressionParameters { new Parameter("x", 0) };

        var cond = new Equal(Variable.X, Number.Zero);
        var @if = new If(cond, Number.One, new UnaryMinus(Number.One));

        Assert.Equal(new NumberValue(1.0), @if.Execute(parameters));

        parameters["x"] = new NumberValue(10);

        Assert.Equal(new NumberValue(-1.0), @if.Execute(parameters));
    }

    [Fact]
    public void CalculateIfTest()
    {
        var parameters = new ExpressionParameters { new Parameter("x", 10) };

        var cond = new Equal(Variable.X, new Number(10));
        var @if = new If(cond, new Number(20));

        Assert.Equal(new NumberValue(20.0), @if.Execute(parameters));
    }

    [Fact]
    public void CalculateElseTest()
    {
        var parameters = new ExpressionParameters { new Parameter("x", 0) };

        var cond = new Equal(Variable.X, new Number(10));
        var @if = new If(cond, new Number(20));
        var expected = new NumberValue(0.0);

        Assert.Equal(expected, @if.Execute(parameters));
    }

    [Fact]
    public void CloneTest()
    {
        var exp = new If(new Equal(Variable.X, new Number(10)), new Number(3), Number.Two);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }

    [Fact]
    public void ConditionIsNotBoolTest()
    {
        var exp = new If(Number.One, Number.One, Number.One);

        TestNotSupported(exp);
    }
}