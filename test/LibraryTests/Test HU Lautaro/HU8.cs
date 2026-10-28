using System.Collections.Generic;
using NUnit.Framework;

namespace Project
{
    public class TestHU8
    {
        [Test]
        public void AplicarFiltro_ExcluyeAtributo_RefinaResultados()
        {
            var listaPrevia = new List<IRecomendable>
            {
                new Cancion("C5", "Cancion 1", new List<string> { "Metal" }),
                new Cancion("C6", "Cancion 2", new List<string> { "Jazz" })
            };
            var filtro = new FiltroRecomendacion();
            var atributosAExcluir = new List<string> { "Metal" };
            var resultadoFinal = filtro.ExcluirPorAtributos(listaPrevia, atributosAExcluir);
            bool contieneCancion2 = false;
            int cantidad = 0;
            foreach (Cancion r in resultadoFinal)
            {
                cantidad++;
                if (r.Nombre == "Cancion 2") contieneCancion2 = true;
            }
            Assert.That(cantidad, Is.EqualTo(1));
            Assert.That(contieneCancion2, Is.True);
        }
    }
}