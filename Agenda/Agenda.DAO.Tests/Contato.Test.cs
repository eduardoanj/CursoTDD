using System;
using System.Collections.Generic;
using System.Linq;
using Agenda.DAO;
using Agenta.Domain;
using NUnit.Framework;

namespace Agenda.DAL.Tests
{
    [TestFixture]
    public class AgendaContatosTest : TestBase
    {
        private DAO.Contatos _contatos;

        [SetUp]
        public void SetUp()
        {
            _contatos = new DAO.Contatos();

        }

        [Test]
        public void AdicionarContatoTest()
        {
            var contact = new Contato
            {
                Id = Guid.NewGuid(),
                Nome = "Dudu"
            };

            _contatos.Adicionar(contact);

   
            Assert.True(true);
        }

        [Test]
        public void ObterContatoTest()
        {
            var contato = new Contato
            {
                Id = Guid.NewGuid(),
                Nome = "Dudu"
            };
            Contato ContatoResultado;
            
            _contatos.Adicionar(contato);
            ContatoResultado = _contatos.Obter(contato.Id);
            
            Assert.AreEqual(contato.Id, ContatoResultado.Id);
            Assert.AreEqual(contato.Nome, ContatoResultado.Nome);
        }

        [Test]
        public void ObterTodosOsContatosTest()
        {
            var contato1 = new Contato{ Id = Guid.NewGuid(), Nome = "Eddy"};
            var contato2 = new Contato{ Id = Guid.NewGuid(), Nome = "Fury"};
            
            _contatos.Adicionar(contato1);
            _contatos.Adicionar(contato2);
            var obterTodos = _contatos.ObterTodos();
            var contatoResultado = obterTodos.Find(x => x.Id == contato1.Id);

            Assert.IsTrue(obterTodos.Count() > 1);
            Assert.AreEqual(contato1.Id, contatoResultado.Id);
            Assert.AreEqual(contato1.Nome, contatoResultado.Nome);
        }

        [TearDown]
        public void TearDown()
        {
            _contatos = null;
        }
    }
}
