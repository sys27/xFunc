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
using System.Collections.Generic;

namespace xFunc.Logics.Expressions
{

    public class LogicParameterCollection : List<char>
    {

        private int bits;

        public LogicParameterCollection() { }

        public LogicParameterCollection(IEnumerable<char> vars) : base(vars) { }

        public bool this[char variable]
        {
            get
            {
                if (!this.Contains(variable))
                    throw new KeyNotFoundException();

                int index = 1 << Count - IndexOf(variable) - 1;

                return (bits & index) == index;
            }
            set
            {
                if (!this.Contains(variable))
                    base.Add(variable);

                int index = 1 << Count - IndexOf(variable) - 1;

                if (((bits & index) == index) != value)
                {
                    if (value)
                    {
                        bits |= index;
                    }
                    else
                    {
                        if ((bits & index) == index)
                            bits ^= index;
                    }
                }
            }
        }

        public new void Add(char character)
        {
            bits <<= Count;
            base.Add(character);
        }

        public new void Remove(char character)
        {
            bits >>= Count;
            base.Remove(character);
        }

        public int Bits
        {
            get { return bits; }
            set { bits = value; }
        }

    }

}
