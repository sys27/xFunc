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

namespace xFunc.Maths
{

    public class MathToken
    {

        private MathTokenType type;
        private double number;
        private char variable;

        public MathToken() { }

        public MathToken(MathTokenType type) : this(type, 0, ' ') { }

        public MathToken(double number) : this(MathTokenType.Number, number, ' ') { }

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
