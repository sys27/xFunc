// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests.ProgrammingTests;

public class ShiftOperatorsTests : TypeAnalyzerBaseTests
{
    [Theory]
    [InlineData(typeof(LeftShift))]
    [InlineData(typeof(RightShift))]
    public void TestAssignUndefined(Type type)
    {
        var exp = Create(type, Variable.X, Variable.X);

        Test(exp, ResultTypes.Number);
    }

    [Theory]
    [InlineData(typeof(LeftShift))]
    [InlineData(typeof(RightShift))]
    public void TestAssignUndefinedNumber(Type type)
    {
        var exp = Create(type, Variable.X, Number.One);

        Test(exp, ResultTypes.Number);
    }

    [Theory]
    [InlineData(typeof(LeftShift))]
    [InlineData(typeof(RightShift))]
    public void TestAssignNumberUndefined(Type type)
    {
        var exp = Create(type, Number.One, Variable.X);

        Test(exp, ResultTypes.Number);
    }

    [Theory]
    [InlineData(typeof(LeftShift))]
    [InlineData(typeof(RightShift))]
    public void TestAssignNumbers(Type type)
    {
        var exp = Create(type, Number.One, Number.One);

        Test(exp, ResultTypes.Number);
    }

    [Theory]
    [InlineData(typeof(LeftShift))]
    [InlineData(typeof(RightShift))]
    public void TestAssignLeftException(Type type)
    {
        var exp = CreateBinary(type, Bool.False, Number.One);

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(LeftShift))]
    [InlineData(typeof(RightShift))]
    public void TestAssignRightException(Type type)
    {
        var exp = CreateBinary(type, Number.One, Bool.False);

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(LeftShift))]
    [InlineData(typeof(RightShift))]
    public void TestAssignException(Type type)
    {
        var exp = Create(type, Bool.False, Bool.False);

        TestException(exp);
    }
}