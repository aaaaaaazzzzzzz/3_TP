using _3_TP.src.core.presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_TP.src.core.commands
{
    public interface IChartCommand
    {
        /// <summary>
        /// Выполнить команду.
        /// </summary>
        /// <param name="presenter">Презентер, отвечающий за отображение на экране.</param>
        void Run(IChartPresenter presenter);
    }
}
