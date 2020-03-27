using Stock.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Repository
{
    internal class PerfilRepository : RepositoryBase, IDataRepository<Perfil>
    {
        public PerfilRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        #region "IDataRepository"

        public void Inserir(Perfil objeto)
        {
            throw new ApplicationException("Não implementado");
        }

        public void Atualizar(Perfil objeto)
        {
            throw new ApplicationException("Não implementado");
        }

        public bool Excluir(Perfil objeto)
        {
            throw new ApplicationException("Não implementado");
        }

        public List<Perfil> Listar()
        {
            return this.Pesquisar(new Perfil());
        }

        public Perfil Obter(object id)
        {
            Perfil item = null;
            List<Perfil> lista = new List<Perfil>();
            lista = this.Pesquisar(new Perfil()
            {
                Id = base.ConvertToType<int>(id)
            });

            if (lista != null && lista.Count != 0)
                item = lista.First();

            return item;
        }

        public List<Perfil> Pesquisar(Perfil objeto)
        {
            List<Perfil> lista = new List<Perfil>();

            StringBuilder sql = new StringBuilder();
            sql.Append(@"
                    SELECT id_perfil,nm_perfil,ds_perfil,cd_perfil_pai
                    FROM perfil_de_acesso
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
                sql.Append("AND id_perfil = @id_perfil ");
                command.Parameters.SetInputValue(command, "@id_perfil", objeto.Id);
            }
            if (!String.IsNullOrEmpty(objeto.Nome))
            {
                sql.Append("AND nm_perfil = @nm_perfil ");
                command.Parameters.SetInputValue(command, "@nm_perfil", objeto.Nome);
            }
            if (!String.IsNullOrEmpty(objeto.Descricao))
            {
                sql.Append("AND ds_perfil LIKE @ds_perfil ");
                command.Parameters.SetInputValue(command, "@ds_perfil", String.Format("%{0}%", objeto.Descricao));
            }
            if (objeto.IdPai.HasValue)
            {
                sql.Append("AND cd_perfil_pai = @cd_perfil_pai ");
                command.Parameters.SetInputValue(command, "@cd_perfil_pai", objeto.IdPai);
            }

            #endregion "Filtros"

            command.CommandText = sql.ToString();

            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Perfil item = new Perfil();
                    item.Descricao = base.ConvertToType<string>(reader["id_perfil"]);
                    item.Nome = base.ConvertToType<string>(reader["nm_perfil"]);
                    item.Descricao = base.ConvertToType<string>(reader["ds_perfil"]);
                    item.IdPai = base.ConvertToType<int>(reader["cd_perfil_pai"]);
                    lista.Add(item);
                }
            }

            return lista;
        }

        #endregion "IDataRepository"
    }
}
