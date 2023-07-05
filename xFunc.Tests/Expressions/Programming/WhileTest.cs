// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Programming;

public class WhileTest
{
    [Fact]
    public void CalculateWhileTest()
    {
        var parameters = new ExpressionParameters();
        parameters.Add(new Parameter("x", 0));

        var body = new Assign(Variable.X, new Add(Variable.X, Number.Two));
        var cond = new LessThan(Variable.X, new Number(10));

        var @while = new While(body, cond);
        @while.Execute(parameters);

        Assert.Equal(new NumberValue(10.0), parameters["x"]);
    }

    [Fact]
    public void CloneTest()
    {
        var body = new Assign(Variable.X, new Add(Variable.X, Number.Two));
        var cond = new LessThan(Variable.X, new Number(10));

        var exp = new While(body, cond);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}