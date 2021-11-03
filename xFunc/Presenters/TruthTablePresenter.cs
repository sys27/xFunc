// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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

        private void SetBits(int bits)
        {
            var i = 0;
            foreach (var param in Parameters)
            {
                Parameters[param.Key] = ((bits >> i) & 1) == 1;

                i++;
            }
        }

        public void Generate(string strExp)
        {
            Expression = parser.Parse(strExp);
            Expressions = Helpers.ConvertExpressionToCollection(Expression);
            Parameters = Helpers.GetParameters(Expression);
            table = new List<TruthTableRowViewModel>();

            var parametersCount = Parameters.Count();
            var expressionCount = Expressions.Count();
            var allBitsSet = (int)Math.Pow(2, parametersCount) - 1;

            for (int i = allBitsSet; i >= 0; i--)
            {
                SetBits(i);

                var result = (bool)Expression.Execute(Parameters);

                var row = new TruthTableRowViewModel(parametersCount, expressionCount)
                {
                    Index = allBitsSet - i + 1
                };

                for (int j = 0; j < parametersCount; j++)
                    row.VarsValues[j] = (bool)Parameters[Parameters.ElementAt(j).Key].Value;

                for (int j = 0; j < expressionCount - 1; j++)
                    row.Values[j] = (bool)Expressions.ElementAt(j).Execute(Parameters);

                if (expressionCount != 0)
                    row.Result = result;

                table.Add(row);
            }
        }

        public IExpression Expression { get; private set; }

        public IEnumerable<IExpression> Expressions { get; private set; }

        public ParameterCollection Parameters { get; private set; }

        public IEnumerable<TruthTableRowViewModel> Table => table;
    }
}