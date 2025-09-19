using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiEF.Data.Models;
using PIIPractica01.Data.Helpers;
using PIIPractica01.Data.Interfaces;
using PIIPractica01.Domain;

namespace PIIPractica01.Data.Implementations
{
    public class ItemRepository : IItemRepository
    {
        private Practica01DbContext _dbContext;
        
        public ItemRepository(Practica01DbContext context)
        {

            _dbContext = context;

        }

        public bool Delete(int id)
        {
            var articuloDelete = GetById(id);
            if (articuloDelete != null)
            {
                _dbContext.Articulos.Remove(articuloDelete);
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Articulo> GetAll()
        {
            return _dbContext.Articulos.ToList();
        }

        public Articulo? GetById(int id)
        {
            return _dbContext.Articulos.Find(id);
        }
        
        public bool Save(Articulo articulo)
        {
            if (articulo != null)
            {

                _dbContext.Articulos.Add(articulo);
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
              
        }

        public bool Update(Articulo articulo, int id)
        {
            if (articulo != null)
            {
                _dbContext.Articulos.Update(articulo);
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
