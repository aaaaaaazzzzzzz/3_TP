using _3_TP.src.core.presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace _3_TP.src.core.commands.salaries
{
    internal class SalariesCommand : IChartCommand
    {
        readonly List<List<float>> data;

        public SalariesCommand(List<List<float>> data)
        {
            this.data = data;
        }

        public void Run(IChartPresenter presenter)
        {
            // Table display
            List<string> columnsName = new List<string>() { "Год", "Мужчины", "Женщины" };
            List<List<string>> rows = new List<List<string>>();
            for (int i = 0; i < data.Count; i++)
            {
                rows.Add(data[i].Select((e) => e.ToString()).ToList());
            }
            presenter.ShowGrid(columnsName, rows);

            // Chart display
            Series menSeries = new Series();
            menSeries.ChartType = SeriesChartType.Spline;
            menSeries.YAxisType = AxisType.Primary;
            menSeries.Name = "Мужчины, руб.";
            data.ForEach((point) => menSeries.Points.AddXY(point[0], point[1]));

            Series womenSeries = new Series();
            womenSeries.ChartType = SeriesChartType.Spline;
            womenSeries.YAxisType = AxisType.Primary;
            womenSeries.Name = "Женщины, руб.";
            data.ForEach((point) => womenSeries.Points.AddXY(point[0], point[2]));

            presenter.ShowChart(new List<Series> { menSeries, womenSeries });

            // Info display
            presenter.ShowAdditionalInfo("Медианные зарплаты за последние 15 лет.");
        }
    }
}
