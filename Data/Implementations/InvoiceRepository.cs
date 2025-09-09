using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using PIIPractica01.Data.Helpers;
using PIIPractica01.Data.Interfaces;
using PIIPractica01.Domain;

namespace PIIPractica01.Data.Implementations
{
    internal class InvoiceRepository : IInvoiceRepository
    {
        PaymentForm paymentForm = new PaymentForm();
        Item item = new Item();
        public List<InvoiceDetail> GetAll()
        {
            List<InvoiceDetail> lst = new List<InvoiceDetail>();

            var dt = DataHelper.GetInstance().ExecuteSpQuery("SP_RECUPERAR_FACTURAS");

            foreach (DataRow row in dt.Rows)
            {
                InvoiceDetail InDe = new InvoiceDetail();
                InDe.Invoice.Nroinvoice = (int)row[0];
                InDe.Invoice.Client = (string)row[2];
                InDe.Invoice.PaymentForm.Name = (string)row[6];
                InDe.Invoice.Date = (DateTime)row[1];
                InDe.Item.Name = (string)row[3];
                InDe.SellPrice = (int)(double)row[4];
                InDe.Amount = (int)row[5];

                lst.Add(InDe);
            }
            return lst;
        }

        public InvoiceDetail? GetById(int id)
        {
            List<SpParameter> param = new List<SpParameter>()
            {
                new SpParameter()
                {
                    Name = "@nro_factura",
                    Valor = id
                }
            };

            var dt = DataHelper.GetInstance().ExecuteSpQuery("SP_RECUPERAR_FACTURAS_POR_ID", param);


            if (dt != null && dt.Rows.Count > 0)
            {
                InvoiceDetail InDe = new InvoiceDetail();
                {
                    InDe.Invoice.Nroinvoice = (int)dt.Rows[0][0];
                    InDe.Invoice.Client = (string)dt.Rows[0][2];
                    InDe.Invoice.PaymentForm.Name = (string)dt.Rows[0][6];
                    InDe.Invoice.Date = (DateTime)dt.Rows[0][1];
                    InDe.Item.Name = (string)dt.Rows[0][3];
                    InDe.SellPrice = (int)(double)dt.Rows[0][4];
                    InDe.Amount = (int)dt.Rows[0][5];

                }
                ;

                return InDe;
            }

            return null;
        }
        



        public bool Save(Invoice invoice)
        {
            bool result = true;
            SqlTransaction? t = null;
            SqlConnection? cnn = null;
            try
            {
                cnn = DataHelper.GetInstance().GetConnection();

                cnn.Open();


                t = cnn.BeginTransaction();


                var cmd = new SqlCommand("SP_INSERTAR_MAESTRO_FACTURAS", cnn, t);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cliente", invoice.Client);
                cmd.Parameters.AddWithValue("@id_forma_pago", invoice.PaymentForm.Id);

                //parametro de salida
                SqlParameter param = new SqlParameter("@id", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();


                int InvoiceID = (int)param.Value;

                foreach (var detail in invoice.GetDetails())
                {

                    var cmdDetail = new SqlCommand("sp_AGREGAR_DETALLE", cnn, t);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmdDetail.Parameters.AddWithValue("@id_factura", InvoiceID);
                    cmdDetail.Parameters.AddWithValue("@id_articulo", detail.Item.Id);
                    cmdDetail.Parameters.AddWithValue("@cantidad", detail.Amount);
                    cmdDetail.Parameters.AddWithValue("@precio_vendido", detail.SellPrice);
                    cmdDetail.ExecuteNonQuery();




                    return result;

                }
                t.Commit();




            }
            catch (SqlException)
            {

                if (t != null)
                    t.Rollback();

                return result;
            }
            finally
            {
                if (cnn != null & cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }

            }


            return result;





            //List<SpParameter> param = new List<SpParameter>()
            //{
            //    new SpParameter("@id_forma_pago", invoice.PaymentForm.Id),
            //    new SpParameter("@id_cliente", invoice.Client.Id)
            //};

            //return DataHelper.GetInstance().ExecuteSpDml("SP_INSERTAR_MAESTRO_FACTURAS", param);

        }




    }
}
