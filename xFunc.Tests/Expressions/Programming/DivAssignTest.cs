// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Programming;

public class DivAssignTest
{
    [Fact]
    public void DivAssignCalc()
    {
        var parameters = new ParameterCollection { new Parameter("x", 10) };
        var div = new DivAssign(Variable.X, Number.Two);
        var result = div.Execute(parameters);
        var expected = new NumberValue(5.0);

        Assert.Equal(expected, result);
        Assert.Equal(expected, parameters["x"]);
    }

    [Fact]
    public void DivAssignAsExpressionTest()
    {
        var parameters = new ParameterCollection { new Parameter("x", 10) };
        var add = new Add(new DivAssign(Variable.X, Number.Two), Number.Two);
        var result = add.Execute(parameters);

        Assert.Equal(new NumberValue(7.0), result);
        Assert.Equal(new NumberValue(5.0), parameters["x"]);
    }

    [Fact]
    public void DivNullParameters()
    {
        var exp = new DivAssign(Variable.X, Number.One);

        Assert.Throws<ArgumentNullException>(() => exp.Execute());
    }

    [Fact]
    public void DivValueBoolParameters()
    {
        var exp = new DivAssign(Variable.X, Bool.False);
        var parameters = new ParameterCollection { new Parameter("x", 1) };

        Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute(parameters));
    }

    [Fact]
    public void BoolDivNumberTest()
    {
        var parameters = new ParameterCollection { new Parameter("x", true) };
        var add = new DivAssign(Variable.X, Number.Two);

        Assert.Throws<ResultIsNotSupportedException>(() => add.Execute(parameters));
    }

    [Fact]
    public void SameEqualsTest()
    {
        var exp = new DivAssign(Variable.X, Number.One);

        Assert.True(exp.Equals(exp));
    }

    [Fact]
    public void EqualsNullTest()
    {
        var exp = new DivAssign(Variable.X, Number.One);

        Assert.False(exp.Equals(null));
    }

    [Fact]
    public void EqualsDifferentTypeTest()
    {
        var exp1 = new DivAssign(Variable.X, Number.One);
        var exp2 = new MulAssign(Variable.X, Number.One);

        Assert.False(exp1.Equals(exp2));
    }

    [Fact]
    public void NullAnalyzerTest1()
    {
        var exp = new DivAssign(Variable.X, Number.One);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Fact]
    public void NullAnalyzerTest2()
    {
        var exp = new DivAssign(Variable.X, Number.One);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }

    [Fact]
    public void CloneTest()
    {
        var exp = new DivAssign(Variable.X, Number.Two);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}