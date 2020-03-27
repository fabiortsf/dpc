using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Repository
{
    public static class ExtencionMethods
    {
        /// <summary>
        /// Adiciona parâmetros de entrada à comandos SQL.
        /// </summary>
        /// <param name="parameterCollection">IDataParameterCollection</param>
        /// <param name="command">IDbCommand</param>
        /// <param name="parameterName">Nome do parâmetro</param>
        /// <param name="parameterValue">Valor do parâmetro</param>
        /// <returns></returns>
        public static IDataParameterCollection SetInputValue(this IDataParameterCollection parameterCollection, IDbCommand command, string parameterName, object parameterValue)
        {
            IDataParameterCollection prC = parameterCollection;
            Type type = Type.GetType("System.String");

            IDbDataParameter param = command.CreateParameter();
            param.ParameterName = parameterName;
            param.Direction = ParameterDirection.Input;

            try
            {
                type = parameterValue.GetType();
            }
            catch
            {
                param.Value = DBNull.Value;
                prC.Add(param);
                return prC;
            }

            if ((type == typeof(System.String)) && (String.IsNullOrEmpty(parameterValue.ToString())))
            {
                param.Value = DBNull.Value;
                prC.Add(param);
            }
            else if ((type == typeof(System.DateTime)) && (((DateTime)parameterValue) == DateTime.MinValue))
            {
                param.Value = DBNull.Value;
                prC.Add(param);
            }
            else if ((type == typeof(System.Char)) && (((Char)parameterValue) == Char.MinValue))
            {
                param.Value = DBNull.Value;
                prC.Add(param);
            }
            else if ((type == typeof(System.Int64)) && (((Int64)parameterValue) == Int64.MinValue))
            {
                param.Value = DBNull.Value;
                prC.Add(param);
            }
            else
            {
                param.Value = parameterValue;
                prC.Add(param);
            }

            return prC;

            #region "Original"
            //            IDataParameterCollection prC = parameterCollection;
            //            Type type = Type.GetType("System.String");

            //            IDbDataParameter param = command.CreateParameter();
            //            param.ParameterName = parameterName;
            //            param.Direction = ParameterDirection.Input;

            //            try {
            //                type = parameterValue.GetType();
            //            } catch {
            //                param.Value = DBNull.Value;
            //                goto FimSetInputValue;
            //            }

            //            if ((type == typeof(System.String)) && (String.IsNullOrEmpty(parameterValue.ToString()))) {
            //                param.Value = DBNull.Value;
            //                goto FimSetInputValue;
            //            } else if ((type == typeof(System.DateTime)) && (((DateTime)parameterValue) == DateTime.MinValue)) {
            //                param.Value = DBNull.Value;
            //                goto FimSetInputValue;
            //            } else if ((type == typeof(System.Char)) && (((Char)parameterValue) == Char.MinValue)) {
            //                param.Value = DBNull.Value;
            //                goto FimSetInputValue;
            //            } else if ((type == typeof(System.Int64)) && (((Int64)parameterValue) == Int64.MinValue)) {
            //                param.Value = DBNull.Value;
            //                goto FimSetInputValue;
            //            } else param.Value = parameterValue;

            //FimSetInputValue:

            //            prC.Add(param);

            //            return prC;
            #endregion "Original"
        }

        /// <summary>
        /// Adiciona parâmetros de saída à comandos SQL.
        /// </summary>
        /// <param name="parameterCollection">IDataParameterCollection</param>
        /// <param name="command">IDbCommand</param>
        /// <param name="parameterName">Nome do parâmetro</param>
        /// <param name="parameterValue">Valor do parâmetro</param>
        /// <returns></returns>
        public static IDataParameterCollection SetOutputValue(this IDataParameterCollection parameterCollection, IDbCommand command, string parameterName, object parameterValue)
        {
            IDataParameterCollection prC = parameterCollection;

            IDbDataParameter param = command.CreateParameter();
            param.ParameterName = parameterName;
            param.Value = parameterValue;
            param.Direction = ParameterDirection.Output;
            prC.Add(param);

            return prC;
        }

        //public static IDbDataParameter SetInputValue(this IDbDataParameter parameter, string parameterName, string parameterValue) {
        //    IDbDataParameter param = parameter;
        //    param.ParameterName = parameterName;
        //    param.Value = parameterValue;
        //    param.Direction = ParameterDirection.Input;
        //    return param;
        //}

        //public static IDbDataParameter SetOutputValue(this IDbDataParameter parameter, string parameterName, string parameterValue) {
        //    IDbDataParameter param = parameter;
        //    param.ParameterName = parameterName;
        //    param.Value = parameterValue;
        //    param.Direction = ParameterDirection.Input;
        //    return param;
        //}
    }
}
