using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIIPractica01.Domain;

namespace PIIPractica01.Services.InterfacesServices
{
    public interface IItemService 
    {
        public List<Item> GetItems();

        public Item? GetItem(int id);


        public bool SaveItem(Item item);
       
        public bool DeleteItem(int id);

        public bool UpdateItem(Item item, int id);



    }
}
