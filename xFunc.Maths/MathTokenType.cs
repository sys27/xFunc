// Copyright 2012 Dmitry Kischenko
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
namespace xFunc.Maths
{

    public enum MathTokenType
    {

        /// <summary>
        /// :=
        /// </summary>
        Assign         = 0,
        /// <summary>
        /// (
        /// </summary>
        OpenBracket    = 1,
        /// <summary>
        /// )
        /// </summary>
        CloseBracket   = 2,
        /// <summary>
        /// ,
        /// </summary>
        Comma          = 3,
        /// <summary>
        /// +
        /// </summary>
        Addition       = 4,
        /// <summary>
        /// -
        /// </summary>
        Subtraction    = 5,
        /// <summary>
        /// *
        /// </summary>
        Multiplication = 6,
        /// <summary>
        /// /
        /// </summary>
        Division       = 7,
        /// <summary>
        /// ^
        /// </summary>
        Exponentiation = 8,
        /// <summary>
        /// - (Unary)
        /// </summary>
        UnaryMinus     = 9,
        /// <summary>
        /// abs
        /// </summary>
        Absolute       = 10,
        /// <summary>
        /// sin
        /// </summary>
        Sine           = 11,
        /// <summary>
        /// cos
        /// </summary>
        Cosine         = 12,
        /// <summary>
        /// tg
        /// </summary>
        Tangent        = 13,
        /// <summary>
        /// Cot
        /// </summary>
        Cotangent      = 14,
        /// <summary>
        /// sec
        /// </summary>
        Secant         = 15,
        /// <summary>
        /// csc
        /// </summary>
        Cosecant       = 16,
        /// <summary>
        /// Arcsin
        /// </summary>
        Arcsine        = 17,
        /// <summary>
        /// Arccos
        /// </summary>
        Arccosine      = 18,
        /// <summary>
        /// Arctan
        /// </summary>
        Arctangent     = 19,
        /// <summary>
        /// Arccot
        /// </summary>
        Arccotangent   = 20,
        /// <summary>
        /// arcsec
        /// </summary>
        Arcsecant      = 21,
        /// <summary>
        /// arccsc
        /// </summary>
        Arccosecant    = 22,
        /// <summary>
        /// sqrt
        /// </summary>
        Sqrt           = 23,
        /// <summary>
        /// root
        /// </summary>
        Root           = 24,
        /// <summary>
        /// Ln
        /// </summary>
        Ln             = 25,
        /// <summary>
        /// Lg
        /// </summary>
        Lg             = 26,
        /// <summary>
        /// Log
        /// </summary>
        Log            = 27,

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
        E              = 28,
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
