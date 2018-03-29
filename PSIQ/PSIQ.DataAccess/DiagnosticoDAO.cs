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
    public class DiagnosticoDAO
    {
        public void Inserir(Diagnostico obj)
        {
            using (SqlConnection conn =
                new SqlConnection(@"Initial Catalog=PSIQ 
                        Data Source= localhost;
                        Integrated Security = SSPI;"))
            {
                string strSQL = @"INSERT INTO DIAGNOSTICO(NOME) 
                                VALUES (@NOME);";
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@NOME", SqlDbType.VarChar).Value = obj.Nome;
                 

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
