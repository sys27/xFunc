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

namespace xFunc.Maths.Tokens
{

    public class FunctionToken : IToken
    {

        private Functions function;

        public FunctionToken(Functions function)
        {
            this.function = function;
        }

        public override bool Equals(object obj)
        {
            FunctionToken token = obj as FunctionToken;
            if (token != null && this.Function == token.Function)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return "Function: " + function;
        }

        public int Priority
        {
            get
            {
                return 100;
            }
        }

        public Functions Function
        {
            get
            {
                return function;
            }
        }

    }

}
