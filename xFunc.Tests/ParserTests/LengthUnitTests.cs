// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class LengthUnitTests : BaseParserTests
{
    [Test]
    public void ParseMeter()
        => ParseTest("1 'm'", LengthValue.Meter(1).AsExpression());

    [Test]
    public void ParseNanometer()
        => ParseTest("1 'nm'", LengthValue.Nanometer(1).AsExpression());

    [Test]
    public void ParseMicrometer()
        => ParseTest("1 'µm'", LengthValue.Micrometer(1).AsExpression());

    [Test]
    public void ParseMillimeter()
        => ParseTest("1 'mm'", LengthValue.Millimeter(1).AsExpression());

    [Test]
    public void ParseCentimeter()
        => ParseTest("1 'cm'", LengthValue.Centimeter(1).AsExpression());

    [Test]
    public void ParseDecimeter()
        => ParseTest("1 'dm'", LengthValue.Decimeter(1).AsExpression());

    [Test]
    public void ParseKilometer()
        => ParseTest("1 'km'", LengthValue.Kilometer(1).AsExpression());

    [Test]
    public void ParseInch()
        => ParseTest("1 'in'", LengthValue.Inch(1).AsExpression());

    [Test]
    public void ParseFoot()
        => ParseTest("1 'ft'", LengthValue.Foot(1).AsExpression());

    [Test]
    public void ParseYard()
        => ParseTest("1 'yd'", LengthValue.Yard(1).AsExpression());

    [Test]
    public void ParseMile()
        => ParseTest("1 'mi'", LengthValue.Mile(1).AsExpression());

    [Test]
    public void ParseNauticalMile()
        => ParseTest("1 'nmi'", LengthValue.NauticalMile(1).AsExpression());

    [Test]
    public void ParseChain()
        => ParseTest("1 'ch'", LengthValue.Chain(1).AsExpression());

    [Test]
    public void ParseRod()
        => ParseTest("1 'rd'", LengthValue.Rod(1).AsExpression());

    [Test]
    public void ParseAstronomicalUnit()
        => ParseTest("1 'au'", LengthValue.AstronomicalUnit(1).AsExpression());

    [Test]
    public void ParseLightYear()
        => ParseTest("1 'ly'", LengthValue.LightYear(1).AsExpression());

    [Test]
    public void ParseParsec()
        => ParseTest("1 'pc'", LengthValue.Parsec(1).AsExpression());
}