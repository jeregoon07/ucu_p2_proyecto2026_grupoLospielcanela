using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Security;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Project;

namespace Library.Tests
{
    [TestFixture]
    public class EstrategiaRelacionadotest
    {
        private static readonly string[] AtributoAccion = new[] { "accion" };
        private static readonly string[] AtributosAccionAventura = new[] { "accion", "aventura" };

        [Test]
        public void Constructordevuelvenull()
        {
            Assert.Throws<ArgumentNullException>(() => new EstrategiaRelacionado(null));
        }

        [Test]
        public void ConstrucorDevuelveValor()
        {
            var itemBase = new ItemFalso
            {
                Id = "1",
                Atributos = AtributoAccion
            };
            var itemSimilar = new ItemFalso
            {
                Id = "2",
                Atributos = AtributosAccionAventura
            };
            var catalogo = new Catalogo();
            catalogo.Agregar(itemSimilar);
            var estrategia = new EstrategiaRelacionado(itemBase);
            var resultado = estrategia.Recomendar(null, catalogo);
            
            Assert.That(resultado, Is.Not.Null);
            Assert.That(resultado.Count, Is.EqualTo(1));
            Assert.That(resultado[0], Is.EqualTo(itemSimilar));
        }

        public class ItemFalso : IRecomendable
        {
            public string Id { get; set; }
            public string Nombre { get; set; }
            public IReadOnlyList<string> Atributos { get; set; }
        }
    }
}