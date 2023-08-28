// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests.ProgrammingTests;

public class AssignmentOperatorsTests : TypeAnalyzerBaseTests
{
    [Test]
    [TestCase(typeof(AddAssign))]
    [TestCase(typeof(SubAssign))]
    [TestCase(typeof(MulAssign))]
    [TestCase(typeof(DivAssign))]
    [TestCase(typeof(LeftShiftAssign))]
    [TestCase(typeof(RightShiftAssign))]
    public void TestAssignUndefined(Type type)
    {
        var exp = Create(type, Variable.X, Variable.X);

        Test(exp, ResultTypes.Number);
    }

    [Test]
    [TestCase(typeof(AddAssign))]
    [TestCase(typeof(SubAssign))]
    [TestCase(typeof(MulAssign))]
    [TestCase(typeof(DivAssign))]
    [TestCase(typeof(LeftShiftAssign))]
    [TestCase(typeof(RightShiftAssign))]
    public void TestAssignNumber(Type type)
    {
        var exp = Create(type, Variable.X, new Number(10));

        Test(exp, ResultTypes.Number);
    }

    [Test]
    [TestCase(typeof(AddAssign))]
    [TestCase(typeof(SubAssign))]
    [TestCase(typeof(MulAssign))]
    [TestCase(typeof(DivAssign))]
    [TestCase(typeof(LeftShiftAssign))]
    [TestCase(typeof(RightShiftAssign))]
    public void TestAssignException(Type type)
    {
        var exp = Create(type, Variable.X, Bool.False);

        TestException(exp);
    }
}