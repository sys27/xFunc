// Copyright 2012-2020 Dmytro Kyshchenko
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Diagnostics.CodeAnalysis;
using xFunc.Maths.Analyzers;
using xFunc.Maths.Analyzers.Formatters;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Maths
{
    /// <summary>
    /// The expression builder.
    /// </summary>
    public class Builder : IExpression
    {
        private IExpression current;

        /// <summary>
        /// Initializes a new instance of the <see cref="Builder"/> class.
        /// </summary>
        /// <param name="initial">The initial value of builder.</param>
        public Builder(IExpression initial)
        {
            Current = initial;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Builder"/> class.
        /// </summary>
        /// <param name="number">The initial value of builder.</param>
        public Builder(double number)
        {
            Current = new Number(number);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Builder"/> class.
        /// </summary>
        /// <param name="variable">The initial value of builder.</param>
        public Builder(string variable)
        {
            Current = new Variable(variable);
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <param name="formatter">The formatter.</param>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public string ToString(IFormatter formatter)
            => Analyze(formatter);

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public override string ToString()
            => Current.ToString();

        /// <summary>
        /// Creates the builder.
        /// </summary>
        /// <param name="initial">The initial value of builder.</param>
        /// <returns>The new instance of builder.</returns>
        public static Builder Create(IExpression initial)
            => new Builder(initial);

        /// <summary>
        /// Creates the builder.
        /// </summary>
        /// <param name="number">The initial value of builder.</param>
        /// <returns>The new instance of builder.</returns>
        public static Builder Create(double number)
            => new Builder(number);

        /// <summary>
        /// Creates the builder.
        /// </summary>
        /// <param name="variable">The initial value of builder.</param>
        /// <returns>The new instance of builder.</returns>
        public static Builder Create(string variable)
            => new Builder(variable);

        /// <summary>
        /// Inserts a custom expression to builder.
        /// </summary>
        /// <param name="customExpression">The custom expression.</param>
        /// <returns>The Current instance of builder.</returns>
        public Builder Expression(Func<IExpression, IExpression> customExpression)
        {
            if (customExpression == null)
                throw new ArgumentNullException(nameof(customExpression));

            Current = customExpression(Current);

            return this;
        }

        #region Standart

        /// <summary>
        /// Creates the <seealso cref="Expressions.Add"/> operation. The Current state is used as summand.
        /// </summary>
        /// <param name="summand">The summand (expression).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Add(IExpression summand)
        {
            Current = new Add(Current, summand);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Add"/> operation. The Current state is used as summand.
        /// </summary>
        /// <param name="summand">The summand (number).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Add(double summand)
        {
            return Add((IExpression)new Number(summand));
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Add"/> operation. The Current state is used as summand.
        /// </summary>
        /// <param name="summand">The summand (variable).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Add(string summand)
        {
            return Add((IExpression)new Variable(summand));
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Sub"/> operation. The Current state is used as minuend.
        /// </summary>
        /// <param name="subtrahend">The subtrahend (expression).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Sub(IExpression subtrahend)
        {
            Current = new Sub(Current, subtrahend);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Sub"/> operation. The Current state is used as minuend.
        /// </summary>
        /// <param name="subtrahend">The subtrahend (number).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Sub(double subtrahend)
        {
            return Sub((IExpression)new Number(subtrahend));
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Sub"/> operation. The Current state is used as minuend.
        /// </summary>
        /// <param name="subtrahend">The subtrahend (variable).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Sub(string subtrahend)
        {
            return Sub((IExpression)new Variable(subtrahend));
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Mul"/> operation. The Current state is used as factor.
        /// </summary>
        /// <param name="factor">The factor (expression).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Mul(IExpression factor)
        {
            Current = new Mul(Current, factor);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Mul"/> operation. The Current state is used as factor.
        /// </summary>
        /// <param name="factor">The factor (number).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Mul(double factor)
        {
            return Mul((IExpression)new Number(factor));
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Mul"/> operation. The Current state is used as factor.
        /// </summary>
        /// <param name="factor">The factor (variable).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Mul(string factor)
        {
            return Mul((IExpression)new Variable(factor));
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Div"/> operation. The Current state is used as numerator.
        /// </summary>
        /// <param name="denominator">The denominator (expression).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Div(IExpression denominator)
        {
            Current = new Div(Current, denominator);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Div"/> operation. The Current state is used as numerator.
        /// </summary>
        /// <param name="denominator">The denominator (number).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Div(double denominator)
        {
            return Div((IExpression)new Number(denominator));
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Div"/> operation. The Current state is used as numerator.
        /// </summary>
        /// <param name="denominator">The denominator (variable).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Div(string denominator)
        {
            return Div((IExpression)new Variable(denominator));
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Pow"/> operation. The Current state is used as base of power.
        /// </summary>
        /// <param name="exponent">The exponent (expression).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Pow(IExpression exponent)
        {
            Current = new Pow(Current, exponent);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Pow"/> operation. The Current state is used as base of power.
        /// </summary>
        /// <param name="exponent">The exponent (number).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Pow(double exponent)
        {
            return Pow((IExpression)new Number(exponent));
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Pow"/> operation. The Current state is used as base of power.
        /// </summary>
        /// <param name="exponent">The exponent (variable).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Pow(string exponent)
        {
            return Pow((IExpression)new Variable(exponent));
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Sqrt"/> function. The Current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Sqrt()
        {
            Current = new Sqrt(Current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Root"/> operation. The Current state is used as radicand.
        /// </summary>
        /// <param name="degree">The degree (expression).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Root(IExpression degree)
        {
            Current = new Root(Current, degree);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Root"/> operation. The Current state is used as radicand.
        /// </summary>
        /// <param name="degree">The degree (number).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Root(double degree)
        {
            return Root((IExpression)new Number(degree));
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Root"/> operation. The Current state is used as radicand.
        /// </summary>
        /// <param name="degree">The degree (variable).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Root(string degree)
        {
            return Root((IExpression)new Variable(degree));
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Exp"/> function. The Current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Exp()
        {
            Current = new Exp(Current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Abs"/> function. The Current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Abs()
        {
            Current = new Abs(Current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Log"/> operation. The Current state is used as argument.
        /// </summary>
        /// <param name="base">The base (expression).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Log(IExpression @base)
        {
            Current = new Log(@base, Current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Log"/> operation. The Current state is used as argument.
        /// </summary>
        /// <param name="base">The base (number).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Log(double @base)
        {
            return Log((IExpression)new Number(@base));
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Log"/> operation. The Current state is used as argument.
        /// </summary>
        /// <param name="base">The base (variable).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Log(string @base)
        {
            return Log((IExpression)new Variable(@base));
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Ln"/> function. The Current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Ln()
        {
            Current = new Ln(Current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Lg"/> function. The Current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Lg()
        {
            Current = new Lg(Current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Lb"/> function. The Current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Lb()
        {
            Current = new Lb(Current);

            return this;
        }

        #endregion Standart

        #region Trigonometric

        /// <summary>
        /// Creates the <seealso cref="Expressions.Trigonometric.Sin"/> operation. The Current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Sin()
        {
            Current = new Sin(Current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Trigonometric.Cos"/> operation. The Current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Cos()
        {
            Current = new Cos(Current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Trigonometric.Tan"/> operation. The Current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Tan()
        {
            Current = new Tan(Current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Trigonometric.Cot"/> operation. The Current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Cot()
        {
            Current = new Cot(Current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Trigonometric.Sec"/> operation. The Current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Sec()
        {
            Current = new Sec(Current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Trigonometric.Csc"/> operation. The Current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Csc()
        {
            Current = new Csc(Current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Trigonometric.Arcsin"/> operation. The Current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Arcsin()
        {
            Current = new Arcsin(Current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Trigonometric.Arccos"/> operation. The Current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Arccos()
        {
            Current = new Arccos(Current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Trigonometric.Arctan"/> operation. The Current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Arctan()
        {
            Current = new Arctan(Current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Trigonometric.Arccot"/> operation. The Current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Arccot()
        {
            Current = new Arccot(Current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Trigonometric.Arcsec"/> operation. The Current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Arcsec()
        {
            Current = new Arcsec(Current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Trigonometric.Arccsc"/> operation. The Current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Arccsc()
        {
            Current = new Arccsc(Current);

            return this;
        }

        #endregion Trigonometric

        #region Hyperbolic

        /// <summary>
        /// Creates the <seealso cref="Expressions.Hyperbolic.Sinh"/> operation. The Current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Sinh()
        {
            Current = new Sinh(Current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Hyperbolic.Cosh"/> operation. The Current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Cosh()
        {
            Current = new Cosh(Current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Hyperbolic.Tanh"/> operation. The Current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Tanh()
        {
            Current = new Tanh(Current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Hyperbolic.Coth"/> operation. The Current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Coth()
        {
            Current = new Coth(Current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Hyperbolic.Sech"/> operation. The Current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Sech()
        {
            Current = new Sech(Current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Hyperbolic.Csch"/> operation. The Current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Csch()
        {
            Current = new Csch(Current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Hyperbolic.Arsinh"/> operation. The Current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Arsinh()
        {
            Current = new Arsinh(Current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Hyperbolic.Arcosh"/> operation. The Current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Arcosh()
        {
            Current = new Arcosh(Current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Hyperbolic.Artanh"/> operation. The Current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Artanh()
        {
            Current = new Artanh(Current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Hyperbolic.Arcoth"/> operation. The Current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Arcoth()
        {
            Current = new Arcoth(Current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Hyperbolic.Arsech"/> operation. The Current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Arsech()
        {
            Current = new Arsech(Current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Hyperbolic.Arcsch"/> operation. The Current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The Current builder is empty.</exception>
        public Builder Arcsch()
        {
            Current = new Arcsch(Current);

            return this;
        }

        #endregion Hyperbolic

        #region IExpression

        /// <summary>
        /// Executes this expression. Don't use this method if your expression has variables or user-functions.
        /// </summary>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        public object Execute() => Current.Execute();

        /// <summary>
        /// Executes this expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        public object Execute(ExpressionParameters parameters)
            => Current.Execute(parameters);

        /// <summary>
        /// Analyzes the Current expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="analyzer">The analyzer.</param>
        /// <returns>
        /// The analysis result.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public TResult Analyze<TResult>(IAnalyzer<TResult> analyzer)
            => Current.Analyze(analyzer);

        /// <summary>
        /// Analyzes the current expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="TContext">The type of additional parameter for analyzer.</typeparam>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="context">The context.</param>
        /// <returns>The analysis result.</returns>
        [ExcludeFromCodeCoverage]
        public TResult Analyze<TResult, TContext>(
            IAnalyzer<TResult, TContext> analyzer,
            TContext context)
            => Current.Analyze(analyzer, context);

        /// <summary>
        /// Clones this instance of the <see cref="IExpression" />.
        /// </summary>
        /// <returns>
        /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public IExpression Clone() => Current.Clone();

        #endregion

        /// <summary>
        /// Gets the Current expression.
        /// </summary>
        /// <value>
        /// The Current expression.
        /// </value>
        public IExpression Current
        {
            get => current;
            private set => current = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}