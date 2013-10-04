using System;
using System.Collections.Generic;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Properties;
using xFunc.Resources;
using xFunc.ViewModels;
using xFunc.Views;

namespace xFunc.Presenters
{

    public class GraphsTabPresenter
    {

        private IMainView view;

        private MathParser parser;
        private int countOfGraphs;
        private List<GraphItemViewModel> listOfGraphs;

        public GraphsTabPresenter(IMainView view)
        {
            this.view = view;

            parser = new MathParser();
            countOfGraphs = Settings.Default.MaxCountOfExpressions;
            listOfGraphs = new List<GraphItemViewModel>(countOfGraphs);
        }

        public void Add(string strExp)
        {
            var exp = parser.Parse(strExp);
            if (!MathParser.HasVar(exp, "x"))
                throw new MathParserException(Resource.VariableNotFoundExceptionError);

            listOfGraphs.Add(new GraphItemViewModel(exp, true, null));

            while (listOfGraphs.Count > countOfGraphs)
                listOfGraphs.RemoveAt(0);

            view.Graphs = listOfGraphs.AsReadOnly();
        }

        public void Remove(GraphItemViewModel exp)
        {
            listOfGraphs.Remove(exp);

            view.Graphs = listOfGraphs.AsReadOnly();
        }

        public void Clear()
        {
            listOfGraphs.Clear();

            view.Graphs = listOfGraphs.AsReadOnly();
        }

    }

}
