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
    class PacienteDAO
    {
        public void InserirPre(Paciente obj)
        {
            using (SqlConnection conn =
                new SqlConnection(@"Initial Catalog=PSIQ 
                        Data Source= localhost;
                        Integrated Security = SSPI;"))
            {
                string strSQL = @"INSERT INTO PACIENTE(NOME,COD_ESTADO, COD_DIAGNOSTICO, DESCRICAO ) 
                                VALUES (@NOME,@COD_ESTADO, @COD_DIAGNOSTICO, @DESCRICAO );";
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@NOME", SqlDbType.VarChar).Value = obj.Nome;
                    cmd.Parameters.Add("@COD_ESTADO", SqlDbType.Int).Value = obj.Estado.Cod;
                    cmd.Parameters.Add("@COD_DIAGNOSTICO", SqlDbType.Int).Value = obj.Diagnostico.Cod;
                    cmd.Parameters.Add(@"Dscricao", SqlDbType.VarChar).Value = obj.DescricaoD

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
