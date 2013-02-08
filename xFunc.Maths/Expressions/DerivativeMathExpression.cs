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

    public class DerivativeMathExpression : IMathExpression
    {

        private IMathExpression parentMathExpression;
        private IMathExpression firstMathExpression;
        private VariableMathExpression variable;

        public DerivativeMathExpression() { }

        public DerivativeMathExpression(IMathExpression firstMathExpression, VariableMathExpression variable) { }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if (obj == null || GetType() != obj.GetType())
                return false;

            DerivativeMathExpression exp = obj as DerivativeMathExpression;
            return firstMathExpression.Equals(exp.FirstMathExpression) && variable.Equals(exp.Variable);
        }

        public override string ToString()
        {
            return string.Format("deriv({0}, {1})", firstMathExpression, variable);
        }

        public double Calculate(MathParameterCollection parameters)
        {
            return Derivative().Calculate(parameters);
        }

        public IMathExpression Derivative()
        {
            if (firstMathExpression is DerivativeMathExpression)
                return MathParser.SimplifyExpressions(firstMathExpression.Derivative(variable).Derivative(variable));

            return MathParser.SimplifyExpressions(firstMathExpression.Derivative(variable));
        }

        // The local "variable" is ignored.
        public IMathExpression Derivative(VariableMathExpression variable)
        {
            if (firstMathExpression is DerivativeMathExpression)
                return MathParser.SimplifyExpressions(firstMathExpression.Derivative(this.variable).Derivative(this.variable));

            return MathParser.SimplifyExpressions(firstMathExpression.Derivative(this.variable));
        }

        public IMathExpression Clone()
        {
            return new DerivativeMathExpression(firstMathExpression.Clone(), (VariableMathExpression)variable.Clone());
        }

        public IMathExpression FirstMathExpression
        {
            get
            {
                return firstMathExpression;
            }
            set
            {
                firstMathExpression = value;
                if (firstMathExpression != null)
                    firstMathExpression.Parent = this;
            }
        }

        public VariableMathExpression Variable
        {
            get
            {
                return variable;
            }
            set
            {
                variable = value;
                if (variable != null)
                    variable.Parent = this;
            }
        }

        public IMathExpression Parent
        {
            get
            {
                return parentMathExpression;
            }
            set
            {
                parentMathExpression = value;
            }
        }

    }

}
