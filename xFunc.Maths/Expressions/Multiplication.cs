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
    
    public class Multiplication : BinaryMathExpression
    {

        public Multiplication() : base(null, null) { }

        public Multiplication(IMathExpression firstOperand, IMathExpression secondOperand) : base(firstOperand, secondOperand) { }

        public override string ToString()
        {
            if (parentMathExpression is BinaryMathExpression)
            {
                return ToString("({0} * {1})");
            }

            return ToString("{0} * {1}");
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return firstMathExpression.Calculate(parameters) * secondMathExpression.Calculate(parameters);
        }

        public override IMathExpression Differentiate(Variable variable)
        {
            var first = MathParser.HasVar(firstMathExpression, variable);
            var second = MathParser.HasVar(secondMathExpression, variable);

            if (first && second)
            {
                Multiplication mul1 = new Multiplication(firstMathExpression.Clone().Differentiate(variable), secondMathExpression.Clone());
                Multiplication mul2 = new Multiplication(firstMathExpression.Clone(), secondMathExpression.Clone().Differentiate(variable));
                Addition add = new Addition(mul1, mul2);

                return add;
            }
            if (first)
            {
                return new Multiplication(firstMathExpression.Clone().Differentiate(variable), secondMathExpression.Clone());
            }
            if (second)
            {
                return new Multiplication(firstMathExpression.Clone(), secondMathExpression.Clone().Differentiate(variable));
            }

            return new Number(0);
        }

        public override IMathExpression Clone()
        {
            return new Multiplication(firstMathExpression.Clone(), secondMathExpression.Clone());
        }

    }

}
