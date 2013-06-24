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

    /// <summary>
    /// Represents a symbol token.
    /// </summary>
    public class SymbolToken : IToken
    {

        private Symbols symbol;
        private int priority;

        /// <summary>
        /// Initializes the <see cref="SymbolToken"/> class.
        /// </summary>
        /// <param name="symbol">A symbol.</param>
        public SymbolToken(Symbols symbol)
        {
            this.symbol = symbol;

            SetPriority();
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            SymbolToken token = obj as SymbolToken;
            if (token != null && this.Symbol == token.Symbol)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
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

        /// <summary>
        /// Gets a priority of current token.
        /// </summary>
        public int Priority
        {
            get
            {
                return priority;
            }
        }

        /// <summary>
        /// Gets the symbol.
        /// </summary>
        public Symbols Symbol
        {
            get
            {
                return symbol;
            }
        }

    }

}
