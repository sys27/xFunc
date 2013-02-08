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

    public class AbsoluteMathExpression : UnaryMathExpression
    {

        public AbsoluteMathExpression() : base(null) { }

        public AbsoluteMathExpression(IMathExpression expression) : base(expression) { }

        public override string ToString()
        {
            return ToString("abs({0})");
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return Math.Abs(firstMathExpression.Calculate(parameters));
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            DivisionMathExpression div = new DivisionMathExpression(firstMathExpression.Clone(), this.Clone());
            MultiplicationMathExpression mul = new MultiplicationMathExpression(firstMathExpression.Clone().Derivative(variable), div);

            return mul;
        }

        public override IMathExpression Clone()
        {
            return new AbsoluteMathExpression(firstMathExpression.Clone());
        }

    }

}
