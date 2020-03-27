using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace Stock.Repository
{
    public class RepositoryBase
    {
        protected IDbTransaction Transaction { get; private set; }
        protected IDbConnection Connection { get { return Transaction.Connection; } }

        public RepositoryBase(IDbTransaction transaction)
        {
            Transaction = transaction;
        }

        /// <summary>
        /// Converte um objeto qualquer para um tipo genérico especificado.
        /// </summary>
        /// <typeparam name="T">Tipo do objeto de saída</typeparam>
        /// <param name="meuObjeto">Objeto a ser convertido</param>
        /// <returns>Objeto do tipo fornecido</returns>
        public T ConvertToType<T>(object meuObjeto)
        {
            if (meuObjeto.GetType().ToString() == "System.DBNull")
                return default(T);

            Type conversionType = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);

            try
            {
                if (meuObjeto.GetType().ToString() == "System.String" && meuObjeto != null)
                    return (T)Convert.ChangeType(meuObjeto.ToString().Trim(), conversionType);

                return (T)Convert.ChangeType(meuObjeto, conversionType);
            }
            catch
            {
                return default(T);
            }
        }

    }
}
