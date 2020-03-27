using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Entity;

namespace Stock.Repository
{
    internal class ProdutoRepository : RepositoryBase, IDataRepository<Produto>
    {
        public ProdutoRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        #region "IDataRepository"

        public void Inserir(Produto objeto)
        {
            string sql = @"
INSERT INTO produto (cd_produto,
                    nm_produto,
                    id_produto_tipo)
            VALUES (@cd_produto,
                    @nm_produto,
                    @id_produto_tipo)  ";

            if (base.Connection.State == ConnectionState.Broken || base.Connection.State == ConnectionState.Closed)
                base.Connection.Open();

            IDbCommand command = base.Connection.CreateCommand();
            command.Transaction = base.Transaction;
            command.CommandText = sql;
            command.CommandTimeout = 3600;
            command.CommandType = CommandType.Text;

            command.Parameters.SetInputValue(command, "@cd_produto", objeto.Codigo);
            command.Parameters.SetInputValue(command, "@nm_produto", objeto.Nome);
            command.Parameters.SetInputValue(command, "@id_produto_tipo", objeto.IdTipo);

            object id = command.ExecuteNonQuery();
        }

        public void Atualizar(Produto objeto)
        {
            string sql = @"
UPDATE [produto]
   SET [nm_produto] = @nm_produto,
       [id_produto_tipo] = @id_produto_tipo
 WHERE cd_produto = @cd_produto
            ";

            if (base.Connection.State == ConnectionState.Broken || base.Connection.State == ConnectionState.Closed)
                base.Connection.Open();

            IDbCommand command = base.Connection.CreateCommand();
            command.Transaction = base.Transaction;
            command.CommandText = sql;
            command.CommandTimeout = 3600;
            command.CommandType = CommandType.Text;

            command.Parameters.SetInputValue(command, "@nm_produto", objeto.Nome);
            command.Parameters.SetInputValue(command, "@id_produto_tipo", objeto.IdTipo);
            command.Parameters.SetInputValue(command, "@cd_produto", objeto.Codigo);

            command.ExecuteNonQuery();
        }

        public bool Excluir(Produto objeto)
        {
            throw new ApplicationException("Não implementado");
        }

        public List<Produto> Listar()
        {
            throw new ApplicationException("Não implementado");
        }

        public Produto Obter(object id)
        {
            Produto item = null;
            List<Produto> lista = new List<Produto>();
            lista = this.Pesquisar(new Produto()
            {
                Codigo = base.ConvertToType<string>(id)
            });

            if (lista != null && lista.Count != 0)
                item = lista.First();

            return item;
        }

        public List<Produto> Pesquisar(Produto objeto)
        {
            List<Produto> lista = new List<Produto>();

            StringBuilder sql = new StringBuilder();
            sql.Append(@"
                SELECT [cd_produto]
                      ,[nm_produto]
                      ,[id_produto_tipo]
                  FROM [produto]
                    WHERE 1 = 1 ");

            if (base.Connection.State == ConnectionState.Broken || base.Connection.State == ConnectionState.Closed)
                base.Connection.Open();

            IDbCommand command = base.Connection.CreateCommand();
            command.Transaction = base.Transaction;
            command.CommandText = sql.ToString();
            command.CommandTimeout = 3600;
            command.CommandType = CommandType.Text;

            #region "Filtros"
            if (!String.IsNullOrEmpty(objeto.Codigo))
            {
                sql.Append("AND cd_produto = @cd_produto ");
                command.Parameters.SetInputValue(command, "@cd_produto", objeto.Codigo);
            }
            if (!String.IsNullOrEmpty(objeto.Nome))
            {
                sql.Append("AND nm_produto LIKE @nm_produto ");
                command.Parameters.SetInputValue(command, "@nm_produto", String.Format("%{0}%", objeto.Nome));
            }
            if (objeto.IdTipo != 0)
            {
                sql.Append("AND id_produto_tipo = @id_produto_tipo ");
                command.Parameters.SetInputValue(command, "@id_produto_tipo", objeto.IdTipo);
            }

            #endregion "Filtros"

            command.CommandText = sql.ToString();

            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Produto item = new Produto();
                    item.Codigo = base.ConvertToType<string>(reader["cd_produto"]);
                    item.IdTipo = base.ConvertToType<int>(reader["id_produto_tipo"]);
                    item.Nome = base.ConvertToType<string>(reader["nm_produto"]);
                    lista.Add(item);
                }
            }

            return lista;
        }

        #endregion "IDataRepository"
    }
}
