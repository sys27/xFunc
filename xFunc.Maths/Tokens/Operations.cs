// Copyright 2012-2014 Dmitry Kischenko
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

namespace xFunc.Maths.Tokens
{

    /// <summary>
    /// Specifies operations.
    /// </summary>
    public enum Operations
    {

        /// <summary>
        /// +
        /// </summary>
        Addition,
        /// <summary>
        /// -
        /// </summary>
        Subtraction,
        /// <summary>
        /// *
        /// </summary>
        Multiplication,
        /// <summary>
        /// /
        /// </summary>
        Division,
        /// <summary>
        /// ^
        /// </summary>
        Exponentiation,
        /// <summary>
        /// - (Unary)
        /// </summary>
        UnaryMinus,
        /// <summary>
        /// !
        /// </summary>
        Factorial,

        /// <summary>
        /// &amp;&amp;
        /// </summary>
        ConditionalAnd,
        /// <summary>
        /// ||
        /// </summary>
        ConditionalOr,
        /// <summary>
        /// ==
        /// </summary>
        Equal,
        /// <summary>
        /// !=
        /// </summary>
        NotEqual,
        /// <summary>
        /// &lt;
        /// </summary>
        LessThan,
        /// <summary>
        /// &lt;=
        /// </summary>
        LessOrEqual,
        /// <summary>
        /// &gt;
        /// </summary>
        GreaterThan,
        /// <summary>
        /// &gt;=
        /// </summary>
        GreaterOrEqual,
        /// <summary>
        /// The increment (++)
        /// </summary>
        Increment,
        /// <summary>
        /// The decrement (--)
        /// </summary>
        Decrement,
        /// <summary>
        /// +=
        /// </summary>
        AddAssign,
        /// <summary>
        /// -=
        /// </summary>
        SubAssign,
        /// <summary>
        /// *=
        /// </summary>
        MulAssign,
        /// <summary>
        /// /=
        /// </summary>
        DivAssign,

        /// <summary>
        /// :=
        /// </summary>
        Assign,

        /// <summary>
        /// ~, not
        /// </summary>
        BitwiseNot,
        /// <summary>
        /// &amp;, and
        /// </summary>
        BitwiseAnd,
        /// <summary>
        /// |, or
        /// </summary>
        BitwiseOr,
        /// <summary>
        /// xor
        /// </summary>
        BitwiseXOr

    }

}
