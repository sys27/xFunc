// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Programming;

public class IncTest
{
    [Test]
    public void NullCtorTest()
        => Assert.Throws<ArgumentNullException>(() => new Inc(null));

    [Test]
    public void IncCalcTest()
    {
        var parameters = new ExpressionParameters { new Parameter("x", 10) };
        var inc = new Inc(Variable.X);
        var result = (NumberValue)inc.Execute(parameters);
        var expected = new NumberValue(11.0);

        Assert.That(result, Is.EqualTo(expected));
        Assert.That(parameters["x"].Value, Is.EqualTo(expected));
    }

    [Test]
    public void IncAsExpExecuteTest()
    {
        var parameters = new ExpressionParameters { new Parameter("x", 10) };
        var inc = new Add(Number.One, new Inc(Variable.X));
        var result = (NumberValue)inc.Execute(parameters);

        Assert.That(result, Is.EqualTo(new NumberValue(12.0)));
        Assert.That(parameters["x"].Value, Is.EqualTo(new NumberValue(11.0)));
    }

    [Test]
    public void IncNullParameters()
    {
        Assert.Throws<ArgumentNullException>(() => new Inc(Variable.X).Execute());
    }

    [Test]
    public void IncBoolTest()
    {
        var parameters = new ExpressionParameters { new Parameter("x", true) };
        var inc = new Inc(Variable.X);

        Assert.Throws<ExecutionException>(() => inc.Execute(parameters));
    }

    [Test]
    public void SameEqualsTest()
    {
        var inc = new Inc(Variable.X);

        Assert.That(inc.Equals(inc), Is.True);
    }

    [Test]
    public void EqualsNullTest()
    {
        var inc = new Inc(Variable.X);

        Assert.That(inc.Equals(null), Is.False);
    }

    [Test]
    public void EqualsDifferentTypeTest()
    {
        var inc = new Inc(Variable.X);
        var dec = new Dec(Variable.X);

        Assert.That(inc.Equals(dec), Is.False);
    }

    [Test]
    public void NullAnalyzerTest1()
    {
        var inc = new Inc(Variable.X);

        Assert.Throws<ArgumentNullException>(() => inc.Analyze<string>(null));
    }

    [Test]
    public void NullAnalyzerTest2()
    {
        var inc = new Inc(Variable.X);

        Assert.Throws<ArgumentNullException>(() => inc.Analyze<string, object>(null, null));
    }

    [Test]
    public void CloneTest()
    {
        var exp = new Inc(Variable.X);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}