using System;
using System.Collections.Generic;
using System.Linq;
using Agenda.DAO;
using Agenta.Domain;
using AutoFixture;
using NUnit.Framework;

namespace Agenda.DAL.Tests
{
    [TestFixture]
    public class AgendaContatosTest : TestBase
    {
        private Contatos _contatos;
        Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _contatos = new Contatos();
            _fixture = new Fixture();

        }

        [Test]
        public void AdicionarContatoTest()
        {
            var contact = _fixture.Create<Contato>();
            _contatos.Adicionar(contact);

   
            Assert.True(true);
        }

        [Test]
        public void ObterContatoTest()
        {
            var contato = _fixture.Create<Contato>();
            Contato ContatoResultado;
            
            _contatos.Adicionar(contato);
            ContatoResultado = _contatos.Obter(contato.Id);
            
            Assert.AreEqual(contato.Id, ContatoResultado.Id);
            Assert.AreEqual(contato.Nome, ContatoResultado.Nome);
        }

        [TearDown]
        public void TearDown()
        {
            _contatos = null;
            _fixture = null;
        }
    }
}
