// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.CompilerServices;
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
        var kind = Empty;

        if (Compare(keyword, "true"))
            kind = TrueKeyword;
        else if (Compare(keyword, "false"))
            kind = FalseKeyword;
        else if (Compare(keyword, "def") || Compare(keyword, "define"))
            kind = DefineKeyword;
        else if (Compare(keyword, "undef") || Compare(keyword, "undefine"))
            kind = UndefineKeyword;
        else if (Compare(keyword, "if"))
            kind = IfKeyword;
        else if (Compare(keyword, "for"))
            kind = ForKeyword;
        else if (Compare(keyword, "while"))
            kind = WhileKeyword;
        else if (Compare(keyword, "nand"))
            kind = NAndKeyword;
        else if (Compare(keyword, "nor"))
            kind = NOrKeyword;
        else if (Compare(keyword, "and"))
            kind = AndKeyword;
        else if (Compare(keyword, "or"))
            kind = OrKeyword;
        else if (Compare(keyword, "xor"))
            kind = XOrKeyword;
        else if (Compare(keyword, "not"))
            kind = NotKeyword;
        else if (Compare(keyword, "eq"))
            kind = EqKeyword;
        else if (Compare(keyword, "impl"))
            kind = ImplKeyword;
        else if (Compare(keyword, "mod"))
            kind = ModKeyword;

        if (kind == Empty)
            return false;

        function = function[endIndex..];
        Current = Token.Create(kind);

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool Compare(ReadOnlySpan<char> id, string str)
        => id.Equals(str, StringComparison.OrdinalIgnoreCase);
}