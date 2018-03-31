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
    public class PostDAO
    {
        public void Inserir(Post obj)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=Blog; Data Source=localhost; Integrated Security=SSPI;"))
            {
                //Criando instrução sql para inserir na tabela de posts
                string strSQL = @"INSERT INTO post (id_usuario, data_hora, mensagem) VALUES (@id_usuario, @data_hora, @mensagem);";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    //Preenchendo os parâmetros da instrução sql
                    cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = obj.Usuario.Id;
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
