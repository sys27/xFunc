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

    public class Root : BinaryMathExpression
    {

        public Root() : base(null, null) { }

        public Root(IMathExpression firstOperand, IMathExpression secondOperand) : base(firstOperand, secondOperand) { }

        public override string ToString()
        {
            return ToString("root({0}, {1})");
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return Math.Pow(firstMathExpression.Calculate(parameters), 1 / secondMathExpression.Calculate(parameters));
        }

        public override IMathExpression Differentiate(Variable variable)
        {
            if (MathParser.HasVar(firstMathExpression, variable) || MathParser.HasVar(secondMathExpression, variable))
            {
                Division div = new Division(new Number(1), secondMathExpression.Clone());
                Exponentiation inv = new Exponentiation(firstMathExpression.Clone(), div);

                return inv.Differentiate(variable);
            }
            
            return new Number(0);
        }

        public override IMathExpression Clone()
        {
            return new Root(firstMathExpression.Clone(), secondMathExpression.Clone());
        }

    }

}
