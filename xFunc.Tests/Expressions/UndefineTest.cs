// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class UndefineTest
{
    [Fact]
    public void InvalidTypeTest()
    {
        Assert.Throws<NotSupportedException>(() => new Undefine(Number.One));
    }

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
    public void KeyNotSupportedTest()
    {
        Assert.Throws<NotSupportedException>(() => new Undefine(Number.One));
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
        var parameters = new ParameterCollection { { "a", new NumberValue(1) } };

        var undef = new Undefine(new Variable("a"));
        undef.Execute(parameters);
        Assert.False(parameters.ContainsKey("a"));
    }

    [Fact]
    public void UndefFuncTest()
    {
        var key1 = new UserFunction("f", new IExpression[0]);
        var key2 = new UserFunction("f", new IExpression[] { Variable.X });

        var functions = new FunctionCollection
        {
            { key1, Number.One },
            { key2, Number.Two },
        };

        var undef = new Undefine(key1);
        var result = undef.Execute(functions);

        Assert.False(functions.ContainsKey(key1));
        Assert.True(functions.ContainsKey(key2));
        Assert.Equal("The 'f()' function is removed.", result);
    }

    [Fact]
    public void UndefFuncWithParamsTest()
    {
        var key1 = new UserFunction("f", new IExpression[0]);
        var key2 = new UserFunction("f", new IExpression[] { Variable.X });

        var functions = new FunctionCollection
        {
            { key1, Number.One },
            { key2, Number.Two },
        };

        var undef = new Undefine(key2);
        var result = undef.Execute(functions);

        Assert.True(functions.ContainsKey(key1));
        Assert.False(functions.ContainsKey(key2));
        Assert.Equal("The 'f(x)' function is removed.", result);
    }

    [Fact]
    public void UndefConstTest()
    {
        var parameters = new ParameterCollection();

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