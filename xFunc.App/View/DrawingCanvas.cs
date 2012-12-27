using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace xFunc.App.View
{
    
    public class DrawingCanvas : Canvas
    {

        private List<Visual> visuals = new List<Visual>();

        public DrawingCanvas()
        {

        }

        protected override Visual GetVisualChild(int index)
        {
            return visuals[index];
        }

        public void AddVisual(Visual visual)
        {
            visuals.Add(visual);
            this.AddVisualChild(visual);
            this.AddLogicalChild(visual);
        }

        public void DeleteVisual(Visual visual)
        {
            visuals.Remove(visual);
            this.RemoveVisualChild(visual);
            this.RemoveLogicalChild(visual);
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return visuals.Count;
            }
        }

    }

}
