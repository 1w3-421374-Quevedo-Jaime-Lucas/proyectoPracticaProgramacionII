using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIIPractica01.Data.Implementations;
using PIIPractica01.Data.Interfaces;
using PIIPractica01.Domain;

namespace PIIPractica01.Services
{
    internal class InvoiceService
    {
        private IInvoiceRepository _repository;

        public InvoiceService()
        {

            _repository = new InvoiceRepository();

        }

        public List<InvoiceDetail> GetInvoices()
        {
            return _repository.GetAll();
        }

        public InvoiceDetail? GetInvoice(int id)
        {
            return _repository.GetById(id);
        }

        public bool SaveInvoice(Invoice invoice)
        {
            return _repository.Save(invoice);
        }
    }
}
