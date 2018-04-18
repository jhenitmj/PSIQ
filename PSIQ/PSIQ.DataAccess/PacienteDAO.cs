using PSIQ.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PSIQ.DataAccess
{
    public class PacienteDAO
    {
        public void Inserir(Paciente obj)
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=PSIQ; Data Source=localhost; Integrated Security=SSPI;"))
            {
                string strSQL = @"INSERT INTO PACIENTE (NOME, CPF, EMAIL, SENHA, DATA_NASCIMENTO, FOTO, COD_ESTADO, COD_TERAPEUTA) 
                                VALUES (@NOME, @CPF, @EMAIL, @SENHA, @DATA_NASCIMENTO, @FOTO, @COD_ESTADO, @COD_TERAPEUTA);";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@NOME", SqlDbType.VarChar).Value = obj.Nome;
                    cmd.Parameters.Add("@CPF", SqlDbType.VarChar).Value = obj.CPF;
                    cmd.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = obj.Email;
                    cmd.Parameters.Add("@SENHA", SqlDbType.VarChar).Value = obj.Senha;
                    cmd.Parameters.Add("@DATA_NASCIMENTO", SqlDbType.DateTime).Value = obj.DtNascimento == DateTime.MinValue ? DateTime.Now : obj.DtNascimento;
                    cmd.Parameters.Add("@FOTO", SqlDbType.VarChar).Value = obj.Foto ?? string.Empty;
                    cmd.Parameters.Add("@COD_ESTADO", SqlDbType.Int).Value = obj.Estado.Cod;
                    cmd.Parameters.Add("@COD_TERAPEUTA", SqlDbType.Int).Value = obj.Terapeuta.Cod;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Deletar(Paciente obj)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=PSIQ; Data Source=localhost; Integrated Security=SSPI;"))
            {
                //Criando instrução sql para inserir na tabela de usuarios
                string strSQL = @"DELETE FROM PACIENTE WHERE COD = @COD;";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    //Preenchendo os parâmetros da instrução sql
                    cmd.Parameters.Add("@Cod", SqlDbType.VarChar).Value = obj.Cod;

                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    //Executando instrução sql
                    cmd.ExecuteNonQuery();
                    //Fechando conexão com o banco de dados
                    conn.Close();
                }
            }
        }

        public Paciente Logar(Paciente obj)
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=PSIQ; Data Source=localhost; Integrated Security=SSPI;"))
            {
                string strSQL = @"SELECT 
                                      P.*,
                                      T.NOME AS NOME_TERAPEUTA,
                                      E.NOME AS NOME_ESTADO
                                  FROM PACIENTE P
                                  INNER JOIN TERAPEUTA T ON (T.COD = P.COD_TERAPEUTA)
                                  LEFT JOIN ESTADO E ON (E.COD = P.COD_ESTADO) 
                                  WHERE P.EMAIL = @EMAIL 
                                  AND P.SENHA = @SENHA;";

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
                    var paciente = new Paciente()
                    {
                        Cod = Convert.ToInt32(row["COD"]),
                        CPF = row["CPF"].ToString(),
                        Nome = row["NOME"].ToString(),
                        Email = row["EMAIL"].ToString(),
                        Senha = row["SENHA"].ToString(),
                        DtNascimento = Convert.ToDateTime(row["DATA_NASCIMENTO"]),
                        Estado = row["COD_ESTADO"] is DBNull ? null : new Estado()
                        {
                            Cod = Convert.ToInt32(row["COD_ESTADO"]),
                            Nome = row["NOME_ESTADO"].ToString(),
                        },
                        Terapeuta = row["COD_TERAPEUTA"] is DBNull ? null : new Terapeuta()
                        {
                            Cod = Convert.ToInt32(row["COD_TERAPEUTA"]),
                            Nome = row["NOME_TERAPEUTA"].ToString(),
                        },
                        Foto = row["FOTO"].ToString(),
                        Descricao = row["DESCRICAO"].ToString()
                    };

                    return paciente;
                }
            }
        }

        public Paciente BuscarPorId(int id)
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=PSIQ; Data Source=localhost; Integrated Security=SSPI;"))
            {
                string strSQL = @"SELECT 
                                      P.*,
                                      T.NOME AS NOME_TERAPEUTA,
                                      E.NOME AS NOME_ESTADO
                                  FROM PACIENTE P
                                  INNER JOIN TERAPEUTA T ON (T.COD = P.COD_TERAPEUTA)
                                  LEFT JOIN ESTADO E ON (E.COD = P.COD_ESTADO) 
                                  WHERE P.COD = @COD;";

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
                    var paciente = new Paciente()
                    {
                        Cod = Convert.ToInt32(row["COD"]),
                        CPF = row["CPF"].ToString(),
                        Nome = row["NOME"].ToString(),
                        Email = row["EMAIL"].ToString(),
                        Senha = row["SENHA"].ToString(),
                        DtNascimento = Convert.ToDateTime(row["DATA_NASCIMENTO"]),
                        Estado = row["COD_ESTADO"] is DBNull ? null : new Estado()
                        {
                            Cod = Convert.ToInt32(row["COD_ESTADO"]),
                            Nome = row["NOME_ESTADO"].ToString(),
                        },
                        Terapeuta = row["COD_TERAPEUTA"] is DBNull ? null : new Terapeuta()
                        {
                            Cod = Convert.ToInt32(row["COD_TERAPEUTA"]),
                            Nome = row["NOME_TERAPEUTA"].ToString(),
                        },
                        Foto = row["FOTO"].ToString(),
                        Descricao = row["DESCRICAO"].ToString()
                    };

                    return paciente;
                }
            }
        }

        public List<Paciente> BuscarTodos()
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=PSIQ; Data Source=localhost; Integrated Security=SSPI;"))
            {
                var lst = new List<Paciente>();
                //Criando instrução sql para selecionar todos os registros na tabela de usuarios
                string strSQL = @"SELECT 
                                      P.*,
                                      T.NOME AS NOME_TERAPEUTA,
                                      E.NOME AS NOME_ESTADO
                                  FROM PACIENTE P
                                  INNER JOIN TERAPEUTA T ON (T.COD = P.COD_TERAPEUTA)
                                  LEFT JOIN ESTADO E ON (E.COD = P.COD_ESTADO);";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = strSQL;
                    //Executando instrução sql
                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    //Fechando conexão com o banco de dados
                    conn.Close();

                    //Percorrendo todos os registros encontrados na base de dados e adicionando em uma lista
                    foreach (DataRow row in dt.Rows)
                    {
                        var paciente = new Paciente()
                        {
                            Cod = Convert.ToInt32(row["COD"]),
                            CPF = row["CPF"].ToString(),
                            Nome = row["NOME"].ToString(),
                            Email = row["EMAIL"].ToString(),
                            Senha = row["SENHA"].ToString(),
                            DtNascimento = Convert.ToDateTime(row["DATA_NASCIMENTO"]),
                            Estado = row["COD_ESTADO"] is DBNull ? null : new Estado()
                            {
                                Cod = Convert.ToInt32(row["COD_ESTADO"]),
                                Nome = row["NOME_ESTADO"].ToString(),
                            },
                            Terapeuta = row["COD_TERAPEUTA"] is DBNull ? null : new Terapeuta()
                            {
                                Cod = Convert.ToInt32(row["COD_TERAPEUTA"]),
                                Nome = row["NOME_TERAPEUTA"].ToString(),
                            },
                            Foto = row["FOTO"].ToString(),
                            Descricao = row["DESCRICAO"].ToString()
                        };

                        lst.Add(paciente);
                    }
                }

                return lst;
            }
        }

        public List<Paciente> BuscarPorTerapeuta(int terapeuta)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=PSIQ; Data Source=localhost; Integrated Security=SSPI;"))
            {
                var lst = new List<Paciente>();
                //Criando instrução sql para selecionar todos os registros na tabela de usuarios
                string strSQL = @"SELECT 
                                      P.*,
                                      T.NOME AS NOME_TERAPEUTA,
                                      E.NOME AS NOME_ESTADO
                                  FROM PACIENTE P
                                  INNER JOIN TERAPEUTA T ON (T.COD = P.COD_TERAPEUTA)
                                  LEFT JOIN ESTADO E ON (E.COD = P.COD_ESTADO)
                                  WHERE COD_TERAPEUTA = @COD_TERAPEUTA;";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@COD_TERAPEUTA", SqlDbType.Int).Value = terapeuta;
                    cmd.CommandText = strSQL;
                    //Executando instrução sql
                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    //Fechando conexão com o banco de dados
                    conn.Close();

                    //Percorrendo todos os registros encontrados na base de dados e adicionando em uma lista
                    foreach (DataRow row in dt.Rows)
                    {
                        var paciente = new Paciente()
                        {
                            Cod = Convert.ToInt32(row["COD"]),
                            CPF = row["CPF"].ToString(),
                            Nome = row["NOME"].ToString(),
                            Email = row["EMAIL"].ToString(),
                            Senha = row["SENHA"].ToString(),
                            DtNascimento = Convert.ToDateTime(row["DATA_NASCIMENTO"]),
                            Estado = row["COD_ESTADO"] is DBNull ? null : new Estado()
                            {
                                Cod = Convert.ToInt32(row["COD_ESTADO"]),
                                Nome = row["NOME_ESTADO"].ToString(),
                            },
                            Terapeuta = row["COD_TERAPEUTA"] is DBNull ? null : new Terapeuta()
                            {
                                Cod = Convert.ToInt32(row["COD_TERAPEUTA"]),
                                Nome = row["NOME_TERAPEUTA"].ToString(),
                            },
                            Foto = row["FOTO"].ToString(),
                            Descricao = row["DESCRICAO"].ToString()
                        };

                        lst.Add(paciente);
                    }
                }

                return lst;
            }
        }
    }
}
