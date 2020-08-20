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

// TODO:
#pragma warning disable CA1062

using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Maths.Analyzers
{
    /// <summary>
    /// The differentiator of expressions.
    /// </summary>
    /// <seealso cref="Analyzer{TResult}" />
    /// <seealso cref="IDifferentiator" />
    public class Differentiator : Analyzer<IExpression>, IDifferentiator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Differentiator"/> class.
        /// </summary>
        public Differentiator()
            : this(new ExpressionParameters(), Variable.X)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Differentiator"/> class.
        /// </summary>
        /// <param name="variable">The variable.</param>
        public Differentiator(Variable variable)
            : this(new ExpressionParameters(), variable)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Differentiator"/> class.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="variable">The variable.</param>
        public Differentiator(ExpressionParameters parameters, Variable variable)
        {
            Parameters = parameters;
            Variable = variable;
        }

        #region Standard

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Abs exp)
        {
            if (!Helpers.HasVariable(exp, Variable))
                return new Number(0);

            return new Mul(
                exp.Argument.Analyze(this),
                new Div(exp.Argument.Clone(), exp.Clone()));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Add exp)
        {
            var hasVariableInLeft = Helpers.HasVariable(exp.Left, Variable);
            var hasVariableInRight = Helpers.HasVariable(exp.Right, Variable);

            return (hasVariableInLeft, hasVariableInRight) switch
            {
                (true, true) => new Add(exp.Left.Analyze(this), exp.Right.Analyze(this)),
                (true, _) => exp.Left.Analyze(this),
                (_, true) => exp.Right.Analyze(this),
                _ => new Number(0),
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Derivative exp)
        {
            if (!Helpers.HasVariable(exp, Variable))
                return new Number(0);

            var diff = exp.Expression;
            if (diff is Derivative)
                diff = diff.Analyze(this);

            diff = diff.Analyze(this);

            return diff;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Div exp)
        {
            var hasVariableInLeft = Helpers.HasVariable(exp.Left, Variable);
            var hasVariableInRight = Helpers.HasVariable(exp.Right, Variable);

            return (hasVariableInLeft, hasVariableInRight) switch
            {
                (true, true) =>
                    new Div(
                        new Sub(
                            new Mul(exp.Left.Analyze(this), exp.Right.Clone()),
                            new Mul(exp.Left.Clone(), exp.Right.Analyze(this))),
                        new Pow(exp.Right.Clone(), new Number(2))),

                (true, _) =>
                    new Div(exp.Left.Analyze(this), exp.Right.Clone()),

                (_, true) =>
                    new Div(
                        new UnaryMinus(new Mul(exp.Left.Clone(), exp.Right.Analyze(this))),
                        new Pow(exp.Right.Clone(), new Number(2))),

                _ => new Number(0),
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Exp exp)
        {
            if (!Helpers.HasVariable(exp, Variable))
                return new Number(0);

            return new Mul(exp.Argument.Analyze(this), exp.Clone());
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Lb exp)
        {
            if (!Helpers.HasVariable(exp, Variable))
                return new Number(0);

            return new Div(
                exp.Argument.Analyze(this),
                new Mul(
                    exp.Argument.Clone(),
                    new Ln(new Number(2))));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Lg exp)
        {
            if (!Helpers.HasVariable(exp, Variable))
                return new Number(0);

            return new Div(
                exp.Argument.Analyze(this),
                new Mul(
                    exp.Argument.Clone(),
                    new Ln(new Number(10))));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Ln exp)
        {
            if (!Helpers.HasVariable(exp, Variable))
                return new Number(0);

            return new Div(exp.Argument.Analyze(this), exp.Argument.Clone());
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Log exp)
        {
            if (Helpers.HasVariable(exp.Left, Variable))
            {
                var div = new Div(
                    new Ln(exp.Right.Clone()),
                    new Ln(exp.Left.Clone()));

                return Analyze(div);
            }

            if (Helpers.HasVariable(exp.Right, Variable))
            {
                return new Div(
                    exp.Right.Analyze(this),
                    new Mul(
                        exp.Right.Clone(),
                        new Ln(exp.Left.Clone())));
            }

            return new Number(0);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Mul exp)
        {
            var hasVariableInLeft = Helpers.HasVariable(exp.Left, Variable);
            var hasVariableInRight = Helpers.HasVariable(exp.Right, Variable);

            return (hasVariableInLeft, hasVariableInRight) switch
            {
                (true, true) =>
                    new Add(
                        new Mul(exp.Left.Analyze(this), exp.Right.Clone()),
                        new Mul(exp.Left.Clone(), exp.Right.Analyze(this))),

                (true, _) =>
                    new Mul(exp.Left.Analyze(this), exp.Right.Clone()),

                (_, true) =>
                    new Mul(exp.Left.Clone(), exp.Right.Analyze(this)),

                _ => new Number(0),
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Number exp)
        {
            return new Number(0);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Pow exp)
        {
            if (Helpers.HasVariable(exp.Left, Variable))
            {
                return new Mul(
                    exp.Left.Analyze(this),
                    new Mul(
                        exp.Right.Clone(),
                        new Pow(
                            exp.Left.Clone(),
                            new Sub(exp.Right.Clone(), new Number(1)))));
            }

            if (Helpers.HasVariable(exp.Right, Variable))
            {
                return new Mul(
                    new Mul(
                        new Ln(exp.Left.Clone()),
                        exp.Clone()),
                    exp.Right.Analyze(this));
            }

            return new Number(0);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Root exp)
        {
            if (!Helpers.HasVariable(exp, Variable))
                return new Number(0);

            var pow = new Pow(
                exp.Left.Clone(),
                new Div(new Number(1), exp.Right.Clone()));

            return Analyze(pow);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Simplify exp)
        {
            if (!Helpers.HasVariable(exp, Variable))
                return new Number(0);

            return exp.Argument.Analyze(this);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Sqrt exp)
        {
            if (!Helpers.HasVariable(exp, Variable))
                return new Number(0);

            return new Div(
                exp.Argument.Analyze(this),
                new Mul(new Number(2), exp.Clone()));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Sub exp)
        {
            var hasVariableInLeft = Helpers.HasVariable(exp.Left, Variable);
            var hasVariableInRight = Helpers.HasVariable(exp.Right, Variable);

            return (hasVariableInLeft, hasVariableInRight) switch
            {
                (true, true) => new Sub(exp.Left.Analyze(this), exp.Right.Analyze(this)),
                (true, _) => exp.Left.Analyze(this),
                (_, true) => new UnaryMinus(exp.Right.Analyze(this)),
                _ => new Number(0),
            };
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(UnaryMinus exp)
        {
            if (!Helpers.HasVariable(exp, Variable))
                return new Number(0);

            return new UnaryMinus(exp.Argument.Analyze(this));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(UserFunction exp)
        {
            if (Parameters == null)
                throw new InvalidOperationException();

            return Parameters.Functions[exp].Analyze(this);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Variable exp)
        {
            if (exp.Equals(Variable))
                return new Number(1);

            return exp.Clone();
        }

        #endregion Standard

        #region Trigonometric

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Arccos exp)
        {
            if (!Helpers.HasVariable(exp, Variable))
                return new Number(0);

            return new UnaryMinus(
                new Div(
                    exp.Argument.Analyze(this),
                    new Sqrt(
                        new Sub(
                            new Number(1),
                            new Pow(exp.Argument.Clone(), new Number(2))))));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Arccot exp)
        {
            if (!Helpers.HasVariable(exp, Variable))
                return new Number(0);

            return new UnaryMinus(
                new Div(
                    exp.Argument.Analyze(this),
                    new Add(
                        new Number(1),
                        new Pow(exp.Argument.Clone(), new Number(2)))));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Arccsc exp)
        {
            if (!Helpers.HasVariable(exp, Variable))
                return new Number(0);

            return new UnaryMinus(
                new Div(
                    exp.Argument.Analyze(this),
                    new Mul(
                        new Abs(exp.Argument.Clone()),
                        new Sqrt(
                            new Sub(
                                new Pow(exp.Argument.Clone(), new Number(2)),
                                new Number(1))))));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Arcsec exp)
        {
            if (!Helpers.HasVariable(exp, Variable))
                return new Number(0);

            return new Div(
                exp.Argument.Analyze(this),
                new Mul(
                    new Abs(exp.Argument.Clone()),
                    new Sqrt(
                        new Sub(
                            new Pow(exp.Argument.Clone(), new Number(2)),
                            new Number(1)))));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Arcsin exp)
        {
            if (!Helpers.HasVariable(exp, Variable))
                return new Number(0);

            return new Div(
                exp.Argument.Analyze(this),
                new Sqrt(
                    new Sub(
                        new Number(1),
                        new Pow(exp.Argument.Clone(), new Number(2)))));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Arctan exp)
        {
            if (!Helpers.HasVariable(exp, Variable))
                return new Number(0);

            return new Div(
                exp.Argument.Analyze(this),
                new Add(
                    new Number(1),
                    new Pow(exp.Argument.Clone(), new Number(2))));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Cos exp)
        {
            if (!Helpers.HasVariable(exp, Variable))
                return new Number(0);

            return new UnaryMinus(
                new Mul(
                    new Sin(exp.Argument.Clone()),
                    exp.Argument.Analyze(this)));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Cot exp)
        {
            if (!Helpers.HasVariable(exp, Variable))
                return new Number(0);

            return new UnaryMinus(
                new Div(
                    exp.Argument.Analyze(this),
                    new Pow(
                        new Sin(exp.Argument.Clone()),
                        new Number(2))));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Csc exp)
        {
            return new Mul(
                new UnaryMinus(exp.Argument.Analyze(this)),
                new Mul(
                    new Cot(exp.Argument.Clone()),
                    new Csc(exp.Argument.Clone())));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Sec exp)
        {
            if (!Helpers.HasVariable(exp, Variable))
                return new Number(0);

            return new Mul(
                exp.Argument.Analyze(this),
                new Mul(
                    new Tan(exp.Argument.Clone()),
                    new Sec(exp.Argument.Clone())));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Sin exp)
        {
            if (!Helpers.HasVariable(exp, Variable))
                return new Number(0);

            return new Mul(
                new Cos(exp.Argument.Clone()),
                exp.Argument.Analyze(this));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Tan exp)
        {
            if (!Helpers.HasVariable(exp, Variable))
                return new Number(0);

            return new Div(
                exp.Argument.Analyze(this),
                new Pow(
                    new Cos(exp.Argument.Clone()),
                    new Number(2)));
        }

        #endregion

        #region Hyperbolic

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Arcosh exp)
        {
            if (!Helpers.HasVariable(exp, Variable))
                return new Number(0);

            return new Div(
                exp.Argument.Analyze(this),
                new Sqrt(
                    new Sub(
                        new Pow(exp.Argument.Clone(), new Number(2)),
                        new Number(1))));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Arcoth exp)
        {
            if (!Helpers.HasVariable(exp, Variable))
                return new Number(0);

            return new Div(
                exp.Argument.Analyze(this),
                new Sub(
                    new Number(1),
                    new Pow(exp.Argument.Clone(), new Number(2))));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Arcsch exp)
        {
            if (!Helpers.HasVariable(exp, Variable))
                return new Number(0);

            return new UnaryMinus(
                new Div(
                    exp.Argument.Analyze(this),
                    new Mul(
                        new Abs(exp.Argument.Clone()),
                        new Sqrt(
                            new Add(
                                new Number(1),
                                new Pow(exp.Argument.Clone(), new Number(2)))))));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Arsech exp)
        {
            return new UnaryMinus(
                new Div(
                    exp.Argument.Analyze(this),
                    new Mul(
                        exp.Argument.Clone(),
                        new Sqrt(
                            new Sub(
                                new Number(1),
                                new Pow(exp.Argument.Clone(), new Number(2)))))));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Arsinh exp)
        {
            if (!Helpers.HasVariable(exp, Variable))
                return new Number(0);

            return new Div(
                exp.Argument.Analyze(this),
                new Sqrt(
                    new Add(
                        new Pow(exp.Argument.Clone(), new Number(2)),
                        new Number(1))));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Artanh exp)
        {
            if (!Helpers.HasVariable(exp, Variable))
                return new Number(0);

            return new Div(
                exp.Argument.Analyze(this),
                new Sub(
                    new Number(1),
                    new Pow(exp.Argument.Clone(), new Number(2))));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Cosh exp)
        {
            if (!Helpers.HasVariable(exp, Variable))
                return new Number(0);

            return new Mul(
                exp.Argument.Analyze(this),
                new Sinh(exp.Argument.Clone()));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Coth exp)
        {
            if (!Helpers.HasVariable(exp, Variable))
                return new Number(0);

            return new UnaryMinus(
                new Div(
                    exp.Argument.Analyze(this),
                    new Pow(
                        new Sinh(exp.Argument.Clone()),
                        new Number(2))));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Csch exp)
        {
            if (!Helpers.HasVariable(exp, Variable))
                return new Number(0);

            return new UnaryMinus(
                new Mul(
                    exp.Argument.Analyze(this),
                    new Mul(
                        new Coth(exp.Argument.Clone()),
                        exp.Clone())));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Sech exp)
        {
            if (!Helpers.HasVariable(exp, Variable))
                return new Number(0);

            return new UnaryMinus(
                new Mul(
                    exp.Argument.Analyze(this),
                    new Mul(
                        new Tanh(exp.Argument.Clone()),
                        exp.Clone())));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Sinh exp)
        {
            if (!Helpers.HasVariable(exp, Variable))
                return new Number(0);

            return new Mul(
                exp.Argument.Analyze(this),
                new Cosh(exp.Argument.Clone()));
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        public override IExpression Analyze(Tanh exp)
        {
            if (!Helpers.HasVariable(exp, Variable))
                return new Number(0);

            return new Div(
                exp.Argument.Analyze(this),
                new Pow(
                    new Cosh(exp.Argument.Clone()),
                    new Number(2)));
        }

        #endregion Hyperbolic

        /// <summary>
        /// Gets or sets the variable.
        /// </summary>
        /// <value>
        /// The variable.
        /// </value>
        public Variable Variable { get; set; }

        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        public ExpressionParameters Parameters { get; set; }
    }
}

#pragma warning restore CA1062