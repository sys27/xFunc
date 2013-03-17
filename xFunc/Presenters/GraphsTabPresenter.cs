using System;
using System.Collections.Generic;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Properties;
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

            parser = new MathParser { AngleMeasurement = AngleMeasurement.Radian };
            countOfGraphs = Settings.Default.MaxCountOfExpressions;
            listOfGraphs = new List<GraphItemViewModel>(countOfGraphs);
        }

        public void Add(string strExp)
        {
            listOfGraphs.Add(new GraphItemViewModel(parser.Parse(strExp), true));

            while (listOfGraphs.Count > countOfGraphs)
                listOfGraphs.RemoveAt(0);

            view.Graphs = listOfGraphs.AsReadOnly();
        }

        public void Remove(GraphItemViewModel exp)
        {
            listOfGraphs.Remove(exp);

            view.Graphs = listOfGraphs.AsReadOnly();
        }

    }

}
