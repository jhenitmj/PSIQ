using PSIQ.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSIQ.DataAccess
{
    public class EstadoDAO
    {
        public List<Estado> BuscarTodos()
        {

            {
                var lst = new List<Estado>();

                using (SqlConnection conn = new SqlConnection(@"Initial Catalog=PSIQ Data
                                                Data Source= localhost;
                                                Integrated Security=SSPI;"))
                {
                    string strSQL = @"Select *from ESTADO;";

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
                                Cod = Convert.ToInt32(row["Cod"]),
                                Nome = row["Nome"].ToString(),


                            };

                            lst.Add(estado);
                        }
                    }
                }

                return lst;

            }
        }
    }
}
