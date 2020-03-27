using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Entity;

namespace Stock.Repository
{
    internal class ProdutoEstoqueRepository : RepositoryBase, IDataRepository<ProdutoEstoque>
    {
        public ProdutoEstoqueRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        #region "IDataRepository"

        public void Inserir(ProdutoEstoque objeto)
        {
            string sql = @"
INSERT INTO produto_estoque (
                    cd_filial,
                    cd_barra_sku,
                    nr_quantidade,
                    dt_ultima_atualizacao)
            VALUES (
                    @cd_filial,
                    @cd_barra_sku,
                    @nr_quantidade,
                    @dt_ultima_atualizacao)

SELECT @@IDENTITY AS ID 
";

            if (base.Connection.State == ConnectionState.Broken || base.Connection.State == ConnectionState.Closed)
                base.Connection.Open();

            IDbCommand command = base.Connection.CreateCommand();
            command.Transaction = base.Transaction;
            command.CommandText = sql;
            command.CommandTimeout = 3600;
            command.CommandType = CommandType.Text;

            command.Parameters.SetInputValue(command, "@cd_filial", objeto.CodigoFilial);
            command.Parameters.SetInputValue(command, "@cd_barra_sku", objeto.CodigoDeBarras);
            command.Parameters.SetInputValue(command, "@nr_quantidade", objeto.Quantidade);
            command.Parameters.SetInputValue(command, "@dt_ultima_atualizacao", objeto.DataUltimaAtualizacao);

            object id = command.ExecuteScalar();

            if (id != DBNull.Value)
                objeto.Id = base.ConvertToType<long>(id);
        }

        public void Atualizar(ProdutoEstoque objeto)
        {
            string sql = @"
UPDATE produto_estoque
SET nr_quantidade = @nr_quantidade,
    dt_ultima_atualizacao = @dt_ultima_atualizacao
WHERE 
    cd_barra_sku = @cd_barra_sku
";

            if (base.Connection.State == ConnectionState.Broken || base.Connection.State == ConnectionState.Closed)
                base.Connection.Open();

            IDbCommand command = base.Connection.CreateCommand();
            command.Transaction = base.Transaction;
            command.CommandText = sql;
            command.CommandTimeout = 3600;
            command.CommandType = CommandType.Text;

            command.Parameters.SetInputValue(command, "@nr_quantidade", objeto.Quantidade);
            command.Parameters.SetInputValue(command, "@dt_ultima_atualizacao", objeto.DataUltimaAtualizacao);
            command.Parameters.SetInputValue(command, "@cd_barra_sku", objeto.CodigoDeBarras);

            object id = command.ExecuteScalar();
        }

        public bool Excluir(ProdutoEstoque objeto)
        {
            throw new ApplicationException("Não implementado");
        }

        public List<ProdutoEstoque> Listar()
        {
            throw new ApplicationException("Não implementado");
        }

        public ProdutoEstoque Obter(object id)
        {
            ProdutoEstoque item = null;
            List<ProdutoEstoque> lista = new List<ProdutoEstoque>();
            lista = this.Pesquisar(new ProdutoEstoque()
            {
                CodigoDeBarras = base.ConvertToType<string>(id)
            });

            if (lista != null && lista.Count != 0)
                item = lista.First();

            return item;
        }

        public List<ProdutoEstoque> Pesquisar(ProdutoEstoque objeto)
        {
            List<ProdutoEstoque> lista = new List<ProdutoEstoque>();

            StringBuilder sql = new StringBuilder();
            sql.Append(@"
                SELECT [cd_produto_estoque],
                       [cd_filial]
                      ,[cd_barra_sku]
                      ,[nr_quantidade]
                      ,[dt_ultima_atualizacao]
                  FROM [produto_estoque]
                    WHERE 1 = 1 ");

            if (base.Connection.State == ConnectionState.Broken || base.Connection.State == ConnectionState.Closed)
                base.Connection.Open();

            IDbCommand command = base.Connection.CreateCommand();
            command.Transaction = base.Transaction;
            command.CommandText = sql.ToString();
            command.CommandTimeout = 3600;
            command.CommandType = CommandType.Text;

            #region "Filtros"
            if (!String.IsNullOrEmpty(objeto.CodigoFilial))
            {
                sql.Append("AND cd_filial = @cd_filial ");
                command.Parameters.SetInputValue(command, "@cd_filial", objeto.CodigoFilial);
            }
            if (!String.IsNullOrEmpty(objeto.CodigoDeBarras))
            {
                sql.Append("AND cd_barra_sku = @cd_barra_sku ");
                command.Parameters.SetInputValue(command, "@cd_barra_sku", objeto.CodigoDeBarras);
            }
            #endregion "Filtros"

            command.CommandText = sql.ToString();

            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ProdutoEstoque item = new ProdutoEstoque();
                    item.CodigoDeBarras = base.ConvertToType<string>(reader["cd_barra_sku"]);
                    item.CodigoFilial = base.ConvertToType<string>(reader["cd_filial"]);
                    item.DataUltimaAtualizacao = base.ConvertToType<DateTime>(reader["dt_ultima_atualizacao"]);
                    item.Id = base.ConvertToType<long>(reader["cd_produto_estoque"]);
                    item.Quantidade = base.ConvertToType<int>(reader["nr_quantidade"]);
                    lista.Add(item);
                }
            }

            return lista;
        }

        #endregion "IDataRepository"
    }
}
