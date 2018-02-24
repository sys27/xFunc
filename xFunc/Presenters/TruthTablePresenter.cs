// Copyright 2012-2017 Dmitry Kischenko
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
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Tokenization;
using xFunc.ViewModels;

namespace xFunc.Presenters
{

    public class TruthTablePresenter
    {

        private Lexer lexer;
        private Parser parser;
        private IExpression expression;
        private IEnumerable<IExpression> expressions;
        private ParameterCollection parameters;
        private List<TruthTableRowViewModel> table;

        public TruthTablePresenter()
        {
            parser = new Parser();
        }

        private void SetBits(int bits, int parametersCount)
        {
            for (int i = 0; i < parametersCount; i++)
                parameters[i] = ((bits >> i) & 1) == 1 ? true : false;
        }

        public void Generate(string strExp)
        {
            lexer = new Lexer();

            var tokens = lexer.Tokenize(strExp);

            expression = parser.Parse(tokens);

            expressions = Helpers.ConvertExpressionToCollection(expression);
            parameters = Helpers.GetParameters(tokens);
            table = new List<TruthTableRowViewModel>();

            var parametersCount = parameters.Count();
            for (int i = (int)Math.Pow(2, parametersCount) - 1; i >= 0; i--)
            {
                SetBits(i, parametersCount);

                var b = (bool)expression.Execute(parameters);

                var row = new TruthTableRowViewModel(parametersCount, expressions.Count())
                {
                    Index = (int)Math.Pow(2, parametersCount) - i
                };

                for (int j = 0; j < parametersCount; j++)
                    row.VarsValues[j] = (bool)parameters[parameters.ElementAt(j).Key];

                for (int j = 0; j < expressions.Count() - 1; j++)
                    row.Values[j] = (bool)expressions.ElementAt(j).Execute(parameters);

                if (expressions.Count() != 0)
                    row.Result = b;

                table.Add(row);
            }
        }

        public IExpression Expression => expression;

        public IEnumerable<IExpression> Expressions => expressions;

        public ParameterCollection Parameters => parameters;

        public IEnumerable<TruthTableRowViewModel> Table => table;

    }

}
