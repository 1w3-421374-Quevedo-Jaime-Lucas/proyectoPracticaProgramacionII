using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using PIIPractica01.Data.Implementations;
using PIIPractica01.Data.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PIIPractica01.Data.Helpers
{
    internal class UnitOfWork : IDisposable
    {

        //clase UnitOfWork creada pero sin utilizar porque no entiendo como utilizarla.
        //Vi las clases de Martin Polliotto y Ruben Botta pero no explican como utilizarlo. Explican para que sirve, pero no explican como usarlo.
        //Ademas busque videos en youtube pero no lo comprendi el como implementarlo.


        private readonly SqlConnection _connection;
        private SqlTransaction _transaction;
        private IInvoiceRepository _invoiceRepository;
        public UnitOfWork(string cnnString)
        {
            _connection = new SqlConnection(cnnString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }
        public IInvoiceRepository InvoiceRepository
        {
            get
            {
                if (_invoiceRepository == null)
                {
                    _invoiceRepository = new InvoiceRepository(_connection);
                }
                return _invoiceRepository;
            }

        }

        public void SaveChanges()
        {
            try
            {
                _transaction.Commit();
            }
            catch (Exception ex)
            {
                _transaction.Rollback();
                throw new Exception("Error al guardar cambios en la base de datos.", ex);
            }
        }
        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
            }
            if (_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
            }
        }
    }
}