// Copyright 2012-2017 Dmitry Kischenko
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

namespace xFunc.Maths.Results
{

    /// <summary>
    /// Represents the string result
    /// </summary>
    public class StringResult : IResult
    {

        private readonly string str;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringResult"/> class.
        /// </summary>
        /// <param name="str">The string representation of result.</param>
        public StringResult(string str)
        {
            this.str = str;
        }

        /// <summary>
        /// Returns a <see cref="String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return str;
        }

        /// <summary>
        /// Gets the string representation of result.
        /// </summary>
        /// <value>
        /// The string representation of result.
        /// </value>
        public string Result
        {
            get
            {
                return str;
            }
        }

        object IResult.Result
        {
            get
            {
                return str;
            }
        }

    }

}
