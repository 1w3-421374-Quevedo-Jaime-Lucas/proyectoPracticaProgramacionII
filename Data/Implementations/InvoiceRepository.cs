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
    public class InvoiceRepository : IInvoiceRepository
    {
        InvoiceDetail InDe = new InvoiceDetail();
        PaymentForm paymentForm = new PaymentForm();



        public List<Invoice> GetAll()
        {
            var invoices = new Dictionary<int, Invoice>(); // clave: Nroinvoice
            var dt = DataHelper.GetInstance().ExecuteSpQuery("SP_RECUPERAR_FACTURAS");

            foreach (DataRow row in dt.Rows)
            {
                InDe.Item = new Item();
                InDe.Item.Name = row[3].ToString();
                InDe.SellPrice = Convert.ToInt32(row[4]);
                InDe.Amount = Convert.ToInt32(row[5]);

                int nroInvoice = Convert.ToInt32(row[0]);

                if (!invoices.ContainsKey(nroInvoice))
                {
                    Invoice invoice = new Invoice(InDe);

                    invoice.Nroinvoice = nroInvoice;
                    invoice.Date = (DateTime)row[1];
                    invoice.Client = row[2].ToString();

                    invoice.PaymentForm = new PaymentForm();
                    invoice.PaymentForm.Name = row[6].ToString();

                    invoices.Add(nroInvoice, invoice);
                }
                else
                {
                    Invoice invoice = invoices[nroInvoice];

                    invoice.AddDetails(InDe);
                }
            }

            return invoices.Values.ToList();
        }





        public List<Invoice> GetById(int id)
        {
            List<SpParameter> param = new List<SpParameter>()
    {
        new SpParameter() { Name = "@nro_factura", Valor = id }
    };

            var dt = DataHelper.GetInstance().ExecuteSpQuery("SP_RECUPERAR_FACTURAS_POR_ID", param);

            var invoices = new Dictionary<int, Invoice>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    InDe.Item = new Item();
                    InDe.Item.Name = row[3].ToString();
                    InDe.SellPrice = Convert.ToInt32(row[4]);
                    InDe.Amount = Convert.ToInt32(row[5]);

                    int nroInvoice = Convert.ToInt32(row[0]);

                    if (!invoices.ContainsKey(nroInvoice))
                    {
                        Invoice invoice = new Invoice(InDe);

                        invoice.Nroinvoice = nroInvoice;
                        invoice.Date = (DateTime)row[1];
                        invoice.Client = row[2].ToString();

                        invoice.PaymentForm = new PaymentForm();
                        invoice.PaymentForm.Name = row[6].ToString();

                        invoices.Add(nroInvoice, invoice);
                    }
                    else
                    {
                        Invoice invoice = invoices[nroInvoice];

                        invoice.AddDetails(InDe);
                    }

                }

                return invoices.Values.ToList();
            }

            return new List<Invoice>();
        }




        //Transaccion de facturas y detalles facturas. No utilizo el UnitofWork porque no entiendo su funcionamiento.
        public Invoice Save(Invoice invoice)
        {
            SqlTransaction? t = null;
            SqlConnection? cnn = null;

            try
            {
                cnn = DataHelper.GetInstance().GetConnection();
                cnn.Open();
                t = cnn.BeginTransaction();


                var cmd = new SqlCommand("SP_INSERTAR_MAESTRO_FACTURAS", cnn, t)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@cliente", invoice.Client);
                cmd.Parameters.AddWithValue("@id_forma_pago", invoice.PaymentForm.Id);

                SqlParameter param = new SqlParameter("@id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(param);

                cmd.ExecuteNonQuery();
                int InvoiceID = (int)param.Value;


                invoice.Nroinvoice = InvoiceID;

                foreach (var detail in invoice.GetDetails())
                {
                    var cmdDetail = new SqlCommand("sp_AGREGAR_DETALLE", cnn, t)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmdDetail.Parameters.AddWithValue("@id_factura", InvoiceID);
                    cmdDetail.Parameters.AddWithValue("@id_articulo", detail.Item.Id);
                    cmdDetail.Parameters.AddWithValue("@cantidad", detail.Amount);
                    cmdDetail.Parameters.AddWithValue("@precio_vendido", detail.SellPrice);

                    cmdDetail.ExecuteNonQuery();
                }

                t.Commit();


                return invoice;
            }
            catch (SqlException)
            {
                t?.Rollback();
                throw;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
        }

    }
}
