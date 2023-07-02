// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class DefineTest
{
    [Fact]
    public void SimpleDefineTest()
    {
        var exp = new Assign(Variable.X, Number.One);
        var parameters = new ExpressionParameters();

        var answer = exp.Execute(parameters);

        Assert.Equal(new NumberValue(1.0), parameters["x"]);
        Assert.Equal("The value '1' was assigned to 'x'.", answer);
    }

    [Fact]
    public void DefineWithFuncTest()
    {
        var exp = new Assign(Variable.X, new Sin(AngleValue.Radian(1).AsExpression()));
        var parameters = new ExpressionParameters();

        var answer = exp.Execute(parameters);

        Assert.Equal(new NumberValue(Math.Sin(1)), parameters[Variable.X.Name]);
        Assert.Equal("The value 'sin(1 radian)' was assigned to 'x'.", answer);
    }

    [Fact]
    public void DefineExpTest()
    {
        var exp = new Assign(Variable.X, new Mul(new Number(4), new Add(new Number(8), Number.One)));
        var parameters = new ExpressionParameters();

        var answer = exp.Execute(parameters);

        Assert.Equal(new NumberValue(36.0), parameters["x"]);
        Assert.Equal("The value '4 * (8 + 1)' was assigned to 'x'.", answer);
    }

    [Fact]
    public void OverrideConstTest()
    {
        var exp = new Assign(new Variable("π"), Number.One);
        var parameters = new ExpressionParameters();

        exp.Execute(parameters);

        Assert.Equal(new NumberValue(1.0), parameters["π"]);
    }

    [Fact]
    public void DefineFuncTest()
    {
        var function = new Lambda(Array.Empty<string>(), Number.One)
            .AsExpression();
        var variable = new Variable("f");
        var exp = new Assign(variable, function);

        var parameters = new ExpressionParameters();
        var result = exp.Execute(parameters);

        Assert.Equal("The value '() => 1' was assigned to 'f'.", result);
    }

    [Fact]
    public void DefineFuncWithParamsTest()
    {
        var function = new Lambda(
                new[] { "x", "y" },
                new Add(Variable.X, Variable.Y))
            .AsExpression();
        var variable = new Variable("f");
        var exp = new Assign(variable, function);

        var parameters = new ExpressionParameters();
        var result = exp.Execute(parameters);

        Assert.Equal("The value '(x, y) => x + y' was assigned to 'f'.", result);
    }

    [Fact]
    public void ParamsNullTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Assign(new Variable("π"), Number.One).Execute(null));
    }

    [Fact]
    public void ExecuteWithoutParametersTest()
    {
        Assert.Throws<NotSupportedException>(() => new Assign(new Variable("π"), Number.One).Execute());
    }

    [Fact]
    public void KeyIsNullTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Assign(null, Number.One));
    }

    [Fact]
    public void ValueIsNullTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Assign(Variable.X, null));
    }

    [Fact]
    public void EqualsSameReferenceTest()
    {
        var def = new Assign(Variable.X, Number.One);

        Assert.True(def.Equals(def));
    }

    [Fact]
    public void EqualsDifferentTypesTest()
    {
        var def = new Assign(Variable.X, Number.One);
        var number = Number.One;

        Assert.False(def.Equals(number));
    }

    [Fact]
    public void EqualsDifferentOnjectsTest()
    {
        var def1 = new Assign(Variable.X, Number.One);
        var def2 = new Assign(Variable.Y, Number.Two);

        Assert.False(def1.Equals(def2));
    }

    [Fact]
    public void CloneTest()
    {
        var exp = new Assign(Variable.X, Number.Zero);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }

    [Fact]
    public void NullAnalyzerTest1()
    {
        var exp = new Assign(Variable.X, Number.Zero);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Fact]
    public void NullAnalyzerTest2()
    {
        var exp = new Assign(Variable.X, Number.Zero);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }
}