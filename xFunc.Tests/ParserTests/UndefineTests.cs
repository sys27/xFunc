// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class UndefineTests : BaseParserTests
{
    [Fact]
    public void UndefParseTest()
    {
        var expected = new Undefine(new Variable("f"));

        ParseTest("undef(f)", expected);
    }

    [Theory]
    [InlineData("undef x)")]
    [InlineData("undef()")]
    [InlineData("undef(x")]
    public void UndefMissingPartsTest(string function)
        => ParseErrorTest(function);
}