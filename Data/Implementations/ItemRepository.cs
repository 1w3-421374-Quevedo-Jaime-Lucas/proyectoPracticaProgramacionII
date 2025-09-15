using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIIPractica01.Data.Helpers;
using PIIPractica01.Data.Interfaces;
using PIIPractica01.Domain;

namespace PIIPractica01.Data.Implementations
{
    public class ItemRepository : IItemRepository
    {
        private readonly DataHelper _dataHelper;

        public ItemRepository()
        {
            _dataHelper = DataHelper.GetInstance();
        }



        List<Item> IItemRepository.GetAll()
        {

            List<Item> lst = new List<Item>();
            var dt = DataHelper.GetInstance().ExecuteSpQuery("SP_RECUPERAR_ARTICULOS");

            foreach (DataRow row in dt.Rows)
            {
                Item I = new Item
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = row["Nombre"].ToString(),
                    UnitPrice = Convert.ToDecimal(row["Precio_Unitario"]),
                    IsActive = true

                };
                lst.Add(I);
            }
            return lst;


        }

        Item? IItemRepository.GetById(int id)
        {
            List<SpParameter> param = new List<SpParameter>()
            {
                new SpParameter()
                {
                    Name = "@id",
                    Valor = id
                }
            };

            var dt = DataHelper.GetInstance().ExecuteSpQuery("SP_RECUPERAR_ARTICULOS_POR_ID", param);


            if (dt != null && dt.Rows.Count > 0)
            {
                Item I = new Item()
                {
                    Id = (int)dt.Rows[0]["id"],
                    Name = (string)dt.Rows[0]["nombre"],
                    UnitPrice = (decimal)dt.Rows[0]["precio_Unitario"],
                    IsActive = true

                };

                return I;
            }

            return null;
        }

        bool IItemRepository.Save(Item item)
        {

            List<SpParameter> param = new List<SpParameter>();

            if (string.IsNullOrWhiteSpace(item.Name) || item.Name.Trim().ToLower() == "string")
            {

                param.Add(new SpParameter("@nombre", DBNull.Value));
            }
            else
            {
                param.Add(new SpParameter("@nombre", item.Name));

            }
            // Validar unitPrice: solo si es mayor a 0
            if (item.UnitPrice > 0)
            {
                param.Add(new SpParameter("@precio_unitario", item.UnitPrice));
            }
            else
            {
                param.Add(new SpParameter("@precio_unitario", DBNull.Value));
            }

                return DataHelper.GetInstance().ExecuteSpDml("SP_GUARDAR_ARTICULOS", param);

        }

        bool IItemRepository.Delete(int id)
        {
            List<SpParameter> sp = new List<SpParameter>();

            sp.Add(new SpParameter()
            {
                Name = "@id",
                Valor = id
            });

            return DataHelper.GetInstance().ExecuteSpDml("SP_ElIMINAR_ARTICULOS", sp);

        }

        public bool Update(Item item, int id)
        {

            List<SpParameter> param = new List<SpParameter>()
            {
                new SpParameter("@nombre", item.Name),
                new SpParameter("@precio_unitario", item.UnitPrice),
                new SpParameter("@id", id)
            };

            return DataHelper.GetInstance().ExecuteSpDml("SP_EDITAR_ARTICULOS", param);



        }
    }
}
