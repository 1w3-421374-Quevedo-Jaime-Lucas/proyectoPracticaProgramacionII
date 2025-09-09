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
    internal class ItemService
    {
        private IItemRepository _itemRepository;

        public ItemService()
        {
            _itemRepository = new ItemRepository();
        }
        public List<Item> GetItems()
        {
            return _itemRepository.GetAll();
        }

        public Item? GetItem(int id)
        {
            return _itemRepository.GetById(id);
        }

        public bool SaveIngredient(Item item)
        {
            return _itemRepository.Save(item);
        }
    }
}
