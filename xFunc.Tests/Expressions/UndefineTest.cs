// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class UndefineTest
{
    [Fact]
    public void ExecuteFailTest()
    {
        Assert.Throws<NotSupportedException>(() => new Undefine(Variable.X).Execute());
    }

    [Fact]
    public void ExecuteParamNullTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Undefine(Variable.X).Execute(null));
    }

    [Fact]
    public void KeyNullTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Undefine(null));
    }

    [Fact]
    public void EqualRefTest()
    {
        var exp = new Undefine(Variable.X);

        Assert.True(exp.Equals(exp));
    }

    [Fact]
    public void EqualDiffTypesTest()
    {
        var exp1 = new Undefine(Variable.X);
        var exp2 = Number.Two;

        Assert.False(exp1.Equals(exp2));
    }

    [Fact]
    public void EqualTest()
    {
        var exp1 = new Undefine(Variable.X);
        var exp2 = new Undefine(Variable.X);

        Assert.True(exp1.Equals(exp2));
    }

    [Fact]
    public void Equal2Test()
    {
        var exp1 = new Undefine(Variable.X);
        var exp2 = new Undefine(Variable.Y);

        Assert.False(exp1.Equals(exp2));
    }

    [Fact]
    public void UndefVarTest()
    {
        var parameters = new ExpressionParameters { { "a", new NumberValue(1) } };

        var undef = new Undefine(new Variable("a"));
        undef.Execute(parameters);
        Assert.False(parameters.ContainsKey("a"));
    }

    [Fact]
    public void UndefFuncTest()
    {
        var lambda = new Lambda(new[] { "x" }, Variable.X);
        var parameters = new ExpressionParameters
        {
            { Variable.X.Name, lambda }
        };
        var undef = new Undefine(Variable.X);

        var result = undef.Execute(parameters);

        Assert.Equal("'x' is removed.", result);
    }

    [Fact]
    public void UndefConstTest()
    {
        var parameters = new ExpressionParameters();

        var undef = new Undefine(new Variable("π"));

        Assert.Throws<ArgumentException>(() => undef.Execute(parameters));
    }

    [Fact]
    public void CloneTest()
    {
        var exp = new Undefine(Variable.X);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }

    [Fact]
    public void NullAnalyzerTest1()
    {
        var exp = new Undefine(Variable.X);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Fact]
    public void NullAnalyzerTest2()
    {
        var exp = new Undefine(Variable.X);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }
}