// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Programming;

public class SubAssignTest
{
    [Fact]
    public void SubAssignCalc()
    {
        var parameters = new ExpressionParameters { new Parameter("x", 10) };
        var sub = new SubAssign(Variable.X, Number.Two);
        var result = sub.Execute(parameters);
        var expected = new NumberValue(8.0);

        Assert.Equal(expected, result);
        Assert.Equal(expected, parameters["x"]);
    }

    [Fact]
    public void SubAssignAsExpressionTest()
    {
        var parameters = new ExpressionParameters { new Parameter("x", 10) };
        var add = new Add(Number.One, new SubAssign(Variable.X, Number.Two));
        var result = add.Execute(parameters);

        Assert.Equal(new NumberValue(9.0), result);
        Assert.Equal(new NumberValue(8.0), parameters["x"]);
    }

    [Fact]
    public void SubNullParameters()
    {
        var exp = new SubAssign(Variable.X, Number.One);

        Assert.Throws<ArgumentNullException>(() => exp.Execute());
    }

    [Fact]
    public void SubValueBoolParameters()
    {
        var exp = new SubAssign(Variable.X, Bool.False);
        var parameters = new ExpressionParameters { new Parameter("x", 1) };

        Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute(parameters));
    }

    [Fact]
    public void BoolSubNumberTest()
    {
        var parameters = new ExpressionParameters { new Parameter("x", true) };
        var add = new SubAssign(Variable.X, Number.Two);

        Assert.Throws<ResultIsNotSupportedException>(() => add.Execute(parameters));
    }

    [Fact]
    public void SameEqualsTest()
    {
        var exp = new SubAssign(Variable.X, Number.One);

        Assert.True(exp.Equals(exp));
    }

    [Fact]
    public void EqualsNullTest()
    {
        var exp = new SubAssign(Variable.X, Number.One);

        Assert.False(exp.Equals(null));
    }

    [Fact]
    public void EqualsDifferentTypeTest()
    {
        var exp1 = new SubAssign(Variable.X, Number.One);
        var exp2 = new DivAssign(Variable.X, Number.One);

        Assert.False(exp1.Equals(exp2));
    }

    [Fact]
    public void NullAnalyzerTest1()
    {
        var exp = new SubAssign(Variable.X, Number.One);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Fact]
    public void NullAnalyzerTest2()
    {
        var exp = new SubAssign(Variable.X, Number.One);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }

    [Fact]
    public void CloneTest()
    {
        var exp = new SubAssign(Variable.X, Number.Two);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}