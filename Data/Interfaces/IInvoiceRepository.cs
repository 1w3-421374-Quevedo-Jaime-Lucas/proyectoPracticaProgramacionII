using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PIIPractica01.Domain;

namespace PIIPractica01.Data.Interfaces
{
    public interface IInvoiceRepository
    {
        List<Invoice> GetAll();
        List<Invoice> GetById(int id);

        Invoice Save(Invoice invoice);

    }
}
