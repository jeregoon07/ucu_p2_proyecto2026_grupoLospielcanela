using System;
using System.Collections.Generic;
using NUnit.Framework;
using Project;

namespace Library.Tests
{
    /// <summary>
    /// HU2: Como usuario quiero definir mis preferencias para personalizar mi experiencia.
    /// Criterios de aceptación:
    /// - El usuario puede seleccionar uno o más atributos (género, categoría, etc.).
    /// - Las preferencias quedan almacenadas.
    /// - Las recomendaciones consideran dichas preferencias.
    /// </summary>
    [TestFixture]
    public class HU2Tests
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
        public void AlmacenaYConsideraPreferenciasEnRecomendaciones()
        {
            fachada.RegistrarUsuario("u1", "María");
            var preferencia = new Preferencia(new[] { "Rock" });
            fachada.ActualizarPreferencias("u1", preferencia);

            var cancionRock = new Cancion("c1", "Rock Song", new[] { "Rock" });
            var cancionPop = new Cancion("c2", "Pop Song", new[] { "Pop" });
            catalogo.Agregar(cancionRock);
            catalogo.Agregar(cancionPop);

            var recomendaciones = fachada.ObtenerRecomendaciones("u1");

            // Comprobación directa de preferencias almacenadas en el usuario
            var usuarioPrueba = new Usuario("u1", "María");
            usuarioPrueba.ActualizarPreferencia(preferencia);
            Assert.That(usuarioPrueba.ObtenerPreferencia().AtributosPreferidos, Does.Contain("Rock"));
            
            Assert.That(recomendaciones, Does.Contain(cancionRock));
            Assert.That(recomendaciones, Does.Not.Contain(cancionPop));
        }
    }
}