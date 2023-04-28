// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using xFunc.Maths.Expressions.Units.AngleUnits;
using xFunc.Maths.Expressions.Units.AreaUnits;
using xFunc.Maths.Expressions.Units.Converters;
using xFunc.Maths.Expressions.Units.LengthUnits;
using xFunc.Maths.Expressions.Units.MassUnits;
using xFunc.Maths.Expressions.Units.PowerUnits;
using xFunc.Maths.Expressions.Units.TemperatureUnits;
using xFunc.Maths.Expressions.Units.TimeUnits;
using xFunc.Maths.Expressions.Units.VolumeUnits;

namespace xFunc.Benchmark.Benchmarks;

public class ConvertBenchmark
{
    private static IConverter converter = new Converter();

    [Benchmark]
    public object AngleConvert()
        => converter.Convert(AngleValue.Gradian(90), "deg");

    [Benchmark]
    public object PowerConvert()
        => converter.Convert(PowerValue.Kilowatt(1), "hp");

    [Benchmark]
    public object TemperatureConvert()
        => converter.Convert(TemperatureValue.Kelvin(1), "°F");

    [Benchmark]
    public object MassConvert()
        => converter.Convert(MassValue.Kilogram(1), "lb");

    [Benchmark]
    public object AreaConvert()
        => converter.Convert(AreaValue.Kilometer(1), "in^2");

    [Benchmark]
    public object LengthConvert()
        => converter.Convert(LengthValue.Kilometer(1), "in");

    [Benchmark]
    public object TimeConvert()
        => converter.Convert(TimeValue.Hour(1), "min");

    [Benchmark]
    public object VolumeConvert()
        => converter.Convert(VolumeValue.Gallon(1), "in^3");
}