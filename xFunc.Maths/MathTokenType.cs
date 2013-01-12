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
        Assign = 0,
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
        Arcsine         = 17,
        /// <summary>
        /// Arccos
        /// </summary>
        Arccosine         = 18,
        /// <summary>
        /// Arctan
        /// </summary>
        Arctangent         = 19,
        /// <summary>
        /// Arccot
        /// </summary>
        Arccotangent         = 20,
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
        /// Exponential
        /// </summary>
        E              = 28,
        /// <summary>
        /// plot
        /// </summary>
        Plot           = 29,
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
