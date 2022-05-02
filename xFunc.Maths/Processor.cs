// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;
using static xFunc.Maths.ThrowHelpers;

namespace xFunc.Maths;

/// <summary>
/// The main point of this library. Brings together all features.
/// </summary>
public class Processor
{
    private readonly ITypeAnalyzer typeAnalyzer;
    private readonly IDifferentiator differentiator;
    private readonly ISimplifier simplifier;
    private readonly IConverter converter;
    private readonly IParser parser;

    /// <summary>
    /// Initializes a new instance of the <see cref="Processor"/> class.
    /// </summary>
    public Processor()
    {
        simplifier = new Simplifier();
        differentiator = new Differentiator();
        converter = new Converter();
        parser = new Parser(differentiator, simplifier, converter);
        typeAnalyzer = new TypeAnalyzer();

        Parameters = new ExpressionParameters();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Processor"/> class.
    /// </summary>
    /// <param name="simplifier">The simplifier.</param>
    /// <param name="differentiator">The differentiator.</param>
    public Processor(
        ISimplifier simplifier,
        IDifferentiator differentiator)
        : this(
            simplifier,
            differentiator,
            new Converter(),
            new TypeAnalyzer(),
            new ExpressionParameters())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Processor" /> class.
    /// </summary>
    /// <param name="simplifier">The simplifier.</param>
    /// <param name="differentiator">The differentiator.</param>
    /// <param name="converter">The converter.</param>
    /// <param name="typeAnalyzer">The type analyzer.</param>
    /// <param name="parameters">The collection of parameters.</param>
    public Processor(
        ISimplifier simplifier,
        IDifferentiator differentiator,
        IConverter converter,
        ITypeAnalyzer typeAnalyzer,
        ExpressionParameters parameters)
    {
        if (simplifier is null)
            ArgNull(ExceptionArgument.simplifier);
        if (differentiator is null)
            ArgNull(ExceptionArgument.differentiator);
        if (converter is null)
            ArgNull(ExceptionArgument.converter);
        if (typeAnalyzer is null)
            ArgNull(ExceptionArgument.typeAnalyzer);

        parser = new Parser();

        this.simplifier = simplifier;
        this.differentiator = differentiator;
        this.converter = converter;
        this.typeAnalyzer = typeAnalyzer;

        Parameters = parameters;
    }

    /// <summary>
    /// Solves the specified expression.
    /// </summary>
    /// <param name="function">The function.</param>
    /// <returns>The result of solving.</returns>
    public IResult Solve(string function) => Solve(function, true);

    /// <summary>
    /// Solves the specified expression.
    /// </summary>
    /// <param name="function">The function.</param>
    /// <param name="simplify">if set to <c>true</c> parser will simplify expression.</param>
    /// <returns>The result of solving.</returns>
    public IResult Solve(string function, bool simplify)
    {
        var exp = Parse(function);
        exp.Analyze(typeAnalyzer);

        var result = exp.Execute(Parameters);
        if (result is NumberValue number)
        {
            return new NumberResult(number.Number);
        }

        if (result is AngleValue angle)
        {
            return new AngleNumberResult(angle);
        }

        if (result is PowerValue power)
        {
            return new PowerNumberResult(power);
        }

        if (result is TemperatureValue temperature)
        {
            return new TemperatureNumberResult(temperature);
        }

        if (result is MassValue mass)
        {
            return new MassNumberResult(mass);
        }

        if (result is LengthValue length)
        {
            return new LengthNumberResult(length);
        }

        if (result is TimeValue time)
        {
            return new TimeNumberResult(time);
        }

        if (result is AreaValue area)
        {
            return new AreaNumberResult(area);
        }

        if (result is VolumeValue volume)
        {
            return new VolumeNumberResult(volume);
        }

        if (result is Complex complex)
        {
            return new ComplexNumberResult(complex);
        }

        if (result is bool boolean)
        {
            return new BooleanResult(boolean);
        }

        if (result is string str)
        {
            return new StringResult(str);
        }

        if (result is IExpression expression)
        {
            if (simplify)
                return new ExpressionResult(Simplify(expression));

            return new ExpressionResult(expression);
        }

        throw new InvalidResultException();
    }

    /// <summary>
    /// Solves the specified function.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="function">The function.</param>
    /// <returns>The result of solving.</returns>
    public TResult Solve<TResult>(string function) where TResult : IResult
        => (TResult)Solve(function);

    /// <summary>
    /// Solves the specified function.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="function">The function.</param>
    /// <param name="simplify">if set to <c>true</c> parser will simplify expression.</param>
    /// <returns>The result of solving.</returns>
    public TResult Solve<TResult>(string function, bool simplify) where TResult : IResult
        => (TResult)Solve(function, simplify);

    /// <summary>
    /// Simplifies the <paramref name="function"/>.
    /// </summary>
    /// <param name="function">The function.</param>
    /// <returns>A simplified expression.</returns>
    public IExpression Simplify(string function)
    {
        var expression = Parse(function);

        return expression.Analyze(simplifier);
    }

    /// <summary>
    /// Simplifies the <paramref name="expression"/>.
    /// </summary>
    /// <param name="expression">A expression to simplify.</param>
    /// <returns>A simplified expression.</returns>
    public IExpression Simplify(IExpression expression)
    {
        if (expression is null)
            throw new ArgumentNullException(nameof(expression));

        return expression.Analyze(simplifier);
    }

    /// <summary>
    /// Differentiates the specified expression.
    /// </summary>
    /// <param name="function">The function.</param>
    /// <returns>Returns the derivative.</returns>
    public IExpression Differentiate(string function)
    {
        var expression = Parse(function);

        return Differentiate(expression, Variable.X);
    }

    /// <summary>
    /// Differentiates the specified expression.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns>Returns the derivative.</returns>
    public IExpression Differentiate(IExpression expression)
        => Differentiate(expression, Variable.X);

    /// <summary>
    /// Differentiates the specified expression.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <param name="variable">The variable.</param>
    /// <returns>Returns the derivative.</returns>
    public IExpression Differentiate(IExpression expression, Variable variable)
        => Differentiate(expression, variable, new ExpressionParameters());

    /// <summary>
    /// Differentiates the specified expression.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <param name="variable">The variable.</param>
    /// <param name="parameters">The parameters.</param>
    /// <returns>
    /// Returns the derivative.
    /// </returns>
    public IExpression Differentiate(
        IExpression expression,
        Variable variable,
        ExpressionParameters parameters)
    {
        if (expression is null)
            throw new ArgumentNullException(nameof(expression));

        var context = new DifferentiatorContext(parameters, variable);

        return expression.Analyze(differentiator, context);
    }

    /// <summary>
    /// Parses the specified function.
    /// </summary>
    /// <param name="function">The function.</param>
    /// <returns>The parsed expression.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="function"/> is null.</exception>
    /// <exception cref="ParseException">Error while parsing.</exception>
    public IExpression Parse(string function)
        => parser.Parse(function);

    /// <summary>
    /// Gets expression parameters object.
    /// </summary>
    /// <value>
    /// The expression parameters object.
    /// </value>
    public ExpressionParameters Parameters { get; }
}