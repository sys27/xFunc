// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class VariableTest
{
    [Fact]
    public void NullTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Variable(null));
    }

    [Fact]
    public void ExecuteNotSupportedTest()
    {
        var exp = Variable.X;

        Assert.Throws<NotSupportedException>(() => exp.Execute());
    }

    [Fact]
    public void ExecuteNullTest()
    {
        var exp = Variable.X;

        Assert.Throws<ArgumentNullException>(() => exp.Execute(null));
    }

    [Fact]
    public void ExecuteTest()
    {
        var exp = Variable.X;
        var parameters = new ExpressionParameters();
        parameters.Add("x", new NumberValue(1.0));

        var result = exp.Execute(parameters);

        Assert.Equal(new NumberValue(1.0), result);
    }

    [Fact]
    public void ConvertToString()
    {
        var exp = Variable.X;

        Assert.Equal("x", exp);
    }

    [Fact]
    public void StringToConvert()
    {
        var exp = "x";
        var result = Variable.X;

        Assert.Equal<Variable>(result, exp);
    }

    [Fact]
    public void EqualsVariableNullTest()
    {
        var variable = Variable.X;

        Assert.False(variable.Equals(null));
    }

    [Fact]
    public void EqualsObjectNullTest()
    {
        var variable = Variable.X;

        Assert.False(variable.Equals((object)null));
    }

    [Fact]
    public void EqualsVariableThisTest()
    {
        var variable = Variable.X;

        Assert.True(variable.Equals(variable));
    }

    [Fact]
    public void EqualsObjectThisTest()
    {
        var variable = Variable.X;

        Assert.True(variable.Equals((object)variable));
    }

    [Fact]
    public void EqualsTest()
    {
        var left = Variable.X;
        var right = Variable.X;

        Assert.True(left.Equals(right));
    }

    [Fact]
    public void NotEqualsTest()
    {
        var left = Variable.X;
        var right = Variable.Y;

        Assert.False(left.Equals(right));
    }

    [Fact]
    public void EqualsDiffTypesTest()
    {
        var left = Variable.X;
        var right = Number.Two;

        Assert.False(left.Equals(right));
    }

    [Fact]
    public void ImplicitNullToString()
    {
        Variable x = null;

        Assert.Throws<ArgumentNullException>(() => (string)x);
    }

    [Fact]
    public void NullAnalyzerTest1()
    {
        var exp = Variable.X;

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Fact]
    public void NullAnalyzerTest2()
    {
        var exp = Variable.X;

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }
}