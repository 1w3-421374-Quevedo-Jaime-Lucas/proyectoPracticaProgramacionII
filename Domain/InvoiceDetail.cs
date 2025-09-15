using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PIIPractica01.Domain
{
    public class InvoiceDetail
    {
        public Item Item { get; set; }
        public int Amount { get; set; }
        public decimal SellPrice { get; set; }


        [JsonIgnore]
        public Invoice? Invoice { get; set; }

        public float SubTotal()
        {
            return (Amount * (int)SellPrice);
        }

    }
}
