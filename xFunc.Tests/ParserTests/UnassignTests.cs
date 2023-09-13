// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class UnassignTests : BaseParserTests
{
    [Test]
    public void UndefParseTest()
    {
        var expected = new Unassign(new Variable("f"));

        ParseTest("unassign(f)", expected);
    }

    [Test]
    [TestCase("unassign x)")]
    [TestCase("unassign()")]
    [TestCase("unassign(x")]
    public void UndefMissingPartsTest(string function)
        => ParseErrorTest(function);
}