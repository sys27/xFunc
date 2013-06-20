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

    public class Derivative : IMathExpression
    {

        private IMathExpression parentMathExpression;
        private IMathExpression firstMathExpression;
        private Variable variable;

        public Derivative() { }

        public Derivative(IMathExpression firstMathExpression, Variable variable) { }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if (obj == null || GetType() != obj.GetType())
                return false;

            Derivative exp = obj as Derivative;
            return firstMathExpression.Equals(exp.FirstMathExpression) && variable.Equals(exp.Variable);
        }

        public override string ToString()
        {
            return string.Format("deriv({0}, {1})", firstMathExpression, variable);
        }

        public double Calculate()
        {
            return Differentiate().Calculate();
        }

        public double Calculate(MathParameterCollection parameters)
        {
            return Differentiate().Calculate(parameters);
        }

        public double Calculate(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            return Differentiate().Calculate(parameters, functions);
        }

        public IMathExpression Differentiate()
        {
            if (firstMathExpression is Derivative)
                return firstMathExpression.Differentiate(variable).Differentiate(variable);

            return firstMathExpression.Differentiate(variable);
        }

        // The local "variable" is ignored.
        public IMathExpression Differentiate(Variable variable)
        {
            if (firstMathExpression is Derivative)
                return firstMathExpression.Differentiate(this.variable).Differentiate(this.variable);

            return firstMathExpression.Differentiate(this.variable);
        }

        public IMathExpression Clone()
        {
            return new Derivative(firstMathExpression.Clone(), (Variable)variable.Clone());
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

        public Variable Variable
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
