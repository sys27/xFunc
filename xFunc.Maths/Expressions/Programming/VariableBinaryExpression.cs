// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions.Programming;

/// <summary>
/// The base class for binary operations with variable as first (left) operand.
/// </summary>
public abstract class VariableBinaryExpression : IExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VariableBinaryExpression"/> class.
    /// </summary>
    /// <param name="variable">The left (first) operand.</param>
    /// <param name="value">The right (second) operand.</param>
    protected VariableBinaryExpression(Variable variable, IExpression value)
    {
        Variable = variable ?? throw new ArgumentNullException(nameof(variable));
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
            return true;

        if (obj is null || GetType() != obj.GetType())
            return false;

        var exp = (VariableBinaryExpression)obj;

        return Variable.Equals(exp.Variable) && Value.Equals(exp.Value);
    }

    /// <inheritdoc />
    public string ToString(IFormatter formatter) => Analyze(formatter);

    /// <inheritdoc />
    public override string ToString() => ToString(CommonFormatter.Instance);

    /// <inheritdoc />
    public object Execute() => Execute(null);

    /// <inheritdoc />
    public object Execute(ExpressionParameters? parameters)
    {
        if (parameters is null)
            throw new ArgumentNullException(nameof(parameters));

        var variableResult = Variable.Execute(parameters);
        if (variableResult is NumberValue variable)
        {
            var rightResult = Value.Execute(parameters);
            if (rightResult is NumberValue number)
            {
                var parameterValue = new ParameterValue(Execute(variable, number));
                parameters[Variable.Name] = parameterValue;

                return parameterValue.Value;
            }

            throw new ResultIsNotSupportedException(this, rightResult);
        }

        throw new ResultIsNotSupportedException(this, variableResult);
    }

    /// <summary>
    /// Executes this expression.
    /// </summary>
    /// <param name="variableValue">The value of variable.</param>
    /// <param name="value">The value.</param>
    /// <returns>
    /// A result of the execution.
    /// </returns>
    protected abstract object Execute(NumberValue variableValue, NumberValue value);

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
    /// <param name="variable">The variable argument of new expression.</param>
    /// <param name="value">The value argument of new expression.</param>
    /// <returns>
    /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
    /// </returns>
    public abstract IExpression Clone(Variable? variable = null, IExpression? value = null);

    /// <summary>
    /// Gets the variable.
    /// </summary>
    public Variable Variable { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    public IExpression Value { get; }
}