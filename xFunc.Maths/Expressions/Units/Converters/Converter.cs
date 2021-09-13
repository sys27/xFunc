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

using System;

namespace xFunc.Maths.Expressions.Units.Converters
{
    /// <summary>
    /// The unit converter.
    /// </summary>
    public class Converter : IConverter
    {
        private readonly IConverter<object>[] converters;

        /// <summary>
        /// Initializes a new instance of the <see cref="Converter"/> class.
        /// </summary>
        public Converter()
            => converters = new IConverter<object>[]
            {
                new AngleConverter(),
                new PowerConverter(),
            };

        /// <inheritdoc />
        public object Convert(object value, string unit)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));
            if (string.IsNullOrWhiteSpace(unit))
                throw new ArgumentNullException(nameof(unit));

            unit = unit.ToLower();

            foreach (var converter in converters)
                if (converter.CanConvertTo(unit))
                    return converter.Convert(value, unit);

            throw new UnitIsNotSupportedException(unit);
        }
    }
}