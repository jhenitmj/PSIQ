using PSIQ.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PSIQ.DataAccess
{
    public class PacienteDAO
    {
        public void InserirPre(Paciente obj)
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=PSIQ; Data Source=localhost; Integrated Security=SSPI;"))
            {
                string strSQL = @"INSERT INTO PACIENTE (NOME, COD_ESTADO, COD_DIAGNOSTICO, DESCRICAO) 
                                VALUES (@NOME, @COD_ESTADO, @COD_DIAGNOSTICO, @DESCRICAO);";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@NOME", SqlDbType.VarChar).Value = obj.Nome;
                    cmd.Parameters.Add("@COD_ESTADO", SqlDbType.Int).Value = obj.Estado.Cod;
                    cmd.Parameters.Add("@COD_DIAGNOSTICO", SqlDbType.Int).Value = obj.Diagnostico.Cod;
                    cmd.Parameters.Add("@DESCRICAO", SqlDbType.VarChar).Value = obj.Descricao;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void AtualizarPre(Paciente obj)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=PSIQ; Data Source=localhost; Integrated Security=SSPI;"))
            {
                //Criando instrução sql para inserir na tabela de PACIENTES
                string strSQL = @"UPDATE PACIENTE SET 
                                      NOME = @NOME,
                                      COD_ESTADO = @COD_ESTADO,
                                      COD_DIAGNOSTICO = @COD_DIAGNOSTICO,
                                      DESCRICAO = @DESCRICAO
                                  WHERE COD = @COD;";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    //Preenchendo os parâmetros da instrução sql
                    cmd.Parameters.Add("@NOME", SqlDbType.VarChar).Value = obj.Nome;
                    cmd.Parameters.Add("@COD", SqlDbType.Int).Value = obj.Cod;
                    cmd.Parameters.Add("@COD_ESTADO", SqlDbType.Int).Value = obj.Estado.Cod;
                    cmd.Parameters.Add("@COD_DIAGNOSTICO", SqlDbType.Int).Value = obj.Diagnostico.Cod;
                    cmd.Parameters.Add("@DESCRICAO", SqlDbType.VarChar).Value = obj.Descricao;

                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    //Executando instrução sql
                    cmd.ExecuteNonQuery();
                    //Fechando conexão com o banco de dados
                    conn.Close();
                }
            }
        }

        public Paciente Logar(string email, string senha)
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=PSIQ; Data Source=localhost; Integrated Security=SSPI;"))
            {
                string strSQL = @"SELECT * FROM TERAPEUTA WHERE EMAIL = @EMAIL AND SENHA = @SENHA;";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = email;
                    cmd.Parameters.Add("@SENHA", SqlDbType.VarChar).Value = senha;
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
                        Nome = row["NOME"].ToString(),
                        Estado = new Estado()
                        {
                            Cod = Convert.ToInt32(row["COD_ESTADO"]),
                            Nome = row["NOME_ESTADO"].ToString(),
                        },
                        Diagnostico = new Diagnostico()
                        {
                            Cod = Convert.ToInt32(row["COD_DIAGNOSTICO"]),
                            Nome = row["NOME_DIAGNOSTICO"].ToString(),
                        }
                    };

                    return paciente;
                }
            }
        }

        public Paciente BuscarPorId(int id)
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
                    var paciente = new Paciente()
                    {
                        Nome = row["NOME"].ToString(),
                        Estado = new Estado()
                        {
                            Cod = Convert.ToInt32(row["COD_ESTADO"]),
                            Nome = row["NOME_ESTADO"].ToString(),
                        },
                        Diagnostico = new Diagnostico()
                        {
                            Cod = Convert.ToInt32(row["COD_DIAGNOSTICO"]),
                            Nome = row["NOME_DIAGNOSTICO"].ToString(),
                        }
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
                string strSQL = @"SELECT * FROM PACIENTE;";

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
                            Nome = row["NOME"].ToString(),
                            Estado = new Estado()
                            {
                                Cod = Convert.ToInt32(row["COD_ESTADO"]),
                                Nome = row["NOME_ESTADO"].ToString(),
                            },
                            Diagnostico = new Diagnostico()
                            {
                                Cod = Convert.ToInt32(row["COD_DIAGNOSTICO"]),
                                Nome = row["NOME_DIAGNOSTICO"].ToString(),
                            }
                        };

                        lst.Add(paciente);
                    }
                }

                return lst;
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

        public void Inserir(Paciente obj)
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=PSIQ; Data Source=localhost; Integrated Security=SSPI;"))
            {
                string strSQL = @"INSERT INTO PACIENTE (NOME, CPF, EMAIL, DTNACIMENTO, SENHA, FOTO) 
                                VALUES (@NOME, @CPF, @EMAIL, @DTNASCIMENTO, @SENHA, @FOTO);";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@NOME", SqlDbType.VarChar).Value = obj.Nome;
                    cmd.Parameters.Add("@CPF", SqlDbType.VarChar).Value = obj.CPF;
                    cmd.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = obj.Email;
                    cmd.Parameters.Add("@DATA_NASCIMENTO", SqlDbType.DateTime).Value = obj.DtNascimento;
                    cmd.Parameters.Add("@SENHA", SqlDbType.VarChar).Value = obj.Senha;
                    cmd.Parameters.Add("@FOTO", SqlDbType.VarChar).Value = obj.Foto ?? string.Empty;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        //public Paciente Logar(string email, string senha)
        //{
        //    using (SqlConnection conn = new SqlConnection(@"Initial Catalog=PSIQ; Data Source=localhost; Integrated Security=SSPI;"))
        //    {
        //        string strSQL = @"SELECT * FROM PACIENTE WHERE EMAIL = @EMAIL AND SENHA = @SENHA;";

        //        using (SqlCommand cmd = new SqlCommand(strSQL))
        //        {
        //            conn.Open();
        //            cmd.Connection = conn;
        //            cmd.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = email ?? string.Empty;
        //            cmd.Parameters.Add("@SENHA", SqlDbType.VarChar).Value = senha ?? string.Empty;
        //            cmd.CommandText = strSQL;
        //            var dataReader = cmd.ExecuteReader();
        //            var dt = new DataTable();
        //            dt.Load(dataReader);
        //            conn.Close();

        //            if (!(dt != null && dt.Rows.Count > 0))
        //                return null;

        //            var row = dt.Rows[0];
        //            var paciente = new Paciente()
        //            {
        //                Cod = Convert.ToInt32(row["COD"]),
        //                Nome = row["NOME"].ToString(),
        //                CPF = Convert.ToInt32(row["CRP"]),
        //                Email = row["EMAIL"].ToString(),
        //                Senha = row["SENHA"].ToString(),
        //                DtNascimento = Convert.ToDateTime(row["DATA_NASCIMENTO"]),
        //                Foto = row["FOTO"].ToString(),
        //                CaminhoFoto = row["CAMINHO_FOTO"].ToString()
        //            };

        //            return paciente ;
        //        }
        //    }
        //}
    }
}
