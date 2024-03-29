// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests.ProgrammingTests;

public class ProgrammingTest : TypeAnalyzerBaseTests
{
    [Test]
    public void TestDec()
    {
        var exp = new Dec(Variable.X);

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestForUndefined()
    {
        var exp = new For(Variable.X, Variable.X, Variable.X, Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestForNumber()
    {
        var exp = new For(Variable.X, Variable.X, Bool.False, Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestForException()
    {
        var exp = new For(Variable.X, Variable.X, new ComplexNumber(2, 3), Variable.X);

        TestException(exp);
    }

    [Test]
    public void TestIfUndefined()
    {
        var exp = new If(Variable.X, new Number(10));

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestIfBool()
    {
        var exp = new If(Bool.False, new Number(10));

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestIfElseBool()
    {
        var exp = new If(Bool.False, new Number(10), Number.Two);

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestIfException()
    {
        var exp = new If(new ComplexNumber(2, 4), new Number(10));

        TestDiffParamException(exp);
    }

    [Test]
    public void TestInc()
    {
        var exp = new Inc(Variable.X);

        Test(exp, ResultTypes.Number);
    }


    [Test]
    public void TestWhileUndefined()
    {
        var exp = new While(Variable.X, Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestWhileNumber()
    {
        var exp = new While(Variable.X, Bool.False);

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestWhileException()
    {
        var exp = new While(Variable.X, Number.One);

        TestException(exp);
    }
}