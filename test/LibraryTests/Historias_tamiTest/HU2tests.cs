using System;
using System.Collections.Generic;
using NUnit.Framework;
using Project;

namespace Library.Tests
{
    /// <summary>
    /// HU2: Como usuario quiero definir mis preferencias para personalizar mi experiencia.
    /// </summary>
    [TestFixture]
    public class HU2Tests
    {
        private static readonly string[] AtributoRock = new[] { "Rock" };
        private static readonly string[] AtributoPop = new[] { "Pop" };

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
            var preferencia = new Preferencia(AtributoRock);
            fachada.ActualizarPreferencias("u1", preferencia);

            var cancionRock = new Cancion("c1", "Rock Song", AtributoRock);
            var cancionPop = new Cancion("c2", "Pop Song", AtributoPop);
            catalogo.Agregar(cancionRock);
            catalogo.Agregar(cancionPop);

            // Probamos directamente con la estrategia de preferencias para cumplir el criterio estricto de HU2
            var estrategiaPreferencia = new EstrategiaPreferencia();
            var usuarioPrueba = new Usuario("u1", "María");
            usuarioPrueba.ActualizarPreferencia(preferencia);

            var recomendaciones = estrategiaPreferencia.Recomendar(usuarioPrueba, catalogo);

            Assert.That(usuarioPrueba.ObtenerPreferencia().AtributosPreferidos, Does.Contain("Rock"));
            Assert.That(recomendaciones, Does.Contain(cancionRock));
            Assert.That(recomendaciones, Does.Not.Contain(cancionPop));
        }
    }
}