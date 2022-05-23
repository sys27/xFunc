// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class LogicalTests : TypeAnalyzerBaseTests
{
    [Fact]
    public void TestBoolValue()
    {
        var exp = Bool.False;

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(Equality))]
    [InlineData(typeof(Implication))]
    [InlineData(typeof(NAnd))]
    [InlineData(typeof(NOr))]
    public void TestUndefined(Type type)
    {
        var exp = CreateBinary(type, Variable.X, Variable.X);

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(Equality))]
    [InlineData(typeof(Implication))]
    [InlineData(typeof(NAnd))]
    [InlineData(typeof(NOr))]
    public void TestBoolUndefined(Type type)
    {
        var exp = CreateBinary(type, Bool.True, Variable.X);

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(Equality))]
    [InlineData(typeof(Implication))]
    [InlineData(typeof(NAnd))]
    [InlineData(typeof(NOr))]
    public void TestUndefinedBool(Type type)
    {
        var exp = CreateBinary(type, Variable.X, Bool.True);

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(Equality))]
    [InlineData(typeof(Implication))]
    [InlineData(typeof(NAnd))]
    [InlineData(typeof(NOr))]
    public void TestBool(Type type)
    {
        var exp = CreateBinary(type, Bool.False, Bool.True);

        Test(exp, ResultTypes.Boolean);
    }

    [Theory]
    [InlineData(typeof(Equality))]
    [InlineData(typeof(Implication))]
    [InlineData(typeof(NAnd))]
    [InlineData(typeof(NOr))]
    public void TestComplexBool(Type type)
    {
        var exp = CreateBinary(type, new ComplexNumber(2, 3), Bool.False);

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(Equality))]
    [InlineData(typeof(Implication))]
    [InlineData(typeof(NAnd))]
    [InlineData(typeof(NOr))]
    public void TestBoolComplex(Type type)
    {
        var exp = CreateBinary(type, Bool.False, new ComplexNumber(2, 3));

        TestBinaryException(exp);
    }

    [Theory]
    [InlineData(typeof(Equality))]
    [InlineData(typeof(Implication))]
    [InlineData(typeof(NAnd))]
    [InlineData(typeof(NOr))]
    public void TestParamTypeException(Type type)
    {
        var exp = CreateBinary(type, new ComplexNumber(2, 3), new ComplexNumber(2, 3));

        TestException(exp);
    }

    [Fact]
    public void TestNotUndefined()
    {
        var exp = new Not(Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Fact]
    public void TestNotNumber()
    {
        var exp = new Not(Number.One);

        Test(exp, ResultTypes.Number);
    }

    [Fact]
    public void TestNotBoolean()
    {
        var exp = new Not(Bool.True);

        Test(exp, ResultTypes.Boolean);
    }

    [Fact]
    public void TestNotException()
    {
        var exp = new Not(new ComplexNumber(1, 2));

        TestException(exp);
    }
}