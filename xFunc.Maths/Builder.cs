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
        public Builder()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Builder"/> class.
        /// </summary>
        /// <param name="initial">The initial value of builder.</param>
        public Builder(IExpression initial)
        {
            Init(initial);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Builder"/> class.
        /// </summary>
        /// <param name="number">The initial value of builder.</param>
        public Builder(double number)
        {
            Init(number);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Builder"/> class.
        /// </summary>
        /// <param name="variable">The initial value of builder.</param>
        public Builder(string variable)
        {
            Init(variable);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="formatter">The formatter.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public string ToString(IFormatter formatter)
        {
            return this.Analyze(formatter);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return current.ToString();
        }

        /// <summary>
        /// Creates the builder.
        /// </summary>
        /// <param name="initial">The initial value of builder.</param>
        /// <returns>The new instance of builder.</returns>
        public static Builder Create(IExpression initial)
        {
            return new Builder(initial);
        }

        /// <summary>
        /// Creates the builder.
        /// </summary>
        /// <param name="number">The initial value of builder.</param>
        /// <returns>The new instance of builder.</returns>
        public static Builder Create(double number)
        {
            return new Builder(number);
        }

        /// <summary>
        /// Creates the builder.
        /// </summary>
        /// <param name="variable">The initial value of builder.</param>
        /// <returns>The new instance of builder.</returns>
        public static Builder Create(string variable)
        {
            return new Builder(variable);
        }

        /// <summary>
        /// Initializes the builder.
        /// </summary>
        /// <param name="initial">The initial value of builder.</param>
        /// <returns>The builder.</returns>
        public Builder Init(IExpression initial)
        {
            this.current = initial;

            return this;
        }

        /// <summary>
        /// Initializes the builder.
        /// </summary>
        /// <param name="number">The initial value of builder.</param>
        /// <returns>The builder.</returns>
        public Builder Init(double number)
        {
            return Init((IExpression)new Number(number));
        }

        /// <summary>
        /// Initializes the builder.
        /// </summary>
        /// <param name="variable">The initial value of builder.</param>
        /// <returns>The builder.</returns>
        public Builder Init(string variable)
        {
            return Init((IExpression)new Variable(variable));
        }

        private void CheckCurrentExpression()
        {
            if (current == null)
                throw new ArgumentNullException(nameof(current));
        }

        /// <summary>
        /// Inserts a custom expression to builder.
        /// </summary>
        /// <param name="customExpression">The custom expression.</param>
        /// <returns>The current instance of builder.</returns>
        public Builder Expression(Func<IExpression, IExpression> customExpression)
        {
            CheckCurrentExpression();

            current = customExpression(current);

            return this;
        }

        #region Standart

        /// <summary>
        /// Creates the <seealso cref="Expressions.Add"/> operation. The current state is used as summand.
        /// </summary>
        /// <param name="summand">The summand (expression).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Add(IExpression summand)
        {
            CheckCurrentExpression();

            current = new Add(current, summand);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Add"/> operation. The current state is used as summand.
        /// </summary>
        /// <param name="summand">The summand (number).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Add(double summand)
        {
            return Add((IExpression)new Number(summand));
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Add"/> operation. The current state is used as summand.
        /// </summary>
        /// <param name="summand">The summand (variable).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Add(string summand)
        {
            return Add((IExpression)new Variable(summand));
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Sub"/> operation. The current state is used as minuend.
        /// </summary>
        /// <param name="subtrahend">The subtrahend (expression).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Sub(IExpression subtrahend)
        {
            CheckCurrentExpression();

            current = new Sub(current, subtrahend);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Sub"/> operation. The current state is used as minuend.
        /// </summary>
        /// <param name="subtrahend">The subtrahend (number).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Sub(double subtrahend)
        {
            return Sub((IExpression)new Number(subtrahend));
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Sub"/> operation. The current state is used as minuend.
        /// </summary>
        /// <param name="subtrahend">The subtrahend (variable).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Sub(string subtrahend)
        {
            return Sub((IExpression)new Variable(subtrahend));
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Mul"/> operation. The current state is used as factor.
        /// </summary>
        /// <param name="factor">The factor (expression).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Mul(IExpression factor)
        {
            CheckCurrentExpression();

            current = new Mul(current, factor);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Mul"/> operation. The current state is used as factor.
        /// </summary>
        /// <param name="factor">The factor (number).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Mul(double factor)
        {
            return Mul((IExpression)new Number(factor));
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Mul"/> operation. The current state is used as factor.
        /// </summary>
        /// <param name="factor">The factor (variable).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Mul(string factor)
        {
            return Mul((IExpression)new Variable(factor));
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Div"/> operation. The current state is used as numerator.
        /// </summary>
        /// <param name="denominator">The denominator (expression).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Div(IExpression denominator)
        {
            CheckCurrentExpression();

            current = new Div(current, denominator);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Div"/> operation. The current state is used as numerator.
        /// </summary>
        /// <param name="denominator">The denominator (number).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Div(double denominator)
        {
            return Div((IExpression)new Number(denominator));
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Div"/> operation. The current state is used as numerator.
        /// </summary>
        /// <param name="denominator">The denominator (variable).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Div(string denominator)
        {
            return Div((IExpression)new Variable(denominator));
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Pow"/> operation. The current state is used as base of power.
        /// </summary>
        /// <param name="exponent">The exponent (expression).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Pow(IExpression exponent)
        {
            CheckCurrentExpression();

            current = new Pow(current, exponent);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Pow"/> operation. The current state is used as base of power.
        /// </summary>
        /// <param name="exponent">The exponent (number).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Pow(double exponent)
        {
            return Pow((IExpression)new Number(exponent));
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Pow"/> operation. The current state is used as base of power.
        /// </summary>
        /// <param name="exponent">The exponent (variable).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Pow(string exponent)
        {
            return Pow((IExpression)new Variable(exponent));
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Sqrt"/> function. The current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Sqrt()
        {
            CheckCurrentExpression();

            current = new Sqrt(current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Root"/> operation. The current state is used as radicand.
        /// </summary>
        /// <param name="degree">The degree (expression).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Root(IExpression degree)
        {
            CheckCurrentExpression();

            current = new Root(current, degree);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Root"/> operation. The current state is used as radicand.
        /// </summary>
        /// <param name="degree">The degree (number).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Root(double degree)
        {
            return Root((IExpression)new Number(degree));
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Root"/> operation. The current state is used as radicand.
        /// </summary>
        /// <param name="degree">The degree (variable).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Root(string degree)
        {
            return Root((IExpression)new Variable(degree));
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Abs"/> function. The current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Abs()
        {
            CheckCurrentExpression();

            current = new Abs(current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Log"/> operation. The current state is used as argument.
        /// </summary>
        /// <param name="base">The base (expression).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Log(IExpression @base)
        {
            CheckCurrentExpression();

            current = new Log(@base, current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Log"/> operation. The current state is used as argument.
        /// </summary>
        /// <param name="base">The base (number).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Log(double @base)
        {
            return Log((IExpression)new Number(@base));
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Log"/> operation. The current state is used as argument.
        /// </summary>
        /// <param name="base">The base (variable).</param>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Log(string @base)
        {
            return Log((IExpression)new Variable(@base));
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Ln"/> function. The current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Ln()
        {
            CheckCurrentExpression();

            current = new Ln(current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Lg"/> function. The current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Lg()
        {
            CheckCurrentExpression();

            current = new Lg(current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Lb"/> function. The current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Lb()
        {
            CheckCurrentExpression();

            current = new Lb(current);

            return this;
        }

        #endregion Standart

        #region Trigonometric

        /// <summary>
        /// Creates the <seealso cref="Expressions.Trigonometric.Sin"/> operation. The current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Sin()
        {
            CheckCurrentExpression();

            current = new Sin(current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Trigonometric.Cos"/> operation. The current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Cos()
        {
            CheckCurrentExpression();

            current = new Cos(current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Trigonometric.Tan"/> operation. The current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Tan()
        {
            CheckCurrentExpression();

            current = new Tan(current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Trigonometric.Cot"/> operation. The current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Cot()
        {
            CheckCurrentExpression();

            current = new Cot(current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Trigonometric.Sec"/> operation. The current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Sec()
        {
            CheckCurrentExpression();

            current = new Sec(current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Trigonometric.Csc"/> operation. The current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Csc()
        {
            CheckCurrentExpression();

            current = new Csc(current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Trigonometric.Arcsin"/> operation. The current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Arcsin()
        {
            CheckCurrentExpression();

            current = new Arcsin(current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Trigonometric.Arccos"/> operation. The current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Arccos()
        {
            CheckCurrentExpression();

            current = new Arccos(current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Trigonometric.Arctan"/> operation. The current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Arctan()
        {
            CheckCurrentExpression();

            current = new Arctan(current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Trigonometric.Arccot"/> operation. The current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Arccot()
        {
            CheckCurrentExpression();

            current = new Arccot(current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Trigonometric.Arcsec"/> operation. The current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Arcsec()
        {
            CheckCurrentExpression();

            current = new Arcsec(current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Trigonometric.Arccsc"/> operation. The current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Arccsc()
        {
            CheckCurrentExpression();

            current = new Arccsc(current);

            return this;
        }

        #endregion Trigonometric

        #region Hyperbolic

        /// <summary>
        /// Creates the <seealso cref="Expressions.Hyperbolic.Sinh"/> operation. The current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Sinh()
        {
            CheckCurrentExpression();

            current = new Sinh(current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Hyperbolic.Cosh"/> operation. The current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Cosh()
        {
            CheckCurrentExpression();

            current = new Cosh(current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Hyperbolic.Tanh"/> operation. The current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Tanh()
        {
            CheckCurrentExpression();

            current = new Tanh(current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Hyperbolic.Coth"/> operation. The current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Coth()
        {
            CheckCurrentExpression();

            current = new Coth(current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Hyperbolic.Sech"/> operation. The current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Sech()
        {
            CheckCurrentExpression();

            current = new Sech(current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Hyperbolic.Csch"/> operation. The current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Csch()
        {
            CheckCurrentExpression();

            current = new Csch(current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Hyperbolic.Arsinh"/> operation. The current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Arsinh()
        {
            CheckCurrentExpression();

            current = new Arsinh(current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Hyperbolic.Arcosh"/> operation. The current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Arcosh()
        {
            CheckCurrentExpression();

            current = new Arcosh(current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Hyperbolic.Artanh"/> operation. The current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Artanh()
        {
            CheckCurrentExpression();

            current = new Artanh(current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Hyperbolic.Arcoth"/> operation. The current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Arcoth()
        {
            CheckCurrentExpression();

            current = new Arcoth(current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Hyperbolic.Arsech"/> operation. The current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Arsech()
        {
            CheckCurrentExpression();

            current = new Arsech(current);

            return this;
        }

        /// <summary>
        /// Creates the <seealso cref="Expressions.Hyperbolic.Arcsch"/> operation. The current state is used as argument.
        /// </summary>
        /// <returns>The builder.</returns>
        /// <exception cref="ArgumentNullException">The current builder is empty.</exception>
        public Builder Arcsch()
        {
            CheckCurrentExpression();

            current = new Arcsch(current);

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
        public object Execute()
        {
            return current.Execute();
        }

        /// <summary>
        /// Executes this expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        public object Execute(ExpressionParameters parameters)
        {
            return current.Execute(parameters);
        }

        /// <summary>
        /// Analyzes the current expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="analyzer">The analyzer.</param>
        /// <returns>
        /// The analysis result.
        /// </returns>
        public TResult Analyze<TResult>(IAnalyzer<TResult> analyzer)
        {
            return this.current.Analyze(analyzer);
        }

        /// <summary>
        /// Clones this instance of the <see cref="IExpression" />.
        /// </summary>
        /// <returns>
        /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
        /// </returns>
        public IExpression Clone()
        {
            return current.Clone();
        }

        /// <summary>
        /// Get or Set the parent expression.
        /// </summary>
        /// <exception cref="System.NotImplementedException">Always.</exception>
        public IExpression Parent
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        #endregion

        /// <summary>
        /// Gets the current expression.
        /// </summary>
        /// <value>
        /// The current expression.
        /// </value>
        public IExpression Current => current;

    }

}
