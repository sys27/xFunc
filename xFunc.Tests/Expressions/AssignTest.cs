// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class AssignTest
{
    [Test]
    public void SimpleDefineTest()
    {
        var exp = new Assign(Variable.X, Number.One);
        var parameters = new ExpressionParameters();

        var answer = exp.Execute(parameters);

        var expected = new NumberValue(1.0);
        Assert.That(parameters["x"].Value, Is.EqualTo(expected));
        Assert.That(answer, Is.EqualTo(expected));
    }

    [Test]
    public void DefineWithFuncTest()
    {
        var exp = new Assign(Variable.X, new Sin(AngleValue.Radian(1).AsExpression()));
        var parameters = new ExpressionParameters();

        var answer = exp.Execute(parameters);

        var expected = new NumberValue(Math.Sin(1));
        Assert.That(parameters[Variable.X.Name].Value, Is.EqualTo(expected));
        Assert.That(answer, Is.EqualTo(expected));
    }

    [Test]
    public void DefineExpTest()
    {
        var exp = new Assign(Variable.X, new Mul(new Number(4), new Add(new Number(8), Number.One)));
        var parameters = new ExpressionParameters();

        var answer = exp.Execute(parameters);

        var expected = new NumberValue(36.0);
        Assert.That(parameters["x"].Value, Is.EqualTo(expected));
        Assert.That(answer, Is.EqualTo(expected));
    }

    [Test]
    public void OverrideConstTest()
    {
        var exp = new Assign(new Variable("π"), Number.One);
        var parameters = new ExpressionParameters();

        exp.Execute(parameters);

        Assert.That(parameters["π"].Value, Is.EqualTo(new NumberValue(1.0)));
    }

    [Test]
    public void DefineFuncTest()
    {
        var function = Number.One.ToLambdaExpression();
        var variable = new Variable("f");
        var exp = new Assign(variable, function);

        var parameters = new ExpressionParameters();
        var result = exp.Execute(parameters);

        Assert.That(result.ToString(), Is.EqualTo("() => 1"));
    }

    [Test]
    public void DefineFuncWithParamsTest()
    {
        var function = new Add(Variable.X, Variable.Y)
            .ToLambdaExpression(Variable.X.Name, Variable.Y.Name);
        var variable = new Variable("f");
        var exp = new Assign(variable, function);

        var parameters = new ExpressionParameters();
        var result = exp.Execute(parameters);

        Assert.That(result.ToString(), Is.EqualTo("(x, y) => x + y"));
    }

    [Test]
    public void ParamsNullTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Assign(new Variable("π"), Number.One).Execute(null));
    }

    [Test]
    public void ExecuteWithoutParametersTest()
    {
        Assert.Throws<NotSupportedException>(() => new Assign(new Variable("π"), Number.One).Execute());
    }

    [Test]
    public void KeyIsNullTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Assign(null, Number.One));
    }

    [Test]
    public void ValueIsNullTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Assign(Variable.X, null));
    }

    [Test]
    public void EqualsSameReferenceTest()
    {
        var def = new Assign(Variable.X, Number.One);

        Assert.That(def.Equals(def), Is.True);
    }

    [Test]
    public void EqualsDifferentTypesTest()
    {
        var def = new Assign(Variable.X, Number.One);
        var number = Number.One;

        Assert.That(def.Equals(number), Is.False);
    }

    [Test]
    public void EqualsDifferentOnjectsTest()
    {
        var def1 = new Assign(Variable.X, Number.One);
        var def2 = new Assign(Variable.Y, Number.Two);

        Assert.That(def1.Equals(def2), Is.False);
    }

    [Test]
    public void CloneTest()
    {
        var exp = new Assign(Variable.X, Number.Zero);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }

    [Test]
    public void NullAnalyzerTest1()
    {
        var exp = new Assign(Variable.X, Number.Zero);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Test]
    public void NullAnalyzerTest2()
    {
        var exp = new Assign(Variable.X, Number.Zero);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }
}