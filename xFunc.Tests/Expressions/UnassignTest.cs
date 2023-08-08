// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class UnassignTest
{
    [Fact]
    public void ExecuteFailTest()
    {
        Assert.Throws<NotSupportedException>(() => new Unassign(Variable.X).Execute());
    }

    [Fact]
    public void ExecuteParamNullTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Unassign(Variable.X).Execute(null));
    }

    [Fact]
    public void KeyNullTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Unassign(null));
    }

    [Fact]
    public void EqualRefTest()
    {
        var exp = new Unassign(Variable.X);

        Assert.True(exp.Equals(exp));
    }

    [Fact]
    public void EqualDiffTypesTest()
    {
        var exp1 = new Unassign(Variable.X);
        var exp2 = Number.Two;

        Assert.False(exp1.Equals(exp2));
    }

    [Fact]
    public void EqualTest()
    {
        var exp1 = new Unassign(Variable.X);
        var exp2 = new Unassign(Variable.X);

        Assert.True(exp1.Equals(exp2));
    }

    [Fact]
    public void Equal2Test()
    {
        var exp1 = new Unassign(Variable.X);
        var exp2 = new Unassign(Variable.Y);

        Assert.False(exp1.Equals(exp2));
    }

    [Fact]
    public void UndefVarTest()
    {
        var parameters = new ExpressionParameters { { "a", new NumberValue(1) } };

        var undef = new Unassign(new Variable("a"));
        undef.Execute(parameters);
        Assert.False(parameters.ContainsKey("a"));
    }

    [Fact]
    public void UndefFuncTest()
    {
        var lambda = Variable.X.ToLambda(Variable.X.Name);
        var parameters = new ExpressionParameters
        {
            { Variable.X.Name, lambda }
        };
        var undef = new Unassign(Variable.X);

        var result = undef.Execute(parameters);

        Assert.Equal("(x) => x", result.ToString());
    }

    [Fact]
    public void UndefConstTest()
    {
        var parameters = new ExpressionParameters();

        var undef = new Unassign(new Variable("π"));

        Assert.Throws<ArgumentException>(() => undef.Execute(parameters));
    }

    [Fact]
    public void CloneTest()
    {
        var exp = new Unassign(Variable.X);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }

    [Fact]
    public void NullAnalyzerTest1()
    {
        var exp = new Unassign(Variable.X);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Fact]
    public void NullAnalyzerTest2()
    {
        var exp = new Unassign(Variable.X);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }
}