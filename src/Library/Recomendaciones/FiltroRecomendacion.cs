using System.Collections.Generic;
using System.Linq;

namespace Project
{
    public class FiltroRecomendacion
    {
        public List<IRecomendable> ExcluirConsumidos(Usuario usuario, List<IRecomendable> recomendaciones)
        {
            var idsConsumidos = usuario.ObtenerHistorial()
                .Select(interaccion => interaccion.Item.Id)
                .ToHashSet();
            return recomendaciones.Where(item => !idsConsumidos.Contains(item.Id)).ToList();
        }
        public List<IRecomendable> ExcluirPorAtributos(List<IRecomendable> recomendaciones, List<string> atributosNoDeseados)
        {
            return recomendaciones.Where(item => 
                !item.Atributos.Any(atributoItem => atributosNoDeseados.Contains(atributoItem))
            ).ToList();
        }
        public List<IRecomendable> AplicarFiltros(Usuario usuario, List<IRecomendable> recomendaciones, List<string> atributosNoDeseados)
        {
            var listaFiltrada = ExcluirConsumidos(usuario, recomendaciones);
            listaFiltrada = ExcluirPorAtributos(listaFiltrada, atributosNoDeseados);
            return listaFiltrada;
        }
    }
}