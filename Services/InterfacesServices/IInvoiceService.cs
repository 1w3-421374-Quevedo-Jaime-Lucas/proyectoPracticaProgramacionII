using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIIPractica01.Data.Implementations;
using PIIPractica01.Data.Interfaces;
using PIIPractica01.Domain;

namespace PIIPractica01.Services.InterfacesServices
{
    public interface IInvoiceService
    {
        public List<Invoice> GetInvoices();


        List<Invoice> GetInvoice(int id);


        public Invoice SaveInvoice(Invoice invoice);
        

    }
}
