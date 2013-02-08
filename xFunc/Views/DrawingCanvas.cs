using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace xFunc.Views
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
            AddVisualChild(visual);
            AddLogicalChild(visual);
        }

        public void DeleteVisual(Visual visual)
        {
            visuals.Remove(visual);
            RemoveVisualChild(visual);
            RemoveLogicalChild(visual);
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
