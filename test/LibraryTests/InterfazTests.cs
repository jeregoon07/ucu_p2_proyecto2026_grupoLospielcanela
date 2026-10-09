using NUnit.Framework;
using System.Collections.Generic;
using Project;

namespace Project.Tests
{
    [TestFixture]
    public class InterfazTests
    {
        private class RecomendableDummy : IRecomendable
        {
            public string Id { get; set; }
            public string Nombre { get; set; }
            public IReadOnlyList<string> Atributos { get; set; }
        }

        [Test]
        public void IRecomendable_Contrato_SeCumpleCorrectamente()
        {
            string idEsperado = "R-001";
            string nombreEsperado = "Dummy";
            var atributosEsperados = new List<string> { "Test", "DummyAtributo" };
            IRecomendable item = new RecomendableDummy
            {
                Id = idEsperado,
                Nombre = nombreEsperado,
                Atributos = atributosEsperados
            };
            Assert.That(item.Id, Is.EqualTo(idEsperado));
            Assert.That(item.Nombre, Is.EqualTo(nombreEsperado));
            Assert.That(item.Atributos, Is.Not.Null);
            Assert.That(item.Atributos.Count, Is.EqualTo(2));
            Assert.That(item.Atributos, Contains.Item("Test"));
        }
    }
}