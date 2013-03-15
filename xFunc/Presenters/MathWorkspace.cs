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
using System.Collections.Generic;
using System.Globalization;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Resources;

namespace xFunc.Presenters
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

        public MathWorkspace(int countOfExps)
        {
            this.countOfExps = countOfExps;
            expressions = new List<MathWorkspaceItem>(countOfExps);
            parser = new MathParser();
            parameters = new MathParameterCollection();
        }

        public void Add(string strExp)
        {
            if (string.IsNullOrWhiteSpace(strExp))
                throw new ArgumentNullException("strExp");

            string[] exps = strExp.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            while (expressions.Count + exps.Length > countOfExps)
                expressions.RemoveAt(0);

            foreach (var s in exps)
            {
                IMathExpression exp = parser.Parse(s);
                MathWorkspaceItem item = new MathWorkspaceItem(s, exp);
                if (exp is Derivative)
                {
                    item.Answer = exp.Differentiate().ToString();
                }
                else if (exp is Assign)
                {
                    Assign assign = exp as Assign;
                    assign.Calculate(parameters);
                    item.Answer = string.Format(Resource.AssignVariable, assign.Variable, assign.Value);
                }
                else
                {
                    item.Answer = exp.Calculate(parameters).ToString(CultureInfo.InvariantCulture);
                }

                expressions.Add(item);
            }
        }

        public void Clear()
        {
            expressions.Clear();
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
