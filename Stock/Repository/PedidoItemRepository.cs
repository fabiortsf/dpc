using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Entity;

namespace Stock.Repository
{
    internal class PedidoItemRepository : RepositoryBase, IDataRepository<PedidoItem>
    {
        public PedidoItemRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        #region "IDataRepository"

        public void Inserir(PedidoItem objeto)
        {
            string sql = @"
INSERT INTO [pedido_item]
           ([id_pedido]
           ,[cd_produto]
           ,[cd_barra_sku]
           ,[vl_item])
     VALUES
           (@id_pedido
           ,@cd_produto
           ,@cd_barra_sku
           ,@vl_item)

SELECT @@IDENTITY AS ID  ";

            if (base.Connection.State == ConnectionState.Broken || base.Connection.State == ConnectionState.Closed)
                base.Connection.Open();

            IDbCommand command = base.Connection.CreateCommand();
            command.Transaction = base.Transaction;
            command.CommandText = sql;
            command.CommandTimeout = 3600;
            command.CommandType = CommandType.Text;

            command.Parameters.SetInputValue(command, "@id_pedido", objeto.IdPedido);
            command.Parameters.SetInputValue(command, "@cd_produto", objeto.codProduto);
            command.Parameters.SetInputValue(command, "@cd_barra_sku", objeto.codProdutoSKU);
            command.Parameters.SetInputValue(command, "@vl_item", objeto.Valor);

            object id = command.ExecuteScalar();

            if (id != DBNull.Value) objeto.Id = base.ConvertToType<int>(id);
        }

        public void Atualizar(PedidoItem objeto)
        {
            throw new ApplicationException("Não implementado");
        }

        public bool Excluir(PedidoItem objeto)
        {
            string sql = @"
            DELETE FROM [pedido_item] WHERE id_pedido_item = @id_pedido_item ";

            if (base.Connection.State == ConnectionState.Broken || base.Connection.State == ConnectionState.Closed)
                base.Connection.Open();

            IDbCommand command = base.Connection.CreateCommand();
            command.Transaction = base.Transaction;
            command.CommandText = sql;
            command.CommandTimeout = 3600;
            command.CommandType = CommandType.Text;

            command.Parameters.SetInputValue(command, "@id_pedido_item", objeto.Id);

            int exec = command.ExecuteNonQuery();

            return (exec != 0);
        }

        public List<PedidoItem> Listar()
        {
            throw new ApplicationException("Não implementado");
        }

        public PedidoItem Obter(object id)
        {
            PedidoItem item = null;
            List<PedidoItem> lista = new List<PedidoItem>();
            lista = this.Pesquisar(new PedidoItem()
            {
                Id = base.ConvertToType<int>(id)
            });

            if (lista != null && lista.Count != 0)
                item = lista.First();

            return item;
        }

        public List<PedidoItem> Pesquisar(PedidoItem objeto)
        {
            List<PedidoItem> lista = new List<PedidoItem>();

            StringBuilder sql = new StringBuilder();
            sql.Append(@"
                SELECT [id_pedido_item]
                      ,[id_pedido]
                      ,[cd_produto]
                      ,[cd_barra_sku]
                      ,[vl_item]
                  FROM [pedido_item]
                    WHERE 1 = 1 ");

            if (base.Connection.State == ConnectionState.Broken || base.Connection.State == ConnectionState.Closed)
                base.Connection.Open();

            IDbCommand command = base.Connection.CreateCommand();
            command.Transaction = base.Transaction;
            command.CommandText = sql.ToString();
            command.CommandTimeout = 3600;
            command.CommandType = CommandType.Text;

            #region "Filtros"
            if (objeto.Id != 0)
            {
                sql.Append("AND id_pedido_item = @id_pedido_item ");
                command.Parameters.SetInputValue(command, "@id_pedido_item", objeto.Id);
            }
            if (objeto.IdPedido != 0)
            {
                sql.Append("AND id_pedido = @id_pedido ");
                command.Parameters.SetInputValue(command, "@id_pedido", objeto.IdPedido);
            }
            if (!String.IsNullOrEmpty(objeto.codProduto))
            {
                sql.Append("AND cd_produto = @cd_produto ");
                command.Parameters.SetInputValue(command, "@cd_produto", objeto.codProduto);
            }
            if (!String.IsNullOrEmpty(objeto.codProdutoSKU))
            {
                sql.Append("AND cd_barra_sku = @cd_barra_sku ");
                command.Parameters.SetInputValue(command, "@cd_barra_sku", objeto.codProdutoSKU);
            }

            #endregion "Filtros"

            command.CommandText = sql.ToString();

            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    PedidoItem item = new PedidoItem();
                    item.codProduto = base.ConvertToType<string>(reader["cd_produto"]);
                    item.codProdutoSKU = base.ConvertToType<string>(reader["cd_barra_sku"]);
                    item.Id = base.ConvertToType<int>(reader["id_pedido_item"]);
                    item.IdPedido = base.ConvertToType<int>(reader["id_pedido"]);
                    item.Valor = base.ConvertToType<decimal>(reader["vl_item"]);
                    lista.Add(item);
                }
            }

            return lista;
        }

        #endregion "IDataRepository"
    }
}
