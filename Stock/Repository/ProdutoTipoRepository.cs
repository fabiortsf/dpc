using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Entity;

namespace Stock.Repository
{
    internal class ProdutoTipoRepository : RepositoryBase, IDataRepository<ProdutoTipo>
     {
        public ProdutoTipoRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        #region "IDataRepository"

        public void Inserir(ProdutoTipo objeto)
        {
            throw new ApplicationException("Não implementado");
        }

        public void Atualizar(ProdutoTipo objeto)
        {
            throw new ApplicationException("Não implementado");
        }

        public bool Excluir(ProdutoTipo objeto)
        {
            throw new ApplicationException("Não implementado");
        }

        public List<ProdutoTipo> Listar()
        {
            return this.Pesquisar(new ProdutoTipo());
        }

        public ProdutoTipo Obter(object id)
        {
            ProdutoTipo item = null;
            List<ProdutoTipo> lista = new List<ProdutoTipo>();
            lista = this.Pesquisar(new ProdutoTipo()
            {
                Id = base.ConvertToType<int>(id)
            });

            if (lista != null && lista.Count != 0)
                item = lista.First();

            return item;
        }

        public List<ProdutoTipo> Pesquisar(ProdutoTipo objeto)
        {
            List<ProdutoTipo> lista = new List<ProdutoTipo>();

            StringBuilder sql = new StringBuilder();
            sql.Append(@"
SELECT [id_produto_tipo]
      ,[nm_tipo]
      ,[in_ativo]
  FROM [produto_tipo]
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
                sql.Append("AND id_produto_tipo = @id_produto_tipo ");
                command.Parameters.SetInputValue(command, "@id_produto_tipo", objeto.Id);
            }
            if (!String.IsNullOrEmpty(objeto.Descricao))
            {
                sql.Append("AND nm_tipo = @nm_tipo ");
                command.Parameters.SetInputValue(command, "@nm_tipo", objeto.Descricao);
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
                    ProdutoTipo item = new ProdutoTipo();
                    item.Ativo = base.ConvertToType<char>(reader["in_ativo"]);
                    item.Descricao = base.ConvertToType<string>(reader["nm_tipo"]);
                    item.Id = base.ConvertToType<int>(reader["id_produto_tipo"]);
                    lista.Add(item);
                }
            }

            return lista;
        }

        #endregion "IDataRepository"
    }
}
