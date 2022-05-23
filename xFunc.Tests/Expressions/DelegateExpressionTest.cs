// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class DelegateExpressionTest
{
    [Fact]
    public void ExecuteTest1()
    {
        var parameters = new ParameterCollection()
        {
            new Parameter("x", 10)
        };
        var func = new DelegateExpression(p => (NumberValue)p.Variables["x"].Value + 1);

        var result = func.Execute(parameters);

        Assert.Equal(new NumberValue(11.0), result);
    }

    [Fact]
    public void ExecuteTest2()
    {
        var func = new DelegateExpression(p => 10.0);

        var result = func.Execute(null);

        Assert.Equal(10.0, result);
    }

    [Fact]
    public void ExecuteTest3()
    {
        var uf1 = new UserFunction("func", new[] { Variable.X });
        var func = new DelegateExpression(p =>
            (NumberValue)p.Variables["x"].Value == 10
                ? new NumberValue(0.0)
                : new NumberValue(1.0));
        var funcs = new FunctionCollection
        {
            { uf1, func }
        };
        var uf2 = new UserFunction("func", new[] { new Number(12) });
        var result = uf2.Execute(new ExpressionParameters(funcs));

        Assert.Equal(new NumberValue(1.0), result);
    }

    [Fact]
    public void ExecuteTest4()
    {
        var func = new DelegateExpression(p => 1.0);
        var result = func.Execute();

        Assert.Equal(new NumberValue(1.0), result);
    }

    [Fact]
    public void EqualRefTest()
    {
        var exp = new DelegateExpression(p => 1.0);

        Assert.True(exp.Equals(exp));
    }

    [Fact]
    public void EqualRefNullTest()
    {
        var exp = new DelegateExpression(p => 1.0);

        Assert.False(exp.Equals(null));
    }

    [Fact]
    public void EqualDiffTypeTest()
    {
        var exp1 = new DelegateExpression(p => 1.0);
        var exp2 = Number.Two;

        Assert.False(exp1.Equals(exp2));
    }

    [Fact]
    public void EqualSameTest()
    {
        Func<ExpressionParameters, object> d = p => 1.0;
        var exp1 = new DelegateExpression(d);
        var exp2 = new DelegateExpression(d);

        Assert.True(exp1.Equals(exp2));
    }

    [Fact]
    public void EqualDiffTest()
    {
        var exp1 = new DelegateExpression(p => 1.0);
        var exp2 = new DelegateExpression(p => 2.0);

        Assert.False(exp1.Equals(exp2));
    }

    [Fact]
    public void NullAnalyzerTest1()
    {
        var exp = new DelegateExpression(p => 1.0);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Fact]
    public void NullAnalyzerTest2()
    {
        var exp = new DelegateExpression(p => 1.0);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }
}