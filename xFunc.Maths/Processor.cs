// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths;

/// <summary>
/// The main point of this library. Brings together all features.
/// </summary>
/// <example>
///   <para>To parse the function to expression tree:</para>
///   <code>
///     var processor = new Processor();
///     var exp = processor.Parse("2 + x");
///   </code>
///
///   <para>To evaluate the function:</para>
///   <code>
///     var processor = new Processor();
///     var result = processor.Solve("2 + 2");
///   </code>
///
///   <para>To simplify the expression:</para>
///   <code>
///     var processor = new Processor();
///     var result = processor.Solve("simplify((x) => arcsin(sin(x)))");
///   </code>
///
///   <para>To differentiate the expression:</para>
///   <code>
///     var processor = new Processor();
///     var result = processor.Solve("deriv((x) => 2x)");
///   </code>
/// </example>
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
    /// <remarks>
    /// Initializes default instance of <see cref="Processor"/> with default implementation of all dependencies. If you can change them, please, see other overloads of this constructor.
    /// </remarks>
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
    /// <seealso cref="IDifferentiator"/>
    /// <seealso cref="ISimplifier"/>
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
    /// <exception cref="ArgumentNullException">Thrown when one of parameters is <c>null</c>.</exception>
    /// <seealso cref="ITypeAnalyzer"/>
    /// <seealso cref="IDifferentiator"/>
    /// <seealso cref="ISimplifier"/>
    /// <seealso cref="IConverter"/>
    /// <seealso cref="IParser"/>
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
    /// Evaluates the specified expression.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <remarks>
    ///   <para>If your expression contains any parameter or user-defined function, then they will be resolved from the built-in <see cref="Parameters"/> collection.</para>
    /// </remarks>
    /// <returns>The result of evaluation.</returns>
    /// <example>
    ///   <code>
    ///     var processor = new Processor();
    ///     var result = processor.Solve("2 + 2");
    ///   </code>
    /// </example>
    /// <seealso cref="Result"/>
    /// <seealso cref="Result.AngleResult"/>
    /// <seealso cref="Result.AreaResult"/>
    /// <seealso cref="Result.BooleanResult"/>
    /// <seealso cref="Result.ComplexNumberResult"/>
    /// <seealso cref="Result.EmptyResult"/>
    /// <seealso cref="Result.LambdaResult"/>
    /// <seealso cref="Result.LengthResult"/>
    /// <seealso cref="Result.MassResult"/>
    /// <seealso cref="Result.MatrixResult"/>
    /// <seealso cref="Result.NumberResult"/>
    /// <seealso cref="Result.PowerResult"/>
    /// <seealso cref="Result.StringResult"/>
    /// <seealso cref="Result.TemperatureResult"/>
    /// <seealso cref="Result.TimeResult"/>
    /// <seealso cref="Result.VectorResult"/>
    /// <seealso cref="Result.VolumeResult"/>
    public Result Solve(string expression)
    {
        var exp = Parse(expression);
        exp.Analyze(typeAnalyzer);

        var result = exp.Execute(Parameters);

        return Result.Create(result);
    }

    /// <summary>
    /// Simplifies the <paramref name="function"/>.
    /// </summary>
    /// <remarks>
    /// This method delegates the actual implementation of simplification of expression to <see cref="ISimplifier"/>.
    /// </remarks>
    /// <param name="function">The function.</param>
    /// <returns>The simplified expression.</returns>
    /// <example>
    ///   <code>
    ///     var processor = new Processor();
    ///     var result = processor.Simplify("arcsin(sin(x))");
    ///   </code>
    /// </example>
    /// <seealso cref="IExpression"/>
    public IExpression Simplify(string function)
    {
        var expression = Parse(function);

        return expression.Analyze(simplifier);
    }

    /// <summary>
    /// Simplifies the <paramref name="expression"/>.
    /// </summary>
    /// <remarks>
    /// This method delegates the actual implementation of simplification of expression to <see cref="ISimplifier"/>.
    /// </remarks>
    /// <param name="expression">A expression to simplify.</param>
    /// <returns>A simplified expression.</returns>
    /// <example>
    ///   <code>
    ///     var processor = new Processor();
    ///     var exp = processor.Parse("arcsin(sin(x))");
    ///     var result = processor.Simplify(exp);
    ///   </code>
    /// </example>
    /// <seealso cref="IExpression"/>
    public IExpression Simplify(IExpression expression)
    {
        if (expression is null)
            throw new ArgumentNullException(nameof(expression));

        return expression.Analyze(simplifier);
    }

    /// <summary>
    /// Differentiates the specified expression.
    /// </summary>
    /// <remarks>
    /// This method delegates the actual implementation of differentiation of expression to <see cref="IDifferentiator"/>.
    /// </remarks>
    /// <param name="function">The function.</param>
    /// <returns>Returns the derivative.</returns>
    /// <example>
    ///   <code>
    ///     var processor = new Processor();
    ///     var result = processor.Differentiate("x ^ 2");
    ///   </code>
    /// </example>
    /// <seealso cref="IExpression"/>
    public IExpression Differentiate(string function)
    {
        var expression = Parse(function);

        return Differentiate(expression, Variable.X);
    }

    /// <summary>
    /// Differentiates the specified expression.
    /// </summary>
    /// <remarks>
    /// This method delegates the actual implementation of differentiation of expression to <see cref="IDifferentiator"/>.
    /// </remarks>
    /// <param name="expression">The expression.</param>
    /// <returns>Returns the derivative.</returns>
    /// <example>
    ///   <code>
    ///     var processor = new Processor();
    ///     var exp = processor.Parse("x ^ 2");
    ///     var result = processor.Differentiate(exp);
    ///   </code>
    /// </example>
    /// <seealso cref="IExpression"/>
    public IExpression Differentiate(IExpression expression)
        => Differentiate(expression, Variable.X);

    /// <summary>
    /// Differentiates the specified expression.
    /// </summary>
    /// <remarks>
    /// This method delegates the actual implementation of differentiation of expression to <see cref="IDifferentiator"/>.
    /// </remarks>
    /// <param name="expression">The expression.</param>
    /// <param name="variable">The variable.</param>
    /// <returns>
    /// Returns the derivative.
    /// </returns>
    /// <seealso cref="IExpression"/>
    /// <seealso cref="Variable"/>
    public IExpression Differentiate(IExpression expression, Variable variable)
    {
        if (expression is null)
            throw new ArgumentNullException(nameof(expression));

        var context = new DifferentiatorContext(variable);

        return expression.Analyze(differentiator, context);
    }

    /// <summary>
    /// Parses the specified function.
    /// </summary>
    /// <param name="function">The function.</param>
    /// <returns>The parsed expression.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="function"/> is <c>null</c> or empty.</exception>
    /// <exception cref="ParseException">Error while parsing.</exception>
    /// <exception cref="TokenizeException">Throw when the lexer encounters the unsupported symbol.</exception>
    /// <example>
    ///   <code>
    ///     var processor = new Processor();
    ///     var exp = processor.Parse("sin(x)");
    ///   </code>
    /// </example>
    /// <seealso cref="IExpression"/>
    public IExpression Parse(string function)
        => parser.Parse(function);

    /// <summary>
    /// Gets expression parameters object.
    /// </summary>
    /// <value>
    /// The expression parameters object.
    /// </value>
    /// <seealso cref="ExpressionParameters"/>
    public ExpressionParameters Parameters { get; }
}