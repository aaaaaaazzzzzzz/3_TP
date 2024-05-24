using _3_TP.src.core.presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace _3_TP.src.core.commands.population
{
    internal class PopulationCommand : IChartCommand
    {
        readonly List<List<float>> data;

        public PopulationCommand(List<List<float>> data)
        {
            this.data = data;
        }

        public void Run(IChartPresenter presenter)
        {
            data[0].Add(0);
            for (int i = 1; i < data.Count; i++)
            {
                data[i].Add(data[i][1] - data[i - 1][1]);
            }

            // Table display
            List<string> columnsName = new List<string>() { "Год", "Рост населения", "Соотношение с предыдущим" };
            List<List<string>> rows = new List<List<string>>();
            for (int i = 0; i < data.Count; i++)
            {
                rows.Add(data[i].Select((e) => e.ToString()).ToList());
            }
            presenter.ShowGrid(columnsName, rows);

            // Chart display
            Series populationSeries = new Series();
            populationSeries.ChartType = SeriesChartType.Spline;
            populationSeries.YAxisType = AxisType.Primary;
            populationSeries.Name = "Популяция, чел.";
            data.ForEach((point) => populationSeries.Points.AddXY(point[0], point[1]));

            Series growSeries = new Series();
            growSeries.ChartType = SeriesChartType.FastLine;
            growSeries.YAxisType = AxisType.Secondary;
            growSeries.Name = "Рост от пред. года, чел.";
            data.ForEach((point) => growSeries.Points.AddXY(point[0], point[2]));

            presenter.ShowChart(new List<Series> { populationSeries, growSeries });

            // Info display
            presenter.ShowAdditionalInfo("Дополнительная информация отсуствует");
        }
    }
}

