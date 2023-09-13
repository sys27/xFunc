// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class HyperbolicTests : TypeAnalyzerBaseTests
{
    [Test]
    [TestCase(typeof(Arcosh))]
    [TestCase(typeof(Arcoth))]
    [TestCase(typeof(Arcsch))]
    [TestCase(typeof(Arsech))]
    [TestCase(typeof(Arsinh))]
    [TestCase(typeof(Artanh))]
    [TestCase(typeof(Cosh))]
    [TestCase(typeof(Coth))]
    [TestCase(typeof(Csch))]
    [TestCase(typeof(Sech))]
    [TestCase(typeof(Sinh))]
    [TestCase(typeof(Tanh))]
    public void TestUndefined(Type type)
    {
        var exp = Create(type, Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    [TestCase(typeof(Arcosh))]
    [TestCase(typeof(Arcoth))]
    [TestCase(typeof(Arcsch))]
    [TestCase(typeof(Arsech))]
    [TestCase(typeof(Arsinh))]
    [TestCase(typeof(Artanh))]
    public void TestAngleNumber(Type type)
    {
        var exp = Create(type, Number.Two);

        Test(exp, ResultTypes.AngleNumber);
    }

    [Test]
    [TestCase(typeof(Cosh))]
    [TestCase(typeof(Coth))]
    [TestCase(typeof(Csch))]
    [TestCase(typeof(Sech))]
    [TestCase(typeof(Sinh))]
    [TestCase(typeof(Tanh))]
    public void TestNumber(Type type)
    {
        var exp = Create(type, Number.Two);

        Test(exp, ResultTypes.Number);
    }

    [Test]
    [TestCase(typeof(Arcosh))]
    [TestCase(typeof(Arcoth))]
    [TestCase(typeof(Arcsch))]
    [TestCase(typeof(Arsech))]
    [TestCase(typeof(Arsinh))]
    [TestCase(typeof(Artanh))]
    [TestCase(typeof(Cosh))]
    [TestCase(typeof(Coth))]
    [TestCase(typeof(Csch))]
    [TestCase(typeof(Sech))]
    [TestCase(typeof(Sinh))]
    [TestCase(typeof(Tanh))]
    public void TestComplexNumber(Type type)
    {
        var exp = Create(type, new ComplexNumber(2, 2));

        Test(exp, ResultTypes.ComplexNumber);
    }

    [Test]
    [TestCase(typeof(Arcosh))]
    [TestCase(typeof(Arcoth))]
    [TestCase(typeof(Arcsch))]
    [TestCase(typeof(Arsech))]
    [TestCase(typeof(Arsinh))]
    [TestCase(typeof(Artanh))]
    [TestCase(typeof(Cosh))]
    [TestCase(typeof(Coth))]
    [TestCase(typeof(Csch))]
    [TestCase(typeof(Sech))]
    [TestCase(typeof(Sinh))]
    [TestCase(typeof(Tanh))]
    public void TestParameterException(Type type)
    {
        var exp = Create(type, Bool.False);

        TestException(exp);
    }
}