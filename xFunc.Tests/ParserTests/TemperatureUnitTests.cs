// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class TemperatureUnitTests : BaseParserTests
{
    [Test]
    public void ParseCelsius()
        => ParseTest("1 '°C'", TemperatureValue.Celsius(1).AsExpression());

    [Test]
    public void ParseFahrenheit()
        => ParseTest("1 '°F'", TemperatureValue.Fahrenheit(1).AsExpression());

    [Test]
    public void ParseKelvin()
        => ParseTest("1 'K'", TemperatureValue.Kelvin(1).AsExpression());
}