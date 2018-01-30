using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace Chapter2
{
    public class Plot
    {
        private PlotModel _plotModel;
        
        public Plot()
        {
            _plotModel = new PlotModel();
            _plotModel.LegendTitle = "Legend";
            _plotModel.LegendOrientation = LegendOrientation.Horizontal;
            _plotModel.LegendPlacement = LegendPlacement.Outside;
            _plotModel.LegendPosition = LegendPosition.TopRight;
            _plotModel.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
            _plotModel.LegendBorder = OxyColors.Black;

            var stepAxis = new LinearAxis { Position = AxisPosition.Bottom, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Title = "Num. Steps" };
            _plotModel.Axes.Add(stepAxis);
            var valueAxis = new LinearAxis { Position = AxisPosition.Left, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Title = "Avg Reward" };
            _plotModel.Axes.Add(valueAxis);
        }

        public void AddSeries(List<float> data, string title, double hue)
        {
            var lineSeries = new LineSeries
            {
                StrokeThickness = 2,
                MarkerSize = 3,
                MarkerStroke = OxyColor.FromHsv(hue, 60, 44),
                CanTrackerInterpolatePoints = true,
                Title = title,
                Smooth = false
            };

            int i = 1;
            data.ForEach(dataPoint =>
            {
                lineSeries.Points.Add(new DataPoint(i, dataPoint));
                i++;
            });
            _plotModel.Series.Add(lineSeries);
        }

        public void Export()
        {
            using (var stream = File.Create("Plot.pdf"))
            {
                var pdfExporter = new PdfExporter { Width = 600, Height = 400 };
                pdfExporter.Export(_plotModel, stream);
            }
        }
    }
}