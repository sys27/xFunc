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

namespace xFunc.Logics.Expressions
{

    public class AssignLogicExpression : ILogicExpression
    {

        private VariableLogicExpression variable;
        private ILogicExpression value;

        public AssignLogicExpression()
        {

        }

        public AssignLogicExpression(VariableLogicExpression variable, ILogicExpression value)
        {
            this.variable = variable;
            this.value = value;
        }

        public override string ToString()
        {
            return string.Format("{0} := {1}", variable, value);
        }

        public bool Calculate(LogicParameterCollection parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException("parameters");

            var localValue = value.Calculate(parameters);
            parameters.Add(variable.Character);
            parameters[variable.Character] = localValue;

            return false;
        }

        public VariableLogicExpression Variable
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

        public ILogicExpression Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
            }
        }

    }

}
