using _3_TP.src.core.commands;
using _3_TP.src.core.commands.infections;
using _3_TP.src.core.commands.population;
using _3_TP.src.core.commands.salaries;
using _3_TP.src.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace _3_TP.src.core.presenter
{
    internal class ChartPresenter : IChartPresenter
    {
        /// <summary>
        /// Словарь, содержащий фабрику для каждой команды.
        /// </summary>
        /// <example>
        /// {"some_command", () => new SomeCommandFactory()}
        /// </example>
        static Dictionary<string, Func<AbstractCommandFactory>> commands
            = new Dictionary<string, Func<AbstractCommandFactory>>()
            {
            {"Инфекции", () => new InfectionsCommandFactory()},
            {"Медианные зарплаты", () => new SalariesCommandFactory()},
            { "Популяция страны", () => new PopulationCommandFactory() }
            };

        AbstractCommandFactory commandFactory;
        IChartView view;

        public IChartView View
        {
            set
            {
                if (view != null)
                {
                    this.view.RequestedStatistics -= RunCommand;
                    this.view.ChangedCommand -= ChangeCommand;
                }
                this.view = value;
                this.view.RequestedStatistics += RunCommand;
                this.view.ChangedCommand += ChangeCommand;
                view.SetCommands(commands.Keys.ToList());
            }
        }

        // Метод для отображения дополнительной информации в представлении
        public void ShowAdditionalInfo(string info)
        {
            view.ShowAdditionalInfo(info);
        }

        // Метод для отображения графика в представлении
        public void ShowChart(List<Series> series)
        {
            view.ShowChart(series);
        }

        // Метод для отображения таблицы в представлении
        public void ShowGrid(List<string> columnsName, List<List<string>> rows)
        {
            view.ShowGrid(columnsName, rows);
        }

        // Метод, выполняющий команду
        private void RunCommand()
        {
            string csv = view.GetCSV();
            if (csv == "" || commandFactory == null) return;
            var command = commandFactory.CreateFromCSV(csv);
            command.Run(this);
        }

        // Метод, изменяющий текущую команду
        private void ChangeCommand(String code)
        {
            // Установка фабрики команд на основании выбранного кода
            commandFactory = commands[code]();
        }
    }

}
