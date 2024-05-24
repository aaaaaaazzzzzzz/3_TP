using _3_TP.src.core.presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace _3_TP.src.core.commands.infections
{
        internal class InfectionsCommand : IChartCommand
        {
            readonly List<List<float>> data;

            public InfectionsCommand(List<List<float>> data)
            {
                this.data = data;
            }

            public void Run(IChartPresenter presenter)
            {
                // Вычисление изменений в заболеваемости
                data[0].Add(0); // Для первого года изменение равно 0 для всех болезней
                for (int i = 1; i < data.Count; i++)
                {
                    data[i].Add(data[i][1] - data[i - 1][1]); // ОРВИ
                    data[i].Add(data[i][2] - data[i - 1][2]); // Туберкулез
                    data[i].Add(data[i][3] - data[i - 1][3]); // Клещевые инфекции
                    data[i].Add(data[i][4] - data[i - 1][4]); // Вирусные гепатиты
                    data[i].Add(data[i][5] - data[i - 1][5]); // ВИЧ и СПИД
                }

                // Отображение таблицы
                List<string> columnsName = new List<string>() { "Год", "ОРВИ", "Туберкулез", "Клещевые инфекции", "Вирусные гепатиты", "ВИЧ и СПИД" };
                List<List<string>> rows = new List<List<string>>();
                for (int i = 0; i < data.Count; i++)
                {
                    rows.Add(data[i].Select((e) => e.ToString()).ToList());
                }
                presenter.ShowGrid(columnsName, rows);

                // Отображение графиков
                Series orviSeries = new Series
                {
                    ChartType = SeriesChartType.Spline,
                    YAxisType = AxisType.Primary,
                    Name = "ОРВИ"
                };
                data.ForEach((point) => orviSeries.Points.AddXY(point[0], point[1]));

                Series tbSeries = new Series
                {
                    ChartType = SeriesChartType.Spline,
                    YAxisType = AxisType.Primary,
                    Name = "Туберкулез"
                };
                data.ForEach((point) => tbSeries.Points.AddXY(point[0], point[2]));

                Series tickSeries = new Series
                {
                    ChartType = SeriesChartType.Spline,
                    YAxisType = AxisType.Primary,
                    Name = "Клещевые инфекции"
                };
                data.ForEach((point) => tickSeries.Points.AddXY(point[0], point[3]));

                Series hepSeries = new Series
                {
                    ChartType = SeriesChartType.Spline,
                    YAxisType = AxisType.Primary,
                    Name = "Вирусные гепатиты"
                };
                data.ForEach((point) => hepSeries.Points.AddXY(point[0], point[4]));

                Series hivSeries = new Series
                {
                    ChartType = SeriesChartType.Spline,
                    YAxisType = AxisType.Primary,
                    Name = "ВИЧ и СПИД"
                };
                data.ForEach((point) => hivSeries.Points.AddXY(point[0], point[5]));

                presenter.ShowChart(new List<Series> { orviSeries, tbSeries, tickSeries, hepSeries, hivSeries });

                // Информация о снижении заболеваемости
                presenter.ShowAdditionalInfo("Дополнительная информация отсуствует");
            }
        }
    }
