using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Project
{
    public class TestHU13
    {
        [Test]
        public void FiltroExcluirConsumido_QuitaItemsDelHistorial()
        {
            var usuario = new Usuario("U3", "LectorFrecuente");
            var cancionYaEscuchada = new Cancion("C7", "Clasico", new List<string> { "Rock" });
            var interaccion = new Interaccion(usuario, cancionYaEscuchada, DateTime.Now, null);
            usuario.RegistrarInteraccion(interaccion);
            var listaSugerida = new List<IRecomendable>
            {
                cancionYaEscuchada,
                new Cancion("C8", "Nueva Cancion", new List<string> { "Rock" })
            };
            var filtro = new FiltroRecomendacion();
            var resultadoFinal = filtro.ExcluirConsumidos(usuario, listaSugerida);
            bool contieneCancionNueva = false;
            bool contieneCancionVieja = false;
            int cantidad = 0;
            foreach (Cancion r in resultadoFinal)
            {
                cantidad++;
                if (r.Nombre == "Nueva Cancion") contieneCancionNueva = true;
                if (r.Nombre == "Clasico") contieneCancionVieja = true;
            }
            Assert.That(cantidad, Is.EqualTo(1));
            Assert.That(contieneCancionVieja, Is.False);
            Assert.That(contieneCancionNueva, Is.True);
        }
    }
}