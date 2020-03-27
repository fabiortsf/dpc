using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Stock.Entity;
using Stock.Repository;

namespace Stock.Service
{
    internal class UnitOfWork : IDisposable
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private bool _disposed;

        public UnitOfWork(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction(IsolationLevel.ReadCommitted);
        }


        #region "Propriedades"

        private ProdutoRepository _ProdutoRepository;
        public ProdutoRepository ProdutoRepository
        {
            get { return _ProdutoRepository ?? (_ProdutoRepository = new ProdutoRepository(_transaction)); }
        }

        private ProdutoSKURepository _ProdutoSKURepository;
        public ProdutoSKURepository ProdutoSKURepository
        {
            get { return _ProdutoSKURepository ?? (_ProdutoSKURepository = new ProdutoSKURepository(_transaction)); }
        }

        private ProdutoEstoqueRepository _ProdutoEstoqueRepository;
        public ProdutoEstoqueRepository ProdutoEstoqueRepository
        {
            get { return _ProdutoEstoqueRepository ?? (_ProdutoEstoqueRepository = new ProdutoEstoqueRepository(_transaction)); }
        }

        private ProdutoPrecoRepository _ProdutoPrecoRepository;
        public ProdutoPrecoRepository ProdutoPrecoRepository
        {
            get { return _ProdutoPrecoRepository ?? (_ProdutoPrecoRepository = new ProdutoPrecoRepository(_transaction)); }
        }

        private ProdutoTipoRepository _ProdutoTipoRepository;
        public ProdutoTipoRepository ProdutoTipoRepository
        {
            get { return _ProdutoTipoRepository ?? (_ProdutoTipoRepository = new ProdutoTipoRepository(_transaction)); }
        }


        private PedidoRepository _PedidoRepository;
        public PedidoRepository PedidoRepository
        {
            get { return _PedidoRepository ?? (_PedidoRepository = new PedidoRepository(_transaction)); }
        }

        private PedidoItemRepository _PedidoItemRepository;
        public PedidoItemRepository PedidoItemRepository
        {
            get { return _PedidoItemRepository ?? (_PedidoItemRepository = new PedidoItemRepository(_transaction)); }
        }

        private PerfilRepository _PerfilRepository;
        public PerfilRepository PerfilRepository
        {
            get { return _PerfilRepository ?? (_PerfilRepository = new PerfilRepository(_transaction)); }
        }

        private UsuarioRepository _UsuarioRepository;
        public UsuarioRepository UsuarioRepository
        {
            get { return _UsuarioRepository ?? (_UsuarioRepository = new UsuarioRepository(_transaction)); }
        }

        #endregion "Propriedades"


        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
        }

        public void Rollback()
        {
            try
            {
                _transaction.Rollback();
            }
            catch
            {
            }
        }


        private void resetRepositories()
        {
            _ProdutoRepository = null;
            _ProdutoSKURepository = null;
            _ProdutoEstoqueRepository = null;
            _PedidoRepository = null;
            _PedidoItemRepository = null;
            _PerfilRepository = null;
            _UsuarioRepository = null;
            _ProdutoPrecoRepository = null;
            _ProdutoTipoRepository = null;
        }


        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            dispose(false);
        }

    }
}
