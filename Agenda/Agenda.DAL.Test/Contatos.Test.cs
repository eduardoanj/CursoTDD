using System;
using NUnit.Framework;

namespace Agenda.DAL.Test
{
    [TestFixture]
    public class ContatosTest
    {
        Contatos _contatos;

        [SetUp]
        public void SetUp()
        {
            _contatos = new Contatos();

        }

        [Test]
        public void IncluirContatoTest()
        {
            //arrange
            string id = Guid.NewGuid().ToString();
            string nome = "Dudu";
            //act
            _contatos.Adicionar(id, nome);

            //assert
            Assert.True(true);

        }

        [TearDown]
        public void TearDown()
        {
            _contatos = null;
        }
    }
}
