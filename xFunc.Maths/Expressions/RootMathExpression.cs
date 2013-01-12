// Copyright 2012 Dmitry Kischenko
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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.Maths.Expressions
{

    public class RootMathExpression : BinaryMathExpression
    {

        public RootMathExpression() : base(null, null) { }

        public RootMathExpression(IMathExpression firstOperand, IMathExpression secondOperand) : base(firstOperand, secondOperand) { }

        public override string ToString()
        {
            return ToString("root({0}, {1})");
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return Math.Pow(firstMathExpression.Calculate(parameters), 1 / secondMathExpression.Calculate(parameters));
        }

        public override IMathExpression Derivative(VariableMathExpression variable)
        {
            if (MathParser.HasVar(firstMathExpression, variable) || MathParser.HasVar(secondMathExpression, variable))
            {
                DivisionMathExpression div = new DivisionMathExpression(new NumberMathExpression(1), secondMathExpression.Clone());
                ExponentiationMathExpression inv = new ExponentiationMathExpression(firstMathExpression.Clone(), div);

                return inv.Derivative(variable);
            }
            else
            {
                return new NumberMathExpression(0);
            }
        }

        public override IMathExpression Clone()
        {
            return new RootMathExpression(firstMathExpression.Clone(), secondMathExpression.Clone());
        }

    }

}
