// Copyright 2012-2020 Dmytro Kyshchenko
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
using xFunc.ViewModels;

namespace xFunc.Presenters
{
    public class TruthTablePresenter
    {
        private readonly Parser parser;

        private List<TruthTableRowViewModel> table;

        public TruthTablePresenter()
        {
            parser = new Parser();
        }

        private void SetBits(int bits, int parametersCount)
        {
            for (int i = 0; i < parametersCount; i++)
                Parameters[i] = ((bits >> i) & 1) == 1;
        }

        public void Generate(string strExp)
        {
            Expression = parser.Parse(strExp);
            Expressions = Helpers.ConvertExpressionToCollection(Expression);
            Parameters = Helpers.GetParameters(Expression);
            table = new List<TruthTableRowViewModel>();

            var parametersCount = Parameters.Count();
            for (int i = (int)Math.Pow(2, parametersCount) - 1; i >= 0; i--)
            {
                SetBits(i, parametersCount);

                var b = (bool)Expression.Execute(Parameters);

                var row = new TruthTableRowViewModel(parametersCount, Expressions.Count())
                {
                    Index = (int)Math.Pow(2, parametersCount) - i
                };

                for (int j = 0; j < parametersCount; j++)
                    row.VarsValues[j] = (bool)Parameters[Parameters.ElementAt(j).Key];

                for (int j = 0; j < Expressions.Count() - 1; j++)
                    row.Values[j] = (bool)Expressions.ElementAt(j).Execute(Parameters);

                if (Expressions.Count() != 0)
                    row.Result = b;

                table.Add(row);
            }
        }

        public IExpression Expression { get; private set; }

        public IEnumerable<IExpression> Expressions { get; private set; }

        public ParameterCollection Parameters { get; private set; }

        public IEnumerable<TruthTableRowViewModel> Table => table;
    }
}