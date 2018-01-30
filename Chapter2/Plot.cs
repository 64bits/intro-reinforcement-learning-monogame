using System.IO;
using OxyPlot;
using OxyPlot.Axes;

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

            var dateAxis = new DateTimeAxis { Position = AxisPosition.Bottom, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, IntervalLength = 80 };
            _plotModel.Axes.Add(dateAxis);
            var valueAxis = new LinearAxis { Position = AxisPosition.Left, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Title = "Value" };
            _plotModel.Axes.Add(valueAxis);
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