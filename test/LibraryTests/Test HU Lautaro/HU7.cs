using System.Collections.Generic;
using NUnit.Framework;

namespace Project
{
    public class TestHU7
    {
        [Test]
        public void UsuarioNuevo_SinHistorial_AplicaEstrategiaPopularidad()
        {
            var usuarioNuevo = new Usuario("U2", "NuevoUser");
            var catalogo = new Catalogo();
            catalogo.Agregar(new Cancion("C3", "Hit del momento", new List<string> { "Pop" }));
            catalogo.Agregar(new Cancion("C4", "Cancion Indie", new List<string> { "Indie" }));
            var estrategia = new EstrategiaPopularidad();
            var resultado = estrategia.Recomendar(usuarioNuevo, catalogo);
            bool tieneResultados = false;
            foreach (Cancion r in resultado)
            {
                tieneResultados = true;
            }
            Assert.That(tieneResultados, Is.True);
        }
    }
}