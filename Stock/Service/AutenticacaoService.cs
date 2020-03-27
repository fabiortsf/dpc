using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Entity;

namespace Stock.Service
{
    public class AutenticacaoService: BaseService
    {
        //public Usuario Login(string email, string senha)
        //{
        //    Usuario usuario = default(Usuario);
        //    List<Usuario> listaPesquisa = new List<Usuario>();

        //    string connectionString = base.connectionString;
        //    using (UnitOfWork uow = new UnitOfWork(connectionString))
        //    {
        //        try
        //        {
        //            listaPesquisa = uow.UsuarioRepository.Pesquisar(new Usuario() { Email = email });


        //            uow.Commit();
        //        }
        //        catch (Exception ex)
        //        {
        //            uow.Rollback();
        //            throw new ApplicationException(ex.Message, ex);
        //        }
        //    }

        //    if (v_oListaPesquisa != null && v_oListaPesquisa.Count != 0)
        //        usuario = v_oListaPesquisa.Find(x => x.Tamanho.Equals("Único", StringComparison.CurrentCultureIgnoreCase));

        //    return usuario;
        //}
    }
}
