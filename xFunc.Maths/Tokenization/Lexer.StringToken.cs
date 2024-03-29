// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Tokenization;

/// <summary>
/// The lexer for mathematical expressions.
/// </summary>
internal ref partial struct Lexer
{
    private bool CreateStringToken(char quote)
    {
        if (function[0] != quote)
            return false;

        var endIndex = function[1..].IndexOf(quote) + 1;
        if (endIndex == 0)
            throw new TokenizeException(Resource.StringTokenizeException);

        var stringValue = function[1..endIndex];

        Current = Token.String(stringValue.ToString());

        function = function[(endIndex + 1)..];

        return true;
    }

    private bool CreateStringToken()
        => CreateStringToken('"') ||
           CreateStringToken('\'');
}