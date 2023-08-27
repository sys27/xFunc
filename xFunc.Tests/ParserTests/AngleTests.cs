// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class AngleTests : BaseParserTests
{
    [Test]
    [TestCase("1 'deg'")]
    [TestCase("1 'degree'")]
    [TestCase("1 'degrees'")]
    [TestCase("1°")]
    public void AngleDeg(string function)
        => ParseTest(function, AngleValue.Degree(1).AsExpression());

    [Test]
    [TestCase("1 'rad'")]
    [TestCase("1 'radian'")]
    [TestCase("1 'radians'")]
    public void AngleRad(string function)
        => ParseTest(function, AngleValue.Radian(1).AsExpression());

    [Test]
    [TestCase("1 'grad'")]
    [TestCase("1 'gradian'")]
    [TestCase("1 'gradians'")]
    public void AngleGrad(string function)
        => ParseTest(function, AngleValue.Gradian(1).AsExpression());

    [Test]
    [TestCase("todeg(1 'deg')")]
    [TestCase("todegree(1 'deg')")]
    public void ToDegTest(string function)
        => ParseTest(function, new ToDegree(AngleValue.Degree(1).AsExpression()));

    [Test]
    [TestCase("torad(1 'deg')")]
    [TestCase("toradian(1 'deg')")]
    public void ToRadTest(string function)
        => ParseTest(function, new ToRadian(AngleValue.Degree(1).AsExpression()));

    [Test]
    [TestCase("tograd(1 'deg')")]
    [TestCase("togradian(1 'deg')")]
    public void ToGradTest(string function)
        => ParseTest(function, new ToGradian(AngleValue.Degree(1).AsExpression()));

    [Test]
    public void ToNumberTest()
        => ParseTest("tonumber(1 'deg')", new ToNumber(AngleValue.Degree(1).AsExpression()));
}