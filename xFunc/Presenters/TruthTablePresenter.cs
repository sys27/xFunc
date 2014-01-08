// Copyright 2012-2014 Dmitry Kischenko
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
using xFunc.ViewModels;
using xFunc.Logics;
using xFunc.Logics.Expressions;

namespace xFunc.Presenters
{

    public class TruthTablePresenter
    {

        private LogicParser parser;
        private ILogicExpression expression;
        private IEnumerable<ILogicExpression> expressions;
        private LogicParameterCollection parameters;
        private List<TruthTableRowViewModel> table;

        public TruthTablePresenter()
        {
            parser = new LogicParser();
        }

        public void Generate(string strExp)
        {
            expression = parser.Parse(strExp);
            expressions = parser.ConvertLogicExpressionToCollection(expression);
            parameters = parser.GetLogicParameters(strExp);
            table = new List<TruthTableRowViewModel>();

            for (int i = (int)Math.Pow(2, parameters.Count) - 1; i >= 0; i--)
            {
                parameters.Bits = i;
                bool b = expression.Calculate(parameters);

                var row = new TruthTableRowViewModel(parameters.Count, expressions.Count());

                row.Index = (int)Math.Pow(2, parameters.Count) - i;
                for (int j = 0; j < parameters.Count; j++)
                {
                    row.VarsValues[j] = parameters[parameters[j]];
                }

                for (int j = 0; j < expressions.Count() - 1; j++)
                {
                    row.Values[j] = expressions.ElementAt(j).Calculate(parameters);
                }

                if (expressions.Count() != 0)
                    row.Result = b;

                table.Add(row);
            }
        }

        public ILogicExpression Expression
        {
            get
            {
                return expression;
            }
        }

        public IEnumerable<ILogicExpression> Expressions
        {
            get
            {
                return expressions;
            }
        }

        public LogicParameterCollection Parameters
        {
            get
            {
                return parameters;
            }
        }

        public IEnumerable<TruthTableRowViewModel> Table
        {
            get
            {
                return table;
            }
        }

    }

}
