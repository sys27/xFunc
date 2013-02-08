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

    public class ExponentiationMathExpression : BinaryMathExpression
    {

        public ExponentiationMathExpression() : base(null, null) { }

        public ExponentiationMathExpression(IMathExpression firstOperand, IMathExpression secondOperand) : base(firstOperand, secondOperand) { }

        public override string ToString()
        {
            if (parentMathExpression != null && parentMathExpression is BinaryMathExpression)
            {
                return ToString("({0} ^ {1})");
            }

            return ToString("{0} ^ {1}");
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return Math.Pow(firstMathExpression.Calculate(parameters), secondMathExpression.Calculate(parameters));
        }

        public override IMathExpression Derivative(VariableMathExpression variable)
        {
            if (MathParser.HasVar(firstMathExpression, variable))
            {
                SubtractionMathExpression sub = new SubtractionMathExpression(secondMathExpression.Clone(), new NumberMathExpression(1));
                ExponentiationMathExpression inv = new ExponentiationMathExpression(firstMathExpression.Clone(), sub);
                MultiplicationMathExpression mul1 = new MultiplicationMathExpression(secondMathExpression.Clone(), inv);
                MultiplicationMathExpression mul2 = new MultiplicationMathExpression(firstMathExpression.Clone().Derivative(variable), mul1);

                return mul2;
            }
            else if (MathParser.HasVar(secondMathExpression, variable))
            {
                LnMathExpression ln = new LnMathExpression(firstMathExpression.Clone());
                MultiplicationMathExpression mul1 = new MultiplicationMathExpression(ln, this.Clone());
                MultiplicationMathExpression mul2 = new MultiplicationMathExpression(mul1, secondMathExpression.Clone().Derivative(variable));

                return mul2;
            }
            else
            {
                return new NumberMathExpression(0);
            }
        }

        public override IMathExpression Clone()
        {
            return new ExponentiationMathExpression(firstMathExpression.Clone(), secondMathExpression.Clone());
        }

    }

}
