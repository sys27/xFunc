// Copyright 2012-2013 Dmitry Kischenko
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

namespace xFunc.Maths.Expressions
{

    public class Exponentiation : BinaryMathExpression
    {

        public Exponentiation() : base(null, null) { }

        public Exponentiation(IMathExpression firstOperand, IMathExpression secondOperand) : base(firstOperand, secondOperand) { }

        public override string ToString()
        {
            if (parentMathExpression is BinaryMathExpression)
            {
                return ToString("({0} ^ {1})");
            }

            return ToString("{0} ^ {1}");
        }

        public override double Calculate()
        {
            return Math.Pow(firstMathExpression.Calculate(), secondMathExpression.Calculate());
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return Math.Pow(firstMathExpression.Calculate(parameters), secondMathExpression.Calculate(parameters));
        }

        public override IMathExpression Differentiate(Variable variable)
        {
            if (MathParser.HasVar(firstMathExpression, variable))
            {
                Subtraction sub = new Subtraction(secondMathExpression.Clone(), new Number(1));
                Exponentiation inv = new Exponentiation(firstMathExpression.Clone(), sub);
                Multiplication mul1 = new Multiplication(secondMathExpression.Clone(), inv);
                Multiplication mul2 = new Multiplication(firstMathExpression.Clone().Differentiate(variable), mul1);

                return mul2;
            }
            if (MathParser.HasVar(secondMathExpression, variable))
            {
                Ln ln = new Ln(firstMathExpression.Clone());
                Multiplication mul1 = new Multiplication(ln, Clone());
                Multiplication mul2 = new Multiplication(mul1, secondMathExpression.Clone().Differentiate(variable));

                return mul2;
            }
            
            return new Number(0);
        }

        public override IMathExpression Clone()
        {
            return new Exponentiation(firstMathExpression.Clone(), secondMathExpression.Clone());
        }

    }

}
