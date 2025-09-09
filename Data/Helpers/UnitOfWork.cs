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

        private readonly SqlConnection _connection;
        private SqlTransaction _transaction;
        private IInvoiceRepository _productoRepository;
        public UnitOfWork(string cnnString)
        {
            _connection = new SqlConnection(cnnString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }
        public IInvoiceRepository ProductoRepository
        {
            get
            {
                if (_productoRepository == null)
                {
                    _productoRepository = new InvoiceRepository(_connection);
                }
                return _productoRepository;
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