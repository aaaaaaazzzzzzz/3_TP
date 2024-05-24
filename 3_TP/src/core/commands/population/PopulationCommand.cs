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
            // Добавление изменения населения для первого года
            data[0].Add(0);
            for (int i = 1; i < data.Count; i++)
            {
                data[i].Add(data[i][1] - data[i - 1][1]);
            }

            // Отображение таблицы
            List<string> columnsName = new List<string>() { "Год", "Рост населения" };
            List<List<string>> rows = new List<List<string>>();
            for (int i = 0; i < data.Count; i++)
            {
                rows.Add(new List<string> { data[i][0].ToString(), data[i][1].ToString() });
            }
            presenter.ShowGrid(columnsName, rows);

            // Отображение графиков
            Series populationSeries = new Series
            {
                ChartType = SeriesChartType.Spline,
                YAxisType = AxisType.Primary,
                Name = "Популяция, чел."
            };
            data.ForEach((point) => populationSeries.Points.AddXY(point[0], point[1]));

            Series growSeries = new Series
            {
                ChartType = SeriesChartType.FastLine,
                YAxisType = AxisType.Secondary,
                Name = "Рост от пред. года, чел."
            };
            data.ForEach((point) => growSeries.Points.AddXY(point[0], point[2]));

            presenter.ShowChart(new List<Series> { populationSeries, growSeries });

            // Информация
            presenter.ShowAdditionalInfo("Рост населения РФ за последние 15 лет");
        }
    }
}

