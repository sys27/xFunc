// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class UnassignTests : BaseParserTests
{
    [Fact]
    public void UndefParseTest()
    {
        var expected = new Unassign(new Variable("f"));

        ParseTest("unassign(f)", expected);
    }

    [Theory]
    [InlineData("unassign x)")]
    [InlineData("unassign()")]
    [InlineData("unassign(x")]
    public void UndefMissingPartsTest(string function)
        => ParseErrorTest(function);
}