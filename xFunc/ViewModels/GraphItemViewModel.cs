// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Windows.Media;
using xFunc.Maths.Expressions;

namespace xFunc.ViewModels
{

    public class GraphItemViewModel : BaseViewModel
    {

        private bool isChecked;
        private DrawingVisual visual;
        private Color color;

        public GraphItemViewModel(IExpression exp, bool isChecked, DrawingVisual visual, Color color)
        {
            this.Expression = exp;
            this.isChecked = isChecked;
            this.visual = visual;
            this.color = color;
        }

        public override string ToString()
        {
            return Expression.ToString();
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
                OnPropertyChanged("IsChecked");
            }
        }

        public IExpression Expression { get; }

        public DrawingVisual Visual
        {
            get
            {
                return visual;
            }
            set
            {
                visual = value;
                OnPropertyChanged(nameof(Visual));
            }
        }

        public Color ChartColor
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
                OnPropertyChanged(nameof(ChartColor));
            }
        }

    }

}