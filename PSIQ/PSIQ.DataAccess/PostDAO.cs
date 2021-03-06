﻿using PSIQ.Models;
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
                string strSQL = @"INSERT INTO POST (COD_PACIENTE, COD_USUARIO, DATA_HORA, MENSAGEM) VALUES (@COD_PACIENTE, @COD_USUARIO, @DATA_HORA, @MENSAGEM);";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    //Preenchendo os parâmetros da instrução sql
                    cmd.Parameters.Add("@COD_PACIENTE", SqlDbType.Int).Value = obj.Paciente != null ? obj.Paciente.Cod : new Nullable<int>();
                    cmd.Parameters.Add("@COD_USUARIO", SqlDbType.Int).Value = obj.Usuario != null ? obj.Usuario.Cod : new Nullable<int>();
                    cmd.Parameters.Add("@DATA_HORA", SqlDbType.DateTime).Value = obj.DataHora;
                    cmd.Parameters.Add("@MENSAGEM", SqlDbType.VarChar).Value = obj.Mensagem;

                    foreach (SqlParameter parameter in cmd.Parameters)
                    {
                        if (parameter.Value == null)
                        {
                            parameter.Value = DBNull.Value;
                        }
                    }

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
                                    PT.*,
                                    P.NOME AS NOME_PACIENTE,
                                    P.FOTO AS FOTO_PACIENTE,
                                    U.NOME AS NOME_USUARIO,
                                    U.FOTO AS FOTO_USUARIO
                                FROM POST PT
                                INNER JOIN USUARIO P ON (P.COD = PT.COD_PACIENTE)
                                INNER JOIN USUARIO U ON (U.COD = PT.COD_USUARIO)
                                WHERE PT.COD = @COD;";

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
                        Paciente = row["COD_PACIENTE"] is DBNull ? null : new Usuario()
                        {
                            Cod = Convert.ToInt32(row["COD_PACIENTE"]),
                            Nome = row["NOME_PACIENTE"].ToString(),
                            Foto = row["FOTO_PACIENTE"].ToString()
                        },
                        Usuario = row["COD_USUARIO"] is DBNull ? null : new Usuario()
                        {
                            Cod = Convert.ToInt32(row["COD_USUARIO"]),
                            Nome = row["NOME_USUARIO"].ToString(),
                            Foto = row["FOTO_USUARIO"].ToString()
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
                                    PT.*,
                                    P.NOME AS NOME_PACIENTE,
                                    P.FOTO AS FOTO_PACIENTE,
                                    U.NOME AS NOME_USUARIO,
                                    U.FOTO AS FOTO_USUARIO
                                FROM POST PT
                                INNER JOIN USUARIO P ON (P.COD = PT.COD_PACIENTE)
                                INNER JOIN USUARIO U ON (U.COD = PT.COD_USUARIO);";

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
                            Paciente = row["COD_PACIENTE"] is DBNull ? null : new Usuario()
                            {
                                Cod = Convert.ToInt32(row["COD_PACIENTE"]),
                                Nome = row["NOME_PACIENTE"].ToString(),
                                Foto = row["FOTO_PACIENTE"].ToString()
                            },
                            Usuario = row["COD_USUARIO"] is DBNull ? null : new Usuario()
                            {
                                Cod = Convert.ToInt32(row["COD_USUARIO"]),
                                Nome = row["NOME_USUARIO"].ToString(),
                                Foto = row["FOTO_USUARIO"].ToString()
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

        public List<Post> BuscarPorPaciente(int paciente)
        {
            var lst = new List<Post>();

            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para selecionar todos os registros na tabela de posts
                string strSQL = @"SELECT 
                                    PT.*,
                                    P.NOME AS NOME_PACIENTE,
                                    P.FOTO AS FOTO_PACIENTE,
                                    U.NOME AS NOME_USUARIO,
                                    U.FOTO AS FOTO_USUARIO
                                FROM POST PT
                                INNER JOIN USUARIO P ON (P.COD = PT.COD_PACIENTE)
                                INNER JOIN USUARIO U ON (U.COD = PT.COD_USUARIO)
                                WHERE PT.COD_PACIENTE = @COD_PACIENTE;";

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
                            Paciente = row["COD_PACIENTE"] is DBNull ? null : new Usuario()
                            {
                                Cod = Convert.ToInt32(row["COD_PACIENTE"]),
                                Nome = row["NOME_PACIENTE"].ToString(),
                                Foto = row["FOTO_PACIENTE"].ToString()
                            },
                            Usuario = row["COD_USUARIO"] is DBNull ? null : new Usuario()
                            {
                                Cod = Convert.ToInt32(row["COD_USUARIO"]),
                                Nome = row["NOME_USUARIO"].ToString(),
                                Foto = row["FOTO_USUARIO"].ToString()
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
