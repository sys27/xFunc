namespace xFunc.Library
{

    public enum TokenType
    {

        /// <summary>
        /// (
        /// </summary>
        OpenBracket    = -1,
        /// <summary>
        /// )
        /// </summary>
        CloseBracket   = 0,
        /// <summary>
        /// ,
        /// </summary>
        Comma          = 1,
        /// <summary>
        /// +
        /// </summary>
        Addition       = 2,
        /// <summary>
        /// -
        /// </summary>
        Subtraction    = 3,
        /// <summary>
        /// *
        /// </summary>
        Multiplication = 4,
        /// <summary>
        /// /
        /// </summary>
        Division       = 5,
        /// <summary>
        /// ^
        /// </summary>
        Exponentiation = 6,
        /// <summary>
        /// - (Unary)
        /// </summary>
        UnaryMinus     = 8,
        /// <summary>
        /// abs
        /// </summary>
        Absolute       = 9,
        /// <summary>
        /// sin
        /// </summary>
        Sine           = 10,
        /// <summary>
        /// cos
        /// </summary>
        Cosine         = 11,
        /// <summary>
        /// tg
        /// </summary>
        Tangent        = 12,
        /// <summary>
        /// Cot
        /// </summary>
        Cotangent      = 13,
        /// <summary>
        /// Arcsin
        /// </summary>
        Arcsin         = 14,
        /// <summary>
        /// Arccos
        /// </summary>
        Arccos         = 15,
        /// <summary>
        /// Arctan
        /// </summary>
        Arctan         = 16,
        /// <summary>
        /// Arccot
        /// </summary>
        Arccot         = 17,
        /// <summary>
        /// sqrt
        /// </summary>
        Sqrt           = 19,
        /// <summary>
        /// root
        /// </summary>
        Root           = 20,
        /// <summary>
        /// Ln
        /// </summary>
        Ln             = 21,
        /// <summary>
        /// Lg
        /// </summary>
        Lg             = 22,
        /// <summary>
        /// Log
        /// </summary>
        Log            = 23,
        /// <summary>
        /// Exponential
        /// </summary>
        E              = 24,
        /// <summary>
        /// plot
        /// </summary>
        Plot           = 29,
        /// <summary>
        /// deriv
        /// </summary>
        Derivative     = 30,

        /// <summary>
        /// !
        /// </summary>
        Not            = 70,
        /// <summary>
        /// &
        /// </summary>
        And            = 62,
        /// <summary>
        /// |
        /// </summary>
        Or             = 63,
        /// <summary>
        /// ->
        /// </summary>
        Implication    = 64,
        /// <summary>
        /// &lt;-&gt;
        /// </summary>
        Equality       = 65,
        /// <summary>
        /// ↓
        /// </summary>
        NOr            = 66,
        /// <summary>
        /// ↑
        /// </summary>
        NAnd           = 67,
        /// <summary>
        /// ⊕
        /// </summary>
        XOr            = 68,
        /// <summary>
        /// table
        /// </summary>
        TruthTable     = 74,
        /// <summary>
        /// True.
        /// </summary>
        True           = 75,
        /// <summary>
        /// False.
        /// </summary>
        False          = 76,

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
