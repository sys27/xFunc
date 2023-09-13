// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions;

/// <summary>
/// Represents the custom function without name.
/// </summary>
public readonly struct Lambda : IEquatable<Lambda>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Lambda"/> struct.
    /// </summary>
    /// <param name="body">The body of the function.</param>
    public Lambda(IExpression body)
        : this(ImmutableArray.Create<string>(), body, null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Lambda"/> struct.
    /// </summary>
    /// <param name="parameters">The list of parameters of the function.</param>
    /// <param name="body">The body of the function.</param>
    public Lambda(IEnumerable<string> parameters, IExpression body)
        : this(parameters.ToImmutableArray(), body, null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Lambda"/> struct.
    /// </summary>
    /// <param name="parameters">The list of parameters of the function.</param>
    /// <param name="body">The body of the function.</param>
    public Lambda(ImmutableArray<string> parameters, IExpression body)
        : this(parameters, body, null)
    {
    }

    private Lambda(ImmutableArray<string> parameters, IExpression body, ExpressionParameters? capturedScope)
    {
        Parameters = parameters;
        Body = body ?? throw new ArgumentNullException(nameof(body));
        CapturedScope = capturedScope;
    }

    /// <summary>
    /// Determines whether two specified instances of <see cref="Lambda"/> are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator ==(Lambda left, Lambda right)
        => left.Equals(right);

    /// <summary>
    /// Determines whether two specified instances of <see cref="Lambda"/> are not equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is not equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator !=(Lambda left, Lambda right)
        => !left.Equals(right);

    /// <inheritdoc />
    public bool Equals(Lambda other)
        => Parameters.SequenceEqual(other.Parameters) &&
           Body.Equals(other.Body);

    /// <inheritdoc />
    public override bool Equals(object? obj)
        => obj is Lambda other && Equals(other);

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
        => HashCode.Combine(Parameters, Body);

    /// <inheritdoc />
    public override string ToString()
        => $"({string.Join(", ", Parameters)}) => {Body}";

    /// <summary>
    /// Calls the function.
    /// </summary>
    /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
    /// <returns>A result of the execution.</returns>
    public object Call(ExpressionParameters? parameters)
        => Body.Execute(parameters);

    /// <summary>
    /// Calls the function.
    /// </summary>
    /// <param name="callParameters">The list of explicit parameters to call lambda with.</param>
    /// <param name="expressionParameters">An object that contains all parameters and functions for expressions.</param>
    /// <returns>A result of the execution.</returns>
    /// <seealso cref="CallExpression"/>
    public object Call(ImmutableArray<IExpression> callParameters, ExpressionParameters? expressionParameters)
    {
        var nestedScope = ExpressionParameters.CreateScoped(CapturedScope ?? expressionParameters);
        var zip = Parameters.Zip(callParameters, (parameter, expression) => (parameter, expression));
        foreach (var (parameter, expression) in zip)
            nestedScope[parameter] = new ParameterValue(expression.Execute(expressionParameters));

        var result = Call(nestedScope);
        if (result is Lambda lambdaResult)
            return lambdaResult.Capture(nestedScope);

        return result;
    }

    /// <summary>
    /// Returns a new lambda instance with captured parameters.
    /// </summary>
    /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
    /// <returns>The lambda with captured parameters.</returns>
    public Lambda Capture(ExpressionParameters? parameters)
        => new Lambda(Parameters, Body, parameters);

    /// <summary>
    /// Converts the current lambda into a list of nested lambdas.
    /// </summary>
    /// <returns>The lambda.</returns>
    /// <seealso cref="Expressions.Curry"/>
    public Lambda Curry()
    {
        if (Parameters.Length <= 1)
            return this;

        var body = Body;

        for (var i = Parameters.Length - 1; i > 0; i--)
            body = new LambdaExpression(new Lambda(ImmutableArray.Create(Parameters[i]), body));

        var lambda = new Lambda(ImmutableArray.Create(Parameters[0]), body);

        return lambda;
    }

    /// <summary>
    /// Converts <see cref="Lambda"/> to <see cref="LambdaExpression"/>.
    /// </summary>
    /// <returns>The function expression.</returns>
    public LambdaExpression AsExpression()
        => new LambdaExpression(this);

    /// <summary>
    /// Gets a list of parameters.
    /// </summary>
    public ImmutableArray<string> Parameters { get; }

    /// <summary>
    /// Gets an expression of the function body.
    /// </summary>
    public IExpression Body { get; }

    /// <summary>
    /// Gets the captured scope.
    /// </summary>
    internal ExpressionParameters? CapturedScope { get; }
}