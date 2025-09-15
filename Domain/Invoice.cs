using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIIPractica01.Domain
{
    public class Invoice
    {
        public int Nroinvoice { get; set; }
        public DateTime Date { get; set; }

        public PaymentForm PaymentForm { get; set; }
        public string Client { get; set; }

        // Inicializamos la lista directamente
        public List<InvoiceDetail> Details { get; set; } = new List<InvoiceDetail>();

        // Constructores
        public Invoice() { }

        private List<InvoiceDetail> details;
        public List<InvoiceDetail> GetDetails() 
        { 
            return details; 
        
        }


        public Invoice(InvoiceDetail d)
        {
            if (d != null)
                Details.Add(d);
        }

        // Métodos para manipular detalles
        public void AddDetails(InvoiceDetail d)
        {
            if (d != null)
                Details.Add(d);
        }

        public void RemoveDetails(int index)
        {
            if (index >= 0 && index < Details.Count)
                Details.RemoveAt(index);
        }

        public float Total()
        {
            float total = 0;
            foreach (var detail in Details)
                total += detail.SubTotal();

            return total;
        }
    }
}
