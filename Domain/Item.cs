using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIIPractica01.Domain
{
    public class Item
    {
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public int Id { get; set; }

        public bool IsActive { get; set; }


        public override string ToString()
        {
            return $"Codigo: {Id} \n Nombre: {Name} \n Precio Unitario: {UnitPrice} \n";
        }
    }
}
