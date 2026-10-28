using System.Collections.Generic;
using NUnit.Framework;

namespace Project
{
    public class HU5
    {
        [Test]
        public void Recomendacion_CoincideConPreferencia_ModificaResultados()
        {
            var usuario = new Usuario("U1", "TestUser");
            usuario.ActualizarPreferencia(new Preferencia(new List<string> { "Rock" }));
            var catalogo = new Catalogo();
            catalogo.Agregar(new Cancion("C1", "Cancion A", new List<string> { "Rock" }));
            catalogo.Agregar(new Cancion("C2", "Cancion B", new List<string> { "Pop" }));
            var estrategia = new EstrategiaPreferencia();
            var resultado = estrategia.Recomendar(usuario, catalogo);
            bool contieneCancionA = false;
            bool contieneCancionB = false;
            foreach (Cancion r in resultado)
            {
                if (r.Nombre == "Cancion A") contieneCancionA = true;
                if (r.Nombre == "Cancion B") contieneCancionB = true;
            }
            Assert.That(contieneCancionA, Is.True);
            Assert.That(contieneCancionB, Is.False);
        }
    }
}