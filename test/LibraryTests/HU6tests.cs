//-----------------------------------------------------------------------------
// <copyright file="HU6Tests.cs" company="Universidad Católica del Uruguay">
// Copyright (c) Programación II. Derechos reservados.
// </copyright>
//-----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using NUnit.Framework;
using Project;
namespace Library.Tests
{
    /// <summary>
    /// HU6.
    /// </summary>
    [TestFixture]
    public class HU6Tests
    {
        private Catalogo catalogo;
        private Usuario usuarioPrincipal;
        private Usuario usuarioSimilar;
        private Cancion cancion1;
        private Cancion cancion2;
        private Cancion cancion3;
        /// <summary>
        /// Configuración inicial de los objetos de prueba para HU6.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.catalogo = new Catalogo();
            this.usuarioPrincipal = new Usuario("u1", "Juan");
            this.usuarioSimilar = new Usuario("u2", "María");
            this.cancion1 = new Cancion("c1", "Canción 1", new[] { "Rock" });
            this.cancion2 = new Cancion("c2", "Canción 2", new[] { "Pop" });
            this.cancion3 = new Cancion("c3", "Canción 3", new[] { "Jazz" });
            this.catalogo.Agregar(this.cancion1);
            this.catalogo.Agregar(this.cancion2);
            this.catalogo.Agregar(this.cancion3);
        }
        /// <summary>
        /// Valida que el sistema identifique usuarios con comportamiento similar,
        /// recomiende los ítems consumidos por ellos y excluya aquellos que el usuario principal ya haya consumido.
        /// </summary>
        [Test]
        public void VerUsuariosSimilaresYrecomiendaitems()
        {
            var estrategia = new EstrategiaUsuarioSimilares();
            // Criterio: El sistema identifica usuarios con comportamiento similar .
            this.usuarioPrincipal.RegistrarInteraccion(new Interaccion(this.usuarioPrincipal, this.cancion1, DateTime.Now));
            this.usuarioSimilar.RegistrarInteraccion(new Interaccion(this.usuarioSimilar, this.cancion1, DateTime.Now));
            // El usuario similar también consumió cancion2
            this.usuarioSimilar.RegistrarInteraccion(new Interaccion(this.usuarioSimilar, this.cancion2, DateTime.Now));
            EstrategiaUsuarioSimilares.TodosLosUsuarios = new List<Usuario> { this.usuarioSimilar };
            var recomendaciones = estrategia.Recomendar(this.usuarioPrincipal, this.catalogo);
            // Criterio: Se recomiendan ítems consumidos por usuarios similares.
            Assert.That(recomendaciones, Has.Count.EqualTo(1));
            Assert.That(recomendaciones[0], Is.EqualTo(this.cancion2));
            // Criterio: No se incluyen ítems ya consumidos por el usuario principal.
            Assert.That(recomendaciones, Does.Not.Contain(this.cancion1));
        }
        /// <summary>
        /// Valida que si no existen usuarios con comportamiento similar,
        /// el sistema no retorne recomendaciones colaborativas vaciando la lista correctamente.
        /// </summary>
        [Test]
        public void SiNoHayUnComportamientoSimilarDaVacio()
        {
            var estrategia = new EstrategiaUsuarioSimilares();
            // El usuario principal consume cancion1, el otro usuario consume cancion3 
            this.usuarioPrincipal.RegistrarInteraccion(new Interaccion(this.usuarioPrincipal, this.cancion1, DateTime.Now));
            this.usuarioSimilar.RegistrarInteraccion(new Interaccion(this.usuarioSimilar, this.cancion3, DateTime.Now));
            EstrategiaUsuarioSimilares.TodosLosUsuarios = new List<Usuario> { this.usuarioSimilar };
            var recomendaciones = estrategia.Recomendar(this.usuarioPrincipal, this.catalogo);
            Assert.That(recomendaciones, Is.Empty);
        }
    }
}