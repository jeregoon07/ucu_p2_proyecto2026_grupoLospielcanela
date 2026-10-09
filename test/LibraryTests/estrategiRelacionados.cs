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
        [Test]
        public void Constructordevuelvenull()
        {
            try
            {
                var estrategia = new EstrategiaRelacionado(null);
                Assert.Fail("Se esperaba una ArgumenNullException");
            }
            catch (ArgumentNullException)
            {
                Assert.Pass();
            }
        }
        [Test]
        public void ConstrucorDevuelveValor()
        {
            var itemBase = new ItemFalso
            {
                Id="1",
                Atributos= new List<string> {"accion"}
            };
            var itemSimilar=new ItemFalso
            {
                Id="2",
                Atributos= new List<string> {"accion", "aventura"}
            };
            var catalogo= new Catalogo();
            catalogo.Agregar(itemSimilar);
            var estrategia= new EstrategiaRelacionado(itemBase);
            var resultado = estrategia.Recomendar(null, catalogo);
            Assert.IsNotNull(resultado);
            Assert.AreEqual(1, resultado.Count);
            Assert.AreEqual(itemSimilar, resultado[0]);

        }
        public class ItemFalso:IRecomendable
        {
            public string Id {get; set;}
            public string Nombre{get;set;}
            public IReadOnlyList<string> Atributos{get;set;}
        }
    }
}

