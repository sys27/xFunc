// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Programming;

public class LeftShiftAssignTest
{
    [Fact]
    public void ExecuteTest()
    {
        var exp = new LeftShiftAssign(Variable.X, new Number(10));
        var parameters = new ParameterCollection
        {
            new Parameter("x", 1.0)
        };
        var actual = exp.Execute(parameters);
        var expected = new NumberValue(1024.0);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteAsExpressionTest()
    {
        var exp = new Add(Number.One, new LeftShiftAssign(Variable.X, new Number(10)));
        var parameters = new ParameterCollection
        {
            new Parameter("x", 1.0)
        };
        var actual = exp.Execute(parameters);

        Assert.Equal(new NumberValue(1025.0), actual);
        Assert.Equal(new NumberValue(1024.0), parameters["x"]);
    }

    [Fact]
    public void ExecuteNullParamsTest()
    {
        var exp = new LeftShiftAssign(Variable.X, new Number(10));

        Assert.Throws<ArgumentNullException>(() => exp.Execute());
    }

    [Fact]
    public void ExecuteDoubleLeftTest()
    {
        var exp = new LeftShiftAssign(Variable.X, new Number(10));
        var parameters = new ParameterCollection
        {
            new Parameter("x", 1.5)
        };

        Assert.Throws<ArgumentException>(() => exp.Execute(parameters));
    }

    [Fact]
    public void ExecuteDoubleRightTest()
    {
        var exp = new LeftShiftAssign(Variable.X, new Number(10.1));
        var parameters = new ParameterCollection
        {
            new Parameter("x", 1.0)
        };

        Assert.Throws<ArgumentException>(() => exp.Execute(parameters));
    }

    [Fact]
    public void ExecuteBoolRightTest()
    {
        var exp = new LeftShiftAssign(Variable.X, Bool.True);
        var parameters = new ParameterCollection
        {
            new Parameter("x", 1.0)
        };

        Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute(parameters));
    }

    [Fact]
    public void ExecuteBoolLeftTest()
    {
        var exp = new LeftShiftAssign(Variable.X, Bool.True);
        var parameters = new ParameterCollection
        {
            new Parameter("x", false)
        };

        Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute(parameters));
    }

    [Fact]
    public void CloneTest()
    {
        var exp = new LeftShiftAssign(Variable.X, new Number(10));
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}