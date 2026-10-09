//------------------------------------------------------------------------------
// <copyright file="EstrategiasRecomendacionTests.cs" company="Universidad Católica del Uruguay">
// Copyright (c) Programación II. Derechos reservados.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using NUnit.Framework;
using Project;

namespace Library.Tests
{
    /// <summary>
    /// Pruebas para las estrategias de recomendación.
    /// </summary>
    [TestFixture]
    public class EstrategiasRecomendacionTests
    {
        private static readonly string[] AtributosRock80s = { "Rock", "80s" };
        private static readonly string[] AtributosRockPop = { "Rock", "Pop" };
        private static readonly string[] AtributosJazz = { "Jazz" };
        private Catalogo catalogo;
        private Usuario usuario;
        private Cancion cancion1;
        private Cancion cancion2;
        private Cancion cancion3;
        [SetUp]
        public void Setup()
        {
            this.catalogo = new Catalogo();
            this.usuario = new Usuario("u1", "Juan Pérez");
            this.cancion1 = new Cancion("c1", "Rock 1", AtributosRock80s);
            this.cancion2 = new Cancion("c2", "Rock 2", AtributosRockPop);
            this.cancion3 = new Cancion("c3", "Jazz", AtributosJazz);
            this.catalogo.Agregar(this.cancion1);
            this.catalogo.Agregar(this.cancion2);
            this.catalogo.Agregar(this.cancion3);
        }
        [Test]
        public void RecomendarSegunPreferencias()
        {
            var estrategia = new EstrategiaPreferencia();
            this.usuario.ActualizarPreferencia(new Preferencia(AtributosRock80s));
            var resultado = estrategia.Recomendar(this.usuario, this.catalogo);
            Assert.That(resultado.Count, Is.EqualTo(2));
            Assert.That(resultado[0], Is.EqualTo(this.cancion1));
            Assert.That(resultado[1], Is.EqualTo(this.cancion2));
        }
        [Test]
        public void RecomendarSinPreferenciasRetornaVacio()
        {
            var estrategia = new EstrategiaPreferencia();
            var resultado = estrategia.Recomendar(this.usuario, this.catalogo);
            Assert.That(resultado, Is.Empty);
        }
        [Test]
        public void RecomendarSegunHistorial()
        {
            var estrategia = new EstrategiaHistorial();
            this.usuario.RegistrarInteraccion(new Interaccion(this.usuario, this.cancion1, DateTime.Now));
            var resultado = estrategia.Recomendar(this.usuario, this.catalogo);
            Assert.That(resultado.Count, Is.EqualTo(2));
            Assert.That(resultado[0], Is.EqualTo(this.cancion2));
            Assert.That(resultado[1], Is.EqualTo(this.cancion3));
        }
        [Test]
        public void RecomendarSegunPopularidad()
        {
            var estrategia = new EstrategiaPopularidad();
            var resultado = estrategia.Recomendar(this.usuario, this.catalogo);
            Assert.That(resultado.Count, Is.EqualTo(3));
        }
        [Test]
        public void RecomendarSegunRelacionados()
        {
            var estrategia = new EstrategiaRelacionados();
            var resultado = estrategia.Recomendar(this.usuario, this.catalogo, this.cancion1);
            Assert.That(resultado.Count, Is.EqualTo(1));
            Assert.That(resultado[0], Is.EqualTo(this.cancion2));
        }
    }
}