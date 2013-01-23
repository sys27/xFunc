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
using xFunc.Logics.Expressions;

namespace xFunc.Logics
{

    public class LogicWorkspace
    {

        private LogicParser parser;

        private int countOfExps;
        private List<LogicWorkspaceItem> expressions;

        private LogicParameterCollection parameters;

        public LogicWorkspace()
            : this(20)
        {

        }

        public LogicWorkspace(int countOfExps)
        {
            this.countOfExps = countOfExps;
            this.expressions = new List<LogicWorkspaceItem>(countOfExps);
            this.parser = new LogicParser();
            this.parameters = new LogicParameterCollection();
        }

        public void Add(string strExp)
        {
            if (string.IsNullOrWhiteSpace(strExp))
                throw new ArgumentNullException("strExp");

            while (expressions.Count >= countOfExps)
                expressions.RemoveAt(0);

            ILogicExpression exp = parser.Parse(strExp);
            LogicWorkspaceItem item = new LogicWorkspaceItem(strExp, exp);
            if (exp is AssignLogicExpression)
            {
                AssignLogicExpression assign = (AssignLogicExpression)exp;
                assign.Calculate(parameters);
                item.Answer = string.Format("The value '{1}' was assigned to the variable '{0}'.", assign.Value, assign.Variable);
            }
            else
            {
                item.Answer = exp.Calculate(parameters).ToString();
            }

            expressions.Add(item);
        }

        public void Remove(LogicWorkspaceItem item)
        {
            if (item.Expression is AssignLogicExpression)
            {
                var assign = item.Expression as AssignLogicExpression;

                parameters.Remove(assign.Variable.Character);
            }

            expressions.Remove(item);
        }

        public void RemoveAt(int index)
        {
            var item = expressions[index];
            if (item.Expression is AssignLogicExpression)
            {
                var assign = item.Expression as AssignLogicExpression;

                parameters.Remove(assign.Variable.Character);
            }

            expressions.RemoveAt(index);
        }

        public LogicParser Parser
        {
            get
            {
                return parser;
            }
        }

        public int CountOfExpressions
        {
            get
            {
                return countOfExps;
            }
        }

        public IEnumerable<LogicWorkspaceItem> Expressions
        {
            get
            {
                return expressions.AsReadOnly();
            }
        }

    }

}
