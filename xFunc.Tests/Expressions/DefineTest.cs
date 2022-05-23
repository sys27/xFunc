// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class DefineTest
{
    [Fact]
    public void InvalidTypeTest()
    {
        Assert.Throws<NotSupportedException>(() => new Define(Number.One, Number.One));
    }

    [Fact]
    public void SimpDefineTest()
    {
        var exp = new Define(Variable.X, Number.One);
        var parameters = new ExpressionParameters();

        var answer = exp.Execute(parameters);

        Assert.Equal(new NumberValue(1.0), parameters.Variables["x"]);
        Assert.Equal("The value '1' was assigned to the variable 'x'.", answer);
    }

    [Fact]
    public void DefineWithFuncTest()
    {
        var exp = new Define(Variable.X, new Sin(AngleValue.Radian(1).AsExpression()));
        var parameters = new ParameterCollection();
        var expParams = new ExpressionParameters(parameters);

        var answer = exp.Execute(expParams);

        Assert.Equal(new NumberValue(Math.Sin(1)), parameters["x"]);
        Assert.Equal("The value 'sin(1 radian)' was assigned to the variable 'x'.", answer);
    }

    [Fact]
    public void DefineExpTest()
    {
        var exp = new Define(Variable.X, new Mul(new Number(4), new Add(new Number(8), Number.One)));
        var parameters = new ExpressionParameters();

        var answer = exp.Execute(parameters);

        Assert.Equal(new NumberValue(36.0), parameters.Variables["x"]);
        Assert.Equal("The value '4 * (8 + 1)' was assigned to the variable 'x'.", answer);
    }

    [Fact]
    public void OverrideConstTest()
    {
        var exp = new Define(new Variable("π"), Number.One);
        var parameters = new ExpressionParameters();

        exp.Execute(parameters);

        Assert.Equal(new NumberValue(1.0), parameters.Variables["π"]);
    }

    [Fact]
    public void DefineFuncTest()
    {
        var uf = new UserFunction("s", new IExpression[0]);
        var func = new Sin(Number.One);
        var exp = new Define(uf, func);
        var parameters = new ExpressionParameters();

        var result = exp.Execute(parameters);

        Assert.Equal(func, parameters.Functions[uf]);
        Assert.Equal("The expression 'sin(1)' was assigned to the function 's()'.", result);
    }

    [Fact]
    public void DefineFuncWithParamsTest()
    {
        var uf = new UserFunction("s", new IExpression[] { Variable.X });
        var func = new Sin(Variable.X);
        var exp = new Define(uf, func);
        var parameters = new ExpressionParameters();

        var result = exp.Execute(parameters);

        Assert.Equal(func, parameters.Functions[uf]);
        Assert.Equal("The expression 'sin(x)' was assigned to the function 's(x)'.", result);
    }

    [Fact]
    public void ParamsNullTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Define(new Variable("π"), Number.One).Execute(null));
    }

    [Fact]
    public void ExecuteWithoutParametesTest()
    {
        Assert.Throws<NotSupportedException>(() => new Define(new Variable("π"), Number.One).Execute());
    }

    [Fact]
    public void KeyIsNullTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Define(null, Number.One));
    }

    [Fact]
    public void ValueIsNullTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Define(Variable.X, null));
    }

    [Fact]
    public void EqualsSameReferenceTest()
    {
        var def = new Define(Variable.X, Number.One);

        Assert.True(def.Equals(def));
    }

    [Fact]
    public void EqualsDifferentTypesTest()
    {
        var def = new Define(Variable.X, Number.One);
        var number = Number.One;

        Assert.False(def.Equals(number));
    }

    [Fact]
    public void EqualsDifferentOnjectsTest()
    {
        var def1 = new Define(Variable.X, Number.One);
        var def2 = new Define(Variable.Y, Number.Two);

        Assert.False(def1.Equals(def2));
    }

    [Fact]
    public void CloneTest()
    {
        var exp = new Define(Variable.X, Number.Zero);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }

    [Fact]
    public void NullAnalyzerTest1()
    {
        var exp = new Define(Variable.X, Number.Zero);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Fact]
    public void NullAnalyzerTest2()
    {
        var exp = new Define(Variable.X, Number.Zero);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }
}