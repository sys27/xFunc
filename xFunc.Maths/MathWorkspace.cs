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
using xFunc.Maths.Expressions;

namespace xFunc.Maths
{

    public class MathWorkspace
    {

        private MathParser parser;

        private int countOfExps;
        private List<MathWorkspaceItem> expressions;

        private MathParameterCollection parameters;

        public MathWorkspace()
            : this(20)
        {

        }

        public MathWorkspace(int countOfExp)
        {
            this.countOfExps = countOfExp;
            expressions = new List<MathWorkspaceItem>(countOfExp);
            parser = new MathParser();
            parameters = new MathParameterCollection();
        }

        public void Add(string strExp)
        {
            if (string.IsNullOrWhiteSpace(strExp))
                throw new ArgumentNullException("strExp");

            while (expressions.Count >= countOfExps)
                expressions.RemoveAt(0);

            IMathExpression exp = parser.Parse(strExp);
            MathWorkspaceItem item = new MathWorkspaceItem(strExp, exp);
            if (exp is DerivativeMathExpression)
            {
                item.Answer = exp.Derivative().ToString();
            }
            else if (exp is AssignMathExpression)
            {
                AssignMathExpression assign = (AssignMathExpression)exp;
                assign.Calculate(parameters);
                item.Answer = string.Format("The value '{1}' was assigned to the variable '{0}'.", assign.Value, assign.Variable);
            }
            else
            {
                item.Answer = exp.Calculate(parameters).ToString();
            }

            expressions.Add(item);
        }

        public void Remove(MathWorkspaceItem item)
        {
            expressions.Remove(item);
        }

        public void RemoveAt(int index)
        {
            expressions.RemoveAt(index);
        }

        public MathParser Parser
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
            set
            {
                countOfExps = value;
            }
        }

        public IEnumerable<MathWorkspaceItem> Expressions
        {
            get
            {
                return expressions.AsReadOnly();
            }
        }

    }

}
