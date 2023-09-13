// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Maths.Results;

/// <summary>
/// Represents the result of expression evaluation.
/// </summary>
/// <remarks>
/// It is a hand-made implementation of the discriminated union. The <see cref="Result"/> class provides the abstraction (root class) for DU, whereas implementation for each possible return type is dedicated to the appropriate nested result class.
/// </remarks>
/// <example>
///   <para>Get result by using the <see cref="Result.TryGetNumber"/> method:</para>
///   <code>
///     var processor = new Processor();
///     var result = processor.Solve("2 + 2");
///     if (result.TryGetNumber(out var number))
///         Console.WriteLine(number); // outputs '4'
///   </code>
///
///   <para>Get result by using the <see cref="Result.Number"/> property:</para>
///   <code>
///     var processor = new Processor();
///     var result = processor.Solve("2 + 2");
///
///     var number = result.Number; // equals '4'
///   </code>
///
///   <para>Get result by using the <see cref="object.ToString()"/> implementation:</para>
///   <code>
///     var processor = new Processor();
///     var result = processor.Solve("2 + 2");
///
///     Console.WriteLine(result); // outputs '4'
///   </code>
///
///   <para>Get result by using the pattern matching:</para>
///   <code>
///     var processor = new Processor();
///     var result = processor.Solve("2 + 2");
///     if (result is Result.NumberResult numberResult)
///         Console.WriteLine(numberResult.Number); // outputs '4'
///   </code>
/// </example>
/// <seealso cref="Processor"/>
public abstract partial class Result
{
    private Result()
    {
    }

    /// <summary>
    /// Wraps the result of expression evaluation into <see cref="Result"/>.
    /// </summary>
    /// <param name="result">The result of expression evaluation.</param>
    /// <returns>The result.</returns>
    /// <exception cref="InvalidResultException">The type of <paramref name="result"/> is not supported.</exception>
    public static Result Create(object result)
        => result switch
        {
            NumberValue number
                => new NumberResult(number),

            AngleValue angle
                => new AngleResult(angle),

            PowerValue power
                => new PowerResult(power),

            TemperatureValue temperature
                => new TemperatureResult(temperature),

            MassValue mass
                => new MassResult(mass),

            LengthValue length
                => new LengthResult(length),

            TimeValue time
                => new TimeResult(time),

            AreaValue area
                => new AreaResult(area),

            VolumeValue volume
                => new VolumeResult(volume),

            Complex complex
                => new ComplexNumberResult(complex),

            bool boolean
                => new BooleanResult(boolean),

            string str
                => new StringResult(str),

            Lambda lambda
                => new LambdaResult(lambda),

            VectorValue vectorValue
                => new VectorResult(vectorValue),

            MatrixValue matrixValue
                => new MatrixResult(matrixValue),

            RationalValue rationalValue
                => new RationalResult(rationalValue),

            EmptyValue
                => new EmptyResult(),

            _ => throw new InvalidResultException(),
        };
}