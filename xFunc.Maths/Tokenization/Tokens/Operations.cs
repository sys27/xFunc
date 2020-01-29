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

namespace xFunc.Maths.Tokenization.Tokens
{

    /// <summary>
    /// Specifies operations.
    /// </summary>
    [Flags]
    public enum Operations
    {

        /// <summary>
        /// +
        /// </summary>
        Plus = 0x1,
        /// <summary>
        /// -
        /// </summary>
        Minus = 0x2,
        /// <summary>
        /// *
        /// </summary>
        Multiplication = 0x4,
        /// <summary>
        /// /
        /// </summary>
        Division = 0x8,
        /// <summary>
        /// ^
        /// </summary>
        Exponentiation = 0x10,
        // TODO:
        // /// <summary>
        // /// - (Unary)
        // /// </summary>
        // UnaryMinus = 0x20,
        /// <summary>
        /// !
        /// </summary>
        Factorial = 0x40,
        /// <summary>
        /// %, mod
        /// </summary>
        Modulo = 0x80,

        /// <summary>
        /// &amp;&amp;
        /// </summary>
        ConditionalAnd = 0x100,
        /// <summary>
        /// ||
        /// </summary>
        ConditionalOr = 0x200,
        /// <summary>
        /// ==
        /// </summary>
        Equal = 0x400,
        /// <summary>
        /// !=
        /// </summary>
        NotEqual = 0x800,
        /// <summary>
        /// &lt;
        /// </summary>
        LessThan = 0x1000,
        /// <summary>
        /// &lt;=
        /// </summary>
        LessOrEqual = 0x2000,
        /// <summary>
        /// &gt;
        /// </summary>
        GreaterThan = 0x4000,
        /// <summary>
        /// &gt;=
        /// </summary>
        GreaterOrEqual = 0x8000,
        /// <summary>
        /// The increment (++)
        /// </summary>
        Increment = 0x10000,
        /// <summary>
        /// The decrement (--)
        /// </summary>
        Decrement = 0x20000,
        /// <summary>
        /// +=
        /// </summary>
        AddAssign = 0x40000,
        /// <summary>
        /// -=
        /// </summary>
        SubAssign = 0x80000,
        /// <summary>
        /// *=
        /// </summary>
        MulAssign = 0x100000,
        /// <summary>
        /// /=
        /// </summary>
        DivAssign = 0x200000,

        /// <summary>
        /// :=
        /// </summary>
        Assign = 0x400000,

        /// <summary>
        /// ~, not
        /// </summary>
        Not = 0x800000,
        /// <summary>
        /// &amp;, and
        /// </summary>
        And = 0x1000000,
        /// <summary>
        /// |, or
        /// </summary>
        Or = 0x2000000,
        /// <summary>
        /// xor
        /// </summary>
        XOr = 0x4000000,
        /// <summary>
        /// =>, ->, impl
        /// </summary>
        Implication = 0x8000000,
        /// <summary>
        /// &lt;=>, &lt;->, eq
        /// </summary>
        Equality = 0x10000000,
        /// <summary>
        /// nor
        /// </summary>
        NOr = 0x20000000,
        /// <summary>
        /// nand
        /// </summary>
        NAnd = 0x40000000

    }

}