// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Programming;

public class IncTest
{
    [Fact]
    public void NullCtorTest()
        => Assert.Throws<ArgumentNullException>(() => new Inc(null));

    [Fact]
    public void IncCalcTest()
    {
        var parameters = new ParameterCollection { new Parameter("x", 10) };
        var inc = new Inc(Variable.X);
        var result = (NumberValue)inc.Execute(parameters);
        var expected = new NumberValue(11.0);

        Assert.Equal(expected, result);
        Assert.Equal(expected, parameters["x"]);
    }

    [Fact]
    public void IncAsExpExecuteTest()
    {
        var parameters = new ParameterCollection { new Parameter("x", 10) };
        var inc = new Add(Number.One, new Inc(Variable.X));
        var result = (NumberValue)inc.Execute(parameters);

        Assert.Equal(new NumberValue(12.0), result);
        Assert.Equal(new NumberValue(11.0), parameters["x"]);
    }

    [Fact]
    public void IncNullParameters()
    {
        Assert.Throws<ArgumentNullException>(() => new Inc(Variable.X).Execute());
    }

    [Fact]
    public void IncBoolTest()
    {
        var parameters = new ParameterCollection { new Parameter("x", true) };
        var inc = new Inc(Variable.X);

        Assert.Throws<ResultIsNotSupportedException>(() => inc.Execute(parameters));
    }

    [Fact]
    public void SameEqualsTest()
    {
        var inc = new Inc(Variable.X);

        Assert.True(inc.Equals(inc));
    }

    [Fact]
    public void EqualsNullTest()
    {
        var inc = new Inc(Variable.X);

        Assert.False(inc.Equals(null));
    }

    [Fact]
    public void EqualsDifferentTypeTest()
    {
        var inc = new Inc(Variable.X);
        var dec = new Dec(Variable.X);

        Assert.False(inc.Equals(dec));
    }

    [Fact]
    public void NullAnalyzerTest1()
    {
        var inc = new Inc(Variable.X);

        Assert.Throws<ArgumentNullException>(() => inc.Analyze<string>(null));
    }

    [Fact]
    public void NullAnalyzerTest2()
    {
        var inc = new Inc(Variable.X);

        Assert.Throws<ArgumentNullException>(() => inc.Analyze<string, object>(null, null));
    }

    [Fact]
    public void CloneTest()
    {
        var exp = new Inc(Variable.X);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}