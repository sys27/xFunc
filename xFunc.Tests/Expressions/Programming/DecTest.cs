// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Programming;

public class DecTest
{
    [Fact]
    public void NullCtorTest()
        => Assert.Throws<ArgumentNullException>(() => new Dec(null));

    [Fact]
    public void DecCalcTest()
    {
        var parameters = new ParameterCollection { new Parameter("x", 10) };
        var dec = new Dec(Variable.X);
        var result = (NumberValue)dec.Execute(parameters);
        var expected = new NumberValue(9.0);

        Assert.Equal(expected, result);
        Assert.Equal(expected, parameters["x"]);
    }

    [Fact]
    public void DecAsExpExecuteTest()
    {
        var parameters = new ParameterCollection { new Parameter("x", 10) };
        var inc = new Add(Number.One, new Dec(Variable.X));
        var result = (NumberValue)inc.Execute(parameters);

        Assert.Equal(new NumberValue(10.0), result);
        Assert.Equal(new NumberValue(9.0), parameters["x"]);
    }

    [Fact]
    public void DecNullParameters()
    {
        Assert.Throws<ArgumentNullException>(() => new Dec(Variable.X).Execute());
    }

    [Fact]
    public void DecBoolTest()
    {
        var parameters = new ParameterCollection { new Parameter("x", true) };
        var dec = new Dec(Variable.X);

        Assert.Throws<ResultIsNotSupportedException>(() => dec.Execute(parameters));
    }

    [Fact]
    public void SameEqualsTest()
    {
        var dec = new Dec(Variable.X);

        Assert.True(dec.Equals(dec));
    }

    [Fact]
    public void EqualsNullTest()
    {
        var dec = new Dec(Variable.X);

        Assert.False(dec.Equals(null));
    }

    [Fact]
    public void EqualsDifferentTypeTest()
    {
        var dec = new Dec(Variable.X);
        var inc = new Inc(Variable.X);

        Assert.False(dec.Equals(inc));
    }

    [Fact]
    public void NullAnalyzerTest1()
    {
        var inc = new Dec(Variable.X);

        Assert.Throws<ArgumentNullException>(() => inc.Analyze<string>(null));
    }

    [Fact]
    public void NullAnalyzerTest2()
    {
        var inc = new Dec(Variable.X);

        Assert.Throws<ArgumentNullException>(() => inc.Analyze<string, object>(null, null));
    }

    [Fact]
    public void CloneTest()
    {
        var exp = new Dec(Variable.X);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}