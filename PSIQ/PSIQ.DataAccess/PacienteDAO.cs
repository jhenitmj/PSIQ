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
    class PacienteDAO
    {
        public void InserirPre(Paciente obj)
        {
            using (SqlConnection conn =
                new SqlConnection(@"Initial Catalog=PSIQ 
                        Data Source= localhost;
                        Integrated Security = SSPI;"))
            {
                string strSQL = @"INSERT INTO PACIENTE(NOME,, COD, COD_ESTADO, COD_DIAGNOSTICO, DESCRICAO ) 
                                VALUES (@NOME,@COD, @COD_ESTADO, @COD_DIAGNOSTICO, @DESCRICAO );";
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@NOME", SqlDbType.VarChar).Value = obj.Nome;
                    cmd.Parameters.Add("@COD", SqlDbType.Int).Value = obj.COD;          
                    cmd.Parameters.Add("@COD_ESTADO", SqlDbType.Int).Value = obj.Estado.Cod;
                    cmd.Parameters.Add("@COD_DIAGNOSTICO", SqlDbType.Int).Value = obj.Diagnostico.Cod;
                    cmd.Parameters.Add(@"Dscricao", SqlDbType.VarChar).Value = obj.DescricaoD;

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
                string strSQL = @"UPDATE PACIENTE set 
                                    nome = @nome,
                                    COD_ESTADO=@COD_ESTADO,
                                    COD_DIAGNOSTICO = @COD_DIAGNOSTICO,
                                    DESCRICAO=@DESCRICAO
                                WHERE COD = @COD;";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    //Preenchendo os parâmetros da instrução sql
                    cmd.Parameters.Add("@NOME", SqlDbType.VarChar).Value = obj.Nome;
                    cmd.Parameters.Add("@COD", SqlDbType.Int).Value = obj.COD;
                    cmd.Parameters.Add("@COD_ESTADO", SqlDbType.Int).Value = obj.Estado.Cod;
                    cmd.Parameters.Add("@COD_DIAGNOSTICO", SqlDbType.Int).Value = obj.Diagnostico.Cod;
                    cmd.Parameters.Add(@"Dscricao", SqlDbType.VarChar).Value = obj.DescricaoD;
                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    //Executando instrução sql
                    cmd.ExecuteNonQuery();
                    //Fechando conexão com o banco de dados
                    conn.Close();
                }
            }
        }

        public List<Paciente> BuscarTodos()
        {
            var lst = new List<Paciente>();

            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=Blog; Data Source=localhost; Integrated Security=SSPI;"))
            {
                //Criando instrução sql para selecionar todos os registros na tabela de usuarios
                string strSQL = @"SELECT * FROM Paciente;";

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
                            Nome = row["Nome"].ToString(),
                            Estado = new Estado()
                            {
                                Cod = Convert.ToInt32(row["cod"]),
                                Nome = row["nome"].ToString(),

                            },

                            Diagnostico = new Diagnostico()
                            {
                                Cod = Convert.ToInt32(row["cod"]),
                                Nome = row["nome"].ToString(),

                            },

                        lst.Add(paciente);
                    }
                }
            }

            return lst;
        }

        public void Deletar(Paciente obj)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=Blog; Data Source=localhost; Integrated Security=SSPI;"))
            {
                //Criando instrução sql para inserir na tabela de usuarios
                string strSQL = @"DELETE FROM Paciente where Cod = @Cod;";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    //Preenchendo os parâmetros da instrução sql
                    cmd.Parameters.Add("@Cod", SqlDbType.VarChar).Value = obj.COD;

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
            using (SqlConnection conn =
                new SqlConnection(@"Initial Catalog=PSIQ 
                        Data Source= localhost;
                        Integrated Security = SSPI;"))
            {
                string strSQL = @"INSERT INTO PACIENTE(NOME,CPF, COD, EMAIL, DtNacimento, SENHA, FOTO) 
                                VALUES (@NOME,@CPF,@COD, @EMAIL, @DTNASCIMENTO, @SENHA, @FOTO );";
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@NOME", SqlDbType.VarChar).Value = obj.Nome;
                    cmd.Parameters.Add("@COD", SqlDbType.Int).Value = obj.COD;
                    cmd.Parameters.Add("@CPF", SqlDbType.VarChar).Value = obj.CPF;
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
