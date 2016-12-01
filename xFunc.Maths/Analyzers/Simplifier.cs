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

    public class Simplifier : IAnalyzer<IExpression>
    {

        private readonly Number zero = 0;
        private readonly Number one = 1;

        public Simplifier() { }

        private IExpression AnalyzeUnary(UnaryExpression exp)
        {
            exp.Argument = exp.Argument.Analyze(this);

            return exp;
        }

        private IExpression AnalyzeBinary(BinaryExpression exp)
        {
            exp.Left = exp.Left.Analyze(this);
            exp.Right = exp.Right.Analyze(this);

            return exp;
        }

        private IExpression AnalyzeTrig(UnaryExpression exp)
        {
            exp.Argument = exp.Argument.Analyze(this);

            var attrs = exp.GetType().GetCustomAttributes(typeof(ReverseFunctionAttribute), false);
            if (attrs.Length > 0)
            {
                var attr = (ReverseFunctionAttribute)attrs[0];

                if (exp.Argument.GetType() == attr.ReverseType)
                    return ((UnaryExpression)exp.Argument).Argument;
            }

            return exp;
        }

        #region Standard

        public IExpression Analyze(Abs exp)
        {
            return exp;
        }

        public IExpression Analyze(Add exp)
        {
            return exp;
        }

        public IExpression Analyze(Ceil exp)
        {
            return exp;
        }

        public IExpression Analyze(Define exp)
        {
            return exp;
        }

        public IExpression Analyze(Del exp)
        {
            return exp;
        }

        public IExpression Analyze(Derivative exp)
        {
            return exp;
        }

        public IExpression Analyze(Div exp)
        {
            return exp;
        }

        public IExpression Analyze(Exp exp)
        {
            return exp;
        }

        public IExpression Analyze(Fact exp)
        {
            return exp;
        }

        public IExpression Analyze(Floor exp)
        {
            return exp;
        }

        public IExpression Analyze(GCD exp)
        {
            return exp;
        }

        public IExpression Analyze(Lb exp)
        {
            return exp;
        }

        public IExpression Analyze(LCM exp)
        {
            return exp;
        }

        public IExpression Analyze(Lg exp)
        {
            // lg(10)
            if (exp.Argument.Equals(new Number(10)))
                return one;

            return exp;
        }

        public IExpression Analyze(Ln exp)
        {
            // ln(e)
            if (exp.Argument.Equals(new Variable("e")))
                return one;

            return exp;
        }

        public IExpression Analyze(Log exp)
        {
            // log(4x, 4x)
            if (exp.Left.Equals(exp.Right))
                return one;

            return exp;
        }

        public IExpression Analyze(Mod exp)
        {
            return exp;
        }

        public IExpression Analyze(Mul exp)
        {
            return exp;
        }

        public IExpression Analyze(Number exp)
        {
            return exp;
        }

        public IExpression Analyze(Pow exp)
        {
            // x^0
            if (exp.Right.Equals(zero))
                return one;
            // x^1
            if (exp.Right.Equals(one))
                return exp.Left;

            return exp;
        }

        public IExpression Analyze(Root exp)
        {
            // root(x, 1)
            if (exp.Right.Equals(one))
                return exp.Left;

            return exp;
        }

        public IExpression Analyze(Round exp)
        {
            return exp;
        }

        public IExpression Analyze(Simplify exp)
        {
            return exp;
        }

        public IExpression Analyze(Sqrt exp)
        {
            return exp;
        }

        public IExpression Analyze(Sub exp)
        {
            return exp;
        }

        public IExpression Analyze(UnaryMinus exp)
        {
            // -(-x)
            if (exp.Argument is UnaryMinus)
                return (exp.Argument as UnaryMinus).Argument;
            // -1
            if (exp.Argument is Number)
            {
                var number = exp.Argument as Number;
                number.Value = -number.Value;

                return number;
            }

            return exp;
        }

        public IExpression Analyze(Undefine exp)
        {
            return exp;
        }

        public IExpression Analyze(UserFunction exp)
        {
            return exp;
        }

        public IExpression Analyze(Variable exp)
        {
            return exp;
        }

        public IExpression Analyze(DelegateExpression exp)
        {
            return exp;
        }

        #endregion Standard

        #region Matrix

        public IExpression Analyze(Vector exp)
        {
            return exp;
        }

        public IExpression Analyze(Matrix exp)
        {
            return exp;
        }

        public IExpression Analyze(Determinant exp)
        {
            return exp;
        }

        public IExpression Analyze(Inverse exp)
        {
            return exp;
        }

        public IExpression Analyze(Transpose exp)
        {
            return exp;
        }

        #endregion Matrix

        #region Complex Numbers

        public IExpression Analyze(ComplexNumber exp)
        {
            return exp;
        }

        public IExpression Analyze(Conjugate exp)
        {
            return exp;
        }

        public IExpression Analyze(Im exp)
        {
            return exp;
        }

        public IExpression Analyze(Phase exp)
        {
            return exp;
        }

        public IExpression Analyze(Re exp)
        {
            return exp;
        }

        public IExpression Analyze(Reciprocal exp)
        {
            return exp;
        }


        #endregion Complex Numbers

        #region Trigonometric

        public IExpression Analyze(Arccos exp)
        {
            return AnalyzeTrig(exp);
        }

        public IExpression Analyze(Arccot exp)
        {
            return AnalyzeTrig(exp);
        }

        public IExpression Analyze(Arccsc exp)
        {
            return AnalyzeTrig(exp);
        }

        public IExpression Analyze(Arcsec exp)
        {
            return AnalyzeTrig(exp);
        }

        public IExpression Analyze(Arcsin exp)
        {
            return AnalyzeTrig(exp);
        }

        public IExpression Analyze(Arctan exp)
        {
            return AnalyzeTrig(exp);
        }

        public IExpression Analyze(Cos exp)
        {
            return AnalyzeTrig(exp);
        }

        public IExpression Analyze(Cot exp)
        {
            return AnalyzeTrig(exp);
        }

        public IExpression Analyze(Csc exp)
        {
            return AnalyzeTrig(exp);
        }

        public IExpression Analyze(Sec exp)
        {
            return AnalyzeTrig(exp);
        }

        public IExpression Analyze(Sin exp)
        {
            return AnalyzeTrig(exp);
        }

        public IExpression Analyze(Tan exp)
        {
            return AnalyzeTrig(exp);
        }

        #endregion

        #region Hyperbolic

        public IExpression Analyze(Arcosh exp)
        {
            return AnalyzeTrig(exp);
        }

        public IExpression Analyze(Arcoth exp)
        {
            return AnalyzeTrig(exp);
        }

        public IExpression Analyze(Arcsch exp)
        {
            return AnalyzeTrig(exp);
        }

        public IExpression Analyze(Arsech exp)
        {
            return AnalyzeTrig(exp);
        }

        public IExpression Analyze(Arsinh exp)
        {
            return AnalyzeTrig(exp);
        }

        public IExpression Analyze(Artanh exp)
        {
            return AnalyzeTrig(exp);
        }

        public IExpression Analyze(Cosh exp)
        {
            return AnalyzeTrig(exp);
        }

        public IExpression Analyze(Coth exp)
        {
            return AnalyzeTrig(exp);
        }

        public IExpression Analyze(Csch exp)
        {
            return AnalyzeTrig(exp);
        }

        public IExpression Analyze(Sech exp)
        {
            return AnalyzeTrig(exp);
        }

        public IExpression Analyze(Sinh exp)
        {
            return AnalyzeTrig(exp);
        }

        public IExpression Analyze(Tanh exp)
        {
            return AnalyzeTrig(exp);
        }

        #endregion Hyperbolic

        #region Statistical

        public IExpression Analyze(Avg exp)
        {
            return exp;
        }

        public IExpression Analyze(Max exp)
        {
            return exp;
        }

        public IExpression Analyze(Min exp)
        {
            return exp;
        }

        public IExpression Analyze(Product exp)
        {
            return exp;
        }

        public IExpression Analyze(Sum exp)
        {
            return exp;
        }

        #endregion Statistical

        #region Logical and Bitwise

        public IExpression Analyze(Expressions.LogicalAndBitwise.And exp)
        {
            return exp;
        }

        public IExpression Analyze(Bool exp)
        {
            return exp;
        }

        public IExpression Analyze(Equality exp)
        {
            return exp;
        }

        public IExpression Analyze(Implication exp)
        {
            return exp;
        }

        public IExpression Analyze(NAnd exp)
        {
            return exp;
        }

        public IExpression Analyze(NOr exp)
        {
            return exp;
        }

        public IExpression Analyze(Not exp)
        {
            return exp;
        }

        public IExpression Analyze(Expressions.LogicalAndBitwise.Or exp)
        {
            return exp;
        }

        public IExpression Analyze(XOr exp)
        {
            return exp;
        }

        #endregion Logical and Bitwise

        #region Programming

        public IExpression Analyze(AddAssign exp)
        {
            return exp;
        }

        public IExpression Analyze(Expressions.Programming.And exp)
        {
            return exp;
        }

        public IExpression Analyze(Dec exp)
        {
            return exp;
        }

        public IExpression Analyze(DivAssign exp)
        {
            return exp;
        }

        public IExpression Analyze(Equal exp)
        {
            return exp;
        }

        public IExpression Analyze(For exp)
        {
            return exp;
        }

        public IExpression Analyze(GreaterOrEqual exp)
        {
            return exp;
        }

        public IExpression Analyze(GreaterThan exp)
        {
            return exp;
        }

        public IExpression Analyze(If exp)
        {
            return exp;
        }

        public IExpression Analyze(Inc exp)
        {
            return exp;
        }

        public IExpression Analyze(LessOrEqual exp)
        {
            return exp;
        }

        public IExpression Analyze(LessThan exp)
        {
            return exp;
        }

        public IExpression Analyze(MulAssign exp)
        {
            return exp;
        }

        public IExpression Analyze(NotEqual exp)
        {
            return exp;
        }

        public IExpression Analyze(Expressions.Programming.Or exp)
        {
            return exp;
        }

        public IExpression Analyze(SubAssign exp)
        {
            return exp;
        }

        public IExpression Analyze(While exp)
        {
            return exp;
        }

        #endregion Programming

    }

}
