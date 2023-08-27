// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class UnassignTest
{
    [Test]
    public void ExecuteFailTest()
    {
        Assert.Throws<NotSupportedException>(() => new Unassign(Variable.X).Execute());
    }

    [Test]
    public void ExecuteParamNullTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Unassign(Variable.X).Execute(null));
    }

    [Test]
    public void KeyNullTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Unassign(null));
    }

    [Test]
    public void EqualRefTest()
    {
        var exp = new Unassign(Variable.X);

        Assert.That(exp.Equals(exp), Is.True);
    }

    [Test]
    public void EqualDiffTypesTest()
    {
        var exp1 = new Unassign(Variable.X);
        var exp2 = Number.Two;

        Assert.That(exp1.Equals(exp2), Is.False);
    }

    [Test]
    public void EqualTest()
    {
        var exp1 = new Unassign(Variable.X);
        var exp2 = new Unassign(Variable.X);

        Assert.That(exp1.Equals(exp2), Is.True);
    }

    [Test]
    public void Equal2Test()
    {
        var exp1 = new Unassign(Variable.X);
        var exp2 = new Unassign(Variable.Y);

        Assert.That(exp1.Equals(exp2), Is.False);
    }

    [Test]
    public void UndefVarTest()
    {
        var parameters = new ExpressionParameters { { "a", new NumberValue(1) } };

        var undef = new Unassign(new Variable("a"));
        undef.Execute(parameters);
        Assert.That(parameters.ContainsKey("a"), Is.False);
    }

    [Test]
    public void UndefFuncTest()
    {
        var lambda = Variable.X.ToLambda(Variable.X.Name);
        var parameters = new ExpressionParameters
        {
            { Variable.X.Name, lambda }
        };
        var undef = new Unassign(Variable.X);

        var result = undef.Execute(parameters);

        Assert.That(result.ToString(), Is.EqualTo("(x) => x"));
    }

    [Test]
    public void UndefConstTest()
    {
        var parameters = new ExpressionParameters();

        var undef = new Unassign(new Variable("π"));

        Assert.Throws<ArgumentException>(() => undef.Execute(parameters));
    }

    [Test]
    public void CloneTest()
    {
        var exp = new Unassign(Variable.X);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }

    [Test]
    public void NullAnalyzerTest1()
    {
        var exp = new Unassign(Variable.X);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Test]
    public void NullAnalyzerTest2()
    {
        var exp = new Unassign(Variable.X);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }
}