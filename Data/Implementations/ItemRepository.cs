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
    internal class ItemRepository : IItemRepository
    {
        List<Item> IItemRepository.GetAll()
        {
            List<Item> lst = new List<Item>();

            var dt = DataHelper.GetInstance().ExecuteSpQuery("SP_RECUPERAR_ARTICULOS");


            foreach (DataRow row in dt.Rows)
            {
                Item I = new Item();
                I.Id = (int)row["Id"];
                I.Name = (string)row["Nombre"];
                I.UnitPrice = (int)row["Precio_Unitario"];

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
                    Name = "@codigo",
                    Valor = id
                }
            };

            var dt = DataHelper.GetInstance().ExecuteSpQuery("SP_RECUPERAR_ARTICULO_POR_ID", param);


            if (dt != null && dt.Rows.Count > 0)
            {
                Item I = new Item()
                {
                    Id = (int)dt.Rows[0]["ID"],
                    Name = (string)dt.Rows[0]["nombre"],
                    UnitPrice = (int)dt.Rows[0]["precio_unitario"],

                };

                return I;
            }

            return null;
        }

        bool IItemRepository.Save(Item item)
        {
            List<SpParameter> param = new List<SpParameter>()
            {
                new SpParameter("@nombre", item.Name),
                new SpParameter("@precio_unitario", item.UnitPrice)
            };

            return DataHelper.GetInstance().ExecuteSpDml("SP_GUARDAR_ARTICULOS", param);

        }

        bool IItemRepository.Delete(int id)
        {
            List<SpParameter> sp = new List<SpParameter>();

            new SpParameter()
            {
                Name = "@id",
                Valor = id
            };

            return DataHelper.GetInstance().ExecuteSpDml("SP_REGISTRAR_BAJA_ARTICULOS", sp);

        }
    }
}
