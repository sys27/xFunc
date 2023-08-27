// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.LogicalAndBitwise;

public class BoolTest
{
    [Test]
    public void ExecuteTest1()
    {
        var exp = Bool.False;

        Assert.That((bool)exp.Execute(), Is.False);
    }

    [Test]
    public void ExecuteTest2()
    {
        var exp = Bool.False;

        Assert.That((bool)exp.Execute(null), Is.False);
    }

    [Test]
    public void ExecuteTest3()
    {
        var exp = Bool.False;

        Assert.That((bool)exp, Is.False);
    }

    [Test]
    public void ExecuteTest4()
    {
        var exp = (Bool)false;

        Assert.That((bool)exp.Execute(), Is.False);
    }

    [Test]
    public void NotEqualsTest()
    {
        var exp1 = Bool.False;
        var exp2 = Bool.True;

        Assert.That(exp1.Equals(exp2), Is.False);
    }

    [Test]
    public void EqualsTest()
    {
        var exp1 = Bool.True;
        var exp2 = Bool.True;

        Assert.That(exp1.Equals(exp2), Is.True);
    }

    [Test]
    public void DifferentTypesEqualsTest()
    {
        var exp1 = Bool.True;
        var exp2 = Number.Two;

        Assert.That(exp1.Equals(exp2), Is.False);
    }

    [Test]
    public void EqualsBoolNullTest()
    {
        var boolean = Bool.True;

        Assert.That(boolean.Equals(null), Is.False);
    }

    [Test]
    public void EqualsObjectNullTest()
    {
        var boolean = Bool.True;

        Assert.That(boolean.Equals((object)null), Is.False);
    }

    [Test]
    public void EqualsBoolThisTest()
    {
        var boolean = Bool.True;

        Assert.That(boolean.Equals(boolean), Is.True);
    }

    [Test]
    public void EqualsObjectThisTest()
    {
        var boolean = Bool.True;

        Assert.That(boolean.Equals((object)boolean), Is.True);
    }

    [Test]
    public void EqualsObjectTest()
    {
        var exp1 = Bool.True;
        var exp2 = (object)Bool.False;

        Assert.That(exp1.Equals(exp2), Is.False);
    }

    [Test]
    public void ImplicitNullToString()
    {
        Bool x = null;

        Assert.Throws<ArgumentNullException>(() =>
        {
            var temp = (bool)x;
        });
    }

    [Test]
    public void NullAnalyzerTest1()
    {
        var exp = Bool.True;

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Test]
    public void NullAnalyzerTest2()
    {
        var exp = Bool.True;

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }

    [Test]
    public void ToStringTest()
    {
        var trueExp = Bool.True;
        var falseExp = Bool.False;

        Assert.That(trueExp.ToString(), Is.EqualTo("True"));
        Assert.That(falseExp.ToString(), Is.EqualTo("False"));
    }
}