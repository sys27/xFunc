// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class TimeUnitTests : BaseParserTests
{
    [Fact]
    public void ParseSecond()
        => ParseTest("1 s", TimeValue.Second(1).AsExpression());

    [Fact]
    public void ParseNanosecond()
        => ParseTest("1 ns", TimeValue.Nanosecond(1).AsExpression());

    [Fact]
    public void ParseMicrosecond()
        => ParseTest("1 μs", TimeValue.Microsecond(1).AsExpression());

    [Fact]
    public void ParseMillisecond()
        => ParseTest("1 ms", TimeValue.Millisecond(1).AsExpression());

    [Fact]
    public void ParseMinute()
        => ParseTest("1 min", TimeValue.Minute(1).AsExpression());

    [Fact]
    public void ParseHour()
        => ParseTest("1 h", TimeValue.Hour(1).AsExpression());

    [Fact]
    public void ParseDay()
        => ParseTest("1 day", TimeValue.Day(1).AsExpression());

    [Fact]
    public void ParseWeek()
        => ParseTest("1 week", TimeValue.Week(1).AsExpression());

    [Fact]
    public void ParseYear()
        => ParseTest("1 year", TimeValue.Year(1).AsExpression());
}