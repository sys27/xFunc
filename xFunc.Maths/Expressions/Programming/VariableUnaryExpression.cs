// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions.Programming;

/// <summary>
/// The abstract base class that represents the unary operation with variable as argument.
/// </summary>
public abstract class VariableUnaryExpression : IExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VariableUnaryExpression"/> class.
    /// </summary>
    /// <param name="variable">The expression.</param>
    protected VariableUnaryExpression(Variable variable)
        => Variable = variable ?? throw new ArgumentNullException(nameof(variable));

    /// <summary>
    /// Determines whether the specified <see cref="object" /> is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
    /// <returns><c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
            return true;

        if (obj is null || GetType() != obj.GetType())
            return false;

        return Variable.Equals(((VariableUnaryExpression)obj).Variable);
    }

    /// <inheritdoc />
    public string ToString(IFormatter formatter) => Analyze(formatter);

    /// <inheritdoc />
    public override string ToString() => ToString(new CommonFormatter());

    /// <inheritdoc />
    public object Execute() => Execute(null);

    /// <inheritdoc />
    public object Execute(ExpressionParameters? parameters)
    {
        if (parameters is null)
            throw new ArgumentNullException(nameof(parameters));

        var result = Variable.Execute(parameters);
        if (result is NumberValue number)
        {
            var parameterValue = new ParameterValue(Execute(number));
            parameters.Variables[Variable.Name] = parameterValue;

            return parameterValue.Value;
        }

        throw new ResultIsNotSupportedException(this, result);
    }

    /// <summary>
    /// Executes this expression.
    /// </summary>
    /// <param name="number">The number.</param>
    /// <returns>
    /// A result of the execution.
    /// </returns>
    protected abstract object Execute(NumberValue number);

    /// <inheritdoc />
    public TResult Analyze<TResult>(IAnalyzer<TResult> analyzer)
    {
        if (analyzer is null)
            throw new ArgumentNullException(nameof(analyzer));

        return AnalyzeInternal(analyzer);
    }

    /// <inheritdoc />
    public TResult Analyze<TResult, TContext>(
        IAnalyzer<TResult, TContext> analyzer,
        TContext context)
    {
        if (analyzer is null)
            throw new ArgumentNullException(nameof(analyzer));

        return AnalyzeInternal(analyzer, context);
    }

    /// <summary>
    /// Analyzes the current expression.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="analyzer">The analyzer.</param>
    /// <returns>
    /// The analysis result.
    /// </returns>
    protected abstract TResult AnalyzeInternal<TResult>(IAnalyzer<TResult> analyzer);

    /// <summary>
    /// Analyzes the current expression.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <typeparam name="TContext">The type of additional parameter for analyzer.</typeparam>
    /// <param name="analyzer">The analyzer.</param>
    /// <param name="context">The context.</param>
    /// <returns>The analysis result.</returns>
    protected abstract TResult AnalyzeInternal<TResult, TContext>(
        IAnalyzer<TResult, TContext> analyzer,
        TContext context);

    /// <summary>
    /// Clones this instance of the <see cref="IExpression" />.
    /// </summary>
    /// <param name="variable">The argument of new expression.</param>
    /// <returns>
    /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
    /// </returns>
    public abstract IExpression Clone(Variable? variable = null);

    /// <summary>
    /// Gets the variable.
    /// </summary>
    /// <value>The variable.</value>
    public Variable Variable { get; }
}