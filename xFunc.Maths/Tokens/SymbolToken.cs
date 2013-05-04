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

    public class SymbolToken : IToken
    {

        private Symbols symbol;
        private int priority;

        public SymbolToken(Symbols symbol)
        {
            this.symbol = symbol;

            SetPriority();
        }

        public override bool Equals(object obj)
        {
            SymbolToken token = obj as SymbolToken;
            if (token != null && this.Symbol == token.Symbol)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return "Symbol: " + symbol;
        }

        private void SetPriority()
        {
            switch (symbol)
            {
                case Symbols.OpenBracket:
                    priority = 1;
                    break;
                case Symbols.CloseBracket:
                    priority = 2;
                    break;
                case Symbols.Comma:
                    priority = 3;
                    break;
            }
        }

        public int Priority
        {
            get
            {
                return priority;
            }
        }

        public Symbols Symbol
        {
            get
            {
                return symbol;
            }
        }

    }

}
