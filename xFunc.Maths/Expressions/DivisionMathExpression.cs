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

    public class DivisionMathExpression : BinaryMathExpression
    {

        public DivisionMathExpression() : base(null, null) { }

        public DivisionMathExpression(IMathExpression firstOperand, IMathExpression secondOperand) : base(firstOperand, secondOperand) { }

        public override string ToString()
        {
            if (parentMathExpression != null && parentMathExpression is BinaryMathExpression)
            {
                return ToString("({0} / {1})");
            }

            return ToString("{0} / {1}");
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return firstMathExpression.Calculate(parameters) / secondMathExpression.Calculate(parameters);
        }

        public override IMathExpression Derivative(VariableMathExpression variable)
        {
            var first = MathParser.HasVar(firstMathExpression, variable);
            var second = MathParser.HasVar(secondMathExpression, variable);

            if (first && second)
            {
                MultiplicationMathExpression mul1 = new MultiplicationMathExpression(firstMathExpression.Clone().Derivative(variable), secondMathExpression.Clone());
                MultiplicationMathExpression mul2 = new MultiplicationMathExpression(firstMathExpression.Clone(), secondMathExpression.Clone().Derivative(variable));
                SubtractionMathExpression sub = new SubtractionMathExpression(mul1, mul2);
                ExponentiationMathExpression inv = new ExponentiationMathExpression(secondMathExpression.Clone(), new NumberMathExpression(2));
                DivisionMathExpression division = new DivisionMathExpression(sub, inv);

                return division;
            }
            else if (first)
            {
                return new DivisionMathExpression(firstMathExpression.Clone().Derivative(variable), secondMathExpression.Clone());
            }
            else if (second)
            {
                MultiplicationMathExpression mul2 = new MultiplicationMathExpression(firstMathExpression.Clone(), secondMathExpression.Clone().Derivative(variable));
                UnaryMinusMathExpression unMinus = new UnaryMinusMathExpression(mul2);
                ExponentiationMathExpression inv = new ExponentiationMathExpression(secondMathExpression.Clone(), new NumberMathExpression(2));
                DivisionMathExpression division = new DivisionMathExpression(unMinus, inv);

                return division;
            }
            else
            {
                return new NumberMathExpression(0);
            }
        }

        public override IMathExpression Clone()
        {
            return new DivisionMathExpression(firstMathExpression.Clone(), secondMathExpression.Clone());
        }

    }

}
