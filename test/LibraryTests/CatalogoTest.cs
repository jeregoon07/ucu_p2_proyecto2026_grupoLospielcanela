using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Security;
using Microsoft.VisualBasic;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Project;

namespace Library.Tests
{
    [TestFixture]
    public class CatalogoTest
    {
        public void AgregarYBuscaPor()
        {
            var catalogo = new Catalogo();
            var itemPrueba = new ItemFalso
            {
                Id= "99",
                Nombre= "objeto de prueba",
                Atributos= new List<string> {"prueba"}
            };
            catalogo.Agregar(itemPrueba);
            var itemEncontrado= catalogo.BuscarPorId("99");
            Assert.IsNotNull(itemEncontrado);
            Assert.AreEqual("99", itemEncontrado.Id);
            Assert.AreEqual(itemPrueba, itemEncontrado);
        }
    }
    public class ItemFalso:IRecomendable
        {
            public string Id {get; set;}
            public string Nombre{get;set;}
            public IReadOnlyList<string> Atributos{get;set;}
        }
    

}