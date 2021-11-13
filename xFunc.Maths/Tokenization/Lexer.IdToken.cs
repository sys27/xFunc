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
    private bool CreateIdToken()
    {
        if (!char.IsLetter(function[0]))
            return false;

        var endIndex = 1;
        while (endIndex < function.Length && char.IsLetterOrDigit(function[endIndex]))
            endIndex++;

        var id = function[..endIndex];

        var kind = Empty;

        if (Compare(id, "true"))
            kind = TrueKeyword;
        else if (Compare(id, "false"))
            kind = FalseKeyword;
        else if (Compare(id, "degrees") || Compare(id, "degree") || Compare(id, "deg"))
            kind = DegreeKeyword;
        else if (Compare(id, "radians") || Compare(id, "radian") || Compare(id, "rad"))
            kind = RadianKeyword;
        else if (Compare(id, "gradians") || Compare(id, "gradian") || Compare(id, "grad"))
            kind = GradianKeyword;
        else if (Compare(id, "def") || Compare(id, "define"))
            kind = DefineKeyword;
        else if (Compare(id, "undef") || Compare(id, "undefine"))
            kind = UndefineKeyword;
        else if (Compare(id, "if"))
            kind = IfKeyword;
        else if (Compare(id, "for"))
            kind = ForKeyword;
        else if (Compare(id, "while"))
            kind = WhileKeyword;
        else if (Compare(id, "nand"))
            kind = NAndKeyword;
        else if (Compare(id, "nor"))
            kind = NOrKeyword;
        else if (Compare(id, "and"))
            kind = AndKeyword;
        else if (Compare(id, "or"))
            kind = OrKeyword;
        else if (Compare(id, "xor"))
            kind = XOrKeyword;
        else if (Compare(id, "not"))
            kind = NotKeyword;
        else if (Compare(id, "eq"))
            kind = EqKeyword;
        else if (Compare(id, "impl"))
            kind = ImplKeyword;
        else if (Compare(id, "mod"))
            kind = ModKeyword;
        else if (Compare(id, "W"))
            kind = WattKeyword;
        else if (Compare(id, "kW"))
            kind = KilowattKeyword;
        else if (Compare(id, "hp"))
            kind = HorsepowerKeyword;

        function = function[endIndex..];

        if (kind == Empty)
            Current = Token.Id(id.ToString().ToLowerInvariant()); // TODO:
        else
            Current = Token.Create(kind);

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool Compare(ReadOnlySpan<char> id, string str)
        => id.Equals(str, StringComparison.OrdinalIgnoreCase);
}