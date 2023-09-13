// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Diagnostics;

namespace xFunc.Maths.Expressions;

/// <summary>
/// Represents the "curry" function which allows you to convert any lambda to the list of nested lambdas which you can partially apply.
/// </summary>
/// <remarks>
/// <para>This function takes an indefinite amount of arguments where:</para>
/// <list type="bullet">
///   <item>
///     <term>the first one is required and should always return a lambda.</term>
///   </item>
///   <item>
///     <term>all other arguments are optional and needed only when you want to apply some parameters immediately.</term>
///   </item>
/// </list>
/// <para>If the provided lambda takes 0 or 1 argument this function does nothing, it returns the same lambda without any modification.</para>
/// <para>If you provided enough parameters to just call a lambda, then this function works in the same way as <see cref="CallExpression"/>. It doesn't convert the provided lambda into the list of nested lambdas but instead just calls it directly.</para>
/// </remarks>
/// <example>
/// <para>Convert the lambda to the list of nested lambdas:</para>
/// <code>
///   f := (a, b, c) => a + b + c
///   p := curry(f)               // `p` will contain (a) => (b) => (c) => a + b + c
/// </code>
/// <para>Partially apply the lambda:</para>
/// <code>
///   f := (a, b, c) => a + b + c
///   p := curry(f)               // `p` will contain (a) => (b) => (c) => a + b + c
///   add1 := p(1)                // partial application, you provided a value for the first parameter only
///                               // so `p(1)` return another lambda which accepts the second parameter.
/// </code>
/// <para>Partially apply the lambda by using the `curry` function:</para>
/// <code>
///   f := (a, b, c) => a + b + c
///   add1 := curry(f, 1)         // partial application, the result is equal to the previous example
///                               // but it automatically convert the lambda to the list of nested lambdas
///                               // and tries to apply all provided parameters
/// </code>
/// </example>
public class Curry : IExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Curry"/> class.
    /// </summary>
    /// <param name="function">The expression that returns a lambda.</param>
    /// <exception cref="ArgumentNullException"><paramref name="function"/> is null.</exception>
    public Curry(IExpression function)
        : this(function, ImmutableArray<IExpression>.Empty)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Curry"/> class.
    /// </summary>
    /// <param name="function">The expression that returns a lambda.</param>
    /// <param name="parameters">The list of parameters.</param>
    /// <exception cref="ArgumentNullException"><paramref name="function"/> is null.</exception>
    public Curry(IExpression function, ImmutableArray<IExpression> parameters)
    {
        Function = function ?? throw new ArgumentNullException(nameof(function));
        Parameters = parameters;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Curry"/> class.
    /// </summary>
    /// <param name="arguments">The arguments.</param>
    /// <exception cref="ArgumentNullException"><paramref name="arguments"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException">The amount of argument in the <paramref name="arguments"/> collection is less than 1.</exception>
    internal Curry(ImmutableArray<IExpression> arguments)
    {
        Debug.Assert(arguments != null, "arguments == null");

        if (arguments.Length < 1)
            throw new ArgumentException(Resource.LessParams, nameof(arguments));

        Function = arguments[0];
        Parameters = arguments[1..];
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
            return true;

        if (obj is null || GetType() != obj.GetType())
            return false;

        var other = (Curry)obj;

        if (!Function.Equals(other.Function) ||
            Parameters.Length != other.Parameters.Length)
            return false;

        return Parameters.SequenceEqual(other.Parameters);
    }

    /// <inheritdoc />
    public string ToString(IFormatter formatter)
        => formatter.Analyze(this);

    /// <inheritdoc />
    public override string ToString()
        => ToString(CommonFormatter.Instance);

    /// <inheritdoc />
    public object Execute()
        => Execute(null);

    /// <inheritdoc />
    public object Execute(ExpressionParameters? parameters)
    {
        if (Function.Execute(parameters) is not Lambda lambda)
            throw ExecutionException.For(this);

        var parametersLength = Parameters.Length;

        // user provided too many parameters
        if (lambda.Parameters.Length < parametersLength)
            throw new ArgumentException(Resource.MoreParams);

        // user provided exact amount of parameters
        if (lambda.Parameters.Length == parametersLength)
            return lambda.Call(Parameters, parameters);

        lambda = lambda.Curry();

        if (parametersLength == 0)
            return lambda;

        var result = Parameters.Aggregate(
            lambda,
            (current, parameter) => (Lambda)current.Call(ImmutableArray.Create(parameter), parameters));

        return result;
    }

    /// <inheritdoc />
    public TResult Analyze<TResult>(IAnalyzer<TResult> analyzer)
    {
        if (analyzer is null)
            throw new ArgumentNullException(nameof(analyzer));

        return analyzer.Analyze(this);
    }

    /// <inheritdoc />
    public TResult Analyze<TResult, TContext>(IAnalyzer<TResult, TContext> analyzer, TContext context)
    {
        if (analyzer is null)
            throw new ArgumentNullException(nameof(analyzer));

        return analyzer.Analyze(this, context);
    }

    /// <summary>
    /// Clones this instance of the <see cref="IExpression" />.
    /// </summary>
    /// <param name="function">The expression that returns function.</param>
    /// <param name="parameters">The list of parameters.</param>
    /// <returns>
    /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
    /// </returns>
    public IExpression Clone(IExpression? function = null, ImmutableArray<IExpression>? parameters = null)
        => new Curry(function ?? Function, parameters ?? Parameters);

    /// <summary>
    /// Gets the expression that returns a lambda to curry.
    /// </summary>
    public IExpression Function { get; }

    /// <summary>
    /// Gets the list of parameters.
    /// </summary>
    public ImmutableArray<IExpression> Parameters { get; }
}