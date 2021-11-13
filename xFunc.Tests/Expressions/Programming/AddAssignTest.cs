// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Programming;

public class AddAssignTest
{
    [Fact]
    public void NullVariableTest()
    {
        Assert.Throws<ArgumentNullException>(() => new AddAssign(null, null));
    }

    [Fact]
    public void NullExpTest()
    {
        Assert.Throws<ArgumentNullException>(() => new AddAssign(Variable.X, null));
    }

    [Fact]
    public void AddAssignCalc()
    {
        var parameters = new ParameterCollection { new Parameter("x", 10) };
        var add = new AddAssign(Variable.X, Number.Two);
        var result = add.Execute(parameters);
        var expected = new NumberValue(12.0);

        Assert.Equal(expected, result);
        Assert.Equal(expected, parameters["x"]);
    }

    [Fact]
    public void AddAssignAsExpressionTest()
    {
        var parameters = new ParameterCollection { new Parameter("x", 10) };
        var add = new Add(Number.One, new AddAssign(Variable.X, Number.Two));
        var result = add.Execute(parameters);

        Assert.Equal(new NumberValue(13.0), result);
        Assert.Equal(new NumberValue(12.0), parameters["x"]);
    }

    [Fact]
    public void AddNullParameters()
    {
        var exp = new AddAssign(Variable.X, Number.One);

        Assert.Throws<ArgumentNullException>(() => exp.Execute());
    }

    [Fact]
    public void SubValueBoolParameters()
    {
        var exp = new AddAssign(Variable.X, Bool.False);
        var parameters = new ParameterCollection { new Parameter("x", 1) };

        Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute(parameters));
    }

    [Fact]
    public void BoolAddNumberTest()
    {
        var parameters = new ParameterCollection { new Parameter("x", true) };
        var add = new AddAssign(Variable.X, Number.Two);

        Assert.Throws<ResultIsNotSupportedException>(() => add.Execute(parameters));
    }

    [Fact]
    public void SameEqualsTest()
    {
        var exp = new AddAssign(Variable.X, Number.One);

        Assert.True(exp.Equals(exp));
    }

    [Fact]
    public void EqualsNullTest()
    {
        var exp = new AddAssign(Variable.X, Number.One);

        Assert.False(exp.Equals(null));
    }

    [Fact]
    public void EqualsDifferentTypeTest()
    {
        var exp1 = new AddAssign(Variable.X, Number.One);
        var exp2 = new SubAssign(Variable.X, Number.One);

        Assert.False(exp1.Equals(exp2));
    }

    [Fact]
    public void NullAnalyzerTest1()
    {
        var exp = new AddAssign(Variable.X, Number.One);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Fact]
    public void NullAnalyzerTest2()
    {
        var exp = new AddAssign(Variable.X, Number.One);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }

    [Fact]
    public void CloneTest()
    {
        var exp = new AddAssign(Variable.X, Number.Two);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}