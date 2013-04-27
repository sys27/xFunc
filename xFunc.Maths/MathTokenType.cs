// Copyright 2012-2013 Dmitry Kischenko
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

namespace xFunc.Maths
{

    /// <summary>
    /// Describes a type of token.
    /// </summary>
    /// <seealso cref="MathToken"/>
    public enum MathTokenType
    {

        /// <summary>
        /// :=
        /// </summary>
        Assign         = 0,
        /// <summary>
        /// undef()
        /// </summary>
        Undefine       = 31,
        /// <summary>
        /// (
        /// </summary>
        OpenBracket    = 2,
        /// <summary>
        /// )
        /// </summary>
        CloseBracket   = 3,
        /// <summary>
        /// ,
        /// </summary>
        Comma          = 4,
        /// <summary>
        /// +
        /// </summary>
        Addition       = 5,
        /// <summary>
        /// -
        /// </summary>
        Subtraction    = 6,
        /// <summary>
        /// *
        /// </summary>
        Multiplication = 7,
        /// <summary>
        /// /
        /// </summary>
        Division       = 8,
        /// <summary>
        /// ^
        /// </summary>
        Exponentiation = 9,
        /// <summary>
        /// - (Unary)
        /// </summary>
        UnaryMinus     = 10,
        /// <summary>
        /// abs
        /// </summary>
        Absolute       = 11,
        /// <summary>
        /// sin
        /// </summary>
        Sine           = 12,
        /// <summary>
        /// cos
        /// </summary>
        Cosine         = 13,
        /// <summary>
        /// tg
        /// </summary>
        Tangent        = 14,
        /// <summary>
        /// Cot
        /// </summary>
        Cotangent      = 15,
        /// <summary>
        /// sec
        /// </summary>
        Secant         = 16,
        /// <summary>
        /// csc
        /// </summary>
        Cosecant       = 17,
        /// <summary>
        /// Arcsin
        /// </summary>
        Arcsine        = 18,
        /// <summary>
        /// Arccos
        /// </summary>
        Arccosine      = 19,
        /// <summary>
        /// Arctan
        /// </summary>
        Arctangent     = 20,
        /// <summary>
        /// Arccot
        /// </summary>
        Arccotangent   = 21,
        /// <summary>
        /// arcsec
        /// </summary>
        Arcsecant      = 22,
        /// <summary>
        /// arccsc
        /// </summary>
        Arccosecant    = 23,
        /// <summary>
        /// sqrt
        /// </summary>
        Sqrt           = 24,
        /// <summary>
        /// root
        /// </summary>
        Root           = 25,
        /// <summary>
        /// Ln
        /// </summary>
        Ln             = 26,
        /// <summary>
        /// Lg
        /// </summary>
        Lg             = 27,
        /// <summary>
        /// Log
        /// </summary>
        Log            = 28,

        /// <summary>
        /// sinh
        /// </summary>
        Sineh          = 40,
        /// <summary>
        /// cosh
        /// </summary>
        Cosineh        = 41,
        /// <summary>
        /// tanh
        /// </summary>
        Tangenth       = 42,
        /// <summary>
        /// coth
        /// </summary>
        Cotangenth     = 43,
        /// <summary>
        /// sech
        /// </summary>
        Secanth        = 44,
        /// <summary>
        /// csch
        /// </summary>
        Cosecanth      = 45,
        /// <summary>
        /// arsinh
        /// </summary>
        Arsineh        = 46,
        /// <summary>
        /// arcosh
        /// </summary>
        Arcosineh      = 47,
        /// <summary>
        /// artanh
        /// </summary>
        Artangenth     = 48,
        /// <summary>
        /// arcoth
        /// </summary>
        Arcotangenth   = 49,
        /// <summary>
        /// arsech
        /// </summary>
        Arsecanth      = 50,
        /// <summary>
        /// arcsch
        /// </summary>
        Arcosecanth    = 51,

        /// <summary>
        /// not
        /// </summary>
        Not            = 60,
        /// <summary>
        /// and
        /// </summary>
        And            = 61,
        /// <summary>
        /// or
        /// </summary>
        Or             = 62,
        /// <summary>
        /// xor
        /// </summary>
        XOr            = 63,

        /// <summary>
        /// Exponential
        /// </summary>
        E              = 29,
        /// <summary>
        /// deriv
        /// </summary>
        Derivative     = 30,

        /// <summary>
        /// Number.
        /// </summary>
        Number         = 100,
        /// <summary>
        /// Variable.
        /// </summary>
        Variable       = 101

    }

}
