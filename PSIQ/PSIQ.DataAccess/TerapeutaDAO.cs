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
                string strSQL = @"INSERT INTO PACIENTE(NOME,CRP, COD, EMAIL, DtNacimento, SENHA, FOTO) 
                                VALUES (@NOME,@CPF,@COD, @EMAIL, @DTNASCIMENTO, @SENHA, @FOTO );";
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@NOME", SqlDbType.VarChar).Value = obj.Nome;
                    cmd.Parameters.Add("@COD", SqlDbType.Int).Value = obj.COD;
                    cmd.Parameters.Add("@CRP", SqlDbType.VarChar).Value = obj.CRP;
                    cmd.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = obj.Email;
                    cmd.Parameters.Add(@"SENHA", SqlDbType.VarChar).Value = obj.Senha;
                    cmd.Parameters.Add(@"FOTO", SqlDbType.VarChar).Value = obj.Foto;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

     
    }
}
