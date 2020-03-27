using Stock.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Repository
{
    internal class UsuarioRepository : RepositoryBase, IDataRepository<Usuario>
    {
        public UsuarioRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        #region "IDataRepository"

        public void Inserir(Usuario objeto)
        {
            throw new ApplicationException("Não implementado");
        }

        public void Atualizar(Usuario objeto)
        {
            throw new ApplicationException("Não implementado");
        }

        public bool Excluir(Usuario objeto)
        {
            throw new ApplicationException("Não implementado");
        }

        public List<Usuario> Listar()
        {
            return this.Pesquisar(new Usuario());
        }

        public Usuario Obter(object id)
        {
            Usuario item = null;
            List<Usuario> lista = new List<Usuario>();
            lista = this.Pesquisar(new Usuario()
            {
                Id = base.ConvertToType<int>(id)
            });

            if (lista != null && lista.Count != 0)
                item = lista.First();

            return item;
        }

        public List<Usuario> Pesquisar(Usuario objeto)
        {
            List<Usuario> lista = new List<Usuario>();

            StringBuilder sql = new StringBuilder();
            sql.Append(@"
SELECT [id_usuario]
      ,[nm_usuario]
      ,[nm_email]
      ,[ds_senha]
  FROM [usuario]
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
                sql.Append("AND id_usuario = @id_usuario ");
                command.Parameters.SetInputValue(command, "@id_usuario", objeto.Id);
            }
            if (!String.IsNullOrEmpty(objeto.Nome))
            {
                sql.Append("AND nm_usuario LIKE @nm_usuario ");
                command.Parameters.SetInputValue(command, "@nm_usuario", String.Format("%{0}%", objeto.Nome));
            }
            if (!String.IsNullOrEmpty(objeto.Email))
            {
                sql.Append("AND nm_email = @nm_email ");
                command.Parameters.SetInputValue(command, "@nm_email", objeto.Email);
            }
            if (!String.IsNullOrEmpty(objeto.Senha))
            {
                sql.Append("AND ds_senha = @ds_senha ");
                command.Parameters.SetInputValue(command, "@ds_senha", objeto.Senha);
            }

            #endregion "Filtros"

            command.CommandText = sql.ToString();

            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Usuario item = new Usuario();
                    item.Email = base.ConvertToType<string>(reader["nm_email"]);
                    item.Id = base.ConvertToType<int>(reader["id_usuario"]);
                    item.Nome = base.ConvertToType<string>(reader["nm_usuario"]);
                    item.Senha = base.ConvertToType<string>(reader["ds_senha"]);
                    lista.Add(item);
                }
            }

            return lista;
        }

        #endregion "IDataRepository"
    }
}
