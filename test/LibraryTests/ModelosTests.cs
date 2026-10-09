using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Project;

namespace Project.Tests
{
    [TestFixture]
    public class ModelosTests
    {
        [Test]
        public void Cancion_CrearInstancia_AsignaPropiedadesCorrectamente()
        {
            // Arrange
            var atributos = new List<string> { "Rock", "Años 80" };
            
            // Act
            var cancion = new Cancion("C-01", "Bohemian Rhapsody", atributos);

            // Assert
            Assert.That(cancion.Id, Is.EqualTo("C-01"));
            Assert.That(cancion.Nombre, Is.EqualTo("Bohemian Rhapsody"));
            Assert.That(cancion.Atributos.Count, Is.EqualTo(2));
        }

        [Test]
        public void Cancion_AtributosNulos_CreaListaVacia()
        {
            // Act
            var cancion = new Cancion("C-02", "Cancion Sin Atributos", null);

            // Assert
            Assert.That(cancion.Atributos, Is.Not.Null);
            Assert.That(cancion.Atributos.Count, Is.EqualTo(0));
        }

        [Test]
        public void Catalogo_AgregarYBuscar_ItemSeAgregaYRecuperaExitosamente()
        {
            // Arrange
            var catalogo = new Catalogo();
            var cancion = new Cancion("C-01", "Tema 1", new List<string>());

            // Act
            catalogo.Agregar(cancion);
            var itemRecuperado = catalogo.BuscarPorId("C-01");

            // Assert
            Assert.That(itemRecuperado, Is.Not.Null);
            Assert.That(itemRecuperado.Nombre, Is.EqualTo("Tema 1"));
        }

        [Test]
        public void Catalogo_AgregarNull_LanzaArgumentNullException()
        {
            // Arrange
            var catalogo = new Catalogo();

            // Act & Assert (Usamos Action para evitar ambigüedad y advertencias)
            Assert.Throws<ArgumentNullException>(new Action(() => catalogo.Agregar(null)));
        }

        [Test]
        public void Catalogo_QuitarItem_ItemEsEliminadoDelCatalogo()
        {
            // Arrange
            var catalogo = new Catalogo();
            var cancion = new Cancion("C-01", "Tema 1", new List<string>());
            catalogo.Agregar(cancion);

            // Act
            catalogo.Quitar("C-01");
            var itemRecuperado = catalogo.BuscarPorId("C-01");

            // Assert
            Assert.That(itemRecuperado, Is.Null);
        }

        [Test]
        public void Usuario_CrearInstanciaValida_CreaUsuarioCorrectamente()
        {
            // Act
            var usuario = new Usuario("U-100", "Juan Perez");

            // Assert
            Assert.That(usuario.Id, Is.EqualTo("U-100"));
            Assert.That(usuario.Nombre, Is.EqualTo("Juan Perez"));
            Assert.That(usuario.Interacciones, Is.Empty);
        }

        [TestCase("", "Juan Perez")]
        [TestCase("U-100", "")]
        [TestCase(null, "Juan Perez")]
        public void Usuario_DatosInvalidos_LanzaArgumentException(string id, string nombre)
        {
            // Act & Assert (Usamos Action para evitar ambigüedad y advertencias)
            Assert.Throws<ArgumentException>(new Action(() => new Usuario(id, nombre)));
        }

        [Test]
        public void Usuario_GuardarContenido_AgregaItemAListaParaMasTarde()
        {
            // Arrange
            var usuario = new Usuario("U-100", "Juan");
            var cancion = new Cancion("C-01", "Tema 1", new List<string>());

            // Act
            usuario.GuardarContenido(cancion);
            var guardados = usuario.ObtenerGuardadosParaMasTarde();

            // Assert
            Assert.That(guardados.Count, Is.EqualTo(1));
            Assert.That(guardados.Contains(cancion), Is.True);
        }

        [Test]
        public void Usuario_RegistrarInteraccion_AgregaInteraccionAlHistorial()
        {
            // Arrange
            var usuario = new Usuario("U-100", "Juan");
            var cancion = new Cancion("C-01", "Tema 1", new List<string>());
            var interaccion = new Interaccion(usuario, cancion, DateTime.Now, 4.5);

            // Act
            usuario.RegistrarInteraccion(interaccion);
            var historial = usuario.ObtenerHistorial();

            // Assert
            Assert.That(historial.Count, Is.EqualTo(1));
            Assert.That(historial[0].Valoracion, Is.EqualTo(4.5));
        }

        [Test]
        public void Preferencia_CrearConNull_CreaListaVacia()
        {
            // Act
            var preferencia = new Preferencia(null);

            // Assert
            Assert.That(preferencia.AtributosPreferidos, Is.Not.Null);
            Assert.That(preferencia.AtributosPreferidos.Count, Is.EqualTo(0));
        }

        [Test]
        public void Interaccion_CrearConUsuarioItemNulos_LanzaExcepcion()
        {
            // Arrange
            var usuario = new Usuario("U-100", "Juan");
            var cancion = new Cancion("C-01", "Tema", new List<string>());

            // Act & Assert (Usamos Action para evitar ambigüedad y advertencias)
            Assert.Throws<ArgumentNullException>(new Action(() => new Interaccion(null, cancion, DateTime.Now)));
            Assert.Throws<ArgumentNullException>(new Action(() => new Interaccion(usuario, null, DateTime.Now)));
        }
    }
}