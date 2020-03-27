using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Entity;

namespace Stock.Repository
{
    internal class PedidoRepository : RepositoryBase, IDataRepository<Pedido>
    {
        public PedidoRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        #region "IDataRepository"

        public void Inserir(Pedido objeto)
        {
            string sql = @"
INSERT INTO [pedido]
           ([dt_pedido]
           ,[id_tipo_pedido]
           ,[vl_pedido])
     VALUES
           (@dt_pedido
           ,@id_tipo_pedido
           ,@vl_pedido)

SELECT @@IDENTITY AS ID  ";

            if (base.Connection.State == ConnectionState.Broken || base.Connection.State == ConnectionState.Closed)
                base.Connection.Open();

            IDbCommand command = base.Connection.CreateCommand();
            command.Transaction = base.Transaction;
            command.CommandText = sql;
            command.CommandTimeout = 3600;
            command.CommandType = CommandType.Text;

            command.Parameters.SetInputValue(command, "@dt_pedido", objeto.DataPedido);
            command.Parameters.SetInputValue(command, "@id_tipo_pedido", objeto.IdTipo);
            command.Parameters.SetInputValue(command, "@vl_pedido", objeto.Valor);

            object id = command.ExecuteScalar();

            if (id != DBNull.Value) objeto.Id = base.ConvertToType<int>(id);
        }

        public void Atualizar(Pedido objeto)
        {
            string sql = @"
UPDATE [pedido]
   SET [vl_pedido] = @vl_pedido
 WHERE id_pedido = @id_pedido
            ";

            if (base.Connection.State == ConnectionState.Broken || base.Connection.State == ConnectionState.Closed)
                base.Connection.Open();

            IDbCommand command = base.Connection.CreateCommand();
            command.Transaction = base.Transaction;
            command.CommandText = sql;
            command.CommandTimeout = 3600;
            command.CommandType = CommandType.Text;

            command.Parameters.SetInputValue(command, "@vl_pedido", objeto.Valor);
            command.Parameters.SetInputValue(command, "@id_pedido", objeto.Id);

            command.ExecuteNonQuery();
        }

        public bool Excluir(Pedido objeto)
        {
            throw new ApplicationException("Não implementado");
        }

        public List<Pedido> Listar()
        {
            throw new ApplicationException("Não implementado");
        }

        public Pedido Obter(object id)
        {
            Pedido item = null;
            List<Pedido> lista = new List<Pedido>();
            lista = this.Pesquisar(new Pedido()
            {
                Id = base.ConvertToType<int>(id)
            });

            if (lista != null && lista.Count != 0)
                item = lista.First();

            return item;
        }

        public List<Pedido> Pesquisar(Pedido objeto)
        {
            List<Pedido> lista = new List<Pedido>();

            StringBuilder sql = new StringBuilder();
            sql.Append(@"
                SELECT [id_pedido]
                      ,[dt_pedido]
                      ,[id_tipo_pedido]
                      ,[vl_pedido]
                  FROM [pedido]
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
                sql.Append("AND id_pedido = @id_pedido ");
                command.Parameters.SetInputValue(command, "@id_pedido", objeto.Id);
            }
            if (objeto.IdTipo != 0)
            {
                sql.Append("AND id_tipo_pedido = @id_tipo_pedido ");
                command.Parameters.SetInputValue(command, "@id_tipo_pedido", objeto.IdTipo);
            }

            #endregion "Filtros"

            command.CommandText = sql.ToString();

            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Pedido item = new Pedido();
                    item.Id = base.ConvertToType<int>(reader["id_pedido"]);
                    item.DataPedido = base.ConvertToType<DateTime>(reader["dt_pedido"]);
                    item.IdTipo = base.ConvertToType<int>(reader["id_tipo_pedido"]);
                    item.Valor = base.ConvertToType<decimal>(reader["vl_pedido"]);
                    lista.Add(item);
                }
            }

            return lista;
        }

        #endregion "IDataRepository"
    }
}
