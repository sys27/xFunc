// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Programming;

public class DecTest
{
    [Test]
    public void NullCtorTest()
        => Assert.Throws<ArgumentNullException>(() => new Dec(null));

    [Test]
    public void DecCalcTest()
    {
        var parameters = new ExpressionParameters { new Parameter("x", 10) };
        var dec = new Dec(Variable.X);
        var result = (NumberValue)dec.Execute(parameters);
        var expected = new NumberValue(9.0);

        Assert.That(result, Is.EqualTo(expected));
        Assert.That(parameters["x"].Value, Is.EqualTo(expected));
    }

    [Test]
    public void DecAsExpExecuteTest()
    {
        var parameters = new ExpressionParameters { new Parameter("x", 10) };
        var inc = new Add(Number.One, new Dec(Variable.X));
        var result = (NumberValue)inc.Execute(parameters);

        Assert.That(result, Is.EqualTo(new NumberValue(10.0)));
        Assert.That(parameters["x"].Value, Is.EqualTo(new NumberValue(9.0)));
    }

    [Test]
    public void DecNullParameters()
    {
        Assert.Throws<ArgumentNullException>(() => new Dec(Variable.X).Execute());
    }

    [Test]
    public void DecBoolTest()
    {
        var parameters = new ExpressionParameters { new Parameter("x", true) };
        var dec = new Dec(Variable.X);

        Assert.Throws<ResultIsNotSupportedException>(() => dec.Execute(parameters));
    }

    [Test]
    public void SameEqualsTest()
    {
        var dec = new Dec(Variable.X);

        Assert.That(dec.Equals(dec), Is.True);
    }

    [Test]
    public void EqualsNullTest()
    {
        var dec = new Dec(Variable.X);

        Assert.That(dec.Equals(null), Is.False);
    }

    [Test]
    public void EqualsDifferentTypeTest()
    {
        var dec = new Dec(Variable.X);
        var inc = new Inc(Variable.X);

        Assert.That(dec.Equals(inc), Is.False);
    }

    [Test]
    public void NullAnalyzerTest1()
    {
        var inc = new Dec(Variable.X);

        Assert.Throws<ArgumentNullException>(() => inc.Analyze<string>(null));
    }

    [Test]
    public void NullAnalyzerTest2()
    {
        var inc = new Dec(Variable.X);

        Assert.Throws<ArgumentNullException>(() => inc.Analyze<string, object>(null, null));
    }

    [Test]
    public void CloneTest()
    {
        var exp = new Dec(Variable.X);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}