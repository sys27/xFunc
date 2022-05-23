// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Tokenization;

/// <summary>
/// The lexer for mathematical expressions.
/// </summary>
internal ref partial struct Lexer
{
    private bool CreateIdToken()
    {
        if (!char.IsLetter(function[0]))
            return false;

        var endIndex = 1;
        while (endIndex < function.Length && char.IsLetterOrDigit(function[endIndex]))
            endIndex++;

        var id = function[..endIndex];
        function = function[endIndex..];
        Current = Token.Id(id.ToString().ToLowerInvariant()); // TODO:

        return true;
    }
}