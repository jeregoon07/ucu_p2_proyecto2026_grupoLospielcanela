//------------------------------------------------------------------------------
// <copyright file="TrainTests.cs" company="Universidad Católica del Uruguay">
// Copyright (c) Programación II. Derechos reservados.
// </copyright>
//------------------------------------------------------------------------------

using NUnit.Framework;

namespace Library.Tests
{
    /// <summary>
    /// Prueba de la clase <see cref="Train"/>.
    /// </summary>
    [TestFixture]
    public class TrainTests
    {
        /// <summary>
        /// El tren para probar.
        /// </summary>
        private Train train;

        /// <summary>
        /// Crea un tren para probar.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.train = new Train();
        }

        /// <summary>
        /// Prueba que el tren arranque.
        /// </summary>
        [Test]
        public void StartEngines_BeforeStart_StartsEngine()
        {
            Assert.That(this.train, Is.Not.Null);
            this.train.StartEngines();
            Assert.That(this.train.IsEngineStarted, Is.True);
        }

        /// <summary>
        /// Prueba que el tren se detenga.
        /// </summary>
        [Test]
        public void StopEngines_EngineStarted_StopsEngine()
        {
            Assert.That(this.train, Is.Not.Null);
            this.train.StartEngines();
            this.train.StopEngines();
            Assert.That(this.train.IsEngineStarted, Is.False);
        }

        /// <summary>
        /// Prueba que no se puede arrancar el tren cuando ya está encendido.
        /// </summary>
        [Test]
        public void StartEngines_AlreadyStarted_ReturnsFalse()
        {
            Assert.That(this.train, Is.Not.Null);
            Assert.That(this.train.StartEngines(), Is.True);
            Assert.That(this.train.StartEngines(), Is.False);
            Assert.That(this.train.IsEngineStarted, Is.True);
        }

        /// <summary>
        /// Prueba que no se puede detener el tren cuando ya está detenido.
        /// </summary>
        [Test]
        public void StopEngines_AlreadyStopped_ReturnsFalse()
        {
            Assert.That(this.train, Is.Not.Null);
            Assert.That(this.train.StopEngines(), Is.False);
            Assert.That(this.train.IsEngineStarted, Is.False);
        }
    }
}
