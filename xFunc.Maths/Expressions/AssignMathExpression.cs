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

namespace xFunc.Maths.Expressions
{

    public class AssignMathExpression : IMathExpression
    {

        private VariableMathExpression variable;
        private IMathExpression value;

        public AssignMathExpression()
            : this(null, null)
        {

        }

        public AssignMathExpression(VariableMathExpression variable, IMathExpression value)
        {
            this.variable = variable;
            this.value = value;
        }

        public double Calculate(MathParameterCollection parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException("parameters");

            parameters[variable.Variable] = value.Calculate(parameters);

            return double.NaN;
        }

        public IMathExpression Derivative()
        {
            throw new NotSupportedException();
        }

        public IMathExpression Derivative(VariableMathExpression variable)
        {
            throw new NotSupportedException();
        }

        public IMathExpression Clone()
        {
            return new AssignMathExpression((VariableMathExpression)variable.Clone(), value.Clone());
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
            }
        }

        public IMathExpression Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }

        public IMathExpression Parent
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

    }

}
