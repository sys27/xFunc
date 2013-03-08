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

namespace xFunc.Maths
{

    /// <summary>
    /// Represents a token.
    /// </summary>
    /// <seealso cref="IMathLexer.Tokenize"/>
    public class MathToken
    {

        private MathTokenType type;
        private double number;
        private char variable;

        /// <summary>
        /// Initializes a new instance of <see cref="MathToken"/>.
        /// </summary>
        public MathToken() { }

        /// <summary>
        /// Initializes a new instance of <see cref="MathToken"/>.
        /// </summary>
        /// <param name="type">A type of token.</param>
        /// <seealso cref="MathTokenType"/>
        public MathToken(MathTokenType type) : this(type, 0, ' ') { }

        /// <summary>
        /// Initializes a new instance of <see cref="MathToken"/> and sets the value of number.
        /// </summary>
        /// <param name="number">A real number.</param>
        public MathToken(double number) : this(MathTokenType.Number, number, ' ') { }

        /// <summary>
        /// Initializes a new instance of <see cref="MathToken"/> and sets the name of variable.
        /// </summary>
        /// <param name="variable">A name of variable.</param>
        public MathToken(char variable) : this(MathTokenType.Variable, 0, variable) { }

        internal MathToken(MathTokenType type, double number, char variable)
        {
            this.type = type;
            this.number = number;
            this.variable = variable;
        }

        public override bool Equals(object obj)
        {
            MathToken token = obj as MathToken;
            if (token != null && token.Type == type)
            {
                if (token.Type == MathTokenType.Variable && token.Variable != variable)
                    return false;
                if (token.Type == MathTokenType.Number && token.Number != number)
                    return false;

                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return type.GetHashCode() ^ number.GetHashCode() ^ variable.GetHashCode();
        }

        public override string ToString()
        {
            if (type == MathTokenType.Number)
            {
                return string.Format("Number: {0}", number);
            }
            if (type == MathTokenType.Variable)
            {
                return string.Format("Var: {0}", variable);
            }

            return type.ToString();
        }

        public MathTokenType Type
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

        public double Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
            }
        }

        public char Variable
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
