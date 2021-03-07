using CrossCutting.DataClasses;
using Logic.LogFileAnalyzation;
using Logic.LogFileAnalyzation.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace UI.SshTarpitAnalyzer.WinForms
{
    public partial class Form1 : Form
    {
        private readonly ILogFileParser logFileParser = new LogFileParser();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IReadOnlyCollection<ConnectionInformation> list = logFileParser.Parse(textBoxLogFile.Text);
            textBoxDurationLog.Text = string.Join(Environment.NewLine, list.OrderByDescending(x => x.Duration)
                .Select(x => $"{x.Duration} {x.Ip}"));

            CreateChart();
            chart1.Series.Clear();
            var series1 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Series1",
                Color = System.Drawing.Color.Green,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                IsValueShownAsLabel = true,
                ChartType = SeriesChartType.Spline,
            };

            this.chart1.Series.Add(series1);

            foreach (var grouped in list.GroupBy(x => (int)(x.Duration.TotalSeconds / 10)).OrderBy(x => x.Key))
            {
                int count = grouped.Count();
                series1.Points.AddXY((grouped.Key * 10) + 10, count);
            }

            double maxY = double.MinValue;
            DataPoint maxDp = null;
            foreach (DataPoint dp in series1.Points)
            {

                //find max value of Y axis and the corresponding point
                if (dp.YValues[0] >= maxY)
                {
                    maxY = dp.YValues[0];
                    maxDp = dp;
                }

            }

            //at the end set the label<br/>
            maxDp.AxisLabel = maxDp.XValue.ToString();
            chart1.Invalidate();
        }

        private void CreateChart() 
        {

        }
    }
}
