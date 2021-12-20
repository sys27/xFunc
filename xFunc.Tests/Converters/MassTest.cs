// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using MassConverter = xFunc.UnitConverters.MassConverter;
using MassUnits = xFunc.UnitConverters.MassUnits;

namespace xFunc.Tests.Converters;

public class MassTest
{
    private readonly MassConverter conv = new MassConverter();

    [Fact]
    public void ConvertToSame()
    {
        var value = conv.Convert(12, MassUnits.Kilograms, MassUnits.Kilograms);

        Assert.Equal(12, value);
    }

    [Fact]
    public void FromKiloToMilli()
    {
        var value = conv.Convert(12, MassUnits.Kilograms, MassUnits.Milligrams);

        Assert.Equal(12000000, value);
    }

    [Fact]
    public void FromMilliToKilo()
    {
        var value = conv.Convert(12000000, MassUnits.Milligrams, MassUnits.Kilograms);

        Assert.Equal(12, value);
    }

    [Fact]
    public void FromKiloToG()
    {
        var value = conv.Convert(12, MassUnits.Kilograms, MassUnits.Grams);

        Assert.Equal(12000, value);
    }

    [Fact]
    public void FromGToKilo()
    {
        var value = conv.Convert(12000, MassUnits.Grams, MassUnits.Kilograms);

        Assert.Equal(12, value);
    }

    [Fact]
    public void FromKiloToSlug()
    {
        var value = conv.Convert(12, MassUnits.Kilograms, MassUnits.Slugs);

        Assert.Equal(0.822261186743533, value, 4);
    }

    [Fact]
    public void FromSlugToKilo()
    {
        var value = conv.Convert(0.822261186743533, MassUnits.Slugs, MassUnits.Kilograms);

        Assert.Equal(12, value, 4);
    }

    [Fact]
    public void FromKiloToLb()
    {
        var value = conv.Convert(12, MassUnits.Kilograms, MassUnits.Pounds);

        Assert.Equal(26.4554714621853, value, 4);
    }

    [Fact]
    public void FromLbToKilo()
    {
        var value = conv.Convert(26.4554714621853, MassUnits.Pounds, MassUnits.Kilograms);

        Assert.Equal(12, value, 4);
    }

    [Fact]
    public void FromKiloToTonne()
    {
        var value = conv.Convert(12000, MassUnits.Kilograms, MassUnits.Tonne);

        Assert.Equal(12, value, 4);
    }

    [Fact]
    public void FromTonneToKilo()
    {
        var value = conv.Convert(12, MassUnits.Tonne, MassUnits.Kilograms);

        Assert.Equal(12000, value, 4);
    }
}