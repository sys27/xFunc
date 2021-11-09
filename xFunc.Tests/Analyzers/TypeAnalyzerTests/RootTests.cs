// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class RootTests : TypeAnalyzerBaseTests
{
    [Fact]
    public void TestRootUndefined()
    {
        var exp = new Root(Variable.X, Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Fact]
    public void TestRootUndefinedAndNumber()
    {
        var exp = new Root(Variable.X, Number.Two);

        Test(exp, ResultTypes.Undefined);
    }

    [Fact]
    public void TestRootNumberAndUndefined()
    {
        var exp = new Root(Number.Two, Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Fact]
    public void TestRootNumber()
    {
        var exp = new Root(new Number(4), Number.Two);

        Test(exp, ResultTypes.Undefined);
    }

    [Fact]
    public void TestRootUndefinedAndBool()
    {
        var exp = new Root(Variable.X, Bool.False);

        TestBinaryException(exp);
    }

    [Fact]
    public void TestRootBoolAndUndefined()
    {
        var exp = new Root(Bool.False, Variable.X);

        TestBinaryException(exp);
    }

    [Fact]
    public void TestRootNumberAndBool()
    {
        var exp = new Root(Number.Two, Bool.False);

        TestBinaryException(exp);
    }

    [Fact]
    public void TestRootBoolAndNumber()
    {
        var exp = new Root(Bool.False, Number.Two);

        TestBinaryException(exp);
    }

    [Fact]
    public void TestRootInvalidArgsException()
    {
        var exp = new Root(Bool.False, Bool.False);

        TestException(exp);
    }
}