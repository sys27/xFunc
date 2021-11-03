// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace xFunc.Views
{

    public class DrawingCanvas : Canvas
    {

        private readonly List<Visual> visuals = new List<Visual>();

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

        public void ClearVisuals()
        {
            foreach (var visual in visuals)
            {
                RemoveVisualChild(visual);
                RemoveLogicalChild(visual);
            }
            visuals.Clear();
        }

        protected override int VisualChildrenCount => visuals.Count;

    }

}