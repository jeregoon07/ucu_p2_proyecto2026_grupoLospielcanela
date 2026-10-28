using System;
using System.Collections.Generic;
using NUnit.Framework;
using Project;

namespace Library.Tests
{
    /// <summary>
    /// HU1: Como usuario quiero registrarme para poder utilizar el sistema.
    /// Criterios de aceptación:
    /// - Dado que no estoy registrado, cuando ingreso mis datos, entonces se crea una cuenta válida.
    /// - El sistema valida que el usuario no exista previamente.
    /// - El usuario queda habilitado para usar el sistema.
    /// </summary>
    [TestFixture]
    public class HU1Tests
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
        public void RegistrarUsuarioNuevoCreaCuentaValida()
        {
            var usuario = fachada.RegistrarUsuario("u1", "Carlos");

            Assert.That(usuario, Is.Not.Null);
            Assert.That(usuario.Id, Is.EqualTo("u1"));
            Assert.That(usuario.Nombre, Is.EqualTo("Carlos"));
        }

        [Test]
        public void NoPermiteRegistrarUsuarioExistente()
        {
            fachada.RegistrarUsuario("u1", "Ana");

            var ex = Assert.Throws<InvalidOperationException>(() => 
                fachada.RegistrarUsuario("u1", "Ana Duplicada")
            );
            Assert.That(ex.Message, Does.Contain("ya existe"));
        }
    }
}