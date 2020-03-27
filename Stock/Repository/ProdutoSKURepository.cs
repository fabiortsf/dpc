using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Entity;

namespace Stock.Repository
{
    internal class ProdutoSKURepository : RepositoryBase, IDataRepository<ProdutoSKU>
    {
        public ProdutoSKURepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        #region "IDataRepository"

        public void Inserir(ProdutoSKU objeto)
        {
            string sql = @"
INSERT INTO produto_sku (cd_barra_sku,
                    cd_produto)
            VALUES (@cd_barra_sku,
                    @cd_produto)
";

            if (base.Connection.State == ConnectionState.Broken || base.Connection.State == ConnectionState.Closed)
                base.Connection.Open();

            IDbCommand command = base.Connection.CreateCommand();
            command.Transaction = base.Transaction;
            command.CommandText = sql;
            command.CommandTimeout = 3600;
            command.CommandType = CommandType.Text;

            command.Parameters.SetInputValue(command, "@cd_barra_sku", objeto.CodigoDeBarras);
            command.Parameters.SetInputValue(command, "@cd_produto", objeto.CodigoProduto);

            object id = command.ExecuteNonQuery();
        }

        public void Atualizar(ProdutoSKU objeto)
        {
            throw new ApplicationException("Não implementado");
        }

        public bool Excluir(ProdutoSKU objeto)
        {
            string sql = @"
                DELETE FROM produto_sku WHERE cd_barra_sku = @cd_barra_sku ";

            if (base.Connection.State == ConnectionState.Broken || base.Connection.State == ConnectionState.Closed)
                base.Connection.Open();

            IDbCommand command = base.Connection.CreateCommand();
            command.Transaction = base.Transaction;
            command.CommandText = sql;
            command.CommandTimeout = 3600;
            command.CommandType = CommandType.Text;

            command.Parameters.SetInputValue(command, "@cd_barra_sku", objeto.CodigoDeBarras);

            int id = command.ExecuteNonQuery();

            return (id != 0);
        }

        public List<ProdutoSKU> Listar()
        {
            throw new ApplicationException("Não implementado");
        }

        public ProdutoSKU Obter(object id)
        {
            ProdutoSKU item = null;
            List<ProdutoSKU> lista = new List<ProdutoSKU>();
            lista = this.Pesquisar(new ProdutoSKU()
            {
                CodigoDeBarras = base.ConvertToType<string>(id)
            });

            if (lista != null && lista.Count != 0)
                item = lista.First();

            return item;
        }

        public List<ProdutoSKU> Pesquisar(ProdutoSKU objeto)
        {
            List<ProdutoSKU> lista = new List<ProdutoSKU>();

            StringBuilder sql = new StringBuilder();
            sql.Append(@"
                SELECT [cd_barra_sku]
                      ,[cd_produto]
                  FROM [produto_sku]
                    WHERE 1 = 1 ");

            if (base.Connection.State == ConnectionState.Broken || base.Connection.State == ConnectionState.Closed)
                base.Connection.Open();

            IDbCommand command = base.Connection.CreateCommand();
            command.Transaction = base.Transaction;
            command.CommandText = sql.ToString();
            command.CommandTimeout = 3600;
            command.CommandType = CommandType.Text;

            #region "Filtros"
            if (!String.IsNullOrEmpty(objeto.CodigoDeBarras))
            {
                sql.Append("AND cd_barra_sku = @cd_barra_sku ");
                command.Parameters.SetInputValue(command, "@cd_barra_sku", objeto.CodigoDeBarras);
            }
            if (!String.IsNullOrEmpty(objeto.CodigoProduto))
            {
                sql.Append("AND cd_produto = @cd_produto ");
                command.Parameters.SetInputValue(command, "@cd_produto", objeto.CodigoProduto);
            }
            #endregion "Filtros"

            command.CommandText = sql.ToString();

            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ProdutoSKU item = new ProdutoSKU();
                    item.CodigoDeBarras = base.ConvertToType<string>(reader["cd_barra_sku"]);
                    item.CodigoProduto = base.ConvertToType<string>(reader["cd_produto"]);
                    lista.Add(item);
                }
            }

            return lista;
        }

        #endregion "IDataRepository"
    }
}
