// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class DelegateExpressionTest
{
    [Test]
    public void ExecuteTest1()
    {
        var parameters = new ExpressionParameters()
        {
            new Parameter("x", 10)
        };
        var func = new DelegateExpression(p => (NumberValue)p["x"].Value + 1);

        var result = func.Execute(parameters);

        Assert.That(result, Is.EqualTo(new NumberValue(11.0)));
    }

    [Test]
    public void ExecuteTest2()
    {
        var func = new DelegateExpression(_ => 10.0);

        var result = func.Execute(null);

        Assert.That(result, Is.EqualTo(10.0));
    }

    [Test]
    public void ExecuteTest4()
    {
        var func = new DelegateExpression(_ => 1.0);
        var result = func.Execute();

        Assert.That(result, Is.EqualTo(new NumberValue(1.0)));
    }

    [Test]
    public void EqualRefTest()
    {
        var exp = new DelegateExpression(_ => 1.0);

        Assert.That(exp.Equals(exp), Is.True);
    }

    [Test]
    public void EqualRefNullTest()
    {
        var exp = new DelegateExpression(_ => 1.0);

        Assert.That(exp.Equals(null), Is.False);
    }

    [Test]
    public void EqualDiffTypeTest()
    {
        var exp1 = new DelegateExpression(_ => 1.0);
        var exp2 = Number.Two;

        Assert.That(exp1.Equals(exp2), Is.False);
    }

    [Test]
    public void EqualSameTest()
    {
        Func<ExpressionParameters, object> d = _ => 1.0;
        var exp1 = new DelegateExpression(d);
        var exp2 = new DelegateExpression(d);

        Assert.That(exp1.Equals(exp2), Is.True);
    }

    [Test]
    public void EqualDiffTest()
    {
        var exp1 = new DelegateExpression(_ => 1.0);
        var exp2 = new DelegateExpression(_ => 2.0);

        Assert.That(exp1.Equals(exp2), Is.False);
    }

    [Test]
    public void NullAnalyzerTest1()
    {
        var exp = new DelegateExpression(_ => 1.0);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Test]
    public void NullAnalyzerTest2()
    {
        var exp = new DelegateExpression(_ => 1.0);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }
}