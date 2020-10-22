using Agenda.DAO;
using Agenta.Domain;
using AutoFixture;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.DAL.Tests
{
    [TestFixture]
    class Contatos2Test : TestBase
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
        public void ObterTodosOsContatosTest()
        {
            var contato1 = _fixture.Create<Contato>();
            var contato2 = _fixture.Create<Contato>();

            _contatos.Adicionar(contato1);
            _contatos.Adicionar(contato2);
            var obterTodos = _contatos.ObterTodos();
            var contatoResultado = obterTodos.Find(x => x.Id == contato1.Id);

            Assert.AreEqual(2, obterTodos.Count());
            Assert.AreEqual(contato1.Id, contatoResultado.Id);
            Assert.AreEqual(contato1.Nome, contatoResultado.Nome);
        }

        [TearDown]
        public void TearDown()
        {
            _contatos = null;
            _fixture = null;
        }
    }
}
