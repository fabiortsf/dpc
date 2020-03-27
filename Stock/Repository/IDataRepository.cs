using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Repository
{
    internal interface IDataRepository<T>
    {
        void Inserir(T objeto);
        void Atualizar(T objeto);
        bool Excluir(T objeto);
        List<T> Listar();
        T Obter(object id);
        List<T> Pesquisar(T objeto);
    }
}
