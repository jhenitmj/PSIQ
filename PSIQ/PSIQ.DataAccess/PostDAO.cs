using PSIQ.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PSIQ.DataAccess
{
    public class PostDAO
    {
        public void Inserir(Post obj)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para inserir na tabela de posts
                string strSQL = @"INSERT INTO POST (COD_PACIENTE, COD_TERAPEUTA, DATA_HORA, MENSAGEM) 
                                  VALUES (@COD_PACIENTE, @COD_TERAPEUTA, @DATA_HORA, @MENSAGEM);";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    //Preenchendo os parâmetros da instrução sql
                    cmd.Parameters.Add("@COD_PACIENTE", SqlDbType.Int).Value = obj.Paciente != null ? obj.Paciente.Cod : new Nullable<int>();
                    cmd.Parameters.Add("@COD_TERAPEUTA", SqlDbType.Int).Value = obj.Terapeuta != null ? obj.Terapeuta.Cod : new Nullable<int>();
                    cmd.Parameters.Add("@DATA_HORA", SqlDbType.DateTime).Value = obj.DataHora;
                    cmd.Parameters.Add("@MENSAGEM", SqlDbType.VarChar).Value = obj.Mensagem;

                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    //Executando instrução sql
                    cmd.ExecuteNonQuery();
                    //Fechando conexão com o banco de dados
                    conn.Close();
                }
            }
        }

        public Post BuscarPorId(int id)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para selecionar todos os registros na tabela de posts
                string strSQL = @"SELECT 
                                    P.*,
                                    T.NOME AS TERAPEUTA,
                                    A.NOME AS PACIENTE,
                                    T.FOTO AS FOTO_TERAPEUTA,
                                    A.FOTO AS FOTO_PACIENTE
                                FROM POST P
                                LEFT JOIN TERAPEUTA T ON (T.COD = P.COD_TERAPEUTA)
                                LEFT JOIN PACIENTE A ON (A.COD = P.COD_PACIENTE)
                                WHERE P.COD = @COD;";

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

                    if (!(dt != null && dt.Rows.Count > 0))
                        return null;

                    var row = dt.Rows[0];
                    var post = new Post()
                    {
                        Cod = Convert.ToInt32(row["COD"]),
                        Terapeuta = row["COD_TERAPEUTA"] is DBNull ? null : new Terapeuta()
                        {
                            Cod = Convert.ToInt32(row["COD_TERAPEUTA"]),
                            Nome = row["TERAPEUTA"].ToString(),
                            Foto = row["FOTO_TERAPEUTA"].ToString()
                        },
                        Paciente = row["COD_PACIENTE"] is DBNull ? null : new Paciente()
                        {
                            Cod = Convert.ToInt32(row["COD_PACIENTE"]),
                            Nome = row["PACIENTE"].ToString(),
                            Foto = row["FOTO_PACIENTE"].ToString()
                        },
                        DataHora = Convert.ToDateTime(row["DATA_HORA"]),
                        Mensagem = row["MENSAGEM"].ToString()
                    };

                    return post;
                }
            }
        }

        public List<Post> BuscarTodos()
        {
            var lst = new List<Post>();

            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para selecionar todos os registros na tabela de posts
                string strSQL = @"SELECT 
                                    P.*,
                                    T.NOME AS TERAPEUTA,
                                    A.NOME AS PACIENTE,
                                    T.FOTO AS FOTO_TERAPEUTA,
                                    A.FOTO AS FOTO_PACIENTE
                                FROM POST P
                                LEFT JOIN TERAPEUTA T ON (T.COD = P.COD_TERAPEUTA)
                                LEFT JOIN PACIENTE A ON (A.COD = P.COD_PACIENTE);";

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
                        var post = new Post()
                        {
                            Cod = Convert.ToInt32(row["COD"]),
                            Terapeuta = row["COD_TERAPEUTA"] is DBNull ? null : new Terapeuta()
                            {
                                Cod = Convert.ToInt32(row["COD_TERAPEUTA"]),
                                Nome = row["TERAPEUTA"].ToString(),
                                Foto = row["FOTO_TERAPEUTA"].ToString()
                            },
                            Paciente = row["COD_PACIENTE"] is DBNull ? null : new Paciente()
                            {
                                Cod = Convert.ToInt32(row["COD_PACIENTE"]),
                                Nome = row["PACIENTE"].ToString(),
                                Foto = row["FOTO_PACIENTE"].ToString()
                            },
                            DataHora = Convert.ToDateTime(row["DATA_HORA"]),
                            Mensagem = row["MENSAGEM"].ToString()
                        };

                        lst.Add(post);
                    }
                }
            }

            return lst;
        }

        public List<Post> BuscarPorUsuario(int paciente)
        {
            var lst = new List<Post>();

            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para selecionar todos os registros na tabela de posts
                string strSQL = @"SELECT 
                                    P.*,
                                    T.NOME AS TERAPEUTA,
                                    A.NOME AS PACIENTE,
                                    T.FOTO AS FOTO_TERAPEUTA,
                                    A.FOTO AS FOTO_PACIENTE
                                FROM POST P
                                LEFT JOIN TERAPEUTA T ON (T.COD = P.COD_TERAPEUTA)
                                LEFT JOIN PACIENTE A ON (A.COD = P.COD_PACIENTE)
                                WHERE P.COD_PACIENTE = @COD_PACIENTE;";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@COD_PACIENTE", SqlDbType.Int).Value = paciente;
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
                        var post = new Post()
                        {
                            Cod = Convert.ToInt32(row["COD"]),
                            Terapeuta = row["COD_TERAPEUTA"] is DBNull ? null : new Terapeuta()
                            {
                                Cod = Convert.ToInt32(row["COD_TERAPEUTA"]),
                                Nome = row["TERAPEUTA"].ToString(),
                                Foto = row["FOTO_TERAPEUTA"].ToString()
                            },
                            Paciente = row["COD_PACIENTE"] is DBNull ? null : new Paciente()
                            {
                                Cod = Convert.ToInt32(row["COD_PACIENTE"]),
                                Nome = row["PACIENTE"].ToString(),
                                Foto = row["FOTO_PACIENTE"].ToString()
                            },
                            DataHora = Convert.ToDateTime(row["DATA_HORA"]),
                            Mensagem = row["MENSAGEM"].ToString()
                        };

                        lst.Add(post);
                    }
                }
            }

            return lst;
        }
    }
}
