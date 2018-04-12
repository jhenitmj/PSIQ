using PSIQ.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PSIQ.DataAccess
{
    public class PostDAO
    {
        public List<Post> BuscarTodos()
        {
            var lst = new List<Post>();

            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=PSIQ; Data Source=localhost; Integrated Security=SSPI;"))
            {
                //Criando instrução sql para selecionar todos os registros na tabela de posts
                string strSQL = @"SELECT 
                                    p.*, 
                                    T.COD_TERAPEUTA,
                                    A.COD_PACIENTE
                                FROM post p 
                                INNER JOIN TERAPEUTA T ON (T.COD = p.COD_TERAPEUTA)
                                INNER JOIN PACIENTE A ON (P.COD_PACIENTE=A.COD);";

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
                            Terapeuta = new Terapeuta()
                            {
                                Cod = Convert.ToInt32(row["COD"]),
                                Nome = row["Terapeuta"].ToString()
                            },
                            Paciente = new Paciente()
                            {
                                Cod = Convert.ToInt32(row["COD"]),
                                Nome = row["Paciente"].ToString()
                            },

                            DataHora = Convert.ToDateTime(row["data_hora"]),
                            Mensagem = row["mensagem"].ToString()
                        };

                        lst.Add(post);
                    }
                }
            }

            return lst;
        }


        public Post BuscarPorId(int id)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=Blog; Data Source=localhost; Integrated Security=SSPI;"))
            {
                //Criando instrução sql para selecionar todos os registros na tabela de posts
                string strSQL = @"SELECT * FROM post where id = @id;";

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
                        Paciente = new Paciente() { Cod = Convert.ToInt32(row["COD_PACIENTE"]) },
                        Terapeuta = new Terapeuta() { CRP = Convert.ToInt32(row["COD_TERAPEUTA"]) },
                        DataHora = Convert.ToDateTime(row["DATA_HORA"]),
                        Mensagem = row["MENSAGEM"].ToString()
                    };

                    return post;
                }
            }
        }

        public void Inserir(Post obj)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=PSIQ; Data Source=localhost; Integrated Security=SSPI;"))
            {
                //Criando instrução sql para inserir na tabela de posts
                string strSQL = @"INSERT INTO post (cod_paciente, cos_terapeuta, data_hora, mensagem) VALUES (@cod_paciente, @cod_terapeuta, @data_hora, @mensagem);";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    //Preenchendo os parâmetros da instrução sql
                    cmd.Parameters.Add("@cod_paciente", SqlDbType.Int).Value = obj.Paciente.Cod;
                    cmd.Parameters.Add("@cod_terapeuta", SqlDbType.Int).Value = obj.Terapeuta.Cod;
                    cmd.Parameters.Add("@data_hora", SqlDbType.DateTime).Value = obj.DataHora;
                    cmd.Parameters.Add("@mensagem", SqlDbType.VarChar).Value = obj.Mensagem;

                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    //Executando instrução sql
                    cmd.ExecuteNonQuery();
                    //Fechando conexão com o banco de dados
                    conn.Close();
                }
            }
        }
    }
}
