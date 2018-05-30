using PSIQ.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PSIQ.DataAccess
{
    public class UsuarioDAO
    {
        public void Inserir(Usuario obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"INSERT INTO USUARIO (TIPO, NOME, EMAIL, SENHA, CPF, CRP, DATA_NASCIMENTO, FOTO, DESCRICAO, COD_TERAPEUTA, COD_ESTADO) 
                                  VALUES (@TIPO, @NOME, @EMAIL, @SENHA, @CPF, @CRP, @DATA_NASCIMENTO, @FOTO, @DESCRICAO, @COD_TERAPEUTA, @COD_ESTADO);";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@TIPO", SqlDbType.Int).Value = obj.Tipo;
                    cmd.Parameters.Add("@NOME", SqlDbType.VarChar).Value = obj.Nome;
                    cmd.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = obj.Email;
                    cmd.Parameters.Add("@SENHA", SqlDbType.VarChar).Value = obj.Senha;
                    cmd.Parameters.Add("@CPF", SqlDbType.VarChar).Value = obj.CPF;
                    cmd.Parameters.Add("@CRP", SqlDbType.VarChar).Value = obj.CRP;
                    cmd.Parameters.Add("@DATA_NASCIMENTO", SqlDbType.DateTime).Value = obj.DataNasc;
                    cmd.Parameters.Add("@FOTO", SqlDbType.VarChar).Value = obj.Foto;
                    cmd.Parameters.Add("@DESCRICAO", SqlDbType.VarChar).Value = obj.Descricao;
                    cmd.Parameters.Add("@COD_TERAPEUTA", SqlDbType.Int).Value = obj.Terapeuta != null && obj.Terapeuta.Cod > 0 ? obj.Terapeuta.Cod : new Nullable<int>();
                    cmd.Parameters.Add("@COD_ESTADO", SqlDbType.Int).Value = obj.Estado != null && obj.Estado.Cod > 0 ? obj.Estado.Cod : new Nullable<int>();

                    foreach (SqlParameter parameter in cmd.Parameters)
                    {
                        if (parameter.Value == null)
                        {
                            parameter.Value = DBNull.Value;
                        }
                    }

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Atualizar(Usuario obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"UPDATE USUARIO SET 
                                      TIPO = @TIPO, 
                                      NOME = @NOME, 
                                      EMAIL = @EMAIL, 
                                      SENHA = @SENHA, 
                                      CPF = @CPF, 
                                      DATA_NASCIMENTO = @DATA_NASCIMENTO, 
                                      FOTO = @FOTO
                                  WHERE COD = @COD;";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@TIPO", SqlDbType.Int).Value = obj.Tipo;
                    cmd.Parameters.Add("@NOME", SqlDbType.VarChar).Value = obj.Nome;
                    cmd.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = obj.Email;
                    cmd.Parameters.Add("@SENHA", SqlDbType.VarChar).Value = obj.Senha;
                    cmd.Parameters.Add("@CPF", SqlDbType.VarChar).Value = obj.CPF;
                    cmd.Parameters.Add("@DATA_NASCIMENTO", SqlDbType.DateTime).Value = obj.DataNasc;
                    cmd.Parameters.Add("@FOTO", SqlDbType.VarChar).Value = obj.Foto;
                    cmd.Parameters.Add("@COD", SqlDbType.Int).Value = obj.Cod;

                    foreach (SqlParameter parameter in cmd.Parameters)
                    {
                        if (parameter.Value == null)
                        {
                            parameter.Value = DBNull.Value;
                        }
                    }

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public Usuario Logar(Usuario obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"SELECT * FROM USUARIO WHERE EMAIL = @EMAIL AND SENHA = @SENHA;";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = obj.Email;
                    cmd.Parameters.Add("@SENHA", SqlDbType.VarChar).Value = obj.Senha;
                    cmd.CommandText = strSQL;
                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    conn.Close();

                    if (!(dt != null && dt.Rows.Count > 0))
                        return null;

                    var row = dt.Rows[0];
                    var usuario = new Usuario()
                    {
                        Cod = Convert.ToInt32(row["COD"]),
                        Tipo = (TIPO_USUARIO)Convert.ToInt32(row["TIPO"]),
                        Nome = row["NOME"].ToString(),
                        Email = row["EMAIL"].ToString(),
                        Senha = row["SENHA"].ToString(),
                        CPF = row["CPF"].ToString(),
                        CRP = row["CRP"].ToString(),
                        DataNasc = Convert.ToDateTime(row["DATA_NASCIMENTO"]),
                        Foto = row["FOTO"].ToString(),
                        Descricao = row["DESCRICAO"].ToString(),
                        Terapeuta = row["COD_TERAPEUTA"] is DBNull ? null : new Usuario() { Cod = Convert.ToInt32(row["COD_TERAPEUTA"]) },
                        Estado = row["COD_ESTADO"] is DBNull ? null : new Estado() { Cod = Convert.ToInt32(row["COD_ESTADO"]) }
                    };

                    return usuario;
                }
            }
        }

        public Usuario BuscarPorCod(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"SELECT * FROM USUARIO WHERE COD = @COD;";

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
                    var usuario = new Usuario()
                    {
                        Cod = Convert.ToInt32(row["COD"]),
                        Tipo = (TIPO_USUARIO)Convert.ToInt32(row["TIPO"]),
                        Nome = row["NOME"].ToString(),
                        Email = row["EMAIL"].ToString(),
                        Senha = row["SENHA"].ToString(),
                        CPF = row["CPF"].ToString(),
                        CRP = row["CRP"].ToString(),
                        DataNasc = Convert.ToDateTime(row["DATA_NASCIMENTO"]),
                        Foto = row["FOTO"].ToString(),
                        Descricao = row["DESCRICAO"].ToString(),
                        Terapeuta = row["COD_TERAPEUTA"] is DBNull ? null : new Usuario() { Cod = Convert.ToInt32(row["COD_TERAPEUTA"]) },
                        Estado = row["COD_ESTADO"] is DBNull ? null : new Estado() { Cod = Convert.ToInt32(row["COD_ESTADO"]) }
                    };

                    return usuario;
                }
            }
        }

        public List<Usuario> BuscarTodos()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                var lst = new List<Usuario>();
                string strSQL = @"SELECT * FROM USUARIO;";

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
                        var usuario = new Usuario()
                        {
                            Cod = Convert.ToInt32(row["COD"]),
                            Tipo = (TIPO_USUARIO)Convert.ToInt32(row["TIPO"]),
                            Nome = row["NOME"].ToString(),
                            Email = row["EMAIL"].ToString(),
                            Senha = row["SENHA"].ToString(),
                            CPF = row["CPF"].ToString(),
                            CRP = row["CRP"].ToString(),
                            DataNasc = Convert.ToDateTime(row["DATA_NASCIMENTO"]),
                            Foto = row["FOTO"].ToString(),
                            Descricao = row["DESCRICAO"].ToString(),
                            Terapeuta = row["COD_TERAPEUTA"] is DBNull ? null : new Usuario() { Cod = Convert.ToInt32(row["COD_TERAPEUTA"]) },
                            Estado = row["COD_ESTADO"] is DBNull ? null : new Estado() { Cod = Convert.ToInt32(row["COD_ESTADO"]) }
                        };

                        lst.Add(usuario);
                    }
                }
                return lst;
            }
        }

        public List<Usuario> BuscarPorTerapeuta(int terapeuta)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                var lst = new List<Usuario>();
                string strSQL = @"SELECT * FROM USUARIO WHERE COD_TERAPEUTA = @COD_TERAPEUTA;";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@COD_TERAPEUTA", SqlDbType.Int).Value = terapeuta;
                    cmd.CommandText = strSQL;
                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    conn.Close();

                    foreach (DataRow row in dt.Rows)
                    {
                        var usuario = new Usuario()
                        {
                            Cod = Convert.ToInt32(row["COD"]),
                            Tipo = (TIPO_USUARIO)Convert.ToInt32(row["TIPO"]),
                            Nome = row["NOME"].ToString(),
                            Email = row["EMAIL"].ToString(),
                            Senha = row["SENHA"].ToString(),
                            CPF = row["CPF"].ToString(),
                            CRP = row["CRP"].ToString(),
                            DataNasc = Convert.ToDateTime(row["DATA_NASCIMENTO"]),
                            Foto = row["FOTO"].ToString(),
                            Descricao = row["DESCRICAO"].ToString(),
                            Terapeuta = row["COD_TERAPEUTA"] is DBNull ? null : new Usuario() { Cod = Convert.ToInt32(row["COD_TERAPEUTA"]) },
                            Estado = row["COD_ESTADO"] is DBNull ? null : new Estado() { Cod = Convert.ToInt32(row["COD_ESTADO"]) }
                        };

                        lst.Add(usuario);
                    }
                }
                return lst;
            }
        }
    }
}
