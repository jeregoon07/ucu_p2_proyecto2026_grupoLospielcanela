//-----------------------------------------------------------------------------
// <copyright file="HU15Tests.cs" company="Universidad Católica del Uruguay">
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
    /// HU15.
    /// </summary>
    [TestFixture]
    public class HU15Tests
    {
        private Catalogo catalogo;
        private Usuario usuario;
        private Cancion itemBaseRock;
        private Cancion itemSimilarRock;
        private Cancion itemBasePop;
        /// <summary>
        /// Configuración inicial de los objetos de prueba para HU15.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.catalogo = new Catalogo();
            this.usuario = new Usuario("u1", "Juan");
            this.itemBaseRock = new Cancion("c1", "Rock 1", new[] { "Rock", "Guitarra" });
            this.itemSimilarRock = new Cancion("c2", "Rock 2", new[] { "Rock", "Bateria" });
            this.itemBasePop = new Cancion("c3", "Pop 1", new[] { "Pop", "Bailable" });
            this.catalogo.Agregar(this.itemBaseRock);
            this.catalogo.Agregar(this.itemSimilarRock);
            this.catalogo.Agregar(this.itemBasePop);
        }
        /// <summary>
        /// Valida que el sistema identifica atributos del ítem base, recomienda ítems similares
        /// y que el resultado cambia según el ítem seleccionado.
        /// </summary>
        [Test]
        public void IdentificaAtributosYRecomiendaSimilaresSegunItemBase()
        {
            var estrategia = new EstrategiaRelacionados();
            // Criterio 1 y 2: Identifica atributos del ítem base y recomienda similares (comparten "Rock")
            var recomendacionesRock = estrategia.Recomendar(this.usuario, this.catalogo, this.itemBaseRock);
            Assert.That(recomendacionesRock, Does.Contain(this.itemSimilarRock));
            Assert.That(recomendacionesRock, Does.Not.Contain(this.itemBasePop));
            // Criterio 3: El resultado cambia según el ítem seleccionado
            var recomendacionesPop = estrategia.Recomendar(this.usuario, this.catalogo, this.itemBasePop);
            Assert.That(recomendacionesPop, Is.Empty);
            Assert.That(recomendacionesPop, Is.Not.EqualTo(recomendacionesRock));
        }
    }
}