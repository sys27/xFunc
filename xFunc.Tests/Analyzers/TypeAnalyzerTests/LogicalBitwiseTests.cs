// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class LogicalBitwiseTests : TypeAnalyzerBaseTests
{
    [Test]
    [TestCase(typeof(And))]
    [TestCase(typeof(Or))]
    [TestCase(typeof(XOr))]
    public void TestLeftUndefined(Type type)
    {
        var exp = Create(type, Variable.X, Number.One);

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    [TestCase(typeof(And))]
    [TestCase(typeof(Or))]
    [TestCase(typeof(XOr))]
    public void TestRightUndefined(Type type)
    {
        var exp = Create(type, Number.One, Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    [TestCase(typeof(And))]
    [TestCase(typeof(Or))]
    [TestCase(typeof(XOr))]
    public void TestNumber(Type type)
    {
        var exp = Create(type, Number.One, Number.One);

        Test(exp, ResultTypes.Number);
    }

    [Test]
    [TestCase(typeof(And))]
    [TestCase(typeof(Or))]
    [TestCase(typeof(XOr))]
    public void TestBool(Type type)
    {
        var exp = Create(type, Bool.True, Bool.True);

        Test(exp, ResultTypes.Boolean);
    }

    [Test]
    [TestCase(typeof(And))]
    [TestCase(typeof(Or))]
    [TestCase(typeof(XOr))]
    public void TestComplexAndNumber(Type type)
    {
        var exp = CreateBinary(type, new ComplexNumber(3, 2), Number.One);

        TestBinaryException(exp);
    }

    [Test]
    [TestCase(typeof(And))]
    [TestCase(typeof(Or))]
    [TestCase(typeof(XOr))]
    public void TestNumberAndComplex(Type type)
    {
        var exp = CreateBinary(type, Number.One, new ComplexNumber(3, 2));

        TestBinaryException(exp);
    }

    [Test]
    [TestCase(typeof(And))]
    [TestCase(typeof(Or))]
    [TestCase(typeof(XOr))]
    public void TestComplexAndBool(Type type)
    {
        var exp = CreateBinary(type, new ComplexNumber(3, 2), Bool.False);

        TestBinaryException(exp);
    }

    [Test]
    [TestCase(typeof(And))]
    [TestCase(typeof(Or))]
    [TestCase(typeof(XOr))]
    public void TestBoolAndComplex(Type type)
    {
        var exp = CreateBinary(type, Bool.False, new ComplexNumber(3, 2));

        TestBinaryException(exp);
    }

    [Test]
    [TestCase(typeof(And))]
    [TestCase(typeof(Or))]
    [TestCase(typeof(XOr))]
    public void TestParamTypeException(Type type)
    {
        var exp = Create(type, new ComplexNumber(3, 2), new ComplexNumber(3, 2));

        TestException(exp);
    }
}