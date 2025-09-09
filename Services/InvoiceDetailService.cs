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
    internal class InvoiceDetailService
    {
        private IInvoiceDetailRepository _repository;

        public InvoiceDetailService()
        {
            _repository = new InvoiceDetailRepository();
        }

        public bool SaveInvoiceDetail(InvoiceDetail invoiceDetail)
        {
            return _repository.Save(invoiceDetail);
        }
    }
}
