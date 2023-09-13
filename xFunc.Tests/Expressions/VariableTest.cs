// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class VariableTest
{
    [Test]
    public void NullTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Variable(null));
    }

    [Test]
    public void ExecuteNotSupportedTest()
    {
        var exp = Variable.X;

        Assert.Throws<NotSupportedException>(() => exp.Execute());
    }

    [Test]
    public void ExecuteNullTest()
    {
        var exp = Variable.X;

        Assert.Throws<ArgumentNullException>(() => exp.Execute(null));
    }

    [Test]
    public void ExecuteTest()
    {
        var exp = Variable.X;
        var parameters = new ExpressionParameters();
        parameters.Add("x", new NumberValue(1.0));

        var result = exp.Execute(parameters);

        Assert.That(result, Is.EqualTo(new NumberValue(1.0)));
    }

    [Test]
    public void ConvertToString()
    {
        var exp = (string)Variable.X;

        Assert.That(exp, Is.EqualTo("x"));
    }

    [Test]
    public void StringToConvert()
    {
        var exp = (Variable)"x";
        var result = Variable.X;

        Assert.That(exp, Is.EqualTo(result));
    }

    [Test]
    public void EqualsVariableNullTest()
    {
        var variable = Variable.X;

        Assert.That(variable.Equals(null), Is.False);
    }

    [Test]
    public void EqualsObjectNullTest()
    {
        var variable = Variable.X;

        Assert.That(variable.Equals((object)null), Is.False);
    }

    [Test]
    public void EqualsVariableThisTest()
    {
        var variable = Variable.X;

        Assert.That(variable.Equals(variable), Is.True);
    }

    [Test]
    public void EqualsObjectThisTest()
    {
        var variable = Variable.X;

        Assert.That(variable.Equals((object)variable), Is.True);
    }

    [Test]
    public void EqualsTest()
    {
        var left = Variable.X;
        var right = Variable.X;

        Assert.That(left.Equals(right), Is.True);
    }

    [Test]
    public void NotEqualsTest()
    {
        var left = Variable.X;
        var right = Variable.Y;

        Assert.That(left.Equals(right), Is.False);
    }

    [Test]
    public void EqualsDiffTypesTest()
    {
        var left = Variable.X;
        var right = Number.Two;

        Assert.That(left.Equals(right), Is.False);
    }

    [Test]
    public void ImplicitNullToString()
    {
        Variable x = null;

        Assert.Throws<ArgumentNullException>(() =>
        {
            var temp = (string)x;
        });
    }

    [Test]
    public void NullAnalyzerTest1()
    {
        var exp = Variable.X;

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Test]
    public void NullAnalyzerTest2()
    {
        var exp = Variable.X;

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }
}