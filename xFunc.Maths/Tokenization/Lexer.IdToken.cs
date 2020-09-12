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
using xFunc.Maths.Tokenization.Tokens;
using static xFunc.Maths.Tokenization.Tokens.KeywordToken;

namespace xFunc.Maths.Tokenization
{
    /// <summary>
    /// The lexer for mathematical expressions.
    /// </summary>
    internal partial struct Lexer
    {
        private IToken? CreateIdToken(ref ReadOnlySpan<char> function)
        {
            if (!char.IsLetter(function[0]))
                return null;

            var endIndex = 1;
            while (endIndex < function.Length && char.IsLetterOrDigit(function[endIndex]))
                endIndex++;

            var id = function[..endIndex];

            IToken token;

            if (Compare(id, "true"))
                token = True;
            else if (Compare(id, "false"))
                token = False;
            else if (Compare(id, "degrees") || Compare(id, "degree") || Compare(id, "deg"))
                token = Degree;
            else if (Compare(id, "radians") || Compare(id, "radian") || Compare(id, "rad"))
                token = Radian;
            else if (Compare(id, "gradians") || Compare(id, "gradian") || Compare(id, "grad"))
                token = Gradian;
            else if (Compare(id, "def") || Compare(id, "define"))
                token = Define;
            else if (Compare(id, "undef") || Compare(id, "undefine"))
                token = Undefine;
            else if (Compare(id, "if"))
                token = If;
            else if (Compare(id, "for"))
                token = For;
            else if (Compare(id, "while"))
                token = While;
            else if (Compare(id, "nand"))
                token = NAnd;
            else if (Compare(id, "nor"))
                token = NOr;
            else if (Compare(id, "and"))
                token = And;
            else if (Compare(id, "or"))
                token = Or;
            else if (Compare(id, "xor"))
                token = XOr;
            else if (Compare(id, "not"))
                token = Not;
            else if (Compare(id, "eq"))
                token = Eq;
            else if (Compare(id, "impl"))
                token = Impl;
            else if (Compare(id, "mod"))
                token = Mod;
            else
                token = new IdToken(id.ToString().ToLowerInvariant()); // TODO:

            function = function[endIndex..];

            return token;
        }
    }
}