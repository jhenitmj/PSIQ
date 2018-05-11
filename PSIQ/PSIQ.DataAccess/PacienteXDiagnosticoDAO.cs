using PSIQ.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PSIQ.DataAccess
{
    public class PacienteXDiagnosticoDAO
    {
        public void Inserir(PacienteXDiagnostico obj)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para inserir na tabela de categorias
                string strSQL = @"INSERT INTO PACIENTE_X_DIAGNOSTICO (COD_PACIENTE, COD_DIAGNOSTICO, DATA_HORA, DESCRICAO) 
                                  VALUES (@COD_PACIENTE, @COD_DIAGNOSTICO, @DATA_HORA, @DESCRICAO);";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    //Preenchendo os parâmetros da instrução sql
                    cmd.Parameters.Add("@COD_PACIENTE", SqlDbType.Int).Value = obj.Paciente.Cod;
                    cmd.Parameters.Add("@COD_DIAGNOSTICO", SqlDbType.Int).Value = obj.Diagnostico.Cod;
                    cmd.Parameters.Add("@DATA_HORA", SqlDbType.DateTime).Value = obj.DataHora;
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

        public void Deletar(PacienteXDiagnostico obj)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para inserir na tabela de categorias
                string strSQL = @"DELETE FROM PACIENTE_X_DIAGNOSTICO WHERE COD = @COD;";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    //Preenchendo os parâmetros da instrução sql
                    cmd.Parameters.Add("@COD", SqlDbType.Int).Value = obj.Cod;

                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    //Executando instrução sql
                    cmd.ExecuteNonQuery();
                    //Fechando conexão com o banco de dados
                    conn.Close();
                }
            }
        }

        public PacienteXDiagnostico BuscarPorCod(int cod)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para selecionar todos os registros na tabela de DiagXPaicientes
                string strSQL = @"SELECT
                                      PD.DATA_HORA,
                                      PD.DESCRICAO,
                                      P.NOME_PACIENTE AS NOME_PACIENTE,
                                      D.NOME_DIAGNOSTICO AS NOME_DIAGNOSTICO
                                  FROM  PACIENTE_X_DIAGNOSTICO PD
                                  INNER JOIN PACIENTE P ON P.COD = PD.COD_PACIENTE
                                  INNER JOIN DIAGNOSTICO D ON D.COD = PD.COD_DIAGNOSTICO;";


                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@COD", SqlDbType.Int).Value = cod;
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
                    var pxd = new PacienteXDiagnostico()
                    {
                        Paciente = new Paciente() { Cod = Convert.ToInt32(row["COD_PACIENTE"]) },
                        Diagnostico = new Diagnostico() { Cod = Convert.ToInt32(row["COD_DIAGNOSTICO"]) },
                        DataHora = Convert.ToDateTime(row["DATA_HORA"]),
                        Descricao = row["DESCRICAO"].ToString()
                    };

                    return pxd;
                }
            }
        }

        public List<PacienteXDiagnostico> BuscarTodos()
        {
            var lst = new List<PacienteXDiagnostico>();

            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para selecionar todos os registros na tabela de Categorias
                string strSQL = @"SELECT * FROM PACIENTE_X_DIAGNOSTICO;";

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
                        var pxd = new PacienteXDiagnostico()
                        {
                            Cod = Convert.ToInt32(row["COD"]),
                            Paciente = new Paciente() { Cod = Convert.ToInt32(row["COD_PACIENTE"]) },
                            Diagnostico = row["COD_DIAGNOSTICO"] is DBNull ? null : new Diagnostico()
                            {
                                Cod = Convert.ToInt32(row["COD_DIAGNOSTICO"]),
                                Nome = row["NOME_DIAGNOSTICO"].ToString(),
                            },
                            DataHora = Convert.ToDateTime(row["DATA_HORA"]),
                            Descricao = row["DESCRICAO"].ToString()
                        };

                        lst.Add(pxd);
                    }
                }
            }

            return lst;
        }

        public List<PacienteXDiagnostico> BuscarPorPaciente(int paciente)
        {
            var lst = new List<PacienteXDiagnostico>();

            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para selecionar todos os registros na tabela de Categorias
                string strSQL = @"SELECT
                                      PD.*,
                                      D.NOME AS NOME_DIAGNOSTICO
                                  FROM  PACIENTE_X_DIAGNOSTICO PD
                                  INNER JOIN PACIENTE P ON P.COD = PD.COD_PACIENTE
                                  INNER JOIN DIAGNOSTICO D ON D.COD = PD.COD_DIAGNOSTICO;";

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
                        var pxd = new PacienteXDiagnostico()
                        {
                            Cod = Convert.ToInt32(row["COD"]),
                            Paciente = new Paciente() { Cod = Convert.ToInt32(row["COD_PACIENTE"]) },
                            Diagnostico = row["COD_DIAGNOSTICO"] is DBNull ? null : new Diagnostico()
                            {
                                Cod = Convert.ToInt32(row["COD_DIAGNOSTICO"]),
                                Nome = row["NOME_DIAGNOSTICO"].ToString(),
                            },
                            DataHora = Convert.ToDateTime(row["DATA_HORA"]),
                            Descricao = row["DESCRICAO"].ToString()
                        };

                        lst.Add(pxd);
                    }
                }
            }

            return lst;
        }
    }
}
