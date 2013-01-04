using System;

namespace xFunc.Library.Logics
{

    public enum LogicTokenType
    {

        /// <summary>
        /// (
        /// </summary>
        OpenBracket    = 0,
        /// <summary>
        /// )
        /// </summary>
        CloseBracket   = 1,

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
        /// Variable.
        /// </summary>
        Variable       = 101

    }

}
