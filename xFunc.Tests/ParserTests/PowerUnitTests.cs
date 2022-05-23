// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class PowerUnitTests : BaseParserTests
{
    [Fact]
    public void ParseWatt()
        => ParseTest("1 W", PowerValue.Watt(1).AsExpression());

    [Fact]
    public void ParseKilowatt()
        => ParseTest("1 kW", PowerValue.Kilowatt(1).AsExpression());

    [Fact]
    public void ParseHorsepower()
        => ParseTest("1 hp", PowerValue.Horsepower(1).AsExpression());
}