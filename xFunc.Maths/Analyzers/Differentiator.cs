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
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Angles;
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Maths.Analyzers
{
    /// <summary>
    /// The differentiator of expressions.
    /// </summary>
    /// <seealso cref="Analyzer{TResult}" />
    /// <seealso cref="IDifferentiator" />
    public class Differentiator : Analyzer<IExpression, DifferentiatorContext>, IDifferentiator
    {
        #region Standard

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Abs exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            if (!Helpers.HasVariable(exp, context.Variable))
                return Number.Zero;

            return new Mul(
                exp.Argument.Analyze(this, context),
                new Div(exp.Argument.Clone(), exp.Clone()));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Add exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            var hasVariableInLeft = Helpers.HasVariable(exp.Left, context.Variable);
            var hasVariableInRight = Helpers.HasVariable(exp.Right, context.Variable);

            return (hasVariableInLeft, hasVariableInRight) switch
            {
                (true, true) => new Add(exp.Left.Analyze(this, context), exp.Right.Analyze(this, context)),
                (true, _) => exp.Left.Analyze(this, context),
                (_, true) => exp.Right.Analyze(this, context),
                _ => Number.Zero,
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Derivative exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            if (!Helpers.HasVariable(exp, context.Variable))
                return Number.Zero;

            var diff = exp.Expression;
            if (diff is Derivative)
                diff = diff.Analyze(this, context);

            diff = diff.Analyze(this, context);

            return diff;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Div exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            var hasVariableInLeft = Helpers.HasVariable(exp.Left, context.Variable);
            var hasVariableInRight = Helpers.HasVariable(exp.Right, context.Variable);

            return (hasVariableInLeft, hasVariableInRight) switch
            {
                (true, true) =>
                    new Div(
                        new Sub(
                            new Mul(exp.Left.Analyze(this, context), exp.Right.Clone()),
                            new Mul(exp.Left.Clone(), exp.Right.Analyze(this, context))),
                        new Pow(exp.Right.Clone(), Number.Two)),

                (true, _) =>
                    new Div(exp.Left.Analyze(this, context), exp.Right.Clone()),

                (_, true) =>
                    new Div(
                        new UnaryMinus(new Mul(exp.Left.Clone(), exp.Right.Analyze(this, context))),
                        new Pow(exp.Right.Clone(), Number.Two)),

                _ => Number.Zero,
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Exp exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            if (!Helpers.HasVariable(exp, context.Variable))
                return Number.Zero;

            return new Mul(exp.Argument.Analyze(this, context), exp.Clone());
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Lb exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            if (!Helpers.HasVariable(exp, context.Variable))
                return Number.Zero;

            return new Div(
                exp.Argument.Analyze(this, context),
                new Mul(
                    exp.Argument.Clone(),
                    new Ln(Number.Two)));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Lg exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            if (!Helpers.HasVariable(exp, context.Variable))
                return Number.Zero;

            return new Div(
                exp.Argument.Analyze(this, context),
                new Mul(
                    exp.Argument.Clone(),
                    new Ln(new Number(10))));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Ln exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            if (!Helpers.HasVariable(exp, context.Variable))
                return Number.Zero;

            return new Div(exp.Argument.Analyze(this, context), exp.Argument.Clone());
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Log exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            if (Helpers.HasVariable(exp.Left, context.Variable))
            {
                var div = new Div(
                    new Ln(exp.Right.Clone()),
                    new Ln(exp.Left.Clone()));

                return Analyze(div, context);
            }

            if (Helpers.HasVariable(exp.Right, context.Variable))
            {
                return new Div(
                    exp.Right.Analyze(this, context),
                    new Mul(
                        exp.Right.Clone(),
                        new Ln(exp.Left.Clone())));
            }

            return Number.Zero;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Mul exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            var hasVariableInLeft = Helpers.HasVariable(exp.Left, context.Variable);
            var hasVariableInRight = Helpers.HasVariable(exp.Right, context.Variable);

            return (hasVariableInLeft, hasVariableInRight) switch
            {
                (true, true) =>
                    new Add(
                        new Mul(exp.Left.Analyze(this, context), exp.Right.Clone()),
                        new Mul(exp.Left.Clone(), exp.Right.Analyze(this, context))),

                (true, _) =>
                    new Mul(exp.Left.Analyze(this, context), exp.Right.Clone()),

                (_, true) =>
                    new Mul(exp.Left.Clone(), exp.Right.Analyze(this, context)),

                _ => Number.Zero,
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Number exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            return Number.Zero;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Angle exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            return Number.Zero;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Pow exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            if (Helpers.HasVariable(exp.Left, context.Variable))
            {
                return new Mul(
                    exp.Left.Analyze(this, context),
                    new Mul(
                        exp.Right.Clone(),
                        new Pow(
                            exp.Left.Clone(),
                            new Sub(exp.Right.Clone(), Number.One))));
            }

            if (Helpers.HasVariable(exp.Right, context.Variable))
            {
                return new Mul(
                    new Mul(
                        new Ln(exp.Left.Clone()),
                        exp.Clone()),
                    exp.Right.Analyze(this, context));
            }

            return Number.Zero;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Root exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            if (!Helpers.HasVariable(exp, context.Variable))
                return Number.Zero;

            var pow = new Pow(
                exp.Left.Clone(),
                new Div(Number.One, exp.Right.Clone()));

            return Analyze(pow, context);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Simplify exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            if (!Helpers.HasVariable(exp, context.Variable))
                return Number.Zero;

            return exp.Argument.Analyze(this, context);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Sqrt exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            if (!Helpers.HasVariable(exp, context.Variable))
                return Number.Zero;

            return new Div(
                exp.Argument.Analyze(this, context),
                new Mul(Number.Two, exp.Clone()));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Sub exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            var hasVariableInLeft = Helpers.HasVariable(exp.Left, context.Variable);
            var hasVariableInRight = Helpers.HasVariable(exp.Right, context.Variable);

            return (hasVariableInLeft, hasVariableInRight) switch
            {
                (true, true) => new Sub(exp.Left.Analyze(this, context), exp.Right.Analyze(this, context)),
                (true, _) => exp.Left.Analyze(this, context),
                (_, true) => new UnaryMinus(exp.Right.Analyze(this, context)),
                _ => Number.Zero,
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(UnaryMinus exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            if (!Helpers.HasVariable(exp, context.Variable))
                return Number.Zero;

            return new UnaryMinus(exp.Argument.Analyze(this, context));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(UserFunction exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            if (context.Parameters == null)
                throw new InvalidOperationException();

            return context.Parameters.Functions[exp].Analyze(this, context);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Variable exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            if (exp.Equals(context.Variable))
                return Number.One;

            return exp.Clone();
        }

        #endregion Standard

        #region Trigonometric

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Arccos exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            if (!Helpers.HasVariable(exp, context.Variable))
                return Number.Zero;

            return new UnaryMinus(
                new Div(
                    exp.Argument.Analyze(this, context),
                    new Sqrt(
                        new Sub(
                            Number.One,
                            new Pow(exp.Argument.Clone(), Number.Two)))));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Arccot exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            if (!Helpers.HasVariable(exp, context.Variable))
                return Number.Zero;

            return new UnaryMinus(
                new Div(
                    exp.Argument.Analyze(this, context),
                    new Add(
                        Number.One,
                        new Pow(exp.Argument.Clone(), Number.Two))));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Arccsc exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            if (!Helpers.HasVariable(exp, context.Variable))
                return Number.Zero;

            return new UnaryMinus(
                new Div(
                    exp.Argument.Analyze(this, context),
                    new Mul(
                        new Abs(exp.Argument.Clone()),
                        new Sqrt(
                            new Sub(
                                new Pow(exp.Argument.Clone(), Number.Two),
                                Number.One)))));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Arcsec exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            if (!Helpers.HasVariable(exp, context.Variable))
                return Number.Zero;

            return new Div(
                exp.Argument.Analyze(this, context),
                new Mul(
                    new Abs(exp.Argument.Clone()),
                    new Sqrt(
                        new Sub(
                            new Pow(exp.Argument.Clone(), Number.Two),
                            Number.One))));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Arcsin exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            if (!Helpers.HasVariable(exp, context.Variable))
                return Number.Zero;

            return new Div(
                exp.Argument.Analyze(this, context),
                new Sqrt(
                    new Sub(
                        Number.One,
                        new Pow(exp.Argument.Clone(), Number.Two))));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Arctan exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            if (!Helpers.HasVariable(exp, context.Variable))
                return Number.Zero;

            return new Div(
                exp.Argument.Analyze(this, context),
                new Add(
                    Number.One,
                    new Pow(exp.Argument.Clone(), Number.Two)));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Cos exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            if (!Helpers.HasVariable(exp, context.Variable))
                return Number.Zero;

            return new UnaryMinus(
                new Mul(
                    new Sin(exp.Argument.Clone()),
                    exp.Argument.Analyze(this, context)));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Cot exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            if (!Helpers.HasVariable(exp, context.Variable))
                return Number.Zero;

            return new UnaryMinus(
                new Div(
                    exp.Argument.Analyze(this, context),
                    new Pow(
                        new Sin(exp.Argument.Clone()),
                        Number.Two)));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Csc exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            return new Mul(
                new UnaryMinus(exp.Argument.Analyze(this, context)),
                new Mul(
                    new Cot(exp.Argument.Clone()),
                    new Csc(exp.Argument.Clone())));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Sec exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            if (!Helpers.HasVariable(exp, context.Variable))
                return Number.Zero;

            return new Mul(
                exp.Argument.Analyze(this, context),
                new Mul(
                    new Tan(exp.Argument.Clone()),
                    new Sec(exp.Argument.Clone())));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Sin exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            if (!Helpers.HasVariable(exp, context.Variable))
                return Number.Zero;

            return new Mul(
                new Cos(exp.Argument.Clone()),
                exp.Argument.Analyze(this, context));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Tan exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            if (!Helpers.HasVariable(exp, context.Variable))
                return Number.Zero;

            return new Div(
                exp.Argument.Analyze(this, context),
                new Pow(
                    new Cos(exp.Argument.Clone()),
                    Number.Two));
        }

        #endregion

        #region Hyperbolic

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Arcosh exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            if (!Helpers.HasVariable(exp, context.Variable))
                return Number.Zero;

            return new Div(
                exp.Argument.Analyze(this, context),
                new Sqrt(
                    new Sub(
                        new Pow(exp.Argument.Clone(), Number.Two),
                        Number.One)));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Arcoth exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            if (!Helpers.HasVariable(exp, context.Variable))
                return Number.Zero;

            return new Div(
                exp.Argument.Analyze(this, context),
                new Sub(
                    Number.One,
                    new Pow(exp.Argument.Clone(), Number.Two)));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Arcsch exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            if (!Helpers.HasVariable(exp, context.Variable))
                return Number.Zero;

            return new UnaryMinus(
                new Div(
                    exp.Argument.Analyze(this, context),
                    new Mul(
                        new Abs(exp.Argument.Clone()),
                        new Sqrt(
                            new Add(
                                Number.One,
                                new Pow(exp.Argument.Clone(), Number.Two))))));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Arsech exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            return new UnaryMinus(
                new Div(
                    exp.Argument.Analyze(this, context),
                    new Mul(
                        exp.Argument.Clone(),
                        new Sqrt(
                            new Sub(
                                Number.One,
                                new Pow(exp.Argument.Clone(), Number.Two))))));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Arsinh exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            if (!Helpers.HasVariable(exp, context.Variable))
                return Number.Zero;

            return new Div(
                exp.Argument.Analyze(this, context),
                new Sqrt(
                    new Add(
                        new Pow(exp.Argument.Clone(), Number.Two),
                        Number.One)));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Artanh exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            if (!Helpers.HasVariable(exp, context.Variable))
                return Number.Zero;

            return new Div(
                exp.Argument.Analyze(this, context),
                new Sub(
                    Number.One,
                    new Pow(exp.Argument.Clone(), Number.Two)));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Cosh exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            if (!Helpers.HasVariable(exp, context.Variable))
                return Number.Zero;

            return new Mul(
                exp.Argument.Analyze(this, context),
                new Sinh(exp.Argument.Clone()));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Coth exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            if (!Helpers.HasVariable(exp, context.Variable))
                return Number.Zero;

            return new UnaryMinus(
                new Div(
                    exp.Argument.Analyze(this, context),
                    new Pow(
                        new Sinh(exp.Argument.Clone()),
                        Number.Two)));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Csch exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            if (!Helpers.HasVariable(exp, context.Variable))
                return Number.Zero;

            return new UnaryMinus(
                new Mul(
                    exp.Argument.Analyze(this, context),
                    new Mul(
                        new Coth(exp.Argument.Clone()),
                        exp.Clone())));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Sech exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            if (!Helpers.HasVariable(exp, context.Variable))
                return Number.Zero;

            return new UnaryMinus(
                new Mul(
                    exp.Argument.Analyze(this, context),
                    new Mul(
                        new Tanh(exp.Argument.Clone()),
                        exp.Clone())));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Sinh exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            if (!Helpers.HasVariable(exp, context.Variable))
                return Number.Zero;

            return new Mul(
                exp.Argument.Analyze(this, context),
                new Cosh(exp.Argument.Clone()));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Tanh exp, DifferentiatorContext context)
        {
            if (exp is null)
                throw ThrowHelpers.ExpNull();
            if (context is null)
                throw ThrowHelpers.ContextNull();

            if (!Helpers.HasVariable(exp, context.Variable))
                return Number.Zero;

            return new Div(
                exp.Argument.Analyze(this, context),
                new Pow(
                    new Cosh(exp.Argument.Clone()),
                    Number.Two));
        }

        #endregion Hyperbolic
    }
}