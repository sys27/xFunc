// Copyright 2012-2018 Dmitry Kischenko
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

namespace xFunc.Maths.Tokenization.Tokens
{

    /// <summary>
    /// Specifies functions.
    /// </summary>
    public enum Functions
    {

        /// <summary>
        /// The Add function
        /// </summary>
        Add,
        /// <summary>
        /// The Sub function
        /// </summary>
        Sub,
        /// <summary>
        /// The Mul function
        /// </summary>
        Mul,
        /// <summary>
        /// The Div function
        /// </summary>
        Div,
        /// <summary>
        /// The Pow function
        /// </summary>
        Pow,
        /// <summary>
        /// abs
        /// </summary>
        Absolute,
        /// <summary>
        /// sin
        /// </summary>
        Sine,
        /// <summary>
        /// cos
        /// </summary>
        Cosine,
        /// <summary>
        /// tg
        /// </summary>
        Tangent,
        /// <summary>
        /// Cot
        /// </summary>
        Cotangent,
        /// <summary>
        /// sec
        /// </summary>
        Secant,
        /// <summary>
        /// csc
        /// </summary>
        Cosecant,
        /// <summary>
        /// Arcsin
        /// </summary>
        Arcsine,
        /// <summary>
        /// Arccos
        /// </summary>
        Arccosine,
        /// <summary>
        /// Arctan
        /// </summary>
        Arctangent,
        /// <summary>
        /// Arccot
        /// </summary>
        Arccotangent,
        /// <summary>
        /// arcsec
        /// </summary>
        Arcsecant,
        /// <summary>
        /// arccsc
        /// </summary>
        Arccosecant,
        /// <summary>
        /// sqrt
        /// </summary>
        Sqrt,
        /// <summary>
        /// root
        /// </summary>
        Root,
        /// <summary>
        /// Ln
        /// </summary>
        Ln,
        /// <summary>
        /// Lg
        /// </summary>
        Lg,
        /// <summary>
        /// Lb
        /// </summary>
        Lb,
        /// <summary>
        /// Log
        /// </summary>
        Log,

        /// <summary>
        /// sinh
        /// </summary>
        Sineh,
        /// <summary>
        /// cosh
        /// </summary>
        Cosineh,
        /// <summary>
        /// tanh
        /// </summary>
        Tangenth,
        /// <summary>
        /// coth
        /// </summary>
        Cotangenth,
        /// <summary>
        /// sech
        /// </summary>
        Secanth,
        /// <summary>
        /// csch
        /// </summary>
        Cosecanth,
        /// <summary>
        /// arsinh
        /// </summary>
        Arsineh,
        /// <summary>
        /// arcosh
        /// </summary>
        Arcosineh,
        /// <summary>
        /// artanh
        /// </summary>
        Artangenth,
        /// <summary>
        /// arcoth
        /// </summary>
        Arcotangenth,
        /// <summary>
        /// arsech
        /// </summary>
        Arsecanth,
        /// <summary>
        /// arcsch
        /// </summary>
        Arcosecanth,

        /// <summary>
        /// Exponential
        /// </summary>
        Exp,

        /// <summary>
        /// Greatest common divisor
        /// </summary>
        GCD,
        /// <summary>
        /// Least common multiple
        /// </summary>
        LCM,
        /// <summary>
        /// fact
        /// </summary>
        Factorial,
        /// <summary>
        /// Summation
        /// </summary>
        Sum,
        /// <summary>
        /// Product
        /// </summary>
        Product,
        /// <summary>
        /// round
        /// </summary>
        Round,
        /// <summary>
        /// floor
        /// </summary>
        Floor,
        /// <summary>
        /// ceil
        /// </summary>
        Ceil,

        /// <summary>
        /// if
        /// </summary>
        If,
        /// <summary>
        /// for
        /// </summary>
        For,
        /// <summary>
        /// while
        /// </summary>
        While,

        /// <summary>
        /// The "Del" operator.
        /// </summary>
        Del,
        /// <summary>
        /// deriv
        /// </summary>
        Derivative,
        /// <summary>
        /// simplify
        /// </summary>
        Simplify,
        /// <summary>
        /// def
        /// </summary>
        Define,
        /// <summary>
        /// undef
        /// </summary>
        Undefine,

        /// <summary>
        /// transpose
        /// </summary>
        Transpose,
        /// <summary>
        /// determinant, det
        /// </summary>
        Determinant,
        /// <summary>
        /// inverse
        /// </summary>
        Inverse,
        /// <summary>
        /// vector
        /// </summary>
        Vector,
        /// <summary>
        /// matrix
        /// </summary>
        Matrix,

        /// <summary>
        /// re (returns real part of complex number)
        /// </summary>
        Re,
        /// <summary>
        /// im (returns imaginary part of complex number)
        /// </summary>
        Im,
        /// <summary>
        /// phase
        /// </summary>
        Phase,
        /// <summary>
        /// conjugate
        /// </summary>
        Conjugate,
        /// <summary>
        /// reciprocal
        /// </summary>
        Reciprocal,

        /// <summary>
        /// The user function
        /// </summary>
        UserFunction,

        /// <summary>
        /// The Min function
        /// </summary>
        Min,
        /// <summary>
        /// The Max function
        /// </summary>
        Max,
        /// <summary>
        /// The Avg function
        /// </summary>
        Avg,
        /// <summary>
        /// The Count function
        /// </summary>
        Count,
        /// <summary>
        /// The VAR function
        /// </summary>
        Var,
        /// <summary>
        /// The VARP function
        /// </summary>
        Varp,
        /// <summary>
        /// The STDEV function
        /// </summary>
        Stdev,
        /// <summary>
        /// The STDEVP function
        /// </summary>
        Stdevp

    }

}
