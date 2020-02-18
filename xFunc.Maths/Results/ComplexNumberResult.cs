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

using System;
using System.Globalization;
using System.Numerics;

namespace xFunc.Maths.Results
{
    /// <summary>
    /// Represents the numerical result.
    /// </summary>
    public class ComplexNumberResult : IResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NumberResult"/> class.
        /// </summary>
        /// <param name="complex">The numerical representation of result.</param>
        public ComplexNumberResult(Complex complex)
        {
            this.Result = complex;
        }

        /// <summary>
        /// Returns a <see cref="String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Result.Format();
        }

        /// <summary>
        /// Gets the numerical representation of result.
        /// </summary>
        /// <value>
        /// The numerical representation of result.
        /// </value>
        public Complex Result { get; }

        object IResult.Result => Result;
    }
}