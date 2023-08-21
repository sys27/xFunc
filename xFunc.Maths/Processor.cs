// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

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
        parser = new Parser();

        this.simplifier = simplifier ?? throw new ArgumentNullException(nameof(simplifier));
        this.differentiator = differentiator ?? throw new ArgumentNullException(nameof(differentiator));
        this.converter = converter ?? throw new ArgumentNullException(nameof(converter));
        this.typeAnalyzer = typeAnalyzer ?? throw new ArgumentNullException(nameof(typeAnalyzer));

        Parameters = parameters;
    }

    /// <summary>
    /// Solves the specified expression.
    /// </summary>
    /// <param name="function">The function.</param>
    /// <returns>The result of solving.</returns>
    public IResult Solve(string function)
    {
        var exp = Parse(function);
        exp.Analyze(typeAnalyzer);

        var result = exp.Execute(Parameters);
        return result switch
        {
            NumberValue number
                => new NumberResult(number.Number),

            AngleValue angle
                => new AngleNumberResult(angle),

            PowerValue power
                => new PowerNumberResult(power),

            TemperatureValue temperature
                => new TemperatureNumberResult(temperature),

            MassValue mass
                => new MassNumberResult(mass),

            LengthValue length
                => new LengthNumberResult(length),

            TimeValue time
                => new TimeNumberResult(time),

            AreaValue area
                => new AreaNumberResult(area),

            VolumeValue volume
                => new VolumeNumberResult(volume),

            Complex complex
                => new ComplexNumberResult(complex),

            bool boolean
                => new BooleanResult(boolean),

            string str
                => new StringResult(str),

            Lambda lambda
                => new LambdaResult(lambda),

            VectorValue vectorValue
                => new VectorValueResult(vectorValue),

            MatrixValue matrixValue
                => new MatrixValueResult(matrixValue),

            _ => throw new InvalidResultException(),
        };
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