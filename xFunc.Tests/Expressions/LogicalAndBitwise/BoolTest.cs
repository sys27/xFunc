// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.LogicalAndBitwise;

public class BoolTest
{
    [Fact]
    public void ExecuteTest1()
    {
        var exp = Bool.False;

        Assert.False((bool)exp.Execute());
    }

    [Fact]
    public void ExecuteTest2()
    {
        var exp = Bool.False;

        Assert.False((bool)exp.Execute(null));
    }

    [Fact]
    public void ExecuteTest3()
    {
        var exp = Bool.False;

        Assert.False(exp);
    }

    [Fact]
    public void ExecuteTest4()
    {
        var exp = (Bool)false;

        Assert.False((bool)exp.Execute());
    }

    [Fact]
    public void NotEqualsTest()
    {
        var exp1 = Bool.False;
        var exp2 = Bool.True;

        Assert.False(exp1.Equals(exp2));
    }

    [Fact]
    public void EqualsTest()
    {
        var exp1 = Bool.True;
        var exp2 = Bool.True;

        Assert.True(exp1.Equals(exp2));
    }

    [Fact]
    public void DifferentTypesEqualsTest()
    {
        var exp1 = Bool.True;
        var exp2 = Number.Two;

        Assert.False(exp1.Equals(exp2));
    }

    [Fact]
    public void EqualsBoolNullTest()
    {
        var boolean = Bool.True;

        Assert.False(boolean.Equals(null));
    }

    [Fact]
    public void EqualsObjectNullTest()
    {
        var boolean = Bool.True;

        Assert.False(boolean.Equals((object)null));
    }

    [Fact]
    public void EqualsBoolThisTest()
    {
        var boolean = Bool.True;

        Assert.True(boolean.Equals(boolean));
    }

    [Fact]
    public void EqualsObjectThisTest()
    {
        var boolean = Bool.True;

        Assert.True(boolean.Equals((object)boolean));
    }

    [Fact]
    public void EqualsObjectTest()
    {
        var exp1 = Bool.True;
        var exp2 = (object)Bool.False;

        Assert.False(exp1.Equals(exp2));
    }

    [Fact]
    public void ImplicitNullToString()
    {
        Bool x = null;

        Assert.Throws<ArgumentNullException>(() => (bool)x);
    }

    [Fact]
    public void NullAnalyzerTest1()
    {
        var exp = Bool.True;

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Fact]
    public void NullAnalyzerTest2()
    {
        var exp = Bool.True;

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }

    [Fact]
    public void ToStringTest()
    {
        var trueExp = Bool.True;
        var falseExp = Bool.False;

        Assert.Equal("True", trueExp.ToString());
        Assert.Equal("False", falseExp.ToString());
    }
}