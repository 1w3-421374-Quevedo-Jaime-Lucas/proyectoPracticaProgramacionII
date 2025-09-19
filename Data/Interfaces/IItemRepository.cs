using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiEF.Data.Models;
using PIIPractica01.Domain;

namespace PIIPractica01.Data.Interfaces
{
    public interface IItemRepository
    {
        List<Articulo> GetAll();
        Articulo? GetById(int id);

        bool Save(Articulo articulo);

        bool Delete(int id);

        bool Update(Articulo articulo, int id);

    }
}
