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

    public class LogicToken
    {

        private LogicTokenType type;
        private double number;
        private char variable;

        public LogicToken() { }

        public LogicToken(LogicTokenType type) : this(type, 0, ' ') { }

        public LogicToken(LogicTokenType type, double number) : this(type, number, ' ') { }

        public LogicToken(LogicTokenType type, char variable) : this(type, 0, variable) { }

        public LogicToken(LogicTokenType type, double number, char variable)
        {
            this.type = type;
            this.number = number;
            this.variable = variable;
        }

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

        public override string ToString()
        {
            if (type == LogicTokenType.Variable)
            {
                return string.Format("Var: {0}", variable);
            }

            return type.ToString();
        }

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
