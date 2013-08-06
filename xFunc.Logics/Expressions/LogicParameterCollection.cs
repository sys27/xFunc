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
using System.Collections.Generic;

namespace xFunc.Logics.Expressions
{

    /// <summary>
    /// Collection of logic variables.
    /// </summary>
    public class LogicParameterCollection : List<string>
    {

        private int bits;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogicParameterCollection"/> class.
        /// </summary>
        public LogicParameterCollection() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogicParameterCollection"/> class.
        /// </summary>
        /// <param name="vars">The variables.</param>
        public LogicParameterCollection(IEnumerable<string> vars) : base(vars) { }

        /// <summary>
        /// Gets or sets the value of specified variable.
        /// </summary>
        /// <value>
        /// The value of variable.
        /// </value>
        /// <param name="variable">The variable.</param>
        /// <returns>The value of variable.</returns>
        /// <exception cref="KeyNotFoundException">The collection does not have this variable.</exception>
        public bool this[string variable]
        {
            get
            {
                if (!Contains(variable))
                    throw new KeyNotFoundException();

                int index = 1 << Count - IndexOf(variable) - 1;

                return (bits & index) == index;
            }
            set
            {
                if (!Contains(variable))
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

        /// <summary>
        /// Adds the specified variable.
        /// </summary>
        /// <param name="character">The variable.</param>
        public new void Add(string character)
        {
            bits <<= Count;
            base.Add(character);
        }

        /// <summary>
        /// Removes the specified variable.
        /// </summary>
        /// <param name="character">The variable.</param>
        public new void Remove(string character)
        {
            bits >>= Count;
            base.Remove(character);
        }

        /// <summary>
        /// Gets or sets the bits.
        /// </summary>
        /// <value>
        /// The bits.
        /// </value>
        public int Bits
        {
            get { return bits; }
            set { bits = value; }
        }

    }

}
