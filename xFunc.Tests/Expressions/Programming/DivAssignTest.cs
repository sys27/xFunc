// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Programming;

public class DivAssignTest
{
    [Test]
    public void DivAssignCalc()
    {
        var parameters = new ExpressionParameters { new Parameter("x", 10) };
        var div = new DivAssign(Variable.X, Number.Two);
        var result = div.Execute(parameters);
        var expected = new NumberValue(5.0);

        Assert.That(result, Is.EqualTo(expected));
        Assert.That(parameters["x"].Value, Is.EqualTo(expected));
    }

    [Test]
    public void DivAssignAsExpressionTest()
    {
        var parameters = new ExpressionParameters { new Parameter("x", 10) };
        var add = new Add(new DivAssign(Variable.X, Number.Two), Number.Two);
        var result = add.Execute(parameters);

        Assert.That(result, Is.EqualTo(new NumberValue(7.0)));
        Assert.That(parameters["x"].Value, Is.EqualTo(new NumberValue(5.0)));
    }

    [Test]
    public void DivNullParameters()
    {
        var exp = new DivAssign(Variable.X, Number.One);

        Assert.Throws<ArgumentNullException>(() => exp.Execute());
    }

    [Test]
    public void DivValueBoolParameters()
    {
        var exp = new DivAssign(Variable.X, Bool.False);
        var parameters = new ExpressionParameters { new Parameter("x", 1) };

        Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute(parameters));
    }

    [Test]
    public void BoolDivNumberTest()
    {
        var parameters = new ExpressionParameters { new Parameter("x", true) };
        var add = new DivAssign(Variable.X, Number.Two);

        Assert.Throws<ResultIsNotSupportedException>(() => add.Execute(parameters));
    }

    [Test]
    public void SameEqualsTest()
    {
        var exp = new DivAssign(Variable.X, Number.One);

        Assert.That(exp.Equals(exp), Is.True);
    }

    [Test]
    public void EqualsNullTest()
    {
        var exp = new DivAssign(Variable.X, Number.One);

        Assert.That(exp.Equals(null), Is.False);
    }

    [Test]
    public void EqualsDifferentTypeTest()
    {
        var exp1 = new DivAssign(Variable.X, Number.One);
        var exp2 = new MulAssign(Variable.X, Number.One);

        Assert.That(exp1.Equals(exp2), Is.False);
    }

    [Test]
    public void NullAnalyzerTest1()
    {
        var exp = new DivAssign(Variable.X, Number.One);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Test]
    public void NullAnalyzerTest2()
    {
        var exp = new DivAssign(Variable.X, Number.One);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }

    [Test]
    public void CloneTest()
    {
        var exp = new DivAssign(Variable.X, Number.Two);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}