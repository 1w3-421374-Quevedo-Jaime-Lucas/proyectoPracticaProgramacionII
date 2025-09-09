using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PIIPractica01.Domain;

namespace PIIPractica01.Data.Interfaces
{
    internal interface IInvoiceRepository
    {
        List<InvoiceDetail> GetAll();
        InvoiceDetail? GetById(int id);

        bool Save(Invoice invoice);

    }
}
