// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class AreaUnitTests : BaseParserTests
{
    [Fact]
    public void ParseSquareMeter()
        => ParseTest("1 m^2", AreaValue.Meter(1).AsExpression());

    [Fact]
    public void ParseSquareMillimeter()
        => ParseTest("1 mm^2", AreaValue.Millimeter(1).AsExpression());

    [Fact]
    public void ParseSquareCentimeter()
        => ParseTest("1 cm^2", AreaValue.Centimeter(1).AsExpression());

    [Fact]
    public void ParseSquareKilometer()
        => ParseTest("1 km^2", AreaValue.Kilometer(1).AsExpression());

    [Fact]
    public void ParseSquareInch()
        => ParseTest("1 in^2", AreaValue.Inch(1).AsExpression());

    [Fact]
    public void ParseSquareFoot()
        => ParseTest("1 ft^2", AreaValue.Foot(1).AsExpression());

    [Fact]
    public void ParseSquareYard()
        => ParseTest("1 yd^2", AreaValue.Yard(1).AsExpression());

    [Fact]
    public void ParseSquareMile()
        => ParseTest("1 mi^2", AreaValue.Mile(1).AsExpression());

    [Fact]
    public void ParseHectare()
        => ParseTest("1 ha", AreaValue.Hectare(1).AsExpression());

    [Fact]
    public void ParseAcre()
        => ParseTest("1 ac", AreaValue.Acre(1).AsExpression());

    [Fact]
    public void ParseSquareUnitWithoutExponent()
        => ParseErrorTest("1 mi^");

    [Fact]
    public void ParseSquareUnitIncorrectExponent()
        => ParseTest("1 mi^10", new Pow(LengthValue.Mile(1).AsExpression(), new Number(10)));
}