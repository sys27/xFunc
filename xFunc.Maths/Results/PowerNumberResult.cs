// Copyright 2012-2021 Dmytro Kyshchenko
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

using xFunc.Maths.Expressions.Units.PowerUnits;

namespace xFunc.Maths.Results
{
    /// <summary>
    /// Represents a power number result.
    /// </summary>
    public class PowerNumberResult : IResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PowerNumberResult"/> class.
        /// </summary>
        /// <param name="value">The numerical representation of result.</param>
        public PowerNumberResult(PowerValue value) => Result = value;

        /// <inheritdoc />
        public override string ToString() => Result.ToString();

        /// <inheritdoc cref="IResult.Result" />
        public PowerValue Result { get; }

        /// <inheritdoc />
        object IResult.Result => Result;
    }
}