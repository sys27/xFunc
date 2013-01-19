// Copyright 2012 Dmitry Kischenko
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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using xFunc.Maths.Expressions;

namespace xFunc.Views
{

    public partial class PlottingGraph : UserControl
    {

        private IMathExpression exp;
        private MathParameterCollection parameters;

        private DrawingVisual gridVisual;
        private DrawingVisual oxoyVisual;
        private DrawingVisual funcVisual;

        private double currentWidth;
        private double currentHeight;
        private double centerX;
        private double centerY;
        private Point startPoint;
        private double cm = 40;

        public PlottingGraph()
        {
            this.parameters = new MathParameterCollection() { { 'x', 0 } };

            InitializeComponent();

            this.SizeChanged += this_SizeChanged;
            this.MouseLeftButtonDown += this_MouseLeftButtonDown;
            this.MouseMove += this_MouseMove;
            this.MouseWheel += this_MouseWheel;
            slider.ValueChanged += slider_ValueChanged;
            renderGrid.Checked += (o, args) => { ReRender(); };
            renderGrid.Unchecked += (o, args) => { ReRender(); };
        }

        private void this_SizeChanged(object o, SizeChangedEventArgs args)
        {
            InitCoords();
            ReRender();
        }

        private void InitCoords()
        {
            currentWidth = this.ActualWidth;
            currentHeight = this.ActualHeight - this.secondRow.Height.Value;
            centerX = Math.Ceiling(currentWidth / 2);
            centerY = Math.Ceiling(currentHeight / 2);
        }

        private void this_MouseLeftButtonDown(object o, MouseButtonEventArgs args)
        {
            startPoint = args.GetPosition(null);
        }

        private void this_MouseMove(object o, MouseEventArgs args)
        {
            if (args.OriginalSource is DrawingCanvas)
            {
                double x = args.GetPosition(this).X;
                double y = args.GetPosition(this).Y;
                point.Text = string.Format("x: {0} {2} y: {1} {2}", Math.Round((x - centerX) / cm, 2), Math.Round(-(y - centerY) / cm, 2), "cm");

                if (args.LeftButton == MouseButtonState.Pressed)
                {
                    double chX = startPoint.X - args.GetPosition(null).X;
                    double chY = startPoint.Y - args.GetPosition(null).Y;

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

            double x = args.GetPosition(null).X;
            double y = args.GetPosition(null).Y;
            point.Text = string.Format("x: {0} {2} y: {1} {2}", Math.Round((x - centerX) / cm, 2), Math.Round(-(y - centerY) / cm, 2), "cm");
        }

        private void slider_ValueChanged(object o, RoutedPropertyChangedEventArgs<double> args)
        {
            double temp = 40;
            cm = temp / args.NewValue;

            ReRender();
        }

        private void ReRender()
        {
            DrawGrid();
            DrawOXOY();
            DrawFunc();
        }

        private void DrawGrid()
        {
            canvas.DeleteVisual(gridVisual);

            if (renderGrid.IsChecked == true)
            {
                gridVisual = new DrawingVisual();
                Pen pen = new Pen(Brushes.Blue, 0.5);
                using (DrawingContext context = gridVisual.RenderOpen())
                {
                    for (double x = centerX; x >= 0; x -= cm)
                    {
                        context.DrawLine(pen, new Point(x, 0), new Point(x, currentHeight));
                    }
                    for (double x = centerX; x <= currentWidth; x += cm)
                    {
                        context.DrawLine(pen, new Point(x, 0), new Point(x, currentHeight));
                    }

                    for (double y = centerY; y >= 0; y -= cm)
                    {
                        context.DrawLine(pen, new Point(0, y), new Point(currentWidth, y));
                    }
                    for (double y = centerY; y <= currentHeight; y += cm)
                    {
                        context.DrawLine(pen, new Point(0, y), new Point(currentWidth, y));
                    }
                }

                canvas.AddVisual(gridVisual);
            }
        }

        private void DrawOXOY()
        {
            canvas.DeleteVisual(oxoyVisual);
            oxoyVisual = new DrawingVisual();
            Pen pen = new Pen(Brushes.Black, 1);
            using (DrawingContext context = oxoyVisual.RenderOpen())
            {
                context.DrawLine(pen, new Point(centerX, 0), new Point(centerX, currentHeight));
                context.DrawLine(pen, new Point(0, centerY), new Point(currentWidth, centerY));
                if (slider.Value <= 1.5)
                    context.DrawText(new FormattedText("0",
                                                       this.Dispatcher.Thread.CurrentCulture,
                                                       FlowDirection.LeftToRight,
                                                       new Typeface("Arial"), 10,
                                                       Brushes.Black),
                                     new Point(centerX - 12, centerY + 7));

                // OX
                for (double x = centerX - cm; x >= 0; x -= cm)
                {
                    context.DrawLine(pen, new Point(x, centerY - 5), new Point(x, centerY + 5));

                    if (slider.Value <= 1.5)
                        context.DrawText(new FormattedText(Math.Round(-(centerX - x) / cm, 0).ToString(),
                                                           this.Dispatcher.Thread.CurrentCulture,
                                                           FlowDirection.LeftToRight,
                                                           new Typeface("Arial"), 10,
                                                           Brushes.Black),
                                         new Point(x - 15, centerY + 7));
                }
                for (double x = centerX + cm; x <= currentWidth; x += cm)
                {
                    context.DrawLine(pen, new Point(x, centerY - 5), new Point(x, centerY + 5));

                    if (slider.Value <= 1.5)
                        context.DrawText(new FormattedText(Math.Round((x - centerX) / cm, 0).ToString(),
                                                           this.Dispatcher.Thread.CurrentCulture,
                                                           FlowDirection.LeftToRight,
                                                           new Typeface("Arial"), 10,
                                                           Brushes.Black),
                                         new Point(x - 12, centerY + 7));
                }

                // OY
                for (double y = centerY - cm; y >= 0; y -= cm)
                {
                    context.DrawLine(pen, new Point(centerX - 5, y), new Point(centerX + 5, y));

                    if (slider.Value <= 1.5)
                        context.DrawText(new FormattedText(Math.Round((centerY - y) / cm, 0).ToString(),
                                                           this.Dispatcher.Thread.CurrentCulture,
                                                           FlowDirection.LeftToRight,
                                                           new Typeface("Arial"),
                                                           10, Brushes.Black),
                                         new Point(centerX - 12, y + 7));
                }
                for (double y = centerY + cm; y <= currentHeight; y += cm)
                {
                    context.DrawLine(pen, new Point(centerX - 5, y), new Point(centerX + 5, y));

                    if (slider.Value <= 1.5)
                        context.DrawText(new FormattedText(Math.Round(-(y - centerY) / cm, 0).ToString(),
                                                           this.Dispatcher.Thread.CurrentCulture,
                                                           FlowDirection.LeftToRight,
                                                           new Typeface("Arial"), 10,
                                                           Brushes.Black),
                                         new Point(centerX - 15, y + 7));
                }
            }
            canvas.AddVisual(oxoyVisual);
        }

        private void DrawFunc()
        {
            if (exp == null)
                return;

            PathGeometry geometry = new PathGeometry();
            PathFigure figure = null;

            bool startFlag = true;
            double y;
            double tempY;
            for (double x = -centerX / cm; x <= (currentWidth - centerX) / cm; x += 0.01 * slider.Value)
            {
                parameters['x'] = x;
                y = exp.Calculate(parameters);

                tempY = centerY - (y * cm);
                if (double.IsNaN(y) || tempY < 0 || tempY > currentHeight)
                {
                    startFlag = true;
                }
                else
                {
                    if (startFlag)
                    {
                        figure = new PathFigure() { IsClosed = false, IsFilled = false };
                        geometry.Figures.Add(figure);

                        figure.StartPoint = new Point(centerX + (x * cm), tempY);
                        startFlag = false;
                    }

                    figure.Segments.Add(new LineSegment(new Point(centerX + (x * cm), tempY), true));
                }
            }

            canvas.DeleteVisual(funcVisual);
            funcVisual = new DrawingVisual();
            Pen pen = new Pen(Brushes.Red, 1);
            using (DrawingContext context = funcVisual.RenderOpen())
            {
                context.DrawGeometry(Brushes.Red, pen, geometry);
            }
            canvas.AddVisual(funcVisual);
        }

        public IMathExpression Expression
        {
            get
            {
                return exp;
            }
            set
            {
                exp = value;
                ReRender();
            }
        }

    }

}
