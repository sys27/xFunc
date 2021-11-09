// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Programming;

public class LessThanTest
{
    [Fact]
    public void CalculateLessTrueTest()
    {
        var parameters = new ParameterCollection { new Parameter("x", 0) };
        var lessThen = new LessThan(Variable.X, new Number(10));

        Assert.True((bool)lessThen.Execute(parameters));
    }

    [Fact]
    public void CalculateLessFalseTest()
    {
        var parameters = new ParameterCollection { new Parameter("x", 10) };
        var lessThen = new LessThan(Variable.X, new Number(10));

        Assert.False((bool)lessThen.Execute(parameters));
    }

    [Fact]
    public void LessAngleTest()
    {
        var exp = new LessThan(
            AngleValue.Degree(10).AsExpression(),
            AngleValue.Degree(12).AsExpression()
        );
        var result = (bool)exp.Execute();

        Assert.True(result);
    }

    [Fact]
    public void LessPowerTest()
    {
        var exp = new LessThan(
            PowerValue.Watt(10).AsExpression(),
            PowerValue.Watt(12).AsExpression()
        );
        var result = (bool)exp.Execute();

        Assert.True(result);
    }

    [Fact]
    public void CalculateInvalidTypeTest()
    {
        var lessThen = new LessThan(Bool.True, Bool.True);

        Assert.Throws<ResultIsNotSupportedException>(() => lessThen.Execute());
    }

    [Fact]
    public void CloneTest()
    {
        var exp = new LessThan(Number.Two, new Number(3));
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}