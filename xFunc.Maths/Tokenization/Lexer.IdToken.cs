// Copyright 2012-2020 Dmytro Kyshchenko
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using static xFunc.Maths.Tokenization.TokenKind;

namespace xFunc.Maths.Tokenization
{
    /// <summary>
    /// The lexer for mathematical expressions.
    /// </summary>
    internal ref partial struct Lexer
    {
        private bool CreateIdToken(ref ReadOnlySpan<char> function)
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

            function = function[endIndex..];

            if (kind == Empty)
                current = new Token(id.ToString().ToLowerInvariant()); // TODO:
            else
                current = new Token(kind);

            return true;
        }
    }
}