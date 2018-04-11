﻿using PSIQ.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PSIQ.DataAccess
{
    public class DiagnosticoDAO
    {
        public void Inserir(Diagnostico obj)
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=PSIQ; Data Source= localhost; Integrated Security = SSPI;"))
            {
                string strSQL = @"INSERT INTO DIAGNOSTICO (NOME) VALUES (@NOME);";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@NOME", SqlDbType.VarChar).Value = obj.Nome;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public List<Diagnostico> BuscarTodos()
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=PSIQ; Data Source= localhost; Integrated Security=SSPI;"))
            {
                var lst = new List<Diagnostico>();
                string strSQL = @"SELECT * FROM DIAGNOSTICO;";

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
                        var diagnostico = new Diagnostico()
                        {
                            Cod = Convert.ToInt32(row["COD"]),
                            Nome = row["NOME"].ToString()
                        };

                        lst.Add(diagnostico);
                    }
                }
                return lst;
            }
        }
    }
}
