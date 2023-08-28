// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Programming;

public class RightShiftAssignTest
{
    [Test]
    public void ExecuteTest()
    {
        var exp = new RightShiftAssign(Variable.X, new Number(9));
        var parameters = new ExpressionParameters
        {
            new Parameter("x", 512.0)
        };
        var actual = exp.Execute(parameters);
        var expected = new NumberValue(1.0);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteAsExpressionTest()
    {
        var exp = new Add(Number.One, new RightShiftAssign(Variable.X, new Number(9)));
        var parameters = new ExpressionParameters
        {
            new Parameter("x", 512.0)
        };
        var actual = exp.Execute(parameters);

        Assert.That(actual, Is.EqualTo(new NumberValue(2.0)));
        Assert.That(parameters["x"].Value, Is.EqualTo(new NumberValue(1.0)));
    }

    [Test]
    public void ExecuteNullParamsTest()
    {
        var exp = new RightShiftAssign(Variable.X, new Number(9));

        Assert.Throws<ArgumentNullException>(() => exp.Execute());
    }

    [Test]
    public void ExecuteDoubleLeftTest()
    {
        var exp = new RightShiftAssign(Variable.X, new Number(10));
        var parameters = new ExpressionParameters
        {
            new Parameter("x", 512.5)
        };

        Assert.Throws<ArgumentException>(() => exp.Execute(parameters));
    }

    [Test]
    public void ExecuteDoubleRightTest()
    {
        var exp = new RightShiftAssign(Variable.X, new Number(10.1));
        var parameters = new ExpressionParameters
        {
            new Parameter("x", 512.0)
        };

        Assert.Throws<ArgumentException>(() => exp.Execute(parameters));
    }

    [Test]
    public void ExecuteBoolRightTest()
    {
        var exp = new RightShiftAssign(Variable.X, Bool.True);
        var parameters = new ExpressionParameters
        {
            new Parameter("x", 512.0)
        };

        Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute(parameters));
    }

    [Test]
    public void ExecuteBoolLeftTest()
    {
        var exp = new RightShiftAssign(Variable.X, Bool.True);
        var parameters = new ExpressionParameters
        {
            new Parameter("x", false)
        };

        Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute(parameters));
    }

    [Test]
    public void CloneTest()
    {
        var exp = new RightShiftAssign(Variable.X, new Number(10));
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}