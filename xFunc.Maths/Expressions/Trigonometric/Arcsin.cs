﻿// Copyright 2012-2013 Dmitry Kischenko
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

namespace xFunc.Maths.Expressions.Trigonometric
{

    public class Arcsin : TrigonometryMathExpression
    {

        public Arcsin() : base(null) { }

        public Arcsin(IMathExpression firstMathExpression) : base(firstMathExpression) { }

        public override string ToString()
        {
            return ToString("arcsin({0})");
        }

        public override double CalculateDergee(MathParameterCollection parameters)
        {
            var radian = firstMathExpression.Calculate(parameters);

            return Math.Asin(radian) / Math.PI * 180;
        }

        public override double CalculateRadian(MathParameterCollection parameters)
        {
            return Math.Asin(firstMathExpression.Calculate(parameters));
        }

        public override double CalculateGradian(MathParameterCollection parameters)
        {
            var radian = firstMathExpression.Calculate(parameters);

            return Math.Asin(radian) / Math.PI * 200;
        }

        protected override IMathExpression _Differentiation(Variable variable)
        {
            Exponentiation involution = new Exponentiation(firstMathExpression.Clone(), new Number(2));
            Subtraction sub = new Subtraction(new Number(1), involution);
            Sqrt sqrt = new Sqrt(sub);
            Division division = new Division(firstMathExpression.Clone().Differentiation(variable), sqrt);

            return division;
        }

        public override IMathExpression Clone()
        {
            return new Arcsin(firstMathExpression.Clone());
        }

    }

}
