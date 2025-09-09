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
        public String Client { get; set; }

        private List<InvoiceDetail> details;

        public List<InvoiceDetail> GetDetails()
        {

            return details;

        }

        public Invoice(InvoiceDetail d)
        { 
        details = new List<InvoiceDetail>();
        
        }

       



        public void AddDetails(InvoiceDetail d)
        {
            if (d != null)
                details.Add(d);

        }
        public void RemoveDetails(int index)
        {
            details.RemoveAt(index);
        }

        public float Total()
        {
            float total = 0;
            foreach (var detail in details)
                total += detail.SubTotal();

            return total;
        }


    }
}
