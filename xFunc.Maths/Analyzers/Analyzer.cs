// Copyright 2012-2018 Dmitry Kischenko
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
    /// <seealso cref="xFunc.Maths.Analyzers.IAnalyzer{TResult}" />
    [ExcludeFromCodeCoverage]
    public abstract class Analyzer<TResult> : IAnalyzer<TResult>
    {

        /// <summary>
        /// Analyzes the specified expression. This method should be only used for expessions which are not supported by xFunc (custom expression create by extendening library).
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        [ExcludeFromCodeCoverage]
        public virtual TResult Analyze(IExpression exp)
        {
            throw new NotSupportedException();
        }

        #region Standard

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Abs exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Add exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Ceil exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Define exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Del exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Derivative exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Div exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Exp exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Fact exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Floor exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(GCD exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Lb exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(LCM exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Lg exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Ln exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Log exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Mod exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Mul exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Number exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Pow exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Root exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Round exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Simplify exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Sqrt exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Sub exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(UnaryMinus exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Undefine exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(UserFunction exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Variable exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(DelegateExpression exp)
        {
            throw new NotSupportedException();
        }

        #endregion Standard

        #region Matrix

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Vector exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Matrix exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Determinant exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Inverse exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Transpose exp)
        {
            throw new NotSupportedException();
        }

        #endregion Matrix

        #region Complex Numbers

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(ComplexNumber exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Conjugate exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Im exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Phase exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Re exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Reciprocal exp)
        {
            throw new NotSupportedException();
        }

        #endregion Complex Numbers

        #region Trigonometric

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Arccos exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Arccot exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Arccsc exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Arcsec exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Arcsin exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Arctan exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Cos exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Cot exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Csc exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Sec exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Sin exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Tan exp)
        {
            throw new NotSupportedException();
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
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Arcosh exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Arcoth exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Arcsch exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Arsech exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Arsinh exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Artanh exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Cosh exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Coth exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Csch exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Sech exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Sinh exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Tanh exp)
        {
            throw new NotSupportedException();
        }

        #endregion Hyperbolic

        #region Statistical

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Avg exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Count exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Max exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Min exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Product exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified exppression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Stdev exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified exppression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Stdevp exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Sum exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified exppression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Var exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified exppression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Varp exp)
        {
            throw new NotSupportedException();
        }

        #endregion Statistical

        #region Logical and Bitwise

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Expressions.LogicalAndBitwise.And exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Bool exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Equality exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Implication exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(NAnd exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(NOr exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Not exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Expressions.LogicalAndBitwise.Or exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(XOr exp)
        {
            throw new NotSupportedException();
        }

        #endregion Logical and Bitwise

        #region Programming

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(AddAssign exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Expressions.Programming.And exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Dec exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(DivAssign exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Equal exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(For exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(GreaterOrEqual exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(GreaterThan exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(If exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Inc exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(LessOrEqual exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(LessThan exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(MulAssign exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(NotEqual exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(Expressions.Programming.Or exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(SubAssign exp)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>
        /// The result of analysis.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public virtual TResult Analyze(While exp)
        {
            throw new NotSupportedException();
        }

        #endregion Programming

    }

}
