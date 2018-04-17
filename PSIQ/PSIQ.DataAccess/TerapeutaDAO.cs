using PSIQ.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PSIQ.DataAccess
{
    public class TerapeutaDAO
    {
        public void Inserir(Terapeuta obj)
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=PSIQ; Data Source=localhost; Integrated Security=SSPI;"))
            {
                string strSQL = @"INSERT INTO TERAPEUTA (NOME, CRP, EMAIL, DATA_NASCIMENTO, SENHA, FOTO) 
                                  VALUES (@NOME, @CRP, @EMAIL, @DATA_NASCIMENTO, @SENHA, @FOTO);";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@NOME", SqlDbType.VarChar).Value = obj.Nome;
                    cmd.Parameters.Add("@CRP", SqlDbType.VarChar).Value = obj.CRP;
                    cmd.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = obj.Email;
                    cmd.Parameters.Add("@DATA_NASCIMENTO", SqlDbType.DateTime).Value = obj.DtNascimento == DateTime.MinValue ? DateTime.Now : obj.DtNascimento;
                    cmd.Parameters.Add("@SENHA", SqlDbType.VarChar).Value = obj.Senha;
                    cmd.Parameters.Add("@FOTO", SqlDbType.VarChar).Value = obj.Foto ?? string.Empty;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public Terapeuta Logar(Terapeuta obj)
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=PSIQ; Data Source=localhost; Integrated Security=SSPI;"))
            {
                string strSQL = @"SELECT * FROM TERAPEUTA WHERE EMAIL = @EMAIL AND SENHA = @SENHA;";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = obj.Email ?? string.Empty;
                    cmd.Parameters.Add("@SENHA", SqlDbType.VarChar).Value = obj.Senha ?? string.Empty;
                    cmd.CommandText = strSQL;
                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    conn.Close();

                    if (!(dt != null && dt.Rows.Count > 0))
                        return null;

                    var row = dt.Rows[0];
                    var terapeuta = new Terapeuta()
                    {
                        Cod = Convert.ToInt32(row["COD"]),
                        Nome = row["NOME"].ToString(),
                        CRP = row["CRP"].ToString(),
                        Email = row["EMAIL"].ToString(),
                        Senha = row["SENHA"].ToString(),
                        DtNascimento = Convert.ToDateTime(row["DATA_NASCIMENTO"]),
                        Foto = row["FOTO"].ToString()
                    };

                    return terapeuta;
                }
            }
        }

        public Terapeuta BuscarPorId(int id)
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=PSIQ; Data Source=localhost; Integrated Security=SSPI;"))
            {
                string strSQL = @"SELECT * FROM TERAPEUTA WHERE COD = @COD;";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@COD", SqlDbType.Int).Value = id;
                    cmd.CommandText = strSQL;
                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    conn.Close();

                    if (!(dt != null && dt.Rows.Count > 0))
                        return null;

                    var row = dt.Rows[0];
                    var terapeuta = new Terapeuta()
                    {
                        Cod = Convert.ToInt32(row["COD"]),
                        Nome = row["NOME"].ToString(),
                        CRP = row["CRP"].ToString(),
                        Email = row["EMAIL"].ToString(),
                        Senha = row["SENHA"].ToString(),
                        DtNascimento = Convert.ToDateTime(row["DATA_NASCIMENTO"]),
                        Foto = row["FOTO"].ToString()
                    };

                    return terapeuta;
                }
            }
        }

        public List<Terapeuta> BuscarTodos()
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=PSIQ; Data Source=localhost; Integrated Security=SSPI;"))
            {
                var lst = new List<Terapeuta>();
                string strSQL = @"SELECT * FROM TERAPEUTA;";

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
                        var terapeuta = new Terapeuta()
                        {
                            Cod = Convert.ToInt32(row["COD"]),
                            Nome = row["NOME"].ToString(),
                            CRP = row["CRP"].ToString(),
                            Email = row["EMAIL"].ToString(),
                            Senha = row["SENHA"].ToString(),
                            DtNascimento = Convert.ToDateTime(row["DATA_NASCIMENTO"]),
                            Foto = row["FOTO"].ToString()
                        };

                        lst.Add(terapeuta);
                    }
                }
                return lst;
            }
        }
    }
}
