using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xFunc.Maths.Expressions;

namespace xFunc.ViewModels
{

    public class GraphItemViewModel
    {

        private bool isChecked;
        private IMathExpression exp;

        public GraphItemViewModel(IMathExpression exp, bool isChecked)
        {
            this.exp = exp;
            this.isChecked = isChecked;
        }

        public override string ToString()
        {
            return exp.ToString();
        }

        public bool IsChecked
        {
            get
            {
                return isChecked;
            }
            set
            {
                isChecked = value;
            }
        }

        public IMathExpression Expression
        {
            get
            {
                return exp;
            }
        }

    }

}
