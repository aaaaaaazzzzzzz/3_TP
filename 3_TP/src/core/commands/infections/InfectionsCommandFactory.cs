using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_TP.src.core.commands.infections
{ 
    internal class InfectionsCommandFactory : AbstractCommandFactory
    {
        public override IChartCommand CreateFromCSV(string csv)
        {
            var data =
                    csv.Split('\n').ToList()
                    .Where(line => !string.IsNullOrWhiteSpace(line))
                    .Select((e) => e.Split(';')
                        .Select((num) => float.Parse(num)).ToList())
                    .ToList();
            return new InfectionsCommand(data);
        }
    }
}
