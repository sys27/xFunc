// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

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
///     var result = processor.Solve&lt;NumberResult&gt;("2 + 2");
///   </code>
///
///   <para>To simplify the expression:</para>
///   <code>
///     var processor = new Processor();
///     var result = processor.Solve&lt;LambdaResult&gt;("simplify((x) => arcsin(sin(x)))");
///   </code>
///
///   <para>To differentiate the expression:</para>
///   <code>
///     var processor = new Processor();
///     var result = processor.Solve&lt;LambdaResult&gt;("deriv((x) => 2x)");
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
    /// <param name="function">The function.</param>
    /// <remarks>
    /// <para>This method returns untyped <see cref="IResult"/>. Usually this method is useful when you just need a result of evaluation of <paramref name="function"/>, otherwise check generic version of this method.</para>
    /// <para>If your expression contains any parameter or user-defined function, then they will be resolved from the built-in <see cref="Parameters"/> collection.</para>
    /// </remarks>
    /// <returns>The result of evaluation.</returns>
    /// <seealso cref="IResult"/>
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
    /// Evaluates the specified function.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="function">The function.</param>
    /// <remarks>
    ///   <para>If your expression contains any parameter or user-defined function, then they will be resolved from the built-in <see cref="Parameters"/> collection.</para>
    ///   <para>
    ///     This method returns specific implementation of <see cref="IResult"/> and by accessing <see cref="IResult.Result"/> property you will be able to get typed result, for example, <c>double</c> instead of <c>object</c>.
    ///   </para>
    ///   <para><see cref="IResult"/> implementations:</para>
    ///   <list type="bullet">
    ///     <item>
    ///       <term><see cref="AngleNumberResult"/></term>
    ///     </item>
    ///     <item>
    ///       <term><see cref="AreaNumberResult"/></term>
    ///     </item>
    ///     <item>
    ///       <term><see cref="BooleanResult"/></term>
    ///     </item>
    ///     <item>
    ///       <term><see cref="ComplexNumberResult"/></term>
    ///     </item>
    ///     <item>
    ///       <term><see cref="LambdaResult"/></term>
    ///     </item>
    ///     <item>
    ///       <term><see cref="LengthNumberResult"/></term>
    ///     </item>
    ///     <item>
    ///       <term><see cref="MassNumberResult"/></term>
    ///     </item>
    ///     <item>
    ///       <term><see cref="MatrixValueResult"/></term>
    ///     </item>
    ///     <item>
    ///       <term><see cref="NumberResult"/></term>
    ///     </item>
    ///     <item>
    ///       <term><see cref="PowerNumberResult"/></term>
    ///     </item>
    ///     <item>
    ///       <term><see cref="StringResult"/></term>
    ///     </item>
    ///     <item>
    ///       <term><see cref="TemperatureNumberResult"/></term>
    ///     </item>
    ///     <item>
    ///       <term><see cref="TimeNumberResult"/></term>
    ///     </item>
    ///     <item>
    ///       <term><see cref="VectorValueResult"/></term>
    ///     </item>
    ///     <item>
    ///       <term><see cref="VolumeNumberResult"/></term>
    ///     </item>
    ///   </list>
    /// </remarks>
    /// <returns>The result of evaluation.</returns>
    /// <example>
    ///   <code>
    ///     var processor = new Processor();
    ///     var result = processor.Solve&lt;NumberResult&gt;("2 + 2");
    ///   </code>
    /// </example>
    /// <seealso cref="IResult"/>
    /// <seealso cref="AngleNumberResult"/>
    /// <seealso cref="AreaNumberResult"/>
    /// <seealso cref="BooleanResult"/>
    /// <seealso cref="ComplexNumberResult"/>
    /// <seealso cref="LambdaResult"/>
    /// <seealso cref="LengthNumberResult"/>
    /// <seealso cref="MassNumberResult"/>
    /// <seealso cref="MatrixValueResult"/>
    /// <seealso cref="NumberResult"/>
    /// <seealso cref="PowerNumberResult"/>
    /// <seealso cref="StringResult"/>
    /// <seealso cref="TemperatureNumberResult"/>
    /// <seealso cref="TimeNumberResult"/>
    /// <seealso cref="VectorValueResult"/>
    /// <seealso cref="VolumeNumberResult"/>
    public TResult Solve<TResult>(string function) where TResult : IResult
        => (TResult)Solve(function);

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
    /// <returns>Returns the derivative.</returns>
    /// <example>
    ///   <code>
    ///     var processor = new Processor();
    ///     var exp = processor.Parse("x ^ 2");
    ///     var result = processor.Differentiate(exp, Variable.X);
    ///   </code>
    /// </example>
    /// <seealso cref="IExpression"/>
    /// <seealso cref="Variable"/>
    public IExpression Differentiate(IExpression expression, Variable variable)
        => Differentiate(expression, variable, new ExpressionParameters());

    /// <summary>
    /// Differentiates the specified expression.
    /// </summary>
    /// <remarks>
    /// This method delegates the actual implementation of differentiation of expression to <see cref="IDifferentiator"/>.
    /// </remarks>
    /// <param name="expression">The expression.</param>
    /// <param name="variable">The variable.</param>
    /// <param name="parameters">The parameters.</param>
    /// <returns>
    /// Returns the derivative.
    /// </returns>
    /// <seealso cref="IExpression"/>
    /// <seealso cref="Variable"/>
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