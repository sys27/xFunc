using System;
using System.Collections.Generic;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Views;

namespace xFunc.Presenters
{

    public class GraphsTabPresenter
    {

        private IMainView view;

        private MathParser parser;
        private List<IMathExpression> listOfGraphs;

        public GraphsTabPresenter(IMainView view)
        {
            this.view = view;

            parser = new MathParser { AngleMeasurement = AngleMeasurement.Radian };
            listOfGraphs = new List<IMathExpression>();
        }

        public void Add(string strExp)
        {
            listOfGraphs.Add(parser.Parse(strExp));

            view.Graphs = listOfGraphs.AsReadOnly();
        }

        public void Remove(IMathExpression exp)
        {
            listOfGraphs.Remove(exp);

            view.Graphs = listOfGraphs.AsReadOnly();
        }

    }

}
