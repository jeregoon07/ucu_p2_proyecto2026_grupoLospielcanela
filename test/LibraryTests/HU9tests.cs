//-----------------------------------------------------------------------------
// <copyright file="HU9Tests.cs" company="Universidad Católica del Uruguay">
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
    /// HU9.
    /// </summary>
    [TestFixture]
    public class HU9Tests
    {
        private Catalogo catalogo;
        private Usuario usuario;
        private Cancion cancionZ;
        private Cancion cancionA;
        [SetUp]
        public void Setup()
        {
            this.catalogo = new Catalogo();
            this.usuario = new Usuario("u1", "Juan");
            // Invertimos los IDs para que el orden por relevancia y el alfabético sean distintos
            this.cancionZ = new Cancion("c2", "Zamba", new[] { "Rock" });
            this.cancionA = new Cancion("c1", "Amor", new[] { "Rock" });
            this.catalogo.Agregar(this.cancionZ);
            this.catalogo.Agregar(this.cancionA);
        }
        [Test]
        public void DefineCriterioPresentaOrdenadoYCambiarCriterioModificaOrden()
        {
            var estrategia = new EstrategiaOrdenada();
            // Criterio 1 y 2: El sistema define un criterio y presenta los resultados ordenados
            EstrategiaOrdenada.CriterioOrden = "Relevancia";
            var resultadosRelevancia = estrategia.Recomendar(this.usuario, this.catalogo);
            Assert.That(resultadosRelevancia, Is.Not.Null);
            Assert.That(resultadosRelevancia, Has.Count.EqualTo(2));
            // Criterio 3: Cambiar el criterio modifica el orden resultante
            EstrategiaOrdenada.CriterioOrden = "Nombre";
            var resultadosNombre = estrategia.Recomendar(this.usuario, this.catalogo);
            // Validamos que al cambiar a "Nombre", el orden cambia y queda primero "Amor" (cancionA)
            Assert.That(resultadosNombre[0], Is.EqualTo(this.cancionA));
            Assert.That(resultadosNombre, Is.Not.EqualTo(resultadosRelevancia));
        }
    }
}