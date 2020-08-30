
using System;
using System.Data.SqlClient;

namespace Agenda.DAL
{
    public class Contatos 
    {
        public void Adicionar(string id, string nome)
        {
            string strCon = @"Data Source=.\sqlexpress;Initial Catalog=Agenda;Integrated Security=True;";

            SqlConnection con = new SqlConnection(strCon);
            con.Open();

            string sql = String.Format("Insert into Contato (Id, Nome) values ('{0}', '{1}');",id , nome);

            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.ExecuteNonQuery();
        }
    }
}
