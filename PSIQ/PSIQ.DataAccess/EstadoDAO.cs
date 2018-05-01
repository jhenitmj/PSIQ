using PSIQ.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PSIQ.DataAccess
{
    public class EstadoDAO
    {
        public List<Estado> BuscarTodos()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                var lst = new List<Estado>();
                string strSQL = @"SELECT * FROM ESTADO;";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = strSQL;
                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    conn.Close();

                    foreach (DataRow row in dt.Rows)
                    {
                        var estado = new Estado()
                        {
                            Cod = Convert.ToInt32(row["COD"]),
                            Nome = row["NOME"].ToString()
                        };

                        lst.Add(estado);
                    }
                }
                return lst;
            }
        }
    }
}
