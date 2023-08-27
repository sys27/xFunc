// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.SimplifierTests;

public class TrigonometricSimplifierTest : BaseSimplifierTest
{
    [Test]
    [TestCase(typeof(Arcsin), typeof(Sin))]
    [TestCase(typeof(Arccos), typeof(Cos))]
    [TestCase(typeof(Arctan), typeof(Tan))]
    [TestCase(typeof(Arccot), typeof(Cot))]
    [TestCase(typeof(Arcsec), typeof(Sec))]
    [TestCase(typeof(Arccsc), typeof(Csc))]
    [TestCase(typeof(Tan), typeof(Arctan))]
    [TestCase(typeof(Cot), typeof(Arccot))]
    [TestCase(typeof(Arsinh), typeof(Sinh))]
    [TestCase(typeof(Arcosh), typeof(Cosh))]
    [TestCase(typeof(Artanh), typeof(Tanh))]
    [TestCase(typeof(Arcoth), typeof(Coth))]
    [TestCase(typeof(Arsech), typeof(Sech))]
    [TestCase(typeof(Arcsch), typeof(Csch))]
    [TestCase(typeof(Sinh), typeof(Arsinh))]
    public void InverseFunctionsTest(Type outer, Type inner)
    {
        var innerFunction = Create(inner, Variable.X);
        var outerFunction = Create(outer, innerFunction);
        var expected = Variable.X;

        SimplifyTest(outerFunction, expected);
    }

    public static IEnumerable<object[]> GetInverseDomainTestData()
    {
        yield return new object[] { new Sin(new Arcsin(Variable.X)), new Sin(new Arcsin(Variable.X)) };

        yield return new object[] { new Sin(new Arcsin(Number.One)), Number.One };

        yield return new object[] { new Sin(new Arcsin(Number.Two)), new Sin(new Arcsin(Number.Two)) };

        yield return new object[] { new Cos(new Arccos(Variable.X)), new Cos(new Arccos(Variable.X)) };

        yield return new object[] { new Cos(new Arccos(Number.One)), Number.One };

        yield return new object[] { new Cos(new Arccos(Number.Two)), new Cos(new Arccos(Number.Two)) };

        yield return new object[] { new Sec(new Arcsec(Variable.X)), new Sec(new Arcsec(Variable.X)) };

        yield return new object[] { new Sec(new Arcsec(Number.Two)), Number.Two };

        yield return new object[] { new Sec(new Arcsec(Number.Zero)), new Sec(new Arcsec(Number.Zero)) };

        yield return new object[] { new Csc(new Arccsc(Variable.X)), new Csc(new Arccsc(Variable.X)) };

        yield return new object[] { new Csc(new Arccsc(Number.Two)), Number.Two };

        yield return new object[] { new Csc(new Arccsc(Number.Zero)), new Csc(new Arccsc(Number.Zero)) };

        yield return new object[] { new Cosh(new Arcosh(Number.Zero)), new Cosh(new Arcosh(Number.Zero)) };

        yield return new object[] { new Cosh(new Arcosh(Number.One)), Number.One };

        yield return new object[] { new Tanh(new Artanh(new Number(-1))), new Tanh(new Artanh(new Number(-1))) };

        yield return new object[] { new Tanh(new Artanh(Number.One)), new Tanh(new Artanh(Number.One)) };

        yield return new object[] { new Tanh(new Artanh(Number.Zero)), Number.Zero };

        yield return new object[] { new Csch(new Arcsch(new Number(-1))), new Number(-1) };

        yield return new object[] { new Csch(new Arcsch(Number.One)), Number.One };

        yield return new object[] { new Csch(new Arcsch(Number.Zero)), new Csch(new Arcsch(Number.Zero)) };

        yield return new object[] { new Sech(new Arsech(new Number(-1))), new Sech(new Arsech(new Number(-1))) };

        yield return new object[] { new Sech(new Arsech(Number.Two)), new Sech(new Arsech(Number.Two)) };

        yield return new object[] { new Sech(new Arsech(Number.One)), Number.One };

        yield return new object[] { new Coth(new Arcoth(new Number(-2))), new Number(-2) };

        yield return new object[] { new Coth(new Arcoth(Number.Zero)), new Coth(new Arcoth(Number.Zero)) };

        yield return new object[] { new Coth(new Arcoth(Number.Two)), Number.Two };
    }

    [Test]
    [TestCaseSource(nameof(GetInverseDomainTestData))]
    public void InverseDomainTest(IExpression exp, IExpression expected)
        => SimplifyTest(exp, expected);

    [Test]
    [TestCase(typeof(Arcsin))]
    [TestCase(typeof(Arccos))]
    [TestCase(typeof(Arctan))]
    [TestCase(typeof(Arccot))]
    [TestCase(typeof(Arcsec))]
    [TestCase(typeof(Arccsc))]
    [TestCase(typeof(Sin))]
    [TestCase(typeof(Cos))]
    [TestCase(typeof(Tan))]
    [TestCase(typeof(Cot))]
    [TestCase(typeof(Sec))]
    [TestCase(typeof(Csc))]
    public void SimplifyArgumentTest(Type type)
    {
        var exp = Create(type, new Add(Number.One, Number.Two));
        var expected = Create(type, new Number(3));

        SimplifyTest(exp, expected);
    }

    [Test]
    [TestCase(typeof(Cosh))]
    [TestCase(typeof(Coth))]
    [TestCase(typeof(Csch))]
    [TestCase(typeof(Sech))]
    [TestCase(typeof(Tanh))]
    public void CoshCopy(Type expType)
    {
        var exp = Create(expType, new Add(Number.One, Number.One));
        var expected = Create(expType, Number.Two);

        SimplifyTest(exp, expected);
    }
}