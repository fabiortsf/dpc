using Stock.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Repository
{
    internal class ProdutoPrecoRepository : RepositoryBase, IDataRepository<ProdutoPreco>
    {
        public ProdutoPrecoRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        #region "IDataRepository"

        public void Inserir(ProdutoPreco objeto)
        {
            string sql = @"
INSERT INTO [produto_preco]
           ([cd_barra_sku]
           ,[cd_tabela]
           ,[vl_venda]
           ,[in_ativo])
     VALUES
           (@cd_barra_sku
           ,@cd_tabela
           ,@vl_venda
           ,@in_ativo)  ";

            if (base.Connection.State == ConnectionState.Broken || base.Connection.State == ConnectionState.Closed)
                base.Connection.Open();

            IDbCommand command = base.Connection.CreateCommand();
            command.Transaction = base.Transaction;
            command.CommandText = sql;
            command.CommandTimeout = 3600;
            command.CommandType = CommandType.Text;

            command.Parameters.SetInputValue(command, "@cd_barra_sku", objeto.CodigoBarras);
            command.Parameters.SetInputValue(command, "@cd_tabela", objeto.CodTabelaPreco);
            command.Parameters.SetInputValue(command, "@vl_venda", objeto.Preco);
            command.Parameters.SetInputValue(command, "@in_ativo", objeto.Ativo);

            object id = command.ExecuteNonQuery();
        }

        public void Atualizar(ProdutoPreco objeto)
        {
            string sql = @"
UPDATE [produto_preco]
   SET [vl_venda] = @vl_venda,
       in_ativo = @in_ativo
 WHERE cd_barra_sku = @cd_barra_sku
AND cd_tabela = @cd_tabela 
            ";

            if (base.Connection.State == ConnectionState.Broken || base.Connection.State == ConnectionState.Closed)
                base.Connection.Open();

            IDbCommand command = base.Connection.CreateCommand();
            command.Transaction = base.Transaction;
            command.CommandText = sql;
            command.CommandTimeout = 3600;
            command.CommandType = CommandType.Text;

            command.Parameters.SetInputValue(command, "@vl_venda", objeto.Preco);
            command.Parameters.SetInputValue(command, "@in_ativo", objeto.Ativo);
            command.Parameters.SetInputValue(command, "@cd_barra_sku", objeto.CodigoBarras);
            command.Parameters.SetInputValue(command, "@cd_tabela", objeto.CodTabelaPreco);

            command.ExecuteNonQuery();
        }

        public bool Excluir(ProdutoPreco objeto)
        {
            string sql = @"
DELETE FROM [produto_preco]
 WHERE cd_barra_sku = @cd_barra_sku
AND in_ativo = @in_ativo 
            ";

            if (base.Connection.State == ConnectionState.Broken || base.Connection.State == ConnectionState.Closed)
                base.Connection.Open();

            IDbCommand command = base.Connection.CreateCommand();
            command.Transaction = base.Transaction;
            command.CommandText = sql;
            command.CommandTimeout = 3600;
            command.CommandType = CommandType.Text;

            command.Parameters.SetInputValue(command, "@cd_barra_sku", objeto.CodigoBarras);
            command.Parameters.SetInputValue(command, "@cd_tabela", objeto.CodTabelaPreco);

            int executou = command.ExecuteNonQuery();

            return executou != 0;
        }

        public List<ProdutoPreco> Listar()
        {
            throw new ApplicationException("Não implementado");
        }

        public ProdutoPreco Obter(object id)
        {
            ProdutoPreco item = null;
            Type valueType = id.GetType();
            if (valueType.IsArray)
            {
                object[] chave = base.ConvertToType<object[]>(id);
                List<ProdutoPreco> lista = this.Pesquisar(new ProdutoPreco()
                {
                    CodigoBarras = base.ConvertToType<string>(chave[0]),
                    CodTabelaPreco = base.ConvertToType<string>(chave[1])
                });

                if (lista.Count != 0)
                    item = lista[0];
            }
            return item;
        }

        public List<ProdutoPreco> Pesquisar(ProdutoPreco objeto)
        {
            List<ProdutoPreco> lista = new List<ProdutoPreco>();

            StringBuilder sql = new StringBuilder();
            sql.Append(@"
        SELECT [cd_barra_sku]
              ,[cd_tabela]
              ,[vl_venda]
              ,[in_ativo]
          FROM [produto_preco]
                    WHERE 1 = 1 ");

            if (base.Connection.State == ConnectionState.Broken || base.Connection.State == ConnectionState.Closed)
                base.Connection.Open();

            IDbCommand command = base.Connection.CreateCommand();
            command.Transaction = base.Transaction;
            command.CommandText = sql.ToString();
            command.CommandTimeout = 3600;
            command.CommandType = CommandType.Text;

            #region "Filtros"
            if (!String.IsNullOrEmpty( objeto.CodigoBarras ))
            {
                sql.Append("AND cd_barra_sku = @cd_barra_sku ");
                command.Parameters.SetInputValue(command, "@cd_barra_sku", objeto.CodigoBarras);
            }
            if (!String.IsNullOrEmpty(objeto.CodTabelaPreco))
            {
                sql.Append("AND cd_tabela = @cd_tabela ");
                command.Parameters.SetInputValue(command, "@cd_tabela", objeto.CodTabelaPreco);
            }
            if (objeto.Ativo != Char.MinValue)
            {
                sql.Append("AND in_ativo = @in_ativo ");
                command.Parameters.SetInputValue(command, "@in_ativo", objeto.Ativo);
            }
            #endregion "Filtros"

            command.CommandText = sql.ToString();

            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ProdutoPreco item = new ProdutoPreco();
                    item.Ativo = base.ConvertToType<char>(reader["Ativo"]);
                    item.CodigoBarras = base.ConvertToType<string>(reader["cd_barra_sku"]);
                    item.CodTabelaPreco = base.ConvertToType<string>(reader["cd_tabela"]);
                    item.Preco = base.ConvertToType<decimal>(reader["vl_venda"]);
                    lista.Add(item);
                }
            }

            return lista;
        }

        #endregion "IDataRepository"
    }
}
