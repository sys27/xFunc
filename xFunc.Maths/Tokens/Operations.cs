using System;

namespace xFunc.Maths.Tokens
{

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
        /// :=
        /// </summary>
        Assign,

        /// <summary>
        /// ~, not
        /// </summary>
        Not,
        /// <summary>
        /// &, and
        /// </summary>
        And,
        /// <summary>
        /// |, or
        /// </summary>
        Or,
        /// <summary>
        /// xor
        /// </summary>
        XOr

    }

}
