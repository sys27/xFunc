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

    public class Log : BinaryMathExpression
    {

        public Log() : base(null, null) { }

        public Log(IMathExpression firstOperand, IMathExpression secondOperand) : base(firstOperand, secondOperand) { }

        public override string ToString()
        {
            return ToString("log({0}, {1})");
        }

        public override double Calculate()
        {
            return Math.Log(firstMathExpression.Calculate(), secondMathExpression.Calculate());
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return Math.Log(firstMathExpression.Calculate(parameters), secondMathExpression.Calculate(parameters));
        }

        public override IMathExpression Differentiate(Variable variable)
        {
            if (MathParser.HasVar(firstMathExpression, variable))
            {
                if (!(secondMathExpression is Number))
                    throw new NotSupportedException();

                Ln ln = new Ln(secondMathExpression.Clone());
                Multiplication mul = new Multiplication(firstMathExpression.Clone(), ln);
                Division div = new Division(firstMathExpression.Clone().Differentiate(variable), mul);

                return div;
            }

            return new Number(0);
        }

        public override IMathExpression Clone()
        {
            return new Log(firstMathExpression.Clone(), secondMathExpression.Clone());
        }

    }

}
