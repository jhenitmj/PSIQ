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
    public class TerapeutaDAO
    {
        public void Inserir(Terapeuta obj)
        {
            using (SqlConnection conn =
                new SqlConnection(@"Initial Catalog=PSIQ 
                        Data Source= localhost;
                        Integrated Security = SSPI;"))
            {
                string strSQL = @"INSERT INTO TERAPEUTA (CRP, IDADE, FOTO, NOME, DTANASCIMENTO, SENHA) 
                                VALUES (@CRP, @IDADE, @FOTO, @NOME, @DTANASCIMENTO, @SENHA);";
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@CRP", SqlDbType.VarChar).Value = obj.CRP;
                    cmd.Parameters.Add("@DTNASCIMENTO", SqlDbType.DateTime).Value = obj.DtNasciento;
                    cmd.Parameters.Add("@SENHA" , SqlDbType.VarChar).Value = obj.Senha;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
