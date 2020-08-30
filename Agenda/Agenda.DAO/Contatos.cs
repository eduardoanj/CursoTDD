using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Agenta.Domain;
using Dapper;

namespace Agenda.DAO
{
    public class Contatos
    {
        private string _strCon;

        public Contatos()
        {
            _strCon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }
        public void Adicionar(Contato contato)
        {
            using (var connection = new SqlConnection(_strCon))
            {
                connection.Execute("Insert into Contato (Id, Nome) values (@Id, @Nome)", contato);
            }
        }

        public Contato Obter(Guid id)
        {
            Contato contato;
            using (var connection = new SqlConnection(_strCon))
            {
                contato = connection.QueryFirst<Contato>("select Id, Nome from Contato where Id = @Id", new {Id = id});
            }
            return contato;
        }

        public List<Contato> ObterTodos()
        {
            var contatos = new List<Contato>();
            using (var connection = new SqlConnection(_strCon))
            {
                contatos = connection.Query<Contato>("select Id, Nome from Contato").ToList();
            }
            return contatos;
        }
    }
}