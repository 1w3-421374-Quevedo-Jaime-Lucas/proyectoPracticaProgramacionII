using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIIPractica01.Data.Implementations;
using PIIPractica01.Data.Interfaces;
using PIIPractica01.Domain;
using PIIPractica01.Services.InterfacesServices;

namespace PIIPractica01.Services
{
    public class InvoiceService : IInvoiceService
    {
        private IInvoiceRepository _repository;
        public InvoiceService()
        {

            _repository = new InvoiceRepository();

        }

        public List<Invoice> GetInvoices()
        {
            return _repository.GetAll();
        }

        public List<Invoice> GetInvoice(int id)
        {
            return _repository.GetById(id);
        }

        public Invoice SaveInvoice(Invoice invoice)
        {
            return _repository.Save(invoice);
        }
    }
}
