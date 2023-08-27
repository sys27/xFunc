// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Programming;

public class SubAssignTest
{
    [Test]
    public void SubAssignCalc()
    {
        var parameters = new ExpressionParameters { new Parameter("x", 10) };
        var sub = new SubAssign(Variable.X, Number.Two);
        var result = sub.Execute(parameters);
        var expected = new NumberValue(8.0);

        Assert.That(result, Is.EqualTo(expected));
        Assert.That(parameters["x"].Value, Is.EqualTo(expected));
    }

    [Test]
    public void SubAssignAsExpressionTest()
    {
        var parameters = new ExpressionParameters { new Parameter("x", 10) };
        var add = new Add(Number.One, new SubAssign(Variable.X, Number.Two));
        var result = add.Execute(parameters);

        Assert.That(result, Is.EqualTo(new NumberValue(9.0)));
        Assert.That(parameters["x"].Value, Is.EqualTo(new NumberValue(8.0)));
    }

    [Test]
    public void SubNullParameters()
    {
        var exp = new SubAssign(Variable.X, Number.One);

        Assert.Throws<ArgumentNullException>(() => exp.Execute());
    }

    [Test]
    public void SubValueBoolParameters()
    {
        var exp = new SubAssign(Variable.X, Bool.False);
        var parameters = new ExpressionParameters { new Parameter("x", 1) };

        Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute(parameters));
    }

    [Test]
    public void BoolSubNumberTest()
    {
        var parameters = new ExpressionParameters { new Parameter("x", true) };
        var add = new SubAssign(Variable.X, Number.Two);

        Assert.Throws<ResultIsNotSupportedException>(() => add.Execute(parameters));
    }

    [Test]
    public void SameEqualsTest()
    {
        var exp = new SubAssign(Variable.X, Number.One);

        Assert.That(exp.Equals(exp), Is.True);
    }

    [Test]
    public void EqualsNullTest()
    {
        var exp = new SubAssign(Variable.X, Number.One);

        Assert.That(exp.Equals(null), Is.False);
    }

    [Test]
    public void EqualsDifferentTypeTest()
    {
        var exp1 = new SubAssign(Variable.X, Number.One);
        var exp2 = new DivAssign(Variable.X, Number.One);

        Assert.That(exp1.Equals(exp2), Is.False);
    }

    [Test]
    public void NullAnalyzerTest1()
    {
        var exp = new SubAssign(Variable.X, Number.One);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Test]
    public void NullAnalyzerTest2()
    {
        var exp = new SubAssign(Variable.X, Number.One);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }

    [Test]
    public void CloneTest()
    {
        var exp = new SubAssign(Variable.X, Number.Two);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}