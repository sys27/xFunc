// Copyright 2012-2017 Dmitry Kischenko
//
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either 
// express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
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

        public void ClearVisuals()
        {
            foreach (var visual in visuals)
            {
                RemoveVisualChild(visual);
                RemoveLogicalChild(visual);
            }
            visuals.Clear();
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
