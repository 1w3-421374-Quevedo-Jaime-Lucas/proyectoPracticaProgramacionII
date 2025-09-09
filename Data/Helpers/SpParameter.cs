using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIIPractica01.Data.Helpers
{
    internal class SpParameter
    {

        public string Name { get; set; }
        public object Valor { get; set; }

        public SpParameter() { }

        public SpParameter(string name, object valor)
        {
            Name = name;
            Valor = valor;
        }
    }
}
