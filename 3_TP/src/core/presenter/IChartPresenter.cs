using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace _3_TP.src.core.presenter
{
    public interface IChartPresenter
    {
        //IChartView View { set; }


        /// <summary>
        /// Выводит на экран график [series]
        /// </summary>
        void ShowChart(List<Series> series);
        /// <summary>
        /// Выводит на экран сообщение [info]
        /// </summary>
        void ShowAdditionalInfo(string info);
        /// <summary>
        /// Выводит на экран таблицу.
        /// </summary>
        /// <param name="columnsName">Названия столбцов</param>
        /// <param name="rows">Данные таблицы</param>
        void ShowGrid(List<string> columnsName, List<List<string>> rows);
    }
}
