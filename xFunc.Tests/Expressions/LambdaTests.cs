// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;

namespace xFunc.Tests.Expressions;

public class LambdaTests
{
    [Test]
    public void EqualsTest()
    {
        var f1 = new Lambda(new[] { "x" }, new Sin(Variable.X));
        var f2 = new Lambda(new[] { "x" }, new Sin(Variable.X));

        Assert.That(f1.Equals(f2), Is.True);
    }

    [Test]
    public void NotEqualsTest1()
    {
        var f1 = new Lambda(new[] { "x" }, new Sin(Variable.X));
        var f2 = new Lambda(new[] { "x", "y" }, new Sin(Variable.X));

        Assert.That(f1.Equals(f2), Is.False);
    }

    [Test]
    public void NotEqualsTest2()
    {
        var f1 = new Lambda(new[] { "x" }, new Sin(Variable.X));
        var f2 = new Lambda(new[] { "x" }, new Add(Variable.X, Variable.Y));

        Assert.That(f1.Equals(f2), Is.False);
    }

    [Test]
    public void EqualsObjectTest()
    {
        var f1 = new Lambda(new[] { "x" }, new Sin(Variable.X));
        var f2 = new Lambda(new[] { "x" }, new Sin(Variable.X));

        Assert.That(f1.Equals(f2 as object), Is.True);
    }

    [Test]
    public void NotEqualsObjectTest1()
    {
        var f1 = new Lambda(new[] { "x" }, new Sin(Variable.X));
        var f2 = new Lambda(new[] { "x", "y" }, new Sin(Variable.X));

        Assert.That(f1.Equals(f2 as object), Is.False);
    }

    [Test]
    public void NotEqualsObjectTest2()
    {
        var f1 = new Lambda(new[] { "x" }, new Sin(Variable.X));
        var f2 = new Lambda(new[] { "x" }, new Add(Variable.X, Variable.Y));

        Assert.That(f1.Equals(f2 as object), Is.False);
    }

    [Test]
    public void NotEqualsDiffTypesObjectTest2()
    {
        var f1 = new Lambda(new[] { "x" }, new Sin(Variable.X));
        var f2 = Variable.X;

        Assert.That(f1.Equals(f2 as object), Is.False);
    }

    [Test]
    public void EqualsOperatorTest()
    {
        var f1 = new Lambda(new[] { "x" }, new Sin(Variable.X));
        var f2 = new Lambda(new[] { "x" }, new Sin(Variable.X));

        Assert.That(f1 == f2, Is.True);
    }

    [Test]
    public void NotEqualsOperatorTest1()
    {
        var f1 = new Lambda(new[] { "x" }, new Sin(Variable.X));
        var f2 = new Lambda(new[] { "x", "y" }, new Sin(Variable.X));

        Assert.That(f1 != f2, Is.True);
    }

    [Test]
    public void NotEqualsOperatorTest2()
    {
        var f1 = new Lambda(new[] { "x" }, new Sin(Variable.X));
        var f2 = new Lambda(new[] { "x" }, new Add(Variable.X, Variable.Y));

        Assert.That(f1 != f2, Is.True);
    }

    [Test]
    public void ToStringTest()
    {
        var function = new Lambda(
            new[] { "x", "y" },
            new Add(Variable.X, Variable.Y));

        Assert.That(function.ToString(), Is.EqualTo("(x, y) => x + y"));
    }

    [Test]
    public void CallTest()
    {
        var function = new Lambda(
            new[] { "x", "y" },
            new Add(Variable.X, Variable.Y));
        var parameters = new ExpressionParameters
        {
            { "x", new ParameterValue(1.0) },
            { "y", new ParameterValue(2.0) }
        };

        var result = function.Call(parameters);
        var expected = new NumberValue(3.0);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void CallWithParameterTest()
    {
        var function = new Lambda(
            new[] { "x", "y" },
            new Add(Variable.X, Variable.Y));

        var result = function.Call(
            new IExpression[] { Number.One, Number.Two }.ToImmutableArray(),
            new ExpressionParameters());

        Assert.That(result, Is.EqualTo(new NumberValue(3)));
    }

    [Test]
    public void CallCapturedTest()
    {
        var function = new Lambda(
            new[] { "x", "y" },
            new Add(new Add(Variable.X, Variable.Y), new Variable("a")));
        function = function.Capture(new ExpressionParameters
        {
            new Parameter("a", 3.0)
        });

        var result = function.Call(
            new IExpression[] { Number.One, Number.Two }.ToImmutableArray(),
            new ExpressionParameters());

        Assert.That(result, Is.EqualTo(new NumberValue(6)));
    }

    [Test]
    public void CallReturnLambdaTest()
    {
        var function = new Lambda(
            new[] { "x" },
            new Lambda(new[] { "y" }, new Add(Variable.X, Variable.Y)).AsExpression());

        var result = function.Call(
            new IExpression[] { Number.One, Number.Two }.ToImmutableArray(),
            new ExpressionParameters());
        var expected = new Lambda(new[] { "y" }, new Add(Variable.X, Variable.Y));

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void CurryWithoutParametersTest()
    {
        var lambda = new Lambda(Number.One);
        var result = lambda.Curry();

        Assert.That(result, Is.EqualTo(lambda));
    }

    [Test]
    public void CurryWithSingleParameterTest()
    {
        var lambda = new Lambda(new[] { "x" }, Variable.X);
        var result = lambda.Curry();

        Assert.That(result, Is.EqualTo(lambda));
    }

    [Test]
    public void CurryTest()
    {
        var lambda = new Lambda(
            new[] { "a", "b", "c" },
            new Add(new Add(new Variable("a"), new Variable("b")), new Variable("c")));
        var expected = new Lambda(
            new[] { "a" },
            new Lambda(
                new[] { "b" },
                new Lambda(
                    new[] { "c" },
                    new Add(new Add(new Variable("a"), new Variable("b")), new Variable("c"))
                ).AsExpression()
            ).AsExpression());

        var result = lambda.Curry();

        Assert.That(result, Is.EqualTo(expected));
    }
}