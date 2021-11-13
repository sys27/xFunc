// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests.ProgrammingTests;

public class ConditionalOperatorsTests : TypeAnalyzerBaseTests
{
    [Theory]
    [InlineData(typeof(ConditionalAnd))]
    [InlineData(typeof(ConditionalOr))]
    public void TestUndefined(Type type)
    {
        var exp = CreateBinary(type, Variable.X, Variable.X);

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(ConditionalAnd))]
    [InlineData(typeof(ConditionalOr))]
    public void TestBoolUndefined(Type type)
    {
        var exp = CreateBinary(type, Bool.True, Variable.X);

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(ConditionalAnd))]
    [InlineData(typeof(ConditionalOr))]
    public void TestUndefinedBool(Type type)
    {
        var exp = CreateBinary(type, Variable.X, Bool.True);

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(ConditionalAnd))]
    [InlineData(typeof(ConditionalOr))]
    public void TestBool(Type type)
    {
        var exp = CreateBinary(type, Bool.False, Bool.True);

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(ConditionalAnd))]
    [InlineData(typeof(ConditionalOr))]
    public void TestComplexBool(Type type)
    {
        var exp = CreateBinary(type, new ComplexNumber(2, 3), Bool.False);

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(ConditionalAnd))]
    [InlineData(typeof(ConditionalOr))]
    public void TestBoolComplex(Type type)
    {
        var exp = CreateBinary(type, Bool.False, new ComplexNumber(2, 3));

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(ConditionalAnd))]
    [InlineData(typeof(ConditionalOr))]
    public void TestConditionalException(Type type)
    {
        var exp = CreateBinary(type, new ComplexNumber(2, 3), new ComplexNumber(2, 3));

        TestException(exp);
    }
}