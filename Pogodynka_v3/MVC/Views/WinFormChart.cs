using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
namespace Pogodynka_v3
{
    class WinFormChart : View
    {
        Chart chart;

        public WinFormChart(ref Chart chart)
        {
            this.chart = chart;
        }

        private void addSeriesToChart(string seriesName)
        {
            chart.Series.Add(seriesName);
            chart.Series[seriesName].ChartArea = "ChartArea1";
            chart.Series[seriesName].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart.Series[seriesName].BorderWidth = 5;
        }
        private void delSeriesFromChart(string seriesName)
        {
            try
            {
                chart.Series.Remove(chart.Series[seriesName]);
            }

            catch(System.ArgumentException)
            {
            }
        }

        public override void updateView(ModelData Parameters)
        {
            if (chart.IsDisposed == true)
            {
                return;
            }
            chart.Invoke(new updateViewDelegate(updateChart), new object[] {Parameters});
        }
        private delegate void updateViewDelegate(ModelData Parameters);
        private void updateChart(ModelData Parameters)
        {
            try
            {
                chart.Series[Parameters.senderID].Points.Clear();
            }
            catch (Exception)
            {
                addSeriesToChart(Parameters.senderID);
            }

            foreach (Temperature temperatureRecord in Parameters.temperatureRecords)
            {
                chart.Series[Parameters.senderID].Points.AddXY(temperatureRecord.time,
                    temperatureRecord.temperatureValue);
            }
        }

        public override void delDataFromView(string modelID)
        {
            delSeriesFromChart(modelID);
        }
        
        public bool isSeriesDisplayed(string seriesName)
        {
            foreach (Series series in this.chart.Series)
            {
                if (series.Name == seriesName)
                {
                    return true;
                }
            }
            return false;
        }

        public override bool isModelBeingViewed(string modelID)
        {
            foreach(Series series in chart.Series)
            {
                if (series.Name == modelID)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
