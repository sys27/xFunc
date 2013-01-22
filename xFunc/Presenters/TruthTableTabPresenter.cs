using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xFunc.ViewModel;
using xFunc.Views;
using xFunc.Logics;
using xFunc.Logics.Expressions;

namespace xFunc.Presenters
{

    public class TruthTableTabPresenter
    {

        private IMainView view;

        private LogicParser parser;
        private ILogicExpression expression;
        private IEnumerable<ILogicExpression> expressions;
        private LogicParameterCollection parameters;
        private List<TruthTableRowViewModel> table;

        public TruthTableTabPresenter(IMainView view)
        {
            this.view = view;

            this.parser = new LogicParser();
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

                TruthTableRowViewModel row = new TruthTableRowViewModel(parameters.Count, expressions.Count());

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

        public List<TruthTableRowViewModel> Table
        {
            get
            {
                return table;
            }
        }

    }

}
