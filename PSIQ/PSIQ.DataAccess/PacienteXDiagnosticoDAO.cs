using PSIQ.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PSIQ.DataAccess
{
    public class PacienteXDiagnosticoDAO
    {
        public void Inserir(PacienteXDiagnostico obj)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=PSIQ; Data Source=localhost; Integrated Security=SSPI;"))
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
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=PSIQ; Data Source=localhost; Integrated Security=SSPI;"))
            {
                //Criando instrução sql para inserir na tabela de categorias
                string strSQL = @"DELETE FROM PACIENTE_X_DIAGNOSTICO WHERE COD_PACIENTE = @COD_PACIENTE AND COD_DIAGNOSTICO = @COD_DIAGNOSTICO;";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    //Preenchendo os parâmetros da instrução sql
                    cmd.Parameters.Add("@COD_PACIENTE", SqlDbType.VarChar).Value = obj.Paciente.Cod;
                    cmd.Parameters.Add("@COD_DIAGNOSTICO", SqlDbType.VarChar).Value = obj.Diagnostico.Cod;

                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    //Executando instrução sql
                    cmd.ExecuteNonQuery();
                    //Fechando conexão com o banco de dados
                    conn.Close();
                }
            }
        }

        public PacienteXDiagnostico BuscarPorId(int paciente, int diagnostico)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=PSIQ; Data Source=localhost; Integrated Security=SSPI;"))
            {
                //Criando instrução sql para selecionar todos os registros na tabela de Categorias
                string strSQL = @"SELECT  
                                      PD.DATA_HORA,
                                      PD.DESCRICAO,
                                      P.NOME_PACIENTE AS NOME_PACIENTE,
                                      D.OME_DIAGNOSTICO AS NOME_DIAGNOSTICO
                                  FROM  PACIENTEXDIAGNOSTICO PD
                                    INNER JOIN PACIENTE P
                                    ON COD_PACIENTE = @COD_PACIENTE 
                                    INNER JOIN DIAGNOSTICO D
                                    ON COD_DIAGNOSTICO = @COD_DIAGNOSTIC0";


                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@COD_PACIENTE", SqlDbType.Int).Value = paciente;
                    cmd.Parameters.Add("@COD_DIAGNOSTICO", SqlDbType.Int).Value = diagnostico;
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
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=PSIQ; Data Source=localhost; Integrated Security=SSPI;"))
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
                            Paciente = new Paciente() { Cod = Convert.ToInt32(row["COD_PACIENTE"]) },
                            Diagnostico = new Diagnostico() { Cod = Convert.ToInt32(row["COD_DIAGNOSTICO"]) },
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
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=PSIQ; Data Source=localhost; Integrated Security=SSPI;"))
            {
                //Criando instrução sql para selecionar todos os registros na tabela de Categorias
                string strSQL = @"SELECT * FROM PACIENTE_X_DIAGNOSTICO WHERE COD_PACIENTE = @COD_PACIENTE;";

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
                            Paciente = new Paciente() { Cod = Convert.ToInt32(row["COD_PACIENTE"]) },
                            Diagnostico = new Diagnostico() { Cod = Convert.ToInt32(row["COD_DIAGNOSTICO"]) },
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
