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

namespace xFunc.Maths.Expressions.Units.Converters
{
    /// <summary>
    /// The interface for unit converter.
    /// </summary>
    /// <typeparam name="TValue">The type of return value of converter.</typeparam>
    public interface IConverter<out TValue>
    {
        /// <summary>
        /// Converts <paramref name="value"/> to specified <paramref name="unit"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="unit">The unit to convert to.</param>
        /// <returns>The converter value.</returns>
        TValue Convert(object value, string unit);

        /// <summary>
        /// Determines whether the current converter can convert to specified unit.
        /// </summary>
        /// <param name="unit">The unit to convert to.</param>
        /// <returns>
        /// <c>true</c> if converter can convert using <paramref name="unit"/>; otherwise, <c>false</c>.
        /// </returns>
        bool CanConvertTo(string unit);
    }
}