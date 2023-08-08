// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class AngleTests : BaseParserTests
{
    [Theory]
    [InlineData("1 'deg'")]
    [InlineData("1 'degree'")]
    [InlineData("1 'degrees'")]
    [InlineData("1°")]
    public void AngleDeg(string function)
        => ParseTest(function, AngleValue.Degree(1).AsExpression());

    [Theory]
    [InlineData("1 'rad'")]
    [InlineData("1 'radian'")]
    [InlineData("1 'radians'")]
    public void AngleRad(string function)
        => ParseTest(function, AngleValue.Radian(1).AsExpression());

    [Theory]
    [InlineData("1 'grad'")]
    [InlineData("1 'gradian'")]
    [InlineData("1 'gradians'")]
    public void AngleGrad(string function)
        => ParseTest(function, AngleValue.Gradian(1).AsExpression());

    [Theory]
    [InlineData("todeg(1 'deg')")]
    [InlineData("todegree(1 'deg')")]
    public void ToDegTest(string function)
        => ParseTest(function, new ToDegree(AngleValue.Degree(1).AsExpression()));

    [Theory]
    [InlineData("torad(1 'deg')")]
    [InlineData("toradian(1 'deg')")]
    public void ToRadTest(string function)
        => ParseTest(function, new ToRadian(AngleValue.Degree(1).AsExpression()));

    [Theory]
    [InlineData("tograd(1 'deg')")]
    [InlineData("togradian(1 'deg')")]
    public void ToGradTest(string function)
        => ParseTest(function, new ToGradian(AngleValue.Degree(1).AsExpression()));

    [Fact]
    public void ToNumberTest()
        => ParseTest("tonumber(1 'deg')", new ToNumber(AngleValue.Degree(1).AsExpression()));
}