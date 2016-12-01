// Copyright 2012-2016 Dmitry Kischenko
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
    /// The interface for analyzers.
    /// </summary>
    /// <typeparam name="TResult">The type of the result of analysis.</typeparam>
    public interface IAnalyzer<TResult>
    {

        #region Standard

        TResult Analyze(Abs exp);
        TResult Analyze(Add exp);
        TResult Analyze(Ceil exp);
        TResult Analyze(Define exp);
        TResult Analyze(Del exp);
        TResult Analyze(Derivative exp);
        TResult Analyze(Div exp);
        TResult Analyze(Exp exp);
        TResult Analyze(Fact exp);
        TResult Analyze(Floor exp);
        TResult Analyze(GCD exp);
        TResult Analyze(Lb exp);
        TResult Analyze(LCM exp);
        TResult Analyze(Lg exp);
        TResult Analyze(Ln exp);
        TResult Analyze(Log exp);
        TResult Analyze(Mod exp);
        TResult Analyze(Mul exp);
        TResult Analyze(Number exp);
        TResult Analyze(Pow exp);
        TResult Analyze(Root exp);
        TResult Analyze(Round exp);
        TResult Analyze(Simplify exp);
        TResult Analyze(Sqrt exp);
        TResult Analyze(Sub exp);
        TResult Analyze(UnaryMinus exp);
        TResult Analyze(Undefine exp);
        TResult Analyze(UserFunction exp);
        TResult Analyze(Variable exp);
        TResult Analyze(DelegateExpression exp);

        #endregion Standard

        #region Matrix

        TResult Analyze(Vector exp);
        TResult Analyze(Matrix exp);
        TResult Analyze(Determinant exp);
        TResult Analyze(Inverse exp);
        TResult Analyze(Transpose exp);

        #endregion Matrix

        #region Complex Numbers

        TResult Analyze(ComplexNumber exp);
        TResult Analyze(Conjugate exp);
        TResult Analyze(Im exp);
        TResult Analyze(Phase exp);
        TResult Analyze(Re exp);
        TResult Analyze(Reciprocal exp);

        #endregion Complex Numbers

        #region Trigonometric

        TResult Analyze(Arccos exp);
        TResult Analyze(Arccot exp);
        TResult Analyze(Arccsc exp);
        TResult Analyze(Arcsec exp);
        TResult Analyze(Arcsin exp);
        TResult Analyze(Arctan exp);
        TResult Analyze(Cos exp);
        TResult Analyze(Cot exp);
        TResult Analyze(Csc exp);
        TResult Analyze(Sec exp);
        TResult Analyze(Sin exp);
        TResult Analyze(Tan exp);

        #endregion

        #region Hyperbolic

        TResult Analyze(Arcosh exp);
        TResult Analyze(Arcoth exp);
        TResult Analyze(Arcsch exp);
        TResult Analyze(Arsech exp);
        TResult Analyze(Arsinh exp);
        TResult Analyze(Artanh exp);
        TResult Analyze(Cosh exp);
        TResult Analyze(Coth exp);
        TResult Analyze(Csch exp);
        TResult Analyze(Sech exp);
        TResult Analyze(Sinh exp);
        TResult Analyze(Tanh exp);

        #endregion Hyperbolic

        #region Statistical

        TResult Analyze(Avg exp);
        TResult Analyze(Max exp);
        TResult Analyze(Min exp);
        TResult Analyze(Product exp);
        TResult Analyze(Sum exp);

        #endregion Statistical

        #region Logical and Bitwise

        TResult Analyze(Expressions.LogicalAndBitwise.And exp);
        TResult Analyze(Bool exp);
        TResult Analyze(Equality exp);
        TResult Analyze(Implication exp);
        TResult Analyze(NAnd exp);
        TResult Analyze(NOr exp);
        TResult Analyze(Not exp);
        TResult Analyze(Expressions.LogicalAndBitwise.Or exp);
        TResult Analyze(XOr exp);

        #endregion Logical and Bitwise

        #region Programming

        TResult Analyze(AddAssign exp);
        TResult Analyze(Expressions.Programming.And exp);
        TResult Analyze(Dec exp);
        TResult Analyze(DivAssign exp);
        TResult Analyze(Equal exp);
        TResult Analyze(For exp);
        TResult Analyze(GreaterOrEqual exp);
        TResult Analyze(GreaterThan exp);
        TResult Analyze(If exp);
        TResult Analyze(Inc exp);
        TResult Analyze(LessOrEqual exp);
        TResult Analyze(LessThan exp);
        TResult Analyze(MulAssign exp);
        TResult Analyze(NotEqual exp);
        TResult Analyze(Expressions.Programming.Or exp);
        TResult Analyze(SubAssign exp);
        TResult Analyze(While exp);

        #endregion Programming

    }

}
