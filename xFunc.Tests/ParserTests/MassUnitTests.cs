// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class MassUnitTests : BaseParserTests
{
    [Fact]
    public void ParseKilogram()
        => ParseTest("1 kg", MassValue.Kilogram(1).AsExpression());

    [Fact]
    public void ParseGram()
        => ParseTest("1 g", MassValue.Gram(1).AsExpression());

    [Fact]
    public void ParseMilligram()
        => ParseTest("1 mg", MassValue.Milligram(1).AsExpression());

    [Fact]
    public void ParseTonne()
        => ParseTest("1 t", MassValue.Tonne(1).AsExpression());

    [Fact]
    public void ParseOunce()
        => ParseTest("1 oz", MassValue.Ounce(1).AsExpression());

    [Fact]
    public void ParsePound()
        => ParseTest("1 lb", MassValue.Pound(1).AsExpression());
}