using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xFunc.App.Views;
using xFunc.Library.Logics;
using xFunc.Library.Maths;
using xFunc.Library.Maths.Expressions;

namespace xFunc.App.Presenters
{

    public class MainPresenter
    {

        private IMainView view;

        private MathWorkspace mathWorkspace;
        private LogicWorkspace logicWorkspace;

        public MainPresenter(IMainView view)
        {
            this.view = view;
            this.mathWorkspace = new MathWorkspace();
            this.logicWorkspace = new LogicWorkspace();
        }

        public void SetAngleMeasurement(AngleMeasurement angle)
        {
            mathWorkspace.Parser.AngleMeasurement = angle;
        }

        public void AddMathExpression(string strExp)
        {
            mathWorkspace.Add(strExp);

            view.MathExpressions = mathWorkspace.Expressions;
        }

        public void AddLogicExpression(string strExp)
        {
            logicWorkspace.Add(strExp);

            view.LogicExpressions = logicWorkspace.Expressions;
        }

    }

}
