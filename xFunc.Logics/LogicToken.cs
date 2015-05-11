// Copyright 2012-2015 Dmitry Kischenko
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
    /// Represents a token.
    /// </summary>
    /// <seealso cref="ILexer.Tokenize"/>
    public class LogicToken
    {

        private LogicTokenType type;
        private string variable;

        /// <summary>
        /// Initializes a new instance of <see cref="LogicToken"/>.
        /// </summary>
        public LogicToken() { }

        /// <summary>
        /// Initializes a new instance of <see cref="LogicToken"/>.
        /// </summary>
        /// <param name="type">A type of token.</param>
        /// <seealso cref="LogicTokenType"/>
        public LogicToken(LogicTokenType type) : this(type, string.Empty) { }

        /// <summary>
        /// Initializes a new instance of <see cref="LogicToken"/> and sets the name of variable.
        /// </summary>
        /// <param name="variable">A name of variable.</param>
        public LogicToken(string variable) : this(LogicTokenType.Variable, variable) { }

        internal LogicToken(LogicTokenType type, string variable)
        {
            this.type = type;
            this.variable = variable;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            LogicToken token = obj as LogicToken;
            if (token != null && token.Type == type)
            {
                if (token.Type == LogicTokenType.Variable && token.Variable != variable)
                    return false;

                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns the hash code for this token.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return type.GetHashCode() ^ variable.GetHashCode();
        }

        /// <summary>
        /// Converts this instance of token into an equivalent string.
        /// </summary>
        /// <returns>The string that represents this token.</returns>
        public override string ToString()
        {
            if (type == LogicTokenType.Variable)
            {
                return string.Format("Var: {0}", variable);
            }

            return type.ToString();
        }

        /// <summary>
        /// Get or Set the type of token.
        /// </summary>
        public LogicTokenType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        /// <summary>
        /// Get or Set the name of variable.
        /// </summary>
        public string Variable
        {
            get
            {
                return variable;
            }
            set
            {
                variable = value;
            }
        }

    }

}
