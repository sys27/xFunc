// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Programming;

public class AddAssignTest
{
    [Test]
    public void NullVariableTest()
    {
        Assert.Throws<ArgumentNullException>(() => new AddAssign(null, null));
    }

    [Test]
    public void NullExpTest()
    {
        Assert.Throws<ArgumentNullException>(() => new AddAssign(Variable.X, null));
    }

    [Test]
    public void AddAssignCalc()
    {
        var parameters = new ExpressionParameters { new Parameter("x", 10) };
        var add = new AddAssign(Variable.X, Number.Two);
        var result = add.Execute(parameters);
        var expected = new NumberValue(12.0);

        Assert.That(result, Is.EqualTo(expected));
        Assert.That(parameters["x"].Value, Is.EqualTo(expected));
    }

    [Test]
    public void AddAssignAsExpressionTest()
    {
        var parameters = new ExpressionParameters { new Parameter("x", 10) };
        var add = new Add(Number.One, new AddAssign(Variable.X, Number.Two));
        var result = add.Execute(parameters);

        Assert.That(result, Is.EqualTo(new NumberValue(13.0)));
        Assert.That(parameters["x"].Value, Is.EqualTo(new NumberValue(12.0)));
    }

    [Test]
    public void AddNullParameters()
    {
        var exp = new AddAssign(Variable.X, Number.One);

        Assert.Throws<ArgumentNullException>(() => exp.Execute());
    }

    [Test]
    public void SubValueBoolParameters()
    {
        var exp = new AddAssign(Variable.X, Bool.False);
        var parameters = new ExpressionParameters { new Parameter("x", 1) };

        Assert.Throws<ExecutionException>(() => exp.Execute(parameters));
    }

    [Test]
    public void BoolAddNumberTest()
    {
        var parameters = new ExpressionParameters { new Parameter("x", true) };
        var add = new AddAssign(Variable.X, Number.Two);

        Assert.Throws<ExecutionException>(() => add.Execute(parameters));
    }

    [Test]
    public void SameEqualsTest()
    {
        var exp = new AddAssign(Variable.X, Number.One);

        Assert.True(exp.Equals(exp));
    }

    [Test]
    public void EqualsNullTest()
    {
        var exp = new AddAssign(Variable.X, Number.One);

        Assert.False(exp.Equals(null));
    }

    [Test]
    public void EqualsDifferentTypeTest()
    {
        var exp1 = new AddAssign(Variable.X, Number.One);
        var exp2 = new SubAssign(Variable.X, Number.One);

        Assert.False(exp1.Equals(exp2));
    }

    [Test]
    public void NullAnalyzerTest1()
    {
        var exp = new AddAssign(Variable.X, Number.One);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Test]
    public void NullAnalyzerTest2()
    {
        var exp = new AddAssign(Variable.X, Number.One);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }

    [Test]
    public void CloneTest()
    {
        var exp = new AddAssign(Variable.X, Number.Two);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}