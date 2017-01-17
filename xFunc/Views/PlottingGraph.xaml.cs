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
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Resources;
using xFunc.ViewModels;

namespace xFunc.Views
{

    public partial class PlottingGraph : UserControl
    {

        private IEnumerable<GraphItemViewModel> exps;
        private ExpressionParameters parameters;

        private double currentWidth;
        private double currentHeight;
        private double centerX;
        private double centerY;
        private Point startPoint;
        private double cm = 37.795276;

        public PlottingGraph()
        {
            this.parameters = new ExpressionParameters(AngleMeasurement.Radian, new ParameterCollection { { "x", 0 } });

            InitializeComponent();
        }

        private void InitCoords()
        {
            currentWidth = this.ActualWidth;
            currentHeight = this.ActualHeight - this.secondRow.Height.Value;
            centerX = Math.Ceiling(currentWidth / 2);
            centerY = Math.Ceiling(currentHeight / 2);
        }

        private void DrawCurrentCoords()
        {
            var x = Mouse.GetPosition(this).X;
            var y = Mouse.GetPosition(this).Y;
            point.Text = string.Format("x: {0} {2} y: {1} {2}", Math.Round((x - centerX) / cm, 2), Math.Round(-(y - centerY) / cm, 2), Resource.cm);
        }

        private void this_SizeChanged(object o, SizeChangedEventArgs args)
        {
            InitCoords();
            ReRender();
        }

        private void this_MouseLeftButtonDown(object o, MouseButtonEventArgs args)
        {
            startPoint = args.GetPosition(null);
        }

        private void this_MouseDoubleClick(object o, MouseButtonEventArgs args)
        {
            InitCoords();
            slider.Value = 1;
            ReRender();
        }

        private void this_MouseMove(object o, MouseEventArgs args)
        {
            if (args.OriginalSource is DrawingCanvas)
            {
                DrawCurrentCoords();

                if (args.LeftButton == MouseButtonState.Pressed)
                {
                    var chX = startPoint.X - args.GetPosition(null).X;
                    var chY = startPoint.Y - args.GetPosition(null).Y;

                    centerX -= chX;
                    centerY -= chY;

                    ReRender();

                    startPoint = args.GetPosition(null);
                }
            }
            else
            {
                point.Text = string.Empty;
            }
        }

        private void this_MouseWheel(object o, MouseWheelEventArgs args)
        {
            if (args.Delta < 0)
            {
                if (slider.Value != slider.Maximum)
                {
                    if (slider.Value > 1)
                    {
                        slider.Value += 0.5;
                    }
                    else
                    {
                        slider.Value += 0.1;
                    }
                }
            }
            else
            {
                if (slider.Value != slider.Minimum)
                {
                    if (slider.Value > 1)
                    {
                        slider.Value -= 0.5;
                    }
                    else
                    {
                        slider.Value -= 0.1;
                    }
                }
            }
        }

        private void slider_ValueChanged(object o, RoutedPropertyChangedEventArgs<double> args)
        {
            double temp = 40;
            cm = temp / args.NewValue;
            DrawCurrentCoords();

            ReRender();
        }

        private void renderGrid_StateChanged(object sender, RoutedEventArgs e)
        {
            ReRender();
        }

        private void canvas_ManipulationStarting(object sender, ManipulationStartingEventArgs e)
        {
            e.ManipulationContainer = this;
            e.Mode = ManipulationModes.Scale | ManipulationModes.Translate;

            e.Handled = true;
        }

        private void canvas_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            var deltaScale = e.DeltaManipulation.Scale;
            if (deltaScale.X < 1 && deltaScale.Y < 1)
            {
                if (slider.Value != slider.Maximum)
                    slider.Value += slider.Value > 1 ? 0.1 : 0.02;
            }
            else if (deltaScale.X > 1 && deltaScale.Y > 1)
            {
                if (slider.Value != slider.Minimum)
                    slider.Value -= slider.Value > 1 ? 0.1 : 0.02;
            }

            var deltaTranslation = e.DeltaManipulation.Translation;
            if (deltaTranslation.X != 0 && deltaTranslation.Y != 0)
            {
                centerX += deltaTranslation.X;
                centerY += deltaTranslation.Y;

                ReRender();
            }

            e.Handled = true;
        }

        public void ReRender()
        {
            if (!this.IsInitialized)
                return;

            canvas.ClearVisuals();
            DrawGrid();
            DrawOXOY();
            if (exps != null)
            {
                foreach (var exp in exps)
                {
                    if (exp.IsChecked)
                        DrawFunc(exp);
                }
            }
        }

        private void DrawGrid()
        {
            if (renderGrid.IsChecked == true)
            {
                var gridVisual = new DrawingVisual();
                var pen = new Pen(Brushes.Blue, 0.5);
                using (var context = gridVisual.RenderOpen())
                {
                    if (centerX < 0)
                    {
                        for (double x = centerX % cm; x <= currentWidth; x += cm)
                        {
                            if (x >= 0)
                                context.DrawLine(pen, new Point(x, 0), new Point(x, currentHeight));
                        }
                    }
                    else if (centerX > currentWidth)
                    {
                        for (double x = currentWidth + ((centerX - currentWidth) % cm); x >= 0; x -= cm)
                        {
                            if (x <= currentWidth)
                                context.DrawLine(pen, new Point(x, 0), new Point(x, currentHeight));
                        }
                    }
                    else
                    {
                        for (double x = centerX; x >= 0; x -= cm)
                        {
                            context.DrawLine(pen, new Point(x, 0), new Point(x, currentHeight));
                        }
                        for (double x = centerX; x <= currentWidth; x += cm)
                        {
                            context.DrawLine(pen, new Point(x, 0), new Point(x, currentHeight));
                        }
                    }

                    if (centerY < 0)
                    {
                        for (double y = centerY % cm; y <= currentHeight; y += cm)
                        {
                            if (y >= 0)
                                context.DrawLine(pen, new Point(0, y), new Point(currentWidth, y));
                        }
                    }
                    else if (centerY > currentHeight)
                    {
                        for (double y = currentHeight + ((centerY - currentHeight) % cm); y >= 0; y -= cm)
                        {
                            if (y <= currentHeight)
                                context.DrawLine(pen, new Point(0, y), new Point(currentWidth, y));
                        }
                    }
                    else
                    {
                        for (double y = centerY; y >= 0; y -= cm)
                        {
                            context.DrawLine(pen, new Point(0, y), new Point(currentWidth, y));
                        }
                        for (double y = centerY; y <= currentHeight; y += cm)
                        {
                            context.DrawLine(pen, new Point(0, y), new Point(currentWidth, y));
                        }
                    }
                }

                canvas.AddVisual(gridVisual);
            }
        }

        private void DrawOXOY()
        {
            var oxoyVisual = new DrawingVisual();
            var pen = new Pen(Brushes.Black, 1);
            using (DrawingContext context = oxoyVisual.RenderOpen())
            {
                var boolX = centerX >= 0 && centerX <= currentWidth;
                var boolY = centerY >= 0 && centerY <= currentHeight;
                if (boolX)
                    context.DrawLine(pen, new Point(centerX, 0), new Point(centerX, currentHeight));
                if (boolY)
                    context.DrawLine(pen, new Point(0, centerY), new Point(currentWidth, centerY));
                if (slider.Value <= 1.5 && centerX >= 12 && centerX <= currentWidth && boolY)
                    context.DrawText(new FormattedText("0", this.Dispatcher.Thread.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 10, Brushes.Black), new Point(centerX - 12, centerY + 7));

                // OX
                if (boolY)
                {
                    for (double x = centerX - cm; x >= 0; x -= cm)
                    {
                        if (x >= 0 && x <= currentWidth)
                            context.DrawLine(pen, new Point(x, centerY - 5), new Point(x, centerY + 5));
                        if (slider.Value <= 1.5 && x >= 15 && x <= currentWidth)
                            context.DrawText(new FormattedText(Math.Round(-(centerX - x) / cm, 0).ToString(CultureInfo.InvariantCulture), this.Dispatcher.Thread.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 10, Brushes.Black), new Point(x - 15, centerY + 7));
                    }
                    for (double x = centerX + cm; x <= currentWidth; x += cm)
                    {
                        if (x >= 0 && x <= currentWidth)
                            context.DrawLine(pen, new Point(x, centerY - 5), new Point(x, centerY + 5));
                        if (slider.Value <= 1.5 && x >= 15 && x <= currentWidth)
                            context.DrawText(new FormattedText(Math.Round((x - centerX) / cm, 0).ToString(CultureInfo.InvariantCulture), this.Dispatcher.Thread.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 10, Brushes.Black), new Point(x - 12, centerY + 7));
                    }
                }

                // OY
                if (boolX)
                {
                    for (double y = centerY - cm; y >= 0; y -= cm)
                    {
                        if (y >= 0 && y <= currentHeight)
                        {
                            context.DrawLine(pen, new Point(centerX - 5, y), new Point(centerX + 5, y));

                            if (slider.Value <= 1.5 && centerX >= 15)
                                context.DrawText(new FormattedText(Math.Round((centerY - y) / cm, 0).ToString(CultureInfo.InvariantCulture), this.Dispatcher.Thread.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 10, Brushes.Black), new Point(centerX - 12, y + 7));
                        }
                    }
                    for (double y = centerY + cm; y <= currentHeight; y += cm)
                    {
                        if (y >= 0 && y <= currentHeight)
                        {
                            context.DrawLine(pen, new Point(centerX - 5, y), new Point(centerX + 5, y));

                            if (slider.Value <= 1.5 && centerX >= 15)
                                context.DrawText(new FormattedText(Math.Round(-(y - centerY) / cm, 0).ToString(CultureInfo.InvariantCulture), this.Dispatcher.Thread.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 10, Brushes.Black), new Point(centerX - 15, y + 7));
                        }
                    }
                }
            }
            canvas.AddVisual(oxoyVisual);
        }

        private void DrawFunc(GraphItemViewModel graph)
        {
            var exp = graph.Expression;
            var geometry = new StreamGeometry();

            using (var context = geometry.Open())
            {
                bool startFlag = true;
                double y;
                double tempY;
                for (double x = -centerX / cm; x <= (currentWidth - centerX) / cm; x += 0.03 * slider.Value)
                {
                    parameters.Variables["x"] = x;
                    y = (double)exp.Execute(parameters);

                    tempY = centerY - (y * cm);
                    if (double.IsNaN(y) || tempY < 0 || tempY > currentHeight)
                    {
                        startFlag = true;
                    }
                    else
                    {
                        if (startFlag)
                        {
                            context.BeginFigure(new Point(centerX + (x * cm), tempY), false, false);
                            startFlag = false;
                        }

                        context.LineTo(new Point(centerX + (x * cm), tempY), true, false);
                    }
                }
            }

            geometry.Freeze();

            var funcVisual = new DrawingVisual();
            var brush = new SolidColorBrush(graph.ChartColor);
            var pen = new Pen(brush, 1);
            pen.Freeze();
            using (DrawingContext context = funcVisual.RenderOpen())
                context.DrawGeometry(brush, pen, geometry);
            graph.Visual = funcVisual;
            canvas.AddVisual(funcVisual);
        }

        public IEnumerable<GraphItemViewModel> Expression
        {
            get
            {
                return exps;
            }
            set
            {
                exps = value;
                ReRender();
            }
        }

    }

}
