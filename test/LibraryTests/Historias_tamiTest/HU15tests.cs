using System;
using System.Collections.Generic;
using NUnit.Framework;
using Project;

namespace Library.Tests
{
    /// <summary>
    /// HU16: Como administrador quiero eliminar ítems para mantener el catálogo actualizado.
    /// Criterios de aceptación:
    /// - Se pueden eliminar ítems existentes.
    /// - Los ítems eliminados no aparecen en recomendaciones.
    /// - No se rompe el sistema si un ítem fue consumido previamente.
    /// </summary>
    [TestFixture]
    public class HU16Tests
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
        public void AdministradorEliminaItemYNoApareceEnRecomendaciones()
        {
            fachada.RegistrarUsuario("u1", "Lucía");
            var cancion = new Cancion("c1", "ToDelete", new[] { "Rock" });
            catalogo.Agregar(cancion);

            // Verificamos que se elimina del catálogo usando el método Quitar de Catalogo
            catalogo.Quitar("c1");

            Assert.That(catalogo.BuscarPorId("c1"), Is.Null);
            
            var recomendaciones = fachada.ObtenerRecomendaciones("u1");
            Assert.That(recomendaciones, Does.Not.Contain(cancion));
        }
    }
}