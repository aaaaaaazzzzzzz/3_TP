using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_TP.src.core.commands
{
    public abstract class AbstractCommandFactory
    {
        public AbstractCommandFactory() { }

        /// <summary>
        /// Создать команду из текста в формате CSV.
        /// </summary>
        /// <param name="csv">Текст в формате CSV.</param>
        /// <returns></returns>
        public abstract IChartCommand CreateFromCSV(String csv);
    }
}
