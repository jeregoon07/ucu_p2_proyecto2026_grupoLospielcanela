//-----------------------------------------------------------------------------
// <copyright file="HU4Tests.cs" company="Universidad Católica del Uruguay">
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
    /// HU4
    /// </summary>
    [TestFixture]
    public class HU4Tests
    {
        private Catalogo catalogo;
        private Usuario usuario;
        private Cancion cancionRock;
        private Cancion cancionPop;
        private Cancion cancionJazz;
        /// <summary>
        /// Configuración inicial de los objetos de prueba para HU4.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.catalogo = new Catalogo();
            this.usuario = new Usuario("u1", "Lucía");
            this.cancionRock = new Cancion("c1", "Rock Song", new[] { "Rock" });
            this.cancionPop = new Cancion("c2", "Pop Song", new[] { "Pop" });
            this.cancionJazz = new Cancion("c3", "Jazz Song", new[] { "Jazz" });
            this.catalogo.Agregar(this.cancionRock);
            this.catalogo.Agregar(this.cancionPop);
            this.catalogo.Agregar(this.cancionJazz);
        }
        /// <summary>
        /// Valida que el sistema use las interacciones previas del usuario 
        /// y excluya de las recomendaciones el contenido que ya fue consumido.
        /// </summary>
        [Test]
        public void ExcluyeConsumidosSegunInteraccionesDeAntes()
        {
            var estrategia = new EstrategiaHistorial();
            // Criterio: El sistema utiliza interacciones previas del usuario.
            this.usuario.RegistrarInteraccion(new Interaccion(this.usuario, this.cancionRock, DateTime.Now));
            var recomendaciones = estrategia.Recomendar(this.usuario, this.catalogo);
            // Criterio: Se generan recomendaciones distintas al contenido ya consumido.
            Assert.That(recomendaciones, Does.Not.Contain(this.cancionRock));
            Assert.That(recomendaciones, Is.Not.Empty);
        }
        /// <summary>
        /// Valida que cualquier cambio o adición en el historial de interacciones 
        /// modifique el resultado de las recomendaciones obtenidas.
        /// </summary>
        [Test]
        public void ElHistorialAfectaLasRecomendaciones()
        {
            var estrategia = new EstrategiaHistorial();
            // Estado inicial del historial con una interacción
            this.usuario.RegistrarInteraccion(new Interaccion(this.usuario, this.cancionRock, DateTime.Now));
            var recomendacionesIniciales = estrategia.Recomendar(this.usuario, this.catalogo);
            // Criterio: Cambios en el historial afectan las recomendaciones.
            // Agregamos una nueva interacción al historial del usuario.
            this.usuario.RegistrarInteraccion(new Interaccion(this.usuario, this.cancionPop, DateTime.Now));
            var recomendacionesModificadas = estrategia.Recomendar(this.usuario, this.catalogo);
            // Verificamos que las listas difieran debido a la actualización del historial.
            Assert.That(recomendacionesModificadas, Is.Not.EqualTo(recomendacionesIniciales));
        }
    }
}