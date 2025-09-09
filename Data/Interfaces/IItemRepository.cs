using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIIPractica01.Domain;

namespace PIIPractica01.Data.Interfaces
{
    internal interface IItemRepository
    {
        List<Item> GetAll();
        Item? GetById(int id);

        bool Save(Item item);

        bool Delete(int id);



    }
}
