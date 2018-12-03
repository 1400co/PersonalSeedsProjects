using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace PersonalFramework
{
    public static class Helper<T>
    {

        #region Privados
        /// <summary>
        /// Mapea una entidad desde la BD
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static IList<T> MapearEntidad(System.Data.IDataReader reader)
        {
            IList<T> Result = new List<T>();

            if (!reader.IsClosed)
            {
                while (reader.Read())
                {
                    T Entidad = Reflection.createObject<T>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                       
                        String columnName = "";
                        
                        columnName = reader.GetName(i);

                        Object value = GetValue(reader, columnName);
                        if (value != null)
                            Reflection.setValueProperty(Entidad, GetPropertyName(TitleCaseString(columnName)), value);
                    }
                    Result.Add(Entidad);
                }
            }

            return Result;
        }

        private static String TitleCaseString(String s)
        {

            //s = s.ToLower();
            if (s == null) return s;

            if (!s.Contains("_"))
            {
                return s.First().ToString().ToUpper() + String.Join("", s.Skip(1));
            }

            String[] words = s.Split('_');

           
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length == 0) continue;

                Char firstChar = Char.ToUpper(words[i][0]);
                String rest = "";
                if (words[i].Length > 1)
                {
                    rest = words[i].Substring(1).ToLower();
                }
                words[i] = firstChar + rest;
            }
            string Resultado = String.Join(" ", words).Replace(" ", "");
            
            return Resultado.First().ToString().ToUpper() + String.Join("", Resultado.Skip(1));
        }

        private static Hashtable hash = new Hashtable();
        /// <summary>
        /// Obtiene el nombre de una propiedad
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        private static String GetPropertyName(String fieldName)
        {
            if (fieldName.Length > 1)
            {
                if (hash.Contains(fieldName))
                    return (String)hash[fieldName];
                else
                {
                    String name = String.Format("{0}{1}", fieldName.Substring(0, 1).ToUpper(), fieldName.Substring(1));
                    hash[fieldName] = name;
                    return name;
                }

            }
            return fieldName;
        }

        /// <summary>
        ///  Obtiene el valor para cierto campo en el SQL dataReader      
        /// </summary>
        /// <param name="sdr"></param>
        /// <param name="nameField"></param>
        /// <returns>si no lo encuentra sera DBNull.Value  </returns>
        private static Object GetValue(System.Data.IDataReader sdr, string nameField)
        {
            try
            {
                Object value = sdr[nameField];
                return value == DBNull.Value ? null : value;
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }
        }
        #endregion
        public static string connectionString { get; set; }

        /// <summary>
        /// Ejecuta un procedimiento almacenado que retorna una lista de objetos tipo T
        /// </summary>
        /// <param name="nombreProcedimiento">Nombre del procedimiento almacenado a
        /// ejecutar</param>
        /// <param name="parametros">parámetros del procedimiento almacenado</param>
        //PSP_METRICS_METHOD_BEGIN : EjecutarProcedimiento 
        public static Resultado<IList<T>> EjecutarProcedimientoConsulta(string nombreProcedimiento, List<KeyValuePair<string, object>> parametros)
        {
            IList<T> result = null;
            SqlDataReader dataReader = null;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(nombreProcedimiento, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        for (int p = 0; parametros != null && p < parametros.Count; p++)
                            cmd.Parameters.AddWithValue(parametros[p].Key, parametros[p].Value);

                        try
                        {
                            //Capturo el error que me dira, si hay conexion o no a la base de datos
                            con.Open();
                        }
                        catch (SqlException sx)
                        {
                            string fuente = (typeof(T)).Name;
                            return new Resultado<IList<T>>(result, "Error", "DAL: " + sx.Message );
                        }


                        dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        result = MapearEntidad(dataReader);

                        con.Close();
                    }
                }

                return new Resultado<IList<T>>(result, String.Empty, String.Empty);
            }
            catch (SqlException sx)
            {
                string fuente = (typeof(T)).Name;
                return new Resultado<IList<T>>(null, "err001", "DAL: " + sx.Message);
            }
            finally
            {
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }
            }
        }

        /// <summary>
        /// Metodo que retorna la lista de propiedades para enviar a ejecutar un procedimiento almacenado,
        /// los datos nulos son ignorados y no se agregan a la lista
        /// </summary>
        /// <param name="obj">El entity que se quiere convertir en una lista keyValuePair</param>
        /// <returns>Lista de propiedades</returns>
        public static List<KeyValuePair<string, object>> ObtenerListaPropiedades(T obj)
        {
            var propiedades = obj.GetType().GetProperties();
            List<KeyValuePair<string, object>> lista = new List<KeyValuePair<string, object>>();
            foreach (var item in propiedades)
            {
                if (item.GetValue(obj, null) != null)
                {
                    lista.Add(new KeyValuePair<string, object>(item.Name, item.GetValue(obj, null)));
                    //lista.Add(new KeyValuePair<string, object>(ToDatabaseName(item.Name), item.GetValue(obj, null)));
                }

            }
            return lista;
        }

    }
}
