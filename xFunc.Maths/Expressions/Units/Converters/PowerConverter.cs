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
using System.Collections.Generic;
using xFunc.Maths.Expressions.Units.PowerUnits;

namespace xFunc.Maths.Expressions.Units.Converters
{
    /// <summary>
    /// The power unit converter.
    /// </summary>
    public class PowerConverter : IConverter<PowerValue>, IConverter<object>
    {
        private readonly HashSet<string> units = new HashSet<string>
        {
            "w", "kw", "hp",
        };

        /// <inheritdoc />
        public PowerValue Convert(object value, string unit)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));
            if (string.IsNullOrWhiteSpace(unit))
                throw new ArgumentNullException(nameof(unit));

            return (value, unit) switch
            {
                (PowerValue powerValue, "w") => powerValue.ToWatt(),
                (PowerValue powerValue, "kw") => powerValue.ToKilowatt(),
                (PowerValue powerValue, "hp") => powerValue.ToHorsepower(),

                (NumberValue numberValue, "w") => PowerValue.Watt(numberValue),
                (NumberValue numberValue, "kw") => PowerValue.Kilowatt(numberValue),
                (NumberValue numberValue, "hp") => PowerValue.Horsepower(numberValue),

                _ when CanConvertTo(unit) => throw new ValueIsNotSupportedException(value),
                _ => throw new UnitIsNotSupportedException(unit),
            };
        }

        /// <inheritdoc/>
        object IConverter<object>.Convert(object value, string unit)
            => Convert(value, unit);

        /// <inheritdoc cref="IConverter{TValue}.CanConvertTo" />
        public bool CanConvertTo(string unit)
            => units.Contains(unit.ToLower());
    }
}