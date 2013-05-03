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
    
    public class Sub : BinaryMathExpression
    {

        public Sub() : base(null, null) { }

        public Sub(IMathExpression firstMathExpression, IMathExpression secondMathExpression) : base(firstMathExpression, secondMathExpression) { }

        public override string ToString()
        {
            if (parentMathExpression is BinaryMathExpression)
            {
                return ToString("({0} - {1})");
            }

            return ToString("{0} - {1}");
        }

        public override double Calculate()
        {
            return firstMathExpression.Calculate() - secondMathExpression.Calculate();
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return firstMathExpression.Calculate(parameters) - secondMathExpression.Calculate(parameters);
        }

        public override double Calculate(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            return firstMathExpression.Calculate(parameters, functions) - secondMathExpression.Calculate(parameters, functions);
        }

        public override IMathExpression Differentiate(Variable variable)
        {
            var first = MathParser.HasVar(firstMathExpression, variable);
            var second = MathParser.HasVar(secondMathExpression, variable);

            if (first && second)
            {
                return new Sub(firstMathExpression.Clone().Differentiate(variable), secondMathExpression.Clone().Differentiate(variable));
            }
            if (first)
            {
                return firstMathExpression.Clone().Differentiate(variable);
            }
            if (second)
            {
                return new UnaryMinus(secondMathExpression.Clone().Differentiate(variable));
            }

            return new Number(0);
        }

        public override IMathExpression Clone()
        {
            return new Sub(firstMathExpression.Clone(), secondMathExpression.Clone());
        }

    }

}
