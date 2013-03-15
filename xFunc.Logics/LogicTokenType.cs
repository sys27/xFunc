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

namespace xFunc.Logics
{

    /// <summary>
    /// Describes a type of token.
    /// </summary>
    /// <seealso cref="LogicToken"/>
    public enum LogicTokenType
    {

        /// <summary>
        /// :=
        /// </summary>
        Assign         = 0,
        /// <summary>
        /// undef()
        /// </summary>
        Undefine       = 1,
        /// <summary>
        /// (
        /// </summary>
        OpenBracket    = 2,
        /// <summary>
        /// )
        /// </summary>
        CloseBracket   = 3,

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
