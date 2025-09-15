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
    public class ItemService : IItemService
    {
        private IItemRepository _itemRepository;

        public ItemService(IItemRepository repository)
        {
            _itemRepository = repository;
        }
        public List<Item> GetItems()
        {
            return _itemRepository.GetAll();
        }

        public Item? GetItem(int id)
        {
            return _itemRepository.GetById(id);
        }

        public bool SaveItem(Item item)
        {
            return _itemRepository.Save(item);
        }

        public bool DeleteItem(int id)
        {
            return _itemRepository.Delete(id);
        }

        public bool UpdateItem(Item item, int id)
        {
            return _itemRepository.Update(item, id);
        }
    }
}
