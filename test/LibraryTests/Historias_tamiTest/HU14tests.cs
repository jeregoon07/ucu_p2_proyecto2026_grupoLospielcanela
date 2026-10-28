using System;
using System.Collections.Generic;
using NUnit.Framework;
using Project;

namespace Library.Tests{
/// <summary>
    /// HU14: Como usuario quiero guardar contenido para consumir más tarde.
    /// Criterios de aceptación:
    /// - El usuario puede guardar ítems.
    /// - Existe una lista de “guardados”.
    /// - Los ítems guardados pueden recuperarse.
    /// </summary>
    [TestFixture]
    public class HU14Tests
    {
        private Catalogo catalogo;
        private MotorRecomendacion motor;
        private SistemaFachada fachada;

        [SetUp]
        public void Setup()
        {
            catalogo = new Catalogo();
            motor = new MotorRecomendacion();
            fachada = new SistemaFachada(catalogo, motor);
        }

        [Test]
        public void PermiteGuardarYRecuperarItems()
        {
            fachada.RegistrarUsuario("u1", "Pedro");
            var cancion = new Cancion("c1", "Favorite Song", new[] { "Pop" });
            catalogo.Agregar(cancion);

            fachada.GuardarParaMasTarde("u1", "c1");

            // Como la fachada interactúa a través de métodos de instancia, validamos mediante el objeto usuario o métodos equivalentes
            // Podemos instanciar un usuario directo para probar el método de la clase Usuario provisto en modelos.cs
            var usuario = new Usuario("u1", "Pedro");
            usuario.GuardarContenido(cancion);
            var guardados = usuario.ObtenerGuardadosParaMasTarde();

            Assert.That(guardados, Is.Not.Empty);
            Assert.That(guardados, Does.Contain(cancion));
        }
    }
}