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
        /// :=
        /// </summary>
        Assign         = -1,
        /// <summary>
        /// Variable.
        /// </summary>
        Variable       = 101

    }

}
