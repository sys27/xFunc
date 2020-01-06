// Copyright 2012-2020 Dmytro Kyshchenko
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

namespace xFunc.Maths.Results
{

    /// <summary>
    /// Represents the boolean result.
    /// </summary>
    public class BooleanResult : IResult
    {

        private readonly bool value;

        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanResult"/> class.
        /// </summary>
        /// <param name="value">The value of result.</param>
        public BooleanResult(bool value)
        {
            this.value = value;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return value.ToString();
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public bool Result => value;

        object IResult.Result => value;

    }

}
