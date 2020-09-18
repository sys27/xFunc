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
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Angles;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions.Programming;
using xFunc.Maths.Expressions.Statistical;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Maths.Analyzers
{
    /// <summary>
    /// The abstract class with default Analyzer API realization. It's useful where you don't need to implement whole interface (just a few methods).
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <typeparam name="TContext">The type of additional parameter for analyzer.</typeparam>
    /// <seealso cref="IAnalyzer{TResult,TContext}" />
    [ExcludeFromCodeCoverage]
    public class Analyzer<TResult, TContext> : IAnalyzer<TResult, TContext>
    {
        #region Standard

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Abs exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Add exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Ceil exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Define exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Del exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Derivative exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Div exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Exp exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Fact exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Floor exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Trunc exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Frac exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(GCD exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Lb exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(LCM exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Lg exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Ln exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Log exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Mod exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Mul exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Number exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Angle exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(ToDegree exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(ToRadian exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(ToGradian exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(ToNumber exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Pow exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Root exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Round exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Simplify exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Sqrt exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Sub exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(UnaryMinus exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Undefine exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(UserFunction exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Variable exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(DelegateExpression exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Sign exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(ToBin exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(ToOct exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(ToHex exp, TContext context)
            => throw new NotSupportedException();

        #endregion Standard

        #region Matrix

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Vector exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Matrix exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Determinant exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Inverse exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Transpose exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(DotProduct exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(CrossProduct exp, TContext context)
            => throw new NotSupportedException();

        #endregion Matrix

        #region Complex Numbers

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(ComplexNumber exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Conjugate exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Im exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Phase exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Re exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Reciprocal exp, TContext context)
            => throw new NotSupportedException();

        #endregion Complex Numbers

        #region Trigonometric

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Arccos exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Arccot exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Arccsc exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Arcsec exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Arcsin exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Arctan exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Cos exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Cot exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Csc exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Sec exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Sin exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Tan exp, TContext context)
            => throw new NotSupportedException();

        #endregion

        #region Hyperbolic

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Arcosh exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Arcoth exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Arcsch exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Arsech exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Arsinh exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Artanh exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Cosh exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Coth exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Csch exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Sech exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Sinh exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Tanh exp, TContext context)
            => throw new NotSupportedException();

        #endregion Hyperbolic

        #region Statistical

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Avg exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expresion.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Count exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Max exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Min exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Product exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Stdev exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Stdevp exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Sum exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Var exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Varp exp, TContext context)
            => throw new NotSupportedException();

        #endregion Statistical

        #region Logical and Bitwise

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(And exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Bool exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Equality exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Implication exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(NAnd exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(NOr exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Not exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Or exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(XOr exp, TContext context)
            => throw new NotSupportedException();

        #endregion Logical and Bitwise

        #region Programming

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(AddAssign exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(ConditionalAnd exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Dec exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(DivAssign exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Equal exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(For exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(GreaterOrEqual exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(GreaterThan exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(If exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(Inc exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(LessOrEqual exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(LessThan exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(MulAssign exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(NotEqual exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(ConditionalOr exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(SubAssign exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(While exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(LeftShift exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(RightShift exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(LeftShiftAssign exp, TContext context)
            => throw new NotSupportedException();

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <param name="context">The context.</param>
        /// <returns>The result of analysis.</returns>
        public virtual TResult Analyze(RightShiftAssign exp, TContext context)
            => throw new NotSupportedException();

        #endregion Programming
    }
}