using NUnit.Framework;
using System.Collections.Generic;
using Project;

namespace Project.Tests
{
    [TestFixture]
    public class CriterioOrdenTests
    {
        private class ItemPrueba : IRecomendable
        {
            public string Id { get; }
            public string Nombre { get; }
            public IReadOnlyList<string> Atributos { get; }

            public ItemPrueba(string id, string nombre, List<string> atributos)
            {
                Id = id;
                Nombre = nombre;
                Atributos = atributos.AsReadOnly();
            }
        }

        [Test]
        public void OrdenarListaFinal_ListaDesordenada_OrdenaItemsAlfabeticamentePorNombre()
        {
            var criterio = new CriterioOrden();
            var items = new List<IRecomendable>
            {
                new ItemPrueba("1", "Yellow", new List<string>()),
                new ItemPrueba("2", "Bohemian Rhapsody", new List<string>()),
                new ItemPrueba("3", "Hotel California", new List<string>())
            };

            var resultado = criterio.OrdenarListaFinal(items);

            Assert.That(resultado.Count, Is.EqualTo(3));
            Assert.That(resultado[0].Nombre, Is.EqualTo("Bohemian Rhapsody"));
            Assert.That(resultado[1].Nombre, Is.EqualTo("Hotel California"));
            Assert.That(resultado[2].Nombre, Is.EqualTo("Yellow"));
        }

        [Test]
        public void OrdenarListaFinal_ListaVacia_RetornaListaVaciaSinErrores()
        {
            var criterio = new CriterioOrden();
            var items = new List<IRecomendable>();

            var resultado = criterio.OrdenarListaFinal(items);

            Assert.That(resultado, Is.Empty);
        }

        [Test]
        public void OrdenarPorRelevancia_DiferentesCoincidencias_OrdenaPorMayorCantidadDeCoincidencias()
        {
            var criterio = new CriterioOrden();
            
            var preferencias = new Preferencia(new List<string> { "Rock", "Clasico", "Vocal" });

            var itemSinCoincidencia = new ItemPrueba("1", "Despacito", new List<string> { "Pop", "Latino", "Urbano" });
            
            var itemDosCoincidencias = new ItemPrueba("2", "Let It Be", new List<string> { "Rock", "Vocal", "Piano" });
            
            var itemTresCoincidencias = new ItemPrueba("3", "Stairway to Heaven", new List<string> { "Rock", "Clasico", "Vocal", "Acustico" });
            
            var itemUnaCoincidencia = new ItemPrueba("4", "Smells Like Teen Spirit", new List<string> { "Rock", "Grunge", "Alternativo" });

            var items = new List<IRecomendable> 
            { 
                itemSinCoincidencia, 
                itemDosCoincidencias, 
                itemTresCoincidencias, 
                itemUnaCoincidencia 
            };

            var resultado = criterio.OrdenarPorRelevancia(items, preferencias);

            Assert.That(resultado.Count, Is.EqualTo(4));
            Assert.That(resultado[0].Nombre, Is.EqualTo("Stairway to Heaven"));    
            Assert.That(resultado[1].Nombre, Is.EqualTo("Let It Be"));             
            Assert.That(resultado[2].Nombre, Is.EqualTo("Smells Like Teen Spirit"));
            Assert.That(resultado[3].Nombre, Is.EqualTo("Despacito"));             
        }
    }
}