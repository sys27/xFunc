// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions;

/// <summary>
/// Represents variables in expressions.
/// </summary>
public class Variable : IExpression, IEquatable<Variable>
{
    /// <summary>
    /// The 'x' variable.
    /// </summary>
    public static readonly Variable X = new Variable("x");

    /// <summary>
    /// The 'y' variable.
    /// </summary>
    public static readonly Variable Y = new Variable("y");

    /// <summary>
    /// Initializes a new instance of the <see cref="Variable"/> class.
    /// </summary>
    /// <param name="name">A name of variable.</param>
    public Variable(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));

        Name = name;
    }

    /// <summary>
    /// Defines an implicit conversion of a Variable object to a string object.
    /// </summary>
    /// <param name="variable">The value to convert.</param>
    /// <returns>An object that contains the converted value.</returns>
    public static implicit operator string(Variable? variable)
        => variable?.Name ?? throw new ArgumentNullException(nameof(variable));

    /// <summary>
    /// Defines an implicit conversion of a string object to a Variable object.
    /// </summary>
    /// <param name="variable">The value to convert.</param>
    /// <returns>An object that contains the converted value.</returns>
    public static implicit operator Variable(string variable)
        => new Variable(variable);

    /// <summary>
    /// Deconstructs <see cref="Variable"/> to <see cref="string"/>.
    /// </summary>
    /// <param name="variable">The name of variable.</param>
    public void Deconstruct(out string variable) => variable = Name;

    /// <inheritdoc />
    public bool Equals(Variable? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return Name == other.Name;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (typeof(Variable) != obj.GetType())
            return false;

        return Equals((Variable)obj);
    }

    /// <inheritdoc />
    public string ToString(IFormatter formatter)
        => Analyze(formatter);

    /// <inheritdoc />
    public override string ToString()
        => ToString(new CommonFormatter());

    /// <inheritdoc />
    /// <exception cref="NotSupportedException">Always.</exception>
    public object Execute() => throw new NotSupportedException();

    /// <inheritdoc />
    public object Execute(ExpressionParameters? parameters)
    {
        if (parameters is null)
            throw new ArgumentNullException(nameof(parameters));

        return parameters.Variables[Name].Value;
    }

    /// <inheritdoc />
    public TResult Analyze<TResult>(IAnalyzer<TResult> analyzer)
    {
        if (analyzer is null)
            throw new ArgumentNullException(nameof(analyzer));

        return analyzer.Analyze(this);
    }

    /// <inheritdoc />
    public TResult Analyze<TResult, TContext>(
        IAnalyzer<TResult, TContext> analyzer,
        TContext context)
    {
        if (analyzer is null)
            throw new ArgumentNullException(nameof(analyzer));

        return analyzer.Analyze(this, context);
    }

    /// <summary>
    /// Gets a name of this variable.
    /// </summary>
    public string Name { get; }
}