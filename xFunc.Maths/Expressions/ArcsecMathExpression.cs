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

    public class ArcsecMathExpression : TrigonometryMathExpression
    {

        public ArcsecMathExpression()
            : base(null)
        {

        }

        public ArcsecMathExpression(IMathExpression firstMathExpression)
            : base(firstMathExpression)
        {

        }

        public override string ToString()
        {
            return ToString("arcsec({0})");
        }

        public override double CalculateDergee(MathParameterCollection parameters)
        {
            return MathExtentions.Asec(firstMathExpression.Calculate(parameters)) / Math.PI * 180;
        }

        public override double CalculateRadian(MathParameterCollection parameters)
        {
            return MathExtentions.Asec(firstMathExpression.Calculate(parameters));
        }

        public override double CalculateGradian(MathParameterCollection parameters)
        {
            return MathExtentions.Asec(firstMathExpression.Calculate(parameters)) / Math.PI * 200;
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            AbsoluteMathExpression abs = new AbsoluteMathExpression(firstMathExpression.Clone());
            ExponentiationMathExpression sqr = new ExponentiationMathExpression(firstMathExpression.Clone(), new NumberMathExpression(2));
            SubtractionMathExpression sub = new SubtractionMathExpression(sqr, new NumberMathExpression(1));
            SqrtMathExpression sqrt = new SqrtMathExpression(sub);
            MultiplicationMathExpression mul = new MultiplicationMathExpression(abs, sqrt);
            DivisionMathExpression div = new DivisionMathExpression(firstMathExpression.Clone().Derivative(variable), mul);

            return div;
        }

        public override IMathExpression Clone()
        {
            return new ArcsecMathExpression(firstMathExpression.Clone());
        }

    }

}
