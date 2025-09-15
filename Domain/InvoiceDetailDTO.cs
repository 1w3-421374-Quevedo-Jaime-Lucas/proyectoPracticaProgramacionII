using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIIPractica01.Domain
{
    public class InvoiceDetailDTO
    {

        public Item Item { get; set; }
        public decimal SellPrice { get; set; }
        public int Amount { get; set; }


        public InvoiceDetailDTO() { }

        // Constructor con parámetros
        public InvoiceDetailDTO(Item item, decimal sellPrice, int amount)
        {
            Item = item;
            SellPrice = sellPrice;
            Amount = amount;
        }
    }
}
