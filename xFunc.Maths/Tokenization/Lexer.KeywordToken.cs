// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using static xFunc.Maths.Tokenization.TokenKind;

namespace xFunc.Maths.Tokenization;

/// <summary>
/// The lexer for mathematical expressions.
/// </summary>
internal ref partial struct Lexer
{
    private bool CreateKeywordToken()
    {
        if (!char.IsLetter(function[0]) && function[0] != '°')
            return false;

        var endIndex = 1;
        while (endIndex < function.Length && char.IsLetterOrDigit(function[endIndex]))
            endIndex++;

        var keyword = function[..endIndex];

        var lowerKeyword = keyword.Length <= 1024
            ? stackalloc char[keyword.Length]
            : new char[keyword.Length];

        keyword.ToLowerInvariant(lowerKeyword);

        var kind = lowerKeyword switch
        {
            "true" => TrueKeyword,
            "false" => FalseKeyword,
            "assign" => AssignKeyword,
            "unassign" => UnassignKeyword,
            "if" => IfKeyword,
            "for" => ForKeyword,
            "while" => WhileKeyword,
            "nand" => NAndKeyword,
            "nor" => NOrKeyword,
            "and" => AndKeyword,
            "or" => OrKeyword,
            "xor" => XOrKeyword,
            "not" => NotKeyword,
            "eq" => EqKeyword,
            "impl" => ImplKeyword,
            "mod" => ModKeyword,
            _ => Empty,
        };

        if (kind == Empty)
            return false;

        function = function[endIndex..];
        Current = Token.Create(kind);

        return true;
    }
}