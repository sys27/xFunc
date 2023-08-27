// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class VolumeUnitTests : BaseParserTests
{
    [Test]
    public void ParseSquareMeter()
        => ParseTest("1 'm^3'", VolumeValue.Meter(1).AsExpression());

    [Test]
    public void ParseSquareCentimeter()
        => ParseTest("1 'cm^3'", VolumeValue.Centimeter(1).AsExpression());

    [Test]
    public void ParseSquareLiter()
        => ParseTest("1 'l'", VolumeValue.Liter(1).AsExpression());

    [Test]
    public void ParseSquareInch()
        => ParseTest("1 'in^3'", VolumeValue.Inch(1).AsExpression());

    [Test]
    public void ParseSquareFoot()
        => ParseTest("1 'ft^3'", VolumeValue.Foot(1).AsExpression());

    [Test]
    public void ParseSquareYard()
        => ParseTest("1 'yd^3'", VolumeValue.Yard(1).AsExpression());

    [Test]
    public void ParseGallon()
        => ParseTest("1 'gal'", VolumeValue.Gallon(1).AsExpression());

    [Test]
    public void ParseSquareUnitWithoutExponent()
        => ParseErrorTest("1 'cm^'");
}