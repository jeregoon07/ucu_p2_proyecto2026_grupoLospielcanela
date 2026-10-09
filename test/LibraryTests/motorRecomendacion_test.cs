using System;
using System.Collections.Generic;
using NUnit.Framework;
using Project;

namespace Project.Tests
{
    [TestFixture]
    public class MotorRecomendacionTests
    {
        [Test]
        public void Recomendar_ContenidoConsumido_NoLoIncluye()
        {
            Usuario usuario = new Usuario("u1", "Ana");
            Cancion escuchada = new Cancion("c1", "Escuchada", new string[0]);
            Cancion nueva = new Cancion("c2", "Nueva", new string[0]);
            usuario.RegistrarInteraccion(new Interaccion(usuario, escuchada, DateTime.Today));
            Catalogo catalogo = new Catalogo();
            catalogo.Agregar(escuchada); catalogo.Agregar(nueva);
            List<IRecomendable> resultado = new MotorRecomendacion().Recomendar(usuario, catalogo);
            Assert.That(resultado, Does.Not.Contain(escuchada));
            Assert.That(resultado, Does.Contain(nueva));
            }
        }
    }

