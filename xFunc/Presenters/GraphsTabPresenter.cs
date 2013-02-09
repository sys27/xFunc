using System;
using System.Collections.Generic;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Properties;
using xFunc.Views;

namespace xFunc.Presenters
{

    public class GraphsTabPresenter
    {

        private IMainView view;

        private MathParser parser;
        private int countOfGraphs;
        private List<IMathExpression> listOfGraphs;

        public GraphsTabPresenter(IMainView view)
        {
            this.view = view;

            parser = new MathParser { AngleMeasurement = AngleMeasurement.Radian };
            countOfGraphs = Settings.Default.MaxCountOfExpressions;
            listOfGraphs = new List<IMathExpression>(countOfGraphs);
        }

        public void Add(string strExp)
        {
            listOfGraphs.Add(parser.Parse(strExp));

            while (listOfGraphs.Count + 1 > countOfGraphs)
                listOfGraphs.RemoveAt(0);

            view.Graphs = listOfGraphs.AsReadOnly();
        }

        public void Remove(IMathExpression exp)
        {
            listOfGraphs.Remove(exp);

            view.Graphs = listOfGraphs.AsReadOnly();
        }

    }

}
