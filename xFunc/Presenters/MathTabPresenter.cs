using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Views;

namespace xFunc.Presenters
{
  
    public class MathTabPresenter
    {

        private IMainView view;

        private MathWorkspace workspace;

        public MathTabPresenter(IMainView view)
        {
            this.view = view;

            // TODO: add settings...
            this.workspace = new MathWorkspace();
        }

        public void Add(string strExp)
        {
            workspace.Add(strExp);

            view.MathExpressions = workspace.Expressions;
        }

        public void Remove(MathWorkspaceItem item)
        {
            workspace.Remove(item);

            view.MathExpressions = workspace.Expressions;
        }

        public MathWorkspace Workspace
        {
            get
            {
                return workspace;
            }
        }

        public AngleMeasurement AngleMeasurement
        {
            get
            {
                return workspace.Parser.AngleMeasurement;
            }
            set
            {
                workspace.Parser.AngleMeasurement = value;
            }
        }

    }

}
