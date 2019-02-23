// Copyright 2012-2019 Dmitry Kischenko
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

namespace xFunc.UnitConverters
{

    /// <summary>
    /// The base interface for all converters.
    /// </summary>
    public interface IConverter
    {

        /// <summary>
        /// Converts a value from one unit type to another.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="from">The unit type the provided value is in.</param>
        /// <param name="to">The unit type to convert the value to.</param>
        /// <returns>
        /// The converted value.
        /// </returns>
        double Convert(double value, object from, object to);

        /// <summary>
        /// Gets the name of this converter.
        /// </summary>
        /// <value>
        /// The name of this converter.
        /// </value>
        string Name { get; }
        /// <summary>
        /// Gets the units.
        /// </summary>
        /// <value>
        /// The units.
        /// </value>
        IDictionary<object, string> Units { get; }

    }

}
